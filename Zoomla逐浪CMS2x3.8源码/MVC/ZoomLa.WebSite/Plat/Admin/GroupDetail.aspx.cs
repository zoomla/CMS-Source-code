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


namespace ZoomLaCMS.Plat.Admin
{
    public partial class GroupDetail : System.Web.UI.Page
    {
        private B_Plat_Group gpBll = new B_Plat_Group();
        private B_User_Plat upBll = new B_User_Plat();
        public int Gid
        {
            get { return DataConvert.CLng(Request.QueryString["ID"]); }
        }
        public int MType { get { return DataConvert.CLng(Request.QueryString["MType"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_User_Plat.IsAdmin())//是本部门的管理员也可,另Gid必须属于本公司
            {
                function.WriteErrMsg("你无权访问该页");
            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            M_Plat_Group gpMod = gpBll.SelReturnModel(Gid);
            M_User_Plat upMod = upBll.SelReturnModel(gpMod.CreateUser);//创建人模型
            DataTable dt = new DataTable();
            if (MType == 1)
                dt = upBll.SelByIDS(gpMod.ManageIDS);
            else
                dt = upBll.SelByGroup(upMod.CompID, Gid);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    if (MType == 1)
                        gpBll.DelMember(e.CommandArgument.ToString(), Gid, 2);
                    else gpBll.DelMember(e.CommandArgument.ToString(), Gid);
                    break;
            }
            MyBind();
        }
        protected void BatRemove_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idChk"]))
            {
                function.Script(this, "alert('选择为空!!');");
            }
            else
            {
                if (MType == 1)
                    gpBll.DelMember(Request.Form["idChk"], Gid, 2);
                else gpBll.DelMember(Request.Form["idChk"], Gid);
            }
            MyBind();
        }
    }
}