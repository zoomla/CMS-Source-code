using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.Template.Code
{
    public partial class PageTlp : System.Web.UI.Page
    {
        M_UserInfo mu = null;
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //会员登录检测
                //B_User.CheckIsLogged(Request.RawUrl);
                //从缓存中获取当前登录用户
                //buser.GetLogin();
                //管理员登录检测
                B_Admin.CheckIsLogged();
            }
        }
        //返回多表中的数据
        public DataTable SelFromDB()
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Title", "%最新%") };
            return SqlHelper.JoinQuery("A.*,B.*", "ZL_CommonModel", "ZL_C_Article", "A.ItemID=B.ID", "A.Title LIKE @Title", "A.CreateTime DESC", sp);
        }
        //获取一个用户
        public M_UserInfo Sel(int uid)
        {
            mu = buser.SelReturnModel(uid);
            return mu;
        }
        //添加一个用户
        public void Insert()
        {
            mu = new M_UserInfo();
            mu.UserName = function.GetRandomString(6);
            mu.UserPwd = "123123";
            mu.Email = "webhx008@163.com";
            mu.Question = "How are you";
            mu.Answer = "This is no problem";
            mu.UserID = buser.Add(mu);
        }
        //更新用户信息
        public void Update(int uid)
        {
            mu = Sel(uid);
            mu.Email = "TestForUpdate@163.com";
            buser.UpdateByID(mu);
        }
        //删除指定用户
        public void Del(int uid)
        {
            buser.DelUserById(uid);
        }
    }
}