using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using System.Data.SqlClient;

public partial class MIS_Approval : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_MisApproval bll = new B_MisApproval();
    DataTable dt = new DataTable();
    M_MisApproval model = new M_MisApproval();
    B_MisType Btype = new B_MisType();
    B_MisProcedure Bprocedure = new B_MisProcedure();
    string Inputer ="";
    new string ID="";


    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        Inputer=buser.GetLogin().UserName;
        ID=Request.QueryString["ID"]; 
        if (!IsPostBack)
        {
            BindDrpType();
            BindrepProcedure();
            if (ViewState["PageIndex"] == null)
                ViewState["PageIndex"] = 0;
            //ViewState["UserSource"] = buser.GetNamesList();
            //UserDataBind();
            this.TxtInputer.Text = Inputer;
            if (ID != null)
            {
                dt = bll.Sel(DataConvert.CLng(ID));
                this.TxtContent.Text = dt.Rows[0]["Content"].ToString();
                this.TxtApprover.Text = dt.Rows[0]["Approver"].ToString();
                this.TxtResults.Value = dt.Rows[0]["Results"].ToString();
            }
        }
    }

    protected void BindDrpType()
    {
        this.DrpType.DataSource = Btype.Sels();
        this.DrpType.DataTextField = "TypeName";
        this.DrpType.DataValueField = "ID";
        this.DrpType.DataBind();
        this.DrpType.Items.Insert(0, new ListItem("全部", "0"));
        this.DrpType.SelectedIndex = 0;
    }
    protected void BindrepProcedure()
    {
        this.repProcedure.DataSource = Bprocedure.Sel();
        this.repProcedure.DataBind();
    }

    protected void Button_Click(object sender,EventArgs e) {
        if (string.IsNullOrEmpty(this.HidPro.Value))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('未选定流程');", true); return;
        }
        if (Request.QueryString["ID"] != null)
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            model = bll.SelReturnModel(ID);
            model.content = TxtContent.Text;
            model.Approver = TxtApprover.Text;
            model.Inputer = buser.GetLogin().UserName;
            model.ProcedureID = Convert.ToInt32(this.HidPro.Value);
            model.CreateTime = DateTime.Now;
            model.Results = DataConvert.CLng(this.TxtResults.Value);
            bll.UpdateByID(model);
            function.WriteSuccessMsg("修改成功！", "Default.aspx");
        }
        else
        {
            if (this.TxtInputer.Text == this.TxtApprover.Text)
            {
                function.WriteErrMsg("审批人不能为申请人");
            }
            else
            {
                model.CreateTime = DateTime.Now;
                model.Approver = this.TxtApprover.Text;
                model.Inputer = buser.GetLogin().UserName;
                model.content = this.TxtContent.Text;
                model.ProcedureID =Convert.ToInt32(this.HidPro.Value);
                model.Results = DataConvert.CLng(this.TxtResults.Value);
                if (string.IsNullOrEmpty(this.TxtContent.Text) && string.IsNullOrEmpty(this.TxtApprover.Text))
                {
                    function.Alert("请填写完整的信息");
                }
                else
                {
                    if (bll.insert(model) > 0)
                    {
                        function.WriteSuccessMsg("添加成功！", "Default.aspx");
                    }
                    else
                    {
                        function.WriteErrMsg("添加失败！", "Default.aspx");
                    }
                }
            }
        }
    }
    protected void DrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter[] sp = new SqlParameter[] 
        { 
            new SqlParameter("TypeID",Convert.ToInt32( this.DrpType.SelectedValue))
        };
        this.repProcedure.DataSource = Bprocedure.Sel("TypeID=@TypeID","ID",sp);
        this.repProcedure.DataBind();
    }
}