using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;

public partial class Plat_Group_MyGroup : System.Web.UI.Page
{
    /*
     *我所加入的,我所创建的,我管理的部门
     */
    B_Plat_Group groupBll = new B_Plat_Group();
    B_User buser = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = new DataTable();
        //if (!string.IsNullOrEmpty(SearchKey))
        //{
        //    dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
        //    dt = dt.DefaultView.ToTable();
        //}
        dt = groupBll.SelGroupByAuth(buser.GetLogin().UserID);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    private string SearchKey
    {
        get
        {
            return ViewState["SearchKey"] as string;
        }
        set
        {
            ViewState["SearchKey"] = value.Trim();
        }

    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2":
                int id = Convert.ToInt32(e.CommandArgument);
                groupBll.Del(id);
                MyBind();
                break;
        }
    }
    protected void GroupAdd_Btn_Click(object sender, EventArgs e)
    {
        AddModel();
        MyBind();
    }
    public void AddModel() 
    {
        M_Plat_Group groupMod = new M_Plat_Group();
        groupMod.Status = 1;
        groupMod.GroupType = 1;
        groupMod.CreateTime = DateTime.Now;
        groupMod.CreateUser = buser.GetLogin().UserID;
        groupBll.Insert(groupMod);
    }

    public string GetUName(string ids) 
    {
        string unames = buser.GetUserNameByIDS(ids);
        return unames.Length > 20 ? unames.Substring(0, 20) + " ..." : unames;
    }
}