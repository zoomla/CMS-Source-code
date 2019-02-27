using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;

using System.Collections.Generic;
using ZoomLa.Components;
using System.IO;
using System.Xml;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.Content
{
    public partial class ListHtmlContent : CustomerPageAction
    {
        protected B_Create CreateBll = new B_Create();
        B_Content bll = new B_Content();
        B_Node nodeBll = new B_Node();
        B_Model bmode = new B_Model();
        B_User bubll = new B_User();
        B_Admin badmin = new B_Admin();
        B_Role RLL = new B_Role();
        B_NodeRole bNr = new B_NodeRole();
        B_AuditingState ba = new B_AuditingState();
        B_Process bp = new B_Process();
        //与当前导入相关类
        ExcelImport import = new ExcelImport();//位于ZoomLa.Components
        protected B_ModelField bfield = new B_ModelField();
        B_Model bmodel = new B_Model();
        public int GeneralID { get { return DataConverter.CLng(Request.QueryString["GeneralID"]); } }
        public int ContentID { get { return DataConverter.CLng(Request.QueryString["ContentID"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li><a href='CreateHtmlContent.aspx'>" + Resources.L.生成发布 + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.内容生成管理 + "</a></li>");
                //直接浏览
                if (ContentID > 0)
                {
                    string htmllink = bll.GetCommonData(ContentID).HtmlLink;
                    if (htmllink != "")
                    {
                        htmllink = "../../" + SiteConfig.SiteOption.GeneratedDirectory + htmllink;
                        Response.Redirect(htmllink);
                    }
                }
                //删除文件
                if (GeneralID > 0)
                {
                    string htmllink = bll.GetCommonData(GeneralID).HtmlLink;
                    if (!string.IsNullOrEmpty(htmllink))
                    {
                        string fleex = "." + htmllink.Split('.')[1];
                        FileSystemObject.Delete(Server.MapPath(htmllink), FsoMethod.File);
                        string HtmlLinkurl = htmllink.Replace(GeneralID + fleex, "");
                        //删除页面与其的分页
                        DirectoryInfo di = new DirectoryInfo(Server.MapPath(HtmlLinkurl));
                        FileInfo[] ff = di.GetFiles(GeneralID + "_" + "*");
                        if (ff.Length != 0)
                        {
                            foreach (FileInfo fi in ff)
                            {
                                fi.Delete();
                            }
                        }
                        bll.UpdateCreate1(GeneralID);
                        function.WriteSuccessMsg(Resources.L.恭喜您删除成功 + "！", "ListHtmlContent.aspx");
                    }
                }
                this.BindOrder();
                MyBind();
            }
        }
        public void MyBind()
        {
            Egv.DataSource = bll.SelForList(DataConverter.CLng(DropDownList1.SelectedValue), TextBox1.Text, DataConverter.CLng(txtbyfilde.SelectedValue), txtbyOrder.SelectedValue);
            Egv.DataBind();
        }
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='ShowContent.aspx?GID={0}&modeid={0}'", this.Egv.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["title"] = Resources.L.双击打开详细页面;
            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MakeHtml")
            {
                Page.Response.Redirect("CreateHtmls.aspx?Type=Contentbyid&InfoID=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "DelHtml")
            {
                int Contentid = DataConverter.CLng(e.CommandArgument.ToString());
                M_CommonData comdt = bll.GetCommonData(Contentid);
                string HtmlLinkurl = comdt.HtmlLink;
                HtmlLinkurl = "../../" + SiteConfig.SiteOption.GeneratedDirectory + HtmlLinkurl;
                FileSystemObject.Delete(Server.MapPath(HtmlLinkurl), FsoMethod.File);
                comdt.HtmlLink = "";
                comdt.IsCreate = 0;
                bll.Update(comdt);
                Response.Redirect("ListHtmlContent.aspx");
            }
        }
        private void BindOrder()
        {
            this.txtbyfilde.Items.Clear();
            this.txtbyOrder.Items.Clear();
            this.txtbyfilde.Items.Add(new ListItem(Resources.L.选择字段, ""));
            this.txtbyfilde.Items.Add(new ListItem(Resources.L.内容ID, "1"));
            this.txtbyfilde.Items.Add(new ListItem(Resources.L.添加时间, "2"));
            this.txtbyfilde.Items.Add(new ListItem(Resources.L.更新时间, "3"));
            this.txtbyfilde.Items.Add(new ListItem(Resources.L.点击数, "4"));
            this.txtbyfilde.Items.Add(new ListItem(Resources.L.推荐级别, "5"));
            //this.txtbyfilde.AutoPostBack = true;
            this.txtbyOrder.Items.Add(new ListItem(Resources.L.排列顺序, ""));
            this.txtbyOrder.Items.Add(new ListItem(Resources.L.升序, "asc"));
            this.txtbyOrder.Items.Add(new ListItem(Resources.L.降序, "desc"));
            this.txtbyOrder.AutoPostBack = true;
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
            string title = "";
            string ids = "";
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    title = title + this.bll.GetCommonData(itemID).Title + "<br />";
                    if (string.IsNullOrEmpty(ids))
                        ids = itemID.ToString();
                    else
                        ids = ids + "," + itemID.ToString();
                    this.bll.SetDel(itemID);
                }
            }
            MyBind();
        }
        public string geturl(string ContentIDs)
        {
            int ContentID = DataConverter.CLng(ContentIDs);
            string htmllink = bll.GetCommonData(ContentID).HtmlLink;
            if (htmllink != "")
            {
                htmllink = htmllink + "\" target=\"_blank\"";
                return htmllink;
            }
            return "";
        }
        public string GetElite(string Elite)
        {
            if (DataConverter.CLng(Elite) > 0)
                return Resources.L.推荐;
            else
                return Resources.L.未推荐;
        }
        public string GetTitle(string ItemID, string NID, string Title, string modeid)
        {
            int nid = DataConverter.CLng(NID);
            string n = "";
            if (nid == this.NodeID)
                n = "<a href=\"ShowContent.aspx?GID=" + ItemID + "&modeid=" + modeid + "\">" + Title + "</a>";
            else
            {
                n = "<strong>[<a href=\"ContentManage.aspx?NodeID=" + NID + "\">" + nodeBll.GetNodeXML(nid).NodeName + "</a>]</strong>&nbsp;<a href=\"ShowContent.aspx?GID=" + ItemID + "&modeid=" + modeid + "\">" + Title + "</a>";
            }
            return n;
        }
        public string GetTitles(string ItemID, string modeid)
        {
            string n = "";
            n = "<a href=\"ShowContent.aspx?GID=" + ItemID + "&modeid=" + modeid + "\" >" + Resources.L.预览 + "</a>";
            return n;
        }
        public string GetStatus(string status)
        {
            return ZLEnum.GetConStatus(DataConvert.CLng(status));
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            string tit = DropDownList1.SelectedValue;
            int stype = DataConvert.CLng(tit);
            string skey = TextBox1.Text;
            MyBind();
        }
        protected void SelectBind(object sender, EventArgs e)
        {
            int otype = DataConverter.CLng(txtbyfilde.SelectedValue);
            string okey = txtbyOrder.SelectedValue;
            MyBind();
        }
        public string GetPic(object modeid)
        {
            if (bmode.GetModelById(DataConverter.CLng(modeid)).ItemIcon != "")
                return "<img src=\"/images/ModelIcon/" + bmode.GetModelById(DataConverter.CLng(modeid)).ItemIcon + "\" style=\"border-width: 0px;\" />";
            else
                return "";
        }
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                for (int i = 0; i < chkArr.Length; i++)
                {
                    M_CommonData comMod = bll.GetCommonData(Convert.ToInt32(chkArr[i]));
                    this.CreateBll.CreateInfo(comMod.GeneralID, comMod.NodeID, comMod.ModelID);
                }
                MyBind();
            }
        }
        protected void btnUnAudit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                foreach (string chk in chkArr)
                {
                    int gid = DataConverter.CLng(chk);
                    M_CommonData conMod = bll.GetCommonData(gid);
                    string fleex = "." + conMod.HtmlLink.Split('.')[1];
                    FileSystemObject.Delete(Server.MapPath(conMod.HtmlLink), FsoMethod.File);
                    string HtmlLinkurl = conMod.HtmlLink.Replace(gid + fleex, "");
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath(HtmlLinkurl));
                    FileInfo[] ff = di.GetFiles(gid + "_" + "*");
                    if (ff.Length != 0)
                    {
                        foreach (FileInfo fi in ff)
                        {
                            fi.Delete();
                        }
                    }
                    conMod.HtmlLink = "";
                    conMod.IsCreate = 0;
                    bll.Update(conMod);
                }
            }
            Response.Redirect("ListHtmlContent.aspx");
        }
    }
}