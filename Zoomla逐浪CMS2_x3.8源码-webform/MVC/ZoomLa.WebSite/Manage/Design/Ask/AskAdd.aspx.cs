using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Design.Ask
{
    public partial class AskAdd : System.Web.UI.Page
    {
        B_Design_Ask askBll=new B_Design_Ask();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_Design_Ask askMod = askBll.SelReturnModel(Mid);
                Title_T.Text = askMod.Title;
                PreViewImg_UP.FileUrl = askMod.PreViewImg;
                Remind_T.Text = askMod.Remind;
                EndDate_T.Text = askMod.EndDate.ToString("yyyy/MM/dd");
            }
            else { EndDate_T.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd"); }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Design_Ask askMod = new M_Design_Ask();
            if (Mid > 0) { askMod = askBll.SelReturnModel(Mid); }
            askMod.Title = Title_T.Text;
            if (!askMod.PreViewImg.Equals(PreViewImg_UP.FVPath))
            {
                askMod.PreViewImg = PreViewImg_UP.SaveFile();
            }
            askMod.Remind = Remind_T.Text;
            askMod.EndDate = DataConvert.CDate(EndDate_T.Text);
            if (askMod.ID > 0) { askBll.UpdateByID(askMod); }
            else { askBll.Insert(askMod); }
            string url = CustomerPageAction.customPath2 + "design/ask/AskList.aspx";
            function.WriteErrMsg("操作成功", url);
        }
    }
}