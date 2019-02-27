using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public partial class Design_Diag_Label_LabelManage :CustomerPageAction
{
    B_Label bll = new B_Label();
    B_Admin badmin = new B_Admin();
    public string LabelCate { get { string _cate = Request.QueryString["Cate"] ?? ""; return HttpUtility.UrlDecode(_cate); } }
    public string KeyWord { get { return TxtLableName.Text; } set { TxtLableName.Text = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!B_ARoleAuth.Check(ZLEnum.Auth.label, "LabelManage"))
        //{
        //    function.WriteErrMsg("没有权限进行此项操作");
        //}
        B_Permission.CheckAuthEx("design");
        if (!IsPostBack)
        {
            //Call.HideBread(Master);
            string cateTlp = "<li class='{0}' role='presentation'><a href='LabelManage.aspx?Cate={1}'>{2}</a></li>";
            lblLabel.Text += string.Format(cateTlp, (string.IsNullOrEmpty(LabelCate) ? "active" : ""), "", "所有标签");
            DataTable CateTable = bll.GetLabelCateListXML();//标签类别
            foreach (DataRow dr in CateTable.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString())) continue;
                string isactive = dr["name"].ToString().Equals(LabelCate) ? "active" : "";
                lblLabel.Text += string.Format(cateTlp, isactive, HttpUtility.UrlEncode(dr["name"].ToString()), dr["name"]);
            }
            //-----------------
            KeyWord = Request.QueryString["KeyWord"] ?? "";
            DataTable dt = bll.SelAllLabel(LabelCate, KeyWord);
            RPT.DataSource = dt;
            RPT.DataBind();
        }
    }
    protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string LabelName = e.CommandArgument.ToString();
            bll.DelLabelXML(LabelName);
            Response.Redirect("LabelManage.aspx");
        }
        if (e.CommandName == "Copy")
        {
            string LabelName = e.CommandArgument.ToString();

            M_Label newlbl = bll.GetLabelXML(LabelName);
            newlbl.LableName = newlbl.LableName + DataSecurity.RandomNum(4);
            newlbl.LabelID = 0;
            bll.AddLabelXML(newlbl);
            Response.Redirect("LabelManage.aspx");
        }
        if (e.CommandName == "Download")
        {
            string LabelName = e.CommandArgument.ToString();
            M_Label newlbl = bll.GetLabelXML(LabelName);
            XmlDocument doc = new XmlDocument();
            XmlNode nodelist = doc.SelectSingleNode("//NewDataSet/Table[LabelID='" + newlbl.LabelID.ToString() + "']");
            string fileName = newlbl.LableName + ".lable";//客户端保存的文件名
            string path = newlbl.LabelCate + "/" + newlbl.LableName + ".label";
            SafeSC.DownFile(bll.dir + path, HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        }
    }
    public string GetLabelLink(string id, string labelname, string LableType)
    {
        string href = "<a href=\"{0}?LabelName=" + Server.UrlEncode(labelname) + "\" title=\"" + labelname + "\">" + labelname + "</a>";
        string re = "";
        if (DataConverter.CLng(LableType) == 1)
            re = string.Format(href, "LabelHtml.aspx");
        else if (DataConverter.CLng(LableType) < 5)
            re = string.Format(href, "LabelSql.aspx");
        else
            re = string.Format(href, "PageLabel.aspx");
        return re;
    }

    public string GetLabelType(string type)
    {
        return bll.GetLabelType(Convert.ToInt32(type));
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("LabelManage.aspx?Cate=&KeyWord=" + HttpUtility.UrlEncode(KeyWord));
    }
    // 批量删除
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"] ?? "";
        foreach (string id in ids.Split(','))
        {
            bll.DelLabelXML(Convert.ToInt32(id));
        }
        Response.Redirect(Request.RawUrl);
    }
}