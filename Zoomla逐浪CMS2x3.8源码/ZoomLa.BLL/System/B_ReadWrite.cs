namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Xml;
    using System.Web;
    using System.IO;
   public class B_ReadWrite
    {

        public string docName = String.Empty;
        private XmlNode node = null;
        public B_ReadWrite()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public bool SetValueAppSettings(string key, string onetext)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc, 1);
            // retrieve the RewriterRule node   
            node = cfgDoc.SelectSingleNode("//appSettings");
            try
            {
                // XPath select setting "add" element that contains this key       
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
               if (addElem != null)
               {
                   addElem.SetAttribute("value", onetext);
               }
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetValueWeb(XmlDocument xdoc,string onetext)
        {
            XmlDocument cfgDoc = xdoc;
           // loadConfigDoc(cfgDoc, 2);
            // retrieve the RewriterRule node   
            node = cfgDoc.SelectSingleNode("//configuration/system.web");
      
            try
            {
                // XPath select setting "add" element that contains this key       
                XmlNode naddElem = node.SelectSingleNode("//system.web");
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//customErrors");
                //node.Value = "aaa";
                //XmlElement addElem = (XmlElement)node;

                if (addElem != null)
                {
                    addElem.SetAttribute("mode", onetext);
                }
                //saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetValueWeb()
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc, 2);
            // retrieve the RewriterRule node   
            node = cfgDoc.SelectSingleNode("//configuration/system.web");

            try
            {
                // XPath select setting "add" element that contains this key       
                XmlNode naddElem = node.SelectSingleNode("//system.web");
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//customErrors");
                //node.Value = "aaa";
                //XmlElement addElem = (XmlElement)node;

                if (addElem != null)
                {
                    return addElem.GetAttribute("mode");
                }
              
                return "0";
            }
            catch
            {
                return "0";
            }
        }
     //    <add verb="*" path="*.jpg,*.jpeg,*.gif,*.png,*.bmp,*.flv" type="ZoomLa.NoLink"/>
        public string GetValueNoLink()
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc, 2);
            // retrieve the RewriterRule node   
            node = cfgDoc.SelectSingleNode("//configuration/system.web/httpHandlers");

            try
            {
                // XPath select setting "add" element that contains this key       
                XmlNode naddElem = node.SelectSingleNode("//system.web");
             //   XmlElement addElem = (XmlElement)node.SelectSingleNode("//customErrors");
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@type='ZoomLa.NoLink']");
                //node.Value = "aaa";
                //XmlElement addElem = (XmlElement)node;

                if (addElem != null)
                {
                    return addElem.GetAttribute("path");
                }

                return "0";
            }
            catch
            {
                return "0";
            }
        }

        public bool SetValueNoLink(string onetext)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc, 2);
            // retrieve the RewriterRule node   
            node = cfgDoc.SelectSingleNode("//configuration/system.web/httpHandlers");

            try
            {
                // XPath select setting "add" element that contains this key       
                XmlNode naddElem = node.SelectSingleNode("//system.web");
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@type='ZoomLa.NoLink']");
                //node.Value = "aaa";
                //XmlElement addElem = (XmlElement)node;

                if (addElem != null)
                {
                    addElem.SetAttribute("path", onetext);
                }
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool removeElement(string elementKey,int type)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc, type);
                // retrieve the RewriterRule node  
                node = cfgDoc.SelectSingleNode("//Rules");
                if (node == null)
                {
                    throw new InvalidOperationException("RewriterRule section not found");
                }
              //   <RewriterRule name="aa">
                // XPath select setting "add" element that contains this key to remove      
            //    node.RemoveChild();
              //  node.SelectSingleNode("//RewriterRule[@name='aa']").RemoveAll();
                node.RemoveChild(node.SelectSingleNode("//RewriterRule[@name='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool removeElementall(int type)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc, type);
                // retrieve the RewriterRule node  
                node = cfgDoc.SelectSingleNode("//Rules");
                if (node == null)
                {
                    throw new InvalidOperationException("RewriterRule section not found");
                }
                //   <RewriterRule name="aa">
                // XPath select setting "add" element that contains this key to remove      
                //    node.RemoveChild();
                //  node.SelectSingleNode("//RewriterRule[@name='aa']").RemoveAll();

                node.RemoveAll();
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }
        private XmlDocument loadConfigDoc(XmlDocument cfgDoc,int type)
        {
            HttpContext current = HttpContext.Current;
            switch (type)
            {
                case 1:
                    if (current != null)
                    {
                        this.docName = current.Server.MapPath("~/Config/AppSettings.config");
                    }
                    else
                    {
                        this.docName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/AppSettings.config");
                    }
                    break;
                case 2:
                    if (current != null)
                    {
                        this.docName = current.Server.MapPath("~/Web.Config");
                    }
                    else
                    {
                        this.docName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.Config");
                    }
                    break;
            }
            
            
            
            // load the config file   
            //if (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
            //{
            //    docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
            //    docName += ".exe.config";
            //}
            //else
            //{
         //   docName = HttpContext.Current.Server.MapPath("../../Web.Config");
            //}
            try
            {
                cfgDoc.Load(docName);
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('读文件时错误,请检查文件是否存在!')</script>");

            }


            return cfgDoc;
        }
    }

}
#region 备份
//   public bool SetValue(string key, string onenode, string Childnode, int type)
//        {
//            XmlDocument cfgDoc = new XmlDocument();
//            loadConfigDoc(cfgDoc, type);
//            // retrieve the RewriterRule node   
//            node = cfgDoc.SelectSingleNode("//Rules");
//            //if (node == null)
//            //{
//            //    throw new InvalidOperationException("RewriterRule section not found");
//            //}
//            try
//            {
//                // XPath select setting "add" element that contains this key       
//               XmlElement addElem = (XmlElement)node.SelectSingleNode("//RewriterRule[@name='" + key + "']");
//               if (addElem != null)
//               {
        
//                   XmlNodeList nls = addElem.ChildNodes;
//                   foreach (XmlNode xn1 in nls)//遍历   
//                   {
//                       XmlElement xe2 = (XmlElement)xn1;//转换类型   
//                       if (xe2.Name == "LookFor")//如果找到   
//                       {
//                           xe2.InnerText = one;//则修改   
//                           break;//找到退出来就可以了   
//                       }
//                        if (xe2.Name == "SendTo")//如果找到   
//                       {
//                           xe2.InnerText = two;//则修改   
//                           break;//找到退出来就可以了   
//                       }

//                   }   

                
//               }
//                //// not found, so we need to add the element, key and value   
//                else
//                {
//                //XmlElement entry = cfgDoc.CreateElement("add");
//                //entry.SetAttribute("key", key);
//                //entry.SetAttribute("value", value);
//                //node.AppendChild(entry);


//                XmlElement newurl = cfgDoc.CreateElement("RewriterRule");
//                newurl.SetAttribute("name", key);//设置该节点ISBN属性   
//                XmlElement newlook = cfgDoc.CreateElement("LookFor");
//                newlook.InnerText = one;
//                newurl.AppendChild(newlook);
//                XmlElement newSend = cfgDoc.CreateElement("SendTo");
//                newSend.InnerText = two;
//                newurl.AppendChild(newSend);
//                node.AppendChild(newurl);
//               }
//                //XmlNode root = doc.DocumentElement;//获取文档的根节点
//                //XmlNode temp;
//                //temp = root.SelectSingleNode("//Rules");
//                //XmlElement newurl = doc.CreateElement("RewriterRule");
//                //newurl.SetAttribute("name", "节点属性1");//设置该节点ISBN属性   
//                //XmlElement newlook = doc.CreateElement("LookFor");
//                //newlook.InnerText = "aaa";
//                //newurl.AppendChild(newlook);
//                //XmlElement newSend = doc.CreateElement("SendTo");
//                //newSend.InnerText = "bbb";
//                //newurl.AppendChild(newSend);
//                //temp.AppendChild(newurl);
//                //save it   
//                saveConfigDoc(cfgDoc, docName);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//        public bool removeElement(string elementKey)
//        {
//            try
//            {
//                XmlDocument cfgDoc = new XmlDocument();
//                loadConfigDoc(cfgDoc, type);
//                // retrieve the RewriterRule node  
//                node = cfgDoc.SelectSingleNode("//Rules");
//                if (node == null)
//                {
//                    throw new InvalidOperationException("RewriterRule section not found");
//                }
//              //   <RewriterRule name="aa">
//                // XPath select setting "add" element that contains this key to remove      
//            //    node.RemoveChild();
//              //  node.SelectSingleNode("//RewriterRule[@name='aa']").RemoveAll();
//                node.RemoveChild(node.SelectSingleNode("//RewriterRule[@name='" + elementKey + "']"));
//                saveConfigDoc(cfgDoc, docName);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//        public bool removeElementall()
//        {
//            try
//            {
//                XmlDocument cfgDoc = new XmlDocument();
//                loadConfigDoc(cfgDoc, type);
//                // retrieve the RewriterRule node  
//                node = cfgDoc.SelectSingleNode("//Rules");
//                if (node == null)
//                {
//                    throw new InvalidOperationException("RewriterRule section not found");
//                }
//                //   <RewriterRule name="aa">
//                // XPath select setting "add" element that contains this key to remove      
//                //    node.RemoveChild();
//                //  node.SelectSingleNode("//RewriterRule[@name='aa']").RemoveAll();

//                node.RemoveAll();
//                saveConfigDoc(cfgDoc, docName);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
//        {
//            try
//            {
//                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
//                writer.Formatting = Formatting.Indented;
//                cfgDoc.WriteTo(writer);
//                writer.Flush();
//                writer.Close();
//                return;
//            }
//            catch
//            {
//                throw;
//            }
//        }
//        private XmlDocument loadConfigDoc(XmlDocument cfgDoc,int type)
//        {
//            HttpContext current = HttpContext.Current;
//            switch (type)
//            {
//                case 1:
//                    if (current != null)
//                    {
//                        this.docName = current.Server.MapPath("~/Config/AppSettings.config");
//                    }
//                    else
//                    {
//                        this.docName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/AppSettings.config");
//                    }
//                    break;
//                case 2:
//                    if (current != null)
//                    {
//                        this.docName = current.Server.MapPath("~/Web.Config");
//                    }
//                    else
//                    {
//                        this.docName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.Config");
//                    }
//                    break;
//            }
            
            
            
//            // load the config file   
//            //if (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
//            //{
//            //    docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
//            //    docName += ".exe.config";
//            //}
//            //else
//            //{
//         //   docName = HttpContext.Current.Server.MapPath("../../Web.Config");
//            //}
//            try
//            {
//                cfgDoc.Load(docName);
//            }
//            catch
//            {
//                HttpContext.Current.Response.Write("<script>alert('读文件时错误,请检查文件是否存在!')</script>");

//            }


//            return cfgDoc;
//        }
//    }
#endregion