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
using System.Globalization;
using ZoomLa.BLL;

namespace ZoomLa.GatherStrainManage
{
    public partial class FileShareCreat : Page
    {
        #region 业务对象 
        B_User ubll = new B_User();
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

        //GSManageBLL gsbll = new GSManageBLL();


        private int MaxSize = 2097152;
        #endregion

        #region 对象初始化
        protected void Page_Load(object sender, EventArgs e)
        { 
            ubll.CheckIsLogin();
            if (!IsPostBack)
            {
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    GetGS();
                }
            }
        }
        #endregion

        #region 辅助方法
        //群族信息
        private void GetGS()
        {
            //GatherStrain gs = gsbll.GetGatherStrainByID(GSID);
            //imgGSICQ.ImageUrl = PicUrl(gs.GSICO);
            //labGSName.Text = gs.GSName;
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
        private string UpLoadPic(FileShare file)
        {

            if (FileUpload1.PostedFile != null)
            {
                string photoName1 = FileUpload1.PostedFile.FileName; //获取初始文件名
                int i = photoName1.LastIndexOf("."); //取得文件名中最后一个"."的索引
                if (i <= 0)
                {
                    return string.Empty;
                }
                string newext = photoName1.Substring(i); //获取文件扩展名
                if (newext == ".exe")
                {

                    return string.Empty;
                }
                if (FileUpload1.PostedFile.ContentLength > MaxSize)
                {
                    return string.Empty;
                }
                file.FactFileName = FileUpload1.FileName;
                file.FileSize = FileUpload1.PostedFile.ContentLength;
                string photoName2 = Guid.NewGuid().ToString() + newext; //重新为文件命名,时间毫秒部分+文件大小+扩展名
                string path = "~\\user\\userzone\\GatherStrainManage\\File\\" + photoName2;
                file.FileURL = path;
                file.BuildFileName = photoName2;
                FileUpload1.SaveAs(path);
                return path;
            }
            else return string.Empty;
        }
        #endregion

        #region 页面功能
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                //double size = gsbll.GetGSRoomSize(GSID);
                //if (FileUpload1.PostedFile.ContentLength < size)
                //{
                //    FileShare file = new FileShare();
                //    file.GSID = GSID;
                //    file.UserID = ubll.GetLogin().UserID;
                //    file.Mono = this.txtMono.Text;
                //    string ret = UpLoadPic(file);
                //    if (ret == string.Empty)
                //        return;
                //    gsbll.CreatGSFile(file);
                //    Page.Response.Redirect("FileShareManage.aspx?GSID=" + GSID+"&where=5");
                //}
                //else
                //{
                //    Fun.MessBox("群族空间不足", this.Page);
                //}
            }
        }

        protected void btnCal_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("FileShareManage.aspx?GSID=" + GSID + "&where=5");
        }
        #endregion


    }
}
