using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.API;

public partial class Edit_EditContent : System.Web.UI.Page
{
    B_User b_User = new B_User();
    //B_EditWord b_EditWord;
    //M_EditWord m_EditWord;
    protected string content = string.Empty;
    protected new string ID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Request.ur 
       // b_User.CheckIsLogin(Request.Url.ToString());
        if (b_User.CheckLogin())
        {

        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "script", "<script>document.getElementById('showLogin').click();</script>");
             
        }
    //    if (Request["type"] == "SaveOnline")
    //    {
    //        if (Request["ID"] != "")
    //        {
    //            if (Request["addNew"] == "add" || Request["ID"]=="0")
    //            {
    //                SaveOnline();
    //            }
    //            else
    //            {
    //                SaveUpdate(Convert.ToInt32(Request["ID"]));
    //            }
    //        }
    //    }
    //    if (Request["ID"] != "")
    //    {
    //        ID = Request["ID"];
    //    }
    }

    protected void showdiag()
    {
        Literal lt = new Literal();
        lt.ID = "msg";
        lt.Text = "<script>alertWin(260,130);</script>";
        Page.Controls.Add(lt);
    }
    protected void verify()
    {
        M_UserInfo info = new M_UserInfo();
        info = b_User.AuthenticateUser(Request["UserName"], Request["PassWord"]);
        //throw new Exception("aaa");
        if (info.IsNull)
        {
            Response.Write("Fail");
            Response.Flush();
            Response.Close();
        }
        else
        {
            Response.Write("Success");
            b_User.SetLoginState(info, "Day");
            Response.Flush();
            Response.Close();
        }
    }

    //private void SaveUpdate(int id)
    //{
    //    string content = Request["content"];
    //    b_EditWord = new B_EditWord();
    //    m_EditWord = new M_EditWord();

    //    M_EditWord me=b_EditWord.Sel(id);
    //    m_EditWord.ID = id;
    //    m_EditWord.UserID = me.UserID;
    //    m_EditWord.Type = me.Type;
    //    m_EditWord.Content = content;
    //    m_EditWord.CreateTime = DateTime.Now;
    //    m_EditWord.Title = me.Title;
    //    m_EditWord.Status = me.Status;
    //    try
    //    {
    //        b_EditWord.Update(m_EditWord);
    //        Response.Write("保存成功");
    //        Response.Flush();
    //        Response.Close();
    //    }
    //    catch
    //    {
    //        Response.Write("保存失败");
    //        Response.Flush();
    //        Response.Close();

    //    }
    //}

    //private void SaveOnline()
    //{
    //    string content = System.Web.HttpUtility.UrlDecode(Request["content"]);
    //     b_EditWord = new B_EditWord();
    //     m_EditWord = new M_EditWord();

    //    m_EditWord.UserID =b_User.GetLogin().UserID;
    //    m_EditWord.Type = "asdf";
    //    m_EditWord.Content = content;
    //    m_EditWord.CreateTime = DateTime.Now;
    //    m_EditWord.Title = "dfe";
    //    m_EditWord.Status = 1;
    //    try
    //    {
    //        b_EditWord.Add(m_EditWord);
    //        Response.Write("保存成功");
    //        Response.Flush();
    //        Response.Close();
    //    }
    //    catch
    //    {
    //        Response.Write("保存失败");
    //        Response.Flush();
    //        Response.Close();
    //    }

    //}
}
