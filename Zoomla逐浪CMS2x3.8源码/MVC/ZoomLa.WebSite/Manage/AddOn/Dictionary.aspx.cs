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
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class Dictionary : System.Web.UI.Page
    {
        B_DataDictionary dictBll = new B_DataDictionary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int DicCateID = string.IsNullOrEmpty(Request.QueryString["CateID"]) ? 0 : DataConverter.CLng(Request.QueryString["CateID"]);
                if (DicCateID <= 0)
                    function.WriteErrMsg("缺少字典分类ID参数！", "../AddOn/DictionaryManage.aspx");
                this.HdnDicCateID.Value = DicCateID.ToString();
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='DictionaryManage.aspx'>数据字典</a></li><li class='active'>" + B_DataDicCategory.GetDicCate(DataConverter.CLng(string.IsNullOrEmpty(Request.QueryString["CateID"]) ? 0 : DataConverter.CLng(Request.QueryString["CateID"]))).CategoryName + "</li>");
        }
        public void DataBind(string key = "")
        {
            Egv.DataSource = B_DataDictionary.GetDicListbyCate(DataConverter.CLng(this.HdnDicCateID.Value));
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                dictBll.Del(DataConverter.CLng(Id));
                DataBind();
            }
            if (e.CommandName == "Edit1")
            {
                string Id = e.CommandArgument.ToString();
                M_Dictionary info = B_DataDictionary.GetModel(DataConverter.CLng(Id));
                this.txtDicName.Text = info.DicName;
                this.HdnDicID.Value = Id;
                this.btnSave.Text = "修改";
            }
        }
        public string GetUsedFlag(string tType)
        {
            bool t = DataConverter.CBool(tType);
            string re = DataConverter.CBool(tType) ? "<font color=green>√</font>" : "<font color=red>×</font>";
            return re;
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] ids = Request.Form["idchk"].Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    dictBll.Del(Convert.ToInt32(ids[i]));
                }
                DataBind();
            }
        }
        protected void btnSetUsed_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string ids = Request.Form["idchk"];
                B_DataDictionary.SetUsedByArr(ids, true);
                DataBind();
            }
        }
        protected void btnSetAllUsed_Click(object sender, EventArgs e)
        {
            B_DataDictionary.SetAllUsed(DataConverter.CLng(this.HdnDicCateID.Value));
            DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtDicName.Text.Trim()))
            {
                int DicCateID = DataConverter.CLng(this.HdnDicID.Value);

                if (DicCateID > 0)
                {
                    M_Dictionary info = B_DataDictionary.GetModel(DicCateID);
                    info.DicName = this.txtDicName.Text.Trim();
                    B_DataDictionary.Update(info);
                }
                else
                {
                    M_Dictionary info = new M_Dictionary();
                    info.DicID = 0;
                    info.DicCate = DataConverter.CLng(this.HdnDicCateID.Value);
                    info.DicName = this.txtDicName.Text.Trim();
                    info.IsUsed = true;
                    B_DataDictionary.AddDic(info);
                }
                this.txtDicName.Text = "";
                this.HdnDicID.Value = "0";
                this.btnSave.Text = "添加";
                DataBind();
            }
        }
        protected void btnSetUnUsed_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                B_DataDictionary.SetUsedByArr(Request.Form["idchk"], false);
                DataBind();
            }
        }
    }
}