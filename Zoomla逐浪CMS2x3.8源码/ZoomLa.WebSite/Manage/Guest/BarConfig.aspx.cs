using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Manage_I_Guest_BarConfig : System.Web.UI.Page
{
    //登录访问,则未登录,只能看到省略的话,不能看到全部
    //指定用户,则列表也不可看
    public int CateID { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
    public string GType { get { return string.IsNullOrEmpty(Request.QueryString["GType"]) ? "0" : Request.QueryString["GType"]; } }
    string vpath = SiteConfig.SiteOption.UploadDir + "/Admin/Bar/";
    M_GuestBookCate cateMod = new M_GuestBookCate();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "BarManage");
            DBBind();
            if (CateID > 0)
            {
                MyBind();
                AddBar_Btn.Text = "修改栏目";
            }
            else
            {
                GType_Rad.SelectedValue = GType;
                if(GType.Equals("0"))
                    function.Script(this, "HideBarSet();");
            }
        }
    }
    public void DBBind() 
    {
        DataTable dt = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar);
        PCate_ul.InnerHtml += string.Format("<li role=\"{1}\" onclick=\"selectCate(this)\"><a role=\"menuitem\" tabindex=\"1\" href=\"javascript:;\">{0}</a></li>", "不指定", 0);
        BindItem(dt);
        selected_Hid.Value = Request.QueryString["PID"];
    }
    public void BindItem(DataTable dt, int pid = 0, int layer = 0)
    {
        DataRow[] drs = dt.Select("ParentID=" + pid);
        string pre = layer > 0 ? "{0}<img src='/Images/TreeLineImages/t.gif' />" : "";
        string nbsp = "";
        for (int i = 0; i < layer; i++)
        {
            nbsp += "&nbsp;&nbsp;&nbsp;";
        }
        pre = string.Format(pre,nbsp);
        foreach (DataRow dr in drs)
        {
            PCate_ul.InnerHtml += string.Format("<li role=\"{1}\" onclick=\"selectCate(this)\"><a role=\"menuitem\" tabindex=\"1\" href=\"javascript:;\">{0}</a></li>", pre + dr["CateName"].ToString(), dr["CateID"].ToString());
            BindItem(dt, Convert.ToInt32(dr["CateID"]), (layer + 1));
        }
    }
    public void MyBind() 
    {
        cateMod = cateBll.SelReturnModel(CateID);
        txtCateName.Text = cateMod.CateName;
        selected_Hid.Value = cateMod.ParentID.ToString();
        NeedLog.SelectedValue = cateMod.NeedLog.ToString();
        PostAuth.SelectedValue = cateMod.PostAuth.ToString();
        GType_Rad.SelectedValue = cateMod.GType.ToString();
        SenderScore_T.Text = cateMod.SendScore.ToString();
        ReplyScore_T.Text = cateMod.ReplyScore.ToString();
        PostType.SelectedValue = DataConverter.CLng(cateMod.PermiBit).ToString();
        ZipImgSize_T.Text = cateMod.ZipImgSize.ToString();
        IsPlat_T.Text = cateMod.IsPlat.ToString();
        ImageInfo_T.Text = cateMod.BarImage;
        cateMod.Desc = BarDesc_T.Text;
        IsCheck_Ra.Checked = cateMod.Status > 1;
        if (cateMod.Status == 2||cateMod.Status==3)
            CheckOpt_Ra.Checked = cateMod.Status == 3;
        DataTable userDT = buser.SelectUserByIds(cateMod.BarOwner);
        if (userDT != null && userDT.Rows.Count > 0)
        {
            userDT = userDT.DefaultView.ToTable(true, "UserID", "UserName");
            BarOwner_Json_T.Value = JsonConvert.SerializeObject(userDT);
        }
        BarOwner_Hid.Value = cateMod.BarOwner;
        BarOption baroption = GuestConfig.GuestOption.BarOption.Find(v => v.CateID == CateID);
        if (baroption!=null)
        {
            UserTime_T.Text = baroption.UserTime.ToString();
            SendTime_T.Text = baroption.SendTime.ToString();
        }
        if (cateMod.GType != (int)M_GuestBookCate.TypeEnum.PostBar)
            function.Script(this, "HideBarSet();");
    }
    protected void AddBar_Btn_Click(object sender, EventArgs e)
    {
        if (CateID > 0)
        {
            cateMod = cateBll.SelReturnModel(CateID);
        }
        cateMod.CateName = txtCateName.Text;
        cateMod.NeedLog = Convert.ToInt32(NeedLog.SelectedValue);
        cateMod.PostAuth = Convert.ToInt32(PostAuth.SelectedValue);
        cateMod.ZipImgSize = DataConverter.CLng(ZipImgSize_T.Text);
        cateMod.BarImage = ImageInfo_T.Text;
        cateMod.GType = Convert.ToInt32(GType_Rad.SelectedValue);
        cateMod.ParentID = Convert.ToInt32(selected_Hid.Value);
        cateMod.BarOwner = BarOwner_Hid.Value;
        cateMod.PermiBit = PostType.SelectedValue;
        cateMod.Desc = BarDesc_T.Text;
        cateMod.IsPlat = DataConverter.CLng(IsPlat_T.Text);
        cateMod.SendScore = Convert.ToInt32(SenderScore_T.Text);
        cateMod.ReplyScore = Convert.ToInt32(ReplyScore_T.Text);
        if (IsCheck_Ra.Checked)
            cateMod.Status = CheckOpt_Ra.Checked?3:2;
        else
            cateMod.Status = 1;
        string returnurl = "";
        if (GType.Equals("0"))
            returnurl = "GuestManage.aspx";
        else
            returnurl = "GuestCateMana.aspx?Type=1";
        GuestConfigInfo guestinfo = GuestConfig.GuestOption;
        GuestConfig config = new GuestConfig();
        if (CateID <= 0 )
        {
            int cateid = cateBll.Insert(cateMod);
            guestinfo.BarOption.Add(new BarOption() { CateID = cateid, UserTime = Convert.ToInt32(UserTime_T.Text),SendTime= Convert.ToInt32(SendTime_T.Text)});
            config.Update(guestinfo);
            function.WriteSuccessMsg("添加成功!", returnurl);
        }
        else if (cateBll.Update(cateMod))
        {
            BarOption baroption = guestinfo.BarOption.Find(v => v.CateID == CateID);
            if (baroption==null)
                guestinfo.BarOption.Add(new BarOption() { CateID = CateID, UserTime = Convert.ToInt32(UserTime_T.Text), SendTime = Convert.ToInt32(SendTime_T.Text) });
            else
            {
                baroption.UserTime = Convert.ToInt32(UserTime_T.Text);
                baroption.SendTime = Convert.ToInt32(SendTime_T.Text);
            }
            config.Update(guestinfo);
            function.WriteSuccessMsg("修改成功!", returnurl);
        }
    }
}