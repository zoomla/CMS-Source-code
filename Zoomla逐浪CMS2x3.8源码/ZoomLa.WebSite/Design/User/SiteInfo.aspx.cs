using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

public partial class Design_User_SiteInfo : System.Web.UI.Page
{
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    B_IDC_DomainList domBll = new B_IDC_DomainList();
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_Design_SiteInfo sfMod = sfBll.SelReturnModel(Mid);
        if (sfMod == null) { function.WriteErrMsg("站点不存在", "/Design/User/"); }
        SiteName_T.Text = sfMod.SiteName;
        M_IDC_DomainList domMod = domBll.SelReturnModel(sfMod.DomainID);
        if (domMod != null)
        {
            Domain_L.Text = "http://" + domMod.DomName;
        }
        Logo_UP.FileUrl = sfMod.Logo;
        //if (!string.IsNullOrEmpty(sfMod.Logo))
        //{
        //    Logo_Img.ImageUrl = sfMod.Logo;
        //    Logo_Img.Visible = true;
        //}
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Design_SiteInfo sfMod = sfBll.SelReturnModel(Mid);
        sfMod.SiteName = SiteName_T.Text;
        Logo_UP.SaveUrl = sfMod.SiteDir + "/UploadFiles/";
        if (!Logo_UP.FVPath.Equals(sfMod.Logo, StringComparison.CurrentCultureIgnoreCase))
        {
            if (Logo_UP.HasFile) { sfMod.Logo = Logo_UP.SaveFile(); }
            else { sfMod.Logo = Logo_UP.FVPath; }
        }
        //if (Logo_UP.FileContent.Length > 100 && SafeSC.IsImage(Logo_UP.FileName))
        //{
        //    sfMod.Logo = SafeSC.SaveFile(sfMod.SiteDir + "/UploadFiles/", Logo_UP);
        //}
        sfBll.UpdateByID(sfMod);
        function.WriteSuccessMsg("修改成功");
    }
}