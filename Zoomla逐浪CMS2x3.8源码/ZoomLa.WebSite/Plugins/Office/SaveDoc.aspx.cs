using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class test_SaveDoc : System.Web.UI.Page
{
    B_OA_Document oaBll = new B_OA_Document();
    OACommon oacom = new OACommon();
    B_User buser = new B_User();
    string[] docarr = "doc,docx,xls,xlsx,rtf".Split(',');
    int AppID { get { return DataConverter.CLng(Request.QueryString["AppID"]); } }//AppID
    public string FName { get { return HttpUtility.UrlDecode(Request.QueryString["fname"]??""); } }//仅传文件名
    //稍后改为直接存入字段中
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Files.Count > 0)
        {
            HttpPostedFile file = Request.Files[0];
            if (AppID > 0)
            {
                M_OA_Document oaMod = oaBll.SelReturnModel(AppID);
                M_UserInfo mu = buser.SelReturnModel(oaMod.UserID);
                SafeSC.SaveFile(oacom.GetMyDir(mu), file, FName);
            }
            else//第一次创建
            {
                M_UserInfo mu = buser.GetLogin();
                SafeSC.SaveFile(oacom.GetMyDir(mu), file, FName);
            }
            Response.Write("true");
        }
        else
        {
            Response.Write("No File Upload!");
        }
        Response.End();
    }
}