using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Message;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Message;

public partial class Guest_Baike_BKEditor : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Baike bkBll = new B_Baike();
    B_BaikeEdit editBll = new B_BaikeEdit();
    private string Tittle { get { return HttpUtility.UrlDecode((Request.QueryString["tittle"] ?? "")).Replace(" ",""); } }
    //admin
    private string Mode { get { return Request.QueryString["mode"] ?? ""; } }
    //传入ID才可修改最新词条
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    private int EditID { get { return DataConverter.CLng(Request.QueryString["EditID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            switch (Mode)
            {
                case "admin":
                    B_Admin.CheckIsLogged();
                    break;
                case "user":
                default://是否限定创建权限,用户所在组是否拥有创建权限
                    B_User.CheckIsLogged(Request.RawUrl);
                    if (!bkBll.AuthCheck(GuestConfig.GuestOption.BKOption.CreateBKGroup, mu.GroupID)) { function.WriteErrMsg("你没有创建或编辑词条的权限"); }
                    break;
            }
            MyBind();
        }
    }
    private void MyBind()
    {
        M_Baike bkMod = GetModel();
        bke_title.InnerText = Tittle;
        if (bkMod.ID > 0)
        {
            bke_title.InnerText = bkMod.Tittle;
            Contents_T.Text = bkMod.Contents;
            Brief_T.Text = bkMod.Brief;
            //Contents_T.Text = SafeSC.ReadFileStr("/Test/content.txt");
            pic_img.Src = bkMod.BriefImg;
            info_hid.Value = bkMod.Extend;
            class_hid.Value = bkMod.Classification;
            class_sp.InnerText = "(" + bkMod.Classification + ")";
            refence_hid.Value = bkMod.Reference;
            BType_T.Text = bkMod.Btype;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Baike bkMod = GetModel();
        M_BaikeEdit editMod = new M_BaikeEdit();
        bkMod.Contents = Contents_T.Text;
        bkMod.Brief = Brief_T.Text;
        bkMod.BriefImg = pic_hid.Value;
        bkMod.Extend = info_hid.Value;
        bkMod.Reference = refence_hid.Value;
        bkMod.UpdateTime = DateTime.Now;
        bkMod.Classification = class_hid.Value;
        //bkMod.Editnumb++;
        bkMod.Btype = BType_T.Text.Replace(" ", "");
        if (bkMod.ID < 1)//新建百科(新百科也需要管理员审核)
        {
            bkMod.Status = (int)ZLEnum.ConStatus.UnAudit;
            bkMod.UserId = mu.UserID;
            bkMod.UserName = mu.UserName;
            bkMod.Tittle = Tittle;
            bkMod.ID = bkBll.insert(bkMod);
            editBll.ConverToEdit(editMod, bkMod, "all");
            editBll.Insert(editMod);
            //function.WriteSuccessMsg("创建百科成功", "/Guest/Baike/Details.aspx?ID=" + bkMod.ID);
        }
        else if (EditID > 0) //修改自己填的未审核百科
        {
            editMod = editBll.SelReturnModel(EditID);
            editBll.ConverToEdit(editMod, bkMod, "all");
            editBll.UpdateByID(editMod);
        }
        else if (Mid > 0) //存为新的版本,待审核,并跳至用户中心处
        {
            editBll.ConverToEdit(editMod, bkMod);
            editMod.Status = (int)ZLEnum.ConStatus.UnAudit;
            editMod.UserId = mu.UserID;
            editMod.UserName = mu.UserName;
            editMod.OldID = Mid;
            editBll.Insert(editMod);
        }
        else { function.WriteErrMsg("保存条件不正确"); }
        function.WriteSuccessMsg("操作成功,请等待管理员审核", "/User/Guest/BaikeContribution.aspx");
    }
    private M_Baike GetModel()
    {
        M_Baike bkMod = null;
        M_UserInfo mu = buser.GetLogin();
        if (Mid < 1 && EditID < 1)
        {
            if (string.IsNullOrEmpty(Tittle)) { function.WriteErrMsg("未指定词条标题"); }
            bkMod = new M_Baike();
        }
        else if (Mid > 0) { bkMod = bkBll.SelReturnModel(Mid); }
        else if (EditID > 0)
        {
            bkMod = editBll.SelReturnModel(EditID);
            if (mu.UserID != bkMod.UserId) { function.WriteErrMsg("你无权修改该版本词条"); }
            if (Mode.Equals("admin")) //管理员可不限制操作
            {

            }
            else
            {
                if (bkMod.Status == 1) { function.WriteErrMsg("该版本已审核,无法再次修改,<a href='BKEditor.aspx?ID=" + bkBll.SelModelByFlow(bkMod.Flow).ID + "'>创建新的版本</a>"); }
            }
        }
        else if (Mid > 0 && EditID > 0) { function.WriteErrMsg("传参错误,指向不明确"); }
        else { function.WriteErrMsg("错误,未匹配的版本"); }
        return bkMod;
    }
}