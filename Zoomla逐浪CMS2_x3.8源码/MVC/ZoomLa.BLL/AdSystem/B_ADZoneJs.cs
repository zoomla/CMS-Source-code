namespace ZoomLa.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;    
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web;

    /// <summary>
    /// B_ADZoneJs 的摘要说明
    /// </summary>
    public class B_ADZoneJs
    {
        private M_Advertisement advertisementInfo;
        private Dictionary<int, string> zoneConfig = new Dictionary<int, string>();
        private M_Adzone zoneInfo;
        
        public B_ADZoneJs()
        {
            this.zoneConfig.Add(0, "Banner");
            this.zoneConfig.Add(1, "Pop");
            this.zoneConfig.Add(2, "Move");
            this.zoneConfig.Add(3, "Fixed");
            this.zoneConfig.Add(4, "Float");
            this.zoneConfig.Add(5, "Code");
            this.zoneConfig.Add(6, "Couplet");
        }
        public string[] GetFileSize()
        {
            int count = this.zoneConfig.Count;
            string[] strArray = new string[count];
            for (int i = 0; i < (count); i++)
            {
                if (FileSystemObject.IsExist(GetJsTemplatePath() + this.GetTemplateName(i), FsoMethod.File))
                {
                    strArray[i] = FileSystemObject.GetFileSize(GetJsTemplatePath() + this.GetTemplateName(i));
                }
                else
                {
                    strArray[i] = "0.0KB";
                }
            }
            return strArray;
        }
        private static bool CheckJSName(string name)
        {
            Regex regex = new Regex(@"^[\w-]+/?\w+\.js$");
            bool flag = false;
            if (regex.IsMatch(name))
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 生成广告对象的JS代码
        /// </summary>
        /// <returns></returns>
        private string CreatAdvertisementJS()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var objAD = new ObjectAD();\n");
            builder.Append("objAD.ADID= " + this.advertisementInfo.ADID + ";");
            builder.Append("objAD.ADType= " + this.advertisementInfo.ADType + ";");
            builder.Append("objAD.ADName= \"" + this.advertisementInfo.ADName + "\";");
            string imgUrl = this.advertisementInfo.ImgUrl;
            if (((imgUrl.StartsWith("/") || imgUrl.StartsWith("~/")) || imgUrl.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) || imgUrl.StartsWith("https", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.Append("objAD.ImgUrl= \"" + imgUrl + "\";");
            }
            else
            {
                builder.Append("objAD.ImgUrl= \"" + VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath) + this.advertisementInfo.ImgUrl + "\";");
            }
            string adintro = this.advertisementInfo.ADIntro;
            adintro = adintro.Replace("\"", "'");
            adintro = adintro.Replace("\n", "");
            adintro = adintro.Replace("\r", "");

            builder.Append("objAD.ImgWidth       = " + this.advertisementInfo.ImgWidth + ";");
            builder.Append("objAD.ImgHeight      = " + this.advertisementInfo.ImgHeight + ";");
            builder.Append("objAD.FlashWmode     = " + this.advertisementInfo.FlashWmode + ";");
            builder.Append("objAD.ADIntro =\"" + adintro + "\";");
            builder.Append("objAD.LinkUrl        = \"" + this.advertisementInfo.LinkUrl + "\";");
            builder.Append("objAD.LinkTarget     = " + this.advertisementInfo.LinkTarget + ";");
            builder.Append("objAD.LinkAlt        = \"" + this.advertisementInfo.LinkAlt + "\";");
            builder.Append("objAD.Priority       = " + this.advertisementInfo.Priority + ";");
            builder.Append("objAD.CountView      = " + this.advertisementInfo.CountView.ToString().ToLower() + ";");
            builder.Append("objAD.CountClick     = " + this.advertisementInfo.CountClick.ToString().ToLower() + ";");
            builder.Append("objAD.OverdueDate    = \"" + this.advertisementInfo.OverdueDate.ToString("yyyy") + "/" + this.advertisementInfo.OverdueDate.ToString("MM") + "/" + this.advertisementInfo.OverdueDate.ToString("dd") + "\";");
            builder.Append("objAD.InstallDir     = \"" + VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath) + "\";");
            builder.Append("objAD.ADDIR= \"" + SiteConfig.SiteOption.AdvertisementDir + "\";");
            builder.Append("ZoneAD_" + this.advertisementInfo.ZoneID + ".AddAD(objAD);");
            return builder.ToString();
        }
        /// <summary>
        /// 生成JS代码版位对象属性JS代码
        /// </summary>    
        /// <returns></returns>
        private string CreateADZoneJS()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ZoneID=", this.zoneInfo.ZoneID, ";" }));
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ZoneWidth=", this.zoneInfo.ZoneWidth, ";" }));
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ZoneHeight=", this.zoneInfo.ZoneHeight, ";" }));
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ShowType=", this.zoneInfo.ShowType, ";" }));
            builder.Append(this.GetZoneTypeJS());
         
            builder.Append("ZoneAD_" + this.zoneInfo.ZoneID + ".Show();");
            if (this.zoneInfo.Active==false)
                builder.Append("}");
            return builder.ToString();
        }
        /// <summary>
        /// 生成版位设置属性JS代码
        /// </summary>
        /// <returns></returns>
        private string GetZoneTypeJS()
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = (this.zoneInfo.ZoneSetting + ",,,,,").Split(new char[] { ',' });
            switch (this.zoneInfo.ZoneType)
            {
                case 1:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".PopType = \"", strArray[0], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Left= ", DataConverter.CLng(strArray[1]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Top= ", DataConverter.CLng(strArray[2]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".CookieHour  = \"", strArray[3], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".LocalityType = \"", strArray[4], "\";" }));
                    break;
                case 2:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Left=", DataConverter.CLng(strArray[0]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Top=", DataConverter.CLng(strArray[1]).ToString(), ";" }));
                    if (!string.IsNullOrEmpty(strArray[2]))
                    {
                        builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Delta=\"", strArray[2], "\";" }));
                    }
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ShowCloseAD=\"", strArray[3], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".CloseFontColor=\"", strArray[4], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".LocalityType = \"", strArray[5], "\";" }));
                    break;

                case 3:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Left= ", DataConverter.CLng(strArray[0]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Top= ", DataConverter.CLng(strArray[1]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ShowCloseAD=\"", strArray[2], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".CloseFontColor=\"", strArray[3], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".LocalityType = \"", strArray[4], "\";" }));
                    break;

                case 4:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".FloatType= \"", strArray[0], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Left= ", DataConverter.CLng(strArray[1]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Top= ", DataConverter.CLng(strArray[2]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ShowCloseAD=\"", strArray[3], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".CloseFontColor=\"", strArray[4], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".LocalityType = \"", strArray[5], "\";" }));
                    break;

                case 6:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Left=", DataConverter.CLng(strArray[0]).ToString(), ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Top=", DataConverter.CLng(strArray[1]).ToString(), ";" }));
                    if (!string.IsNullOrEmpty(strArray[2]))
                    {
                        builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".Delta=\"", strArray[2], "\";" }));
                    }
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".ShowCloseAD=\"", strArray[3], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneID, ".CloseFontColor=\"", strArray[4], "\";" }));
                    break;
            }
            return builder.ToString();
        }
        /// <summary>
        /// 创建广告版位JS
        /// </summary>
        /// <param name="adZoneInfo"></param>
        /// <param name="advertisementInfoList"></param>
        public void CreateJS(M_Adzone adZoneInfo, IList<M_Advertisement> advertisementInfoList)
        {
            //将当前版位对象设置为要操作的版位对象
            this.zoneInfo = adZoneInfo;

            //读取模板基本代码
            StringBuilder builder = new StringBuilder(this.GetZoneJSTemplate());
            if (this.zoneInfo.Active == false)
            {
                builder.Insert(0, "var jsshow=0;if (jsshow==1 ){");

            }
            //定义版位对象
            builder.Append("var ZoneAD_" + adZoneInfo.ZoneID + "=new ");
            builder.Append(string.Concat(new object[] { this.zoneConfig[adZoneInfo.ZoneType], "ZoneAD(\"ZoneAD_", adZoneInfo.ZoneID, "\");" }));
            //将广告对象定义加入到JS代码中
            for (int i = 0; i < advertisementInfoList.Count; i++)
            {
                this.advertisementInfo = advertisementInfoList[i];
                this.advertisementInfo.ZoneID = adZoneInfo.ZoneID;
                if (this.advertisementInfo.Passed && (this.advertisementInfo.Days >= 0))
                {
                    builder.Append(this.CreatAdvertisementJS());
                }
            }

            //加入版位属性定义
            builder.Append(this.CreateADZoneJS());
            FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath("~/" + SiteConfig.SiteOption.AdvertisementDir)) + adZoneInfo.ZoneJSName, builder.ToString());
        }


        /// <summary>
        /// 获取JS模板内容
        /// </summary>
        /// <returns></returns>
        public string GetZoneJSTemplate()
        {
            return this.GetADZoneJSTemplateContent(this.zoneInfo.ZoneType);
        }
        /// <summary>
        /// 根据版位的类型获取相应模板的内容
        /// </summary>
        /// <param name="zoneType"></param>
        /// <returns></returns>
        public string GetADZoneJSTemplateContent(int zoneType)
        {
            string templateName = this.GetTemplateName(zoneType);
            return FileSystemObject.ReadFile(GetJsTemplatePath() + templateName);
        }
        /// <summary>
        /// 根据类型获取版位的模板文件名
        /// </summary>
        /// <param name="zoneType"></param>
        /// <returns></returns>
        public string GetTemplateName(int zoneType)
        {
            return ("Template_" + this.zoneConfig[zoneType] + ".js");
        }
        /// <summary>
        /// 获取模板目录
        /// </summary>
        /// <returns></returns>
        private static string GetJsTemplatePath()
        {
            string advertisementDir = SiteConfig.SiteOption.AdvertisementDir;
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                advertisementDir = current.Server.MapPath("~/" + advertisementDir);
            }
            else
            {
                advertisementDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, advertisementDir);
            }
            return (VirtualPathUtility.AppendTrailingSlash(advertisementDir) + "ADTemplate/");
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public bool SaveJSTemplate(string template,int zoneType)
        {
            string templateName = this.GetTemplateName(zoneType);
            try
            {
                SafeSC.WriteFile(GetJsTemplatePath() + templateName,template);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}