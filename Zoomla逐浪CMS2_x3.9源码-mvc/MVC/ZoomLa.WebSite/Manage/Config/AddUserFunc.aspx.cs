using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Config
{
    public partial class AddUserFunc : CustomerPageAction
    {
        B_Search b_search = new B_Search();
        B_Admin badmin = new B_Admin();
        B_Group groupBll = new B_Group();
        M_Search searchMod = null;
        public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "checkname":
                        result = CheckSearchName();
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                return;
            }
            if (!IsPostBack)
            {
                if (Mid > 0)
                {
                    searchMod = b_search.GetSearchById(Mid);
                    txtName.Text = searchMod.Name;
                    //rdoType.SelectedValue = search.Type.ToString();
                    txtFileUrl.Text = searchMod.FlieUrl;
                    Edit_Btn.InnerText = "保存设置";
                    hideid.Value = ID.ToString();
                    ItemIcon_T.Text = searchMod.Ico;
                    EditSearchName_Hid.Value = searchMod.Name;
                    rdoOpenType.SelectedValue = searchMod.OpenType.ToString();
                    SupportMobile.SelectedValue = searchMod.Mobile.ToString();
                    MotroSize.SelectedValue = searchMod.Size.ToString();
                    txtOrderID.Value = searchMod.OrderID.ToString();
                    IsEliteLevel.Checked = searchMod.EliteLevel == 1 ? true : false;
                    function.Script(this, "CheckUserType();");
                }
                DataTable dt = groupBll.Select_All();
                selGroup_Rpt.DataSource = dt;
                selGroup_Rpt.DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href=\"UserFunc.aspx?LinkType=3\">会员导航</a></li><li class='active'>添加会员导航</li>");
            }
        }
        public string CheckSearchName()
        {
            DataTable dt = b_search.SelByName(Request.Form["name"], 2);
            if (dt.Rows.Count > 0)
            { return "-1"; }
            return "1";
        }
        //获取用户组选中状态
        public string GetChecked()
        {
            string groupid = Eval("GroupID").ToString();
            if (Mid > 0)
                searchMod = b_search.GetSearchById(Mid);
            if (searchMod != null && searchMod.UserGroup.Split(',').Contains(groupid))
                return "checked";
            else
                return "";
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            b_search = new B_Search();
            M_Search search = new M_Search();
            search.Name = txtName.Text;
            //search.Type = DataConverter.CLng(rdoType.SelectedValue);
            search.FlieUrl = txtFileUrl.Text.Trim();
            string pic = ItemIcon_T.Text;
            if (pic != "")
            {
                search.Ico = pic;
            }
            search.Mobile = Convert.ToInt32(SupportMobile.SelectedValue);
            search.Size = Convert.ToInt32(MotroSize.SelectedValue);
            search.OpenType = DataConverter.CLng(rdoOpenType.SelectedValue);
            search.AdminID = badmin.GetAdminLogin().AdminId;

            search.Type = 2;
            search.UserGroup = "";
            if (!string.IsNullOrEmpty(Request.Form["selGroup"]))
                search.UserGroup = Request.Form["selGroup"];//用户组权限
                                                            ///search.LinkType =DataConverter.CLng(rdoLinkType.SelectedValue);

            search.State = 1;
            search.LinkState = 2;
            search.EliteLevel = IsEliteLevel.Checked == true ? 1 : 0;
            if (Mid > 0)
            {
                search.Id = Mid;
                search.OrderID = Convert.ToInt32(txtOrderID.Value);
                bool res = b_search.UpdateByID(search);
                if (res)
                    function.WriteSuccessMsg("修改成功!", "UserFunc.aspx?EliteLevel=2");
                else
                    function.WriteErrMsg("修改失败!");
            }
            else
            {
                search.OrderID = b_search.SelMaxOrder() + 1;
                int res = b_search.insert(search);
                function.WriteSuccessMsg("添加成功!", "UserFunc.aspx?EliteLevel=2");
            }
        }
    }
}