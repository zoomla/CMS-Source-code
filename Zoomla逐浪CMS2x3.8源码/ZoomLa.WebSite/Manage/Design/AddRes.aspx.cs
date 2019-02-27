using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Manage_Design_AddRes : CustomerPageAction
{
    public M_Design_RES resMod = new M_Design_RES();
    B_Design_RES resBll = new B_Design_RES();
    ImgHelper imgHelper = new ImgHelper();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='ResList.aspx'>资源管理</a></li><li class='active'>资源上传</li>");
        }
    }
    public void MyBind()
    {
        if (Mid > 0)
        {
            resMod = resBll.SelReturnModel(Mid);
            Name_T.Text = resMod.Name;
            PreviewImg_T.Text = resMod.PreviewImg;
            if (!string.IsNullOrEmpty(resMod.VPath)) { Res_UP.FileUrl = resMod.VPath; }
            Type_DP.SelectedValue = resMod.ZType;
            Useage_Dp.SelectedValue = resMod.Useage;
            function.Script(this, "SetRadVal('zstatus_rad','" + resMod.ZStatus + "');");
            function.Script(this, "SetChkVal('use_chk','" + resMod.use + "');");
            function.Script(this, "SetChkVal('fun_chk','" + resMod.fun + "');");
            function.Script(this, "SetChkVal('style_chk','" + resMod.style + "');");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        //根据场景和类型来决定存放路径
        Res_UP.SaveUrl = "/UploadFiles/design/res/"+ Type_DP.SelectedValue + "/" + Useage_Dp.SelectedValue + "/";
        string vdir = Res_UP.SaveUrl;
        if (Mid > 0) { resMod = resBll.SelReturnModel(Mid); }
        resMod.Name = Name_T.Text.Replace(" ","");
        resMod.ZStatus = DataConvert.CLng(Request.Form["zstatus_rad"]);
        resMod.ZType = Type_DP.SelectedValue;
        resMod.Useage = Useage_Dp.SelectedValue;
        if (!Res_UP.FVPath.Equals(resMod.VPath, StringComparison.CurrentCultureIgnoreCase))
        {
            if (Res_UP.HasFile)
            {
                resMod.VPath = Res_UP.SaveFile();
            }
            else
            {
                resMod.VPath = Res_UP.FVPath;
            }
        }
        //如果是图片资源,并且未指定缩图,则自动生成,否则使用手输的值
        if (resMod.ZType.Equals("img") && SafeSC.IsImage(resMod.VPath) && string.IsNullOrEmpty(PreviewImg_T.Text))
        {
            int width = 0, height = 0;
            switch (resMod.Useage)
            {
                case "bk_h5":
                    width = 200; height = 300;
                    break;
                case "bk_pc":
                    width = 500; height = 280;
                    break;
            }
            string fname = Path.GetFileNameWithoutExtension(resMod.VPath);
            string ext = Path.GetExtension(resMod.VPath);
            resMod.PreviewImg = imgHelper.SaveImg(vdir + fname + ".thumb" + ext, imgHelper.ZoomImg(resMod.VPath, height, width));
        }
        else { resMod.PreviewImg = PreviewImg_T.Text; }
        resMod.use = Request.Form["use_chk"];
        resMod.fun = Request.Form["fun_chk"];
        resMod.style = Request.Form["style_chk"];
        if (Mid > 0)
        {
            resBll.UpdateByID(resMod);
        }
        else
        {
            resMod.UserID = B_Admin.GetLogin().AdminId;
            resBll.Insert(resMod);
        }
        function.WriteSuccessMsg("操作成功", "ResList.aspx");
    }
}