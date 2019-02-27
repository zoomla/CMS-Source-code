using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Model;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class StoreUpdate : CustomerPageAction
    {
        B_ModelField mfbll = new B_ModelField();
        B_Content cbll = new B_Content();
        protected B_Sensitivity sll = new B_Sensitivity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uname", Request.QueryString["username"]) };
                DataTable shopinfo = mfbll.SelectTableName("ZL_CommonModel", "tablename like 'ZL_Store_%' and Inputer=@uname", sp);
                if (shopinfo.Rows.Count < 1) { function.WriteErrMsg("店铺不存在"); }
                DataTable modeinfo = mfbll.SelectTableName("ZL_Model", "TableName=@TableName", new SqlParameter[] { new SqlParameter("TableName", shopinfo.Rows[0]["TableName"].ToString()) });
                if (shopinfo.Rows.Count > 0)
                {
                    if (modeinfo.Rows.Count > 0)
                    {
                        UserShopName_T.Text = shopinfo.Rows[0]["Title"].ToString();
                        this.ViewState["GeneralID"] = shopinfo.Rows[0]["GeneralID"].ToString();
                        DataTable cmdinfo = cbll.GetContent(Convert.ToInt32(shopinfo.Rows[0]["GeneralID"].ToString()));
                        if (cmdinfo.Rows.Count > 0)
                        {
                            this.HiddenField1.Value = cmdinfo.Rows[0]["StoreModelID"].ToString();
                            ModelHtml.Text = mfbll.InputallHtml(int.Parse(cmdinfo.Rows[0]["StoreModelID"].ToString()), 0, new ModelConfig()
                            {
                                ValueDT = cmdinfo
                            });
                        }
                    }
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Shop/ProductManage.aspx'>商城管理</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li class='active'>修改店铺信息</li>");
        }
        protected void Esubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = this.mfbll.GetModelFieldList(int.Parse(this.HiddenField1.Value));
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                {
                    if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                    {
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                    }
                }
                if (dr["FieldType"].ToString() == "FileType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        DataRow row2 = table.NewRow();
                        row2[0] = sizefield;
                        row2[1] = "FileSize";
                        row2[2] = this.Page.Request.Form["txt_" + sizefield];
                        table.Rows.Add(row2);
                    }
                }
                if (dr["FieldType"].ToString() == "MultiPicType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
                        {
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                        }
                        DataRow row1 = table.NewRow();
                        row1[0] = sizefield;
                        row1[1] = "ThumbField";
                        row1[2] = this.Page.Request.Form["txt_" + sizefield];
                        table.Rows.Add(row1);
                    }
                }
                DataRow row = table.NewRow();
                row[0] = dr["FieldName"].ToString();
                string ftype = dr["FieldType"].ToString();
                row[1] = ftype;
                string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
                {
                    fvalue = sll.ProcessSen(fvalue);
                }
                row[2] = fvalue;
                table.Rows.Add(row);
            }
            DataRow rs4 = table.NewRow();
            rs4[0] = "StoreName";
            rs4[1] = "TextType";
            rs4[2] = this.UserShopName_T.Text;
            table.Rows.Add(rs4);
            M_CommonData CData = cbll.GetCommonData(Convert.ToInt32(this.ViewState["GeneralID"].ToString()));
            CData.Title = UserShopName_T.Text;
            cbll.UpdateContent(table, CData);
            Response.Redirect("StoreManage.aspx");
        }
    }
}