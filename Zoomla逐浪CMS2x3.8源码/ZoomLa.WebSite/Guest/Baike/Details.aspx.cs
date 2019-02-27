using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.BLL.Message;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Common;

public partial class Guestbook_BkDetails : System.Web.UI.Page
{
    B_Baike bkBll = new B_Baike();
    B_BaikeEdit editBll = new B_BaikeEdit();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
    //用于预览
    public int EditID { get { return DataConvert.CLng(Request.QueryString["editid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void MyBind()
    {
        M_Baike bkMod = GetModel();
        Contents_div.InnerHtml = bkMod.Contents;
        tittle_sp.InnerText = bkMod.Tittle;
        if (!string.IsNullOrEmpty(bkMod.Classification)) { cate_sp.InnerText = "( " + bkMod.Classification + " )"; }
        Brief_L.Text = bkMod.Brief;
        if (string.IsNullOrEmpty(bkMod.BriefImg)) { pic_div.Visible = false; }
        else { pic_img.Src = bkMod.BriefImg; }
        info_hid.Value = bkMod.Extend;
        refence_hid.Value = bkMod.Reference;
        //词条标签
        string[] btypeArr = bkMod.Btype.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string item in btypeArr)
        {
            BType_L.Text += "<a href='/Guest/Baike/Search.aspx?btype=" + HttpUtility.UrlEncode(item) + "' class='btype_a' target='_blank'>" + item + "</a>";
        }
        if (Mid > 0) { edit_a.HRef = "BKEditor.aspx?ID=" + Mid; }
        else { edit_a.HRef = "BKEditor.aspx?EditID=" + EditID; }
    }
    private M_Baike GetModel()
    {
        M_UserInfo mu = buser.GetLogin();
        M_Baike model = null;
        if (Mid > 0)
        {
            model = bkBll.SelReturnModel(Mid);
            if (model.Status == (int)ZLEnum.ConStatus.UnAudit) { function.WriteErrMsg("该信息尚未审核"); }
        }
        else if (EditID > 0)
        {
            model = editBll.SelReturnModel(EditID);
            //if (model.UserId != mu.UserID && model.Status == (int)ZLEnum.ConStatus.UnAudit) { function.WriteErrMsg("非创建人无权预览该词条"); }
        }
        if (model == null) { function.WriteErrMsg("词条不存在"); }
        return model;
    }
}