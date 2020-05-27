using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.BLL;

public partial class MIS_MisSignSetInfo : System.Web.UI.Page
{
    B_MisSign Bms = new B_MisSign();
    M_MisSign Mms = new M_MisSign();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
          
        }
    }
    protected void BtnSub_Click(object sender,EventArgs e)
    {
        int id = Convert.ToInt32(Request["ID"]);
        string dayInfo = "";
        for (int i = 0; i < this.cbkDays.Items.Count; i++)
        {
            if (this.cbkDays.Items[i].Selected)
            {
                dayInfo = dayInfo + "," + this.cbkDays.Items[i].Text;
            }
        }
      
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("MisAttendance.aspx");
    }
}