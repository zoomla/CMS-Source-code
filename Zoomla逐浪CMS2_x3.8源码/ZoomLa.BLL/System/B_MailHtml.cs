namespace ZoomLa.BLL
{
using System;
using System.Data;
using System.Text.RegularExpressions;
using ZoomLa.Model;
using System.Collections;
using ZoomLa.SQLDAL;
using System.Text;
using System.Data.SqlClient;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Globalization;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using ZoomLa.BLL.FTP;
using ZoomLa.Model.FTP;
using System.Net;

   public class B_MailHtml
   {
       /// <summary>
       /// 网站上传目录
       /// </summary>
       private string UploadDir = SiteConfig.SiteOption.UploadDir;
       /// <summary>
       /// 网站地址
       /// </summary>
       private string SiteUrl = SiteConfig.SiteInfo.SiteUrl;
       /// <summary>
       /// 网站名称
       /// </summary>
       private string SiteName = SiteConfig.SiteInfo.SiteName;
       /// <summary>
       /// 网站标题
       /// </summary>
       private string SiteTitle = SiteConfig.SiteInfo.SiteTitle;
       private string MetaKeywords = SiteConfig.SiteInfo.MetaKeywords;
       private string MetaDescription = SiteConfig.SiteInfo.MetaDescription;
       /// <summary>
       /// 网站LOGO
       /// </summary>
       private string LogoUrl = SiteConfig.SiteInfo.LogoUrl;
       /// <summary>
       /// 
       /// </summary>
       private string BannerUrl = SiteConfig.SiteInfo.BannerUrl;
       private string Webmaster = SiteConfig.SiteInfo.Webmaster;
       private string WebmasterEmail = SiteConfig.SiteInfo.WebmasterEmail;
       private string Copyright = SiteConfig.SiteInfo.Copyright;
       private string ManageDir = SiteConfig.SiteOption.ManageDir;
       private string CssDir = SiteConfig.SiteOption.CssDir;
       private string StylePath = SiteConfig.SiteOption.StylePath;
       private string AdvertisementDir = SiteConfig.SiteOption.AdvertisementDir;
       private string JS = SiteConfig.SiteOption.JS;
       private string SiteID = SiteConfig.SiteOption.SiteID;
       private string LogoAdmin = SiteConfig.SiteInfo.LogoAdmin;
       B_User buser = new B_User();
       public string SysLabelProc(string syslabel,string Title)
       {
           M_Uinfo muinfo = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
           //处理系统标签获得函数后的内容
           string lbl = syslabel;
           lbl = lbl.Replace("{$", "").Replace("/}", "");
           switch (lbl)
           {
               case "SiteName":
                   return SiteName;

               case "SiteURL":
                   return "<a href='"+SiteUrl+"' alt='"+SiteName+"' target='_blank'>"+SiteUrl+"</a>";

               case "SiteTitle":
                   return SiteTitle;

               case "MetaKeywords":
                   return MetaKeywords;

               case "MetaDescription":
                   return MetaDescription;

               case "LogoUrl":
                   return "<img src='"+LogoUrl+"' width='206' height='40' />";

               case "Banner":
                   return BannerUrl;

               case "Webmaster":
                   return Webmaster;

               case "WebmasterEmail":
                   return WebmasterEmail;

               case "Copyright":
                   return Copyright;

               case "UserName":
                   return buser.GetLogin().UserName;

               case "password":
                   return buser.GetLogin().UserPwd;

               case "Company"://风格路径
                   return buser.GetLogin().CompanyName;

               case "OfficePhone":
                   return muinfo.OfficePhone;
               case "HoneyName":
                       return muinfo.HoneyName;
               case "UserFace":
                       return "<img src='" + muinfo.UserFace + "' width='32' height='32' />";
               case "sex":
                   if (muinfo.UserSex == true)
                       return "男";
                   else
                       return "女";
               case "BirthDay":
                   return muinfo.BirthDay;
               case "Country":
                   return muinfo.Country;
               case "Province":
                   return muinfo.Province;
               case "City":
                   return muinfo.City;
               case "County":
                   return muinfo.County;
               case "Address":
                   return muinfo.Address;
               case "ZipCode":
                   return muinfo.ZipCode;
               case "Mobile":
                   return muinfo.Mobile;
               case "Fax":
                   return muinfo.Fax;
               case "Email":
                   return "<a href='mailto:" + buser.GetLogin().Email + "'>" + buser.GetLogin().Email + "</a>";
               case "HomePage":
                   return muinfo.HomePage;
               case "QQ":
                   return muinfo.QQ;
               case "MSN":
                   return muinfo.MSN;
               case "Sign":
                   return muinfo.Sign; 
               case "Title":
                   return Title;
               default:
                   return "[err:站群标签引用错误]";

           }
       }
    }
}
