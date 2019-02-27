using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_AddCrm_OptionManage : System.Web.UI.Page
{
    string xmlPath = HttpContext.Current.Server.MapPath("~/Config/CRM_Dictionary.xml");
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='CustomerList.aspx?usertype=0'>CRM配置</a></li><li><a href='AddOption.aspx'>选项管理</a></li><li><a href='AddOption.aspx'>[添加新选项]</a></li>");
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(xmlPath);
        for (int i = 1; i < ds.Tables.Count; i++)
        {
            ds.Tables[0].Merge(ds.Tables[i]);
        }
        if (ds.Tables[0].Columns["isAdd"] == null)
        {
            ds.Tables[0].Columns.Add("isAdd", typeof(string));
        }
        ds.Tables[0].DefaultView.RowFilter = "isAdd=1";
        DataTable resultDT = ds.Tables[0].DefaultView.ToTable();
        resultDT.Columns.Add("ID", typeof(int));//用来作为标识位
        for (int i = 0; i < resultDT.Rows.Count; i++)
        {
            resultDT.Rows[i]["ID"] = i + 1;
        }
        RPT.DataSource = resultDT;
        RPT.DataBind();
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(xmlPath);
        if (e.CommandName == "Del")
        {
            XmlNode root = myDoc.SelectSingleNode("CRM_Dictionary");
            string node_id = e.CommandArgument.ToString();
            string sort = e.CommandArgument.ToString();
            XmlNodeList list = myDoc.SelectNodes("/CRM_Dictionary/" + node_id);
            for (int i = 0; i < list.Count; i++)
            {
                root.RemoveChild(list[i]);
            }
            myDoc.Save(Server.MapPath("/Config/CRM_Dictionary.xml"));//保存.xml文件
            function.WriteSuccessMsg("删除成功!");
        }
    }

    public string GetOptionStr()
    {
        int optype = DataConverter.CLng(Eval("buildMethod").ToString());
        switch (optype)
        {
            case 1:
                return "下拉框";
            case 2:
                return "单选框";
            case 3:
                return "多选框";
            case 4:
                return "多行文本(不带Html)";
            case 5:
                return "多行文本(Html)";
            default:
                return "";
        }
    }
}