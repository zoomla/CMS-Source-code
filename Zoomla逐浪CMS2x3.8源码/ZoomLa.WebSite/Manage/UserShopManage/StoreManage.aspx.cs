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
using ZoomLa.Common;
using ZoomLa.Web;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.Model;
using ZoomLa.BLL.Helper;

public partial class manage_UserShopManage_StoreManage : CustomerPageAction
{
    B_User bubll = new B_User();
    B_Content cbll = new B_Content();
    M_Message messInfo = new M_Message();
    B_User bll = new B_User();
    B_Content bcontent = new B_Content();
    protected DataTable modeinfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            DataBind();
        }
        Option();
        this.ids.Text = Request["id"];
        this.types.Text = Request["type"];
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Shop/ProductManage.aspx'>商城管理</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li class='active'>店铺审核</li>");
    }
    public void DataBind(string key = "")
    {
        B_ModelField mll = new B_ModelField();
        DataTable list = mll.SelectTableName("ZL_CommonModel", "Tablename like 'ZL_Store_%'  order by GeneralID desc");
        switch (Request.QueryString["type"])
        {
            case "0":
                list.DefaultView.RowFilter = "Status=99";
                break;
            case "1":
                list.DefaultView.RowFilter = "Status=0";
                break;
            case "2":
                list.DefaultView.RowFilter = "Status=99 AND EliteLevel=1";
                break;
            default:
                break;
        }
        this.modeinfo = list;
        Egv.DataSource = list.DefaultView.ToTable();
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    public void Option()
    {
        if (!string.IsNullOrEmpty(Request["menu"]))
        {
            DataBind();
            string uname = string.Empty;
            M_CommonData b_CommonData = cbll.GetCommonData(DataConverter.CLng(Request.QueryString["id"]));
            switch (Request["menu"])
            {
                case "Audit":
                    messInfo.Sender = "1";
                    messInfo.Title = "店铺审核通知_通过";
                    messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
                    messInfo.Content = "店铺审核通知_通过";
                    messInfo.Receipt = "";
                    uname = b_CommonData.Inputer;
                    string[] namearrr = uname.Split(new char[] { ',' });
                    for (int i = 0; i < namearrr.Length; i++)
                    {
                        messInfo.Incept = namearrr[i];
                        B_Message.Add(messInfo);
                    }
                    b_CommonData.Status = 99;
                    cbll.Update(b_CommonData); Response.Write("<script language=javascript>alert('通过审核!并已发送短消息通知');location.href='StoreManage.aspx';</script>");
                    break;
                default: break;
            }
        }
    }
    #region 页面方法
    //初始化
    protected string GetStorename(string gid)
    {
        M_CommonData dtinfo = cbll.GetCommonData(DataConverter.CLng(gid));
        return dtinfo.Title;
    }
    protected int GetUserID(string uname)
    {
        B_User buser = new B_User();
        return buser.GetUserByName(uname).UserID;
    }
    protected string GetAddtime(string gid)
    {
        M_CommonData dtinfo = cbll.GetCommonData(DataConverter.CLng(gid));
        return dtinfo.CreateTime + "";
    }
    protected string GetState(string gid)
    {
        M_CommonData CData = cbll.GetCommonData(DataConverter.CLng(gid));

        if (this.modeinfo.Rows.Count > 0)
        {
            switch (CData.Status)
            {
                case 99:
                    if (DataConverter.CLng(this.modeinfo.Rows[0]["EliteLevel"]) == 1)
                        return "推荐";
                    else
                        return "<span class='rd_green'>已审核</span>";
                case 0:
                    return "<span class='rd_red'>未通过</span>";
                default:
                    return "<span class='rd_red'>关闭</span>";
            }
        }
        else
        {
            return "";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Button bt = sender as Button;
            string type = bt.CommandName;
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    M_CommonData cinfo = cbll.GetCommonData(Convert.ToInt32(chkArr[i]));
                    switch (type)
                    {
                        case "1"://推荐
                            cinfo.EliteLevel = 1;
                            cinfo.Status = 99;
                            cbll.Update(cinfo);
                            break;
                        case "2"://关闭
                            cinfo.Status = -1;
                            cbll.Update(cinfo);
                            break;
                        case "3"://取消推荐
                            cinfo.EliteLevel = 0;
                            cbll.Update(cinfo);
                            break;
                        case "4"://取消关闭
                            cinfo.Status = 99;
                            cbll.Update(cinfo);
                            break;
                        case "5"://批量删除
                            cbll.DelContent(Convert.ToInt32(chkArr[i]));
                            break;
                    }
                }
                DataBind();
            }
        }
        catch (Exception ee)
        {
            function.WriteErrMsg(ee.Message);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        cbll.DelContent(int.Parse(lb.CommandName));
        DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        M_CommonData CData = cbll.GetCommonData(int.Parse(lb.CommandName));
        CData.Status = 99;
        cbll.Update(CData);
        DataBind();
    }
    public string GetStatus(string gid)
    {
        return Eval("Status").ToString().Equals("99") ? "<span style='color:green'>已审核</span>" : "<span>未通过</span>";
        //if (Eval("Status").ToString() == "99")
        //{
        //    //<a  href="#TB_inline?height=200&amp;width=500&amp;inlineId=hidCont&amp;modal=true" class="btn_demo thickbox"   >QQQQ</a>
        //    return "<a  title='点击取消审核' href='?id=" + gid + "&ShowMsg=true'  ><span style='color:green'>未通过</span></a>&nbsp;&nbsp;<a href='?id=" + gid + "' title='通过审核'><span style='color:red'>通过审核</span></a>";
        //}
        //else
        //{
        //    return "<a href='?id=" + gid + "' title='取消审核'><span style='color:red'>未通过</span></a>&nbsp;&nbsp;<a href='?menu=Audit&id=" + gid + "' title='点击通过审核'><span style='color:green'>通过审核</span></a>";
        //}
    }
    #endregion
    protected void Batch_Click(object sender, EventArgs e)
    {
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            Button bt = sender as Button;
            if (bt.CommandName == "5")
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    cbll.DelContent(Convert.ToInt32(chkArr[i]));
                }
            }
            if (bt.CommandName == "1")
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    M_CommonData cinfo = cbll.GetCommonData(Convert.ToInt32(chkArr[i]));
                    if (cinfo.Status != 99)
                    {
                        string uname = string.Empty;
                        messInfo.Sender = "1";
                        messInfo.Title = "店铺审核通知_通过";
                        messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
                        messInfo.Content = "已通过";
                        messInfo.Receipt = "";
                        uname = cinfo.Inputer;
                        string[] namearr = uname.Split(new char[] { ',' });
                        for (int j = 0; j < namearr.Length; j++)
                        {
                            messInfo.Incept = namearr[j];
                            B_Message.Add(messInfo);
                        }
                    }
                    cinfo.Status = 99;
                    cbll.Update(cinfo);
                }
                function.WriteSuccessMsg("批量审核成功，并已发送短消息！", CustomerPageAction.customPath2 + "UserShopManage/StoreManage.aspx");
            }
            if (bt.CommandName == "2")
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    M_CommonData cinfo = cbll.GetCommonData(Convert.ToInt32(chkArr[i]));
                    if (cinfo.Status != 0)
                    {
                        string uname = string.Empty;
                        messInfo.Sender = "1";
                        messInfo.Title = "店铺审核通知_不通过";
                        messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
                        messInfo.Content = "不通过";
                        messInfo.Receipt = "";
                        uname = cinfo.Inputer;
                        string[] namearr = uname.Split(new char[] { ',' });
                        for (int j = 0; j < namearr.Length; j++)
                        {
                            messInfo.Incept = namearr[j];
                            B_Message.Add(messInfo);
                        }
                    }
                    cinfo.Status = 0;
                    cbll.Update(cinfo);
                }
                function.WriteSuccessMsg("批量取消审核成功，并已发送短消息！", CustomerPageAction.customPath2 + "UserShopManage/StoreManage.aspx");
            }
        }
        else
            function.WriteErrMsg("请指定店铺！", CustomerPageAction.customPath2 + "UserShopManage/StoreManage.aspx");
        DataBind();
    }
    protected void msgsendBtn_Click(object sender, EventArgs e)
    {
        M_CommonData b_CommonData = cbll.GetCommonData(DataConverter.CLng(Request.QueryString["id"]));
        string uname = string.Empty;
        b_CommonData.Status = 0;
        cbll.Update(b_CommonData);
        messInfo.Sender = "1";
        messInfo.Title = "店铺审核通知_未通过";
        messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
        messInfo.Content = this.msgContent.Text;
        messInfo.Receipt = "";
        uname = b_CommonData.Inputer;
        string[] namearr = uname.Split(new char[] { ',' });
        for (int i = 0; i < namearr.Length; i++)
        {
            messInfo.Incept = namearr[i];
            B_Message.Add(messInfo);
        }
        Response.Write("<script language=javascript>alert('店铺未通过审核，并已发送短消息通知!');location.href='StoreManage.aspx';</script>");
        Response.Flush();
        Response.Close();
        Response.Write("<script language=javascript>tb_remove();</script>");
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
    //搜索店铺
    public void SearchStore(object sender, EventArgs e)
    {
        DataTable dt = cbll.SelBySkey(SKey.Text);
        modeinfo = dt;
        Egv.DataSource = dt;
        Egv.DataBind();
    }

    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='StroeUpdate.aspx?username=" + dr["Inputer"].ToString() + "'");
        }
    }
    public string GetIpLocation(string ip)
    {
        return IPScaner.IPLocation(ip);
    }
}