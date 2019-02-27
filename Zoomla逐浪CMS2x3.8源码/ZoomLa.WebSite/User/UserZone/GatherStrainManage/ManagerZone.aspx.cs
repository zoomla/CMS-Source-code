using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BDUModel;
using BDUBLL;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using BDULogic;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using System.Globalization;

public partial class User_UserZone_GatherStrainManage_ManagerZone : Page
{
    #region 业务对象
    B_User ubll = new B_User();
    public Guid GSID
    {
        get
        {
            if (ViewState["GSID"] != null)
            {
                return new Guid(ViewState["GSID"].ToString());
            }
            else return Guid.Empty;
        }
        set
        {
            ViewState["GSID"] = value;
        }
    }

    //GSManageBLL gsbll = new GSManageBLL();
    PagedDataSource pads = new PagedDataSource();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //ubll.CheckIsLogin();
        //string type = Request["type"];
        //if (!IsPostBack)
        //{
        //    GetGS();
        //    if (type == "2" )
        //    {
        //        DV_Invite.Visible = false;
        //        ViewState["GSID"] = Page.Request.QueryString["GSID"];
        //        string str = "root=3";
        //        pads.DataSource = GSManageLogic.GetUserMemberBystr(GSID, str).DefaultView;
        //        if (pads.Count == 0)
        //        {
        //            addmanager.Visible = false;
        //            LB_Q.Visible = true;
        //            LB_Q.Text = "当前无其他会员";
        //        }
        //        else
        //        {
        //            this.RP_Show.DataSource = pads;
        //            this.RP_Show.DataBind();
        //        }
        //        LB_Name.Text = "当前副群主";
        //        string st = "root=2";
        //        DataTable lis = GSManageLogic.GetUserMemberBystr(GSID, st);
        //        if (lis.Rows.Count == 0)
        //        {
        //            DV_XS.Visible = false;
        //            LB_show.Visible = true;
        //            LB_show.Text = "当前无副群主";
        //        }
        //        else
        //        {
        //        this.Repeater2.DataSource = lis;
        //        this.Repeater2.DataBind();  
        //         } 
        //    }
        //    else if(type == "4")
        //    {
        //        DV_Invite.Visible = false;
        //        ViewState["GSID"] = Page.Request.QueryString["GSID"];
        //        string str = "root=3";
        //        pads.DataSource = GSManageLogic.GetUserMemberBystr(GSID, str).DefaultView;
        //        if (pads.Count == 0)
        //        {
        //            addmanager.Visible = false;
        //            LB_Q.Visible = true;
        //            LB_Q.Text = "当前无其他会员";
        //        }
        //        else
        //        {
        //            this.RP_Show.DataSource = pads;
        //            this.RP_Show.DataBind();
        //        }
        //        LB_Name.Text = "当前受冻结的会员";
        //        string st = "root=4";
        //        DataTable lis = GSManageLogic.GetUserMemberBystr(GSID, st);
        //        if (lis.Rows.Count == 0)
        //        {
        //            DV_XS.Visible = false;
        //            LB_show.Visible = true;
        //            LB_show.Text = "当前无受冻结会员";
        //        }
        //        else
        //        {
        //        this.Repeater2.DataSource = lis;
        //        this.Repeater2.DataBind();  
        //         }
                
        //    }
        //    else if ( type == "6")
        //    {
                
        //        addmanager.Visible = false;
        //        ViewState["GSID"] = Page.Request.QueryString["GSID"];
        //        GetUser();
        //        LB_Name.Text = "当前受邀请的会员";
        //        string st = "root=6";
        //        DataTable lis = GSManageLogic.GetUserMemberBystr(GSID, st);
        //        if (lis.Rows.Count == 0)
        //        {
        //            DV_XS.Visible = false;
        //            LB_show.Visible = true;
        //            LB_show.Text = "当前无受邀请会员";
        //        }
        //        else
        //        {
        //            this.Repeater2.DataSource = lis;
        //            this.Repeater2.DataBind();
        //        }
        //    }
        //    else if (type == "5") 
        //    {
        //        DV_Invite.Visible = false;
        //        ViewState["GSID"] = Page.Request.QueryString["GSID"];
        //        pads.DataSource = GSManageLogic.GetUserBystr(GSID, "root<=6").DefaultView;
        //        if (pads.Count == 0)
        //        {
        //            addmanager.Visible = false;
        //            LB_Q.Visible = true;
        //            LB_Q.Text = "当前无其他会员";
        //        }
        //        else
        //        {
        //            this.RP_Show.DataSource = pads;
        //            this.RP_Show.DataBind();
        //        }
        //        LB_Name.Text = "当前拉黑人名单";
        //        string st = "root=5";
        //        DataTable lis = GSManageLogic.GetUserMemberBystr(GSID, st);
        //        if (lis.Rows.Count == 0)
        //        {
        //            DV_XS.Visible = false;
        //            LB_show.Visible = true;
        //            LB_show.Text = "当前无拉黑会员";
        //        }
        //        else
        //        {
        //            this.Repeater2.DataSource = lis;
        //            this.Repeater2.DataBind();
        //        } 
            //}
        ////}
       
        //pads.AllowPaging = true;
        //pads.PageSize = 2;
        //Allnum.Text = pads.PageCount.ToString();
        //int CurrentPage;
        //if (Request.QueryString["page"] != null)
        //{
        //    CurrentPage = Convert.ToInt32(Request.QueryString["page"]);
        //}
        //else
        //{
        //    CurrentPage = 1;
        //}
        //pads.CurrentPageIndex = CurrentPage - 1;
        //Nowpage.Text = CurrentPage.ToString();
        //if (!pads.IsFirstPage)
        //{
        //    lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + Convert.ToString(CurrentPage - 1) + "&GSID=" + GSID + "&type=" + type; 
        //}
        //if (!pads.IsLastPage)
        //{
        //    lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + Convert.ToString(CurrentPage + 1)+"&GSID="+GSID+"&type="+type;
        //}
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion

    private void GetGS()
    {
        //string id= Request["GSID"].ToString();
        //M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
        //GatherStrain gs = gsbll.GetGatherStrainByID(new Guid(id));
        //imgGSICQ.ImageUrl = PicUrl(gs.GSICO);
    }

    public string PicUrl(string picUrl)
    {
        string pic = picUrl;

        if (!string.IsNullOrEmpty(pic)) { pic = pic.ToLower(); }
        if (!string.IsNullOrEmpty(pic) && (pic.IndexOf(".gif") > -1 || pic.IndexOf(".jpg") > -1 || pic.IndexOf(".png") > -1))
        {
            string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";
            if (pic.StartsWith("~/"))
            {
                pic.Replace("~/", "/");
            }

            else if (pic.StartsWith("http://", true, CultureInfo.CurrentCulture) || pic.StartsWith("/", true, CultureInfo.CurrentCulture) || pic.StartsWith(delpath, true, CultureInfo.CurrentCulture))
                pic = "/" + pic;
            else
            {
                pic = "/" + delpath + "/" + pic;
            }
        }
        else
        {
            pic = "/UploadFiles/nopic.gif";
        }

        return pic;
    }
    protected void RP_Show_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //int id =Convert.ToInt32( e.CommandArgument);
        //int type =Convert.ToInt32( Request["type"]);

        //if (e.CommandName == "add")
        //{
            
        //    if (type == 2)
        //    {
        //        GSManageLogic.SetUserRoot(id, GSID, type);
        //        Response.Write("<script type=text/javascript>alert('保存成功！'); </script>");
        //        ((LinkButton)e.Item.FindControl("add")).Text = "已添加";
        //        ((LinkButton)e.Item.FindControl("add")).Enabled = false;
        //    }
        //    else if (type == 4)
        //    {
        //        GSManageLogic.SetUserRoot(id, GSID, type);
        //        Response.Write("<script type=text/javascript>alert('冻结成功！'); </script>");
        //        ((LinkButton)e.Item.FindControl("add")).Text = "已冻结";
        //        ((LinkButton)e.Item.FindControl("add")).Enabled = false;
        //    }
        //    else if(type==5)
        //    {
        //        GSManageLogic.AddUserToGS(id,GSID,5);
        //        Response.Write("<script type=text/javascript>alert('拉黑成功！'); </script>");
        //        ((LinkButton)e.Item.FindControl("add")).Text = "已拉黑";
        //        ((LinkButton)e.Item.FindControl("add")).Enabled = false;
        //    }
        //}
    }

    private void GetUser()
    {
        //string str = "root<=6";
        //pads.DataSource = GSManageLogic.GetUserBystr(GSID, str).DefaultView;
        //if (pads.Count == 0)
        //{
        //    addmanager.Visible = false;
        //    LB_Q.Visible = true;
        //    LB_Q.Text = "当前无其他会员可邀请";
        //}
        //else
        //{
        //    this.RP_Invite.DataSource = pads;
        //    this.RP_Invite.DataBind();
        //}
    }
    protected void RP_Show_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        int type = Convert.ToInt32(Request["type"]);
        DataTable GS = Sql.Sel("ZL_Sns_GatherStrain","ID='"+GSID+"'","");
        DataTable staff = Sql.Sel("ZL_User", "UserID", id);

        if (e.CommandName == "Invite")
        {
            M_Message messInfo = new M_Message();
            messInfo.Sender = "族群管理员";
            messInfo.Title = "邀请入群";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
            messInfo.Content ="《"+GS.Rows[0]["GSName"].ToString()+ "》族群邀请你加入，请到“我的空间”里族群板块进行添加加入。";
            messInfo.Receipt = "";
            messInfo.Incept =staff.Rows[0]["UserName"].ToString();
            //GSManageLogic.AddUserToGS(id, GSID, 6);
            Response.Write("<script type=text/javascript>alert('邀请短消息通知已发出！'); </script>");
            ((LinkButton)e.Item.FindControl("Invite")).Text = "已邀请";
            ((LinkButton)e.Item.FindControl("Invite")).Enabled = false;
        }
    }
    protected void RP_Show_ItemCommand2(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        int type = Convert.ToInt32(Request["type"]);

        if (e.CommandName == "cancel")
        {

            //if (type == 2)
            //{
            //    GSManageLogic.SetUserRoot(id, GSID, 3);
            //    Response.Write("<script type=text/javascript>alert('操作成功！'); </script>");
            //    ((LinkButton)e.Item.FindControl("cancel")).Text = "已撤销";
            //    ((LinkButton)e.Item.FindControl("cancel")).Enabled = false;
            //}
            //else if (type == 4)
            //{
            //    GSManageLogic.SetUserRoot(id, GSID, 3);
            //    Response.Write("<script type=text/javascript>alert('操作成功！'); </script>");
            //    ((LinkButton)e.Item.FindControl("add")).Text = "已撤销";
            //    ((LinkButton)e.Item.FindControl("add")).Enabled = false;
            //}
            //else if (type == 5)
            //{
            //    GSManageLogic.delGSuse(id,GSID);
            //    Response.Write("<script type=text/javascript>alert('操作成功！'); </script>");
            //    ((LinkButton)e.Item.FindControl("add")).Text = "已撤销";
            //    ((LinkButton)e.Item.FindControl("add")).Enabled = false;
            //}
            //else if (type == 6)
            //{
            //    GSManageLogic.delGSuse(id, GSID);
            //    DataTable GS = Sql.Sel("ZL_Sns_GatherStrain", "ID='" + GSID + "'", "");
            //    DataTable staff = Sql.Sel("ZL_User", "UserID", id);
            //    M_Message messInfo = new M_Message();
            //    messInfo.Sender = "族群管理员";
            //    messInfo.Title = "邀请入群";
            //    messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
            //    messInfo.Content = "《" + GS.Rows[0]["GSName"].ToString() + "》族群已取消了对你的邀请。";
            //    messInfo.Receipt = "";
            //    messInfo.Incept = staff.Rows[0]["UserName"].ToString();
            //    Response.Write("<script type=text/javascript>alert('短消息发出，操作成功！'); </script>");
            //    ((LinkButton)e.Item.FindControl("add")).Text = "已撤销";
            //    ((LinkButton)e.Item.FindControl("add")).Enabled = false;
            //}
        }
    }
}