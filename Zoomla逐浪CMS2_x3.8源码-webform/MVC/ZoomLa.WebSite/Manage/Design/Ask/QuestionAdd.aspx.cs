using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Design.Ask
{
    public partial class QuestionAdd : System.Web.UI.Page
    {
        B_Design_Question questBll = new B_Design_Question();
        B_Design_Ask askBll = new B_Design_Ask();
        public int AskID { get { return DataConvert.CLng(ViewState["AskID"]); } set { ViewState["AskID"] = value; } }
        public string QType { get { return DataConvert.CStr(Request.QueryString["QType"]); } }
        public string QFlag { get { return DataConvert.CStr(Request.QueryString["QFlag"]); } }
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Design_Question questMod = new M_Design_Question();
                if (Mid > 0)
                {
                    questMod = questBll.SelReturnModel(Mid);
                    AskID = questMod.AskID;
                }
                else
                {
                    AskID = DataConvert.CLng(Request.QueryString["AskID"]);
                }
                //-------------------------------------------------------
                if (AskID < 1) { function.WriteErrMsg("未指定问卷ID"); }
                M_Design_Ask askMod = askBll.SelReturnModel(AskID);
                AskTitle_T.Text = askMod.Title;
                Ask_Hid.Value = JsonConvert.SerializeObject(askMod);
                Question_Hid.Value = JsonConvert.SerializeObject(questMod);
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Design_Question questMod = JsonConvert.DeserializeObject<M_Design_Question>(Question_Hid.Value);
            if (questMod.ID < 1) { questBll.Insert(questMod); }
            else { questBll.UpdateByID(questMod); }
            function.WriteSuccessMsg("操作成功", "QuestionList.aspx?AskID=" + questMod.AskID);
        }
    }
}