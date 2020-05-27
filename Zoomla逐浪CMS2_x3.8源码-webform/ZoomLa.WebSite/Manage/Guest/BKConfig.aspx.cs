using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class Manage_Guest_BKConfig : System.Web.UI.Page
{
    B_Group groupBll = new B_Group();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "BaikeManage");
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx" + "'>工作台</a></li><li><a href='BKCheck.aspx'>百科管理</a></li><li class='active'>百科配置</li>");
        }
    }
    public void MyBind()
    {
        DataTable dt = groupBll.Select_All();
        selGroup_Rpt.DataSource = dt;
        selGroup_Rpt.DataBind();
        CreateGroup_Rpt.DataSource = dt;
        CreateGroup_Rpt.DataBind();
        EditGroup_Rpt.DataSource = dt;
        EditGroup_Rpt.DataBind();
        PointType_R.SelectedValue = GuestConfig.GuestOption.BKOption.PointType;
        CreatePoint_T.Text = GuestConfig.GuestOption.BKOption.CreatePoint.ToString();
        EditPoint_T.Text = GuestConfig.GuestOption.BKOption.EditPoint.ToString();
        RemmPoint_T.Text = GuestConfig.GuestOption.BKOption.RemmPoint.ToString();
    }
    //获取选中状态
    public string GetCheck(int type)
    {
        string ids = "";
        switch (type)
        {
            case 1:
                ids = "," + GuestConfig.GuestOption.BKOption.selGroup + ",";
                break;
            case 2:
                ids = "," + GuestConfig.GuestOption.BKOption.CreateBKGroup + ",";
                break;
            case 3:
                ids = "," + GuestConfig.GuestOption.BKOption.EditGroup + ",";
                break;
            default:
                break;
        }
        return ids.Contains("," + Eval("GroupID").ToString() + ",") ? "checked=checked" : "";
    }
    protected void Save_B_Click(object sender, EventArgs e)
    {
        GuestConfigInfo guestinfo = GuestConfig.GuestOption;
        guestinfo.BKOption.selGroup = Request.Form["selGroup"];
        guestinfo.BKOption.CreateBKGroup = Request.Form["CreateGroup"];
        guestinfo.BKOption.EditGroup = Request.Form["EditGroup"];
        guestinfo.BKOption.PointType = PointType_R.SelectedValue;
        guestinfo.BKOption.CreatePoint = DataConverter.CLng(CreatePoint_T.Text);
        guestinfo.BKOption.EditPoint = DataConverter.CLng(EditPoint_T.Text);
        guestinfo.BKOption.RemmPoint = DataConverter.CLng(RemmPoint_T.Text);
        GuestConfig config = new GuestConfig();
        config.Update(guestinfo);
        function.WriteSuccessMsg("保存百科配置成功!");
    }
}