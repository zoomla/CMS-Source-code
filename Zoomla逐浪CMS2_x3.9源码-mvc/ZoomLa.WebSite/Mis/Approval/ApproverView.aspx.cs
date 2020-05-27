using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.Approval
{
    public partial class ApproverView : System.Web.UI.Page
    {
        new string ID = "";
        DataTable dt = new DataTable();
        B_MisApproval bll = new B_MisApproval();
        /// <summary>
        /// 当前申请模型
        /// </summary>
        M_MisApproval mll = new M_MisApproval();
        B_MisProcedure Bprocedure = new B_MisProcedure();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            ID = Request.QueryString["ID"];
            int appID = DataConverter.CLng(ID);
            if (!IsPostBack)
            {
                //mll = bll.SelReturnModel(appID);
                //if (mll.Results==-1||CurrentLevel.IsComplete||!CurrentLevel.UserID.Contains(buser.GetLogin().UserID.ToString()))//如当前用户无审批权限,后期看是否增加判断,如同名也不显示
                //{
                //    appDiv.Visible = false;
                //}
                //TxtContent.Text = mll.content;
                //TxtInputer.Text = mll.Inputer;
                //TxtTime.Text = mll.CreateTime.ToString();
                //TxtSend.Text = mll.Send;
                //TxtProcess.Text = GetProduceName(mll.ProcedureID);
                //resultL.Text = mll.ResultStr;
                //ResultDataBind();
            }
        }
        private void ResultDataBind()
        {
            DataTable dt = appProgBll.SelByAppID(ID);
            proRepeater.DataSource = dt;
            proRepeater.DataBind();
        }
        protected void BtnSubPeson_Click(object sender, EventArgs e)
        {
            ID = Request.QueryString["ID"];
            if (ID != null)
            {
                string Peson = this.txtPeson.Value;
                string[] strArr;
                strArr = Peson.Split(',');
                for (int i = 0; i < strArr.Length; i++)
                {
                    if (strArr[i].Equals(buser.GetLogin().UserName))
                    {
                        function.WriteErrMsg("抄送人不能为申请人", "ApproverView.aspx?ID=" + ID);
                        break;
                    }
                    else
                    {
                    }
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?type=" + 1);
        }

        protected M_Mis_AppProg appProgModel = new M_Mis_AppProg();
        protected B_Mis_AppProg appProgBll = new B_Mis_AppProg();
        protected M_MisProLevel proModel = new M_MisProLevel();
        protected B_MisProLevel proBll = new B_MisProLevel();

        protected void yesBtn_Click(object sender, EventArgs e)
        {
            InsertRecord(99);
            Response.Redirect(Request.RawUrl);
        }
        protected void noBtn_Click(object sender, EventArgs e)
        {
            InsertRecord(-1);
            Response.Redirect(Request.RawUrl);
        }
        //-----------------Tools
        public void InsertRecord(int status, string remind = "")
        {
            //用PID来存储目前的进度吧
            mll = bll.SelReturnModel(DataConverter.CLng(ID));
            appProgModel.AppID = mll.ID;
            appProgModel.ProID = mll.ProcedureID;

            appProgModel.ProLevel = CurrentLevel.stepNum;
            appProgModel.ProLevelName = CurrentLevel.stepName;

            appProgModel.ApproveID = buser.GetLogin().UserID;
            appProgModel.Result = status;
            appProgModel.Remind = remind;
            appProgModel.CreateTime = DateTime.Now;
            appProgBll.Insert(appProgModel);
            CurrentLevel = null;
            if (status == -1)
            {
                mll.Results = -1;
            }
            //else if (CurrentLevel.IsComplete)
            //{
            //    mll.Results = 99;
            //}
            //else
            //{
            //    mll.Results = 1;
            //}
            bll.UpdateByID(mll);
        }
        //返回需要进行的流程模型
        public M_MisProLevel GetNextLevel()
        {
            int appID = DataConverter.CLng(ID);
            M_MisProLevel model = new M_MisProLevel();
            //获取下一级
            mll = bll.SelReturnModel(appID);//申请模型
            DataTable appProgDT = appProgBll.SelByAppID(ID);//已进行到的流程
            DataTable proLevelDT = proBll.SelByProID(mll.ProcedureID);//全部流程
            if (appProgDT.Rows.Count < 1)//尚未开始
            {
                model = model.GetModelFromDR(proLevelDT.Rows[0]);//用第一个填充,其值是经过Level排序的
            }
            else if (appProgDT.Rows.Count < proLevelDT.Rows.Count)//已经开始但未完成 
            {
                string proLevel = appProgDT.Rows[appProgDT.Rows.Count - 1]["ProLevel"].ToString();//现在进行到的最后
                model = proBll.SelByProIDAndStepNum(mll.ProcedureID, DataConvert.CLng(proLevel));

            }
            else //已完成，或无流程的
            {
                model.Status = 99;
            }
            return model;
        }
        public string GetResult(object o)
        {
            string result = "";
            switch (o.ToString())
            {
                case "-1":
                    result = "<span style='color:red;'>不同意</span>";
                    break;
                case "99":
                    result = "<span style='color:green;'>同意</span>";
                    break;
                default:
                    result = "<span>尚未处理</span>";
                    break;
            }
            return result;
        }
        public string GetProduceName(int ID)
        {
            string result = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", ID) };
            DataTable dt = Bprocedure.Sel("ID=@ID", "ID", sp);
            if (dt != null && dt.Rows.Count > 0)
                result = dt.Rows[0]["ProcedureName"].ToString();
            return result;
        }
        public M_MisProLevel CurrentLevel
        {
            get
            {
                if (ViewState["CurrentLevel"] == null)
                    ViewState["CurrentLevel"] = GetNextLevel();
                return ViewState["CurrentLevel"] as M_MisProLevel;
            }
            set { ViewState["CurrentLevel"] = value; }
        }
    }
}