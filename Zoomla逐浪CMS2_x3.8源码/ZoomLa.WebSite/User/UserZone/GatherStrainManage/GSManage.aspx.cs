using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BDUBLL;
using BDUModel;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using BDULogic;
using System.Globalization;


namespace ZoomLa.GatherStrainManage
{
    public partial class GSManage : Page
    {
        #region 业务对象
        //GSManageBLL gsBll = new GSManageBLL();
        B_User ubll = new B_User();
        #endregion

        #region  对象初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGSList();
                GetIntive();
            }
        }
        #endregion

        #region  辅助方法
        private void GetGSList()
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            //List<GatherStrain> list = GSManageLogic.GetGatherStrainByUserIDAndStr(ubll.GetLogin().UserID,"root<=4");
            //this.gvGS.DataSource = list;
            //this.DataBind();
        }

        private void GetIntive()
        {
            //List<GatherStrain> list = GSManageLogic.GetGatherStrainByUserIDAndStr(ubll.GetLogin().UserID, "root=6");
           // throw new Exception(list.Count.ToString());
            //GV_Intive.DataSource = list;
            //DataBind();
        }

        protected void gvGS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = e.Row.FindControl("Label1") as Label;
                if (this.gvGS.DataKeys[e.Row.RowIndex].Values["UserID"].ToString() == ubll.GetLogin().UserID.ToString())
                {
                    lb.Text = "我是创始人";
                    lb.BackColor = System.Drawing.Color.Red;
                }
                

            }
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            string key = this.TextBox1.Text; 
            //List<GatherStrain> list = gsBll.GetGSLikeName(key, ubll.GetLogin().UserID);
            //this.TextBox1.Text = "";
            //this.gvMu.DataSource = list;
            //this.gvMu.DataBind();
        }
        
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            GridViewRow gr = lb.Parent.Parent as GridViewRow;
            //gsBll.AddUserToGS(ubll.GetLogin().UserID,new Guid(this.gvMu.DataKeys[gr.RowIndex].Value.ToString()),3);
            //GetGSList();
            //string key = this.TextBox1.Text;
            //List<GatherStrain> list = gsBll.GetGSLikeName(key, ubll.GetLogin().UserID);
            //this.gvMu.DataSource = list;
            //this.gvMu.DataBind();
        }
        protected void LBintive_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            GridViewRow gr = lb.Parent.Parent as GridViewRow;
            //GSManageLogic.SetUserRoot(ubll.GetLogin().UserID, new Guid(this.GV_Intive.DataKeys[gr.RowIndex].Value.ToString()), 3);
            GetGSList();
            GetIntive();
        }
        public string PicUrl(string picUrl)
        {
            string pic = picUrl;
            if (!string.IsNullOrEmpty(pic)) { pic = pic.ToLower(); }
            if (!string.IsNullOrEmpty(pic) && (pic.IndexOf(".gif") > -1 || pic.IndexOf(".jpg") > -1 || pic.IndexOf(".png") > -1))
            {
                string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";
                if (pic.StartsWith("~/"))
                {
                    pic.Replace("~/", "/");
                }

                else if (pic.StartsWith("http://", true, CultureInfo.CurrentCulture) || pic.StartsWith("/", true, CultureInfo.CurrentCulture) || pic.StartsWith(delpath, true, CultureInfo.CurrentCulture))
                    pic = "/" + pic;
                else
                {
                    pic = "/" + delpath + "/" + pic;
                }
            }
            else
            {
                pic = "/UploadFiles/nopic.gif";
            }

            return pic;
        }
        #endregion
    }
}
