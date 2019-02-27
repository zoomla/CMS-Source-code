using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Text.RegularExpressions;
using ZoomLa.BLL.Message;
using ZoomLa.Model.Message;
using ZoomLa.Common;
using System.Data.SqlClient;

public partial class Manage_I_Guest_BaikeCheck : CustomerPageAction
{
    B_User buser = new B_User();
    B_Baike bkBll = new B_Baike();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindcheak();
            BindClass();
            MyBind(Mid);
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li><a href='BkCheck.aspx'>百科词条管理</a></li><li class='active'>词条审核</li>");
        }
    }
    protected void MyBind(int tittle)
    {
        M_Baike bkMod = bkBll.SelReturnModel(Mid);
        BaikeName.Text = bkMod.Tittle;
        txbRoleName.Text = bkMod.UserName;
        tbRoleInfo.Text = bkMod.Contents;
        TextBox1.Text = bkMod.Reference;
        TextBox2.Text = bkMod.Btype;
        TextBox3.Text = bkMod.Extend;
        TextBox5.Text = bkMod.Brief;
        string gradestr = bkMod.GradeIDS;
        string[] gradeids = gradestr.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
        if (gradeids.Length != 0)
        {
            classification_D.SelectedValue = gradeids[1];
            int cid = Convert.ToInt32(classification_D.SelectedValue);
            switch (gradeids.Length)
            {
                case 3:
                    BindClass2(cid);
                    classification2_D.SelectedValue = gradeids[2];
                    break;
                case 4:
                    BindClass2(cid);
                    classification2_D.SelectedValue = gradeids[2];
                    BindClass3(cid, Convert.ToInt32(classification2_D.SelectedValue));
                    classification3_D.SelectedValue = gradeids[3];
                    break;
            }
        }
    }
    protected void Bindcheak()
    {
        check.Items.Insert(0, "选择审核");
        check.Items[0].Value = "0";
        check.Items.Insert(1, "通过");
        check.Items[1].Value = "1";
        check.Items.Insert(2, "待完善");
        check.Items[2].Value = "2";
        check.Items.Insert(3, "待创建");
        check.Items[3].Value = "-1";
        check.Items.Insert(4, "不通过");
        check.Items[4].Value = "-2";
        check.SelectedItem.Text = "选择审核";
    }

    protected void BindClass()
    {
        DataTable dt = B_GradeOption.GetCateList();
        classification_D.DataSource = dt;
        classification_D.DataBind();
        classification_D.Items.Insert(0, new ListItem("请选择分类", ""));
    }
    protected void save_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request["id"]);
        int s = Convert.ToInt32(check.SelectedValue);
        string action = Request.QueryString["action"];
        if (action == "manage")
        {
            if (classification_D.SelectedIndex <= 0)
            {
                function.WriteErrMsg("请选择分类");
                return;
            }

            string cl = "";
            string gradeids ="";
            if (classification3_D.SelectedIndex > 0)
            {
                cl = classification3_D.SelectedItem.Text;
                gradeids =cl+":"+classification_D.SelectedValue+":"+classification2_D.SelectedValue + ":" + classification3_D.SelectedValue;
            }
            else if (classification2_D.SelectedIndex > 0)
            {
                cl = classification2_D.SelectedItem.Text;
                gradeids = cl + ":" + classification_D.SelectedValue + ":" + classification2_D.SelectedValue;
            }
            else
            {
                cl = classification_D.SelectedItem.Text;
                gradeids = cl + ":" + classification_D.SelectedValue;
            }
            M_Baike bkMod = bkBll.SelReturnModel(id);
            bkMod.Status = s;
            bkMod.Brief = TextBox5.Text;
            bkMod.Classification = cl;
            bkMod.GradeIDS = gradeids;
            bkBll.UpdateByID(bkMod);
            function.WriteSuccessMsg("审核成功", "BkCheck.aspx");
        }
    }

    public string NoHTML(string Htmlstring) //替换HTML标记
    {
        string pattern = "http://([^\\s]+)\".+?span.+?\\[(.+?)\\].+?>(.+?)<";
        Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
        //删除脚本
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
        if (Htmlstring.Length > 200)
        {
            return Htmlstring.Substring(0, 200);
        }
        return Htmlstring;
    }
    protected void backlast_Click(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "manage")
        {
            Response.Redirect("BkCheck.aspx");
        }
    }
    protected void classification_D_SelectedIndexChanged(object sender, EventArgs e)
    {
        int gid=Convert.ToInt32(classification_D.SelectedValue);
        BindClass2(gid);
    }

    private void BindClass2(int gid)
    {
        classification2_D.Items.Clear();
        classification3_D.Items.Clear();
        if (!string.IsNullOrEmpty(classification_D.SelectedValue))
        {
            DataTable dt = B_GradeOption.GetGradeList(gid, 0);
            classification2_D.DataSource = dt;
            classification2_D.DataBind();
            classification2_D.Items.Insert(0, new ListItem("请选择分类", ""));
        }
    }
    protected void classification2_D_SelectedIndexChanged(object sender, EventArgs e)
    {
        int gid=Convert.ToInt32(classification_D.SelectedValue);
        int pid=Convert.ToInt32(classification2_D.SelectedValue);
        BindClass3(gid, pid);
    }

    private void BindClass3(int gid, int pid)
    {
        classification3_D.Items.Clear();
        DataTable dt = B_GradeOption.GetGradeList(gid, pid);
        classification3_D.DataSource = dt;
        classification3_D.DataBind();
        classification3_D.Items.Insert(0, new ListItem("请选择分类", ""));
    }
}