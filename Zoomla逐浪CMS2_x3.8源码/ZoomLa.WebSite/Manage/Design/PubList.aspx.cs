using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Helper;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Manage_Design_PubList : System.Web.UI.Page
{
    public string H5ID { get { return Request.QueryString["H5ID"] ?? ""; } }
    public string FName { get { return HttpUtility.UrlDecode(Request.QueryString["FName"]??""); } }
    public string Skey { get { return Skey_T.Text; } set { Skey_T.Text = value; } }
    B_Design_Pub pubBll = new B_Design_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.HideBread(Master);
        }
    }
    private void MyBind()
    {
        EGV.DataSource = pubBll.Sel(H5ID, -100, FName, Skey);
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                int id = Convert.ToInt32(e.CommandArgument);
                pubBll.Del(id);
                break;
        }
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView dr = e.Row.DataItem as DataRowView;
        //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
        //}
    }
    public string GetIP() { return IPScaner.IPLocation(Eval("IP", "")); }
    public string GetUser() 
    {
        string uid = Eval("UserID", "");
        if (string.IsNullOrEmpty(uid)) { return "游客"; }
        else { return "<a href='javascript:;' onclick='showuinfo("+uid+");'>"+Eval("UserName")+"</a>"; }
    }
    public string GetContent()
    {
        string result = "";
        string content = Eval("FormContent", "");
        if (string.IsNullOrEmpty(content) || content.Equals("[]")) { return result; }
        else
        {
            JArray jarr = JsonConvert.DeserializeObject<JArray>(content);
            foreach (var item in jarr)
            {
                result += item["name"] + ":" + item["value"] + "|";
            }
            return result;
        }
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            pubBll.DelByIDS(ids);
        }
        MyBind();
    }
    protected void Search_B_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}