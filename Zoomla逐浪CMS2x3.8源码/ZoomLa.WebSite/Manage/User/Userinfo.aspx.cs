using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

public partial class manage_User_Userinfo : CustomerPageAction
{
    B_Model bm = new B_Model();
    B_ModelField bmf = new B_ModelField();
    public string tabTitles = "";
    public string tabs = "";

    B_User buser = new B_User();
    B_Permission perBll = new B_Permission();
    B_UserBaseField bub = new B_UserBaseField();
    B_User_Plat upBll = new B_User_Plat();
    public M_UserInfo mu = new M_UserInfo();
    public M_Uinfo basemu = new M_Uinfo();
    public int UserID { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && UserID>0)//升级为作者
            {
                UpdateToAuthor(UserID);
                Response.Redirect("UserInfo.aspx?ID=" + UserID);
                Response.End();
            }
            List<M_UserInfo> Dinfo = new List<M_UserInfo>();
            List<M_Uinfo> Iinfo = new List<M_Uinfo>();
            ShowPlatInfo(UserID);
            mu = buser.GetUserByUserID(UserID);
            //判断是否是认证用户
            if (mu.State == 2) { ApproveFailure_B.Visible = true; }
            else { Approve_B.Visible = true; }
            basemu = buser.GetUserBaseByuserid(UserID);
            Dinfo.Add(mu);
            Iinfo.Add(basemu);
            UInfo_RPT.DataSource = Dinfo;
            UInfo_RPT.DataBind();
            BaseMU_RPT.DataSource = Iinfo;
            BaseMU_RPT.DataBind();
            //增加模型选项卡
            DataTable dtModelUser = bm.GetListUser();
            string labstr = "";
            int tabnum = 3;
            for (int i = 0; i < dtModelUser.Rows.Count; i++)
            {
                string tablename = dtModelUser.Rows[i]["TableName"].ToString();
                DataTable info = bmf.SelectTableName(tablename, "userid=" + UserID);
                if (info != null && info.Rows.Count > 0)
                {
                    int modelid = Convert.ToInt32(dtModelUser.Rows[i]["ModelId"].ToString());
                    DataTable modelfiled = bmf.GetModelFieldList(modelid);
                    labstr += "<td id=\"TabTitle" + (tabnum) + ("\" class=\"tabtitle\" onclick=\"ShowTabss(" + (tabnum)) + ")\">\r";
                    labstr += dtModelUser.Rows[i]["ModelName"].ToString() + "\r";
                    labstr += ("</td>\r");
                    tabTitles += ", \"TabTitle" + (tabnum) + "\"";
                    tabs += ", \"Tabs" + (tabnum) + "\"";
                    ltlTab.Text += "<tbody id=\"Tabs" + (tabnum) + "\" style=\"display: none\">";
                    ltlTab.Text += "<tr>";
                    ltlTab.Text += "<td colspan=\"4\">";
                    ltlTab.Text += " <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\">";

                    for (int k = 0; k < modelfiled.Rows.Count; k++)
                    {
                        if (k % 2 == 0)
                        {
                            ltlTab.Text += "<tr class=\"tdbg\">\r";
                        }
                        ltlTab.Text += "<td class=\"tdbgleft\" style=\"width: 15%; height: 22px\" align=\"right\">\r";
                        ltlTab.Text += modelfiled.Rows[k]["FieldAlias"].ToString() + "\r";
                        ltlTab.Text += "</td>\r";
                        ltlTab.Text += "<td style=\"width: 35%; height: 22px\" align=\"left\">\r";
                        ltlTab.Text += info.Rows[0][modelfiled.Rows[k]["FieldName"].ToString()].ToString() + "\r";
                        ltlTab.Text += "</td>\r";
                        if (k % 2 != 0)
                        {
                            ltlTab.Text += "</tr>\r";
                        }
                    }
                    ltlTab.Text += " </table>";
                    ltlTab.Text += " </td>";
                    ltlTab.Text += " </tr>";
                    ltlTab.Text += "</tbody>";

                    tabnum++;
                }
            }
            M_Uinfo binfo = buser.GetUserBaseByuserid(UserID);
            M_UserInfo uinfo = buser.SeachByID(UserID);
            tbSign.Text = binfo.Sign;
            txtDeadLine.Text = uinfo.DeadLine.ToString();//有效期截止时间
            txtCerificateDeadLine.Text = uinfo.CerificateDeadLine.ToString();
            Privancy.Text = tbPrivacy.SelectedValue.ToString();
            Privancy.Text = tbPrivacy.Items[binfo.Privating].Text;
            DataTable dtuser2 = buser.GetUserBaseByuserid(UserID.ToString());
            lblHtml.Text = bub.GetUpdateHtml(dtuser2);
            BindUserRole(uinfo);
            AuthorDT = null;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='AdminManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>会员信息</a> 当前用户:" + mu.UserName + "</li>");
        }
    }
    //绑定用户角色
    void BindUserRole(M_UserInfo mu)
    {
        DataTable dt = perBll.Sel();
        foreach (DataRow item in dt.Rows)
        {
            ListItem li = new ListItem(item["RoleName"].ToString(), item["ID"].ToString());
            if (mu.UserRole.Contains("," + item["ID"] + ","))
                li.Selected = true;
            li.Enabled = false;
            cblRoleList.Items.Add(li);
        }
    }
    void ShowPlatInfo(int id)
    {
        try
        {
            M_User_Plat upMod = upBll.SelReturnModel(id);
            if (upMod != null && upMod.Status != -1)
            {
                platInfo_A.Visible = true;
                DownPlat_B.Visible = true;
                tbTrueName_L.Text = upMod.TrueName;
                tbCompName_L.Text = upMod.CompName;
                tbPost_L.Text = upMod.GroupName;
                tbPhone_L.Text = upMod.Mobile;
            }
            else { UpPlat_B.Visible = true; }
        }
        catch //数据库未升级,可能报错
        {
            UpPlat_B.Visible = false;
        }
    }
    public string GetIpLocation(string ip)
    {
        return IPScaner.IPLocation(ip);
    }
    public string GetGroupName(string GroupID)
    {
        B_Group bgp = new B_Group();
        return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
    }
    public string GetHoneyName()
    {
        return mu.HoneyName;
    }
    public string GetPrivat(string ss)
    {
        int sss = DataConverter.CLng(ss);
        switch (sss)
        {
            case 0:
                return "公开信息";
            case 1:
                return "只对好友公开";
            case 2:
                return "完全保密";
            default:
                return "";
        }
    }
    protected void DelUserPost_Btn_Click(object sender, EventArgs e)
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        int count = barBll.DelByUID(UserID);
        function.WriteSuccessMsg("删除成功,共移除" + count + "个贴子");
    }
    protected void UPClient_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo userinfo = buser.GetUserByUserID(UserID);
        M_Uinfo users = buser.GetUserBaseByuserid(UserID);
        B_Client_Basic bsc = new B_Client_Basic();
        M_Client_Basic client = new M_Client_Basic();
        //通过客户姓名获取客户信息
        DataTable dt = new DataTable();
        dt = bsc.SelByName(userinfo.UserName.Trim());
        if (dt == null || dt.Rows.Count <= 0) //如果找不到客户则升级
        {
            client.Flow = 0;
            //client.Client_Source = "在线注册";
            //client.Client_Area = users.Province;
            client.P_name = userinfo.UserName;
            client.Add_Date = DateTime.Now;
            client.Code = DataSecurity.MakeFileRndName();
            //client.Add_Name = userinfo.TrueName;
            client.Title = "在线注册";
            M_Client_Penson person = new M_Client_Penson();
            person.Code = client.Code;
            person.Birthday = DataConverter.CDate(users.BirthDay);
            person.city = users.County;
            person.country = users.Country;
            person.Fax_phone = users.Fax;
            person.Home_Phone = users.HomePhone;
            person.Homepage = users.HomePage;
            person.ICQ_Code = users.ICQ;
            person.Id_Code = users.IDCard;
            person.MSN_Code = users.MSN;
            person.Native = users.Province;
            person.province = users.County;
            person.QQ_Code = users.QQ;
            person.Telephone = users.Mobile;
            person.UC_Code = users.UC;
            person.Work_Phone = users.OfficePhone;
            person.YaoHu_Code = users.Yahoo;
            person.ZipCodeW = users.ZipCode;
            bsc.GetInsert(client);
            new B_Client_Penson().GetInsert(person);
            function.WriteSuccessMsg("升级成功,请进入客户管理系统管理客户信息!");
        }
        else
        {
            Response.Redirect("EditCus.aspx?id=" + UserID);
        }
    }
    protected void UInfo_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "useLogin")
        {
            //根据用户名和密码验证会员身份，并取得会员信息
            int userid = DataConverter.CLng(e.CommandArgument);
            M_UserInfo mu = buser.GetSelect(userid);
            buser.SetLoginState(mu);
            Response.Write("<script>window.open('/user/Default.aspx')</script>");
        }
    }
    //已发布文章数
    protected string GetCount()
    {
        M_UserInfo userinfo = buser.GetUserByUserID(DataConverter.CLng(Request.QueryString["id"]));
        B_Content sd_CommonModel = new B_Content();
        return sd_CommonModel.CountDatas("Inputer", userinfo.UserName).ToString();
    }
    //计算云盘空间
    protected string GetCloud()
    {
        string path = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName;
        if (Directory.Exists(path))
        {
            long len = GetDirectoryLength(path);
            string dirLen = Math.Round((double)len / 1024 / 1024, 2) + " MB";
            return "已用了 " + dirLen;
        }
        else
        {
            return "未开通云盘";
        }
    }
    public long GetDirectoryLength(string dirPath)
    {
        if (!Directory.Exists(dirPath))
            return 0;
        long len = 0;
        DirectoryInfo di = new DirectoryInfo(dirPath);
        foreach (FileInfo fi in di.GetFiles())
        {
            len += fi.Length;
        }
        DirectoryInfo[] dis = di.GetDirectories();
        if (dis.Length > 0)
        {
            for (int i = 0; i < dis.Length; i++)
            {
                len += GetDirectoryLength(dis[i].FullName);
            }
        }
        return len;
    }

    protected void CloudManage_Click(object sender, EventArgs e)
    {
        mu = buser.GetUserByUserID(UserID);
        if (mu.IsCloud.ToString() == "1")
        {
            FileSystemObject.Delete(base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName, FsoMethod.Folder);
            if (buser.UpdateIsCloud(mu.UserID, 0))
                function.WriteSuccessMsg("云盘关闭成功", CustomerPageAction.customPath + "user/UserInfo.aspx?id=" + Request["id"]);
        }
        else
        {

            string path = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName;
            Directory.CreateDirectory(path);
            string pathfile = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName + "/我的文档";
            string pathphoto = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName + "/我的相册";
            string pathmusic = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName + "/我的音乐";
            string pathvideo = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.UploadDir + "/" + mu.UserName + "/我的视频";
            Directory.CreateDirectory(pathfile);
            Directory.CreateDirectory(pathphoto);
            Directory.CreateDirectory(pathmusic);
            Directory.CreateDirectory(pathvideo);
            if (buser.UpdateIsCloud(mu.UserID, 1))
                function.WriteSuccessMsg("云盘开通成功", CustomerPageAction.customPath + "user/UserInfo.aspx?id=" + Request["id"]);
        }
    }

    protected string cloud()
    {
        if (mu.IsCloud.ToString() == "1")
            return "关闭云盘";
        else
            return "开通云盘";
    }

    protected void ResetSpwd_Click(object sender, EventArgs e)
    {
        //if (ull.UpByWhere("ZL_User", "PayPassWord='7fef6171469e80d32c0559f88b377245'", "UserID=" + Request.QueryString["id"]))
        //{
        //    function.WriteSuccessMsg("恭喜，二级密码成功重设为初始值admin888", CustomerPageAction.customPath + "user/UserInfo.aspx?id=" + Request["id"]);
        //}
    }
    // 推荐人
    protected string GetParentUser()
    {
        mu = buser.GetUserByUserID(DataConverter.CLng(Request["id"]));
        if (mu.ParentUserID != "")
        {
            try
            {
                M_UserInfo pmu = buser.GetUserByUserID(Convert.ToInt32(mu.ParentUserID));
                if (pmu.UserName != "")
                {
                    return pmu.UserName + "(<span style='color:blue;'>" + pmu.UserID + "</span>)";
                }
                else
                {
                    return pmu.ParentUserID;
                }
            }
            catch
            {
                return mu.ParentUserID;
            }
        }
        return "";
    }
    //-------作者
    protected B_Author authorBll = new B_Author();
    protected M_Author authorMod = new M_Author();
    public string GetIsAuthor(object o)
    {
        string result = "<span><a onclick=\"author('" + Request.RawUrl + "&action=Author');\" style='color:green;cursor:pointer;' title='升级为作者'>[+]</a></span>";
        int flag = authorBll.IsAuthor(DataConvert.CLng(o.ToString()), AuthorDT);
        if (flag != -1)
        {
            result = "<span><a href='../AddOn/Author.aspx?Action=Modify&AUId=" + flag + "' style='color:green;' title='查看详情'>[本站作者]</a></span>";
        }
        return result;
    }
    public void UpdateToAuthor(int userID)
    {
        M_UserInfo mu = buser.GetUserByUserID(userID);
        if (authorBll.IsBindAuthor(userID)) function.WriteErrMsg("用户已经是升级,无法重复绑定!!");
        authorMod.AuthorName = mu.UserName;
        authorMod.AuthorID = mu.UserID;
        authorMod.AuthorEmail = mu.Email;
        authorMod.AuthorLastUseTime = DateTime.Now;
        authorMod.AuthorBirthDay = DateTime.Now;
        authorBll.insert(authorMod);
    }
    public DataTable AuthorDT
    {
        get
        {
            if (Session["AuthorDT"] == null)
                Session["AuthorDT"] = authorBll.GetSourceAll();
            return Session["AuthorDT"] as DataTable;
        }
        set
        {
            Session["AuthorDT"] = null;
        }
    }
    public string GetCompanyName(string userID)
    {
        mu = buser.GetUserByUserID(DataConverter.CLng(userID));
        return mu.CompanyName;
    }
    public string GetCompanyDesc(string userID)
    {
        mu = buser.GetUserByUserID(DataConverter.CLng(userID));
        return mu.CompanyDescribe;
    }
    protected void UpPlat_B_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.SelReturnModel(UserID);
        M_User_Plat upMod = upBll.SelReturnModel(UserID);
        M_Plat_Comp compMod = new M_Plat_Comp();
        B_Plat_Comp compBll = new B_Plat_Comp();
        if (upMod == null)
        {
            //------------------------------------
            upMod = new M_User_Plat();
            upMod.UserID = mu.UserID;
            upMod.TrueName = B_User.GetUserName(mu.HoneyName, mu.TrueName, mu.UserName);
            upMod.Post = "";
            upMod.Status = 1;
            //----------------------------------------
            compBll.CreateByUser(upMod);
            upBll.Insert(upMod);
        }
        else
        {
            if (upMod.CompID == 0)//兼容之前的逻辑
            {
                compBll.CreateByUser(upMod);
            }
            upMod.Status = 1;
            upBll.UpdateByID(upMod);
        }
        ShowPlatInfo(upMod.UserID);
        function.WriteSuccessMsg("操作成功,你现在可以为该用户指定公司");
    }
    protected void DownPlat_B_Click(object sender, EventArgs e)
    {
        //该用户已是能力中心用户的情况下,移除其能力中心身份信息(只是将其公司ID改为0)
        M_User_Plat upMod = upBll.SelReturnModel(UserID);
        if (upMod != null)
        {
            upMod.Status = -1;
            upBll.UpdateByID(upMod);
        }
        function.WriteSuccessMsg("操作成功");
    }
    protected void LockUser_Click(object sender, EventArgs e)
    {
        M_UserInfo userMod = buser.GetUserByUserID(UserID);
        if (userMod != null && userMod.State == 0)
        {
            buser.BatAudit(userMod.UserID.ToString());
            function.WriteSuccessMsg("操作成功！");
        }
    }
    protected void ClearCode_B_Click(object sender, EventArgs e)
    {
        M_UserInfo userMod = buser.GetUserByUserID(UserID);
        userMod.ZnPassword = "";
        buser.UpDateUser(userMod);
        function.WriteSuccessMsg("操作成功!");
    }
    /// <summary>
    /// 聚合认证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Approve_B_Click(object sender, EventArgs e)
    {
        buser.BatAudit(UserID.ToString(), 2);
        Approve_B.Visible = false;
        ApproveFailure_B.Visible = true;
    }
    /// <summary>
    /// 取消认证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ApproveFailure_B_Click(object sender, EventArgs e)
    {
        buser.BatAudit(UserID.ToString(), 3);
        Approve_B.Visible = true;
        ApproveFailure_B.Visible = false;
    }
}