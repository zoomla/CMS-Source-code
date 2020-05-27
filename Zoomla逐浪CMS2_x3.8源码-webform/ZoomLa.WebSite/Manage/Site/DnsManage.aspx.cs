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

public partial class Manage_Site_DnsManager : System.Web.UI.Page
{
    //IP与域名可自动识别，加上正则判断吧
    protected M_IDC_DNSTable dnsModel = new M_IDC_DNSTable();
    protected B_IDC_DNSTable dnsBll = new B_IDC_DNSTable();
    protected B_Admin badmin = new B_Admin();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li class='active'>DNS管理</li>");
           DataBind();
        }
    }
    private void DataBind(string key="") 
    {
        DataTable dt=new DataTable();
        if (string.IsNullOrEmpty(key))
            dt = dnsBll.SelAll();
        else
            dt = dnsBll.SelAll(key);
        EGV.DataSource = dt;
        EGV.DataBind();

    }
    //处理页码
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
    public void WreiteToTxt(string txtPath, string v)
    {
        FileStream fs = File.Open(txtPath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(v);
        sw.Close();
    }
    public string ReadFromTxt(string txtPath)
    {
        StreamReader sr = File.OpenText(txtPath);
        string result = "";
        try
        {
            result = sr.ReadToEnd();
        }
        catch { }
        finally { sr.Close(); }
        return result;
    }
    //输出txt
    protected void outputBtn_Click(object sender, EventArgs e)
    {
        DataTable dt = dnsBll.SelAll();
        //string s = "";
        if (!Directory.Exists(StationGroup.DnsOutputPath))
            Directory.CreateDirectory(StationGroup.DnsOutputPath);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    s = StationGroup.DNSTemplate;
        //    s = s.Replace("{dom/}", dt.Rows[i]["Domain"].ToString());
        //    s = s.Replace("{domIP/}", dt.Rows[i]["IP"].ToString());
        //    s = s.Replace("{maildom/}", dt.Rows[i]["MX"].ToString());
        //    s = s.Replace("{dns/}", "dns.hx008.com");
        //    string txtPath = @StationGroup.DnsOutputPath + dt.Rows[i]["Domain"].ToString() + ".txt";
        //    WreiteToTxt(txtPath, s);
        //}
        remindDiv.Visible = true;
        remindDiv.InnerText = " 输出完成,目录:" + StationGroup.DnsOutputPath;
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text.Trim());
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2":
                dnsBll.DeleteByID(e.CommandArgument.ToString());
                DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(),"","alert('操作成功');",true);
                break;
        }
    }
}


