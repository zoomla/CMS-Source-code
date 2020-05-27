using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class manage_Search_SearchFunc : CustomerPageAction
{
    B_Search b_search = new B_Search();
    //0:后台,3:用户
    //public int LinkType { get { return  DataConverter.CLng(Request.QueryString["linktype"]); } }
    public int LinkState { get { return DataConverter.CLng(Request.QueryString["state"]); } }
    protected void Page_Load(object sendergvCard, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li class=\"active\">" + Resources.L.管理导航 + "<a href=\"AddSearch.aspx\">[" + Resources.L.添加管理导航 + "]</a></li>");
        }
    }

    private void MyBind()
    {
        //List<M_Search> dt = b_search.GetSearchByCon(CustomerPageAction.customPath2);
        ////if (dt != null && dt.Count > 0)
        ////{
        ////    this.nocontent.Style["display"] = "none";
        ////    this.EGV.Visible = true;
        ////}
        ////else
        ////{
        ////    this.nocontent.Style["display"] = "";
        ////    this.EGV.Visible = false;
        ////}
        //if (LinkType == 3)
        //    dt = dt.FindAll(d => d.LinkType == 3);
        //else if (LinkType > 0)
        //    dt = dt.FindAll(d => d.LinkType != 3);
        DataTable dt = b_search.SelByType(1, customPath2, LinkState);
        this.EGV.DataSource = dt;
        this.EGV.DataBind();
    }

    //批量删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        b_search = new B_Search();
        if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
        {
            b_search.DelByIDS(Request.Form["chkSel"]);
        }
        else
            function.WriteErrMsg("删除失败，请重新选择！");
        Response.Redirect("SearchFunc.aspx");
    }
    public string GetItemIcon()
    {
        return StringHelper.GetItemIcon(Eval("ico").ToString());
    }
    //批量启用
    protected void Btnuse_Click(object sender, EventArgs e)
    {
        b_search = new B_Search();
        if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
        {
            b_search.UpdateStatusByIDS(Request.Form["chkSel"], 1);
        }
        else
            function.WriteErrMsg("启用失败，请重新选择！");
        Response.Redirect("SearchFunc.aspx");
    }

    //批量停用
    protected void Btnstop_Click(object sender, EventArgs e)
    {
        b_search = new B_Search();
        if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
        {
            b_search.UpdateStatusByIDS(Request.Form["chkSel"], 2);
        }
        else
            function.WriteErrMsg("停用失败，请重新选择！");
        Response.Redirect("SearchFunc.aspx");
    }


    //文件邦定事件
    protected void gvCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = DataConverter.CLng((e.Row.FindControl("hfId") as HiddenField).Value);
            e.Row.Attributes.Add("ondblclick", "location.href='AddSearch.aspx?menu=edit&id=" + id + "'");
            M_Search sea = b_search.GetSearchById(id);
            if (sea != null)
            {
                Image imgLinkType = e.Row.FindControl("imgLinkType") as Image;
                Label linkType = e.Row.FindControl("linkType") as Label;
                Label lblState = e.Row.FindControl("lblState") as Label;
                switch (sea.Type)
                {
                    case 0://站内链接  
                        if (sea.LinkState == 1 && sea.State == 1) //存在该文件并且启用
                        {
                            lblState.Text = "启用";
                        }
                        if (sea.LinkState == 2 || sea.State == 2)//不存在该文件
                        {
                            lblState.Text = "停用";
                        }
                        linkType.Text = "<i class='fa fa-folder-open' title='站内应用' style='color:#FF7A00;'></i>";
                        break;
                    case 1://用户中心
                        linkType.Text = "<i class='fa fa-list-alt' title='用户中心' style='color:#FF7A00;'></i>";
                        break;
                    case 2://站外链接
                        linkType.Text = "<i class='fa fa-folder-open' title='站内应用' style='color:#FF7A00;'></i>";
                        break;
                    default:
                        break;
                }
                if (sea.State == 2) //文件未启用
                {
                    lblState.Text = "停用";
                }
                if (sea.State == 1) //文件启用
                {
                    lblState.Text = "启用";
                }
                sea.FlieUrl = sea.FlieUrl.ToLower();
                if (sea.FlieUrl.StartsWith("http:") || sea.FlieUrl.StartsWith("https:"))
                    linkType.Text = "<i class='fa fa-chain' title='站外链接' style='color:#FF7A00;'></i>";
            }
        }
    }

    public string getDate(string date)
    {
        return string.Format("{0:d}", Convert.ToDateTime(date));
    }

    public string getOpentype(string opentype)
    {
        switch (opentype)
        {
            case "0":
                return "_self";
            case "1":
                return "_blank";
            case "2":
                return "_parent";
            case "3":
                return "_top";
            default:
                return "_self";
        }
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
        {
            string[] chkArr = Request.Form["chkSel"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
    public string IsMobile(object o)
    {
        string result = "<i class='{0}' title='{1}' style='color:#FF7A00;'/>";
        if (o.ToString().Equals("1"))
        {
            result = string.Format(result, "fa fa-check", "支持移动");
        }
        else
        {
            result = string.Format(result, "fa fa-close", "不支持");
        }
        return result;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void saveOrder_Btn_Click(object sender, EventArgs e)
    {
        //mid,oid,nid,wid
        string[] wid = Request.Form["order_T"].Split(',');//需要更换成的ID
        string[] ids = Request.Form["order_Hid"].Split(',');//信息描述
        for (int i = 0; i < wid.Length; i++)
        {
            if (wid[i] == "")
            {
                function.Script(this, "alert('排序不能为空!');");
                return;
            }
            int wantPid = DataConverter.CLng(wid[i]);
            int mid = DataConverter.CLng(ids[i].Split(':')[0]);
            int oid = DataConverter.CLng(ids[i].Split(':')[1]);
            int nowPid = DataConverter.CLng(ids[i].Split(':')[2]);
            if (wantPid == nowPid) continue;//没有修改排序值
            else
            {
                b_search.UpdateOrder(mid, wantPid);
            }// if end;
        }//for end;
        MyBind();
    }
}
