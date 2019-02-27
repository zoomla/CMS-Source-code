using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

namespace ZoomLaCMS.Manage.Config
{
    public partial class AddSensitivity :CustomerPageAction
    {
        protected B_Sensitivity sll = new B_Sensitivity();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">敏感词汇</li>");
            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] != null)
                {
                    string menu = Request.QueryString["menu"].ToString();
                    if (Mid > 0)
                    {
                        this.Label1.Text = "修改敏感词汇";
                        this.Button1.Text = " 修 改 ";
                        M_Sensitivity infos = sll.SelReturnModel(Mid);
                        this.keyword.Text = infos.keyname;
                        //function.WriteErrMsg(infos.keyname.Length);
                        this.istrue.Checked = infos.istrue == 1;
                    }
                }
                else
                {
                    this.Label1.Text = "添加敏感词汇";
                    this.Button1.Text = " 添 加 ";
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sensitivity.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Mid > 0)
            {
                M_Sensitivity Msinfo = sll.SelReturnModel(Mid);
                Msinfo.istrue = this.istrue.Checked ? 1 : 0;
                Msinfo.keyname = this.keyword.Text.Trim();
                sll.GetUpdate(Msinfo);
                function.WriteSuccessMsg("修改成功!", "Sensitivity.aspx");
            }
            else
            {
                M_Sensitivity Msinfo = new M_Sensitivity();
                Msinfo.istrue = this.istrue.Checked ? 1 : 0;
                Msinfo.keyname = this.keyword.Text.Trim();
                if (sll.IsExist(Msinfo.keyname)) { function.WriteErrMsg("[" + Msinfo.keyname + "]已存在,无法重复添加!"); return; }
                sll.GetInsert(Msinfo);
                function.WriteSuccessMsg("添加成功!", "Sensitivity.aspx");
            }
        }
    }
}