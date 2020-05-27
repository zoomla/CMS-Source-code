using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.BLL.Helper;
using System.Data;

namespace ZoomLaCMS.Common
{
    public partial class ContentFile : System.Web.UI.Page
    {
        B_Node nodeBll = new B_Node();
        B_ModelField fieldBll = new B_ModelField();
        ImgHelper imgHelp = new ImgHelper();
        private int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
        private int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        private string FieldName { get { return Request.QueryString["FieldName"] ?? ""; } }
        private int SType { get { return DataConverter.CLng(Request.QueryString["S"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Water=False,MaxPicSize=1024,PicFileExt=jpg|png|gif|bmp|jpeg,SelUpfile=True,Compress=True
            DataTable dt = fieldBll.GetTableByFieldName(FieldName, ModelConfig.GetTableName((ModelConfig.SType)SType), ModelID);
            if (dt.Rows.Count < 1) { RepStr("[" + FieldName + "]字段不存在"); }
            DataRow dr = dt.Rows[0];
            FieldModel fieldMod = new FieldModel(dr["Content"].ToString());
            water_div.Visible = true;
            function.Script(this, "SetRadVal('water_rad'," + fieldMod.GetValInt("Water") + ");");
        }
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            int sizes = 0;
            if (!File_UP.HasFile)
            {
                ReturnManage("请指定一个上传文件");
                return;
            }
            //if (!SafeSC.IsImage(FupFile.FileName)) { function.WriteErrMsg("只能上传图片文件"); }
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(4) + Path.GetExtension(File_UP.FileName);
            string savePath = "";
            if (SafeSC.IsImage(File_UP.FileName))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(File_UP.PostedFile.InputStream);
                if (Request.Form["water_rad"].Equals("1"))
                {
                    img = ImgHelper.AddWater(img);
                }
                if (SiteConfig.ThumbsConfig.ThumbsWidth > 0 || SiteConfig.ThumbsConfig.ThumbsHeight > 0)
                {
                    img = imgHelp.ZoomImg(img, SiteConfig.ThumbsConfig.ThumbsHeight, SiteConfig.ThumbsConfig.ThumbsWidth);
                }
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                savePath = imgHelp.SaveImg(FileSavePath() + fname, bmp);
            }
            else { RepStr("仅允许上传图片文件"); }
            sizes = (int)File_UP.FileContent.Length;
            GetScriptByModuleName(savePath.Replace(SiteConfig.SiteOption.UploadDir, ""), sizes);
            ReturnManage("文件上传成功");
        }
        private string FileSavePath()
        {
            string vpath = "";
            M_Node nodeMod = nodeBll.GetNodeXML(NodeID);
            vpath = SiteConfig.SiteOption.UploadDir + "/" + nodeMod.NodeDir + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            while (vpath.Contains("//")) { vpath = vpath.Replace("//", "/"); }
            return vpath;
        }
        private void GetScriptByModuleName(string thumbnailPath, int size)
        {
            string upload = SiteConfig.SiteOption.UploadDir.TrimEnd('/') + "/" + thumbnailPath;
            function.Script(this, "parent.DealwithUploadPic(\"" + thumbnailPath + "\",\"txt_" + FieldName + "\");parent.DealwithUploadImg(\"" + upload + "\",\"Img_" + FieldName + "\");");
        }
        private void RepStr(string msg) { Response.Clear(); Response.Write(msg); Response.Flush(); Response.End(); }
        private void ReturnManage(string msg)
        {
            LblMessage.Text = msg;
            //if (!string.IsNullOrEmpty(msg))
            //{
            //    function.Script(this, "parent.DealwithUploadErrMessage(\"" + msg + "\");");
            //}
        }
    }
}