using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.IDAL;
using ZoomLa.DALFactory;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
/// <summary>
/// B_ADZone 的摘要说明
/// </summary>
public class B_ADZone
{
    public B_ADZone()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    private static readonly ID_Adzone adzone = IDal.CreateADZone();
    /// <summary>
    /// 激活版位
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static bool ADZone_Active(int strZoneId)
    {
        return adzone.ADZone_Active(strZoneId);
    }
    /// <summary>
    /// 批量激活
    /// </summary>
    /// <param name="ZoneIds"></param>
    /// <returns></returns>
    public static bool BatchActive(string ZoneIds)
    {
        return adzone.BatchActive(ZoneIds);
    }
    /// <summary>
    /// 添加版位
    /// </summary>
    /// <param name="adZone"></param>
    /// <returns></returns>
    public static bool ADZone_Add(M_Adzone adZone)
    {
        if (!adzone.ADZone_Add(adZone))
        {
            return false;
        }
        else
        {
            CreateJS(adZone.ZoneID.ToString());
        }
        return true;
    }
    /// <summary>
    /// 创建JS文件
    /// </summary>
    /// <param name="id"></param>
    public static void CreateJS(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            string[] strArray = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            B_ADZoneJs adjs = new B_ADZoneJs();
            for (int i = 0; i < strArray.Length; i++)
            {
                M_Adzone adZoneById = getAdzoneByZoneId(DataConverter.CLng(strArray[i]));
                if (adZoneById.Active)
                {
                    IList<M_Advertisement> aDList = B_Advertisement.GetADList(adZoneById.ZoneID);
                    if (aDList.Count >= 0)
                    {
                        adjs.CreateJS(adZoneById, aDList);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 查询某个版位的广告列表
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static DataTable ADZone_GetExportList(string strZoneId)
    {
        return adzone.ADZone_GetExportList(DataConverter.CLng(strZoneId));
    }
    /// <summary>
    /// 修改版位
    /// </summary>
    /// <param name="adZone"></param>
    /// <returns></returns>
    public static bool ADZone_Update(M_Adzone adZone)
    {
        if (!adzone.ADZone_Update(adZone))
        {
            return false;
        }
        else
        {
            CreateJS(adZone.ZoneID.ToString());
        }
        return true;        
    }
    /// <summary>
    /// 暂停版位
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static bool ADZone_Pause(string strZoneId)
    {
        return adzone.ADZone_Pause(DataConverter.CLng(strZoneId));
    }
    /// <summary>
    /// 批量暂停
    /// </summary>
    /// <param name="ZoneIDs"></param>
    /// <returns></returns>
    public static bool BatchPause(string ZoneIDs)
    {
        return adzone.BatchPause(ZoneIDs);
    }
    /// <summary>
    /// 获取所有版位
    /// </summary>
    /// <returns></returns>
    public static DataTable ADZone_GetAll()
    {
        return adzone.ADZone_GetAll();
    }
    /// <summary>
    /// 获取某个广告所属第一个版位
    /// </summary>
    /// <param name="adverseId"></param>
    /// <returns></returns>
    public static M_Adzone getAdzones(int adverseId)
    {
        return adzone.getAdzone(adverseId);
    }
    /// <summary>
    /// 获取广告所属版位的ID数组
    /// </summary>
    /// <param name="adverseId"></param>
    /// <returns></returns>
    public static int[] getAdzoneS(int adverseId)
    {
        return adzone.getAdzoneS(adverseId);
    }
    /// <summary>
    /// 获取最大的版位ID
    /// </summary>
    /// <returns></returns>
    public static int ADZone_MaxID()
    {   
        return adzone.ADZone_MaxID();
    }
    /// <summary>
    /// 删除版位
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static bool ADZone_Remove(string strZoneId)
    {
        return adzone.ADZone_Remove(DataConverter.CLng(strZoneId));
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ZoneIds"></param>
    /// <returns></returns>
    public static bool BatchRemove(string ZoneIds)
    {
        return adzone.BatchRemove(ZoneIds);
    }
    /// <summary>
    /// 清空版位
    /// </summary>
    /// <param name="adzoneid"></param>
    public static void ADZone_Clear(int adzoneid)
    {
         adzone.ADZone_Clear(adzoneid) ;
    }
    /// <summary>
    /// 复制版位
    /// </summary>
    /// <param name="adzoneid"></param>
    /// <returns></returns>
    public static bool ADZone_Copy(int adzoneid)
    {
        return adzone.ADZone_Copy(adzoneid);        
    }
    /// <summary>
    /// 获取指定版位的详细信息
    /// </summary>
    /// <param name="adzoneid"></param>
    /// <returns></returns>
    public static M_Adzone getAdzoneByZoneId(int adzoneid)
    {
        return adzone.getAdzoneByZoneId(adzoneid);
    }
    /// <summary>
    /// 条件查询指定的版位
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public static  DataTable ADZone_ByCondition(string con)
    {
        return adzone.ADZone_ByCondition(con);
    }
    /// <summary>
    /// 统计某个版位的广告数
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static  int ADZone_Count(int strZoneId)
    {
        return adzone.ADZone_Count(strZoneId);
    }
    /// <summary>
    /// 删除某个广告所在的版位
    /// </summary>
    /// <param name="ADID"></param>
    /// <returns></returns>
    public static bool Delete_ADZone_Ad(string ADID)
    {
        return adzone.Delete_ADZone_Ad(ADID);    
    }

}
