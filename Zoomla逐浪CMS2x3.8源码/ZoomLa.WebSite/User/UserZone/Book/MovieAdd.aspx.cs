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
using ZoomLa.Sns.BLL;
using ZoomLa.Common;
using ZoomLa.Sns.Model;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL;

public partial class MovieAdd : Page
{
    #region 调用业务对象
    BookBLL bbll = new BookBLL();
    B_User ubll = new B_User();
    #endregion
    int currentUser = 0;
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
            this.titletxt.Text = base.Request.QueryString["BookName"];
            this.Label1.Text = "图片大小不得超过" + SiteConfig.SiteOption.UploadFileMaxSize + "KB";
        
        }
    }
    #endregion

    #region 处理函数 
    protected void sBtn_Click(object sender, EventArgs e)
    {
        try
        {
            BookTable bt = new BookTable();
            bt.BookTitle = this.titletxt.Text;
            bt.BookOtherTitle = this.othertitletxt.Text;
            bt.BookState = 1;
            bt.BookIsbn = this.isbntxt.Text;
            bt.BookAnthor = this.antxt.Text;
            bt.BookConcerm = this.concermtxt.Text;
            bt.BookContent = this.contenttxt.Value.Replace("\n", "<br/>");
            bt.BookYear = this.yeartxt.Text;
            bt.Uid = ubll.GetLogin().UserID;
            bbll.InsertBooktable(bt);
            Response.Redirect("MovieList.aspx");
        }
        catch (Exception ee)
        {
            function.WriteSuccessMsg(ee.Message);
        }
    }
    #endregion
}

