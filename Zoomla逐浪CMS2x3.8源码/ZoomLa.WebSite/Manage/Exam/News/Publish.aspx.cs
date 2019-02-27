using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;

public partial class Manage_I_Exam_Publish : System.Web.UI.Page
{
    string vpath = "";
    M_Content_Publish pubMod = new M_Content_Publish();
    B_Content_Publish pubBll = new B_Content_Publish();
    private int Pid 
    {
        get { return Convert.ToInt32(Request.QueryString["Pid"]); }
    }
    private int MyID { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            string result = "";
            switch (action)
            {
                case "GetModel":
                    int id = Convert.ToInt32(value);
                    pubMod = pubBll.SelReturnModel(id);
                    result = pubMod.Json + "$$$" + pubMod.ImgPath+"$$$"+pubMod.ID+"$$$"+pubMod.Title;
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        vpath = "/UploadFiles/Admin/Publish/" + Pid + "/";
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        string span = "";
        pubMod = pubBll.SelReturnModel(Pid);
        if (MyID > 0)
        {
            M_Content_Publish curMod = pubBll.SelReturnModel(MyID);
            span = "<span class='curspan'>当前：" + pubMod.NewsName + "_" + curMod.Title + "</span>";
            AttachFile_T.Text = Path.GetFileName(curMod.AttachFile);
            SaveAll_Btn.Text = "修改";
            function.Script(this, "SetTime('" + curMod.ImgPath + "','" + curMod.ID + "','" + curMod.Title + "','" + curMod.Json + "','" + curMod.CDate + "')");
        }
        else
        {
            span = "<span class='curspan'>当前：" + pubMod.NewsName + "</span>";
            Time_t.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        Call.SetBreadCrumb(Master, "<li><a href='News.aspx'>数字出版</a></li><li><a href='NewsDiv.aspx?Pid=" + pubMod.ID + "'>版面管理</a></li><li><a href='" + Request.RawUrl + "'>版块管理</a>" + span + "</li>");
    }
    protected void ImgPath_Btn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(imgpath_fp.FileName) || imgpath_fp.FileContent.Length < 100) { function.Script(this, "alert('未选择图片文件');"); }
        else
        {
            //SafeSC.SaveFile(vpath, imgpath_fp);
            if (!imgpath_fp.SaveAs(vpath + Path.GetFileName(imgpath_fp.FileName))) { function.WriteErrMsg(imgpath_fp.ErrorMsg); }
            ImgPath_Hid.Value = vpath + imgpath_fp.FileName;
            function.Script(this, "SetImg('" + ImgPath_Hid.Value + "');");
        }
    }
    protected void SaveAll_Btn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(CurEditID_Hid.Value))
        {
            pubBll.Insert(FillMod());
            function.WriteSuccessMsg("添加成功", "NewsDiv.aspx?Pid=" + Pid);
        }
        else
        {
            pubBll.UpdateByID(FillMod(Convert.ToInt32(CurEditID_Hid.Value)));
            function.WriteSuccessMsg("修改成功", "NewsDiv.aspx?Pid="+Pid);
        }
    }
    public M_Content_Publish FillMod(int id = 0)
    {
        M_Content_Publish model = new M_Content_Publish();
        if (id > 0)
            model = pubBll.SelReturnModel(id);
        model.Title = NewsTitle_T.Text.Trim();
        model.ImgPath = ImgPath_Hid.Value;
        model.Json = Json_Hid.Value;
        model.Pid = Pid;
        model.CDate = DataConverter.CDate(Time_t.Text);
        if (AttachFile_File.FileContent.Length > 0)
        {
            string fpath = vpath + Path.GetFileName(AttachFile_File.FileName);
            if (!AttachFile_File.SaveAs(fpath)) { function.WriteErrMsg(AttachFile_File.ErrorMsg); }
            model.AttachFile = fpath;
            //model.AttachFile = SafeSC.SaveFile(vpath, AttachFile_File.PostedFile);
        }
        return model;
    }
    
}