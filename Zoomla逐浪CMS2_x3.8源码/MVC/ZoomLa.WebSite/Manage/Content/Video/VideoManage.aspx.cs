using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Content.Video
{
    public partial class VideoManage : System.Web.UI.Page
    {
        //Text="/test_video/cut_ps0001.wmv"
        M_Content_Video videoMod = new M_Content_Video();
        B_Content_Video videoBll = new B_Content_Video();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        private const string workdir = "/UploadFiles/Video/Conver/";
        VideoHelper conver = new VideoHelper(function.VToP("/Tools/ffmpeg.exe"), workdir);
        private string Source { get { return Source_T.Text.Replace(" ", ""); } set { Source_T.Text = value; } }
        private string SourceDir { get { return Path.GetDirectoryName(Source).Replace("\\", "/") + "/"; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Directory.Exists(function.VToP(workdir)))
                {
                    Directory.CreateDirectory(function.VToP(workdir));
                }
                if (!File.Exists(Server.MapPath("/Tools/ffmpeg.exe")))
                {
                    function.WriteErrMsg("你当前还未下载视频解码器,请<a href='http://www.z01.com/Tools/ffmpeg.exe' target='_blank'>点击下载</a>,并放至/Tools/目录下");
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='../ContentManage.aspx'>内容管理</a></li><li><a href='VideoList.aspx'>视频列表</a></li><li class='active'><a href='" + Request.RawUrl + "'>视频处理</a></li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                videoMod = videoBll.SelReturnModel(Mid);
                Source = videoMod.VPath;
            }
            ImgWater_T.Text = WaterModuleConfig.WaterConfig.imgLogo;
            ImgRPT.DataSource = GetWaterImgDT();
            ImgRPT.DataBind();
        }
        protected void Conver_Btn_Click(object sender, EventArgs e)
        {
            string fpath = Server.MapPath(Source_T.Text);
            OutputPackage model = conver.ConvertToFLV(Source);
            videoMod = videoBll.SelReturnModel(Mid);
            videoMod.VName = Path.GetFileName(model.VPath);
            videoMod.VPath = model.VPath;
            videoBll.UpdateByID(videoMod);
            Source = model.VPath;
            function.WriteSuccessMsg("转换完成!");
        }
        protected void CutImg_Btn_Click(object sender, EventArgs e)//当前视频名.jpg
        {
            string imgvpath = SourceDir + DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(4) + ".jpg";
            conver.CutImgFromVideo(Source, imgvpath, Convert.ToInt32(CutImgSec_T.Text));
            PreView_Img.Visible = true;
            PreView_L.Text = imgvpath;
            PreView_Img.ImageUrl = imgvpath;
        }
        protected void Remove_Btn_Click(object sender, EventArgs e)
        {
            string fname = "cut_" + Path.GetFileName(Source);
            conver.CutFromVideo(Source, SourceDir + fname, Convert.ToInt32(RemoveSecond_T.Text), 0);
            function.WriteErrMsg("移除完成");
        }
        protected void Merge_Btn_Click(object sender, EventArgs e)
        {
            string fname = "all_" + Path.GetFileName(Source);
            conver.ComBineVideo(SourceDir + fname, Source, Merge_T.Text);
            function.WriteErrMsg("合并完成");
        }
        protected void Water_Btn_Click(object sender, EventArgs e)
        {
            //先将图片拷至目标文件夹,再开始
            string fname = "wat_" + Path.GetFileName(Source);
            string ppath = conver.WaterMark(Source, SourceDir + fname, ImgWater_T.Text, 2);
            Source = function.PToV(ppath);
            videoMod = videoBll.SelReturnModel(Mid);
            videoMod.VPath = Source;
            videoBll.UpdateByID(videoMod);
            function.WriteSuccessMsg("水印添加完成", "", 0);
        }
        public DataTable GetWaterImgDT()
        {
            List<JObject> list = JsonConvert.DeserializeObject<List<JObject>>(WaterModuleConfig.WaterConfig.WaterImgs);
            DataTable dt = FileSystemObject.GetDTFormat2();
            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = Path.GetFileName(list[i]["url"].ToString());
                dr["Path"] = list[i]["url"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}