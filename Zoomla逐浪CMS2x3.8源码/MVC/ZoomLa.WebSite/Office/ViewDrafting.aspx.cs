using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.MIS.OA
{
    public partial class ViewDrafting : System.Web.UI.Page
    {
        public M_OA_Document moa = new M_OA_Document();
        B_OA_Document boa = new B_OA_Document();
        B_User buser = new B_User();
        M_UserInfo minfo = new M_UserInfo();
        B_MisProLevel stepBll = new B_MisProLevel();
        B_MisProcedure proBll = new B_MisProcedure();
        //自由流程
        B_OA_FreePro freeBll = new B_OA_FreePro();
        public string proID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["ID"]);
                moa = boa.SelReturnModel(id);
                minfo = buser.SeachByID(moa.UserID);
                this.LBTitle.Text = moa.Title;
                this.LBKeyWords.Text = moa.Keywords;
                this.AddUSer.Text = minfo.HoneyName;
                this.AddTime.Text = moa.CreateTime.ToString();
                M_MisProLevel freeMod = freeBll.SelByDocID(id);
                if (freeMod != null)
                {
                    if (freeMod.ProID == 0)
                        preViewBtn.Visible = false;
                    RUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.ReferUser);

                    CUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.CCUser);
                    RUserID_Hid.Value = freeMod.ReferUser;
                    CUserID_Hid.Value = freeMod.CCUser;
                }
            }
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Drafting.aspx");
        }
        protected void ListButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Office/Default.aspx");
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Drafting.aspx?Edit=1&appID=" + Request.QueryString["ID"]);
        }
        //确定投递人,生成规则写入表中
        //protected void Free_Sure_Btn_Click(object sender, EventArgs e)
        //{
        //    int id = Convert.ToInt32(Request.QueryString["ID"]);
        //    CreateStep(id);
        //    remind.InnerText = "投递成功,你可以继续<添加公文>,或进入<发文管理>,查看自己的公文.";
        //}
        //// 添加步骤,如步骤存在，则更新步骤
        //public void CreateStep(int id) 
        //{
        //    M_MisProLevel freeMod = freeBll.SelByDocID(id);
        //    moa = boa.SelReturnModel(id);
        //    bool isUpdate = true;
        //    if (freeMod == null)
        //    {
        //        isUpdate = false;
        //        freeMod = new M_MisProLevel(); 
        //    }
        //    freeMod.ProID = 0;
        //    freeMod.stepNum = 1;
        //    freeMod.stepName = "自由流程第1步";
        //    freeMod.SendMan = buser.GetLogin().UserID.ToString();
        //    freeMod.ReferUser = RUserID_Hid.Value.Trim(',');
        //    freeMod.ReferGroup = "";
        //    freeMod.CCUser = "";
        //    freeMod.CCGroup = "";
        //    freeMod.HQoption = 1;
        //    freeMod.Qzzjoption = 0;
        //    freeMod.HToption = 2;
        //    freeMod.EmailAlert = "";
        //    freeMod.EmailGroup = "";
        //    freeMod.SmsAlert = "";
        //    freeMod.SmsGroup = "";
        //    freeMod.BackOption = id;
        //    freeMod.PublicAttachOption = 1;
        //    freeMod.PrivateAttachOption = 1;
        //    freeMod.Status = 1;
        //    freeMod.CreateTime = DateTime.Now;
        //    freeMod.Remind = moa.Title+"的自由流程";
        //    if (isUpdate)
        //    {
        //        freeBll.UpdateByID(freeMod);
        //    }
        //    else 
        //    {
        //        freeBll.Insert(freeMod);
        //    }
        //    RUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.ReferUser);//写入 ViewState中
        //    RUserID_Hid.Value = freeMod.ReferUser;
        //}
    }
}