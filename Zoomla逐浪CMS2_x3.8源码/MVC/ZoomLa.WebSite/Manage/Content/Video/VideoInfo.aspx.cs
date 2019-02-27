using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Content.Video
{
    public partial class VideoInfo : System.Web.UI.Page
    {
        B_Content_Video videoBll = new B_Content_Video();
        B_User buser = new B_User();
        public int CID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='../ContentManage.aspx'>内容管理</a></li><li class='active'>视频列表 "
                    + "(<a href='javascript:;'>视频信息</a>)</li>");
                MyBind();
            }
        }
        public void MyBind()
        {
            M_Content_Video model = videoBll.SelReturnModel(CID);
            ID_L.Text = model.ID.ToString();
            VName_T.Text = model.VName;
            VPath_T.Text = model.VPath;
            VSize_T.Text = FileSystemObject.GetFileSizeByPath(model.VPath);
            VTime_T.Text = model.VTime;
            UserName_L.Text = buser.SelReturnModel(model.UserID).UserName;
            Desc_T.Text = model.Desc;
            Thum_Img.Src = model.Thumbnail;
            Thum_Hid.Value = model.Thumbnail;

        }
        protected void SaveInfo_B_Click(object sender, EventArgs e)
        {
            M_Content_Video model = videoBll.SelReturnModel(CID);
            model.VName = VName_T.Text;
            model.VPath = VPath_T.Text;
            model.VSize = VSize_T.Text;
            model.VTime = VTime_T.Text;
            model.Desc = Desc_T.Text;
            model.Thumbnail = Thum_Hid.Value;
            videoBll.UpdateByID(model);
            Response.Redirect("VideoList.aspx");
        }
    }
}