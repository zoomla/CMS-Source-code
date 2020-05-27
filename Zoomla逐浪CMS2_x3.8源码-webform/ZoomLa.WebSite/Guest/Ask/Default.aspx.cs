using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ZoomLa.Components;

public partial class Guestbook_Ask : System.Web.UI.Page
{
    protected B_User b_User = new B_User();//基本用户BLl
    protected M_UserInfo m_UserInfo = new M_UserInfo();
    protected B_Ask b_Ask = new B_Ask();//问题BLL
    protected M_Ask m_Ask = new M_Ask();
    B_GuestAnswer b_Ans=new B_GuestAnswer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo info = b_User.GetLogin();
            bindType();
            bindElit();
            bindNew();
            bindscore();
            bindlatest();
            bindRepeater7();
            bindRepeater9();
            bindRepeater10();
            bindRepeater11();
        }

    }

    protected string getstyle()
    {
        if (b_User.CheckLogin())
        {
            return "display:inline-table";
        }
        else return "display:none";
    }
    protected string getstyles()
    {
        if (b_User.CheckLogin())
        {
            return "display:none";
        }
        else return "display:inline-table";
    }
    /// <summary>
    /// 取已解决问题总数
    /// </summary>
    /// <returns></returns>
    public string getSolvedCount()
    {
        return b_Ask.IsExistInt("Status=2").ToString();
    }
    /// <summary>
    /// 取待解决问题总数
    /// </summary>
    /// <returns></returns>
    public string getSolvingCount()
    {
        return b_Ask.IsExistInt("Status=1").ToString();
    }
    /// <summary>
    /// 取得用户总数
    /// </summary>
    /// <returns></returns>
    public string getUserCount()
    {
        return b_User.GetUserNameListTotal("").ToString();
    }
    /// <summary>
    /// 取得当前在线人数
    /// </summary>
    /// <returns></returns>
    public string getLogined()
    {
        DateTime dtNow = DateTime.Now.AddMinutes(-1);
        if (Application["online"] != null)
            return Application["online"].ToString();
        else
            return "";
    }
    ///<summary>
    ///取得最佳回答采纳率
    ///</summary>
    /// <returns></returns>
    public string getAdoption()
    {
        double adopCount = b_Ans.IsExistInt(2);
        double count = b_Ans.getnum();
        return ((adopCount / count) * 100).ToString("0.00") + "%";
    }

    protected void bindType()
    {  
        DataTable dt = B_GradeOption.GetGradeList(2, 0);
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)//指触发的类型为DadaList中的基本行或内容行
        {
            Repeater rep = e.Item.FindControl("Repeater2") as Repeater;//找到里层的repeater对象
            DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
            int Gid = DataConvert.CLng(rowv["GradeID"]);
            DataTable dt = B_GradeOption.GetGradeList(2, Gid);
            rep.DataSource = dt;
            rep.DataBind();
        }
    }
    protected void bindElit()
    {
        DataTable dt = b_Ask.Sel("Elite=1", "ID desc",null);
        Repeater3.DataSource = dt;
        Repeater3.DataBind();
    }
      protected void bindNew()
    {
        DataTable dt = b_Ask.Sel("Status=1", "ID desc",null);
        Repeater4.DataSource = dt;
        Repeater4.DataBind();
    }
      protected void bindscore()
      {

          DataTable dt = b_Ask.Sel("Score<>0", "", null);
          Repeater5.DataSource = dt;
          Repeater5.DataBind();
      }
      protected void bindlatest()
      {  
              DataTable dtss = b_Ask.Sel("Status=2", "AddTime desc", null);
              Repeater6.DataSource = dtss;
              Repeater6.DataBind();
      }
      protected void bindRepeater7()
      {
          DataTable dt = b_Ask.SelUser();//根据被采纳问题数取知道之星
          Repeater7.DataSource = dt;
          Repeater7.DataBind();
      }
      protected void Repeater7_ItemDataBound(object sender, RepeaterItemEventArgs e)
      {
          if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)//指触发的类型为DadaList中的基本行或内容行
          {
              Repeater rep = e.Item.FindControl("Repeater8") as Repeater;
              DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
              int userID = Convert.ToInt32((rowv["UserID"]));
              DataTable dt = b_Ask.Sel("UserId=" + userID, "AddTime desc", null);
              rep.DataSource = dt;
              rep.DataBind();
              HtmlAnchor fix = (HtmlAnchor)e.Item.FindControl("fix");
              if (userID == b_User.GetLogin().UserID)
              {
                  fix.Visible = false;
              }
              else
              {
                  fix.HRef = "Add.aspx?fix=" + userID;
              }
          }
      }
    protected int  getcount(string id)
    {
        if (id != "")
        {
            int number;
            DataTable dt = b_Ans.Sel(" QueId=" + Convert.ToInt32(id) + " and supplyment=0", "",null);
            if (dt.Rows.Count > 0)
            {
                number =dt.Rows.Count;
                return number;
            }
            else return 0;
        }
        else return 0;
    
    }

    protected string gettype(string id)
    {
        string result = "";
        if (!string.IsNullOrWhiteSpace(id))
        {
            DataTable dt = Sql.Sel("zl_grade", " GradeID=" + Convert.ToInt32(id), "");
            if (dt.Rows.Count > 0)
            {
                result = (dt.Rows[0]["GradeName"]).ToString();
            }
        }
        return result;
    }
    /// <summary>
    /// 获取回答数
    /// </summary>
    /// <returns></returns>
    protected string getanswer(string uid)
    {
        return b_Ans.GetAnserCount(DataConvert.CLng(uid)).ToString();
    }
    protected string getask(string uid)
    {
        return b_Ask.GetAskCount(DataConvert.CLng(uid)).ToString();
    }
    protected void bindRepeater9()
    {

        DataTable dt = b_Ask.GetTopUser();
        Repeater9.DataSource = dt;
        Repeater9.DataBind();
    }
    protected void Repeater9_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlImage img = e.Item.FindControl("ph") as HtmlImage ;
        img.Src = "/Guest//Guest/App_Themes/Guest/images/num" + (e.Item.ItemIndex + 1).ToString() + ".gif";
    }
    protected void bindRepeater10()
    {
        DataTable dt = b_Ask.GetTopUser();
        Repeater10.DataSource = dt;
        Repeater10.DataBind();
    }
    protected void Repeater10_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlImage img = e.Item.FindControl("ph") as HtmlImage;
        img.Src = "/Guest//Guest/App_Themes/Guest/images/num" + (e.Item.ItemIndex + 1).ToString() + ".gif";
    }
    /// <summary>
    /// 热点关键词
    /// </summary>
    protected void bindRepeater11()
    {
        DataTable dt = b_Ask.SelfieldOrd("QueType", 10);//根据被采纳问题数取知道之星
        Repeater11.DataSource = dt;
        Repeater11.DataBind();
    }
    /// <summary>
    /// 获取回答数
    /// </summary>
    /// <returns></returns>
    protected string getanswers(string qid)
    {
        return b_Ans.GetAnserCountByQid(DataConvert.CLng(qid)).ToString();
    }


    public string GetLeftString(string str,int length)
    {
        return str.Length > length ? str.Substring(0, length) + "..." : str;
    }
}