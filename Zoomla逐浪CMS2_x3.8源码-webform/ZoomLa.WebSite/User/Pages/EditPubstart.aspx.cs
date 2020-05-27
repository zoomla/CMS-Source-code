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

public partial class User_Pages_EditPubstart : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_Model bmodel = new B_Model();
    private B_Pub bpub = new B_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            

            string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"].ToString()) ? "0" : Request.QueryString["Pubid"].ToString();
            if (DataConverter.CLng(Pubid) <= 0)
                function.WriteErrMsg("缺少用户ID参数！");
            int ModelID = DataConverter.CLng(bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString());
            string menu = string.IsNullOrEmpty(Request.QueryString["menu"]) ? null : Request.QueryString["menu"].ToString();
            //    int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
            int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);

            if (menu == "false")
            {
                if (buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, ID, 12))
                {

                    if (!string.IsNullOrEmpty(menu))
                    {
                        Response.Redirect("ViewPub.aspx?Pubid=" + Pubid );
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('审核成功!');location.href='ViewPub.aspx?Pubid=" + Pubid +"';</script>");

                    }


                }
                else
                {
                    if (!string.IsNullOrEmpty(menu))
                    {
                        Response.Redirect("ViewPub.aspx?Pubid=" + Pubid);
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('审核失败!');location.href='ViewPub.aspx?Pubid=" + Pubid + "';</script>");
                    }
                }
            }
            else
            {
                if (buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, ID, 13))
                {

                    if (!string.IsNullOrEmpty(menu))
                    {
                        Response.Redirect("ViewPub.aspx?Pubid=" + Pubid);
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('取消审核!');location.href='ViewPub.aspx?Pubid=" + Pubid + "';</script>");

                    }


                }
                else
                {
                    if (!string.IsNullOrEmpty(menu))
                    {
                        Response.Redirect("ViewPub.aspx?Pubid=" + Pubid);
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('取消审核失败!');location.href='ViewPub.aspx?id=" + ModelID + "';</script>");

                    }
                }
            }
        }
    }
}
