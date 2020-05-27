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
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Page
{
    public partial class UserModelField : CustomerPageAction
    {
        private B_ModelField bll = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "PageModelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string mid = base.Request.QueryString["ModelID"];
                if (string.IsNullOrEmpty(mid))
                {
                    function.WriteErrMsg("没有指定模块ID");
                }
                this.ViewState["ModelID"] = mid;
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li><a href='UserModelManage.aspx'>黄页申请设置</a></li><li class='active'>当前申请样式:" + LModelName.Text + "<a id='ShowLink' href='javascript:ShowList()'>[显示所有字段]</a><a href='AdduserField.aspx?ModelID=" + Request.QueryString["ModelID"] + "'>[添加字段]</a></li>");
        }
        public void MyBind()
        {
            B_Model bll1 = new B_Model();
            int ModelID = DataConverter.CLng(this.ViewState["ModelID"].ToString());
            M_ModelInfo modeli = bll1.GetModelById(ModelID);
            this.Label1.Text = "当前表:" + modeli.TableName;
            this.LModelName.Text = modeli.ModelName;
            this.TxtTemplate.Text = modeli.ContentModule;
            this.RepSystemModel.DataSource = bll.GetSysUserField();
            this.RepSystemModel.DataBind();
            this.RepModelField.DataSource = bll.GetModelFieldListall(ModelID);
            this.RepModelField.DataBind();
        }
        public string GetFieldType(string TypeName)
        {
            return bll.GetFieldType(TypeName);
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
        protected void SetTemplate(object sender, EventArgs e)
        {
            B_Model bll1 = new B_Model();
            int ModelID = DataConverter.CLng(this.ViewState["ModelID"].ToString());
            bll1.UpdateTemplate(this.TxtTemplate.Text, ModelID);
            function.WriteSuccessMsg("设定模板成功", "UserModelField.aspx?ModelID=" + ModelID.ToString());
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int ModelID = DataConverter.CLng(this.ViewState["ModelID"]);
            if (e.CommandName == "UpMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);

                M_ModelField Fieldinfo = this.bll.GetModelByID(ModelID.ToString(), Id);
                if (Fieldinfo.OrderID != this.bll.GetMinOrder(Fieldinfo.ModelID))
                {
                    M_ModelField FieldPre = bll.GetPreField(Fieldinfo.ModelID, Fieldinfo.OrderID);
                    int CurrOrder = Fieldinfo.OrderID;
                    Fieldinfo.OrderID = FieldPre.OrderID;
                    FieldPre.OrderID = CurrOrder;
                    //this.bll.UpdateByID(Fieldinfo);
                    //this.bll.UpdateByID(FieldPre);
                    this.bll.UpdateOrder(Fieldinfo, FieldPre);
                    //this.bll.UpdateOrder(FieldPre);
                }
            }
            if (e.CommandName == "DownMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_ModelField Fieldinfo = this.bll.GetModelByID(ModelID.ToString(), Id);
                if (Fieldinfo.OrderID != this.bll.GetMaxOrder(Fieldinfo.ModelID))
                {
                    M_ModelField FieldPre = this.bll.GetNextField(Fieldinfo.ModelID, Fieldinfo.OrderID);
                    int CurrOrder = Fieldinfo.OrderID;
                    Fieldinfo.OrderID = FieldPre.OrderID;
                    FieldPre.OrderID = CurrOrder;
                    //this.bll.UpdateByID(Fieldinfo);
                    //this.bll.UpdateByID(FieldPre);
                    this.bll.UpdateOrder(Fieldinfo, FieldPre);
                }
            }
            if (e.CommandName == "Delete")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                bll.DelByFieldID(Id);
            }
            MyBind();
        }
    }
}