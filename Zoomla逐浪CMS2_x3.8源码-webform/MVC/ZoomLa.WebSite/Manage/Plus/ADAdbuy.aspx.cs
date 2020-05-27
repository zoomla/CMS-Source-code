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
using ZoomLa.Model;
using System.IO;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Model.AdSystem;
using ZoomLa.BLL.AdSystem;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class ADAdbuy : CustomerPageAction
    {
        public B_Adbuy B_A = new B_Adbuy();
        public M_Adbuy M_A = new M_Adbuy();
        public B_User B_U = new B_User();
        public M_Uinfo M_U = new M_Uinfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.ViewState["AdName"] = "";
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Plus/ADManage.aspx'>广告管理</a></li><li class='active'><a href='ADAdbuy.aspx'>广告申请</a></li>" + Call.GetHelp(31));
        }

        public void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                int type = DataConverter.CLng(Request.QueryString["type"]);
                dt = B_ADZone.ADZone_ByCondition(" Where ShowTime=" + type + " order by ID desc");
                Egv.DataKeyNames = new string[] { "ZoneID" };
            }
            else
            {
                string adname = this.ViewState["AdName"].ToString();
                if (string.IsNullOrEmpty(adname))
                {
                    dt = B_A.SelectAdbuy();
                    Egv.DataKeyNames = new string[] { "ID" };
                }
                else
                {
                    dt = B_ADZone.ADZone_ByCondition(" Where ShowTime like @adname order by ID desc", new SqlParameter[] { new SqlParameter("adname", "%" + adname + "%") });
                    Egv.DataKeyNames = new string[] { "ID" };
                }
            }
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        // 批量删除
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_Adbuy.Adbuy_Delete(chkArr[i]);
                }
            }
            DataBind();
        }
        // 单击选择行，双击打开编辑页面
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in this.Egv.Rows)
            {
                if (row.RowState == DataControlRowState.Edit)
                {
                    row.Attributes.Remove("ondblclick");
                    row.Attributes.Remove("style");
                    row.Attributes["title"] = "编辑行";
                    continue;
                }
            }
            base.Render(writer);
        }
        public string SetName(string id)
        {
            string appName = B_U.SeachByID(Convert.ToInt32(id)).UserName;
            return appName;
        }
        public string SetZoomName(string id)
        {
            string ZoomName = B_ADZone.getAdzoneByZoneId(Convert.ToInt32(id)).ZoneName;
            return ZoomName;
        }
        public new string ContentType(string id)
        {
            string Content = B_A.SelectId(Convert.ToInt32(id)).Content;
            return Content;
        }
        public int SalesType(string id)
        {
            int Sales = B_A.SelectId(Convert.ToInt32(id)).Scale;
            return Sales;
        }
        public string GetAudit(string b)
        {
            if (b == "null") { return "审核申请"; }
            else if (b == "true") { return "取消审核"; }
            else if (b == "false") { return "审核申请"; }
            return "";
        }
        public string SetTime(string date)
        {
            string[] dt = date.Split(' ');
            date = dt[0];
            string time = date;
            return time;
        }
        public string LnkFiles(string files)
        {
            if (files == "")
                return "无";
            else
                return "<a target='_blank'  href='" + files + "' title='点击下载'>下载</a>";
        }
        //批量审核
        protected void BtnAudit_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_Adbuy.Adbuy_SetAudit(chkArr[i]);
                }
            }
            DataBind();
        }
        //批量取消审核
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_Adbuy.Adbuy_CancelAudit(chkArr[i]);
                }
            }
            DataBind();
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}