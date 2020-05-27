using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

namespace ZoomLaCMS.Manage.User.Service
{
    public partial class AddSeat : CustomerPageAction
    {
        protected string SeatType = "添加席位";
        B_Admin adminBll = new B_Admin();
        B_ServiceSeat seatBll = new B_ServiceSeat();
        M_ServiceSeat seatMod = new M_ServiceSeat();
        B_Role brbll = new B_Role();
        B_User buser = new B_User();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = seatBll.Select_All();
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    ddlIndex.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                if (Mid > 0)
                {
                    SeatType = "修改席位";
                    seatMod = seatBll.GetSelect(Mid);
                    M_UserInfo mu = buser.GetSelect(seatMod.S_AdminID);
                    SeatName_T.Text = seatMod.S_Name;
                    User_T.Text = mu.UserName;
                    ddlIndex.SelectedValue = seatMod.S_Index.ToString();
                    RadioButtonList1.Checked = seatMod.S_Default == 1 ? true : false;
                    SFileUp.FileUrl = seatMod.S_FaceImg;
                }
                else
                {
                    ddlIndex.Items.Add(new ListItem((ddlIndex.Items.Count + 1).ToString(), (ddlIndex.Items.Count + 1).ToString()));
                }
                Call.SetBreadCrumb(Master, "<li>后台管理</li><li>客服通</li><li><a href='ServiceSeat.aspx'>客服列表</a></li><li>客服管理</li>");
            }
        }
        //添加或修改客服
        protected void Button1_Click(object sender, EventArgs e)
        {
            SFileUp.SaveFile();
            M_UserInfo mu = buser.GetUserByName(User_T.Text);
            if (mu == null)
            {
                mu = new M_UserInfo();
                mu.UserName = User_T.Text;
                mu.UserPwd = StringHelper.MD5("123456");
                mu.Status = 0;
                mu.Email = function.GetRandomString(8) + "@ss.com";
                mu.HoneyName = SeatName_T.Text;
                mu.UserFace = SFileUp.FileUrl;
                mu.UserID = buser.AddModel(mu);
            }
            if (Mid > 0) { seatMod = seatBll.GetSelect(Mid); }
            seatMod.S_Index = DataConverter.CLng(ddlIndex.SelectedValue);
            seatMod.S_Name = SeatName_T.Text;
            seatMod.S_AdminID = mu.UserID;
            seatMod.S_Default = RadioButtonList1.Checked ? 1 : 0;
            seatMod.S_Remrk = mu.UserName;
            seatMod.S_FaceImg = SFileUp.FileUrl;
            if (RadioButtonList1.Checked)
            {
                seatBll.UpdateDefault(0);
            }
            if (Mid > 0)
            {
                mu.HoneyName = SeatName_T.Text;
                mu.UserFace = SFileUp.FileUrl;
                buser.UpDateUser(mu);
                seatBll.InsertUpdateByAdminid(seatMod);
                function.WriteSuccessMsg("席位修改成功", "ServiceSeat.aspx");
            }
            else
            {
                seatBll.Insert(seatMod);
                function.WriteSuccessMsg("席位添加成功", "ServiceSeat.aspx");
            }
        }
    }
}