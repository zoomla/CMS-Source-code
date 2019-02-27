using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Collections.Generic;

namespace FreeHome.common
{
    public class UserRegConfig
    {
        public static List<string> GetInitUserReg(string xmlurl,string Jname)
        {

            List<string> list = new List<string>();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(xmlurl);
            XmlReader dr = new XmlNodeReader(xdd);
            while (dr.Read())
            {
                if (dr.NodeType == XmlNodeType.Element)
                {
                    if (dr.Name == Jname)
                    {

                        if (dr.MoveToAttribute("name"))
                        {
                            list.Add(dr.Value);
                        }
                    }
                }
            }

            return list;
        }
    }
}
