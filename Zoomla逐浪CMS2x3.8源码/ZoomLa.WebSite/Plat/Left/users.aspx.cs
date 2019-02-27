using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Plat_Left_Users : System.Web.UI.Page
{
    //只显示最上级的几个大部,不显示过多层级
    B_User_Plat upBll = new B_User_Plat();
    B_Plat_Group gpBll = new B_Plat_Group();
    M_User_Plat upMod = null;
    private DataTable UserDT { get { return ViewState["UserDT"] as DataTable; } set { ViewState["UserDT"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind() 
    {
        //未关联部门的用户
        upMod = B_User_Plat.GetLogin();
        UserDT = upBll.SelByCompany(upMod.CompID);
        RPT.DataSource = GetGroupDT();
        RPT.DataBind();
        usercount_sp.InnerHtml = UserDT.Rows.Count.ToString();
    }
    private DataTable GetGroupDT() 
    {
        DataTable gpdt = DBCenter.SelWithField("ZL_Plat_Group", "ID,GroupName,MemberIDS", "1=0");
        DataTable dt = DBCenter.SelWithField("ZL_Plat_Group", "ID,GroupName,MemberIDS");
        DataRow dr = gpdt.NewRow();
        dr["ID"] = 0;
        dr["GroupName"] = "未关联部门用户";
        gpdt.Rows.Add(dr);
        gpdt.Merge(dt);
        return gpdt;
    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            DataTable dt = new DataTable();
            var rpt = e.Item.FindControl("User_RPT") as Repeater;
            var sp = e.Item.FindControl("usercount_sp") as System.Web.UI.HtmlControls.HtmlGenericControl;
            if (drv["ID"].ToString().Equals("0")) 
            {
                UserDT.DefaultView.RowFilter = "Gid='' OR Gid IS NULL ";
            }
            else
            {
                string uids = StrHelper.PureIDSForDB(DataConvert.CStr(drv["MemberIDS"]));
                if (!string.IsNullOrEmpty(uids))
                {
                    UserDT.DefaultView.RowFilter = "UserID IN (" + uids + ")";
                }
            }
            dt = UserDT.DefaultView.ToTable();
            sp.InnerHtml = dt.Rows.Count.ToString();
            rpt.DataSource = dt;
            rpt.DataBind();
        }
    }
}