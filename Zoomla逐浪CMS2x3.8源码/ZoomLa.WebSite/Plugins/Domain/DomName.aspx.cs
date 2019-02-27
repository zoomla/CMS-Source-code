using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Plugins_Domain_DomName : System.Web.UI.Page
{
    protected IISHelper iisHelper = new IISHelper();
    protected B_User buser = new B_User();
    protected B_IDC_DomainTemp domTempBll = new B_IDC_DomainTemp();
    protected B_IDC_DomainPrice domPriceBll = new B_IDC_DomainPrice();
    protected B_IDC_DomainList domListBll = new B_IDC_DomainList();

    protected string clientID, apiPasswd;
    protected void Page_Load(object sender, EventArgs e)
    {
        EGV.txtFunc = txtPageFunc;
        clientID = StationGroup.newNetClientID;
        apiPasswd = StationGroup.newNetApiPasswd;
        if (!IsPostBack)
        {
            DataBindDP();
            DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setDefaultCheck('" + StationGroup.DefaultCheck + "');", true);
        }
    }
    private void DataBindDP()
    {
        //prvinceDP.DataSource = city.readProvince();//省份
        //prvinceDP.DataTextField = "Name";
        //prvinceDP.DataValueField = "Name";
        //prvinceDP.DataBind();
        tempListDP.DataSource = domTempBll.SelByUserID(buser.GetLogin().UserID);//模板
        tempListDP.DataValueField = "ID";
        tempListDP.DataTextField = "TempName";
        tempListDP.DataBind();
        tempListDP.Items.Insert(0, new ListItem("新建模板", "0"));
    }
    private void DataBind(string key = "")
    {
        int userID = buser.GetLogin().UserID;
        if (userID == 0) return;
        DataTable dt = domListBll.SelWithUserByID(userID);
        if (!string.IsNullOrEmpty(key))
        {
            dt.DefaultView.RowFilter = "Domain like '%" + key + "%'";
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //----EGV
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
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        EGV.EditIndex = -1;
        EGV.Columns[4].Visible = false;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Edit2":
                EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                EGV.Columns[4].Visible = true;
                break;
            case "Renewals"://确定续费
                EGV.EditIndex = -1;
                Update(Convert.ToInt32(e.CommandArgument as string));
                EGV.Columns[4].Visible = false;
                break;
            default: break;
        }
        DataBind();
    }
    private void Update(int rowNum)
    {
        GridViewRow gr = EGV.Rows[rowNum];
        string url = ((Label)gr.FindControl("lDomain")).Text.Trim();
        string period = Request.Form["periodDP"];
        Renewals(url, period);
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(keyWord.Text.Trim());
    }
    //续费业务方法
    private string Renewals(string url, string period)
    {
        // begindate    续费起始日期 yyyy-mm-dd,即当前到期时间
        string begindate = DomNameHelper.GetEndDate(url, "yyyy-MM-dd");
        string checksum = DomNameHelper.MD5("DomainRenew" + clientID + apiPasswd + url + "E" + begindate, 32);//以32位
        List<QueryParam> param = new List<QueryParam>();
        param.Add(new QueryParam("checksum", checksum));
        param.Add(new QueryParam("dn", url));//域名
        param.Add(new QueryParam("enc", "E"));
        param.Add(new QueryParam("client", clientID));
        param.Add(new QueryParam("begindate", begindate));
        param.Add(new QueryParam("period", period));//续费多久，以年为单位
        DomNameHelper _XinNet = new DomNameHelper(ApiType.domain, param);
        return _XinNet.Result;
    }
    //添加模板
    protected void addTempBtn_Click(object sender, EventArgs e)
    {
        string tName = Request.Form["tempName"];
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
        param.Add(new QueryParam("ucity1", prvinceDP.SelectedValue + Request.Form["cityText"]));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
        param.Add(new QueryParam("ucity2", Request.Form["ucity2"]));
        param.Add(new QueryParam("uaddr1", Request.Form["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
        param.Add(new QueryParam("uaddr2", Request.Form["uaddr2"]));
        param.Add(new QueryParam("uzip", Request.Form["uzip"]));//注册人邮政编码                    [必须]
        param.Add(new QueryParam("uteln", Request.Form["uteln"]));//注册人电话号码
        param.Add(new QueryParam("ateln", Request.Form["ateln"]));
        //param.Add(new QueryParam("ufaxa", Request.Form["ufaxa"]));   //传真区号
        //param.Add(new QueryParam("ufaxn", Request.Form["ufaxn"]));//不能超过8位,与API的不能超过12位不同

        for (int i = 0; i < param.Count; i++)
        {
            value += param[i].QueryName + ":" + param[i].QueryValue + ",";
        }
        value += "prvinceDP:" + prvinceDP.SelectedValue + ",";
        value += "cityText:" + Request.Form["cityText"];
        if (tempListDP.SelectedIndex < 1)
        {
            domTempBll.Insert(tName, value, buser.GetLogin().UserID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');", true);
        }
        else
        {
            domTempBll.UpdateByID(tName, value, tempListDP.SelectedValue);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');", true);
        }
        DataBindDP();
    }
    protected void tempListDP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tempListDP.SelectedIndex > 0)
        {
            DataTable dt = domTempBll.SelByID(tempListDP.SelectedValue);
            if (dt.Rows.Count < 1) return;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + dt.Rows[0]["TempValue"] + "');", true);//key:value,key2:value2
        }
    }
    //充值
    protected void beginCharge_Click(object sender, EventArgs e)
    {
        string money = chargeText.Text.Trim();
        Response.Redirect("~/PayOnline/OrderPay.aspx?Money=" + money);
    }
}