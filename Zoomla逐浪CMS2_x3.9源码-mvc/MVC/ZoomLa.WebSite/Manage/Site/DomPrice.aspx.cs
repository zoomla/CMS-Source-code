using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Site
{
    public partial class DomPrice : System.Web.UI.Page
    {

        B_IDC_DomainPrice domPriceBll = new B_IDC_DomainPrice();
        M_IDC_DomPrice domPriceModel = new M_IDC_DomPrice();
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li class='active'>域名定价</li>");
                domNameDP.DataSource = domPriceBll.GetNotDefindSuffix();//绑定后缀名列表
                domNameDP.DataTextField = "DomName";
                domNameDP.DataValueField = "DomName";
                domNameDP.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))//Edit
                {
                    BindEditData(Request.QueryString["ID"]);
                }
                else
                {
                    DomPriceDataBind();
                }
            }
        }
        //----Tool
        public string GetStatus(object o)
        {
            string result = "<span style='color:red'>已禁用</span>";
            if (o.ToString().Equals("1"))
                result = "<span style='color:green'>已启用</span>";
            return result;
        }
        public string GetInterFace(object o)
        {
            string result = "新网";
            return result;
        }
        //----
        private void BindEditData(string id)
        {
            viewDiv.Visible = false;
            addDiv.Visible = true;
            tab2AddBtn.Text = "修改";
            domPriceModel = domPriceBll.SelByID(id);
            if (domPriceModel != null)
            {
                addSpan.Visible = false;
                domNameL.Visible = true;
                domNameL.Text = domPriceModel.DomName;
                tab2DomPrice.Text = domPriceModel.DomPrice.ToString();
                detailText.Text = domPriceModel.ProDetail;
                statusChk.Checked = domPriceModel.Status.ToString().Equals("1") ? true : false;
            }
            else
            {
                function.WriteErrMsg("域名信息不存在");
            }
        }
        private void DomPriceDataBind(string keyWord = "")
        {
            viewDiv.Visible = true;
            addDiv.Visible = false;
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(keyWord))
            {
                dt = domPriceBll.Sel(keyWord);
            }
            else
            {
                dt = domPriceBll.Sel();
            }
            domPriceEGV.DataSource = dt;
            domPriceEGV.DataBind();
        }
        //添加与修改
        protected void tab2AddBtn_Click(object sender, EventArgs e)
        {
            string name = domNameDP.SelectedValue;
            string price = tab2DomPrice.Text.Trim();
            string id = Request.QueryString["ID"];
            if (name.IndexOf(".") != 0)
                name = "." + name;
            if (!IsNumber(price))
            {
                function.WriteErrMsg("价格不正确，必须为数字");
            }

            if (string.IsNullOrEmpty(id))//添加
            {
                domPriceModel.DomName = name;
                domPriceModel.DomPrice = Convert.ToDecimal(price);
                domPriceModel.ProDetail = detailText.Text.Trim();
                domPriceModel.IFSupplier = 1;
                domPriceModel.Status = statusChk.Checked ? 1 : 0;
                domPriceModel.CreateDate = DateTime.Now;
                if (domPriceBll.Insert(domPriceModel) < 0) function.WriteErrMsg("有同名商品存在,取消添加!!");
            }
            else//修改
            {
                domPriceModel = domPriceBll.SelByID(id);
                domPriceModel.DomPrice = Convert.ToDecimal(price);
                domPriceModel.ProDetail = detailText.Text.Trim();
                domPriceModel.IFSupplier = 1;
                domPriceModel.Status = statusChk.Checked ? 1 : 0;
                domPriceBll.UpdateByID(domPriceModel);
            }
            Response.Redirect("DomPrice.aspx");
        }
        protected void domPriceEGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                //case "Edit2":
                //    domPriceEGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                //    break;
                //case "Renewals":
                //    string[] s = e.CommandArgument.ToString().Split(':');
                //    Update(DataConvert.CLng(s[0]), s[1]);
                //    domPriceEGV.EditIndex = -1;
                //    break;
                case "Del2":
                    string id = e.CommandArgument as string;
                    if (this.domPriceBll.DelByID(id))
                        function.Alert("删除成功！");
                    else
                        function.Alert("删除失败！");
                    DomPriceDataBind();
                    break;
                default:
                    break;
            }
            DomPriceDataBind();
        }
        //protected void Update(int rowNum, string id)
        //{
        //    GridViewRow gr = domPriceEGV.Rows[rowNum];
        //    DomName = ((TextBox)gr.FindControl("eDomName")).Text.Trim();
        //    DomPrice = ((TextBox)gr.FindControl("eDomPrice")).Text.Trim();
        //    if (IsNumber(DomPrice))
        //    {
        //        this.domPriceBll.UpdatePriceByID(id, DomPrice);
        //    }
        //    else
        //    {
        //        function.WriteErrMsg("有同名数据存在或价格输入不正确");
        //    }
        //}
        protected void domPriceEGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            domPriceEGV.EditIndex = -1;
            DomPriceDataBind();
        }
        //是否为数字
        private bool IsNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex objNotNumberPattern = new System.Text.RegularExpressions.Regex("[^0-9.-]");
            System.Text.RegularExpressions.Regex objTwoDotPattern = new System.Text.RegularExpressions.Regex("[0-9]*[.][0-9]*[.][0-9]*");
            System.Text.RegularExpressions.Regex objTwoMinusPattern = new System.Text.RegularExpressions.Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            System.Text.RegularExpressions.Regex objNumberPattern = new System.Text.RegularExpressions.Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber) && !objTwoMinusPattern.IsMatch(strNumber) && objNumberPattern.IsMatch(strNumber);
        }
        protected void mimeEGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            domPriceEGV.PageIndex = e.NewPageIndex;
            DomPriceDataBind();
        }
        //搜索
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DomPriceDataBind(searchText.Text.Trim());
        }
        //每页显示100条数据,当页更改
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < domPriceEGV.Rows.Count; i++)
            {
                GridViewRow dr = domPriceEGV.Rows[i];
                domPriceBll.UpdatePriceByID(dr.Cells[0].Text, (dr.FindControl("DomPriceText") as TextBox).Text);
            }
            DomPriceDataBind();
        }
        //显示添加
        protected void disAddBtn_Click(object sender, EventArgs e)
        {
            viewDiv.Visible = false;
            addDiv.Visible = true;
            addSpan.Visible = true;
            domNameL.Visible = false;
            tab2AddBtn.Text = "添加";
        }
    }
}