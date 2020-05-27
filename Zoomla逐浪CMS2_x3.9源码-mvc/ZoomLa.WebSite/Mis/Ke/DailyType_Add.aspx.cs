using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.Ke
{
    public partial class DailyType_Add : System.Web.UI.Page
    {
        B_MisInfo misBll = new B_MisInfo();
        B_User buser = new B_User();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            if (Mid > 0)
            {
                M_MisInfo misMod = misBll.SelReturnModel(Mid);
                Title_T.Text = misMod.Title;
                Type_DP.SelectedValue = misMod.Type.ToString();
                IsShare_RB.SelectedValue = misMod.IsShare.ToString();
                IsElit_RB.SelectedValue = misMod.IsElit.ToString();
                IsWarn_RB.SelectedValue = misMod.IsWarn.ToString();
                Level_DP.SelectedValue = misMod.Level.ToString();
                Content_T.Text = misMod.Content;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_MisInfo misMod = new M_MisInfo();
            if (Mid > 0) { misMod = misBll.SelReturnModel(Mid); }
            misMod.Title = Title_T.Text;
            misMod.Type = DataConvert.CLng(Type_DP.SelectedValue);
            misMod.IsShare = IsShare_RB.SelectedValue;
            misMod.IsElit = DataConvert.CLng(IsElit_RB.SelectedValue);
            misMod.IsWarn = IsWarn_RB.SelectedValue;
            misMod.Level = DataConvert.CLng(Level_DP.SelectedValue);
            misMod.Content = Content_T.Text;
            if (Mid > 0)
            {
                misBll.UpdateByID(misMod);
            }
            else
            {
                M_UserInfo mu = buser.GetLogin();
                misMod.Inputer = mu.UserName;
                misMod.Status = 0;
                misMod.MID = mu.UserID;
                misBll.insert(misMod);
            }
            function.Script(this, "alert('操作成功!');HideMe();");
        }
    }
}