namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;

    /// <summary>
    /// B_FunLabel 的摘要说明
    /// </summary>
    public class B_FunLabel
    {
        public B_FunLabel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public string GetFunLabel()
        {
            string funlabel = "";

            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:GetNodeUrl(节点ID)/}\">获得节点链接</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:GetInfoUrl(内容ID)/}\">获得内容页链接</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:GetNodeOpen(节点ID)/}\">节点打开方式</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:GetInfoOpen(节点ID)/}\">项目打开方式</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:TimeNow()/}\">当前时间</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:DateNow()/}\">当前日期</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:DateAndTime()/}\">当前日期时间</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:ConverToWeek(日期时间)/}\">将日期转换成星期</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:FormatDate(日期时间,格式字符串)/}\">日期时间格式化</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:CutText(字符串,长度,后缀)/}\">固定长度的字符串</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:SplitDown(下载地址)/}\">下载地址连接</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:SplitPicUrl(图片地址数组)/}\">多图片链接</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:GetPicUrlCount(图片地址数组)/}\">字段图片链接数</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:GetPicUrl(图片地址)/}\">图片链接地址</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:PreInfoID(当前内容ID)/}\">当前内容上一篇</div>";
            funlabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL:NextInfoID(当前内容ID)/}\">当前内容下一篇</div>";
            return funlabel;
        }
        public string GetSysLabel()
        {
            string syslabel = "";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$SiteName/}\">网站名称</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$SiteURL/}\">网站Url</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$SiteTitle/}\">网站标题</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$MetaKeywords/}\">网站关键字</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$MetaDescription/}\">网站摘要</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$LogoUrl/}\">网站LOGO</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$Banner/}\">网站Banner</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$Webmaster/}\">网站站长</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$WebmasterEmail/}\">网站Email</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$Copyright/}\">版权信息</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$UploadDir/}\">上传目录</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$ManageDir/}\">管理目录</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$CssDir/}\">CSS目录</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{$AdDir/}\">广告目录</div>";
            syslabel += "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{ZL.Page/}\">分页导航</div>";
            return syslabel;
        }
    }
}