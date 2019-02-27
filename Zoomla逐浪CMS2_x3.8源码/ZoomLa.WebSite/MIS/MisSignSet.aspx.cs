using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MIS_MisSignSet : System.Web.UI.Page
{
    B_MisSign Bms = new B_MisSign();
    M_MisSign Mms = new M_MisSign();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRep();
        }
    }

    private void BindRep()
    {
        dt = Bms.Sel();
        this.repSign.DataSource = dt;
        this.repSign.DataBind();
    }


    protected string StatusName(int id)
    {
        string Names = "";
        switch (id)
        {
            case 0:
                Names = "执行中";
                break;
            case 1:
                Names = "未执行";
                break;
            default:
                break;
        }
        return Names;
    }
}