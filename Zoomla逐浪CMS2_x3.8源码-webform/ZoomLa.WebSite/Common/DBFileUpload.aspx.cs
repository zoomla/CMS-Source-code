using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.BLL.Helper;

public partial class Common_DBFileUpload : System.Web.UI.Page
{
    B_ModelField fieldBll = new B_ModelField();
    public int ModelID { get { return DataConverter.CLng(Request.QueryString["modelid"]); } }
    public string FieldName { get { return Request.QueryString["fieldname"] ?? ""; } }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        long size = FupFile.FileContent.Length;
        if (size > 10 * 1024 * 1024) { function.Script(this, "alert('文件不能大于10mb');");return; }
        M_ModelField fieldmod = fieldBll.GetModelByFieldName(ModelID, FieldName);
        if (fieldmod == null) { return; }
        if (SafeSC.FileNameCheck(FupFile.FileName)){ function.Script(this, "alert('此文件格式不允许上传!');"); return; }
        if (fieldmod.FieldType.Equals("SqlType") && !SafeSC.IsImage(FupFile.FileName)) { function.Script(this, "alert('只能上传图片!');"); return; }
        string content = fieldmod.Content;
        Stream filedata = FupFile.FileContent;
        byte[] filebyte = new byte[filedata.Length];
        filedata.Read(filebyte, 0, filebyte.Length);
        function.Script(this, "parent.DealwithUploadPic('"+ FupFile.FileName+"|"+ Convert.ToBase64String(filebyte) + "','FIT_" + FieldName+"');");
        function.Script(this, "parent.DealwithUploadPic('" + FupFile.FileName+"','txt_" + FieldName+"');");
        LblMessage.Text = "文件上传成功";
    }
}