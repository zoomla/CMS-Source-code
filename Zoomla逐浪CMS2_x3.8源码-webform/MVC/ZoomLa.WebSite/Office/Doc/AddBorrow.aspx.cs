using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.MIS;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.MIS;
using ZoomLa.Model.User;

namespace ZoomLaCMS.MIS.OA.Doc
{
    public partial class AddBorrow : System.Web.UI.Page
    {
        B_OA_Document oaBll = new B_OA_Document();
        B_User buser = new B_User();
        B_OA_Borrow borBll = new B_OA_Borrow();
        B_Permission perBll = new B_Permission();
        private string DocIDS { get { return Request.QueryString["ids"]; } }
        private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!perBll.CheckAuth(buser.GetLogin().UserRole, "oa_pro_file")) { function.WriteErrMsg("你没有访问该页面的权限"); }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            if (Mid > 0)
            {
                M_OA_Borrow borMod = borBll.SelReturnModel(Mid);
                ids_Hid.Value = borMod.DocIDS;
                ids_T.Text = borMod.DocTitles;
                UserIDS_Hid.Value = borMod.Uids;
                UserIDS_T.Text = borMod.UNames;
                EDate_T.Text = borMod.EDate.ToString();
            }
            else
            {
                ids_Hid.Value = DocIDS;
                ids_T.Text = oaBll.SelTitleByIDS(DocIDS);
                EDate_T.Text = DateTime.Now.AddDays(7).ToString();
            }
        }
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserIDS_Hid.Value) || string.IsNullOrEmpty(ids_Hid.Value))
            {
                function.WriteErrMsg("未指定借阅人,或需要借阅的文档!");
            }
            M_UserInfo mu = buser.GetLogin();
            M_OA_Borrow borMod = new M_OA_Borrow();
            if (Mid > 0)
            {
                borMod = borBll.SelReturnModel(Mid);
            }
            borMod.UserID = mu.UserID;
            borMod.Uids = StrHelper.IdsFormat(UserIDS_Hid.Value);//借阅人
            borMod.UNames = buser.GetUserNameByIDS(borMod.Uids);
            borMod.DocIDS = StrHelper.IdsFormat(ids_Hid.Value);//借阅公文
            borMod.DocTitles = oaBll.SelTitleByIDS(borMod.DocIDS);
            borMod.EDate = Convert.ToDateTime(EDate_T.Text);
            if (Mid > 0)
            {
                borBll.UpdateByID(borMod);
            }
            else
            {
                borBll.Insert(borMod);
            }
            Response.Redirect("BorrowList.aspx");
        }
    }
}