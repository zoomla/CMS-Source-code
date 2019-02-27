using System;
using System.IO;
using System.Web.UI;
using System.Web;
using URLRewriter.Config;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace URLRewriter
{
	/// <summary>
	/// 提供HttpHandler跳转
	/// </summary>
	/// <remarks>The RewriterFactoryHandler checks the rewriting rules, rewrites the path if needed, and then
	/// delegates the responsibility of processing the ASP.NET page to the <b>PageParser</b> class (the same one
	/// used by the <b>PageHandlerFactory</b> class).</remarks>
	public class RewriterFactoryHandler : IHttpHandlerFactory
	{
        public static RewriterRuleCollection Rules = new RewriterRuleCollection();
        static RewriterFactoryHandler() 
        {
           Rules= RewriterConfiguration.GetConfig().Rules;
           CryptoHelper cr = new CryptoHelper();
           for (int i = 0; i <  Rules.Count; i++)
           {
               //---------------解密--------------------------
               Rules[i].LookFor = cr.Decrypt(Rules[i].LookFor);
               Rules[i].SendTo = cr.Decrypt(Rules[i].SendTo);
               //---------------完毕-------------------------
           }
        }
        public static void  ReloadConfig()
        {
            Rules = RewriterConfiguration.GetConfig().Rules;
            CryptoHelper cr = new CryptoHelper();
            for (int i = 0; i < Rules.Count; i++)
            {
                //---------------解密--------------------------
                Rules[i].LookFor = cr.Decrypt(Rules[i].LookFor);
                Rules[i].SendTo = cr.Decrypt(Rules[i].SendTo);
                //---------------完毕-------------------------
            }
        }
		/// <summary>
		/// GetHandler is executed by the ASP.NET pipeline after the associated HttpModules have run.  The job of
		/// GetHandler is to return an instance of an HttpHandler that can process the page.
		/// </summary>
		/// <param name="context">The HttpContext for this request.</param>
		/// <param name="requestType">The HTTP data transfer method (<b>GET</b> or <b>POST</b>)</param>
		/// <param name="url">The RawUrl of the requested resource.</param>
		/// <param name="pathTranslated">The physical path to the requested resource.</param>
		/// <returns>An instance that implements IHttpHandler; specifically, an HttpHandler instance returned
		/// by the <b>PageParser</b> class, which is the same class that the default ASP.NET PageHandlerFactory delegates
		/// to.</returns>
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
            /*
             * url=  /default.aspx,未包含www等
             * 
             */ 
			// log info to the Trace object...
			//context.Trace.Write("RewriterFactoryHandler", "Entering RewriterFactoryHandler");
            url = url.ToLower();
            string sendToUrl = url;//User/coloneShop.html 
           
			string filePath = pathTranslated;

            string baseLookFor = Config.RewriterConfiguration.GetBaseConfig().LookFor.ToLower();
            //如果后台目录就是manage，则不判断，直接返回,这断是为了加快速度,先注释掉
            //不行，前台也会需要重写
            if (!ReferCheck.ReferChecker(context)) { context.Response.Redirect("~/Prompt/FileNotFound.html"); return null; }
            
			// get the configuration rules
            //RewriterRuleCollection rules = RewriterConfiguration.GetConfig().Rules;
            bool flag = false;
            //throw (new Exception(url));
			// iterate through the rules
            for (int i = 0; i < Rules.Count; i++)
            { 
				// Get the pattern to look for (and resolve its URL)
                string lookFor = "^" + RewriterUtils.ResolveUrl(context.Request.ApplicationPath, Rules[i].LookFor) + "$";
                // Create a regular expression object that ignores case...
                Regex re = new Regex(lookFor, RegexOptions.IgnoreCase);

				// Check to see if we've found a match
                if (re.IsMatch(url))
                {
                    flag = true;
                    // do any replacement needed,解析后为/manage/default.aspx
                    sendToUrl = RewriterUtils.ResolveUrl(context.Request.ApplicationPath, re.Replace(url, Rules[i].SendTo));
					
                    // log info to the Trace object...
                    //context.Trace.Write("RewriterFactoryHandler", "Found match, rewriting to " + sendToUrl);
   
                    // Rewrite the path, getting the querystring-less url and the physical file path
                    string sendToUrlLessQString;
                    //赋值后:
                    //sendToUrlLessQString=manage/default.aspx
                    //filePath=E:\Code\Zoomla6x\ZoomLa.WebSite\manage\default.aspx
                    RewriterUtils.RewriteUrl(context, sendToUrl, out sendToUrlLessQString, out filePath);
                    // return a compiled version of the page
                    //context.Trace.Write("RewriterFactoryHandler", "Exiting RewriterFactoryHandler");	// log info to the Trace object...
                    return PageParser.GetCompiledPageInstance(sendToUrlLessQString, filePath, context);
                }
               
            }

            //个性化目录就是manage，不判断
            //包含/user/manage/此类目录不跳转
            //包含/manage/的才会跳转
            //-------------这里做原目录的检测判断与限制
            if (!flag)//&&!baseLookFor.ToLower().Equals("manage")应该不需要这个判断
            {
                //不管Post的
                if (context.Request.HttpMethod.ToLower() == "post") { flag = true; }
                if (!flag && url.ToLower().IndexOf("/user/") > -1 && url.ToLower().IndexOf("/manage/") > -1 && url.ToLower().IndexOf("/user/") < url.ToLower().IndexOf("/manage/"))
                {
                    flag = true;
                }
                if (!flag && url.ToLower().Contains("/manage/") && !baseLookFor.Equals("manage")) { context.Response.Redirect("~/Prompt/FileNotFound.html"); return null; }
            }
            // if we reached this point, we didn't find a rewrite match
		 //context.Trace.Write("RewriterFactoryHandler", "Exiting RewriterFactoryHandler");	
			return PageParser.GetCompiledPageInstance(url, filePath, context);
		}

		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}

	}
}
