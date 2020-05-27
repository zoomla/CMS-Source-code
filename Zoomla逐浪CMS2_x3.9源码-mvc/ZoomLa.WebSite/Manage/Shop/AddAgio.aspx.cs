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
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class AddAgio : CustomerPageAction
    {
        B_SchemeInfo bsi = new B_SchemeInfo();
        B_Scheme bs = new B_Scheme();
        protected string proName = "";
        protected string type = "添加";
        private int AID
        {
            get
            {
                if (ViewState["AID"] != null)
                    return int.Parse(ViewState["AID"].ToString());
                else
                    return 0;
            }
            set { ViewState["AID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin ba = new B_Admin();
            ba.CheckIsLogin();

            M_SchemeInfo msi = new M_SchemeInfo();
            if (!IsPostBack)
            {
                if (Request.QueryString["AID"] != null)
                {
                    AID = int.Parse(Request.QueryString["AID"].ToString());
                    proName = bs.GetSelect(AID).SName;
                }

                if (Request.QueryString["ID"] != null)
                {
                    type = "修改";
                    msi = bsi.GetSelect(int.Parse(Request.QueryString["ID"].ToString()));
                    txtStartNum.Text = msi.SIULimit.ToString();
                    txtEndNum.Text = msi.SILLimit.ToString();
                    txtAgio.Text = msi.SIAgio.ToString();
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='PresentProject.aspx'>促销方案管理</a></li><li><a href='AgioProject.aspx'>打折方案管理</a></li><li><a href='AgioList.aspx?ID=" + Request.QueryString["AID"] + "'>" + proName + "方案打折信息管理</a></li><li class='active'>添加打折信息</a></li>");
            }
        }
        protected void Submit_B_Click(object sender, EventArgs e)
        {

            M_SchemeInfo msi = new M_SchemeInfo();
            if (Request.QueryString["ID"] != null)
            {
                int ids = int.Parse(Request.QueryString["ID"].ToString());
                msi = bsi.GetSelect(ids);
            }
            msi.SIAgio = int.Parse(txtAgio.Text);
            msi.SIULimit = int.Parse(txtStartNum.Text);
            msi.SILLimit = int.Parse(txtEndNum.Text);
            msi.SID = AID;
            if (Request.QueryString["ID"] != null)
            {
                msi.ID = int.Parse(Request.QueryString["ID"].ToString());
                bsi.GetUpdate(msi);
            }
            else
            {
                msi.SIAddTime = DateTime.Now;
                bsi.GetInsert(msi);
            }
            Response.Write("<script>location.href='AgioList.aspx?ID=" + AID.ToString() + "'</script>");
        }
    }
}