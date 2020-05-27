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
using ZoomLa.Components;

public partial class manage_Zone_AddSchool : CustomerPageAction
{
    B_School sll = new B_School();
    B_Admin badmin = new B_Admin();
    public int SchID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (SchID>0)
            {
                M_School schoolinfo = sll.GetSelect(SchID);
                txtID.Value = schoolinfo.ID.ToString();
                txtSchoolName.Text = schoolinfo.SchoolName;
                txtSchoolType.Text = schoolinfo.SchoolType.ToString();
                txtVisage.Text = schoolinfo.Visage.ToString();
                SchoolIcon_T.Text = schoolinfo.Country;
                txtSchoolInfo.Text = schoolinfo.SchoolInfo;
                pro_hid.Value = schoolinfo.Province.Trim();
                Button1.Text = "修改信息";
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='SnsSchool.aspx'>学校信息配置</a></li><li class='active'>添加学校</li>");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_School sinfo = new M_School();
        sinfo.SchoolName = txtSchoolName.Text.Trim();
        sinfo.SchoolType = DataConverter.CLng(txtSchoolType.Text);
       // sinfo.Country = txtCountry.SelectedValue.Trim();
        sinfo.Addtime = DateTime.Now;
        sinfo.Province = Request.Form["province_dp"];
        sinfo.City = Request.Form["city_dp"];
        sinfo.County = Request.Form["county_dp"];
        sinfo.Visage = DataConverter.CLng(txtVisage.SelectedValue);
        M_AdminInfo admininfo = badmin.GetAdminLogin();
        string saveurl = SiteConfig.SiteOption.UploadDir+"Exam/"+ admininfo.AdminName+ admininfo.AdminId+"/";
        sinfo.Country = SchoolIcon_T.Text;
        sinfo.SchoolInfo = txtSchoolInfo.Text.Trim();
        if (SchID>0)
        {
            sinfo.ID = SchID;
            sll.GetUpdate(sinfo);
        }
        int schid= sll.GetInsert(sinfo);
        function.WriteSuccessMsg("操作成功!", "SchoolShow.aspx?id=" + schid);
    } 
}
