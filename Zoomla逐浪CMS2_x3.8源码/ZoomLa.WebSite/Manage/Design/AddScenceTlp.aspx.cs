using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Manage_Design_AddScenceTlp : CustomerPageAction
{
    B_Design_Scence pageBll = new B_Design_Scence();
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_Design_TlpClass classBll = new B_Design_TlpClass();
    public int ZType = 1;
    protected int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            NodeTree.DataSource = classBll.Sel();
            NodeTree.MyBind();
            PreView_UP.SaveUrl = "/UploadFiles/Design/Tlp/";
            PreView_UP.IsRelName = false;
            string looklink = Mid > 0 ? "<div class='pull-right hidden-xs'><a href='/design/h5/preview.aspx?tlpid=" + Mid + "' target='_blank' title='预览场景'><i class='fa fa-eye'></i></a></div>" : "";
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='TlpList.aspx?type=1'>模板列表</a></li><li class='active'>模板管理</li>" + looklink);
        }
    }
    private void MyBind()
    {
        if (Mid > 0)
        {
            M_Design_Tlp tlpMod = tlpBll.SelReturnModel(Mid);
            if (tlpBll == null) { function.WriteErrMsg("模板不存在"); }
            TlpName_T.Text = tlpMod.TlpName;
            Price_T.Text = tlpMod.Price.ToString("f2");
            Remind_T.Text = tlpMod.Remind;
            if (!string.IsNullOrEmpty(tlpMod.PreviewImg)) { PreView_UP.FileUrl = tlpMod.PreviewImg; }
            if (tlpMod.ClassID > 0) { Node_Hid.Value = tlpMod.ClassID.ToString(); }
            function.Script(this, "SetRadVal('zstatus_rad','" + tlpMod.ZStatus + "');");
            score_tr.Visible = true;
            function.Script(this, "setscore(" + tlpMod.Score + ");");
            CDate_L.Text = tlpMod.CDate.ToString("yyyy-MM-dd hh:mm:ss");
            //--------
            M_Design_Page pageMod = pageBll.SelModelByTlp(tlpMod.ID);
            edit_a.HRef = "/design/h5/default.aspx?Source=tlp&id=" + pageMod.guid;
            edit_a.Visible = true;
            function.Script(this, "SetChkVal('defby_chk','" + tlpMod.DefBy + "');");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Design_Tlp tlpMod = new M_Design_Tlp();
        if (Mid > 0) { tlpMod = tlpBll.SelReturnModel(Mid); }
        string oldimg = tlpMod.PreviewImg;
        tlpMod.TlpName = TlpName_T.Text;
        tlpMod.Price = DataConvert.CDouble(Price_T.Text);
        tlpMod.Remind = Remind_T.Text;
        tlpMod.ClassID = DataConvert.CLng(Node_Hid.Value);
        tlpMod.ZStatus = DataConvert.CLng(Request.Form["zstatus_rad"]);
        tlpMod.Score = DataConvert.CDouble(Request.Form["score_num"]);
        tlpMod.ZType = ZType;
        tlpMod.DefBy = Request.Form["defby_chk"];
        if (!PreView_UP.FVPath.Equals(tlpMod.PreviewImg, StringComparison.CurrentCultureIgnoreCase))
        {
            if (PreView_UP.HasFile) { tlpMod.PreviewImg = PreView_UP.SaveFile(); }
            else { tlpMod.PreviewImg = PreView_UP.FVPath; }
        }
        if (Mid > 0)
        {
            if (!tlpMod.PreviewImg.Equals(oldimg))
            {
                M_Design_Page pageMod = pageBll.SelModelByTlp(tlpMod.ID);
                pageMod.PreviewImg = tlpMod.PreviewImg;
                pageBll.UpdateByID(pageMod);
            }
            tlpBll.UpdateByID(tlpMod);
        }
        else
        {
            tlpMod.ID = tlpBll.Insert(tlpMod);
            //添加一个新的场景
            M_Design_Page pageMod = new M_Design_Page();
            pageMod.TlpID = tlpMod.ID;
            pageMod.ZType = 1;
            pageMod.PreviewImg = tlpMod.PreviewImg;
            pageBll.Insert(pageMod);
        }
        function.WriteSuccessMsg("操作成功", "TlpList.aspx?type=" + ZType);
    }
}