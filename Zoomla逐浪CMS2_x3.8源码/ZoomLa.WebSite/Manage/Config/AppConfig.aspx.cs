using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Manage_I_Config_AppConfig : CustomerPageAction
{
    B_ReadWrite br = new B_ReadWrite();
    EnviorHelper enHelper = new EnviorHelper();
    private static object Lockobj = new object();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            string bb = br.GetValueWeb();
            CustomError_Chk.Checked = bb.Equals("On");
            HiddenText.Value = TextBox1.Text;
            TextBox1.Text = br.GetValueNoLink();
            IsManageReg_Chk.Checked = SiteConfig.SiteOption.RegManager == 1;
            MyBind();
            //-----错误码
            Call.SetBreadCrumb(Master, "<li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">维护中心</li>" + Call.GetHelp(7));
        }
    }
    public void MyBind()
    {
        DataTable dt = enHelper.SelAllErrorCode();
        ErrRPT.DataSource = dt;
        ErrRPT.DataBind();
        //默认文档
        JArray jarray = new JArray();
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(Server.MapPath("/web.config"));
        XmlNodeList nodes = xdoc.SelectNodes("//defaultDocument/files/add");
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                JObject jobj = new JObject();
                jobj.Add(new JProperty("name", nodes[i].Attributes["value"].Value));
                jarray.Add(jobj);
            }
            Default_Hid.Value = JsonConvert.SerializeObject(jarray);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(Server.MapPath("/web.config"));
        foreach (RepeaterItem item in ErrRPT.Items)
        {
            int errcode = Convert.ToInt32((item.FindControl("ErrCode_Hid") as HiddenField).Value);
            string errurl = (item.FindControl("ErrUrl_T") as TextBox).Text;
            enHelper.UpdateCustomError(xdoc, errcode, errurl);
        }
        br.SetValueWeb(xdoc,CustomError_Chk.Checked ? "On" : "Off");
        if (TextBox1.Text != HiddenText.Value)
        {
            br.SetValueNoLink(TextBox1.Text);
        }
        if (CheckBox1.Checked)
        {
            br.SetValueAppSettings("Installed", "false");
        }
        SiteConfig.SiteOption.RegManager = IsManageReg_Chk.Checked ? 1 : 0;
        SiteConfig.Update();
        UpdateDefaultConfig(xdoc);
        xdoc.Save(Server.MapPath("/web.config"));
        function.WriteSuccessMsg("维护中心配置保存成功!!");
    }
    private void UpdateDefaultConfig(XmlDocument xdoc)
    {
        //保存默认文档
        XmlNode node = xdoc.SelectSingleNode("//defaultDocument");
        if (node == null)
        {
            XmlNode defnode = xdoc.SelectSingleNode("//system.webServer").AppendChild(xdoc.CreateElement("defaultDocument"));
            defnode.AppendChild(xdoc.CreateElement("files"));
        }
        node = xdoc.SelectSingleNode("//defaultDocument/files");
        node.RemoveAll();
        node.AppendChild(xdoc.CreateElement("clear"));
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(Default_Hid.Value);
        foreach (DataRow dr in dt.Rows)
        {
            XmlNode addnode = xdoc.CreateElement("add");
            addnode.Attributes.Append(xdoc.CreateAttribute("value"));
            addnode.Attributes["value"].Value = dr["name"].ToString();
            node.AppendChild(addnode);
        }

    }
}