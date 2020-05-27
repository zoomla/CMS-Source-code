using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model.Content;

namespace ZoomLaCMS.Manage.Pub
{
    public partial class PubExcel : System.Web.UI.Page
    {
        B_Pub_Excel excelBll = new B_Pub_Excel();
        M_Pub_Excel excelMod = new M_Pub_Excel();
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>" + Resources.L.后台管理 + "</li><li><a href='PubManage.aspx'>" + Resources.L.互动模块 + "</a></li><li><a href='PubExcel.aspx'>" + Resources.L.互动Excel配置 + "</a><a href='javascript:;' style='color:red;margin-left:5px;' title='" + Resources.L.添加 + "' onclick='Clear();'>[" + Resources.L.添加导出配置 + "]</a></li>");
                MyBind();
            }
        }
        public void MyBind()
        {
            EGV.DataSource = excelBll.Sel();
            EGV.DataBind();
        }
        protected void Add_Btn_Click(object sender, EventArgs e)
        {
            excelMod = FillMod();
            if (!string.IsNullOrEmpty(ID_Hid.Value))
                excelBll.UpdateByID(excelMod);
            else
                excelBll.Insert(excelMod);
            MyBind();
        }
        private M_Pub_Excel FillMod()
        {
            M_Pub_Excel model = new M_Pub_Excel();
            if (!string.IsNullOrEmpty(ID_Hid.Value))
                model = excelBll.SelReturnModel(Convert.ToInt32(ID_Hid.Value));
            model.TableName = TableName_T.Text;
            model.CNames = CNames_T.Text;
            model.Fields = Fields_T.Text;
            model.UserID = badmin.GetAdminLogin().AdminId;
            model.CreateTime = DateTime.Now;
            return model;
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
                    excelBll.Del(Convert.ToInt32(e.CommandArgument));
                    MyBind();
                    break;
                case "edit2":
                    EditPub(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
        void EditPub(int id)
        {
            excelMod = excelBll.SelReturnModel(id);
            TableName_T.Text = excelMod.TableName;
            CNames_T.Text = excelMod.CNames;
            Fields_T.Text = excelMod.Fields;
            ID_Hid.Value = excelMod.ID.ToString();
            function.Script(this, "ShowModel();");
        }
        protected void CurEdit_B_Click(object sender, EventArgs e)
        {
            EditPub(DataConverter.CLng(curid_hid.Value));
        }
    }
}