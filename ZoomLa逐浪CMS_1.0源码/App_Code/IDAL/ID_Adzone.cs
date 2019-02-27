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
    /// <summary>
    /// ID_Adzone 的摘要说明
    /// </summary>
    public interface ID_Adzone
    {
        /// <summary>
        /// 设置版位激活
        /// </summary>
        /// <param name="ZoneId"></param>
        /// <returns></returns>
        bool ADZone_Active(int ZoneId);
        /// <summary>
        /// 批量激活版位
        /// </summary>
        /// <param name="ZoneIds"></param>
        /// <returns></returns>
        bool BatchActive(string ZoneIds);
        /// <summary>
        /// 添加版位
        /// </summary>
        /// <param name="adZone"></param>
        /// <returns></returns>
        bool ADZone_Add(M_Adzone adZone);
        /// <summary>
        /// 查询某个版位的广告列表
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        DataTable ADZone_GetExportList(int ZoneId);
        /// <summary>
        /// 统计某个版位的广告数
        /// </summary>
        /// <param name="ZoneId"></param>
        /// <returns></returns>
        int ADZone_Count(int ZoneId);
        /// <summary>
        /// 修改版位
        /// </summary>
        /// <param name="adZone"></param>
        /// <returns></returns>
        bool ADZone_Update(M_Adzone adZone);
        /// <summary>
        /// 暂停版位活动
        /// </summary>
        /// <param name="ZoneId"></param>
        /// <returns></returns>
        bool ADZone_Pause(int ZoneId);
        /// <summary>
        /// 批量暂停版位
        /// </summary>
        /// <param name="ZoneIDs"></param>
        /// <returns></returns>
        bool BatchPause(string ZoneIDs);
        /// <summary>
        /// 获取最大的版位ID
        /// </summary>
        /// <returns></returns>
        int ADZone_MaxID();
        /// <summary>
        /// 获取所有版位列表
        /// </summary>
        /// <returns></returns>
        DataTable ADZone_GetAll();
        /// <summary>
        /// 获取某个广告所属第一个版位
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        M_Adzone getAdzone(int advId);
        /// <summary>
        /// 获取广告所属版位的ID数组
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        int[] getAdzoneS(int advId);
        /// <summary>
        /// 删除版位
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        bool ADZone_Remove(int ZoneId);
        /// <summary>
        /// 批量删除版位
        /// </summary>
        /// <param name="ZoneIds"></param>
        /// <returns></returns>
        bool BatchRemove(string ZoneIds);
        /// <summary>
        /// 删除某个版位里的广告
        /// </summary>
        /// <param name="adzoneid"></param>
        void ADZone_Clear(int adzoneid);
        /// <summary>
        /// 复制版位
        /// </summary>
        /// <param name="adzoneid"></param>
        /// <returns></returns>
        bool ADZone_Copy(int adzoneid);
        /// <summary>
        /// 获取指定版位的详细信息
        /// </summary>
        /// <param name="adzoneid"></param>
        /// <returns></returns>
        M_Adzone getAdzoneByZoneId(int adzoneid);
        /// <summary>
        /// 条件查询指定的版位
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        DataTable ADZone_ByCondition(string con);
        /// <summary>
        /// 删除某个广告所在的版位
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        bool Delete_ADZone_Ad(string ADID);
    }
}