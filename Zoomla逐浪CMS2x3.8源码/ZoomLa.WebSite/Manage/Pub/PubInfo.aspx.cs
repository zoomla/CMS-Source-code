using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;
using System.Xml;

public partial class Manage_I_Pub_PubInfo : System.Web.UI.Page
{
    private B_Pub pubBll = new B_Pub();
    private B_Admin badmin = new B_Admin();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    //copy,edit
    public string Menu { get { return (Request.QueryString["Menu"] ?? "").ToLower(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "modelname":
                    result = SeeModelName();
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
            return;
        }
        if (!IsPostBack)
        {
            ModelList_DP.DataSource = pubBll.Sel();
            ModelList_DP.DataBind();
            string bread = "<li class='active'>{0}</li>";
            //-------------------------------------------------------------
            if (Mid > 0)
            {
                M_Pub pubMod = pubBll.SelReturnModel(Mid);
                M_AdminInfo adminMod = B_Admin.GetLogin();
                string prowinfo = B_Role.GetPowerInfoByIDs(adminMod.RoleList);
                if (!adminMod.IsSuperAdmin() && !prowinfo.Contains("," + pubMod.PubTableName + ","))
                {
                    function.WriteErrMsg("无权限管理该互动模型!!");
                }
                ModelList_DP.SelectedValue = pubMod.PubTableName;
                PubTableName.Enabled = false;
                PubLogin.Checked = pubMod.PubLogin == 1;
                PubLoginUrls.Visible = PubLogin.Checked;
                PubLoginUrl.Text = pubMod.PubLoginUrl;
                bread = string.Format(bread, "修改[" + pubMod.PubName + "]");
                #region Menu操作
                switch (Menu)
                {
                    case "edit":
                        {
                            PubType.Enabled = false;
                            Lbtitle.Text = "修改模块信息";
                            PubName.Text = pubMod.PubName;
                            PubClass.SelectedValue = pubMod.PubClass.ToString();
                            PubType.SelectedValue = pubMod.PubType.ToString();

                            if (pubMod.PubOneOrMore == 2)
                            {
                                PubOneOrMore.SelectedValue = "2";
                                PubOneOrMorenum.Visible = true;
                                PubOneOrMorenum.Text = pubMod.PubOneOrMore.ToString();
                            }
                            else
                            {
                                PubIPOneOrMorenum.Visible = false;
                                PubOneOrMore.SelectedValue = pubMod.PubOneOrMore.ToString();
                            }

                            if (pubMod.PubIPOneOrMore < 2)
                            {
                                PubIPOneOrMorenum.Visible = false;
                                PubIPOneOrMore.SelectedValue = pubMod.PubIPOneOrMore.ToString();
                            }
                            else
                            {
                                PubIPOneOrMore.SelectedValue = "2";
                                PubIPOneOrMorenum.Visible = true;
                                PubIPOneOrMorenum.Text = pubMod.PubIPOneOrMore.ToString();
                            }
                            Interval_T.Text = pubMod.Interval.ToString();
                            if (!string.IsNullOrEmpty(pubMod.PubTableName))
                                PubTableName.Text = pubMod.PubTableName.Replace("ZL_Pub_", "");
                            PubTemplate_hid.Value = pubMod.PubTemplate;
                            PubLoadstr.Text = pubMod.PubLoadstr;
                            PubIsTrue.Checked = pubMod.PubIsTrue == 1 ? true : false;
                            PubCode.Checked = pubMod.PubCode == 1 ? true : false;
                            PubOpenComment.Checked = pubMod.PubOpenComment == 1 ? true : false;
                            //CookieNum_Rad.SelectedValue = (pubinfo.PubTimeSlot < 1 ? 0 : 1).ToString();
                            //CookieNum_T.Text = pubinfo.PubTimeSlot < 1 ? "1" : pubinfo.PubTimeSlot.ToString();
                            RaPublic.Checked = pubMod.Pubid == 1 ? true : false;
                            Pubkeep.Text = pubMod.Pubkeep.ToString();
                            Puberrmsg.Text = pubMod.Puberrmsg.ToString();
                            if (pubMod.PubEndTime.ToString() == "9999-12-31 23:59:59")
                                PubEndTime.Text = "";
                            else
                                PubEndTime.Text = pubMod.PubEndTime.ToString().Replace("/", "-");
                            PubInputTM_hid.Value = pubMod.PubInputTM;
                            PubInputLoadStr.Text = pubMod.PubInputLoadStr;
                            Pubinfo.Text = pubMod.Pubinfo;
                            PubGourl.Text = pubMod.PubGourl;
                            pubflag.Checked = pubMod.PubFlag == 1;
                            if (!string.IsNullOrEmpty(pubMod.PubPermissions))
                            {
                                CheckBox1.Checked = pubMod.PubPermissions.Contains("Look");
                                CheckBox2.Checked = pubMod.PubPermissions.Contains("Edit");
                                CheckBox3.Checked = pubMod.PubPermissions.Contains("Del");
                                CheckBox4.Checked = pubMod.PubPermissions.Contains("Sen");
                            }
                        }
                        break;
                    case "copy":
                        {
                            PubTableName.Enabled = true;
                            PubName.Attributes.Add("onkeyup", "Getpy('PubName','PubTableName','PubLoadstr','PubInputLoadStr')");
                            Lbtitle.Text = "复制模块信息";
                            PubName.Text = "新建" + pubMod.PubName;
                            PubClass.SelectedValue = pubMod.PubClass.ToString();
                            PubType.SelectedValue = pubMod.PubType.ToString();
                            PubOneOrMore.SelectedValue = pubMod.PubOneOrMore.ToString();
                            PubIPOneOrMore.SelectedValue = pubMod.PubIPOneOrMore.ToString();
                            if (!string.IsNullOrEmpty(pubMod.PubTableName))
                            {
                                PubTableName.Text = pubMod.PubTableName.Replace("ZL_Pub_", "");
                            }
                            PubTemplate_hid.Value = pubMod.PubTemplate;
                            PubLoadstr.Text = "XJ" + pubMod.PubLoadstr;
                            PubIsTrue.Checked = pubMod.PubIsTrue == 1 ? true : false;
                            PubCode.Checked = pubMod.PubCode == 1 ? true : false;
                            PubOpenComment.Checked = pubMod.PubOpenComment == 1 ? true : false;
                            Pubkeep.Text = pubMod.Pubkeep.ToString();
                            Puberrmsg.Text = pubMod.Puberrmsg.ToString();
                            if (pubMod.PubEndTime.ToString() == "9999-12-31 23:59:59")
                            {
                                PubEndTime.Text = "";
                            }
                            else
                            {
                                PubEndTime.Text = pubMod.PubEndTime.ToString().Replace("/", "-");
                            }
                            PubInputTM_hid.Value = pubMod.PubInputTM;
                            PubInputLoadStr.Text = "XJ" + pubMod.PubInputLoadStr;
                            Pubinfo.Text = pubMod.Pubinfo;
                            PubGourl.Text = pubMod.PubGourl;

                            if (!string.IsNullOrEmpty(pubMod.PubPermissions))
                            {
                                CheckBox1.Checked = pubMod.PubPermissions.Contains("Look");
                                CheckBox2.Checked = pubMod.PubPermissions.Contains("Edit");
                                CheckBox3.Checked = pubMod.PubPermissions.Contains("Del");
                                CheckBox4.Checked = pubMod.PubPermissions.Contains("Sen");
                            }
                        }
                        break;
                    case "delete":
                        {
                            pubMod.PubIsDel = 1;
                            pubBll.GetUpdate(pubMod);
                            function.WriteErrMsg("存档成功", "PubManage.aspx");
                        }
                        break;
                    case "truedelete":
                        {
                            pubBll.GetDelete(Mid);
                            B_Role.DelPower(pubMod.PubTableName);
                            function.WriteSuccessMsg("删除成功", "PubRecycler.aspx");
                        }
                        break;
                    case "revert":
                        {
                            pubMod.PubIsDel = 0;
                            pubBll.InsertUpdate(pubMod);
                            function.WriteSuccessMsg("还原成功", "PubRecycler.aspx");
                        }
                        break;
                }
                #endregion
            }
            else
            {
                PubName.Attributes.Add("onkeyup", "Getpy('PubName','PubTableName','PubLoadstr','PubInputLoadStr')");
                ModelList_DP.Items.Insert(0, new ListItem("请选择模型", ""));
                bread = string.Format(bread,"添加互动");
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Content/ContentManage.aspx'>内容管理</a></li><li><a href='PubManage.aspx'>互动模块</a></li>" + bread);
        }
    }
    protected void PubLogin_SelectedIndexChanged(object sender, EventArgs e)
    {
        PubLoginUrls.Visible = PubLogin.Checked;
    }
    private string SeeModelName()
    {
        string pubtbnamestr = "ZL_Pub_" +Request.Form["value"];
        DataTable Pubtable = pubBll.SelByPubTbName(pubtbnamestr);
        if (Pubtable.Rows.Count > 0)
           return "<font color=blue>已经存在此模型名称! 可重复使用!</font>";
        else
           return "<font color=green> 此互动模型不存在，系统将自动创建!</font>";
    }
    protected void PubInputLoadStr_TextChanged(object sender, EventArgs e)
    {
        SeePubInputLoadStr();
    }
    private void SeePubInputLoadStr()
    {
        DataTable PubInputLoadStrtable = pubBll.SelBy(this.PubLoadstr.Text,PubInputLoadStr.Text,Mid.ToString());
        if (PubInputLoadStrtable.Rows.Count > 0)
        {
           Label3.Text = "<font color=red><b>×</b> 已经存在此信息提交标签</font>";
        }
        else
        {
           Label3.Text = "<font color=green><b>√</b> 此信息提交标签可用!</font>";
        }
    }
    protected void PubLoadstr_TextChanged(object sender, EventArgs e)
    {
        SeePubLoadstr();
    }
    private void SeePubLoadstr()
    {
        DataTable PubLoadstrtable = pubBll.SelBy(this.PubLoadstr.Text, PubInputLoadStr.Text, Mid.ToString());
        if (PubLoadstrtable.Rows.Count > 0)
        {
           Label4.Text = "<font color=red><b>×</b> 已经存在此互动标签</font>";
        }
        else
        {
           Label4.Text = "<font color=green><b>√</b> 此互动标签可用!</font>";
        }
    }
    //提交
    protected void Submit_B_Click(object sender, EventArgs e)
    {
        M_Pub pubMod = new M_Pub();
        bool addtrue = true;
        #region 验证模块
        if (Mid<1)
        {
            DataTable tempinfo = pubBll.SelByName(PubName.Text);
            if (tempinfo.Rows.Count > 0)
            {
                addtrue = false;
                function.WriteErrMsg("已存在此互动模块!请更换模块名称再试!!");
            }
            DataTable PubInputLoadStrtable = pubBll.SelBy("", "PubInputLoadStr.Text", Mid.ToString());
            if (PubInputLoadStrtable.Rows.Count > 0)
            {
                addtrue = false;
                function.WriteErrMsg("已经存在此提交标签!");
            }
            DataTable PubLoadstrtable = pubBll.SelBy(PubLoadstr.Text, "", Mid.ToString());
            if (PubLoadstrtable.Rows.Count > 0)
            {
                addtrue = false;
                function.WriteErrMsg("已经存在此互动标签!");
            }
        }
        #endregion
        if (addtrue)
        {
            if (Mid > 0)
            {
               pubMod = pubBll.SelReturnModel(Mid);
               if (Menu.Equals("copy")) { pubMod.Pubid = 0; }
            }
            pubMod.PubAddnum = 0;
            pubMod.PubCreateTime = DateTime.Now;
            pubMod.PubBindPub = 0;
            pubMod.PubClass = DataConverter.CLng(this.PubClass.SelectedValue);
            pubMod.PubCode = PubCode.Checked ? 1 : 0;
            //界面处理显示结束时间,如果是最大则不显示
            if (!string.IsNullOrEmpty(this.PubEndTime.Text))
            {
                pubMod.PubEndTime = DataConverter.CDate(this.PubEndTime.Text.Replace("/", "-"));
            }
            else
            {
                pubMod.PubEndTime = DateTime.MaxValue;
            }
            pubMod.PubInputLoadStr = PubInputLoadStr.Text;
            pubMod.PubType = DataConverter.CLng(this.PubType.SelectedValue);
            pubMod.PubNodeID = "";
            pubMod.PubTemplateID = "";
            pubMod.PubIsDel = 0;
            pubMod.PubIsTrue = PubIsTrue.Checked ? 1 : 0;
            pubMod.PubLoadstr = PubLoadstr.Text;
            pubMod.PubLogin = PubLogin.Checked ? 1 : 0;
            pubMod.PubLoginUrl = PubLoginUrl.Text;
            pubMod.PubTableName = "ZL_Pub_" + PubTableName.Text;
            pubMod.PubName = PubName.Text;
            pubMod.PubOpenComment = PubOpenComment.Checked ? 1 : 0;
            pubMod.PubShowType = DataConverter.CLng(this.PubShowType.SelectedValue);
            //pubMod.PubTimeSlot = CookieNum_Rad.SelectedValue.Equals("0") ? 0 : DataConverter.CLng(CookieNum_T.Text); 
            pubMod.Pubkeep = DataConverter.CLng(this.Pubkeep.Text);
            pubMod.PubInputTM = "";
            pubMod.PubTemplate = "";
            pubMod.Puberrmsg = Puberrmsg.Text;
            pubMod.PubFlag = pubflag.Checked ? 1 : 0;
            string perm = "";
            if (CheckBox1.Checked)
                perm += "Look";
            if (CheckBox2.Checked)
                perm += ",Edit";
            if (CheckBox3.Checked)
                perm += ",Del";
            if (CheckBox4.Checked)
                perm += ",Sen";
            pubMod.PubPermissions = perm;
            pubMod.PubGourl = PubGourl.Text;
            pubMod.Public = RaPublic.Checked ? 1 : 0;
            //设置限制用户提交数量
            if (this.PubOneOrMore.SelectedValue == "2")
            {
                pubMod.PubOneOrMore = DataConverter.CLng(this.PubOneOrMorenum.Text);
            }
            else
            {
                pubMod.PubOneOrMore = DataConverter.CLng(this.PubOneOrMore.SelectedValue);
            }
            //设置限制IP提交数量
            if (this.PubIPOneOrMore.SelectedValue == "2")
            {
                pubMod.PubIPOneOrMore = DataConverter.CLng(this.PubIPOneOrMorenum.Text);
            }
            else
            {
                pubMod.PubIPOneOrMore = DataConverter.CLng(this.PubIPOneOrMore.SelectedValue);
            }
            pubMod.Interval = DataConverter.CLng(Interval_T.Text);
            string strPath = "";
            string strPathname = "";
            string PubInputTMurl = "";
            string Tempinputstr = @"";
            //模型ID
            if (pubMod.Pubid == 0)
            {
                pubMod.PubModelID = pubBll.CreateModelInfo(pubMod);//建立数据表与模型Field,Model
                int Pubinsertid = pubBll.GetInsert(pubMod);

                #region 创建互动模板
                string PubTemplateurl = "";
                string Tmpstr = "";
                M_Label lab = new M_Label();
                B_Label blab = new B_Label();
                lab.LabelAddUser = badmin.GetAdminLogin().AdminId;
                string getstr = "";
                switch (pubMod.PubClass)
                {
                    //0-内容 1-商城 2-黄页 3-店铺 4-会员
                    case 0:
                        getstr = "ID";
                        break;
                    case 1:
                        getstr = "ID";
                        break;
                    case 2:
                        getstr = "Pageid";
                        break;
                    case 3:
                        getstr = "id";
                        break;
                    case 4:
                        getstr = "Userid";
                        break;
                    case 5:
                        getstr = "Nodeid";
                        break;
                    case 6:
                        getstr = "";
                        break;
                    default:
                        getstr = "ID";
                        break;
                }
                string pubcontdid = "-1";
                if (pubMod.PubClass != 6)
                {
                    pubcontdid = "{$GetRequest(" + getstr + ")$}";
                }
                //创建标签
                switch (pubMod.PubType)
                {
                    case 0://评论
                        //创建源标签
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 4;
                        lab.LabelTable = "" + pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = "" + pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = "" + pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder = "" + pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "{Repeate}\n用户名:{Field=\"PubUserName\"/}<br />\n评论说明:{Field=\"PubContent\"/}<br />\n用户IP:{Field=\"PubIP\"/}<br />\n评论时间:{Field=\"PubAddTime\"/}<br />{/Repeate}<br />\n{ZL.Page/}";
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认评论" + pubMod.PubName + "模板.html";
                        strPathname = "默认评论" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        strPath = strPath.Replace("/", @"\");
                        FileSystemObject.WriteFile(strPath, Tmpstr);
                        break;
                    case 1://投票
                        //创建标签
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 4;
                        lab.LabelTable = "" + pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = "" + pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = "" + pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder = "" + pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "{Repeate}\n用户名:{Field=\"PubUserName\"/}<br />\n投票说明:{Field=\"PubContent\"/}<br />\n用户IP:{Field=\"PubIP\"/}<br />\n投票时间:{Field=\"PubAddTime\"/}<br />{/Repeate}<br />\n{ZL.Page/}";
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认投票" + pubMod.PubName + "模板.html";
                        strPathname = "默认投票" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 2://活动
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 4;
                        lab.LabelTable = "" + pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = "" + pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = "" + pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder = "" + pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "<a href=PubAction.aspx?menu=hd&act=add&Pubid=" + Pubinsertid.ToString() + ">发起活动</a>\n{Repeate}\n用户名:{Field=\"PubUserName\"/}<br />\n活动内容:{Field=\"PubContent\"/}<br />\n用户IP:{Field=\"PubIP\"/}<br />\n提交时间:{Field=\"PubAddTime\"/}\n{/Repeate}<br />\n{ZL.Page/}";
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认活动" + pubMod.PubName + "模板.html";
                        strPathname = "默认活动" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 3://留言
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 4;
                        lab.LabelTable = "" + pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = "" + pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = "" + pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder = "" + pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "{Repeate}\n用户名:{Field=\"PubUserName\"/}<br />\n留言内容:{Field=\"PubContent\"/}<br />\n用户IP:{Field=\"PubIP\"/}<br />\n提交时间:{Field=\"PubAddTime\"/}\n{/Repeate}<br />\n{ZL.Page/}";
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认留言" + pubMod.PubName + "模板.html";
                        strPathname = "默认留言" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 4://问券
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 4;
                        lab.LabelTable = "" + pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = "" + pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = "" + pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder = "" + pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "{Repeate}\n用户名:{Field=\"PubUserName\"/}<br />\n问券内容:{Field=\"PubContent\"/}<br />\n用户IP:{Field=\"PubIP\"/}<br />\n提交时间:{Field=\"PubAddTime\"/}\n{/Repeate}<br />\n{ZL.Page/}<br />";
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认问券" + pubMod.PubName + "模板.html";
                        strPathname = "默认问券" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 5://统计
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 2;
                        lab.LabelTable = pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " and Parentid=0  And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder =  pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "点击数:{Field=\"Pubnum\"/}";
                        lab.Desc = pubMod.PubTableName + "动态标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认统计" + pubMod.PubName + "模板.html";
                        strPathname = "默认统计" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 6://竞标
                        //创建标签
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 4;
                        lab.LabelTable = "" + pubMod.PubTableName + " left join ZL_Pub on " + pubMod.PubTableName + ".Pubupid=ZL_Pub.Pubid";
                        lab.LabelField = "" + pubMod.PubTableName + ".*,ZL_Pub.*";
                        lab.LabelWhere = "" + pubMod.PubTableName + ".Pubupid=" + Pubinsertid.ToString() + " And " + pubMod.PubTableName + ".PubContentid=" + pubcontdid + " And " + pubMod.PubTableName + ".Pubstart=1";
                        lab.Param = "";
                        lab.LabelOrder = "" + pubMod.PubTableName + ".ID DESC";
                        lab.LabelCount = "10";
                        lab.Content = "{Repeate}\n用户名:{Field=\"PubUserName\"/}<br />\n竞标说明:{Field=\"PubContent\"/}<br />\n用户IP:{Field=\"PubIP\"/}<br />\n竞标时间:{Field=\"PubAddTime\"/}<br />{/Repeate}<br />\n{ZL.Page/}";
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "默认竞标" + pubMod.PubName + "模板.html";
                        strPathname = "默认竞标" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 7:
                        lab.LableName = pubMod.PubName + "调用标签";
                        lab.LabelCate = "互动标签";
                        lab.LableType = 1;
                        lab.LabelTable = "";
                        lab.LabelField = "";
                        lab.LabelWhere = "";
                        lab.Param = "";
                        lab.LabelOrder = "";
                        lab.LabelCount = "10";
                        if (string.IsNullOrEmpty(starOPT.Text.Trim()))
                            function.WriteErrMsg("选项不能为空!!");
                        string[] starOP = starOPT.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        lab.Content = GetContent(starOP, Pubinsertid);
                        lab.Desc = pubMod.PubTableName + "分页标签";
                        lab.LabelNodeID = 0;
                        blab.AddLabelXML(lab);
                        Tmpstr = "{ZL.Label id=\"" + pubMod.PubName + "调用标签\"/}\n{Pub." + PubInputLoadStr.Text + "/}";
                        strPath = "评星" + pubMod.PubName + "模板.html";
                        strPathname = "评星" + pubMod.PubName + "模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 8:
                        break;
                    default:
                        function.WriteErrMsg("类型错误,该类型不存在!!!");
                        break;
                }
                strPath = strPath.Replace("/", @"\");
                if (!string.IsNullOrEmpty(strPath))
                FileSystemObject.WriteFile(strPath, Tmpstr);
                PubTemplateurl = "/互动模板/" + strPathname;
                #endregion
                #region 创建互动提交模板
                switch (pubMod.PubType)
                {
                    case 0://评论
                        Tempinputstr = @"<form name=""form{PubID/}"" method=""post"" action=""/PubAction.aspx"">
<input type=""hidden"" name=""PubID"" id=""PubID"" value=""{PubID/}"" />
<input type=""hidden"" name=""PubContentid"" id=""PubContentid"" value=""{PubContentid/}"" />
<input type=""hidden"" name=""PubInputer"" id=""PubInputer"" value=""{PubInputer/}"" />
<div class=""form-group"">
<label for=""PubTitle"">评论标题：</label>
<input type=""text"" class=""form-control"" id=""PubTitle"" name=""PubTitle"" />{PubCode/}
</div>
<div class=""form-group"">
<label for=""PubContent"">评论内容：</label>
<textarea class=""form-control"" name=""PubContent"" cols=""50"" rows=""10"" id=""PubContent""></textarea>
</div>
<div class=""form-group text-center"">
<button type=""submit"" class=""btn btn-default"">提交</button>
<button type=""reset"" class=""btn btn-default"">重置</button>
</div>
</form>";
                        Tempinputstr = Tempinputstr.Replace(@"{PubID/}", Pubinsertid.ToString());
                        strPath = "默认评论" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认评论" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 1://投票
                        Tempinputstr = @"<form name=""form{PubID/}"" method=""post"" action=""/PubAction.aspx"">
<input type=""hidden"" name=""PubID"" id=""PubID"" value=""{PubID/}"" />
<input type=""hidden"" name=""PubContentid"" id=""PubContentid"" value=""{PubContentid/}"" />
<input type=""hidden"" name=""PubInputer"" id=""PubInputer"" value=""{PubInputer/}"" />
<div class=""form-group"">
<label for=""PubTitle"">投票标题：</label>
<input type=""text"" class=""form-control"" id=""PubTitle"" name=""PubTitle"" />
{PubCode/}
</div>
<div class=""form-group"">
<label for=""PubContent"">投票内容：</label>
<textarea class=""form-control"" name=""PubContent"" cols=""50"" rows=""10"" id=""PubContent""></textarea>
</div>
<div class=""form-group text-center"">
<button type=""submit"" class=""btn btn-default"">提交</button>
<button type=""reset"" class=""btn btn-default"">重置</button>
</div>
</form>";
                        Tempinputstr = Tempinputstr.Replace(@"{PubID/}", Pubinsertid.ToString());
                        strPath = "默认投票" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认投票" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 2://活动
                        Tempinputstr = @"<form name=""form{PubID/}"" method=""post"" action=""/PubAction.aspx"">
<input type=""hidden"" name=""PubID"" id=""PubID"" value=""{PubID/}"" />
<input type=""hidden"" name=""PubContentid"" id=""PubContentid"" value=""{PubContentid/}"" />
<input type=""hidden"" name=""PubInputer"" id=""PubInputer"" value=""{PubInputer/}"" />
<div class=""form-group"">
<label for=""PubTitle"">活动标题：</label>
<input type=""text"" class=""form-control"" id=""PubTitle"" name=""PubTitle"" />
{PubCode/}
</div>
<div class=""form-group"">
<label for=""PubContent"">活动内容：</label>
<textarea class=""form-control"" name=""PubContent"" cols=""50"" rows=""10"" id=""PubContent""></textarea>
</div>
<div class=""form-group text-center"">
<button type=""submit"" class=""btn btn-default"">提交</button>
<button type=""reset"" class=""btn btn-default"">重置</button>
</div>
</form>";
                        Tempinputstr = Tempinputstr.Replace(@"{PubID/}", Pubinsertid.ToString());
                        strPath = "默认活动" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认活动" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 3://留言
                        Tempinputstr = @"<form name=""form{PubID/}"" method=""post"" action=""/PubAction.aspx"">
<input type=""hidden"" name=""PubID"" id=""PubID"" value=""{PubID/}"" />
<input type=""hidden"" name=""PubContentid"" id=""PubContentid"" value=""{PubContentid/}"" />
<input type=""hidden"" name=""PubInputer"" id=""PubInputer"" value=""{PubInputer/}"" />
<div class=""form-group"">
<label for=""PubTitle"">留言标题：</label>
<input type=""text"" class=""form-control"" id=""PubTitle"" name=""PubTitle"" />
{PubCode/}
</div>
<div class=""form-group"">
<label for=""PubContent"">留言内容：</label>
<textarea class=""form-control"" name=""PubContent"" cols=""50"" rows=""10"" id=""PubContent""></textarea>
</div>
<div class=""form-group text-center"">
<button type=""submit"" class=""btn btn-default"">提交</button>
<button type=""reset"" class=""btn btn-default"">重置</button>
</div>
</form>";
                        Tempinputstr = Tempinputstr.Replace(@"{PubID/}", Pubinsertid.ToString());
                        strPath = "默认留言" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认留言" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 4://问券
                        Tempinputstr = @"<form name=""form{PubID/}"" method=""post"" action=""/PubAction.aspx"">
<input type=""hidden"" name=""PubID"" id=""PubID"" value=""{PubID/}"" />
<input type=""hidden"" name=""PubContentid"" id=""PubContentid"" value=""{PubContentid/}"" />
<input type=""hidden"" name=""PubInputer"" id=""PubInputer"" value=""{PubInputer/}"" />
<div class=""form-group"">
<label for=""PubTitle"">问券标题：</label>
<input type=""text"" class=""form-control"" id=""PubTitle"" name=""PubTitle"" />
{PubCode/}
</div>
<div class=""form-group"">
<label for=""PubContent"">问券内容：</label>
<textarea class=""form-control"" name=""PubContent"" cols=""50"" rows=""10"" id=""PubContent""></textarea>
</div>
<div class=""form-group text-center"">
<button type=""submit"" class=""btn btn-default"">提交</button>
<button type=""reset"" class=""btn btn-default"">重置</button>
</div>
</form>";
                        Tempinputstr = Tempinputstr.Replace(@"{PubID/}", Pubinsertid.ToString());
                        strPath = "默认问券" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认问券" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 5://统计
                        Tempinputstr = @"<a href=/PubAction.aspx?pubid=" + Pubinsertid + "&PubContentid={PubContentid/}>通过链接提交</a>";
                        strPath = "默认统计" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认统计" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 6://竞标
                        Tempinputstr = @"<form name=""form{PubID/}"" method=""post"" action=""/PubAction.aspx"">
<input type=""hidden"" name=""PubID"" id=""PubID"" value=""{PubID/}"" />
<input type=""hidden"" name=""PubContentid"" id=""PubContentid"" value=""{PubContentid/}"" />
<input type=""hidden"" name=""PubInputer"" id=""PubInputer"" value=""{PubInputer/}"" />
<div class=""form-group"">
<label for=""PubTitle"">竞标标题：</label>
<input type=""text"" class=""form-control"" id=""PubTitle"" name=""PubTitle"" />
{PubCode/}
</div>
<div class=""form-group"">
<label for=""PubContent"">竞标内容：</label>
<textarea class=""form-control"" name=""PubContent"" cols=""50"" rows=""10"" id=""PubContent""></textarea>
</div>
<div class=""form-group text-center"">
<button type=""submit"" class=""btn btn-default"">提交</button>
<button type=""reset"" class=""btn btn-default"">重置</button>
</div>
</form>";
                        Tempinputstr = Tempinputstr.Replace(@"{PubID/}", Pubinsertid.ToString());

                        strPath = "默认竞标" + pubMod.PubName + "提交模板.html";
                        strPathname = "默认竞标" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    case 7://评星
                        if (string.IsNullOrEmpty(starOPT.Text.Trim()))
                            function.WriteErrMsg("选项不能为空!!");
                        string[] starOP = starOPT.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        Tempinputstr = GetContent(starOP, Pubinsertid);
                        //存值方式Content中
                        //name:value,name:value然后用先获取contentID下的记录，然后再统计用lamda统计得分
                        strPath = "评星" + pubMod.PubName + "提交模板.html";
                        strPathname = "评星" + pubMod.PubName + "提交模板.html";
                        strPath = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/互动模板/" + strPath;
                        break;
                    default:
                        break;
                }
                if (pubflag.Checked)//开启cookie验证
                {
                    Tempinputstr += @"    if (!localStorage[""cookflag""]) { localStorage[""cookflag""] = GetRanPass(12); } if (!getCookie(""cookflag"")) { setCookie(""cookflag"", localStorage[""cookflag""]); }";
                }
                strPath = strPath.Replace("/", @"\");
                FileSystemObject.WriteFile(strPath, Tempinputstr);
                PubInputTMurl = "/互动模板/" + strPathname;
                #endregion
                pubMod = pubBll.GetSelect(Pubinsertid);
                pubMod.PubInputTM = PubInputTMurl;//提交窗口模板
                pubMod.PubTemplate = PubTemplateurl;
                pubMod.Public = RaPublic.Checked ? 1 : 0;
                pubBll.GetUpdate(pubMod);
                function.WriteSuccessMsg("添加成功", pubMod.PubType == 8 ? "FormMangae.aspx" : "pubmanage.aspx");
            }
            else
            {
                pubMod.PubTableName = ModelList_DP.SelectedValue;
                pubMod.PubInputTM = PubInputTM_hid.Value;
                pubMod.PubTemplate = PubTemplate_hid.Value;
                pubMod.Pubinfo = Pubinfo.Text;
                pubBll.GetUpdate(pubMod);
                function.WriteSuccessMsg("更新成功", pubMod.PubType == 8 ? "FormMangae.aspx" : "pubmanage.aspx");
            }
        }
    }
    // 中文转换声母
    public string GetChineseFirstChar(string chineseStr)
    {
        StringBuilder sb = new StringBuilder();
        int length = chineseStr.Length;
        for (int i = 0; i < length; i++)
        {
            string chineseChar = chineseStr[i].ToString();
            sb.Append(GetpyChar(chineseChar));
        }
        return sb.ToString();
    }
    // 获得拼音
    public string GetpyChar(string cn)
    {
        #region 处理过程
        byte[] arrCN = Encoding.Default.GetBytes(cn);
        if (arrCN.Length > 1)
        {
            int area = (short)arrCN[0];
            int pos = (short)arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25)
                    max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                }
            }
            return cn;
        }
        else
            return cn;
        #endregion
    }
    
    //-----评星支持方法
    //获取评星内容
    public string GetContent(string[] op, int id)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("~/Config/PubSupport.xml"));
        XmlNode node = xmlDoc.SelectSingleNode("/DataSet/Table[ID='1']");
        string result = node.SelectSingleNode("LabelHead").InnerText;
        string total = node.SelectSingleNode("LabelHead2").InnerText;//汇总
        for (int i = 0; i < op.Length; i++)
        {
            string s = node.SelectSingleNode("LoopContent").InnerText;
            s = s.Replace("{opName/}", op[i]);
            s = s.Replace("{radioName/}", GetChineseFirstChar(op[i]));
            result += s;
        }
        for (int i = 0; i < op.Length; i++)
        {
            string s = node.SelectSingleNode("LoopContent2").InnerText;
            s = s.Replace("{opName/}", op[i]);
            s = s.Replace("{radioName/}", GetChineseFirstChar(op[i]));
            total += s;
        }
        result += node.SelectSingleNode("LabelFoot").InnerText;
        result = result.Replace("{pubid/}", id.ToString());

        total += node.SelectSingleNode("LabelFoot2").InnerText;

        return result + total;
    }
}