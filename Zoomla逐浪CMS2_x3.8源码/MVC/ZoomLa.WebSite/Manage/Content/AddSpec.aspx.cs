using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Content
{
    public partial class AddSpec : CustomerPageAction
    {
        B_Spec bll = new B_Spec();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        public int Pid
        {
            get
            {
                return DataConverter.CLng(Request.QueryString["Pid"]);
            }
        }
        public int Mid
        {
            get
            {
                return DataConverter.CLng(Request.QueryString["ID"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "checkspecname":
                        result = CheckSpecName();
                        break;
                    case "checkspecdir":
                        result = CheckSpecDir();
                        break;
                    case "checkall":
                        result = CheckSpecName();
                        if (result.Equals("{\"status\":\"1\"}"))
                        { result = CheckSpecDir(); }
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                return;
            }
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='SpecialManage.aspx'>专题管理</a></li><li class=\"active\">添加专题</li>");
                CDate_T.Text = DateTime.Now.ToString();
                CUser_T.Text = badmin.GetAdminLogin().AdminName;
                if (Mid < 1)
                {
                    lblCate.Text = Pid > 0 ? bll.GetSpec(Pid).SpecName : "无";
                }
                else //修改
                {
                    M_Spec info = bll.GetSpec(Mid);
                    if (info.IsNull) { function.WriteErrMsg("要修改的专题不存在"); }
                    TxtSpecName.Text = info.SpecName;
                    TxtSpecDir.Text = info.SpecDir;
                    Image_T.Text = info.SpecPicUrl;
                    EditSpecName_Hid.Value = info.SpecName;
                    EditSpecDir_Hid.Value = info.SpecDir;
                    TxtSpecDesc.Text = info.SpecDesc;
                    RBLOpenType.SelectedValue = DataConverter.CLng(info.OpenNew).ToString();
                    RBLFileExt.SelectedValue = info.ListFileExt.ToString();
                    RBLListFileRule.SelectedValue = info.ListFileRule.ToString();
                    TxtListTemplate_hid.Value = info.ListTemplate;
                    TxtSpeacKeyword.Text = info.SpecKeyword;
                    TxtSpeacTips.Text = info.SpecTips;
                    TxtSpeacDescriptive.Text = info.SpecDescriptive;
                    CUser_T.Text = info.CUser;
                    CDate_T.Text = info.CDate.ToString();
                    M_Spec pinfo = bll.GetSpec(DataConverter.CLng(info.Pid));
                    lblCate.Text = (!pinfo.IsNull) ? pinfo.SpecName : "无";
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_AdminInfo admininfo = badmin.GetAdminLogin();
            M_Spec info = new M_Spec();
            if (Mid > 0) { info = bll.SelReturnModel(Mid); }
            info.SpecName = TxtSpecName.Text;
            info.SpecDir = TxtSpecDir.Text;
            info.SpecPicUrl = Image_T.Text;
            info.SpecDesc = TxtSpecDesc.Text;
            info.OpenNew = DataConverter.CBool(RBLOpenType.SelectedValue);
            info.ListFileExt = DataConverter.CLng(RBLFileExt.SelectedValue);
            info.ListFileRule = DataConverter.CLng(RBLListFileRule.SelectedValue);
            info.ListTemplate = TxtListTemplate_hid.Value;
            info.SpecKeyword = TxtSpeacKeyword.Text;
            info.SpecTips = TxtSpeacTips.Text;
            info.SpecDescriptive = TxtSpeacDescriptive.Text;
            info.CDate = DataConverter.CDate(CDate_T.Text);
            info.CUser = CUser_T.Text;
            info.EditDate = DateTime.Now;
            if (Mid < 1)
            {
                info.Pid = Pid;
                info.OrderID = bll.GetNextOrderID(info.Pid);
                bll.insert(info);
            }
            else
            {
                bll.UpdateByID(info);
            }
            function.WriteSuccessMsg("操作成功", "SpecialManage.aspx");
        }
        string CheckSpecName()
        {
            DataTable SepcSite = bll.GetIsSpecName(Request.Form["specname"]);
            int status = 1;
            if (SepcSite.Rows.Count > 0)
            {
                status = -1;
            }
            return "{\"status\":\"" + status + "\"}";
        }
        string CheckSpecDir()
        {
            DataTable specdirs = bll.GetIsSpecDir(Request.Form["specdir"]);
            int status = 1;
            if (specdirs.Rows.Count > 0)
            { status = -1; }
            return "{\"status\":\"" + status + "\"}";
        }
        protected void TxtSpecName_TextChanged(object sender, EventArgs e)
        {
            DataTable SepcSite = bll.GetIsSpecName(TxtSpecName.Text);
            if (SepcSite.Rows.Count > 0)
            {
                TxtSpecName.Style.Add("color", "red");
                EBtnSubmit.Enabled = false;
                function.WriteErrMsg("发现同栏目下标识名重复，建议定义不同栏目名，请点击确定重新添加节点。");
            }
            else
            {
                EBtnSubmit.Enabled = true;
                TxtSpecName.Style.Add("color", "");
            }

            TxtSpecDir.Text = function.GetChineseFirstChar(TxtSpecName.Text);

            DataTable SepcDir = bll.GetIsSpecName(TxtSpecDir.Text);
            if (SepcDir.Rows.Count > 0)
            {
                TxtSpecDir.Style.Add("color", "red");
                EBtnSubmit.Enabled = false;
                function.WriteErrMsg("发现同栏目下标识名重复，建议定义不同栏目名，请点击确定重新添加节点。");
            }
            else
            {
                EBtnSubmit.Enabled = true;
                TxtSpecDir.Style.Add("color", "");
            }
        }
    }
}