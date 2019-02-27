namespace ZoomLa.WebSite.Manage
{
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

    public partial class ModelField : System.Web.UI.Page
    {
        private B_ModelField bll = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("ModelEdit"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            string mid = base.Request.QueryString["ModelID"];
            if (string.IsNullOrEmpty(mid))
            {
                function.WriteErrMsg("没有指定模块ID");
            }
            this.ViewState["ModelID"] = mid;
            if (!this.Page.IsPostBack)
            {
                DataBindList();
            }
        }
        public void DataBindList()
        {
            B_Model bll1 = new B_Model();
            int ModelID;
            ModelID = DataConverter.CLng(this.ViewState["ModelID"].ToString());
            M_ModelInfo modeli = bll1.GetModelById(ModelID);
            this.Literal1.Text = modeli.ModelName;
            this.LModelName.Text = this.Literal1.Text;
            this.TxtTemplate.Text = modeli.ContentModule;
            this.RepSystemModel.DataSource = bll.GetSysteFieldList();
            this.RepSystemModel.DataBind();
            this.RepModelField.DataSource = bll.GetModelFieldList(ModelID);
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
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_ModelField Fieldinfo = this.bll.GetModelByID(Id);
                if (Fieldinfo.OrderID != this.bll.GetMinOrder(Fieldinfo.ModelID))
                {
                    M_ModelField FieldPre = this.bll.GetPreField(Fieldinfo.ModelID, Fieldinfo.OrderID);
                    int CurrOrder = Fieldinfo.OrderID;
                    Fieldinfo.OrderID = FieldPre.OrderID;
                    FieldPre.OrderID = CurrOrder;
                    this.bll.UpdateOrder(Fieldinfo);
                    this.bll.UpdateOrder(FieldPre);
                }
            }
            if (e.CommandName == "DownMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_ModelField Fieldinfo = this.bll.GetModelByID(Id);
                if (Fieldinfo.OrderID != this.bll.GetMaxOrder(Fieldinfo.ModelID))
                {
                    M_ModelField FieldPre = this.bll.GetNextField(Fieldinfo.ModelID, Fieldinfo.OrderID);
                    int CurrOrder = Fieldinfo.OrderID;
                    Fieldinfo.OrderID = FieldPre.OrderID;
                    FieldPre.OrderID = CurrOrder;
                    this.bll.UpdateOrder(Fieldinfo);
                    this.bll.UpdateOrder(FieldPre);
                }
            }
            if (e.CommandName == "Delete")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                int ModelID;
                ModelID = DataConverter.CLng(this.ViewState["ModelID"].ToString());
                B_Model bll1 = new B_Model();
                string tablename = bll1.GetModelById(ModelID).TableName;
                M_ModelField fieldinfo = this.bll.GetModelByID(Id);
                if (fieldinfo.FieldType == "PicType" || fieldinfo.FieldType == "FileType")
                {
                    string[] Setting = fieldinfo.Content.Split(new char[] { ',' });
                    bool chk = DataConverter.CBool(Setting[0].Split(new char[] { '=' })[1]);
                    string fieldname = Setting[1].Split(new char[] { '=' })[1];
                    if (chk && fieldname != "")
                    {
                        this.bll.DelSubField(tablename, fieldname);
                    }
                }
                this.bll.Del(Id, tablename);
            }
            DataBindList();
        }
    }
}