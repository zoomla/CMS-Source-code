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
using BDUModel;
using BDUBLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using System.Globalization;
using System.Data.SqlClient;

namespace ZoomLa.GatherStrainManage
{
    public partial class HuaTeeRevert : Page
    {
        public Guid GSID
        {
            get
            {
                if (ViewState["GSID"] != null)
                {
                    return new Guid(ViewState["GSID"].ToString());
                }
                else return Guid.Empty;
            }
            set
            {
                ViewState["GSID"] = value;
            }
        }
        public Guid HTID
        {
            get
            {
                if (ViewState["HTID"] != null)
                {
                    return new Guid(ViewState["HTID"].ToString());
                }
                else return Guid.Empty;
            }
            set
            {
                ViewState["HTID"] = value;
            }
        }
        //GSManageBLL gsbll = new GSManageBLL();
        B_User ubll = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ubll.CheckIsLogin(); 
            string id = Request.QueryString["ID"];
            string gid=Request.QueryString["Gid"];
            if (Request.QueryString["menu"] == "edit")
            {
                Page.Response.Redirect("AppearHuaTee.aspx?GSID=" + gid.ToString() + "&HTID=" + id.ToString());
            }
            if (Request.QueryString["menu"] == "delete")
            {
                string ids=id.ToString();
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ids", ids) };
                Sql.Del("ZL_Sns_GSReverCricicism", "HuaTeePicID=@ids",sp);
                Sql.Del("ZL_Sns_GSHuatee", "ID=@ids",sp);
                Page.Response.Redirect("CreatHuaTee.aspx?GSID=" + gid.ToString() );
            }
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            if (!IsPostBack)
            {
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    //群族信息
                    GetGS();
                    //设置权限
                    JudgeRoot();
                    if (Page.Request.QueryString["HTID"] != null)
                    {
                        ViewState["HTID"] = Page.Request.QueryString["HTID"];
                        //读取话题
                        GetHT();
                        //读取评论
                        GetRC();
                    }

                }
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            GSReverCricicism gsrc = new GSReverCricicism();
            gsrc.Content = this.FreeTextBox1.Value.Replace("\n","<br/>");
            gsrc.HuaTeePicID = HTID;
            gsrc.UserID = ubll.GetLogin().UserID;
            gsrc.Flage = 0;
            //gsrc.Taxis = gsbll.GetReverCricicismMaxTaxis(HTID, 0) + 1;
            //gsbll.CreatReverCricicism(gsrc, 0);
            Response.Write("<script>location.href='HuaTeeRevert.aspx?GSID="+ViewState["GSID"].ToString()+"&HTID="+ViewState["HTID"].ToString()+"'</script>");
        }
        //删除回复
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
        }
        //删除话题
        protected void lbtnDeleteHT_Click(object sender, EventArgs e)
        {
        }
        //编辑话题 
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
        }
        #region 辅助方法
        //群族信息
        private void GetGS()
        {
            //GatherStrain gs = gsbll.GetGatherStrainByID(GSID);
            //imgGSICQ.ImageUrl = PicUrl(gs.GSICO);
            //labGSName.Text = gs.GSName;
            //ViewState["UserID"] = gs.UserID;
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
        //读取话题
        private void GetHT()
        {
            List<GSHuatee> list = new List<GSHuatee>();
            //GSHuatee gsHT = gsbll.GetGSHuateeByID(HTID);
            //if (gsHT != null)
            //{
            //    this.Label1.Text = gsHT.HuaTeeTitle;
            //    list.Add(gsHT);
            //    this.gvHuaTess.DataSource = list;
            //    this.gvHuaTess.DataBind();
            //}
        }
        //读取评论
        private void GetRC()
        {
            //List<GSReverCricicism> list = gsbll.GetGSReverCricicismList(HTID, 0);
            //GetUserFriend(list);

        }
        private void GetUserFriend(List<GSReverCricicism> list)
        {
            gvRes.DataSource = list;
            gvRes.DataBind();
        }
        
        protected bool GetVisible(string id, string userid)
        {
            bool str = false;
            if (ubll.GetLogin().UserID == int.Parse(userid))
            {
                str = true;
            }
            else
            {
                if (ViewState["UserID"] != null)
                {
                    if (ubll.GetLogin().UserID == int.Parse(ViewState["UserID"].ToString()))
                    {
                        str = true;
                    }
                }
                else
                {
                    str = false;
                }
            }
            return str;
        }
        protected void JudgeRoot()
        {
        }
        #endregion
    }
}
