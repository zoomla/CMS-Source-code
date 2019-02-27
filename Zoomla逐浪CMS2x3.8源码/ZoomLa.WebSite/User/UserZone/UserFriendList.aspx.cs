using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
public partial class User_Guild_UserFriendList : System.Web.UI.Page
{
    private B_User user = new B_User();
    private int CurrentPageNum = 0;
    private string sqlWhere = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            user.CheckIsLogin();
            if (Request.QueryString["Currentpage"] != null)
            {
                CurrentPageNum = DataConverter.CLng(Request.QueryString["Currentpage"]);
            }
            this.GetFriendSexBind();
            //this.GetCityBind();
        }
    }
    //绑定性别
    private void GetFriendSexBind()
    {
    
        if (Request.Form["sex"] != null)
        {
            int sex = DataConverter.CLng(Request.Form["sex"]);
            if (sex == 1)
            {
                sqlWhere = " where UserSex=1";
            }
            else if (sex == 2)
            {
                sqlWhere = "where UserSex=0";
            }
        }
       
         if (Request.Form["age"] != null)
        {
           

            int age = DataConverter.CLng(Request.Form["age"]);
            if (age == 1)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())<=18";
            }
            else if (age == 2)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=18 and DateDiff(year,BirthDay,getdate())<=25";
            }
            else if (age == 3)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=25 and DateDiff(year,BirthDay,getdate())<=33";
            }
            else if (age == 4)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=33 and DateDiff(year,BirthDay,getdate())<=45";
            }
            else if(age==5)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=45";
            }
        }
       
        this.GetRepeaterBind(sqlWhere);
    }
    //绑定年龄
    private void GetFriendAgeBind()
    {
        if (Request.Form["age"] != null)
        {
            int age = DataConverter.CLng(Request.Form["age"]);
            if (age == 1)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())<=18";
            }
            else if (age==2)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=18 and DateDiff(year,BirthDay,getdate())<=25";
            }
            else if (age == 3)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=25 and DateDiff(year,BirthDay,getdate())<=33";
            }
            else if (age == 4)
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=33 and DateDiff(year,BirthDay,getdate())<=45";
            }
            else
            {
                sqlWhere = "where DateDiff(year,BirthDay,getdate())>=45";
            }
        }
        this.GetRepeaterBind(sqlWhere);
    }
    //绑定城市
    private void GetCityBind()
    {

         if (Request.Form["province"] != null && Request.Form["city"] != null)
         {
            string Province = Request.Form["province"].ToString();
            string City = Request.Form["city"].ToString();
            sqlWhere = " where Province=" + Province + " and County=" + City;

        }
        //this.GetRepeaterBind(sqlWhere);
    }
    //分页
    private void GetRepeaterBind(string sqlWhere)
    {
        //DataTable dt = user.GetUserNameFriendWhereList(sqlWhere);
        DataTable dt = new DataTable();
        this.lblFriendCount.Text = dt.Rows.Count.ToString();
        if (dt == null)
        {
            panelpage.Visible = false;
            this.Repeater1.DataSource = null;
            this.Repeater1.DataBind();
        }

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dt.DefaultView;
        pds.CurrentPageIndex = CurrentPageNum;
        pds.PageSize = 10;       //每页显示的项数      
        int pagesize = pds.PageSize;
        int count = pds.Count;//总项数
        pds.AllowPaging = true;
        this.Repeater1.DataSource = pds;
        this.Repeater1.DataBind();
        int PageAll = pds.PageCount;//总页数
        CurrentPageNum = pds.CurrentPageIndex;//当前页数
        int RePagenum = CurrentPageNum - 1;//一页
        int NextPagenum = CurrentPageNum + 1;//下一页       
        int Endpagenum = PageAll - 1;//最后一页等于总页数     
        if (CurrentPageNum <= 0)//当前页数小于等于0
        {
            RePagenum = 0;
            RePage.Enabled = false;

        }
        if (CurrentPageNum >= pds.PageCount - 1)//当前页数大于或等于总页数
        {
            NextPage.Enabled = false;//下一页不可用
            NextPagenum = CurrentPageNum;

        }
        PageSize.Text = pds.PageSize.ToString();
        PageCount.Text = PageAll.ToString();
        Count.Text = count.ToString();
        CurrentPage.Text = (CurrentPageNum + 1).ToString();
        FirstPage.Text = "<a href=UserFriendList.aspx?Currentpage=" + 0 + ">首页</a>";
        RePage.Text = "<a href=UserFriendList.aspx?Currentpage=" + RePagenum.ToString() + ">上一页</a>";
        NextPage.Text = "<a href=UserFriendList.aspx?Currentpage=" + NextPagenum.ToString() + ">下一页</a>";
        EndPage.Text = "<a href=UserFriendList.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        this.RePage.Enabled = true;
        this.NextPage.Enabled = true;
        if (CurrentPageNum == 0)
        {
            FirstPage.Text = "首页";
            FirstPage.Enabled = false;
        }
        if (CurrentPageNum <= 0)//当前页数小于等于0
        {
            RePage.Enabled = false;
            RePage.Text = "上一页";
        }
        if (CurrentPageNum >= pds.PageCount - 1)//当前页数大于或等于总页数
        {
            NextPage.Enabled = false;//下一页不可用               
            NextPage.Text = "下一页";
        }
        if (CurrentPageNum == PageAll - 1)
        {
            EndPage.Enabled = false;
            EndPage.Text = "尾页";
        }
        
    }

    public string GetImg(string url)
    {
        if (url.IndexOf("~") > -1)
        {
            return url.Substring(1);
        }
        else
        {
            return url;
        }
    }
    
    //用户名
    public string GetUserName(object uid)
    {
        string username = "";
        username = user.GetUserByUserID(DataConverter.CLng(uid)).UserName;
        return username;
    }
    //性别
    public string GetUserSex(object uid)
    {
        string UserSex = "";
        string sex = "";
        UserSex = user.GetUserBaseByuserid(DataConverter.CLng(uid)).UserFace.ToString();
        if (UserSex == "True")
        {
            sex = "男";
        }
        else
        {
            sex = "女";
        }
        return sex;
    }
    //年龄
    public string GetUserAge(object uid)
    {
        string age = "";
        age = (DateTime.Now.Year - DataConverter.CDate(user.GetUserBaseByuserid(DataConverter.CLng(uid)).BirthDay).Year).ToString();
        return age;
    }
    //地区
    public string GetUserCity(string uid)
    {
        string City = "";
        City = user.GetUserBaseByuserid(DataConverter.CLng(uid)).County;
        //List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //list2.Add(new ListItem("","01"));
        return "";
    }
}
