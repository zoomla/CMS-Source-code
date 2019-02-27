using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

/*
 * 功能:来源检测,因不能同时占用*.aspx,所以合并,并提升性能
 */ 
namespace URLRewriter
{
    class ReferCheck
    {
        //如果检测通过则继续访问,检测不通过则直接跳到404页面
        /// <summary>
        /// 来源检测，暂不增加限定为本站
        /// </summary>
        /// <param name="context"></param>
        /// <returns>true为有来源或不需检测,false为无来源</returns>
         public static bool ReferChecker(HttpContext context)
        {
             bool flag = true;
             try
             {
                 //这里如果报错,则是其值为空,直接返回true,如果这里不报则值改为false;
                 if (string.IsNullOrEmpty(context.Application["needCheckRefer"] as string)) return true;
                 string needCheckRefer = context.Application["needCheckRefer"].ToString().ToLower();
                 string url = context.Request.Url.AbsoluteUri.ToLower();//目标网站，登录页不检测
                 if (url.IndexOf("login") > 0) return true;
                 for (int i = 0; i < needCheckRefer.Split(',').Length; i++)
                 {
                     if (url.Contains(needCheckRefer.Split(',')[i]) && context.Request.UrlReferrer == null)
                         flag = false;
                 }
             }
             catch (System.NullReferenceException)
             {
                 return flag;
             }
             return flag;
         }
    }
}
