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
using ZoomLa.Components;
using ZoomLa.Model;
using System.Collections.Generic;

public partial class User_UserZone_School_ShowSchoolMessage : System.Web.UI.Page
{
    #region 业务对象
    B_User ubll = new B_User();
    //UserTableBLL utbll = new UserTableBLL();
    B_RoomMessage mebll = new B_RoomMessage();
    B_ClassRoom cll = new B_ClassRoom();
    public string RoomName = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetLogin();
            //初始化接收ID
            if (Request.QueryString["Roomid"] != null)
            {
                inceptID = int.Parse(Request.QueryString["Roomid"].ToString());
                RoomName = cll.GetSelect(inceptID).RoomName;
                GetMessage();
            }

        }

    }

    #region 页面调用方法
    //接收ID
    protected  int inceptID
    {
        get
        {
            if (ViewState["inceptID"] != null)
                return int.Parse(ViewState["inceptID"].ToString());
            else return 0;
        }
        set
        {
            ViewState["inceptID"] = value;
        }
    }

    //绑定留言信息
    private void GetMessage()
    {
        int CPage;
        int temppage;
        List<M_RoomMessage> list = mebll.GetMessageByInceptID(inceptID);
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
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = list;
        cc.AllowPaging = true;
        cc.PageSize = 5;
        cc.CurrentPageIndex = CPage - 1;
        this.DataList1.DataSource = cc;
        this.DataList1.DataBind();

        Allnum.Text = list.Count.ToString();
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

        Toppage.Text = "<a href=?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("?Currentpage=" + this.DropDownList1.Text);
    }

    //显示用户昵称
    protected string getusername(string uid)
    {
        return ubll.GetUserByUserID(int.Parse(uid)).UserName;

    }
    //显示用户头像
    protected string getuserpic(string uid)
    {
        return ubll.GetUserBaseByuserid(int.Parse(uid)).UserFace;
    }

    //添加留言
    protected void savebtn_Click(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        M_RoomMessage me = new M_RoomMessage();
        me.InceptID = inceptID;
        me.SendID = ubll.GetLogin().UserID;
        me.Mcontent = this.TEXTAREA1.Value.Replace("\n","<br/>");
        me.RestoreID = 0;
       
        mebll.InsertMessage(me);
        this.TEXTAREA1.Value = "";
        Response.Redirect("ShowSchoolMessage.aspx?Roomid=" + inceptID);
    }


    //绑定回复信息
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dataList2 = (DataList)e.Item.FindControl("DataList2");

        List<M_RoomMessage> list = mebll.GetRestoreMessageByID(int.Parse(DataList1.DataKeys[e.Item.ItemIndex].ToString()));
        if (list.Count > 0)
        {
            dataList2.DataSource = list;
            dataList2.DataBind();
        }
    }

    //删除留言
    protected void lbtsave_Click(object sender, EventArgs e)
    {
        LinkButton bt = sender as LinkButton;
        DataListItem di = bt.Parent as DataListItem;
        mebll.DelMessage(int.Parse(this.DataList1.DataKeys[di.ItemIndex].ToString()));
        GetMessage();
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        DataListItem dl = lb.Parent as DataListItem;
        DataList datalist = dl.Parent as DataList;
        string it = datalist.DataKeys[dl.ItemIndex].ToString();
        mebll.DelMessage(int.Parse(it));
        GetMessage();
    }

    ////删除回复
    //protected void lbtsave_Click(object sender, EventArgs e)
    //{
    //    DataList dataList2 = (DataList)this.DataList1.Item.FindControl("DataList2");

    //    LinkButton bt = sender as LinkButton;
    //    DataListItem di=bt.Parent as DataListItem;
    //    mebll.DelMessage(new Guid(this.DataList1.DataKeys[di.ItemIndex].ToString()));
    //    GetMessage();
    //} 
    #endregion
}
