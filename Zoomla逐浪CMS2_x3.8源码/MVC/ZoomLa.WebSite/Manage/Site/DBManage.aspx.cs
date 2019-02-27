using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Site
{
    public partial class DBManage :CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected B_User buser = new B_User();
        protected B_IDC_DBList dbBll = new B_IDC_DBList();
        protected M_IDC_DBList dbMod = new M_IDC_DBList();
        protected IISHelper iisHelper = new IISHelper();
        protected DBModHelper dbHelper = new DBModHelper(StationGroup.DBManagerName, StationGroup.DBManagerPasswd);
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            IdentityAnalogue ia = new IdentityAnalogue();
            ia.CheckEnableSA();
            if (!IsPostBack)
            {
                SiteDPBind();
                if (string.IsNullOrEmpty(Request.QueryString["ID"]))//添加与浏览
                {
                    DataBind();
                    dbPwdT.Text = function.GeneratePasswd();
                }
                else
                {
                    dbMod = dbBll.SelReturnModel(Request.QueryString["ID"]);
                    addBtn_Click(null, null);
                    dbNameT.Text = dbMod.DBName;
                    dbUserT.Text = dbMod.DBUser;
                    dbPwdT.Text = dbMod.DBInitPwd;
                    bindUserT.Text = dbMod.UserName;
                    bindUserD.Value = dbMod.UserID.ToString();
                    bindDomT.Text = dbMod.Remind;
                    bindSiteDP.SelectedValue = dbMod.SiteID.ToString();
                    saveBtn.Text = "修改";
                }
            }
            Call.HideBread(Master);
        }
        private void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(key))
                dt = dbBll.SelAll();
            else
                dt = dbBll.SelByKeyWord(key);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        private void SiteDPBind()
        {
            bindSiteDP.DataSource = iisHelper.GetWebSiteList();
            bindSiteDP.DataValueField = "SiteID";
            bindSiteDP.DataTextField = "SiteName";
            bindSiteDP.DataBind();
            bindSiteDP.Items.Insert(0, new ListItem("请选择站点", "-1"));
        }
        //处理页码
        public void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;
            size = pageSize.ToString();
            DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    //删除记录，同时删除目标数据库
                    string id = e.CommandArgument.ToString();
                    dbMod = dbBll.SelReturnModel(id);
                    if (string.IsNullOrEmpty(dbMod.DBName)) { dbBll.DelByID(id); DataBind(); }
                    else if (dbHelper.DelDB(dbMod.DBName)) { dbHelper.DelUserByName(dbMod.DBUser); dbBll.DelByID(id); DataBind(); }
                    else { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('移除失败,请检查数据库权限!!!');", true); }
                    break;
            }
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBind(searchText.Text.Trim());
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            dbListDiv.Visible = false;
            addDiv.Visible = true;
        }
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                dbMod = dbBll.SelReturnModel(Request.QueryString["ID"]);
            }
            dbUserT.Text = dbUserT.Text.Trim();
            dbPwdT.Text = dbPwdT.Text.Trim();
            dbNameT.Text = dbNameT.Text.Trim();
            dbMod.DBName = dbNameT.Text;
            dbMod.DBUser = dbUserT.Text;
            dbMod.DBInitPwd = dbPwdT.Text;
            dbMod.Remind = bindDomT.Text.Trim();
            dbMod.Status = 1;
            if (bindSiteDP.SelectedIndex > 0)
            {
                dbMod.SiteID = DataConverter.CLng(bindSiteDP.SelectedValue);
                dbMod.SiteName = bindSiteDP.SelectedItem.Text;
            }
            else
            {
                dbMod.SiteID = 0;
                dbMod.SiteName = "尚未绑定";
            }
            if (!string.IsNullOrEmpty(bindUserD.Value))
            {
                dbMod.UserID = DataConverter.CLng(bindUserD.Value);
                dbMod.UserName = buser.GetUserByUserID(dbMod.UserID).UserName;
            }
            dbMod.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(Request.QueryString["ID"]))//添加
            {
                try
                {
                    dbHelper.CreateDatabase(dbNameT.Text);
                    dbHelper.CreateDatabaseUser(dbUserT.Text, dbPwdT.Text);
                    dbHelper.CreateUserMap(dbNameT.Text, dbUserT.Text);
                    dbBll.Insert(dbMod);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');location=location;", true);
                }
                catch (Exception ex)
                {
                    remindSpan.Visible = true;
                    remindSpan.InnerText = "提示:" + ex.Message;
                }
            }
            else
            {
                dbBll.UpdateModel(dbMod);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');location=location;", true);
            }
        }
        protected void cPwdBtn_Click(object sender, EventArgs e)
        {
            dbPwdT.Text = function.GeneratePasswd();
        }
    }
}