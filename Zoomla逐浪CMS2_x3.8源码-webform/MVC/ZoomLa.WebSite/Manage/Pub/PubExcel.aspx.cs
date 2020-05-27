using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Content;

namespace ZoomLaCMS.Manage.Pub
{
    public partial class PubExcel : System.Web.UI.Page
    {
        B_Pub_Excel excelBll = new B_Pub_Excel();
        M_Pub_Excel excelMod = new M_Pub_Excel();
        B_Admin badmin = new B_Admin();
        B_ModelField mfBll = new B_ModelField();
        B_Model modBll = new B_Model();
        public int ModelID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>" + Resources.L.后台管理 + "</li><li><a href='PubManage.aspx'>" + Resources.L.互动模块 + "</a></li><li><a href='PubExcel.aspx'>" + Resources.L.互动Excel配置 + "</a><a href='javascript:;' style='color:red;margin-left:5px;' title='" + Resources.L.添加 + "' onclick='Clear();'>[" + Resources.L.添加导出配置 + "]</a></li>");
                MyBind();
                if (ModelID > 0)
                {
                    M_ModelInfo modeli = modBll.GetModelById(ModelID);
                    if (modeli == null) return;
                    TableName_T.Text = modeli.TableName;
                    DataTable tablelist = mfBll.SelByModelID(ModelID, true);
                    DataTable dt = SysField_Pub(tablelist);
                    string fields = "", fieldNames = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        fields += dr["FieldName"] + ",";
                        fieldNames += dr["FieldAlias"] + ",";
                    }
                    Fields_T.Text = fields.Trim(',');
                    CNames_T.Text = fieldNames.Trim(',');
                    function.Script(this, "ShowModel();");
                }
            }
        }
        public void MyBind()
        {
            EGV.DataSource = excelBll.Sel();
            EGV.DataBind();
        }
        private DataTable SysField_Pub(DataTable dt)
        {
            dt.DefaultView.RowFilter = "sys_type=0";
            dt = dt.DefaultView.ToTable();
            string[] fields = "互动编号:Pubupid,用户名:PubUserName,用户ID:PubUserID,内容ID:PubContentid,录入者:PubInputer,父级ID:Parentid,IP地址:PubIP,互动数量:Pubnum,开始:Pubstart,互动标题:PubTitle,互动内容:PubContent,添加时间:PubAddTime,评价:Optimal".Split(',');
            foreach (string field in fields)
            {
                DataRow dr = dt.NewRow();
                dr["sys_type"] = 1;
                dr["FieldAlias"] = field.Split(':')[0];
                dr["FieldName"] = field.Split(':')[1];
                dr["FieldType"] = "TextType";
                dr["OrderID"] = 0;
                dt.Rows.Add(dr);
            }
            return dt;
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