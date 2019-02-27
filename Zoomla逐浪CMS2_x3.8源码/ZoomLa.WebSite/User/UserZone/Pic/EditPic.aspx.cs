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
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLa.WebSite.User.UserZone.Pic
{
    public partial class EditPic : Page
    {
        protected string CategName;
        private PicTure picture = new PicTure();
        private PicTure_BLL ture = new PicTure_BLL();
        private PicCateg_BLL categ = new PicCateg_BLL();
        private PicCateg piccateg = new PicCateg();
            B_User ubll = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ubll.CheckIsLogin(); 
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            ViewState["PicID"] = Request.QueryString["picID"];
            picture=ture.GetPic(new Guid(ViewState["PicID"].ToString()));
            piccateg=categ.GetPicCateg(picture.PicCategID);
            CategName = piccateg.PicCategTitle;
            ViewState["picurl"] = picture.PicUrl;
            ViewState["c"] = picture.PicCategID;
            if (piccateg.PicCategUserID == ubll.GetLogin().UserID)
            {
                Image1.ImageUrl = picture.PicUrl;
                this.TextBox1.Text = picture.PicName;
                this.TextBox2.Text = picture.Remark;
            }
            else
            {
                Response.End();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            picture.ID = new Guid(ViewState["PicID"].ToString());
            picture.PicName = Request.Form["TextBox1"];
            picture.Remark = Request.Form["TextBox2"];
            ture.Update(picture);
            Page.Response.Redirect("ShowPic.aspx?picID=" + ViewState["PicID"]);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DelPic.CleanFiles(AppDomain.CurrentDomain.BaseDirectory + ViewState["picurl"]);
            //ture.DelPic(new Guid(ViewState["PicID"].ToString()));
            Response.Write("<script>location.href='PicTureList.aspx?CategID=" + ViewState["c"] + "'</script>");
        }
    }
}
