using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.OA.Sched
{
    public partial class BCAdd : System.Web.UI.Page
    {
        M_OA_BC bcMod = new M_OA_BC();
        B_OA_BC bcBll = new B_OA_BC();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    bcMod = bcBll.SelReturnModel(DataConverter.CLng(Request.QueryString["ID"]));
                    bcNameT.Text = bcMod.BCName;
                    remindT.Text = bcMod.Remind;
                    beginTimeT.Text = bcMod.StartTime.ToString();
                    endTimeT.Text = bcMod.EndTime.ToString();
                    ColorDefault.Text = bcMod.BackColor;
                }
            }
        }
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            string st = beginTimeT.Text.Trim();
            string et = endTimeT.Text.Trim();
            if (string.IsNullOrEmpty(st) || string.IsNullOrEmpty(et))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('时间不能为空!!!');", true);
            }
            else if (!DataConvert.IsDate(st) || !DataConvert.IsDate(et))
            {
                //只有时间也能正常判断,格式则为2014-04-10 17:00:00.000
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('日期格式不正确!!!');", true);
            }
            else if (Convert.ToDateTime(st) >= Convert.ToDateTime(et))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('起始时间不能大于或等于下次结束时间');", true);
            }
            else
            {
                //正常处理
                bcMod.BCName = bcNameT.Text.Trim();
                bcMod.Remind = remindT.Text.Trim();
                bcMod.StartTime = Convert.ToDateTime(st);
                bcMod.EndTime = Convert.ToDateTime(et);
                bcMod.AddTime = DateTime.Now;
                bcMod.BackColor = ColorDefault.Text.Trim();
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    bcMod.ID = DataConverter.CLng(Request.QueryString["ID"]);
                    bcBll.UpdateByID(bcMod);
                }
                else
                    bcBll.Insert(bcMod);
                Response.Redirect("BCManage.aspx");
            }
        }
    }
}