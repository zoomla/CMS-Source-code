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
using BDUModel;
using BDUBLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL;

public partial class AddPicCateg : Page
{
    protected string type;
        B_User ubll = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {

        PicCateg_BLL CategBll = new PicCateg_BLL();
        PicCateg categ = new PicCateg();
        ubll.CheckIsLogin(); 
        if (!IsPostBack)
        { 
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            this.rblState.Items.Add(new ListItem("任何人都可见", "1"));
            this.rblState.Items.Add(new ListItem("仅好友可见", "2"));
            this.rblState.Items.Add(new ListItem("仅自己可见", "3"));
            this.rblState.Items.Add(new ListItem("设置密码","4"));
            ViewState["userID"] = ubll.GetLogin().UserID;
            rblState.SelectedIndex = 0;
        }
    }

    protected void butEnter_Click(object sender, EventArgs e)
    {
        PicCateg_BLL CategBll = new PicCateg_BLL();
        PicCateg categ = new PicCateg();
        categ.PicCategTitle = txtCategName.Text;
        categ.PicCategUserID = Convert.ToInt32 (ViewState["userID"].ToString());
        categ.State = Convert.ToInt32(rblState.SelectedValue);
        if (rblState.SelectedValue == "4")
        {
            categ.PicCategPws = ViewState["pws"].ToString();
        }
        else {
            categ.PicCategPws = "";
        }
        Guid id = CategBll.Add(categ,ubll.GetLogin().UserID,0);
        Response.Redirect("UpPic.aspx?CategID=" + id);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CategList.aspx?intervieweeID=" + Request.QueryString["intervieweeID"] );
    }

    protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblState.SelectedValue == "4")
        {
            testing.Visible = true;
        }
        else
        {
            testing.Visible = false;
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        ViewState["pws"] = TextBox1.Text;
        testing.Visible = false;
        Response.Write("<script>alert('密码保存为：" + ViewState["pws"] + ",保存好哦！');</script>");
    }
}
