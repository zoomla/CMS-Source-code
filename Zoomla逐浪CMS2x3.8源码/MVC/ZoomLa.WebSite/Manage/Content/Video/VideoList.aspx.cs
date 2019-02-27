using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.IO;
using System.Data;


namespace ZoomLaCMS.Manage.Content.Video
{
    public partial class VideoList : System.Web.UI.Page
    {
        B_Content_Video videoBll = new B_Content_Video();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../ContentManage.aspx'>内容管理</a></li><li class='active'>视频列表 "
                    + "(<a href='javascript:;' onclick='ShowUpFile()'>添加视频</a>)</li>");
                GetExist();
                MyBind();
            }
        }
        private void MyBind()
        {
            EGV.DataSource = videoBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    M_Content_Video videoMod = videoBll.SelReturnModel(id);
                    videoBll.Del(id);
                    SafeSC.DelFile(videoMod.VPath);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView dr = e.Row.DataItem as DataRowView;
            //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
            //}
        }
        public string GetUserName()
        {
            return buser.SelReturnModel(DataConverter.CLng(Eval("UserID"))).UserName;
        }
        public void GetExist()
        {
            string filepath = Server.MapPath("/Tools/ffmpeg.exe");
            //if (!File.Exists(filepath))
            //{
            //    Response.Redirect("VideoConfig.aspx");
            //}
        }
    }
}