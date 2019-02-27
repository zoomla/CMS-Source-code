using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZoomLa.Common;
/*
 * Ueditor的一些辅助处理
 */ 
namespace ZoomLa.BLL.Helper
{
    public class UEHelper
    {
        public string GetSubTitle(string msg)
        {
            string text = StringHelper.StripHtml(msg, 500).Replace(" ", "");
            string result = (text.Length > 50 ? text.Substring(0, 50) + "..." : text) + "<br/><ul class='thumbul'>";
            RegexHelper regHelper = new RegexHelper();
            int need = 3;
            int curCount = 0;
            if (msg.Contains("edui-faked-video"))//在线视频,如不以swf结尾,则直接显示链接
            {
                string qvtlp = "<li class='thumbli'><img src='/App_Themes/Guest/images/Bar/videologo.png' data-type='quotevideo' data-content='{0}'/></li>";
                //只取其引用,不存实体
                MatchCollection mcs = regHelper.GetValuesBySE(msg, "<embed", "/>");
                for (int i = 0; i < need && i < mcs.Count; i++)
                {
                    string src = regHelper.GetHtmlAttr(mcs[i].Value, "src");//引用区分大小写
                    if (Path.GetExtension(src).Equals(".swf"))
                    {
                        result += string.Format(qvtlp, src);
                        curCount++;
                    }
                    else
                    {
                        msg = msg.Replace(mcs[i].Value, string.Format("<a href='{0}'>{0}</a>", src));
                    }
                }
            }
            if (msg.Contains("<video ") && curCount < need)//上传的视频文件
            {
                string videotlp = "<li class='thumbli'><img src='/App_Themes/Guest/images/Bar/videologo.png' data-type='video' data-content='{0}'/></li>";
                msg = msg.Replace("<video", " <video");
                MatchCollection mcs = regHelper.GetValuesBySE(msg, "<video", ">");
                for (int i = 0; i < need && i < mcs.Count && curCount < need; i++)
                {
                    string src = regHelper.GetHtmlAttr(mcs[i].Value, "src");
                    result += string.Format(videotlp, src);
                    curCount++;
                }
            }
            if (msg.Contains("<img ") && curCount < need)//图片
            {
                MatchCollection mcs = regHelper.GetValuesBySE(msg, "<img", "/>");//匹配图片文件
                for (int i = 0; i < need && i < mcs.Count && curCount < need; i++)
                {
                    if (mcs[i].Value.Contains("/Ueditor")) { continue; }//不存表情
                    result += "<li class='thumbli'>" + mcs[i].Value + "</li>";
                    curCount++;
                }
            }
            return result += "</ul>";
        }
    }
}
