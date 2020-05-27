using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.OA.Flow
{
    public partial class FlowApply : System.Web.UI.Page
    {
        B_Content conBll = new B_Content();
        B_User buser = new B_User();
        B_ModelField fieldBll = new B_ModelField();
        B_Model bmode = new B_Model();
        B_MisProcedure proceBll = new B_MisProcedure();
        B_MisProLevel stepBll = new B_MisProLevel();
        B_OA_Document oaBll = new B_OA_Document();
        B_OA_FreePro freeBll = new B_OA_FreePro();
        B_OA_ShowField showBll = new B_OA_ShowField();
        B_Permission perBll = new B_Permission();
        OACommon oaCom = new OACommon();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["AppID"]); } }
        public int ProID
        {
            get { return Convert.ToInt32(ViewState["ProID"]); }
            set { ViewState["ProID"] = value; }
        }
        public int NodeID { get { return DataConverter.CLng(ViewState["NodeID"]); } set { ViewState["NodeID"] = value; } }
        public int ModelID { get { return Convert.ToInt32(ViewState["ModelID"]); } set { ViewState["ModelID"] = value; } }
        //绑定模板名
        private string ascx { get { return (ViewState["ascx"] as string); } set { ViewState["ascx"] = value; } }
        private B_OAFormUI OAFormUI
        {
            get
            {
                var control = OAForm_Div.FindControl("ascx_" + ascx);
                if (control == null)//加载默认
                {
                    control = OAForm_Div.FindControl("ascx_def");
                    control.Visible = true;
                    return (B_OAFormUI)control;
                }
                else { control.Visible = true; return (B_OAFormUI)control; }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_MisProcedure proceMod = new M_MisProcedure();
                if (Mid < 1)
                {
                    ProID = DataConverter.CLng(Request.QueryString["ProID"]);
                    if (ProID < 1) { function.WriteErrMsg("请先指定需要创建的流程!"); }
                    proceMod = proceBll.SelReturnModel(ProID);
                    if (!perBll.ContainRole(proceMod.Sponsor, mu.UserRole)) { function.WriteErrMsg("你没有权限使用该流程"); }
                    ascx = proceMod.FlowTlp;
                    ModelID = Convert.ToInt32(proceMod.FormInfo);
                    OAFormUI.InitControl(ViewState, ModelID);
                    switch (proceMod.MyProType)
                    {
                        case M_MisProcedure.ProTypes.Admin:
                            SelUser_Tr.Visible = false;
                            break;
                    }
                    ViewState["No"] = CreateNo(proceMod);
                    //OAFormUI.Title_T = proceMod.ProcedureName;
                    OAFormUI.SendDate_ASCX = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm");
                }
                else
                {
                    M_OA_Document oaMod = new M_OA_Document();
                    oaMod = oaBll.SelReturnModel(Mid);
                    if (mu.UserID != oaMod.UserID) { function.WriteErrMsg("非发起人不能修改文档!"); }
                    if (oaBll.HasBegin(oaMod.ID)) { function.WriteErrMsg("已开始的流程不能修改"); }
                    ProID = oaMod.ProID;
                    proceMod = proceBll.SelReturnModel(oaMod.ProID);
                    FName_Hid.Value = oaMod.PrivateAttach;//Word文档
                    ascx = proceMod.FlowTlp;
                    ModelID = Convert.ToInt32(proceMod.FormInfo);
                    OAFormUI.InitControl(ViewState, ModelID);
                    OAFormUI.Title_ASCX = oaMod.Title;
                    OAFormUI.SendDate_ASCX = oaMod.SendDate.ToString();
                    OAFormUI.NO_ASCX = oaMod.No;
                    M_MisProLevel freeMod = freeBll.SelByDocID(oaMod.ID);
                    if (freeMod != null)
                    {
                        ReferUser_T.Text = buser.GetUserNameByIDS(freeMod.ReferUser);
                        ReferUser_Hid.Value = freeMod.ReferUser;
                    }
                    ViewState["No"] = oaMod.No;
                    Save_Btn.Text = "修改发文";
                }
                OAFormUI.NO_ASCX = ViewState["No"].ToString();
                NodeID = proceMod.NodeID;
                ShowPage(proceMod);
            }
        }
        //保存
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            OAFormUI.vstate = ViewState;
            Call commonCall = new Call();
            M_OA_Document oaMod = null;
            if (Mid > 0)
            {
                oaMod = oaBll.SelReturnModel(Mid);
            }
            oaMod = FillMod(oaMod);
            M_MisProcedure proceMod = proceBll.SelReturnModel(ProID);
            DataTable table = OAFormUI.CreateTable(OAFormUI.GetFields(OAFormUI.ModelID).Split(','));
            if (Mid > 0)//修改
            {
                M_CommonData conMod = conBll.SelReturnModel(Convert.ToInt32(oaMod.Content));
                conBll.UpdateContent(table, conMod);
                oaBll.UpdateByID(oaMod);
            }
            else
            {
                oaMod.Content = commonCall.AddContentToNode(oaMod, NodeID, OAFormUI.ModelID, table).ToString();
                oaMod.ID = oaBll.insert(oaMod);
            }
            oaCom.CreateStep(oaMod, proceMod, new OAStepParam()
            {
                IsFirst = true,
                ReferUser = ReferUser_Hid.Value,
                StepNum = 1
            });
            Response.Redirect("ViewDrafting.aspx?ID=" + oaMod.ID + "&FlowType=2");
        }
        //------------Tools
        public M_OA_Document FillMod(M_OA_Document oaMod = null)
        {
            M_UserInfo mu = buser.GetLogin();
            if (oaMod == null) { oaMod = new M_OA_Document(); }
            M_MisProcedure proceMod = proceBll.SelReturnModel(ProID);
            oaMod.UserID = mu.UserID;
            oaMod.CreateTime = DateTime.Now;
            oaMod.Status = 0;
            oaMod.ProID = ProID;
            //oaMod.Branch = groupBll.GetGroupNameByID(mu.GroupID.ToString());
            oaMod.CurStepNum = 0;
            oaMod.PublicAttach = Attach_Hid.Value.Trim('|');
            oaMod.PrivateAttach = FName_Hid.Value;
            //-----------
            oaMod.Title = OAFormUI.Title_ASCX;
            oaMod.SendDate = DataConverter.CDate(OAFormUI.SendDate_ASCX);
            oaMod.No = ViewState["No"].ToString();
            oaMod.ProType = proceMod.TypeID;
            return oaMod;
        }
        public void ShowPage(M_MisProcedure proceMod)
        {
            if (Mid > 0)
            {
                M_OA_Document oaMod = oaBll.SelReturnModel(Mid);
                DataTable dtContent = conBll.GetContent(Convert.ToInt32(oaMod.Content));
                if (dtContent == null || dtContent.Rows.Count < 1) { return; }
                OAFormUI.dataRow = dtContent.Rows[0];
                OAFormUI.MyBind();
            }
        }
        private string CreateNo(M_MisProcedure proceMod)
        {
            //20150406190538
            return DateTime.Now.ToString(proceMod.ID + "yyyyMMddHHmmss");
        }
        ////动态加载自定义表单
        //private B_OAFormUI LoadControl(string ascxName)
        //{
        //    UserControl control = null;
        //    control = (UserControl)Page.LoadControl(ascxName);
        //    control.ID = "FormUI";
        //    OAForm_Div.Controls.Add(control);
        //    return (B_OAFormUI)control;
        //}
    }
}