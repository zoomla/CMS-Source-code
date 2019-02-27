namespace ZoomLaCMS.Common.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    public partial class SelStruct : System.Web.UI.Page
    {
        /*
      * 部门选择
      * SelStruct用于部门下用户选择
      * 支持单选和多选,返回Json结果
      */
        B_Structure strBll = new B_Structure();
        string hasChild, noChild;
        public string CName { get { return Request.QueryString["CName"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            hasChild = "{2}<label><input name='nodeChk' type='checkbox' data-name='{1}' value='{0}' onclick='ChkChild(this);'><i class='fa fa-folder' style='color:#337AB7'></i></label><a href='javascript:;' id='a{0}' class='list1' onclick='ShowChild();'><span class='list_span'>{1}</span></a>";
            noChild = "{2}<label><input name='nodeChk' type='checkbox' data-name='{1}' value='{0}'><i class='fa fa-file-text' style='color:#337AB7'></i></label><a href='javascript:;'>{1}</a>";
            DataTable dt = strBll.Sel();
            NodeHtml_Lit.Text = "<ul class='tvNav list-unstyled'><li><input id='AllCheck' type='checkbox' onclick='checkAll()'><a class='list1' id='a0' href='javascript:;' ><span class='fa fa-list'></span><span class='list_span'>全部部门</span></a>" + B_Node.GetLI(dt, hasChild, noChild) + "</li></ul>";
        }
    }
}