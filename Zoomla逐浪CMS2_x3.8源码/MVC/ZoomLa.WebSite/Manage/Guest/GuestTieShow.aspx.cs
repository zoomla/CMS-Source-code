namespace ZoomLaCMS.Manage.Guest
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Message;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Message;
    using ZoomLa.BLL.Helper;
    public partial class GuestTieShow : System.Web.UI.Page
    {
        protected M_GuestBookCate guestMod = new M_GuestBookCate();
        protected B_Guest_Bar barBll = new B_Guest_Bar();
        protected M_Guest_Bar barMod = new M_Guest_Bar();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_Guest_Medals medalBll = new B_Guest_Medals();
        //所属贴吧id
        protected int CateID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["CateID"]) ? 0 : DataConverter.CLng(Request.QueryString["CateID"]);
            }
        }
        //帖子id
        protected new int ID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["GID"]) ? 0 : DataConverter.CLng(Request.QueryString["GID"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "分类名称";
            function.AccessRulo();
            Egv1.txtFunc = txtPageFunc;
            if (!this.Page.IsPostBack)
            {
                guestMod = cateBll.SelReturnModel(CateID);
                if (guestMod != null)
                    str = guestMod.CateName;
                if (ID <= 0)
                    function.WriteErrMsg("留言ID不正确！", "../Plus/Default.aspx");
                else
                {
                    DataBind();
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='GuestCateMana.aspx?Type=1'>贴吧版面</a></li><li><a href='TieList.aspx?CateID=" + Request.QueryString["CateID"] + "'>" + str + "</a></li><li class='active'>贴子内容</li>");
        }
        public void DataBind(string key = "")
        {
            DataTable dt = barBll.SelByPid(1, int.MaxValue, ID).dt;
            barMod = barBll.SelReturnModel(ID);
            ID_L.Text = barMod.ID.ToString();
            IP_L.Text = barMod.IP == "" ? "无IP地址" : barMod.IP;
            CDate_L.Text = barMod.CDate.ToString();
            CDate_T.Text = barMod.CDate.ToString();
            CUser_LB.Text = barMod.CUName == "" ? "[匿名]" : "<a href=\"javascript:;\" onclick=\"ShowUserInfo(" + barMod.CUser + ")\">" + barMod.CUName + "</a>";//
            Title_L.Text = barMod.Title;
            Title_L.Attributes["style"] += barMod.Style;
            txtTitle.Text = barMod.Title;
            txtTitle.Attributes["style"] += barMod.Style;
            MedalBind();
            string content = StrHelper.DecompressString(barMod.MsgContent);
            MsgContent_L.Text = content;
            MsgContent_T.Text = content;
            Egv1.DataSource = dt;
            Egv1.DataBind();
        }
        public void MedalBind()
        {
            DataTable medaldt = medalBll.SelByBid(ID);
            Medals_Li.Text = medalBll.GetMedalIcon(medaldt);
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv1.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv1.PageSize;
            }
            Egv1.PageSize = pageSize;
            Egv1.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv1.PageIndex = e.NewPageIndex;
            DataBind();
        }
        public string GetUserName(string UserID)
        {
            B_User buser = new B_User();
            string uname = buser.SeachByID(DataConverter.CLng(UserID)).UserName;
            return uname == "" ? "[匿名]" : uname;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("TieList.aspx?CateID=" + CateID);

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReplyGuest.aspx?CateID=" + Request.QueryString["CateID"] + "&GID=" + this.HdnGID.Value);
        }
        //删除等操作
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del":
                    barBll.Del(Convert.ToInt32(e.CommandArgument));
                    Response.Redirect("GuestTieShow.aspx?GID=" + ID + "&CateID=" + CateID);
                    break;
                case "QList":
                    Response.Redirect("GuestTieShow.aspx?GID=" + e.CommandArgument.ToString() + "&CateID=" + CateID);
                    break;
            }
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    barBll.Del(itemID);
                }
            }
            DataBind();
            Response.Redirect("GuestTieShow.aspx?GID=" + ID + "&CateID=" + CateID);
        }
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
        public string GetMsgContent()
        {
            return StrHelper.DecompressString(Eval("MsgContent").ToString());
        }
        protected void Egv_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            barMod = barBll.SelReturnModel(ID);
            barMod.Title = txtTitle.Text;
            barMod.Style = txtStyle_Hid.Value;
            barMod.MsgContent = MsgContent_T.Text;
            barMod.CDate = DateTime.Parse(CDate_T.Text);
            barBll.UpdateByID(barMod);
            DataBind();
        }
        protected void CUser_LB_Click(object sender, EventArgs e)
        {
            barMod = barBll.SelReturnModel(ID);
            if (!string.IsNullOrEmpty(barMod.CUName))
            {
                Response.Redirect("../User/UserInfo.aspx?id=" + barMod.CUser);
            }

        }

        protected void AddMedal_Btn_Click(object sender, EventArgs e)
        {
            barMod = barBll.SelReturnModel(ID);
            if (medalBll.CheckMedalDiff(ID, -1)) { function.WriteErrMsg("您给此贴颁发过勋章了!"); }
            medalBll.Insert(new M_Guest_Medals() { UserID = barMod.CUser, BarID = ID, Sender = -1, MedalID = 3 });
            function.WriteSuccessMsg("颁发成功!");
        }
    }
}