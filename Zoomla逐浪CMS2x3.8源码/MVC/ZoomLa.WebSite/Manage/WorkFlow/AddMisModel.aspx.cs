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

namespace ZoomLaCMS.Manage.WorkFlow
{
    public partial class AddMisModel : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        B_Mis_Model bmis = new B_Mis_Model();
        M_Mis_Model mmis = new M_Mis_Model();
        OACommon oacom = new OACommon();
        private int Mid { get{return DataConvert.CLng(Request.QueryString["ID"]);} }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid > 0)
                {
                    mmis = bmis.SelReturnModel(Mid);
                    ModelName.Text = mmis.ModelName;
                    ModelContent.Text = mmis.ModelContent;
                    DocType_DP.SelectedValue = mmis.DocType.ToString();
                    bindNodeT.Text = mmis.BindNode;
                    if (!string.IsNullOrEmpty(mmis.WordPath)) { WordUP.FileUrl = mmis.WordPath; }
                    BtnSav.Text = "修改";
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='MisModelManage.aspx'>模板管理</a></li><li class='active'>套红管理</a></li>");
            }
        }
        protected void BtnSav_Click(object sender, EventArgs e)
        {
            if (Mid > 0) { mmis = bmis.SelReturnModel(Mid); }
            mmis.ModelName = ModelName.Text;
            mmis.ModelContent = ModelContent.Text;
            mmis.BindNode = bindNodeT.Text.Trim();
            mmis.DocType = Convert.ToInt32(DocType_DP.SelectedValue);
            WordUP.SaveUrl = "/UploadFiles/Manage/WorkFlow/";
            if (!WordUP.FVPath.Equals(mmis.WordPath, StringComparison.CurrentCultureIgnoreCase))
            {
                if (WordUP.HasFile)
                {
                    mmis.WordPath = WordUP.SaveFile();
                }
                else
                {
                    mmis.WordPath = WordUP.FVPath;
                }
            }
            if (mmis.ID > 0)
            {
                bmis.UpdateByID(mmis);
            }
            else
            {
                bmis.insert(mmis);
            }
            Response.Redirect("MisModelManage.aspx");
        }
    }
}