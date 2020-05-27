using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

/*
 * 管理本公司的所有部门,删除,移除成员,更改信息
 */
public partial class Plat_Admin_GroupAdmin : System.Web.UI.Page
{
    B_Plat_Group gpBll = new B_Plat_Group();
    B_User buser = new B_User();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request["action"];
            int id = DataConverter.CLng(Request["id"]);
            switch (action)
            {
                case "del":
                    gpBll.Del(id);
                    break;
            }
            Response.Write(""); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_User_Plat upMod=B_User_Plat.GetLogin();
        CompName_L.Text = upMod.CompName;
        DataTable dt = gpBll.SelByCompID(B_User_Plat.GetLogin().CompID);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string ShowName()
    {
        int depath = DataConverter.CLng(Eval("depth"));
        int id = Convert.ToInt32(Eval("ID"));
        string name = Eval("GroupName", "");
        string result = "";
        for (int i = 0; i <= depath; i++)
        {
            result += "<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20' title='' />";
        }
        result += "<img src='/Images/TreeLineImages/t.gif' border='0' />";
        result = result + "<a href='AddGroup.aspx?ID=" + id + "'><i class='fa fa-folder-open'></i> " + name + "</a>";
        return result;
    }
    //获取管理员昵称
    protected string GetManageName(string ids)
    {
        string result = "";
        DataTable dt = buser.SelectUserByIds(ids);
        if (dt == null) { return ""; }
        foreach (DataRow row in dt.Rows)
        {
            result += row["HoneyName"].ToString() + ",";
        }
        return result.Trim(',');
    }
}