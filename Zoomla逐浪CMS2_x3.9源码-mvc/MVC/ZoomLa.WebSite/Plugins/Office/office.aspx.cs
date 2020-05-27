namespace ZoomLaCMS.Plugins.Office
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model.MIS;
    using ZoomLa.BLL.MIS;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Data;
    using System.IO;
    /*
     * 取消权限与步骤绑定,权限改为与用户角色绑定
     */
    public partial class office : System.Web.UI.Page
    {

        B_User buser = new B_User();
        B_OA_Document oaBll = new B_OA_Document();
        B_OA_Sign signBll = new B_OA_Sign();
        B_OA_DocSigned signedBll = new B_OA_DocSigned();
        B_Mis_Model modBll = new B_Mis_Model();
        B_MisProLevel stepBll = new B_MisProLevel();
        B_MisProcedure proceBll = new B_MisProcedure();
        B_Permission perBll = new B_Permission();
        OACommon oacom = new OACommon();
        string[] docarr = "doc,docx,xls,xlsx,rtf".Split(',');
        public string Action { get { return (Request.QueryString["Action"] ?? ""); } }
        public int ProID { get { return DataConverter.CLng(Request.QueryString["ProID"]); } }
        //AppID
        public int AppID { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        //一般情况下传入Word名打开自己的Word,传入AppID,打开申请附带的正文Word,传入Add和流程ID,则新建Word
        public string FName
        {
            get
            {
                if (ViewState["FName"] == null) { ViewState["FName"] = HttpUtility.UrlDecode(Request.QueryString["FName"]); }
                return (ViewState["FName"] as string ?? "");
            }
            set { ViewState["FName"] = value; }
        }
        private DataTable AuthDT { get { return ViewState["AuthDT"] as DataTable; } set { ViewState["AuthDT"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                #region AJAX请求
                string result = "", action = Request["action"], value = Request["value"];
                switch (action)
                {
                    case "signpwdcheck":
                        {
                            int id = DataConverter.CLng(Request["SignID"]);
                            string signpwd = Request["SignPwd"];
                            M_OA_Sign signMod = signBll.SelByPwd(id, signpwd);
                            if (signMod == null)
                            {
                                result = "0";
                            }
                            else
                            {
                                result = signMod.VPath.Replace("//", "/");
                                //-----写入至数据库
                                M_UserInfo mu = buser.GetLogin();
                                M_OA_DocSigned signedMod = new M_OA_DocSigned();
                                signedMod.ProID = ProID;//同名签章在一个流程中只能有一个
                                signedMod.Step = 0;//暂时不使用
                                signedMod.UserID = mu.UserID;
                                signedMod.VPath = signMod.VPath;
                                signedMod.SignID = id;
                                signedMod.SignName = signMod.SignName;
                                signedMod.AppID = AppID;
                                signedMod.CUName = mu.UserName;
                                signedBll.InsertNoDup(signedMod);
                            }

                        }
                        break;
                    default://addword
                        break;
                }
                Response.Clear();
                Response.Write(result); Response.Flush(); Response.End();
                #endregion
            }
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_MisProcedure proceMod = proceBll.SelReturnModel(ProID);
                AuthDT = perBll.SelAuthByRoles(mu.UserRole);
                string getword = "/Plugins/Office/OfficeAction.ashx?action=getword&AppID=" + AppID;
                if (!string.IsNullOrEmpty(FName))
                {
                    getword += "&fname=" + HttpUtility.UrlEncode(FName);
                    function.Script(this, "DelayLoadFile('" + getword + "');");
                    AuthCheck(AuthDT);
                }
                else if (Action.Equals("add") && ProID > 0)//新建
                {
                    FName = GetWordName(proceMod.ProcedureName);
                    function.Script(this, "CreateDoc();");
                    AuthCheck(AuthDT);
                }
                else if (AppID > 0)//根据主键ID,打开其已有文档
                {
                    Sign_DP.Enabled = true;
                    M_OA_Document oaMod = oaBll.SelReturnModel(AppID);
                    if (oaMod.IsComplete) { AuthCheck(null); }//已完结则关闭操作权限
                    else
                    {
                        M_MisProLevel stepMod = stepBll.SelByProIDAndStepNum(oaMod.ProID, oaMod.CurStepNum + 1);
                        AuthCheck(AuthDT);
                    }
                    //------------打开公文
                    if (!string.IsNullOrEmpty(oaMod.PrivateAttach))
                    {
                        FName = Path.GetFileName(HttpUtility.UrlDecode(oaMod.PrivateAttach));
                        function.Script(this, "DelayLoadFile('" + getword + "');");
                    }
                }
                else
                {
                    function.WriteErrMsg("参数不正确,请先核对!");
                }
                MyBind(); BindSigned();
            }
        }
        //改为与用户角色绑定,不与步骤绑定
        public void AuthCheck(DataTable authDT)
        {
            //------------权限
            traceless_btn.Visible = perBll.CheckAuth(authDT, "oa_doc_traceless");
            hastrace_btn.Visible = perBll.CheckAuth(authDT, "oa_doc_hastrace");
            Head_DP.Enabled = perBll.CheckAuth(authDT, "oa_doc_head");
            Sign_DP.Enabled = perBll.CheckAuth(authDT, "oa_doc_sign");
            if (!perBll.CheckAuth(authDT, "oa_doc_edit"))
            {
                save_btn.Visible = false;
                Protect_Hid.Value = "0";
            }
        }
        public string GetWordName(string title)
        {
            //为兼容IE8,不使用docx
            return title + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = signBll.SelByUserID(mu.UserID);
            Sign_DP.DataSource = dt;
            Sign_DP.DataBind();
            Sign_DP.Items.Insert(0, new ListItem("请选择签章", ""));
            Head_DP.DataSource = modBll.SelWordHead();
            Head_DP.DataBind();
            Head_DP.Items.Insert(0, new ListItem("请选择套红", ""));
        }

        //已使用签章数据,新建文档时应隐掉签章选单,否则不好关联数据
        public void BindSigned()
        {
            DataTable dt = signedBll.SelByAppID(AppID);
            Signed_RPT.DataSource = dt;
            Signed_RPT.DataBind();
        }
        protected void Signed_Btn_Click(object sender, EventArgs e)
        {
            //BindSigned();
            //ScriptManager.RegisterStartupScript(SignedList_Panel, SignedList_Panel.GetType(), "", "$('#signedlist_div').show();", true);
        }
    }
}