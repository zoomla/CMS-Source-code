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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Model.AdSystem;
using ZoomLa.BLL.AdSystem;

public partial class User_Info_AdPlan : System.Web.UI.Page
{
    public B_Adbuy B_A = new B_Adbuy();
    public B_User B_U = new B_User();
    public M_Uinfo M_U = new M_Uinfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_U.CheckIsLogin(); 
            this.ViewState["AdName"] = "";
            MyBind();
        }
    }

    private void MyBind()
    { 
        DataTable dt = new DataTable();
        string adname = this.ViewState["AdName"].ToString();
        if (string.IsNullOrEmpty(adname))
        { 
            dt = B_A.SelectAdbuy();          
        }
        else
        {
            dt = B_ADZone.ADZone_ByCondition(" Where ShowTime like '%" + adname + "%'" + " order by ID desc");
        }
        EGV.DataSource = dt;
        EGV.DataBind();       
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Id="";
        switch (e.CommandName) { 
            case "Edit":
                Page.Response.Redirect("ADZone.aspx?ZoneId=" + e.CommandArgument.ToString());
                break;
            case "Del":
                //string Id = e.CommandArgument.ToString();
                //M_Adzone Old = B_ADZone.getAdzoneByZoneId(DataConverter.CLng(Id));
                //string jssource = Old.ZoneJSName;
                //jssource = VirtualPathUtility.AppendTrailingSlash(Request.PhysicalApplicationPath + "/" + SiteConfig.SiteOption.AdvertisementDir) + jssource;
                //if (B_ADZone.ADZone_Remove(Id))
                //{
                //    FileSystemObject.Delete(jssource, FsoMethod.File);
                //    Response.Write("<script>alert('删除成功！')</script>");
                //}
                MyBind();
                break;
            case "AddAdv":
                string id = e.CommandArgument.ToString();
                B_Adbuy B_A = new B_Adbuy();
                M_Adbuy M_A = new M_Adbuy();
                B_ADZone B_AD = new B_ADZone();
                M_Adzone M_AD = new M_Adzone();
                M_A.ID = Convert.ToInt32(id);
                DataTable dt = B_A.SelectByID(M_A);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string Time = dt.Rows[0]["Time"].ToString();
                    string Price = dt.Rows[0]["Price"].ToString();
                    string ADID = dt.Rows[0]["ADID"].ToString();
                    M_AD = B_ADZone.getAdzoneByZoneId(Convert.ToInt32(ADID));
                    string title = M_AD.ZoneName;
                    Page.Response.Redirect("AdSubmit.aspx?ZoneId=" + e.CommandArgument.ToString() + "&Price=" + Price + "&title=" + title);
                }
                break;
            case "Copy":
                Id = e.CommandArgument.ToString();
                int NewID = B_ADZone.ADZone_Copy(DataConverter.CLng(Id));
                if (NewID > 0)
                {
                    M_Adzone mzone = B_ADZone.getAdzoneByZoneId(NewID);
                    string ZoneJSName = mzone.ZoneJSName;
                    ZoneJSName = ZoneJSName.Split(new string[] { "/" }, StringSplitOptions.None)[0].ToString();
                    if (ZoneJSName.Length == 5) { mzone.ZoneJSName = mzone.ZoneJSName.Insert(4, "0"); }
                    B_ADZone.ADZone_Update(mzone);
                    B_ADZone.CreateJS(NewID.ToString());
                    Response.Write("<script>alert('复制成功！" + NewID.ToString() + "')</script>");
                }
                MyBind();
                break;
            case "Clear":
                Id = e.CommandArgument.ToString();
                if (B_ADZone.ADZone_Clear(DataConverter.CLng(Id)))
                {
                    Response.Write("<script>alert('清除成功！')</script>");
                }
                MyBind();
                break;
            case "SetAct":
                Id = e.CommandArgument.ToString();
                if (!B_ADZone.getAdzoneByZoneId(DataConverter.CLng(Id)).Active)
                {
                    B_ADZone.ADZone_Active(DataConverter.CLng(Id));
                }
                else
                {
                    B_ADZone.ADZone_Pause(Id);
                }
                B_ADZone.CreateJS(Id);
                MyBind();
                break;
            case "Refresh":
                B_ADZone.CreateJS(e.CommandArgument.ToString());
                function.WriteSuccessMsg("刷新版位成功!");
                MyBind();
                break;
            case "PreView":
                Page.Response.Redirect("PreviewAD.aspx?ZoneID=" + e.CommandArgument.ToString() + "&Type=Zone");
                break;
            case "JS":
                Page.Response.Redirect("ShowJSCode.aspx?ZoneID=" + e.CommandArgument.ToString());
                break;
            default:
                break;
        
        
        }
         
    }
    public string GetActive(string i)
    {
        string re = DataConverter.CBool(i) ? "活动" : "暂停";
        return re;
    }
    public string GetUnActive(string i)
    {

        string re = !DataConverter.CBool(i) ? "启动" : "暂停";
        return re;
    }
    public static string getzoneshowtypename(string i)
    {
        int index = DataConverter.CLng(i);
        string zoneshowtypename = "";
        switch (index)
        {
            case 1:
                zoneshowtypename = "权重随机显示";
                break;
            case 2:
                zoneshowtypename = "权重优先显示";
                break;
            case 3:
                zoneshowtypename = "顺序循环显示";
                break;
        }
        return zoneshowtypename;

    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string Ids = "";
        for (int i = 0; i <= EGV.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)EGV.Rows[i].FindControl("chkSel");
            if (cbox.Checked == true)
            {
                if (string.IsNullOrEmpty(Ids))
                    Ids = EGV.DataKeys[i].Value.ToString();
                else
                    Ids += "," + EGV.DataKeys[i].Value.ToString();
            }
        }
        if (!string.IsNullOrEmpty(Ids))
            B_ADZone.BatchRemove(Ids);
        MyBind();
    }
    /// <summary>
    /// 批量激活
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnActive_Click(object sender, EventArgs e)
    {
        string Ids = "";
        for (int i = 0; i <= EGV.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)EGV.Rows[i].FindControl("chkSel");
            if (cbox.Checked == true)
            {
                if (string.IsNullOrEmpty(Ids))
                    Ids = EGV.DataKeys[i].Value.ToString();
                else
                    Ids += "," + EGV.DataKeys[i].Value.ToString();
            }
        }
        if (!string.IsNullOrEmpty(Ids))
            B_ADZone.BatchActive(Ids);
        MyBind();
    }
    /// <summary>
    /// 批量暂停
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnPause_Click(object sender, EventArgs e)
    {
        string Ids = "";
        for (int i = 0; i <= EGV.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)EGV.Rows[i].FindControl("chkSel");
            if (cbox.Checked == true)
            {
                if (string.IsNullOrEmpty(Ids))
                    Ids = EGV.DataKeys[i].Value.ToString();
                else
                    Ids += "," + EGV.DataKeys[i].Value.ToString();
            }
        }
        if (!string.IsNullOrEmpty(Ids))
            B_ADZone.BatchPause(Ids);
        MyBind();
    }
    /// <summary>
    /// 批量刷新JS
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnRefurbish_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= EGV.Rows.Count - 1; i++)
        {
            CheckBox cbox = EGV.Rows[i].FindControl("chkSel") as CheckBox;
            if (cbox.Checked == true)
            {
                B_ADZone.CreateJS(EGV.DataKeys[i].Value.ToString());
            }
        }
        function.WriteSuccessMsg("刷新版位成功!");
        MyBind();
    }
    /// <summary>
    /// 分页（每页显示的条数）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        MyBind  ();
    }
    /// <summary>
    /// 鼠标移动变色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    /// <summary>
    /// 单击选择行，双击打开编辑页面
    /// </summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer)
    {
        foreach (GridViewRow row in this.EGV.Rows)
        {
            if (row.RowState == DataControlRowState.Edit)
            { // 编辑状态
                //row.Attributes.Remove("onclick");
                row.Attributes.Remove("ondblclick");
                row.Attributes.Remove("style");
                row.Attributes["title"] = "编辑行";
                continue;
            }
        }
        base.Render(writer);
    }
    public string SetName(string id)
    {
        string appName = B_U.SeachByID(Convert.ToInt32(id)).UserName;
        return appName;
    }

    public string SetZoomName(string id)
    {
        string ZoomName = B_ADZone.getAdzoneByZoneId(Convert.ToInt32(id)).ZoneName;
        return ZoomName;
    }

    public string SetTime(string date)
    {
        string[] dt = date.Split(' ');
        date = dt[0];
        string time = date;
        return time;
    }

    //protected void show(object sender, EventArgs e)
    //{
    //    int id = B_A.SelectAdbuyID().ID;
    //    //foreach (GridViewRow rows in this.GridView1.Rows)
    //    //{
    //    //    if (rows.RowState == DataControlRowState.Selected)
    //    //    {
    //    //  Response.Write("<script>window.location.href='AdPlanAdd.aspx?id=" + id + "';</script>");
    //    Response.Write("<script>alert('" + id + "');</script>");
    //    //    }
    //    //}
    //}
    public int ScaleType(string id)
    {
        int Sales = B_ADZone.getAdzoneByZoneId(Convert.ToInt32(id)).Sales;
        return Sales;
    }
    public int ShowTimeType(string id)
    {
        int ShowTime = B_ADZone.getAdzoneByZoneId(Convert.ToInt32(id)).ShowType;
        return ShowTime;
    }

    public string priceType(string str)
    {
        return string.Format("{0:F2}", Convert.ToDouble(str));
    }
    public string LnkFiles(string files)
    {

        if (files == "")
        {
            return "暂无附件";
        }
        else
        {
            return "<a target='_blank'  href='" + files + "' title='点击下载'>下载</a>";
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}