using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class manage_AddOn_InputProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {　
		B_Admin badmin = new B_Admin();
       　badmin.CheckIsLogin();
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "select")
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);

            //string types = Binfo.WorkName;
            //string scripttxt = "setvalue('Projects','" + Binfo.WorkName + "');";
            //function.Script(this,scripttxt + ";onstr();");
        }

        //DataTable tables = bll.GetProjectWorkStatus();
        //tables.DefaultView.Sort = "WorkID ";
        //Page_list(tables);
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        if (Cll != null)
        {
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
    }
    #endregion
}
