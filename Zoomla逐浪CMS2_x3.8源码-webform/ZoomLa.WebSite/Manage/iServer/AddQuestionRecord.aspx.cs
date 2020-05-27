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

public partial class manage_iServer_AddQuestionRecord : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["OrderID"] != null)
            {
                string OrderID = Request.QueryString["OrderID"].ToString();
                this.OrderID.Value = OrderID;
                this.DropDownList4.SelectedValue = "订单";
                this.DropDownList3.SelectedValue = "网页表单";
            }
            B_User ull = new B_User();
            this.txtUserName.Text = ull.GetLogin().UserName;
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='BiServer.aspx?num=-3&strTitle='>有问必答</a></li><li class='active'>添加问题记录</li>" + Call.GetHelp(50));
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        B_User buser = new B_User();
        M_IServer iserver = new M_IServer();
        string strName = txtUserName.Text.Trim();
        iserver.UserId = buser.GetUserByName(strName).UserID;
        iserver.Title = txtTitle.Text.ToString();
        iserver.Content = textarea1.Value;
        iserver.Priority = this.DropDownList2.SelectedValue.ToString();
        iserver.Type = this.DropDownList4.SelectedValue.ToString();
        iserver.SubTime = DateTime.Now;
        iserver.Root = this.DropDownList3.SelectedValue.ToString();
        iserver.State = this.DropDownList1.SelectedValue.ToString();
        iserver.Path = Attach_Hid.Value;
        if (Request.Form["OrderID"] != null)
        {
            iserver.OrderType = DataConverter.CLng(Request.Form["OrderID"]);
        }
        else
        {
            iserver.OrderType = 0;
        }
        if (iserver.State.ToString().Equals("已解决"))
        {
            iserver.SolveTime = DateTime.Now;
        }
        if (strName == null || strName == "" || iserver.UserId <= 0)
        {
            function.WriteErrMsg("请输入正确的会员登录帐号");
            return;
        }
        if (txtTitle.Text == "" || this.textarea1.Value == "" || txtTitle.Text == null || this.textarea1.Value == null)
        {
            function.WriteErrMsg("请输入标题或内容!");
            return;
        }
        else
        {
            if (new B_IServer().Add(iserver))
            {
                if (Request.Form["OrderID"] != null)
                {
                    //Orderlistinfo.aspx?id=8
                    function.WriteSuccessMsg("添加成功!", "../Shop/Orderlistinfo.aspx?id=" + Request.Form["OrderID"]);
                }
                else
                {
                    function.WriteSuccessMsg("添加成功!", "BiServer.aspx");
                }
            }
            else
            {
                function.WriteErrMsg("添加失败");
                return;
            }
        }

    }

}
