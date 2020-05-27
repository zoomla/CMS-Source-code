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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.IO;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Manage.iServer
{
    public partial class BselectiServer : CustomerPageAction
    {
        B_IServer Serverbll = new B_IServer();
        string[] typeArray = new string[] { "", "咨询", "投诉", "建议", "要求", "界面使用", "bug报告", "订单", "财务", "域名", "主机", "邮局", "DNS", "MSSQL", "MySQL", "IDC", "网站推广", "网站制作", "其它" };
        string menu = "";
        string orderId = "0";
        int type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            //设置通知方式

            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteOption"))
            {
                btnSMSNotice.Enabled = false;
                btnSMSNotice.ToolTip = "没有权限进行此项操作";
                btnEmailNotice.Enabled = false;
                btnEmailNotice.ToolTip = "没有权限进行此项操作";
            }

            if (!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='BiServer.aspx?num=-3&strTitle='>有问必答</a></li><li><a href='BselectiServer.aspx'>问题列表</a></li><li class='active'>" + retuenMapNav() + "</li>");
        }
        public void DataBind(string key = "")
        {
            B_User buser = new B_User();
            M_UserInfo info = buser.GetLogin();
            DataTable table = new DataTable();
            string state = "";
            string num = Request.QueryString["num"] == null ? "" : Request.QueryString["num"];
            string strTitle = "";
            switch (num)
            {
                case "1":
                    state = "未解决";
                    break;
                case "2":
                    state = "处理中";
                    break;
                case "3":
                    state = "已解决";
                    break;
                default:
                    state = "";
                    break;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["strTitle"]))
            {
                strTitle = Server.UrlEncode(Request.QueryString["strTitle"]).ToString();
            }
            GetQueryString();
            table = Serverbll.SeachLikeTitle(strTitle, state, info.UserID, menu, typeArray[type], DataConvert.CLng(orderId));
            if (DropDownList2.SelectedValue != "所有状态" && !string.IsNullOrEmpty(DropDownList2.SelectedValue))
            {
                DataRow[] dr = table.Select("State='" + DropDownList2.SelectedValue.ToString() + "'");
                DataTable dt = table.Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                table = dt;
            }
            if (DropDownList3.SelectedValue != "所有优先级" && DropDownList3.SelectedValue != "")
            {
                DataRow[] dr = table.Select("Priority='" + DropDownList3.SelectedValue.ToString() + "'");
                DataTable dt = table.Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                table = dt;
            }
            if (DropDownList4.SelectedValue != "所有来源" && DropDownList4.SelectedValue != "")
            {
                DataRow[] dr = table.Select("Root='" + DropDownList4.SelectedValue.ToString() + "'");
                DataTable dt = table.Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                table = dt;
            }
            if (DropDownList5.SelectedValue != "0" && DropDownList5.SelectedValue != "")
            {
                DataRow[] dr = table.Select("Type='" + DropDownList5.SelectedItem.Text + "'");
                DataTable dt = table.Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                table = dt;
            }
            Egv.DataSource = table;
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
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del":
                    M_IServer model = new B_IServer().SeachById(DataConverter.CLng(e.CommandArgument));
                    Serverbll.DeleteById(model.QuestionId);
                    break;
                default:
                    break;
            }
            DataBind();
        }
        public string GetUserName(string UserId)
        {
            B_User buser = new B_User();
            return buser.GetUserByUserID(DataConverter.CLng(UserId)).UserName;
        }
        public string GetGroupName()
        {
            B_User buser = new B_User();
            string GroupID = buser.GetLogin().GroupID.ToString();
            B_Group bgp = new B_Group();
            return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
        }
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string state = "";
            string num = Request.QueryString["num"] == null ? "" : Request.QueryString["num"];
            string strTitle = search_title.Text;
            switch (num)
            {
                case "1":
                    state = "未解决";
                    break;
                case "2":
                    state = "处理中";
                    break;
                case "3":
                    state = "已解决";
                    break;
                default:
                    state = "";
                    break;
            }
            GetQueryString();
            Response.Redirect("BselectiServer.aspx?menu=" + menu + "&state=" + state + "&OrderID=" + orderId + "&num=-1&strTitle=" + Server.UrlEncode(strTitle) + "&type=" + type + "");
        }
        //启用或关闭邮件通知
        protected void btnEmailNotice_Click(object sender, EventArgs e)
        {

            try
            {
                SiteConfig.Update();//更改配置文件
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location.href=location.href", true);
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("文件未找到！", "SiteInfo.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("检查您的服务器是否给配置文件或文件夹配置了写入权限。", "../Config/SiteInfo.aspx");
            }
        }
        //启用或关闭短信通知
        protected void btnSMSNotice_Click(object sender, EventArgs e)
        {

            try
            {
                SiteConfig.Update();//更改配置文件
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location.href=location.href", true);
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("文件未找到！", "SiteInfo.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("检查您的服务器是否给配置文件或文件夹配置了写入权限。", "../Config/SiteInfo.aspx");
            }
        }

        protected void batDel_Click(object sender, EventArgs e)//批量删除
        {
            string items = Request.Form["chkSel"];
            if (items == null)
            {
                function.WriteErrMsg("未选中任何内容");
                return;
            }
            if (items.IndexOf(",") == -1)//只有一个数值被选中，则不会带,
            {
                int QuestionId = DataConverter.CLng(items);
                Serverbll.DeleteById(QuestionId);
            }
            else if (items.IndexOf(",") > -1)
            {
                string[] dels = items.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int s = 0; s < dels.Length; s++)
                {
                    int QuestionId = DataConverter.CLng(dels[s]);
                    Serverbll.DeleteById(QuestionId);
                }
            }
            string url = HttpContext.Current.Request.Url.PathAndQuery;
            function.WriteSuccessMsg("批量删除成功!");
        }
        private void GetQueryString()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]))
            {
                menu = Request.QueryString["menu"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["OrderID"]))
            {
                orderId = Request.QueryString["OrderID"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                type = DataConverter.CLng(Request.QueryString["type"]);
            }
        }
        protected void Bind(object sender, EventArgs e)
        {
            DataBind();
        }
        protected string retuenMapNav()
        {
            string mapNav = "所有问题";
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                mapNav = typeArray[DataConverter.CLng(Request.QueryString["type"])];
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["num"]))
            {
                string[] NN = { "", "未解决的问题", "处理中的问题", "已解决的问题" };
                int number = DataConverter.CLng(Request.QueryString["num"]);
                if (number > 0)
                    mapNav = NN[DataConverter.CLng(Request.QueryString["num"])];
            }
            return mapNav;
        }
    }
}