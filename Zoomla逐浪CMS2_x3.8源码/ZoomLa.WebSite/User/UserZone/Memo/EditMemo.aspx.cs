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
using BDUBLL;
using BDUModel;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class EditMemo :Page
{
    Memo_BLL memobll = new Memo_BLL();
    UserMemo usermemo = new UserMemo();
    B_User ubll = new B_User();
    private bool isnew
    {
        get
        {
            if (ViewState["ID"] != null)
                return bool.Parse(ViewState["ID"].ToString());
            else return true;
        }
        set
        {
            ViewState["ID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    { 
        ubll.CheckIsLogin(); 
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            if (Request.QueryString["ID"] != null)
            {
                ViewState["ID"] = Request.QueryString["ID"].ToString();
                if (!ViewState["ID"].Equals("1"))
                {
                    usermemo = memobll.GetMemo(new Guid(ViewState["ID"].ToString()));
                    txtTitle.Text = usermemo.MemoTitle;
                    txtContext.Text = usermemo.MemoContext.Replace("<br/>","\n");
                    txtTime.Text = usermemo.MemoTime.ToShortDateString ().ToString();
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        usermemo.MemoContext = this.txtContext.Text.Replace("\n","<br/>");
        usermemo.MemoTitle = this.txtTitle.Text;
        usermemo.MemoTime = Convert.ToDateTime(this.txtTime.Text.Trim());
        usermemo.UserID = ubll.GetLogin().UserID;
            
        if (Request.QueryString["ID"] != null)
        {
            usermemo.ID = new Guid(Request.QueryString["ID"].ToString());
            memobll.Update(usermemo);
        }
        else
        {
            memobll.Add(usermemo);
        }
        Page.Response.Redirect("MemoList.aspx");
    }
}

