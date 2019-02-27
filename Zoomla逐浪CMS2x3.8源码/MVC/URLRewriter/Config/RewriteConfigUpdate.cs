using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Web;



        /*
         * 功能:配置文件的AU功能
         */
namespace URLRewriter.Config
{
    public class RewriteConfigUpdate
    {
        private string path;
        public RewriteConfigUpdate(string path)
        {
            this.path = path;
        }

        public RewriteConfigUpdate(): this("~/Config/UrlRewrite.config")
        { }

        /// <summary>
        /// 更新配置文件
        /// </summary>
        /// <param name="context">当前HttpContext</param>
        /// <param name="config">加密后的</param>
        public void Update(HttpContext context, URLRewriter.Config.RewriterConfiguration config)
        {

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(URLRewriter.Config.RewriterConfiguration));
                using (Stream stream = new FileStream(context.Server.MapPath(path), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    serializer.Serialize(stream, config, namespaces);
                    stream.Close();
                }
            }
            catch{}
            finally { RewriterFactoryHandler.ReloadConfig(); }

        }
        /// <summary>
        /// 通过这个方法来调用更新方法,减少代码量
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="newCustomPath">新的路径</param>
        public void Update(HttpContext context,string newCustomPath)
        {
            URLRewriter.Config.RewriterConfiguration config = new URLRewriter.Config.RewriterConfiguration();
            URLRewriter.Config.RewriterRuleCollection rcollection = URLRewriter.Config.RewriterConfiguration.GetConfig().Rules;

            string oldCustomPath = rcollection[0].LookFor;
            rcollection[0].LookFor = newCustomPath.Replace(" ", "");
            //先赋值再加密,这里要从一开始
            URLRewriter.CryptoHelper chelper = new CryptoHelper();
            for (int i = 1; i < rcollection.Count; i++)
                rcollection[i].LookFor = rcollection[i].LookFor.Replace(oldCustomPath, rcollection[0].LookFor);
            //只有前四条做判断替换，剩下的都是前台Url重写
            for (int i = 0; i < 4; i++)
            {
                rcollection[i].LookFor = chelper.Encrypt(rcollection[i].LookFor);
                rcollection[i].SendTo = chelper.Encrypt(rcollection[i].SendTo);
                rcollection[i].LookFor = rcollection[i].LookFor;
                rcollection[i].SendTo = rcollection[i].SendTo;
            }
            config.Rules = rcollection;
            Update(HttpContext.Current, config);
        }
    }
}
