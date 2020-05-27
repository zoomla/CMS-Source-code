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

public partial class User_Pages_Delpub : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_Model bmodel = new B_Model();
    private B_Pub bpub = new B_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            buser.CheckIsLogin();
            string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"].ToString()) ? "0" : Request.QueryString["Pubid"].ToString();
            if (DataConverter.CLng(Pubid) <= 0)
                function.WriteErrMsg("缺少用户ID参数！");
            int ModelID = DataConverter.CLng(bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString());
            string dd = string.IsNullOrEmpty(Request.QueryString["small"]) ? null : Request.QueryString["small"].ToString();
            //int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
            int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);

            if (buser.DelModelInfo(bmodel.GetModelById(ModelID).TableName, ID))
            {

                if (!string.IsNullOrEmpty(dd))
                {
                    Response.Redirect("ViewSmallPub.aspx?Pubid=" + Pubid + "&ID=" + dd);
                }
                else
                {
                    Response.Write("<script language=javascript>alert('删除成功!');location.href='ViewPub.aspx?Pubid=" + Pubid + "';</script>");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dd))
                {
                    Response.Redirect("ViewSmallPub.aspx?ModelID=" + ModelID + "&ID=" + dd);
                }
                else
                {
                    Response.Write("<script language=javascript>alert('删除失败!');location.href='ViewPub.aspx?id=" + ModelID + "';</script>");
                }
            }
        }
    }
}
