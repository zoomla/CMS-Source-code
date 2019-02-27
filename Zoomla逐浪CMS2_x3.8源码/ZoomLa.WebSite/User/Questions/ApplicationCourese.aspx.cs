using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

public partial class User_Questions_ApplicationCourese : System.Web.UI.Page
{
    protected B_Questions_Class cll = new B_Questions_Class();
    protected B_User ull = new B_User();
    protected B_Course bcourse = new B_Course();
    protected B_ExClassgroup ell = new B_ExClassgroup();

    protected B_UserCourse buserCourse = new B_UserCourse();


    protected void Page_Load(object sender, EventArgs e)
    {
        ull.CheckIsLogin();
        if (!IsPostBack)
        {
            RepNodeBind();
            //LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
        }
    }

    /// <summary>
    /// 数据邦定
    /// </summary>
    private void RepNodeBind()
    {
        int cid = DataConverter.CLng(Request.QueryString["c_id"]);
        //用户审核通过课程
        DataTable qus = bcourse.Select_classidNotInCouresId(cid);
        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = qus.DefaultView;
        if (qus != null && qus.Rows.Count > 0)
        {
            this.nocontent.Style["display"] = "none";
            this.EGV.Visible = true;
        }
        else
        {
            this.nocontent.Style["display"] = "";
            this.EGV.Visible = false;

        }
        if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
        {
            pds.PageSize = Convert.ToInt32(Request.QueryString["txtPage"]);
        }
        if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
        {
            pds.PageSize = Convert.ToInt32(Request.Form["txtPage"]);
        }

        pds.AllowPaging = true;
        EGV.DataSource = pds;
        this.EGV.DataBind();
    }



    public string getStr(int cid)
    {
        string result = "";
        System.Data.DataTable dt = ell.Select_CourseID(cid);
        if (dt != null && dt.Rows.Count > 0)
        {
            ZoomLa.Model.M_UserCourse muc = buserCourse.Select_ClassID(ZoomLa.Common.DataConverter.CLng(dt.Rows[0]["GroupID"]), cid, ull.GetLogin().UserID);
            if (muc != null && muc.ID > 0)
            {


                B_Payment bPayment = new B_Payment();
                M_Payment mPayment = bPayment.SelModelByPayNo(muc.OrderID);
                if (mPayment.Status == 3)
                {
                    result = " <font style='color:#999999'>已付款<br>(等待审核)</font> ";
                }
                else
                {
                    result = " <a href=\"CreateCourse.aspx?CourseID=" + cid + "&ucid=" + muc.ID + "\" >已申请</a><font style='color:#999999'><br>(等待汇款)</font> ";
                }

            }
            else
            {

                result = "<a href=\"CreateCourse.aspx?CourseID=" + cid + "\">申请上课</a>";
            }
        }
        else
        {
            result = " <font style='color:#999999'>暂未开班</font> ";
        }
        return result;
    }
    public string GetClass(string id)
    {
        DataTable dt = ell.Select_CourseID(DataConverter.CLng(id));
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0]["CourseHour"].ToString();
        }
        else
        {
            return "0";
        }
    }


    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        RepNodeBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        this.RepNodeBind();
    }
}

