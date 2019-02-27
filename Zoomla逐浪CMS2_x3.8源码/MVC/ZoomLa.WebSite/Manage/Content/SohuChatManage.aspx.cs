using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Content
{
    public partial class SohuChatManage : CustomerPageAction
    {
        protected XmlDocument Xml = new XmlDocument();
        protected XmlDocument xmlDoc = new XmlDocument();
        protected XmlNode XApp = null;
        protected string ppath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\LabelSupport.xml";
        protected string code = " window.SCS_NO_IFRAME = true;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Xml.Load(Server.MapPath("/config/Suppliers.xml"));
                XApp = Xml.SelectSingleNode("SuppliersList/SohuChat");
                chat_AppIDT.Text = XApp.Attributes["Key"].Value;
                chat_AppKeyT.Text = XApp.Attributes["Secret"].Value;
                xmlDoc.Load(ppath);
                XmlNode node = xmlDoc.SelectSingleNode("/DataSet/Table[LabelName='SohuChat']");
                frontJS.Text = node.SelectSingleNode("LabelContent").InnerText;
                int radioFlag = frontJS.Text.Contains(code) ? 1 : 2;
                if (!string.IsNullOrEmpty(Request.QueryString["show"]))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "display", "disDiv(1);setRadio(" + radioFlag + ");", true);
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='CommentManage.aspx'>评论管理</a></li><li><a href='" + Request.RawUrl + "'>畅言评论管理</a><a href='javascript:disDiv(1);void(0);' style='color: green; margin-left: 5px; font-size: 13px;' id='configHref'>[修改配置]</a><a href='javascript:disDiv(2);void(0);' id='regHref' style='color: green; margin-left: 5px; font-size: 13px;display:none;'>[注册管理畅言]</a></li>");
            }
        }
        //保存
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            Xml.Load(Server.MapPath("/config/Suppliers.xml"));
            XApp = Xml.SelectSingleNode("SuppliersList/SohuChat");
            XApp.Attributes["Key"].Value = chat_AppIDT.Text.Trim();
            XApp.Attributes["Secret"].Value = chat_AppKeyT.Text.Trim();
            Xml.Save(Server.MapPath("/config/Suppliers.xml"));
            //----更新标签
            xmlDoc.Load(ppath);
            XmlNode node = xmlDoc.SelectSingleNode("/DataSet/Table[LabelName='SohuChat']");
            string nodeText = node.SelectSingleNode("LabelContent").InnerText;
            string reg1 = "(appid = '(.)+')";
            string reg2 = "(conf = '(.)+')";
            nodeText = Regex.Replace(nodeText, reg1, "appid = '" + chat_AppIDT.Text.Trim() + "'", RegexOptions.IgnoreCase);
            nodeText = Regex.Replace(nodeText, reg2, "conf = '" + chat_AppKeyT.Text.Trim() + "'", RegexOptions.IgnoreCase);
            if (Request.Form["codeRadio"].Equals("1"))//高速版
            {
                nodeText = nodeText.Replace("h.insertBefore(s, h.firstChild);", "h.insertBefore(s, h.firstChild);" + code);
            }
            else { nodeText = nodeText.Replace(code, ""); }

            node.SelectSingleNode("LabelContent").InnerText = nodeText;
            xmlDoc.Save(ppath);
            Response.Redirect("SohuChatManage.aspx?show=1");
        }
    }
}