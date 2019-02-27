using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Design
{
    public partial class AddTlp : CustomerPageAction
    {
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_Design_TlpClass classBll = new B_Design_TlpClass();
        private int ZType = 0;
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                NodeTree.DataSource = classBll.Sel();
                NodeTree.MyBind();
                PreView_UP.SaveUrl = "/UploadFiles/Design/SETlp/";
                PreView_UP.IsRelName = false;
                string looklink = Mid > 0 ? "<div class='pull-right hidden-xs'><a href='/design/preview.aspx?tlpid=" + Mid + "' target='_blank' title='预览场景'><i class='fa fa-eye'></i></a></div>" : "";
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='TlpList.aspx'>模板列表</a></li><li class='active'>模板管理</li>" + looklink);
            }
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
                    tlpBll.P_Del(id);
                    break;
            }
            MyBind();
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
                EGV.DataSource = tlpBll.SelByTlpID(Mid);
                Init_Btn.Visible = true;
                if (tlpMod.ClassID > 0) { Node_Hid.Value = tlpMod.ClassID.ToString(); }
                function.Script(this, "SetRadVal('zstatus_rad','" + tlpMod.ZStatus + "');");
                score_tr.Visible = true;
                function.Script(this, "setscore(" + tlpMod.Score + ");");
            }
            EGV.DataBind();
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Design_Tlp tlpMod = new M_Design_Tlp();
            if (Mid > 0) { tlpMod = tlpBll.SelReturnModel(Mid); }
            tlpMod.TlpName = TlpName_T.Text;
            tlpMod.Price = DataConvert.CDouble(Price_T.Text);
            tlpMod.Remind = Remind_T.Text;
            tlpMod.ClassID = DataConvert.CLng(Node_Hid.Value);
            tlpMod.ZStatus = DataConvert.CLng(Request.Form["zstatus_rad"]);
            tlpMod.Score = DataConvert.CDouble(Request.Form["score_num"]);
            if (!PreView_UP.FVPath.Equals(tlpMod.PreviewImg, StringComparison.CurrentCultureIgnoreCase))
            {
                if (PreView_UP.HasFile) { tlpMod.PreviewImg = PreView_UP.SaveFile(); }
                else { tlpMod.PreviewImg = PreView_UP.FVPath; }
            }
            if (Mid > 0) { tlpBll.UpdateByID(tlpMod); }
            else { tlpBll.AddNewTlp(tlpMod); }
            function.WriteSuccessMsg("操作成功", "TlpList.aspx?type=" + ZType);
        }
        protected void Init_Btn_Click(object sender, EventArgs e)
        {
            //清空数据,如不存在则新建
            DataTable dt = DBCenter.Sel("ZL_Design_Page", "TlpID=" + Mid + " AND SiteID=0 AND Path IN('/index','/content','/list','/nav')");
            foreach (DataRow dr in dt.Rows)
            {
                DBCenter.UpdateSQL("ZL_Design_Page", "page='',comp=''", "[ID]=" + dr["ID"]);
            }
            M_Design_Page pageMod = new M_Design_Page();
            pageMod.TlpID = Mid;
            pageMod.ZType = (int)M_Design_Page.PageEnum.Page;
            pageMod.TbName = "ZL_Design_Page";
            if (dt.Select("Path='/index'").Length < 1)
            {
                pageMod.guid = Guid.NewGuid().ToString();
                pageMod.Title = "首页";
                pageMod.Path = "/index";
                DBCenter.Insert(pageMod);
            }
            pageMod.ZType = (int)M_Design_Page.PageEnum.Page;
            if (dt.Select("Path='/list'").Length < 1)
            {
                pageMod.guid = Guid.NewGuid().ToString();
                pageMod.Title = "列表页";
                pageMod.Path = "/list";
                DBCenter.Insert(pageMod);
            }
            pageMod.ZType = (int)M_Design_Page.PageEnum.Page;
            if (dt.Select("Path='/content'").Length < 1)
            {
                pageMod.guid = Guid.NewGuid().ToString();
                pageMod.Title = "内容页";
                pageMod.Path = "/content";
                DBCenter.Insert(pageMod);
            }
            pageMod.ZType = (int)M_Design_Page.PageEnum.Page;
            if (dt.Select("Path='/nav'").Length < 1)
            {
                pageMod.guid = Guid.NewGuid().ToString();
                pageMod.Title = "首页";
                pageMod.Path = "/nav";
                pageMod.ZType = (int)M_Design_Page.PageEnum.Global;
                DBCenter.Insert(pageMod);
            }
            function.WriteSuccessMsg("模板初始化成功");
        }
    }
}