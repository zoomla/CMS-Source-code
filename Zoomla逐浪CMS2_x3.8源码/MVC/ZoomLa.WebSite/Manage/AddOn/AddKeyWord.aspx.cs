using System;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class AddKeyWord : System.Web.UI.Page
    {
        B_KeyWord keyBll = new B_KeyWord();
        public int Mid {get{return DataConverter.CLng(Request.QueryString["ID"]);} }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='KeyWordManage.aspx'>关键字集</a></li><li class='active'>关键字</li>");
            }

        }
        private void MyBind() 
        {
            if(Mid>0)
            {
                M_KeyWord keyMod=keyBll.SelReturnModel(Mid);
                KeyWord_T.Text=keyMod.KeywordText;
                TxtPriority.Text=keyMod.Priority.ToString();
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_KeyWord keyMod = new M_KeyWord();
            if (Mid > 0) { keyMod = keyBll.SelReturnModel(Mid); }
            else 
            {
                if (keyBll.IsExist(KeyWord_T.Text)) {function.WriteErrMsg("该关键词已存在"); }
            }
            keyMod.KeywordText = KeyWord_T.Text.Trim();
            keyMod.KeywordType = 1;
            keyMod.Priority = DataConverter.CLng(TxtPriority.Text.ToString());
            if (keyMod.KeyWordID > 0) {keyBll.UpdateByID(keyMod); }
            else {keyBll.insert(keyMod); }
            function.WriteSuccessMsg("操作成功","KeyWordManage.aspx");
        }
    }
}