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
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Page
{
    public partial class PageRecyle : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Model bmode = new B_Model();
        private B_User buser = new B_User();
        private B_ModelField mll = new B_ModelField();
        protected B_Templata tll = new B_Templata();
        public M_UserInfo UserInfo;
        public int ModelID;
        public string flag;
        public string KeyWord;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(base.Request.QueryString["ModelID"]))
                {
                    this.ModelID = 0;
                }
                else
                {
                    this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
                }

                this.flag = base.Request.QueryString["type"];

                if (this.ModelID != 0)
                {
                    M_Templata tempinfos = tll.Getbyid(this.ModelID);
                    if (tempinfos.IsTrue != 1)
                    {
                        function.WriteErrMsg("找不到此栏目或此栏目未启用!", "Recyle.aspx", "栏目出错");
                    }

                    string modeinfo = tempinfos.Modelinfo.ToString();
                    string printinfo = "";
                    if (!string.IsNullOrEmpty(modeinfo))
                    {
                        if (modeinfo.IndexOf("|") > 0)
                        {
                            if (modeinfo.IndexOf(",") > 0)
                            {
                                string[] modearr = modeinfo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                {
                                    for (int i = 0; i < modearr.Length; i++)
                                    {
                                        string[] ddaar = modearr[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        int modeid = DataConverter.CLng(ddaar[0]);
                                        //string tempname = bmode.GetModelById(modeid).ModelName.ToString();
                                        printinfo = printinfo + "[<a href=\"AddContent.aspx?ModelID=" + modeid.ToString() + "&Nodeid=" + this.ModelID.ToString() + "\">添加" + bmode.GetModelById(modeid).ItemName + "</a>]";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (modeinfo.IndexOf(",") > 0)
                            {
                                string[] ddaar = modeinfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                int modeid = DataConverter.CLng(ddaar[0]);
                                if (bmode.GetModelById(modeid).ModelName != null)
                                {
                                    string tempname = bmode.GetModelById(modeid).ModelName.ToString();
                                    printinfo = printinfo + "[<a href=\"AddContent.aspx?ModelID=" + modeid.ToString() + "&Nodeid=" + this.ModelID.ToString() + "\">添加" + bmode.GetModelById(modeid).ItemName + "</a>]";
                                }
                            }
                        }
                    }

                    RepNodeBind(this.ModelID);
                }
                else
                {

                    RepNodeBind(0);
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li> <li><a href='PageContent.aspx'>黄页管理</a></li> <li style='color=red'><a href='PageRecyle.aspx'>回收站</a></li>");
        }
        private void RepNodeBind(int id)
        {

            if (id > 0)
            {
                this.hdflag.Value = this.flag;
                this.hdid.Value = id.ToString();
                this.hdmdid.Value = this.ModelID.ToString();
            }
            else
            {
                id = DataConverter.CLng(this.hdid.Value);
                this.flag = this.hdflag.Value.ToString();
                this.ModelID = DataConverter.CLng(this.hdmdid.Value);
            }

            M_Templata modetable = tll.Getbyid(id);
            int uuid = DataConverter.CLng(modetable.UserID); //用户ID
            string modeinfo = modetable.Modelinfo;
            string SearchTitle = Request.Form["TxtSearchTitle"];

            this.Egv.DataSource = this.bll.Page_GetRecycle();
            this.Egv.DataKeyNames = new string[] { "GeneralID" };
            this.Egv.DataBind();
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtSearchTitle.Value.Trim()))
            {
                this.KeyWord = this.TxtSearchTitle.Value.Trim().Replace(" ", "");
                Egv.DataSource = bll.Page_GetRecycle(KeyWord);
                Egv.DataBind();
            }
            else
            {
                Egv.DataSource = bll.Page_GetRecycle();
                Egv.DataBind();
            }
        }
        public string GetUrl(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "/Page/PageContent.aspx?ItemID=" + p;
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        public bool ChkStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == 99)
                return false;
            else
                return true;

        }
        public string GetModel(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);

            if (cinfo.ModelID == 0)
            {
                return "";
            }
            else
            {
                return "[" + bmode.GetModelById(cinfo.ModelID).ItemName + "] ";
            }
        }
        public string GetStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == 0)
                return "待审核";
            if (s == 99)
                return "已审核";
            if (s == -1)
                return "退档";
            return "回收站";
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind(this.ModelID);
        }
        protected string GetCommandID()
        {
            M_UserInfo info = buser.GetLogin();
            DataTable cmdinfo = mll.SelectTableName("ZL_PageReg", "TableName like 'ZL_Reg_%' and UserName='" + info.UserName + "'");
            if (cmdinfo != null)
            {
                if (cmdinfo.Rows.Count > 0)
                {
                    return cmdinfo.Rows[0]["ID"].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Rec")
            {

                this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
                this.bll.Reset(e.CommandArgument.ToString());//将状态改回0
                Response.Write("<script language=javascript>alert('还原成功!');location.href='PageRecyle.aspx?ModelID=" + ModelID + "';</script>");
            }

            else if (e.CommandName == "Del")
            {
                bll.Del(Convert.ToInt32(e.CommandArgument));
                function.WriteSuccessMsg("删除成功");
                HttpContext.Current.Response.Write("<script>alert('删除成功');location=location;</script>");
            }
        }//还原与删除
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }//全选
        protected void btnRecAll_Click(object sender, EventArgs e)//批量还原
        {
            this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    this.bll.Reset(Egv.DataKeys[i].Value.ToString());
                }
            }
            Response.Write("<script language=javascript>alert('批量还原成功!');location.href='PageRecyle.aspx?ModelID=" + ModelID + "';</script>");
        }
        protected void Bat_Del_Click(object sender, EventArgs e)//批量彻底删除
        {
            this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    bll.Del(itemID);
                }
            }
            Response.Write("<script language=javascript>alert('批量删除成功!');location.href='PageRecyle.aspx?ModelID=" + ModelID + "';</script>");
        }
        protected void Btn_DelAll_Click(object sender, EventArgs e)
        {
            if (bll.Page_ClearRecycle())
            {
                Egv.DataSource = bll.Page_GetRecycle();
                Egv.DataBind();
                function.WriteSuccessMsg("清空成功！");
            }
        }
    }
}