namespace ZoomLa.BLL
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
    using ZoomLa.IDAL;
    using ZoomLa.DALFactory;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Collections;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;

    /// <summary>
    /// B_Advertisement 的摘要说明
    /// </summary>
    public class B_Advertisement
    {
        public B_Advertisement()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private static readonly ID_Advertisement advertisement = IDal.CreateAdvertisement();
        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <returns></returns>
        public static bool Advertisement_Add(M_Advertisement Advertisement)
        {
            return advertisement.Advertisement_Add(Advertisement);          
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        public static bool Advertisement_CancelPassed(string StrAdId)
        {
            return advertisement.Advertisement_CancelPassed(StrAdId);
        }
        /// <summary>
        /// 设为审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        public static bool Advertisement_SetPassed(string StrAdId)
        {
            return advertisement.Advertisement_SetPassed(StrAdId);
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <returns></returns>
        public static bool Advertisement_Update(M_Advertisement Advertisement)
        {
            return advertisement.Advertisement_Update(Advertisement);
        }
        /// <summary>
        /// 获取通过审核的某版位的广告列表
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public static IList<M_Advertisement> GetADList(int zoneId)
        {
            return advertisement.GetADList(zoneId);
        }        
        /// <summary>
        /// 读取所有广告
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAdvertisementList()
        {
            return advertisement.GetAllAdvertisementList();
        }
        /// <summary>
        /// 获取广告的最大ID
        /// </summary>
        /// <returns></returns>
        public static int MaxID()
        {
            return advertisement.MaxID();
        }
        /// <summary>
        /// 获取某个版位的所有广告ID的数组
        /// </summary>
        /// <param name="adverseId"></param>
        /// <returns></returns>
        public static int[] getAdIds(int adverseId)
        {
           return advertisement.getAdIds(adverseId);
        }
        /// <summary>
        /// 删除某个广告
        /// </summary>
        /// <param name="strADID"></param>
        /// <returns></returns>
        public static bool Advertisement_Delete(string strADID)
        {
            return advertisement.Advertisement_Delete(strADID);
        }
        /// <summary>
        /// 获取某个广告的详细信息
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static M_Advertisement Advertisement_GetAdvertisementByid(int adid)
        {
            return advertisement.Advertisement_GetAdvertisementByid(adid);
        }
        /// <summary>
        /// 复制某个广告
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static bool Advertisement_Copy(int adid)
        {
            return advertisement.Advertisement_Copy(adid);
        }
        /// <summary>
        /// 获取某个版位内广告的列表
        /// </summary>
        /// <param name="zoneid"></param>
        /// <returns></returns>
        public static DataTable Advertisement_GetAdvertisementByZoneid(int zoneid)
        {
            return advertisement.Advertisement_GetAdvertisementByZoneid(zoneid);
        }
        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static DataTable Advertisement_SelectByCondition(string con)
        {            
            return advertisement.Advertisement_SelectByCondition(con);
        }
        /// <summary>
        /// 插入关联信息
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="ADId"></param>
        /// <returns></returns>
        public static  bool Add_Zone_Advertisement(int zoneID, int ADId)
        {
            return advertisement.Add_Zone_Advertisement(zoneID, ADId);        
        }
        /// <summary>
        /// 获取某广告所属的所有版位ID组成的字符串
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public static string GetZoneIDByAd(int ADID)
        {
            return advertisement.GetZoneIDsByAdvID(ADID);
        }
        /// <summary>
        /// 是否存在指定版位ID和广告ID的关联信息
        /// </summary>
        /// <param name="ZoneID"></param>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public static bool IsExistZoneAdv(int ZoneID, int ADID)
        {
            return advertisement.IsExistZoneAdv(ZoneID, ADID);
        }

        public static string GetAdContent(M_Advertisement advertisementInfo)
        {
            StringBuilder builder2;
            StringBuilder builder = new StringBuilder();
            switch (advertisementInfo.AdType)
            {
                case 1:
                    {
                        builder2 = new StringBuilder();
                        builder2.Append("<img src=\"");
                        string imgUrl = advertisementInfo.ImgUrl;
                        if (((!imgUrl.StartsWith("/") && !imgUrl.StartsWith("~/")) && !imgUrl.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) && !imgUrl.StartsWith("https", StringComparison.CurrentCultureIgnoreCase))
                        {
                            builder2.Append(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath));
                            //builder2.Append(VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir));
                            builder2.Append(imgUrl);
                            break;
                        }                        
                        builder2.Append(imgUrl);
                        break;
                    }
                case 2:
                    builder.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\"");
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder.Append(" width=\"");
                        builder.Append(advertisementInfo.ImgWidth);
                        builder.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder.Append("  height=\"");
                        builder.Append(advertisementInfo.ImgHeight);
                        builder.Append("\"");
                    }
                    builder.Append("><param name=\"movie\" value=\"");
                    builder.Append(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath));                    
                    builder.Append(advertisementInfo.ImgUrl);
                    builder.Append("\">");
                    if (advertisementInfo.FlashWmode == 1)
                    {
                        builder.Append("<param name=\"wmode\" value=\"transparent\">");
                    }
                    builder.Append("<param name=\"quality\" value=\"autohigh\">");
                    builder.Append("<embed src=\"");
                    builder.Append(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath));
                    //builder.Append(VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir));
                    builder.Append(advertisementInfo.ImgUrl);
                    builder.Append("\" quality=\"autohigh\"  pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\"");
                    if (advertisementInfo.FlashWmode == 1)
                    {
                        builder.Append(" wmode=\"transparent\" ");
                    }
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder.Append(" width=\"");
                        builder.Append(advertisementInfo.ImgWidth);
                        builder.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder.Append("  height=\"");
                        builder.Append(advertisementInfo.ImgHeight);
                        builder.Append("\"");
                    }
                    builder.Append("></embed></object>");
                    goto Label_04B5;

                case 3:
                    builder.Append(advertisementInfo.ADIntro);
                    goto Label_04B5;

                case 4:
                    builder.Append(advertisementInfo.ADIntro);
                    goto Label_04B5;

                case 5:
                    builder.Append("<iframe id=\"AD_");
                    builder.Append(advertisementInfo.AdId);
                    builder.Append("\" marginwidth=0 marginheight=0 hspace=0 vspace=0 frameborder=0 scrolling=no width=100% height=100% src=\"");
                    builder.Append(advertisementInfo.ADIntro);
                    builder.Append("\">AD</iframe>");
                    goto Label_04B5;

                default:
                    goto Label_04B5;
            }
            builder2.Append("\"");
            if (advertisementInfo.ImgWidth > 0)
            {
                builder2.Append(" width=\"");
                builder2.Append(advertisementInfo.ImgWidth);
                builder2.Append("\"");
            }
            if (advertisementInfo.ImgHeight > 0)
            {
                builder2.Append("  height=\"");
                builder2.Append(advertisementInfo.ImgHeight);
                builder2.Append("\"");
            }
            builder2.Append(" border=\"0\"></img>");
            if (!string.IsNullOrEmpty(advertisementInfo.LinkUrl))
            {
                builder.Append("<a href=\"");
                builder.Append(advertisementInfo.LinkUrl);
                builder.Append("\"");
                if (advertisementInfo.LinkTarget == 0)
                {
                    builder.Append(" target=\"_self\"");
                }
                else
                {
                    builder.Append(" target=\"_blank\"");
                }
                builder.Append(" title=\"");
                builder.Append(advertisementInfo.LinkAlt);
                builder.Append("\"");
                builder.Append(">");
                builder.Append(builder2.ToString());
                builder.Append("</a>");
            }
            else
            {
                builder = builder2;
            }
        Label_04B5:
            return builder.ToString();
        }
    }
}