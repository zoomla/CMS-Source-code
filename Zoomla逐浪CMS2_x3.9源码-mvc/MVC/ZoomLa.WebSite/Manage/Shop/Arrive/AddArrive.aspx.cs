using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;

namespace ZoomLaCMS.Manage.Shop.Arrive
{
    public partial class AddArrive : CustomerPageAction
    {
        B_Arrive avBll = new B_Arrive();
        B_User buser = new B_User();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../ProductManage.aspx'>商城管理</a></li><li><a href=\"ArriveManage.aspx\">优惠券列表</a></li><li>优惠券管理</li>");
            }
        }
        private void MyBind()
        {

            if (Mid > 0)
            {
                EBtnSubmit.Text = "保存设置";
                no.Visible = true;
                createNum.Visible = false;
                txtName.Enabled = false;
                Magclass.Enabled = false;
                M_Arrive avMod = avBll.GetArriveById(Mid);
                txtName.Text = avMod.ArriveName;
                Magclass.SelectedValue = avMod.Type.ToString();
                txtNo.Text = avMod.ArriveNO;
                txtPwd.Text = avMod.ArrivePwd;
                EditecodType.Style.Add("display", "none");

                minAmount_T.Text = avMod.MinAmount.ToString();
                maxAmount_T.Text = avMod.MaxAmount.ToString();
                Amount_T.Text = avMod.Amount.ToString();
                Amount_T.Enabled = avMod.State == 0;
                AgainTime_T.Text = avMod.AgainTime.ToString();
                EndTime_T.Text = avMod.EndTime.ToString();
                txtState.Text = "未激活";
                if (avMod.State > 0) { txtState.Text = avMod.State == 1 ? "已激活" : "已使用"; }
                M_UserInfo info = buser.GetUserByUserID(avMod.UserID);
                if (!info.IsNull)
                {
                    txtUserID.Text = info.UserName;
                    hfid.Value = avMod.UserID.ToString();
                }
                else
                {
                    txtUserID.Text = "未送出";
                }
            }
            else
            {
                createNum.Visible = true;
                no.Visible = false;
                AgainTime_T.Text = DateTime.Now.ToString("yyyy/MM/dd");
                EndTime_T.Text = DateTime.Now.AddYears(1).ToString("yyyy/MM/dd");
            }
        }

        //制作抵用劵
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Arrive avMod = new M_Arrive();
            if (Mid > 0) { avMod = avBll.SelReturnModel(Mid); }
            avMod.ArriveName = txtName.Text;
            avMod.MinAmount = DataConverter.CDouble(minAmount_T.Text);
            avMod.MaxAmount = DataConverter.CDouble(maxAmount_T.Text);
            avMod.AgainTime = DataConverter.CDate(AgainTime_T.Text);
            avMod.EndTime = DataConverter.CDate(EndTime_T.Text);
            avMod.Amount = DataConverter.CDouble(Amount_T.Text);
            avMod.Type = DataConverter.CLng(Magclass.SelectedItem.Value);
            if (avMod.EndTime <= DateTime.Now) { function.WriteErrMsg("到期时间不能早于当前时间"); }
            if (avMod.AgainTime >= avMod.EndTime) { function.WriteErrMsg("到期时间不能晚于发布时间"); }
            if (avMod.MaxAmount < 0 || avMod.MinAmount < 0) { function.WriteErrMsg("使用范围数值不正确"); }
            if (avMod.MaxAmount != 0 && avMod.MinAmount > avMod.MaxAmount) { function.WriteErrMsg("使用范围不正确,最小值不能大于最大值"); }
            if (avMod.Amount < 1) { function.WriteErrMsg("优惠金额不正确,最小值为1"); }
            //----------------------------------------
            if (avMod.ID < 1)//添加优惠券
            {
                avMod.Flow = Guid.NewGuid().ToString();
                int num = DataConverter.CLng(txtCreateNum.Text);
                for (int i = 0; i < num; i++)
                {
                    switch (EcodeType.SelectedItem.Value)
                    {
                        case "0"://纯数字
                            avMod.ArriveNO = function.GetRandomString(9, 2);
                            break;
                        case "1"://字母
                            avMod.ArriveNO = "ZL" + function.GetRandomString(9, 3).ToLower();
                            break;
                        case "2"://混淆
                            avMod.ArriveNO = "ZL" + function.GetRandomString(9, 3).ToLower();
                            break;
                        default:
                            break;
                    }
                    avMod.ArrivePwd = "ZL" + function.GetRandomString(9);
                    avMod.State = isValid_Chk.Checked ? 1 : 0;
                    avMod.UserID = 0;
                    avBll.GetInsert(avMod);
                }
                function.WriteSuccessMsg("批量添加成功!", "ArriveManage.aspx?name=" + avMod.ArriveName);
            }
            else
            {
                avMod.ArriveNO = txtNo.Text;
                avMod.ArrivePwd = txtPwd.Text;
                avMod.State = txtState.Text == "未使用" ? 0 : 1;
                avMod.UserID = DataConverter.CLng(hfid.Value);
                avBll.GetUpdate(avMod);
                function.WriteSuccessMsg("修改成功!", "ArriveManage.aspx");
            }
        }
    }
}