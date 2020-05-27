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
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLa.WebSite.User.UserZone.Pic
{
    public partial class EditCateg : Page
    {
        protected string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            PicCateg_BLL CategBll = new PicCateg_BLL();
            Guid CategID=new Guid ();
            PicCateg categ=new PicCateg (); 
            B_User ubll = new B_User();
            ubll.CheckIsLogin(); 
            if (!IsPostBack)
            {
                try
                {
                    M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
                    CategID = new Guid(Request.QueryString["CategID"]);
                    categ = CategBll.GetPicCateg(CategID);
                    txtCategName.Text = categ.PicCategTitle;
                    ViewState["pws"] = categ.PicCategPws;
                }
                catch (ArgumentNullException)
                {
                    Response.End();
                }
                this.rblState.Items.Add(new ListItem("�κ��˶��ɼ�", "1"));
                this.rblState.Items.Add(new ListItem("�����ѿɼ�", "2"));
                this.rblState.Items.Add(new ListItem("���Լ��ɼ�", "3"));
                this.rblState.Items.Add(new ListItem("��������", "4"));
                type = "�޸��ҵ�"+categ.PicCategTitle;
                ViewState["CategID"] = CategID;
               
                try
                {
                    rblState.SelectedValue = categ.State.ToString();
                }
                catch
                {
                    rblState.SelectedIndex = 0;
                }
            }
        }

        protected void butEnter_Click(object sender, EventArgs e)
        {
            PicCateg_BLL CategBll=new PicCateg_BLL ();
            PicCateg categ=new PicCateg ();
            categ.ID = new Guid(ViewState["CategID"].ToString());
            categ.State=Convert.ToInt32(rblState.SelectedValue) ;
            categ.PicCategPws = ViewState["pws"].ToString();
            categ.PicCategTitle=txtCategName.Text;
            Guid id=CategBll.Update(categ);
            Page.Response.Redirect("PicTureList.aspx?CategID=" + Request.QueryString["CategID"] + "&where=2");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("PicTureList.aspx?CategID=" + Request.QueryString["CategID"]+"&where=2");
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
            Response.Write("<script>alert('���뱣��Ϊ��" + ViewState["pws"] + ",�����Ŷ��');</script>");
        }
}
}
