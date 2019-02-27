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
using System.Collections.Generic;
using BDUBLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Sns.BLL;

namespace ZoomLa.WebSite.User.UserZone.Pic
{
    public partial class ShowPic : Page 
    {
        public  string upurl = "";
        public string downurl = "";
        public string UserName, Name, CategName;
        public PicTure_BLL turebll = new PicTure_BLL();
        public PicTure picture = new PicTure();
        public PicCritique piccritique = new PicCritique();
        private List<PicTure> ture = new List<PicTure>();
        private PicCateg_BLL categ = new PicCateg_BLL();
        private PicCateg piccateg = new PicCateg();
        private UserTable usertable = new UserTable();
        private UserTableBLL userbll = new UserTableBLL();
        B_User ubll = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ubll.CheckIsLogin();
            if (!IsPostBack)
            {
                M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
                ViewState["picID"] = Request.QueryString["picID"];
                GetCurentPic();
                Bind();
            }
        }


        private void GetCurentPic()
        {
            picture = turebll.GetPic(new Guid(ViewState["picID"].ToString()));

            ture = turebll.GetPicTureList(picture.PicCategID, null);
            ViewState["CategID"] = ture[0].PicCategID;
            piccateg = categ.GetPicCateg(new Guid(ViewState["CategID"].ToString()));
            CategName = piccateg.PicCategTitle;

            Name = "我";
            this.Panel1.Visible = true;

            UserName = "我";
            this.img.ImageUrl = picture.PicUrl;
            //ImageBind();

            this.labCount.Text = "共" + ture.Count + "条记录";
            int i = -1;
            foreach (PicTure um in ture)
            {
                i++;
                if (um.ID.ToString() == ViewState["picID"].ToString())
                {
                    break;
                }
            }
            if ((i - 1) >= 0)
                this.tdUp.InnerHtml = "<a href='ShowPic.aspx?picID=" + ture[i - 1].ID.ToString() + "'>" +"上一张"+ ture[i - 1].PicName.ToString() + "</a>";
            if ((i + 1) < ture.Count)
                this.tdDown.InnerHtml = "<a href='ShowPic.aspx?picID=" + ture[i + 1].ID.ToString() + "'>" + "下一张" + ture[i + 1].PicName.ToString() + "</a>";
           
        }

      
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='ShowPic.aspx?picID=" + ViewState["picID"] + "'</script>");
        }
      

        private void Bind()
        {
            List<PicCritique> picList = new List<PicCritique>();
            EGV.DataSource = picList;
            EGV.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            categ.CategFirstPic(new Guid(ViewState["picID"].ToString()), new Guid(ViewState["CategID"].ToString()));
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            turebll.DelPic(new Guid(Request.QueryString["picID"].ToString()));
            Response.Redirect("PicTureList.aspx?CategID=" + ViewState["CategID"]);
        }

        //protected void AspNetPager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        //{

        //    ImageBind();
        //}

        //private void ImageBind()
        //{
        //    PagePagination page = new PagePagination();
        //    page.PageSize = AspNetPager2.PageSize;
        //    page.PageIndex = AspNetPager2.CurrentPageIndex;
        //    Dictionary<string, string> order = new Dictionary<string, string>();
        //    order.Add("PicUpTime", "0");
        //    page.PageOrder = order;
        //    ture = turebll.GetPicTureList(new Guid(ViewState["CategID"].ToString()), page);
        //    img.ImageUrl = ture[0].PicUrl;
        //}
    }
}
