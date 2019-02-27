using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Manage_Design_EditScene : CustomerPageAction
{
    public int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
    B_Design_Scence scenceBll = new B_Design_Scence();
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        if (Mid < 1) { function.WriteErrMsg("没有指定场景"); }
        M_Design_Page pageMod = scenceBll.SelReturnModel(Mid);
        Title_T.Text = pageMod.Title;
        function.Script(this, "setscore(" + pageMod.Score + ");");
        Status_Rad.SelectedValue = pageMod.Status.ToString();
        CUser_L.Text = "<a href='javascript:;' onclick='showuser(" + pageMod.CUser + ");' title='查看用户' >" + buser.SelReturnModel(pageMod.CUser).UserName + "</a>";
        UserID_Hid.Value = pageMod.UserID.ToString();
        UserName_T.Text = pageMod.UserName;
        CDate_L.Text = pageMod.CDate.ToString("yyyy-MM-dd hh:mm:ss");
        UPDate_L.Text = pageMod.UPDate.ToString("yyyy-MM-dd hh:mm:ss");
        PreviewImg_UP.FileUrl = pageMod.PreviewImg;
        ThumbImg_UP.FileUrl = pageMod.ThumbImg;
        Meta_T.Text = pageMod.Meta;
        Remind_T.Text = pageMod.Remind;
        function.Script(this, "SetChkVal('seflag_chk','" + pageMod.Seflag + "');");
        string looklink = "/h5/" + pageMod.ID;
        Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li><a href='SceneList.aspx'>场景列表</a></li><li class='active'>场景管理</a></li><div class='pull-right hidden-xs'><a href='" + looklink + "' target='_blank' title='预览场景'><i class='fa fa-eye'></i></a></div>");
    }

    protected void Submit_B_Click(object sender, EventArgs e)
    {
        M_Design_Page pageMod = scenceBll.SelReturnModel(Mid);
        pageMod.Title = Title_T.Text;
        pageMod.Score = DataConvert.CDouble(Request.Form["score_num"]);
        pageMod.Status = DataConvert.CLng(Status_Rad.SelectedValue);
        if (ThumbImg_UP.HasFile)
        {
            ThumbImg_UP.SaveFile();
            pageMod.ThumbImg = ThumbImg_UP.FileUrl;
        }
        else //未指定文件,则以文本框中的为准
        {
            pageMod.ThumbImg = ThumbImg_UP.FVPath;
        }
        if (PreviewImg_UP.HasFile)
        {
            HttpPostedFile file = PreviewImg_UP.Request.Files[0];
            var image = System.Drawing.Image.FromStream(file.InputStream);
            PreviewImg_UP.SaveFile();
            pageMod.PreviewImg = PreviewImg_UP.FileUrl;
        }
        else
        {
            pageMod.PreviewImg = PreviewImg_UP.FVPath;
        }
        pageMod.UserID = DataConvert.CLng(UserID_Hid.Value.Trim(' '));
        pageMod.UserName = buser.SelReturnModel(pageMod.UserID).UserName;
        pageMod.Seflag = Request.Form["seflag_chk"];
        pageMod.Meta = Meta_T.Text;
        pageMod.Remind = Remind_T.Text;
        scenceBll.UpdateByID(pageMod);
        function.WriteSuccessMsg("操作成功！", "SceneList.aspx");
    }

    protected void Del_B_Click(object sender, EventArgs e)
    {
        //if (scenceBll.Del(Mid)) { function.WriteSuccessMsg("删除成功!","SceneList.aspx"); }
        M_Design_Page pageMod = scenceBll.SelReturnModel(Mid);
        pageMod.Status = -1;
        scenceBll.UpdateByID(pageMod);
        function.WriteSuccessMsg("删除成功!", "SceneList.aspx");
    }

    protected void UpdateToTlp_B_Click(object sender, EventArgs e)
    {
        M_Design_Page pageMod = new M_Design_Page();
        M_Design_Page tlppageMod = scenceBll.SelReturnModel(Mid);
        M_Design_Tlp tlpMod = new M_Design_Tlp();
        tlpMod.TlpName = tlppageMod.Title;
        tlpMod.ZType = 1;
        tlpMod.ZStatus = 0;
        tlpMod.ClassID = 0;
        tlpMod.PreviewImg = tlppageMod.PreviewImg;
        tlpMod.ID = tlpBll.Insert(tlpMod);

        pageMod.Title = tlppageMod.Title;
        pageMod.TlpID = tlpMod.ID;
        pageMod.page = tlppageMod.page;
        pageMod.comp = tlppageMod.comp;
        pageMod.scence = tlppageMod.scence;
        pageMod.Score = tlppageMod.Score;
        pageMod.ThumbImg = tlppageMod.ThumbImg;
        pageMod.PreviewImg = tlppageMod.PreviewImg;
        scenceBll.Insert(pageMod);
        function.WriteSuccessMsg("操作成功", "TlpList.aspx?type=1");
    }
}