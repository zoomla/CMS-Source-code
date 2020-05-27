using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
public partial class Manage_WorkFlow : System.Web.UI.Page
{
    B_MisType typeBll = new B_MisType();
    M_MisProcedure proMod = new M_MisProcedure();
    B_MisProcedure proBll = new B_MisProcedure();
    B_Permission perBll = new B_Permission();
    B_User buser = new B_User();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
            if (Mid>0)
            {
                proMod = proBll.SelReturnModel(Mid);
                proNameT.Text = proMod.ProcedureName;
                NickName_T.Text = proMod.SponsorGroup;
                ProClass_DP.SelectedValue = proMod.ClassID.ToString();
                ProType_DP.SelectedValue = proMod.TypeID.ToString();
                //FormType_Rad.SelectedValue = proMod.FormType.ToString();
                FormInfo_T.Text = proMod.FormInfo;
                //manager_Hid.Value = proMod.Manager;
                sponsor_Hid.Value = proMod.Sponsor;
                //manager_T.Text = perBll.GetRoleNameByIDs(proMod.Manager); //buser.GetUserNameByIDS(proMod.Manager);
                sponsor_T.Text = perBll.GetRoleNameByIDs(proMod.Sponsor);
                flowdoc_chk.Checked = proMod.AllowAttach == 1;
                //CanEditField_T.Text = proMod.CanEditField;
                remindT.Text = proMod.Remind;
                FlowTlp_DP.SelectedValue = proMod.FlowTlp;
                PrintTlp_DP.SelectedValue = proMod.PrintTlp;
                function.Script(this, "SetChkVal('docauth_chk','" + proMod.DocAuth + "');");
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='Default.aspx'>流程管理</a></li><li class='active'>添加流程</a></li>");
    }
    private void DataBind(string key="") 
    {
        ProClass_DP.DataSource = typeBll.Sels();
        ProClass_DP.DataTextField = "TypeName";
        ProClass_DP.DataValueField = "ID";
        ProClass_DP.DataBind();
    }
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        if (Mid > 0)
        {
            proMod = proBll.SelReturnModel(Mid);
        }
        proMod.ProcedureName = proNameT.Text.Trim();
        proMod.ClassID = DataConvert.CLng(ProClass_DP.SelectedValue);
        proMod.TypeID = Convert.ToInt32(ProType_DP.SelectedValue);
        //proMod.FormType = Convert.ToInt32(FormType_Rad.SelectedValue);
        proMod.FormInfo = FormInfo_T.Text;
        //proMod.Manager = buser.GetIdsByUserName(manager_T.Text.Trim());
        proMod.Sponsor = sponsor_Hid.Value;
        proMod.SponsorGroup = NickName_T.Text.Trim();
        //proMod.ModelID = modelDP.SelectedValue;
        proMod.AllowAttach = flowdoc_chk.Checked ? 1 : 0;
        proMod.AllowFlow = 1;
        proMod.CanEditField = "*";
        proMod.Remind = remindT.Text.Trim();
        proMod.NodeID = 0;//绑定节点,暂无用
        proMod.DocAuth = Request.Form["docauth_chk"];
        proMod.FlowTlp = FlowTlp_DP.SelectedValue;
        proMod.PrintTlp = PrintTlp_DP.SelectedValue;
        if (Mid > 0)
        {
            proBll.UpdateByID(proMod);
        }
        else
        {
            proBll.insert(proMod);
        }
        function.WriteSuccessMsg("操作成功", "Default.aspx");
    }
}