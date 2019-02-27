namespace ZoomLa.WebSite.User.Content
{
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
    using ZoomLa.Common;    
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Sns.Model;
    using ZoomLa.Sns.BLL;

    public partial class MyFavori : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_User buser = new B_User();
        private B_Favorite bfav = new B_Favorite();
        public M_UserInfo UserInfo;
        private M_Uinfo muif = new M_Uinfo();
        private UserMoreinfo umif = new UserMoreinfo();
        private UserTableBLL utbl = new UserTableBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {               
                RepNodeBind();
            }
        }
        /// <summary>
        /// 绑定数据到GridView
        /// </summary>
        private void RepNodeBind()
        {
            this.UserInfo = buser.GetLogin();
            this.Egv.DataSource = this.bfav.GetFavByUserID(this.UserInfo.UserID);
            this.Egv.DataKeyNames = new string[] { "FavoriteID" };
            this.Egv.DataBind();
        }
        /// <summary>
        /// GridView 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        /// <summary>
        /// 全部选择控件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {

            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bfav.DelFavorite(itemID);
                }
            }
            RepNodeBind();
        }
        /// <summary>
        /// GridView操作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (this.Page.IsValid)
            {                
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bfav.DelFavorite(DataConverter.CLng(Id));
                    RepNodeBind();
                }
            }
        }
        public string GetUrl(string infoid)
        {
            string userface = "";
            int p = DataConverter.CLng(infoid);
            userface = buser.GetUserBaseByuserid(p).UserFace;
            return userface;
        }
        public string GetInfo(string infoid)
        {
            string uinfo = "";
            int p = DataConverter.CLng(infoid);
            muif=buser.GetUserBaseByuserid(p);
            umif = utbl.GetMoreinfoByUserid(p);
            string friendstatus = "";
            switch (buser.GetUserBaseByuserid(buser.GetLogin().UserID).SFStatus)
            { 
                case 0:
                    friendstatus = "征友中";
                    break;
                case 1:
                    friendstatus = "已经找到了";
                    break;
                case -1:
                    friendstatus = "暂停交友";
                    break;
            }
            uinfo = muif.HoneyName + "&nbsp;&nbsp;&nbsp;&nbsp" + (DateTime.Now.Year - DataConverter.CDate(muif.BirthDay).Year) + "岁&nbsp;&nbsp;&nbsp;&nbsp" + umif.UserStature + "cm&nbsp;&nbsp;&nbsp;&nbsp" + umif.UserBachelor + "&nbsp;&nbsp;&nbsp;&nbsp" + friendstatus;
            return uinfo;
        }
    }
}