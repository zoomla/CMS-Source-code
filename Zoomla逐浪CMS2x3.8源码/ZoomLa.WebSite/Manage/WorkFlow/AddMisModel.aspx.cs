using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_WorkFlow_AddMisModel : System.Web.UI.Page
{
    protected B_Admin badmin = new B_Admin();
    protected B_Mis_Model bmis = new B_Mis_Model();
    protected M_Mis_Model mmis = new M_Mis_Model();
    OACommon oacom = new OACommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                mmis = bmis.SelReturnModel(DataConvert.CLng(Request.QueryString["ID"]));
                ModelName.Text = mmis.ModelName;
                ModelContent.Text = mmis.ModelContent;
                DocType_DP.SelectedValue = mmis.DocType.ToString();
                bindNodeT.Text = mmis.BindNode;
                WordUP_T.Text = mmis.WordPath;
                BtnSav.Text = "修改";
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='MisModelManage.aspx'>模板管理</a></li><li class='active'>套红管理</a></li>");
    }
    protected void BtnSav_Click(object sender, EventArgs e)
    {
        mmis.ModelName = ModelName.Text;
        mmis.ModelContent = ModelContent.Text;
        mmis.CreateTime = DateTime.Now;
        mmis.BindNode = bindNodeT.Text.Trim();
        mmis.DocType = Convert.ToInt32(DocType_DP.SelectedValue);
        mmis.WordPath = WordUP_T.Text.Trim();
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            mmis.ID = DataConvert.CLng(Request.QueryString["ID"]);
            bmis.UpdateByID(mmis);
        }
        else
        {
            bmis.insert(mmis);
        }
        Response.Redirect("MisModelManage.aspx");
    }
    protected void WordUP_Btn_Click(object sender, EventArgs e)
    {
        if (oacom.IsWord(Word_UP.FileName))
        {
            string vpath = "/Manage/WorkFlow/" + Path.GetFileName(Word_UP.FileName);
            if(!Word_UP.SaveAs(vpath))
            {
                function.WriteErrMsg(Word_UP.ErrorMsg);
            }
            WordUP_T.Text =vpath;
        }
        else
        {
            function.Script(this, "alert('仅允许上传doc或docx文件');");
        }
    }
}