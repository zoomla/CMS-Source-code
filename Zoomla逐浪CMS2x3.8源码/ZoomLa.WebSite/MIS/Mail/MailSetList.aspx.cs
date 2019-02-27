using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class MIS_Mail_MailSetList : System.Web.UI.Page
{
    B_MailSet bll = new B_MailSet();
    M_MailSet model = new M_MailSet();
    B_User buser = new B_User();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(Request.Url.LocalPath);
        if (!IsPostBack)
        {
            dt = bll.SelByUid(buser.GetLogin().UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                Bind(dt);
            }
        }
    }
    protected void Bind(DataTable dt)
    {
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            LinkButton isu = (LinkButton)e.Item.FindControl("isUser");
            Label labSta = (Label)e.Item.FindControl("labSta");
            int mid = DataConverter.CLng(rowv.Row["ID"].ToString());
            dt = bll.Sel(mid);
            if (dt != null)
            {
                if (dt.Rows[0]["Status"].ToString() == "0")
                {
                    isu.Text = "启用";
                    labSta.Text = "已停用";
                }
                else
                {
                    isu.Text = "停用";
                    labSta.Text = "已启用";
                }
            }
        }
    }
    protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        // LinkButton isu = (LinkButton)e.Item.FindControl("isUser");
        int id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "isUser")
        {
            dt = bll.Sel(id);
            if (dt != null && dt.Rows[0]["Status"].ToString() == "0")
            {
                bll.UpdateStatus(1, id);
                Response.Redirect("MailSetList.aspx");
            }
            else
            {
                bll.UpdateStatus(0, id);
                Response.Redirect("MailSetList.aspx");
            }
        }
    }
    protected string GetisDef(string id, bool isdef)
    {
        string str = "<input name='isdef' id='isdef" + id + "' style='cursor: pointer;' onclick='setDefault(" + id + ");' type='radio' />";
        if (isdef)
            str = "<input name='isdef' id='isdef" + id + "' style='cursor: pointer;' onclick='setDefault(" + id + ");' checked type='radio' />";
        return str;
    }

}