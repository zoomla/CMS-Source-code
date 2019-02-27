using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.OA.Flow
{
    public partial class ApplyList : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_OA_Document oaBll = new B_OA_Document();
        B_OA_Borrow borBll = new B_OA_Borrow();
        B_MisProcedure proBll = new B_MisProcedure();
        B_OA_FreePro freeBll = new B_OA_FreePro();
        M_OA_Document oaMod = new M_OA_Document();
        /// <summary>
        /// 需要查看处理哪个状态的公文与事务
        /// </summary>
        public string CurrentView
        {
            get
            {
                if (ViewState["CurrentView"] == null)
                    ViewState["CurrentView"] = Request.QueryString["view"];
                return ViewState["CurrentView"] as string;
            }
            set
            {
                ViewState["CurrentView"] = value;
            }
        }
        public int ProID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
        private string SearchKey
        {
            get
            {
                return ViewState["SearchKey"] as string;
            }
            set
            {
                ViewState["SearchKey"] = value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string proName = "";
                if (ProID > 0)
                {
                    M_MisProcedure proceMod = proBll.SelReturnModel(ProID);
                    if (proceMod != null)
                    {
                        proName = "[" + proceMod.ProcedureName + "]";
                    }
                }
                switch (CurrentView)
                {
                    case "1":
                        CurView_L.Text = "待办公文" + proName;
                        break;
                    case "2":
                        CurView_L.Text = "已办公文" + proName;
                        break;
                    case "3":
                        CurView_L.Text = "我的公文" + proName;
                        break;
                    case "4":
                        CurView_L.Text = "我的借阅" + proName;
                        break;
                }
                MyBind();
            }
        }
        protected void ProBind()
        {
            DataTable dt = proBll.Sel();
            ProRPT.DataSource = dt;
            ProRPT.DataBind();
        }
        protected void MyBind(string keys = "")
        {
            DataTable dt = new DataTable();
            M_UserInfo mu = buser.GetLogin();

            switch (CurrentView)
            {
                case "1"://待办有经办权限的
                    dt = oaBll.SelDocByUid(mu.UserID);//SelByDocType
                    break;
                case "2"://已办
                    dt = oaBll.SelByApproveID(mu.UserID);
                    break;
                case "3"://我的文档(未归档)
                    dt = oaBll.SelByUserID(mu.UserID);
                    break;
                case "4"://我的借阅
                    dt = borBll.SelByUid(mu.UserID);
                    break;
            }
            if (ProID > 0)//按流程筛选,后期整合入Bll与Skey一起
            {
                dt.DefaultView.RowFilter = "ProID='" + ProID + "'";
                dt = dt.DefaultView.ToTable();
            }
            if (!string.IsNullOrEmpty(SearchKey))
            {
                dt.DefaultView.RowFilter = "Title Like '%" + SearchKey + "%'";
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
            ProBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del1":
                    //boad.DeleteByID(e.CommandArgument.ToString());
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功');", true);
                    //DataBind();
                    return;
            }
        }
        public string GetStatus(string Status)
        {
            string flag = "";
            switch (Status)
            {
                case "-1":
                    flag = "<span style='color:red;'>不同意</span>";
                    break;
                case "0":
                case "2":
                case "98":
                    flag = "<span style='color:blue;'>进行中</span>";
                    break;
                case "99":
                    flag = "<span style='color:blue;'>已完成</span>";
                    break;
                default:
                    flag = "<span style='color:blue;'>进行中</span>";
                    break;
            }
            return flag;
        }
        //根据CurView显示不同的操作栏
        public string GetLinks()
        {
            string editTlp = "<a href='/Office/Flow/FlowApply.aspx?Appid=" + Eval("ID") + "' >修改</a>";
            string delTlp = "<a href='javascript:;' onclick='SetDel(" + Eval("ID") + ");' style='margin-left:5px;'>删除</a>";
            string dealTlp = "<a href='/Office/FreeFlow/FlowAudit.aspx?Appid=" + Eval("ID") + "' style='margin-left:5px;'>办理</a>";
            string viewTlp = "<a href='/Office/FreeFlow/FlowAudit.aspx?Appid=" + Eval("ID") + "&action=view' style='margin-left:5px;'>查看</a>";
            //仅用于第一步&&该公文尚未被处理,可清空下一步主办人
            string withdrawTlp = "<a href='javascript:;' onclick='SetWithDraw(" + Eval("ID") + ");' style='margin-left:5px;'>撤回</a>";
            string tlp = "";
            if (Convert.ToInt32(Eval("CurStepNum")) > 0)
            {
                editTlp = "<a href='javascript:;' style='color:gray;' title='流程已开始,禁止修改'>修改</a>";
                delTlp = "<a href='javascript:;' style='color:gray;margin-left:5px;' title='流程已开始,禁止删除'>删除</a>";
                withdrawTlp = "<a href='javascript:;' style='color:gray;margin-left:5px;' title='流程已开始,禁止撤回'>撤回</a>";
            }
            switch (CurrentView)
            {
                case "1":
                    tlp = dealTlp + viewTlp;
                    break;
                case "2":
                case "3":
                    tlp = editTlp + delTlp + viewTlp + withdrawTlp;
                    break;
                case "4":
                    tlp = viewTlp;
                    break;
                default:
                    tlp = viewTlp;
                    break;
            }
            return tlp;
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                int appid = Convert.ToInt32(dr["ID"]);
                e.Row.Attributes.Add("ondblclick", "location='/Office/FreeFlow/FlowAudit.aspx?Appid=" + appid + "';");
                e.Row.ToolTip = "双击查看";
            }
        }
        protected void Del_Btn_Click(object sender, EventArgs e)
        {
            int id = DataConvert.CLng(DelID_Hid.Value);
            M_UserInfo mu = buser.GetLogin();
            if (id > 0)
            {
                M_OA_Document oaMod = oaBll.SelReturnModel(id);
                if (oaMod.CurStepNum > 0 || oaMod.UserID != mu.UserID) { function.WriteErrMsg("已开始流程,不可删除"); }
                else
                {
                    oaBll.Del(id);
                    MyBind();
                }
            }
        }
        protected void WithDraw_Btn_Click(object sender, EventArgs e)
        {
            int id = DataConvert.CLng(DelID_Hid.Value);
            M_UserInfo mu = buser.GetLogin();
            if (id > 0)
            {
                M_OA_Document oaMod = oaBll.SelReturnModel(id);
                if (oaMod.CurStepNum > 0 || oaMod.UserID != mu.UserID) { function.WriteErrMsg("流程已开始,不可撤回"); }
                else
                {
                    freeBll.DelByStep(oaMod.ID, 0);
                    function.WriteSuccessMsg("撤回成功!");
                }
            }
        }
    }
}