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
	using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public partial class Default : Page
    {
        private int m_t;
        B_User buser = new B_User();
        int currentUser = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = buser.GetLogin().UserID;
            //string url = Request.CurrentExecutionFilePath.ToString();
            //throw new Exception(url);
            if (!this.Page.IsPostBack)
            {
                buser.CheckIsLogin();
                string url = Request.CurrentExecutionFilePath;
                DataTable list = Sql.Sel("ZL_User", "UserID=" + currentUser, "");
                if (list.Rows.Count != 0)
                {

                    string str = list.Rows[0]["seturl"].ToString();
                    string[] strarr = str.Split(',');

                    for (int i = 0; i <= strarr.Length - 1; i++)
                    {
                        if (strarr[i].ToLower() == url.ToLower())
                        {


                            DV_show.Visible = false;
                            Login.Visible = true;
                            return;
                        }

                    }

                }
                this.m_t = DataConverter.CLng(Request.QueryString["t"]);
                if (this.m_t == 0)
                    this.m_t = 1;                
            }
        }

        protected void sure_Click(object sender, EventArgs e)
        {
            M_UserInfo info = buser.GetLogin();
            string PWD = Second.Text;

            if (StringHelper.MD5(PWD) == info.PayPassWord)
            {


                DV_show.Visible = true;
                Login.Visible = false;
            }
            else
            {
                Response.Write("<script>alert('密码错误,请重新输入！');</script>");
                DV_show.Visible = false;
                Login.Visible = true;
                ;
            }
        }
        public int Tnum
        {
            get { return this.m_t; }
        }
    }
}