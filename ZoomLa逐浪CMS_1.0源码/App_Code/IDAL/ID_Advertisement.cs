namespace ZoomLa.IDAL
{   
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Model;
    using System.Collections.Generic;
    using System.Collections;
    /// <summary>
    /// ID_Advertisement 的摘要说明
    /// </summary>
    public interface ID_Advertisement
    {
        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <returns></returns>
        bool Advertisement_Add(Model.M_Advertisement Advertisement);
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        bool Advertisement_CancelPassed(string StrAdId);
        /// <summary>
        /// 设为审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        bool Advertisement_SetPassed(string StrAdId);
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <returns></returns>
        bool Advertisement_Update(M_Advertisement Advertisement);
        /// <summary>
        /// 删除某个广告
        /// </summary>
        /// <param name="strADID"></param>
        /// <returns></returns>
        bool Advertisement_Delete(string strADID);
        /// <summary>
        /// 获取通过审核的某版位的广告列表
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        IList<M_Advertisement> GetADList(int zoneId);
        /// <summary>
        /// 读取所有广告
        /// </summary>
        /// <returns></returns>
        DataTable GetAllAdvertisementList();
        /// <summary>
        /// 获取广告的最大ID
        /// </summary>
        /// <returns></returns>
        int MaxID();
        /// <summary>
        /// 获取某个版位的所有广告ID的数组
        /// </summary>
        /// <param name="ZoneID"></param>
        /// <returns></returns>
        int[] getAdIds(int ZoneID);
        /// <summary>
        /// 获取某个广告的详细信息
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        M_Advertisement Advertisement_GetAdvertisementByid(int adid);
        /// <summary>
        /// 复制某个广告
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        bool Advertisement_Copy(int adid);
        /// <summary>
        /// 获取某个版位内广告的列表
        /// </summary>
        /// <param name="zoneid"></param>
        /// <returns></returns>
        DataTable Advertisement_GetAdvertisementByZoneid(int zoneid);
        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        DataTable Advertisement_SelectByCondition(string con);
        /// <summary>
        /// 插入关联信息
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="ADId"></param>
        /// <returns></returns>
        bool Add_Zone_Advertisement(int zoneID, int ADId);
        /// <summary>
        /// 获取某广告所属的所有版位ID组成的字符串
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        string GetZoneIDsByAdvID(int ADID);
        /// <summary>
        /// 是否存在指定版位ID和广告ID的关联信息
        /// </summary>
        /// <param name="ZoneID"></param>
        /// <param name="ADID"></param>
        /// <returns></returns>
        bool IsExistZoneAdv(int ZoneID, int ADID);
    }
}