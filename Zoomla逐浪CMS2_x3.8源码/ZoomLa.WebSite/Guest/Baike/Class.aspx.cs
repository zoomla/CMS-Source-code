using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using ZoomLa.Components;

public partial class Guest_Baike_Class : System.Web.UI.Page
{

    B_Baike b_Baike = new B_Baike();
    B_User b_User = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            M_UserInfo info = b_User.GetLogin();
            if (!string.IsNullOrEmpty(GuestConfig.GuestOption.BKOption.selGroup))
            {//用户组查看权限
                string groups = "," + GuestConfig.GuestOption.BKOption.selGroup + ",";
                if (!groups.Contains("," + info.GroupID.ToString() + ","))
                    function.WriteErrMsg("您没有查看百科的权限!");
            }
            bindType();
        }
    }
    protected void bindType()
    {
        DataTable dt = B_GradeOption.GetGradeList(3, 0);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)//指触发的类型为DadaList中的基本行或内容行
        {
            Repeater rep = e.Item.FindControl("Child_RPT") as Repeater;//找到里层的repeater对象
            DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
            int Gid = DataConvert.CLng(rowv["GradeID"]);
            DataTable dt = B_GradeOption.GetGradeList(3, Gid);
            rep.DataSource = dt;
            rep.DataBind();
        }
    }
    
    protected string getstyle()
    {
        if (b_User.CheckLogin())
        {
            return "display:inline-table";
        }
        else return "display:none";
    }
    protected string getstyles()
    {
        if (b_User.CheckLogin())
        {
            return "display:none";
        }
        else return "display:inline-table";
    }
}