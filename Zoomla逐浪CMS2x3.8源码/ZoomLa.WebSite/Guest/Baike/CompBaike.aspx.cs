using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.Message;
using System.Data;

public partial class Guest_Baike_CompBaike : System.Web.UI.Page
{
    B_BaikeEdit editBll = new B_BaikeEdit();
    B_Baike bkBll = new B_Baike();
    B_User buser = new B_User();
    public int EditID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public int OldID { get { return DataConverter.CLng(Request.QueryString["oldid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        if (EditID > 0)
        {
            M_BaikeEdit editMod = editBll.SelReturnModel(EditID);
            M_Baike bkMod = bkBll.SelModelByFlow(editMod.Flow);
            curcode.InnerHtml = editMod.Contents;
            CurDate_L.Text = editMod.AddTime.ToString();
            CurSelBaike_Li.Text = "<a href='Details.aspx?EditID=" + editMod.ID + "' target='_blank'>查看</a>";
            CurUserName_L.Text = buser.SelReturnModel(editMod.UserId).UserName;
            CurWhy_L.Text = editMod.UserRemind;
            PreDate_L.Text = bkMod.AddTime.ToString();
            PreSelBaike_Li.Text = "<a href='Details.aspx?id=" + bkMod.ID + "' target='_blank'>查看</a>";
            PreUserName_L.Text = bkMod.UserName;
            precode.InnerHtml = bkMod.Contents;
        }
    }
}