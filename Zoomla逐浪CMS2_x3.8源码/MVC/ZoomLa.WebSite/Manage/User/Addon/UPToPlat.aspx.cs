using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User.Addon
{
    //升级或修改所属公司
    public partial class UPToPlat : System.Web.UI.Page
    {
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_Plat_Group gpBll = new B_Plat_Group();
        B_User_Plat upBll = new B_User_Plat();
        B_User buser = new B_User();
        public int UserID { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable compDT = compBll.Sel();
                if (UserID < 1) { function.WriteErrMsg("未指定需要操作的用户"); }
                if (compDT.Rows.Count < 1) { function.WriteErrMsg("还没有公司数据,请先添加公司"); }
                M_UserInfo mu = buser.SelReturnModel(UserID);
                UserName_L.Text = mu.UserName;
                PlatComp_DP.DataSource = compDT;
                PlatComp_DP.DataBind();
                PlatGroup_DP.DataSource = gpBll.SelByCompID(Convert.ToInt32(PlatComp_DP.SelectedValue));
                PlatGroup_DP.DataBind();
                PlatGroup_DP.Items.Insert(0, new ListItem("不分配", ""));
                //---------
                M_User_Plat upMod = upBll.SelReturnModel(UserID);
                if (upMod == null || upMod.CompID < 1)
                {
                    PlatInfo_L.Text = "<span style='color:red;'>该用户尚未加入过能力中心</span>";
                    Remove_Btn.Visible = false;
                }
                else
                {
                    PlatInfo_L.Text += "<span class='btn btn-default margin_r5'>所属公司：" + (string.IsNullOrEmpty(upMod.CompName) ? "无" : upMod.CompName) + "</span>";
                    PlatInfo_L.Text += "<span class='btn btn-default margin_r5'>所属部门：" + (string.IsNullOrEmpty(upMod.GroupName) ? "未分配" : upMod.GroupName) + "</span>";
                }
                Call.HideBread(Master);
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.SelReturnModel(UserID);
            M_User_Plat upMod = upBll.SelReturnModel(UserID);
            M_Plat_Group gpMod = gpBll.SelReturnModel(DataConvert.CLng(PlatGroup_DP.SelectedValue));
            M_Plat_Comp compMod = compBll.SelReturnModel(Convert.ToInt32(PlatComp_DP.SelectedValue));
            if (upMod == null)
            {
                upMod = upBll.NewUser(mu, compMod);
                upMod.Plat_Group = PlatGroup_DP.SelectedValue;
                upBll.Insert(upMod);
            }
            else
            {
                upMod.CompID = compMod.ID;
                upMod.Status = 1;
                upBll.UpdateByID(upMod);
            }
            if (gpMod != null)
            {
                gpBll.AddMember(UserID.ToString(), gpMod.ID);
            }
            function.WriteSuccessMsg("操作成功", Request.RawUrl);
        }
        protected void PlatComp_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlatGroup_DP.DataSource = gpBll.SelByCompID(Convert.ToInt32(PlatComp_DP.SelectedValue));
            PlatGroup_DP.DataBind();
            PlatGroup_DP.Items.Insert(0, new ListItem("无部门", "0"));
        }
        protected void Remove_Btn_Click(object sender, EventArgs e)
        {
            M_User_Plat upMod = upBll.SelReturnModel(UserID);
            if (upMod != null)
            {
                upMod.Status = -1;
                upMod.CompID = 0;
                upMod.Plat_Group = "";
                upBll.UpdateByID(upMod);
            }
            function.WriteSuccessMsg("操作成功",Request.RawUrl);
        }
    }
}