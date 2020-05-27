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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Pages_ViewSmallPub : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_Admin badmin = new B_Admin();
    private B_ModelField bfield = new B_ModelField();
    private B_Model bmodel = new B_Model();
    private int m_type;
    private B_Pub pub = new B_Pub();
    public string tableName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            M_AdminInfo ad = badmin.GetAdminLogin();
            this.HiddenPubRole.Value = ad.PubRole.ToString();
            if (ad.PubRole==1)
                Button3.Visible=true;
            int pubid = DataConverter.CLng(Request.QueryString["Pubid"]);
            ViewState["pubid"] = pubid.ToString();

            M_Pub pubinfo = pub.GetSelect(pubid);
            string ModelID = (pubinfo.PubModelID == 0) ? "0" : pubinfo.PubModelID.ToString();

            if (DataConverter.CLng(ModelID) <= 0)
            {
                function.WriteErrMsg("无模块信息");
            }
            int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);
            this.HdnID.Value = ID.ToString();
            string type = (Request.QueryString["type"] == null) ? "0" : Request.QueryString["type"].ToString();
            M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));

            this.HdnModelID.Value = ModelID.ToString();
            this.HiddenType.Value = type;
            this.HiddenPubid.Value = pubid.ToString();
            this.ViewState["ModelID"] = ModelID.ToString();
            this.ViewState["cType"] = "1";
            Bind();
            //this.Label1.Text = "<a href='AddPub.aspx?Parentid=" + this.HdnID.Value + "&Pubid=" + this.HiddenPubid.Value + "'>[&nbsp;&nbsp;&nbsp;添加回复&nbsp;&nbsp;]</a>";

            int pubtype = pubinfo.PubType;
            string pubtypename = "";
            switch (pubtype)
            {
                case 0:
                    pubtypename = "评论";
                    break;
                case 1:
                    pubtypename = "投票";
                    break;
                case 2:
                    pubtypename = "活动";
                    break;
                case 3:
                    pubtypename = "留言";
                    break;
                case 4:
                    pubtypename = "调查";
                    break;
                case 5:
                    pubtypename = "统计";
                    break;
            }
            this.Label2.Text = pubtypename;
        }
    }

  
    /// <summary>
    /// GridView 翻页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
  

    private void RepNodeBind()
    {
        this.m_type = DataConverter.CLng(this.ViewState["cType"].ToString());
        DataTable UserData;

        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));
        try
        {
            switch (m_type)
            {
                case 1:
                    UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 13);
                    break;
                case 2:
                    UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 19);
                    break;
                case 3:
                    UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 20);
                    break;
                default:
                    UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 13);
                    break;
            }
            this.gvCard.DataSource = UserData;
            this.gvCard.DataBind();
        }
        catch (Exception)
        {
            function.WriteErrMsg("模型表[" + model.TableName + "]不存在!您可以点");
        }
    }
  
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "View")
        {

            Page.Response.Redirect("PubView.aspx?ID=" + e.CommandArgument.ToString() + "&Pubid=" + this.HiddenPubid.Value.ToString() + "&small=small");
        }
        if (e.CommandName == "Audit")
        {
            string Id = e.CommandArgument.ToString();
            int modeid = DataConverter.CLng(this.ViewState["ModelID"].ToString());
            M_ModelInfo aa = bmodel.GetModelById(modeid);



        }
        if (e.CommandName == "Del")
        {
            string Id = e.CommandArgument.ToString();
            buser.DelModelInfo(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, DataConverter.CLng(Id));

        }
        if (e.CommandName == "Edit")
            Page.Response.Redirect("EditPub.aspx?Pubid=" + this.HiddenPubid.Value.ToString() + "&ID=" + e.CommandArgument.ToString() + "&small=" + this.HdnID.Value);
        RepNodeBind();

    }
   

    protected void LbtnAllPub_Click(object sender, EventArgs e)
    {
        //所有评论
        this.m_type = 1;
        this.ViewState["cType"] = this.m_type.ToString();
        RepNodeBind();
    }
    protected void LbtnUNAuditedPub_Click(object sender, EventArgs e)
    {
        this.m_type = 3;
        this.ViewState["cType"] = this.m_type.ToString();
        RepNodeBind();
    }
    protected void LbtnuditedPub_Click(object sender, EventArgs e)
    {
        this.m_type = 2;
        this.ViewState["cType"] = this.m_type.ToString();
        RepNodeBind();
    }
    private void Bind()
    {
        int CPage, temppage;




        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        this.m_type = DataConverter.CLng(this.ViewState["cType"].ToString());
        DataTable Cll=new DataTable();
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));
        try
        {
            switch (m_type)
            {
                case 1:
                    Cll = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 13);
                    break;
                case 2:
                    Cll = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 19);
                    break;
                case 3:
                    Cll = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 20);
                    break;
                default:
                    Cll = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 13);
                    break;
            }
         
        }
        catch (Exception)
        {
            function.WriteErrMsg("模型表[" + model.TableName + "]不存在!您可以点");
        }
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 12;
        cc.CurrentPageIndex = CPage - 1;
        gvCard.DataSource = cc;
        gvCard.DataBind();

        Allnum.Text = Cll.DefaultView.Count.ToString();

        
        int thispagenull = cc.PageCount;//总页数
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//上一页
        int downpagenum = CPage + 1;//下一页
        int Endpagenum = thispagenull;

        if (thispagenull <= CPage)
        {
            downpagenum = thispagenull;
            Downpage.Enabled = false;
        }
        else
        {
            Downpage.Enabled = true;
        }


        if (nextpagenum <= 0)
        {
            nextpagenum = 0;
            Nextpage.Enabled = false;
        }
        else
        {
            Nextpage.Enabled = true;
        }

        Toppage.Text = "<a href=?Currentpage=0&pubid=" + this.HiddenPubid.Value + "&ID=" + Request.QueryString["ID"] + ">首页</a>";
        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&pubid=" + this.HiddenPubid.Value + "&ID=" + Request.QueryString["ID"] + ">上一页</a>";
        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&pubid=" + this.HiddenPubid.Value + "&ID=" + Request.QueryString["ID"] + ">下一页</a>";
        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&pubid=" + this.HiddenPubid.Value + "&ID=" + Request.QueryString["ID"] + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();


        if (!this.Page.IsPostBack)
        {
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
        }

        DropDownList1.SelectedValue = temppage.ToString();

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(HdnModelID.Value));
        string CID = Request.Form["Item"];
        if (!String.IsNullOrEmpty(CID) && buser.DelModelInfoAllo(model.TableName, CID))
        {
            Response.Write("<script language=javascript>alert('批量删除成功!');location.href='ViewPub.aspx?pubid=" + this.HiddenPubid.Value + "';</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='ViewPub.aspx?pubid=" + this.HiddenPubid.Value + "';</script>");
        }
    }
    public string showuse(string Pubstart, string id)
    {

        if (this.HiddenPubRole.Value == "1")
        {
            int ido = DataConverter.CLng(id);
            int sid = DataConverter.CLng(Pubstart);
            string str = "";
            //<a href="EditPubstart.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>" >审核</a>
            if (sid == 1)
            {
                str = "<a href=EditPubstart.aspx?Pubid=" + this.HiddenPubid.Value + "&ID=" + id + "&menu=true><span style='color:red;'>取消审核</span></a>";
            }
            else
            {
                str = "<a href=EditPubstart.aspx?Pubid=" + this.HiddenPubid.Value + "&ID=" + id + "&menu=false>审核</a>";
            }

            return str;
        }
        else
        {
            return "";
        }
    }

      //&nbsp;<a href="EditPub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>" >修改</a>
      //  &nbsp;<a href=" Delpub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%# Eval("ID")%>" OnClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a>

    public string showedit(object id, int type)
    {

        if (this.HiddenPubRole.Value == "1")
        {
            int ido = DataConverter.CLng(id);
            switch (type)
            {
                case 1:
                    return "<a href='EditPub.aspx?Pubid=" + this.HiddenPubid.Value + "&ID=" + ido + "&small=" + this.HdnID.Value + "'>修改</a>";

                case 2:
                    return "<a href='Delpub.aspx?Pubid=" + this.HiddenPubid.Value + "&ID=" + ido + "&small=" + this.HdnID.Value + "' OnClick='return confirm('不可恢复性删除数据,你确定将该数据删除吗？');'>删除</a>";
          
            }
              return "";
        }
        else
        {
            return "";
        }
    }
    public string returnlen(object str)
    {
        string intostr = str.ToString();
        return BaseClass.Left(intostr, 20);
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        Response.Redirect("ViewSmallPub.aspx?Pubid=" + Request.QueryString["Pubid"] + "&ID=" + Request.QueryString["ID"] + "&Currentpage=" + Request.Form["DropDownList1"] + "");
    }
}
