using System;
using System.Collections.Generic;
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
namespace ZoomLaCMS.Manage.Plat
{
    public partial class MemberList : CustomerPageAction
    {
        B_User_Plat upBll = new B_User_Plat();
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_User buser = new B_User();
        // 公司ID
        private int CompID { get { return DataConvert.CLng(Request.QueryString["CompID"]); } }
        private int GroupID { get { return DataConvert.CLng(Request.QueryString["GroupID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='CompList.aspx'>能力中心</a></li><li><a href='MemberList.aspx'>成员列表</a></li>");
                MyBind();
            }
        }

        private void MyBind(string key = "")
        {
            EGV.DataSource = upBll.SelByCompany(CompID, key);
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int uid = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName)
            {
                case "del":
                    upBll.Comp_RemoveUser(uid);
                    break;
                case "setAdmin":
                    //upBll.SetAdmin(id);
                    break;
                case "recallAdmin":
                    //upBll.RecallAdmin(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView dr = e.Row.DataItem as DataRowView;
            //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
            //}
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            string skey = Skey_T.Text;
            if (!string.IsNullOrEmpty(skey))
            {
                sel_box.Attributes.Add("style", "display:inline;");
                EGV.Attributes.Add("style", "margin-top:44px;");
            }
            else
            {
                sel_box.Attributes.Add("style", "display:none;");
                EGV.Attributes.Add("style", "margin-top:0px;");
            }
            MyBind(skey);
        }

        protected void BatAdd_Btn_Click(object sender, EventArgs e)
        {
            M_Plat_Comp compMod = compBll.SelReturnModel(CompID);
            string[] ids = UserIDS_Hid.Value.Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ids.Length; i++)
            {
                int uid = DataConvert.CLng(ids[i]);
                M_User_Plat upMod = upBll.SelReturnModel(uid);
                if (upMod == null)
                {
                    M_UserInfo newmu = buser.SelReturnModel(uid);
                    upMod = upBll.NewUser(newmu, compMod);
                    upBll.Insert(upMod);
                }
                else { upMod.CompID = CompID;upBll.UpdateByID(upMod); }
            }
            MyBind();
        }
    }
}