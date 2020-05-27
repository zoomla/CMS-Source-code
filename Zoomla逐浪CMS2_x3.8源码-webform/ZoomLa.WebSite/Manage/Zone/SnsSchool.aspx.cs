using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;
using ZoomLa.SQLDAL;

public partial class manage_Zone_SnsSchool : CustomerPageAction
{
    B_School sll = new B_School();
    M_School schMod = new M_School();
    protected void Page_Load(object sender, EventArgs e)
    {
        RPT.SPage = SelPage;
        if (!IsPostBack)
        {
            RPT.DataBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Exam/Papers_System_Manage.aspx'>教育模块</a></li><li class='active'>学校管理<a href='AddSchool.aspx'>[添加学校]</a></li>");
        }
    }
    public DataTable SelPage(int pageSize, int cpage)
    {
        string wherestr = "1=1";
        string provin = "", city = "", county = "";
        if (!string.IsNullOrEmpty(address_hid.Value))
        {
            provin = address_hid.Value.Split(',')[0];
            city = address_hid.Value.Split(',')[1];
            county = address_hid.Value.Split(',')[2];
        }
        if (!NoProvince_Check.Checked) { wherestr += " AND Province=@province"; }
        if (!NoCity_Check.Checked) { wherestr += " AND city=@city"; }
        if (!NoCounty_Check.Checked) { wherestr += " AND county=@county"; }
        if (!string.IsNullOrEmpty(Search_T.Text)) { wherestr += " AND SchoolName LIKE @search"; }
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("province", provin), new SqlParameter("city", city), new SqlParameter("county", county),new SqlParameter("search","%"+Search_T.Text+"%") };
        PageSetting setting = new PageSetting()
        {
            fields = "*",
            psize = pageSize,
            cpage = cpage,
            where = wherestr,
            sp = sp,
            t1 = "ZL_School",
        };
        DataTable dt = DBCenter.SelPage(setting);
        RPT.ItemCount = setting.itemCount;
        return dt;
    }
    protected string GetSchoolType(string txtSchooltype)
    {
        return schMod.GetScoolType(DataConverter.CLng(txtSchooltype));
    }
    protected string GetVisage(string txtVisage)
    {
        int Visage = DataConverter.CLng(txtVisage);
        string Visagename = "";
        switch (Visage)//1-公办 2-私立
        { 
            case 1:
                Visagename = "公办";
                break;
            case 2:
                Visagename = "私立";
                break;
        }
        return Visagename;
    }
    protected void DelBtn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            sll.DelByIDS(Request.Form["idchk"]);
        }
    }
    public string GetIcon(string icon)
    {
        icon = string.IsNullOrEmpty(icon) ? "/UploadFiles/nopic.gif" : icon;
        if (icon.Contains("/")||string.IsNullOrEmpty(icon))//判断是否为路径
        { return "<a href='" + icon + "' target='_bank'><img src='" + icon + "' onerror=\"this.src='/UploadFiles/nopic.gif'\" style='width:30px; height:30px;' /></a>"; }
        return "<span class='" + icon + "'></span>";
    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del1":
                sll.GetDelete(Convert.ToInt32(e.CommandArgument.ToString()));
                function.WriteSuccessMsg("删除成功!", "SnsSchool.aspx");
                break;
            default:
                break;
        }
        RPT.DataBind();
    }

    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        RPT.DataBind();
    }
}
