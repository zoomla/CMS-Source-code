using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL.Helper;
namespace ZoomLaCMS.MIS.OA.Flow
{
    public partial class ViewDrafting : System.Web.UI.Page
    {
        public M_OA_Document moa = new M_OA_Document();
        protected B_OA_Document boa = new B_OA_Document();
        protected B_User buser = new B_User();
        protected M_UserInfo minfo = new M_UserInfo();
        //自由流程
        B_OA_FreePro freeBll = new B_OA_FreePro();
        B_MisProLevel stepBll = new B_MisProLevel();
        //OA_Document ID;
        private int Mid { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                moa = boa.SelReturnModel(Mid);
                minfo = buser.SeachByID(moa.UserID);
                this.LBTitle.Text = moa.Title;
                this.LBKeyWords.Text = moa.Keywords;
                this.AddUSer.Text = minfo.HoneyName;
                this.AddTime.Text = moa.CreateTime.ToString();
                title_lab.Text = moa.Title;
                docType_Tr.Visible = false;
                switch (moa.ProType)
                {
                    case (int)M_MisProcedure.ProTypes.Admin:
                        //M_MisProLevel stepMod = stepBll.SelByProIDAndStepNum(moa.ProID,1);
                        //RUserName_Lab.Text = buser.GetUserNameByIDS(stepMod.ReferUser);
                        break;
                    default:
                        M_MisProLevel freeMod = freeBll.SelByDocID(Mid);
                        if (freeMod != null)
                        {
                            if (freeMod.ProID == 0)
                                preViewBtn.Visible = false;
                            RUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.ReferUser);
                            //CUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.CCUser);
                            //RUserID_Hid.Value = freeMod.ReferUser;
                            //CUserID_Hid.Value = freeMod.CCUser;
                        }
                        break;
                }
                string steps = StrHelper.GetIDSFromDT(stepBll.SelByProID(moa.ProID), "StepName");
                if (!string.IsNullOrEmpty(steps)) { function.Script(this, "$(\"#prog_div\").ZLSteps(\"" + steps + "\");"); }
            }
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FlowList.aspx");
        }
        protected void ListButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplyList.aspx?view=3");
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FlowApply.aspx?appID=" + Mid);
        }
    }
}