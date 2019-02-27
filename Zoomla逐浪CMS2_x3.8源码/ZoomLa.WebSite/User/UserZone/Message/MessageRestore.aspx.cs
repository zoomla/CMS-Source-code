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
using FHBLL;
using FHModel;
using BDUBLL;
using BDUModel;
using ZoomLa.BLL;

namespace FreeHome.User
{
    public partial class MessageRestore : Page
    {
        #region 业务对象
        MessageboardBLL mebll = new MessageboardBLL();
        #endregion

        B_User bu = new B_User();
        int currentUser = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = bu.GetLogin().UserID;
            bu.CheckIsLogin();
            if (!IsPostBack)
            {
                ViewState["rID"] = Request.QueryString["rID"];
            }
        }

        #region 页面调用方法
        private Guid rID
        {
            get
            {
                if (ViewState["rID"] != null)
                    return new Guid(ViewState["rID"].ToString());
                else return Guid.Empty;
            }
            set
            {
                ViewState["rID"] = value;
            }
        }
       

        protected void savebtn_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            Messageboard me = new Messageboard();
            me.RestoreID = rID;
            me.Mcontent = this.TEXTAREA1.Value;
            me.SendID = currentUser;
            mebll.InsertMessage(me);
            Page.ClientScript.RegisterStartupScript(typeof(string), "TabJs", "<script>window.returnVal='" + rd.NextDouble() + "';window.parent.hidePopWin(true);window.parent.location.href='MyMessage.aspx';</script>");
        }

        #endregion
    }
}
