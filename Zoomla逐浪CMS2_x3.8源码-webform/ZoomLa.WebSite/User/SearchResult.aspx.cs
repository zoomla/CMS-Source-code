using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_SearchResult : CustomerPageAction
{
    public string Key { get { return Request.QueryString["key"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt;
        if (!string.IsNullOrEmpty(Key))
        {
            dt = PageDT;
        }
        else
        { dt = PageDT; }
        EGV.DataSource = PageDT;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    public DataTable PageDT
    {
        get
        {
            if (ViewState["PageDT"] == null)
            {
                string key = Request.QueryString["key"].Trim();
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("/Config/UserMap.config"));
                DataTable dt = ds.Tables[0];
                dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
                ViewState["PageDT"] = DecreateDT(dt, key);
            }
            return ViewState["PageDT"] as DataTable;
        }
    }
    public DataTable DecreateDT(DataTable dt, string key)
    {
        dt.Columns.Add(new DataColumn("Index", typeof(int)));
        dt.Columns.Add(new DataColumn("DTitle", typeof(string)));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Index"] = (i + 1);
            dt.Rows[i]["Url"] = dt.Rows[i]["Url"].ToString();
            string title = dt.Rows[i]["Title"].ToString();
            dt.Rows[i]["DTitle"] = title.Replace(key, "<span style='color:red;'>" + key + "</span>");
        }
        return dt;
    }
}