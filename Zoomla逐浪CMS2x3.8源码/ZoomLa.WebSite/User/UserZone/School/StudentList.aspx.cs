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
using ZoomLa.Components;

public partial class User_UserZone_School_StudentList : System.Web.UI.Page
{
    B_User bull = new B_User();
    B_Student bs = new B_Student();
    B_ClassRoom cll = new B_ClassRoom();
    public string RoomName = "";
    protected int RoomID
    {
        get
        {
            if (ViewState["RoomID"] == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(ViewState["RoomID"].ToString());
            }
        }
        set
        {
            ViewState["RoomID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bull.CheckIsLogin();
        if (!IsPostBack)
        {
            RoomID = int.Parse(Request.QueryString["Roomid"]);
            M_UserInfo mu = bull.GetLogin();
            
            Bind();
        }
        RoomName = cll.GetSelect(RoomID).RoomName;
    }

    private void Bind()
    {
        DataTable dt = bs.SelByURid(RoomID ,3);
        this.EGV.DataSource = dt;
        this.EGV.DataBind();
    }

    public string GetName(string id)
    {
        string str = bull.GetUserByUserID(int.Parse(id)).UserName;
        if (string.IsNullOrEmpty(str))
        {
            return "未登记";
        }
        else
        {
            return str;
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        EGV.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        EGV.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        M_Student ms=bs.GetSelect(int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
        ms.StudentType=int.Parse(((DropDownList)EGV.Rows[e.RowIndex].FindControl("DropDownList2")).SelectedValue);
        if (ms.StudentType == 2)
        {
            string str=((TextBox)EGV.Rows[e.RowIndex].FindControl("TextBox1")).Text;
            if (str == "")
            {
                ms.StudentTypeTitle = "班干部";
            }
            else
            {
                ms.StudentTypeTitle = str;
            }
        }
        else
        {
            ms.StudentTypeTitle = "学生";
        }
        bs.GetUpdate(ms);
        EGV.EditIndex = -1;
        Bind();
    }

    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
        DropDownList ddltype = (DropDownList)sender;
        ddltype.Items.Add(new ListItem("学生", "1"));
        ddltype.Items.Add(new ListItem("班干部", "2"));
        GridViewRow det = (GridViewRow)ddltype.NamingContainer;
        if (bs.GetSelect(int.Parse(EGV.DataKeys[det.RowIndex].Value.ToString())).StudentType != 1)
        {
            ddltype.SelectedValue = "2";
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddltype = (DropDownList)sender;
        GridViewRow det = (GridViewRow)ddltype.NamingContainer;
        ((TextBox)EGV.Rows[det.RowIndex].FindControl("TextBox1")).Text="";

    }
}
