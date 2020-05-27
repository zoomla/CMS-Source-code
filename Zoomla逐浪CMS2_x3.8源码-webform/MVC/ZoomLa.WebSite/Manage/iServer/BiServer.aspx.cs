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

namespace ZoomLaCMS.Manage.iServer
{
    public partial class BiServer : CustomerPageAction
    {
        B_IServer Serverbll = new B_IServer();
        string[] typeArray ={"","咨询","投诉","建议","要求","界面使用", "bug报告", "订单", "财务", "域名", "主机" , "邮局" , "DNS", "MSSQL"
                            ,"MySQL", "IDC", "网站推广", "网站制作", "其它"};
        string menu = "";
        string orderId = "";
        int type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                //读取用户提交的问题分类
                repSeachBtn.DataSource = Serverbll.GetSeachUserIdType(-1);
                repSeachBtn.DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='BiServer.aspx'>有问必答</a></li><li class='active'><a href='AddQuestionRecord.aspx'>[添加问题记录]</a></li>" + Call.GetHelp(49));
        }

        public void MyBind()
        {
            string state = "";
            string num = Request.QueryString["num"] == null ? "" : Request.QueryString["num"];
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
                case "4":
                    state = "已锁定";
                    break;
                default:
                    state = "未解决";
                    break;
            }
            GetQueryString();
            lblAllNum.Text = Serverbll.getiServerNum("", -1, menu, typeArray[type], DataConverter.CLng(orderId)).ToString();
            lblNum_ch.Text = Serverbll.getiServerNum("处理中", -1, menu, typeArray[type], DataConverter.CLng(orderId)).ToString();
            lblnum_w.Text = Serverbll.getiServerNum("未解决", -1, menu, typeArray[type], DataConverter.CLng(orderId)).ToString();
            lblnum_y.Text = Serverbll.getiServerNum("已解决", -1, menu, typeArray[type], DataConverter.CLng(orderId)).ToString();

            if (int.Parse(lblnum_w.Text.ToString()) >= 0)
                panel_w.Visible = true;
            if (int.Parse(lblNum_ch.Text.ToString()) >= 0)
                panel_ch.Visible = true;
            if (int.Parse(lblnum_y.Text.ToString()) >= 0)
                panel_y.Visible = true;

            resultsRepeater_w.DataSource = Serverbll.SeachTop(state, -1, menu, typeArray[type], DataConverter.CLng(orderId));
            resultsRepeater_w.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string strTitle = this.TextBox1.Text.ToString();
            GetQueryString();
            if (menu != "")
                Response.Redirect("SelectiServer.aspx?num=-1&menu=" + menu + "&strTitle=" + strTitle);
            else
                Response.Redirect("SelectiServer.aspx?num=-1&strTitle=" + strTitle);
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
        public string GetUserName(string UserId)
        {
            B_User buser = new B_User();
            return buser.GetUserByUserID(DataConverter.CLng(UserId)).UserName;
        }
        protected int returnType(object typeName)
        {
            int index = 0;
            for (int i = 0; i < typeArray.Length; i++)
            {
                if (typeName.ToString().Trim() == typeArray[i])
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        protected void resultsRepeater_w_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del":
                    M_IServer model = Serverbll.SeachById(DataConverter.CLng(e.CommandArgument));
                    Serverbll.DeleteById(model.QuestionId);
                    MyBind();
                    break;
                default:
                    break;
            }
        }
    }
}