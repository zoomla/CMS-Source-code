using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text.RegularExpressions;
using ZoomLa.Components;

public partial class manage_Search_AddSearch : CustomerPageAction
{
    B_Search b_search = new B_Search();
    B_Admin badmin = new B_Admin();
    B_Group groupBll = new B_Group();
    M_Search searchMod =null;
    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "checkname":
                    result = CheckSearchName();
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush();Response.End();
            return;
        }
        if (!IsPostBack)
        {
            if (Mid > 0)
            {
                searchMod = b_search.GetSearchById(Mid);
                txtName.Text = searchMod.Name;
                //rdoType.SelectedValue = search.Type.ToString();
                txtFileUrl.Text = searchMod.FlieUrl;
                Edit_Btn.InnerText = "保存设置";
                EditSearchName_Hid.Value = searchMod.Name;
                hideid.Value = ID.ToString();
                ItemIcon_T.Text = searchMod.Ico;
                rdoOpenType.SelectedValue = searchMod.OpenType.ToString();
                SupportMobile.SelectedValue = searchMod.Mobile.ToString();
                MotroSize.SelectedValue = searchMod.Size.ToString();
                txtOrderID.Value = searchMod.OrderID.ToString();
                 
            } 
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href=\"SearchFunc.aspx\">管理导航</a></li><li class=\"active\">管理导航管理</li>");
        }
    }
    public string CheckSearchName()
    {
        DataTable dt = b_search.SelByName(Request.Form["name"], 1);
        if (dt.Rows.Count > 0)
        { return "-1"; }
        return "1";
    }
    //获取用户组选中状态
    public string GetChecked()
    {
        string groupid = Eval("GroupID").ToString();
        if (Mid > 0)
            searchMod = b_search.GetSearchById(Mid);
        if (searchMod != null && searchMod.UserGroup.Split(',').Contains(groupid))
            return "checked";
        else
            return "";
    }
    //添加搜索信息
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        b_search = new B_Search();
        M_Search search = new M_Search();
        search.Name = txtName.Text;
        //search.Type = DataConverter.CLng(rdoType.SelectedValue);
        search.FlieUrl = txtFileUrl.Text.Trim();
        string pic = ItemIcon_T.Text;
        if (pic != "")
        {
            search.Ico = pic;
        }
        search.Mobile = Convert.ToInt32(SupportMobile.SelectedValue);
        search.Size = Convert.ToInt32(MotroSize.SelectedValue);
        search.OpenType = DataConverter.CLng(rdoOpenType.SelectedValue);
        search.AdminID = badmin.GetAdminLogin().AdminId;
        Regex regexObj = new Regex(@"\b(?:(?:https?|ftp|file)://|www\.|ftp\.)[-A-Z0-9+&@#/%=~_|$?!:,.]*[A-Z0-9+&@#/%=~_|$]", RegexOptions.IgnoreCase);
        //try
        //{
        //    if (regexObj.IsMatch(txtFileUrl.Text.Trim()))
        //    {
        //        // Successful match// 链接类型：1为站内链接，2为站外链接
        //        search.LinkType = 2;
        //    }
        //    else
        //    {
        //        // Match attempt failed
        //        search.LinkType = 1;
        //    }
        //}
        //catch
        //{
        //    search.LinkType = 1;
        //    // Syntax error in the regular expression
        //}  
        ///search.LinkType =DataConverter.CLng(rdoLinkType.SelectedValue);
        //if (search.LinkType == 1)  //如果为站内链接就判断是否存在文件
        //{
        //    if (File.Exists(txtFileUrl.Text.Trim().Substring(1)))
        //    {
        //        search.Time = File.GetLastWriteTime(txtFileUrl.Text.Trim());
        //        search.LinkState = 1;
        //        search.State = 1;
        //    }
        //    else
        //    {
        //        search.Time = DateTime.Now;
        //        search.State = 1;
        //        search.LinkState = 2;
        //    }
        //}
        //else 
        //{
        //    search.State = 1;
        //    search.LinkState = 2;
        //    //search.Time = DataConverter.CDate("1755-1-1");
        //}
        search.State = 1;
        search.LinkState = 2;
        search.EliteLevel = 0;
        search.Type = 1;
        string pageUrl = search.Type == 1 ? "SearchFunc.aspx?EliteLevel=" + search.EliteLevel : "SearchFunc.aspx";
        if ( Mid > 0)
        {
            search.Id = Mid;
            search.OrderID = Convert.ToInt32(txtOrderID.Value);
            bool res = b_search.UpdateByID(search);
            if (res)
                function.WriteSuccessMsg("修改成功!", pageUrl);
            else
                function.WriteErrMsg("修改失败!");
        }
        else
        {
            search.OrderID = b_search.SelMaxOrder() + 1;
            int res = b_search.insert(search);
                function.WriteSuccessMsg("添加成功!", pageUrl);
        }
    }

}
