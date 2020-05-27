using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Site
{
    public partial class DomTempMange : System.Web.UI.Page
    {
        protected B_IDC_DomainTemp domTempBll = new B_IDC_DomainTemp();
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            badmin.CheckIsLogin();
            EGV.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                DataBind();
                DataBindDP();
            }
            Call.HideBread(Master);
        }
        private void DataBind(string key = "")
        {
            EGV.DataSource = domTempBll.Sel();
            EGV.DataBind();
        }
        private void DataBindDP()
        {
            //prvinceDP.DataSource = city.readProvince();//省份
            //prvinceDP.DataTextField = "Name";
            //prvinceDP.DataValueField = "Name";
            //prvinceDP.DataBind();
            tempListDP.DataSource = domTempBll.Sel();//模板
            tempListDP.DataValueField = "ID";
            tempListDP.DataTextField = "TempName";
            tempListDP.DataBind();
            tempListDP.Items.Insert(0, new ListItem("新建模板", "0"));
        }
        //添加模板
        protected void addTempBtn_Click(object sender, EventArgs e)
        {
            string tName = Request.Form["tempName"];
            string value = GetValueFromPage();
            if (tempListDP.SelectedIndex < 1)
            {
                domTempBll.Insert(tName, value, badmin.GetAdminLogin().AdminId);
            }
            else
            {
                domTempBll.UpdateByID(tName, value, tempListDP.SelectedValue);
            }
            if (tempListDP.SelectedIndex < 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');", true);
            }
            DataBindDP();
        }

        private string GetValueFromPage()
        {
            string value = "";
            List<QueryParam> param = new List<QueryParam>();
            param.Add(new QueryParam("tempName", Request.Form["tempName"]));
            param.Add(new QueryParam("uname1", Request.Form["uname1"]));//注册人|单位名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uname2", Request.Form["uname2"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("rname1", Request.Form["rname1"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("rname2", Request.Form["rname2"]));
            param.Add(new QueryParam("aname1", Request.Form["rname1"]));//管理联系人 中|英名称   [国内域名必填]|[国际域名必须],与上方用同一信息
            param.Add(new QueryParam("aname2", Request.Form["rname2"]));
            param.Add(new QueryParam("aemail", Request.Form["aemail"]));//管理联系人电子邮件地址                [必须]
            param.Add(new QueryParam("ucity1", Request.Form["ucity1"]));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("ucity2", Request.Form["ucity2"]));
            param.Add(new QueryParam("uaddr1", Request.Form["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uaddr2", Request.Form["uaddr2"]));
            param.Add(new QueryParam("uzip", Request.Form["uzip"]));//注册人邮政编码                    [必须]
            param.Add(new QueryParam("uteln", Request.Form["uteln"]));//注册人电话号码
            param.Add(new QueryParam("ateln", Request.Form["ateln"]));
            param.Add(new QueryParam("ufaxa", Request.Form["ufaxa"]));   //传真区号
            param.Add(new QueryParam("ufaxn", Request.Form["ufaxn"]));//不能超过8位,与API的不能超过12位不同
            for (int i = 0; i < param.Count; i++)
            {
                value += param[i].QueryName + ":" + param[i].QueryValue + ",";
            }
            value += "prvinceDP:" + prvinceDP.SelectedValue + ",";
            value += "cityText:" + Request.Form["cityText"];
            return value;
        }
        protected void tempListDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "Showtemplatediv();", true);
            if (tempListDP.SelectedIndex > 0)
            {
                DataTable dt = domTempBll.SelByID(tempListDP.SelectedValue);
                if (dt.Rows.Count < 1)
                    return;
                this.addTempBtn.Text = "修改模板";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + dt.Rows[0]["TempValue"] + "');", true);//key:value,key2:value2
            }
            else
            {
                this.addTempBtn.Text = "添加模板";
            }
        }

        public void txtPageFunc(string size)
        {

            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void mimeEGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit2":
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "Showtemplatediv();", true);
                    this.tempListDP.SelectedValue = e.CommandArgument.ToString();
                    this.addTempBtn.Text = "修改模板";
                    DataTable dt = domTempBll.SelByID(tempListDP.SelectedValue);
                    if (dt.Rows.Count < 1)
                        return;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + dt.Rows[0]["TempValue"] + "');", true);//key:value,key2:value2
                    break;
                case "Del2":
                    string id = e.CommandArgument.ToString();
                    this.domTempBll.DelByID(id);
                    Response.Redirect("DomTempMange.aspx");
                    break;
                default:
                    break;
            }
        }
    }
}