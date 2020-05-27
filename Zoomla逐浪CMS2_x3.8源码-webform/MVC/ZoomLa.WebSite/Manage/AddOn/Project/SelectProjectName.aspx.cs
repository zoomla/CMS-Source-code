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


namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class SelectProjectName : System.Web.UI.Page
    {
        private B_Client_Basic bll = new B_Client_Basic();
        private B_Client_Enterprise ell = new B_Client_Enterprise();
        private B_Client_Penson pll = new B_Client_Penson();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.HideBread(Master);
                if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "select")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    M_Client_Basic Binfo = bll.GetSelect(id);
                    string types = Binfo.Client_Type;
                    string Code = Binfo.Code;
                    string scripttxt = "setvalue('TxtUserName','" + Binfo.P_name + "');";
                    if (types == "0")
                    {
                        DataTable ptable = pll.SelByCode(Binfo.Code);
                        if (ptable != null && ptable.Rows.Count > 0)
                        {
                            M_Client_Penson pinfo = pll.GetSelect(DataConverter.CLng(ptable.Rows[0]["Flow"]));
                            scripttxt += "setvalue('TxtUserTel','" + pinfo.Fax_phone + "');";
                            scripttxt += "setvalue('TxtUserMobile','" + pinfo.Work_Phone + "');";
                            scripttxt += "setvalue('TxtUserQQ','" + pinfo.QQ_Code + "');";
                            scripttxt += "setvalue('TxtUserMSN','" + pinfo.MSN_Code + "');";
                            scripttxt += "setvalue('TxtUserAddress','" + pinfo.Message_Address + "');";
                            scripttxt += "setvalue('TxtUserEmail','" + pinfo.Email_Address + "');";
                        }
                        function.Script(this, scripttxt + ";onstr();");
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>" + scripttxt + ";onstr();</script>");
                    }
                    else
                    {
                        DataTable pstable = ell.SelByCode(Binfo.Code);
                        if (pstable != null && pstable.Rows.Count > 0)
                        {
                            M_Client_Enterprise einfo = ell.GetSelect(DataConverter.CLng(pstable.Rows[0]["Flow"]));
                            scripttxt += "setvalue('TxtUserTel','" + einfo.Phone + "');";
                            scripttxt += "setvalue('TxtUserMobile','" + einfo.Phone + "');";
                            scripttxt += "setvalue('TxtUserAddress','" + einfo.Message_Address + "');";
                        }
                        function.Script(this, scripttxt + ";onstr();");
                    }
                }
                DataTable tables = bll.Select_All();
                tables.DefaultView.Sort = "Flow desc";
                Page_list(tables);
            }
        }
        public void Page_list(DataTable Cll)
        {
            if (Cll != null)
            {
                RPT.DataSource = Cll;
                RPT.DataBind();
            }
        }
    }
}