using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDUBLL;
using BDUModel;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

public partial class User_UserZone_Structure : System.Web.UI.Page
{
    B_Structure bll = new B_Structure();
    DataTable dt = new DataTable();
    DataTable dt2 = new DataTable();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dt = bll.SelStatus(1, 99);
            string strid = buser.GetLogin().StructureID;
            if (strid != "")
            {
                manageinfos.Visible = true;
                nostruct.Visible = false;
            }
            if (Request.QueryString["Struct"] == "yx")
            {
                manageinfos.Visible = true;
                nostruct.Visible = false;
                mystruct.Visible = false;
                joinstruct.Visible = false;
                yxstruct.Visible = true;
            }
            if (Request.QueryString["Struct"] == "my")
            {
                manageinfos.Visible = true;
                nostruct.Visible = false;
                mystruct.Visible = true;
                yxstruct.Visible = false;
                joinstruct.Visible = false;
            }
            if (Request.QueryString["Struct"] == "join")
            {
                manageinfos.Visible = true;
                nostruct.Visible = false;
                mystruct.Visible = false;
                yxstruct.Visible = false;
                joinstruct.Visible = true;
            }
            bind();
        }
    }
    protected void bind()
    {
        DataTable dtAll = bll.SelStatus(1, 99);
        if (!string.IsNullOrEmpty(buser.GetLogin().StructureID))
        {
            dtAll.DefaultView.RowFilter = "ID not in(" + buser.GetLogin().StructureID.Trim(',') + ")";
        }
        dt = dtAll.DefaultView.ToTable();
        Repeater1.DataSource = dt;
        Repeater1.DataBind();

        dtAll.DefaultView.RowFilter = "UserID=" + buser.GetLogin().UserID;
        dt2 = dtAll.DefaultView.ToTable();
        Repeater2.DataSource = dt2;
        Repeater2.DataBind();
        DataTable dt3 = new DataTable();
        if (!string.IsNullOrEmpty(buser.GetLogin().StructureID))
        {
            dtAll.DefaultView.RowFilter = "ID in(" + buser.GetLogin().StructureID.Trim(',') + ")";
            dt3 = dtAll.DefaultView.ToTable();
        }
        //DataColumn dc1 = new DataColumn();
        //dc1.ColumnName = "IDD";
        //dc1.DataType = typeof(int);
        //DataColumn dc2 = new DataColumn();
        //dc2.ColumnName = "Namee";
        //dc2.DataType = typeof(string);
        //dt3.Columns.Add(dc1);
        //dt3.Columns.Add(dc2);
        //string stid = buser.GetLogin().StructureID;
        //if (!string.IsNullOrEmpty(stid))
        //{
        //    string[] stids = stid.Split(new Char[] { ',' });
        //    if (stids.Length > 0)
        //    {
        //        for (int i = 0; i < stids.Length; i++)
        //        {
        //            if (!string.IsNullOrEmpty(stids[i]))
        //            {
        //                DataRow dr1 = dt3.NewRow();
        //                DataTable dt4 = bll.Sel(Convert.ToInt32(stids[i]));
        //                if (dt4 != null && dt4.Rows.Count > 0)
        //                {
        //                    dr1[0] = DataConverter.CLng(stids[i]);
        //                    dr1[1] = dt4.Rows[0]["Name"].ToString();
        //                    dt3.Rows.Add(dr1);
        //                }
        //            }
        //        }
        //    }
        //}
        Repeater3.DataSource = dt3;
        Repeater3.DataBind();
    }
    protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        if (e.CommandName == "Del")
        {
            bll.Del(id);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！');location.href=location.href;", true);
        }
        if (e.CommandName == "join")
        {
            M_UserInfo info = buser.SeachByID(buser.GetLogin().UserID);
            info.StructureID +=  id;
            buser.UpDateUser(info);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('已成功加入该企业！');location.href=location.href;", true);
        }
    }
    protected void Repeater3_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        if (e.CommandName == "joout")
        {
            M_UserInfo info = buser.SeachByID(buser.GetLogin().UserID);
            string sid = info.StructureID;
            sid = sid.Replace("," + id + ",", "");
            info.StructureID = sid;
            buser.UpDateUser(info);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('已成功退出该企业！');location.href=location.href;", true);
        }
    }
    public DataTable SelStatus(int Group, int status)
    {
        string str = "select * from ZL_Structure where [Group]=" + Group + " and status=" + status;
        return SqlHelper.ExecuteTable(CommandType.Text, str);
    }
}