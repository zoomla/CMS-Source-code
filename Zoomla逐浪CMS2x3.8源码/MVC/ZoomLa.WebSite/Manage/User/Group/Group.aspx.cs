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
using ZoomLa.Common;
using System.Xml;


namespace ZoomLaCMS.Manage.User
{
    public partial class GroupPage : CustomerPageAction
    {
        B_Group bll = new B_Group();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public int ParentID { get { return DataConverter.CLng(ViewState["ParentID"]); } set { ViewState["ParentID"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "UserGroup")) { function.WriteErrMsg(Resources.L.没有权限进行此项操作); }
                ParentID = DataConverter.CLng(Request.QueryString["ParentID"]);
                if (Mid>0)
                {
                    M_Group info = this.bll.GetByID(Mid);
                    this.ParentID = info.ParentGroupID;
                    this.TxtGroupName.Text = info.GroupName;
                    this.TxtDescription.Text = info.Description;
                    this.RBLReg.Checked = info.RegSelect;
                    this.UpGradeMoney.Text = info.UpGradeMoney.ToString();
                    this.UpSIcon_T.Text = info.UpSIcon.ToString();
                    this.UpPoint_T.Text = info.UpPoint.ToString();
                    this.RBcompanyG.Checked = info.CompanyGroup == 1 ? true : false;
                    this.RBVipG.Checked = info.VIPGroup == 1 ? true : false;
                    this.txtVIPNum.Text = info.VIPNum.ToString();
                    this.OtherName.Text = info.OtherName;
                    this.txtRebateRate.Text = info.RebateRate.ToString();
                    this.txtCreit.Text = info.Credit.ToString();
                    this.txt_Enroll.Checked = info.Enroll.Contains("isenroll") ? true : false;
                    foreach (ListItem item in ClassEnroll_Radio.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value) && info.Enroll.Contains(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                if (ParentID > 0)
                {
                    M_Group info = this.bll.GetByID(ParentID);
                    Label1.Text = info.GroupName;
                }
                else { Label1.Text = Resources.L.系统会员组; }
                Call.SetBreadCrumb(Master, "<li>" + Resources.L.后台管理 + "</li><li>" + Resources.L.会员管理 + "</li><li><a href=\"GroupManage.aspx\">" + Resources.L.会员组管理 + "</a></li>");
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Group info = new M_Group();
            if (Mid > 0) { info = bll.SelReturnModel(Mid); }
            info.GroupName = this.TxtGroupName.Text.Trim();
            info.Description = this.TxtDescription.Text.Trim();
            info.RegSelect = RBLReg.Checked;
            info.UpGradeMoney = DataConverter.CDouble(UpGradeMoney.Text);
            info.UpSIcon = DataConverter.CDouble(UpSIcon_T.Text);
            info.UpPoint = DataConverter.CDouble(UpPoint_T.Text);
            info.OtherName = this.OtherName.Text;
            info.CompanyGroup = RBcompanyG.Checked ? 1 : 0;
            info.RebateRate = DataConverter.CFloat(this.txtRebateRate.Text.Trim());
            info.Credit = DataConverter.CLng(txtCreit.Text);
            info.Enroll = "";
            if (txt_Enroll.Checked)
                info.Enroll = "isenroll";
            if (!string.IsNullOrEmpty(ClassEnroll_Radio.SelectedValue))
                info.Enroll += "," + ClassEnroll_Radio.SelectedValue;
            info.Enroll = info.Enroll.Trim(',');
            info.VIPGroup = RBVipG.Checked ? 1 : 0;
            info.VIPNum = Convert.ToInt32(this.txtVIPNum.Text.ToString());
            if (info.GroupID > 0) { bll.Update(info); }
            else
            {
                info.ParentGroupID = ParentID;
                info.IsDefault = (!bll.HasGroup());
                bll.Add(info);
            }
            function.WriteSuccessMsg("操作成功", "GroupManage.aspx");
        }
    }
}