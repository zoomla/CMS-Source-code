using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
public partial class User_UserFriend_AddFave : System.Web.UI.Page
{

    B_User bu = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Favorite bf = new B_Favorite();
        M_Favorite mf = new M_Favorite();
        int fid = 0;
        M_UserInfo muinfo = bu.GetLogin();
        int UserID = muinfo.UserID;
        bu.CheckIsLogin();
        if (Request.QueryString["Fid"] != null)
        {
            fid = Convert.ToInt32(Request.QueryString["Fid"]);
        }
        if (Request.QueryString["type"] != null)
        {
            string type = Request.QueryString["type"];
            string str = "添加成功！！";
            if (UserID != fid)
            {
                switch (type)
                {
                    case "addfave":
                        if (bf.GetFavByUserIDAndFriendID(UserID, fid))
                        {
                            str = "此用户已经在您的收藏中！";
                        }
                        else
                        {
                            mf.Owner = UserID;
                            mf.InfoID = fid;
                            mf.AddDate = System.DateTime.Now;
                            bf.AddFavorite(mf);
                            str = "收藏成功！";
                        }
                        break;
                    case "addfav":
                        DataTable table = null;
                        if (table != null)
                        {
                            if (table.Rows.Count > 0)
                            {
                                str = "此用户已经在您的收藏中！";
                            }
                            else
                            {
                                str = "收藏成功！";
                            }
                        }
                        break;
                }
                Response.Write("<script>alert('" + str + "');history.go(-1);</script>");
            }
            else
            {
                Response.Write("<script>alert('不可加自己为好友或收藏');history.go(-1);</script>");
            }
        }
    }
}