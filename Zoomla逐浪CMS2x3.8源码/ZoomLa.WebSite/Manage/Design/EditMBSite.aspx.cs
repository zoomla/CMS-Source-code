using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;

public partial class Manage_Design_EditMBSite : CustomerPageAction
{
    B_Design_MBSite msBll = new B_Design_MBSite();
    B_User buser = new B_User();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li><a href='MBSiteList.aspx'>微建站</a></li><li class='active'>微站信息</li>");
        }
    }
    public void MyBind()
    {
        M_Design_MBSite msMod = msBll.SelReturnModel(Mid);
        if (msMod == null) { function.WriteErrMsg("微站不存在!"); }
        SiteName_T.Text = msMod.SiteName;
        SiteImg_UP.FileUrl = msMod.SiteImg;
        TlpName_L.Text = GetTlpName(msMod.TlpID);
        CUser_L.Text = "<a href='javascript:;' onclick='showuser(" + msMod.UserID + ")'>" + buser.SelReturnModel(msMod.UserID).UserName + "</a>";
        CDate_L.Text = msMod.CDate.ToString("yyyy-MM-dd");
        SiteCfg_L.Text = msMod.SiteCfg;
    }
    public string GetTlpName(int tlpid)
    {
        DataTable dt = msBll.GetTlpDT();
        foreach (DataRow dr in dt.Rows)
        {
            if (DataConverter.CLng(dr["ID"]) == tlpid)
            {
                return dr["TlpName"].ToString();
            }
        }
        return "未知模板";
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Design_MBSite msMod = msBll.SelReturnModel(Mid);
        msMod.SiteName = SiteName_T.Text;
        SiteImg_UP.SaveUrl = "/UploadFiles/User/" + buser.SelReturnModel(msMod.UserID).UserName + "/";
        string oldimg = msMod.SiteImg;
        if (SiteImg_UP.HasFile)
        {
            HttpPostedFile file = SiteImg_UP.Request.Files[0];
            var image = System.Drawing.Image.FromStream(file.InputStream);
            SiteImg_UP.SaveFile();
            msMod.SiteImg = SiteImg_UP.FileUrl;
        }
        else
        {
            msMod.SiteImg = SiteImg_UP.FVPath;
        }

        msBll.UpdateByID(msMod);
        function.WriteSuccessMsg("修改成功", "MBSiteList.aspx");
    }
}