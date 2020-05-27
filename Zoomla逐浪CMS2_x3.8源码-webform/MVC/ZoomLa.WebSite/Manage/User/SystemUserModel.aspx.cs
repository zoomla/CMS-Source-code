using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using System.Text;
using System.Configuration;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.User
{
    public partial class SystemUserModel :CustomerPageAction
    {
        private B_Label bll = new B_Label();
        B_UserBaseField bbf = new B_UserBaseField();
        B_ModelField bmf = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "UserModelField"))
                {
                    function.WriteErrMsg(Resources.L.没有权限进行此项操作);
                }
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='UserManage.aspx'>" + Resources.L.用户管理 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.会员管理 + "</a></li> <li>" + Resources.L.注册字段管理 + "<a id='ShowLink' href='javascript:;' onclick='ShowList();'>[" + Resources.L.显示所有字段 + "]</a><a href='../Content/AddModelField.aspx?ModelID=-1&ModelType=9'>[" + Resources.L.添加字段 + "]</a></li>" + Call.GetHelp(106));
        }
        public void MyBind()
        {
            DataTable dt = bll.GetTableField("ZL_UserBase", SqlHelper.ConnectionString);
            DataTable dtubf = bbf.Select_All();
            dtubf.DefaultView.Sort = " OrderId asc";
            StringBuilder sb = new StringBuilder();
            if (dtubf.Rows.Count > 0)
            {
                for (int i = 0; i < dtubf.Rows.Count; i++)
                {
                    if (dtubf.Rows[i]["FieldName"] != null)
                    {
                        sb.Append("'" + dtubf.Rows[i]["FieldName"].ToString() + "',");
                    }
                }
            }
            string str = sb.ToString();
            if (str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            if (str.Length > 0)
            {
                DataRow[] drs = dt.Select(" fieldname not in (" + str + ")");
                DataTable dts = dt.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    dts.ImportRow((DataRow)drs[i]);
                }
                this.RepSystemModel.DataSource = dts;
                this.RepSystemModel.DataBind();
            }
            else
            {
                this.RepSystemModel.DataSource = dt;
                this.RepSystemModel.DataBind();
            }

            this.RepModelField.DataSource = dtubf;
            this.RepModelField.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        public string GetStyleTrue(string flag)
        {
            if (DataConverter.CBool(flag))
            {
                return "<font color=\"green\">√</font>";
            }
            else
            {
                return "<font color=\"red\">×</font>";
            }
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            //上移
            if (e.CommandName == "UpMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_UserBaseField mubf = this.bbf.GetSelect(Id);
                M_UserBaseField FieldPre = this.bbf.GetPreField(mubf.OrderId);

                if (mubf.OrderId != FieldPre.OrderId)
                {
                    int CurrOrder = mubf.OrderId;
                    mubf.OrderId = FieldPre.OrderId;
                    FieldPre.OrderId = CurrOrder;
                    this.bbf.GetUpdate(mubf);
                    this.bbf.GetUpdate(FieldPre);
                }
            }
            //下移
            if (e.CommandName == "DownMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_UserBaseField mubf = this.bbf.GetSelect(Id);
                M_UserBaseField FieldNext = this.bbf.GetNextField(mubf.OrderId);
                if (mubf.OrderId != FieldNext.OrderId)
                {
                    int CurrOrder = mubf.OrderId;
                    mubf.OrderId = FieldNext.OrderId;
                    FieldNext.OrderId = CurrOrder;
                    this.bbf.GetUpdate(mubf);
                    this.bbf.GetUpdate(FieldNext);
                }
            }
            //删除
            if (e.CommandName == "Delete")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                bbf.GetDelete(Id);
            }
            MyBind();
        }
        public string GetFieldType(string TypeName)
        {
            return bmf.GetFieldType(TypeName);
        }
    }
}