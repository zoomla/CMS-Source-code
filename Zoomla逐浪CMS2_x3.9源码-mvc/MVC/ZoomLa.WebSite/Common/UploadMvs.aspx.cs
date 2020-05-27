using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.PdoApi.TencentMVS;

namespace ZoomLaCMS.Common
{
    public partial class UploadMvs : System.Web.UI.Page
    {
        C_TencentMVS mvsBll = new C_TencentMVS();

        //远程文件路径
        private string remotePath { get { return Request.QueryString["remotePath"]; } }
        private string name { get { return Request.QueryString["name"]; } }
        //空间名称
        private string bucketName = "systemcover";
        public bool IsUpadte { get { return ViewState["isupdate"] as string == "1"; } set { ViewState["isupdate"] = value ? "1" : ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            if (!string.IsNullOrEmpty(remotePath))
            {
                string result = mvsBll.GetFileStat(bucketName, remotePath);
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                if (IsSuccess(obj))
                {
                    IsUpadte = true;
                    Title_T.Text = obj["data"]["video_title"].ToString();
                    Cover_T.Text = obj["data"]["video_cover"].ToString();
                    Remind_T.Text = obj["data"]["video_desc"].ToString();
                }
            }
        }

        protected void Up_B_Click(object sender, EventArgs e)
        {
            if (!Video_UP.HasFile)
            {
                if (IsUpadte)//修改视频属性信息
                {
                    if (string.IsNullOrEmpty(Cover_T.Text)) { function.WriteErrMsg("必须指定视频封面URL!"); }
                    string res = mvsBll.UpdateFile(bucketName, remotePath, "", Cover_T.Text, Title_T.Text, Remind_T.Text);
                    JObject o = JsonConvert.DeserializeObject<JObject>(res);
                    if (IsSuccess(o))
                    {
                        function.Script(this, "alert('修改成功!');CloseDiag();");
                    }
                    else
                    {
                        Error_L.Text = "修改失败，错误代码:{code:" + o["code"] + ",message:" + o["message"] + "}";
                    }
                }
                else { Error_L.Text = "*请选择要上传的文件"; return; }
            }
            else
            {
                Video_UP.SaveUrl = SiteConfig.SiteOption.UploadDir + "Video/";
                Video_UP.FVPath = Video_UP.SaveFile();
                //------------------------
                var vpath = function.VToP(Video_UP.FVPath);//本地视频文件路径
                var fileName = Video_UP.FName;//远程文件路径
                var title = Title_T.Text;//视频标题
                var videoCover = Cover_T.Text;//图片封面路径（因图片不能上传，只能填写网络图片地址）
                var desc = Remind_T.Text;//视频描述
                function.Script(this, "beginup();");
                //分片上传，文件大小无限制
                var result = mvsBll.SliceUploadFile(bucketName, fileName, vpath, videoCover, "", title, desc);
                function.Script(this, "endup();");
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                if (IsSuccess(obj))
                {
                    string access_url = obj["data"]["access_url"].ToString();
                    function.Script(this, "alert('上传成功!');UploadMvs('" + name + "','" + fileName + "','" + access_url + "');");
                }
                else
                {
                    Error_L.Text = "上传失败，错误代码:{code:" + obj["code"] + ",message:" + obj["message"] + "}";
                }
            }
            //----------------------------
        }
        private bool IsSuccess(JObject obj)
        {
            return obj["code"] != null && DataConverter.CLng(obj["code"]) == 0;
        }
    }
}