namespace ZoomLaCMS.Plat.Daily
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    public partial class AddDaily : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_MisInfo bll = new B_MisInfo();
        M_MisInfo model = new M_MisInfo();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged("/Plat/Daily/");
            if (!IsPostBack)
            {
                Bind1();
                Bind2();
                Bind3();
            }
        }

        protected void Bind1()
        {

            dt = bll.Sel(buser.GetLogin().UserName);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void Bind2()
        {
            dt = bll.Sel(buser.GetLogin().UserName, 1, Request["Date"]);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        protected void Bind3()
        {
            SqlParameter[] sp = new SqlParameter[]
            {
            new SqlParameter("Date",Request["Date"])
            };
            dt = bll.Sel(buser.GetLogin().UserName, 10, Request["Date"]);
            Repeater3.DataSource = dt;
            Repeater3.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            model.Content = Request["log_1"];
            DateTime dtTime = DateTime.Now;
            if (!string.IsNullOrEmpty(Request["TextDate"]))
            {
                dtTime = Convert.ToDateTime(Request["TextDate"]);
            }
            else {
                dtTime = Convert.ToDateTime(Request["Date"]);
            }
            model.CreateTime = dtTime;
            model.Type = 2;
            model.Inputer = buser.GetLogin().UserName;
            model.MID = Convert.ToInt32(Request["Mid"]);
            model.ProID = Convert.ToInt32(Request["ProID"]);
            model.Title = Request["Date"];

            bll.insert(model);
            //  function.WriteErrMsg("添加成功！", "Default.aspx");
            function.WriteSuccessMsg("添加成功！！！", "AddDaily.aspx?Date=" + Request["Date"]);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            model.Content = Request["jie_1"];
            DateTime dtTime = DateTime.Now;
            if (!string.IsNullOrEmpty(Request["Date"]))
            {
                dtTime = Convert.ToDateTime(Request["Date"]);
            }
            model.CreateTime = dtTime;
            model.Type = 1;
            model.Inputer = buser.GetLogin().UserName;
            model.MID = Convert.ToInt32(Request["Mid"]);
            model.ProID = Convert.ToInt32(Request["ProID"]);
            model.Title = Request["Date"];
            bll.insert(model);
            // function.WriteErrMsg("添加成功！", "Default.aspx");
            function.WriteSuccessMsg("添加成功！！！", "AddDaily.aspx?Date=" + Request["Date"]);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DateTime dtTime = DateTime.Now;
            if (!string.IsNullOrEmpty(Request["Date"]))
            {
                dtTime = Convert.ToDateTime(Request["Date"]);
            }
            model.CreateTime = dtTime;
            model.Type = 10;
            model.Inputer = buser.GetLogin().UserName;
            model.MID = Convert.ToInt32(Request["Mid"]);
            model.ProID = Convert.ToInt32(Request["ProID"]);
            model.Title = Request.QueryString["Date"];
            string filpath = "";
            if (add_fields() != "")
                filpath = add_fields();
            string[] files = filpath.Split(new Char[] { '|' });
            int fileCount = 0;
            if (files.Length > 0)
            {
                for (fileCount = 0; fileCount < files.Length; fileCount++)
                {
                    //定义访问客户端上传文件的对象 
                    string fileNamePath;
                    //取得上传得文件名
                    fileNamePath = (Server.MapPath("/UploadFiles/") + files[fileCount]).Replace("/", "\\");
                }
            }
            model.Content = filpath;
            bll.insert(model);
            //   function.WriteErrMsg("添加成功！", "Default.aspx");
            function.WriteSuccessMsg("添加成功！！！", "AddDaily.aspx?Date=" + Request.QueryString["Date"]);
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string days = Request.QueryString["Date"];
            DataTable dt = bll.Sel(id);
            if (e.CommandName == "SetSta")
            {
                if (Convert.ToInt32(dt.Rows[0]["Status"]) == 0)
                {
                    bll.UpdateStatus(1, id);

                    function.WriteSuccessMsg("设置成功！！！", "AddDaily.aspx?Date=" + days);
                }
                else {
                    bll.UpdateStatus(0, id);

                    function.WriteSuccessMsg("设置成功！！！", "AddDaily.aspx?Date=" + days);
                }
            }
            if (e.CommandName == "SetDel")
            {
                bll.Del(id);
                function.WriteSuccessMsg("删除成功！！！", "AddDaily.aspx?Date=" + days);
            }
        }

        //插入 
        protected string add_fields()
        {
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string fpath = "";
            int fileCount = 0;
            for (fileCount = 0; fileCount < files.Count; fileCount++)
            {
                //定义访问客户端上传文件的对象
                System.Web.HttpPostedFile postedFile = files[fileCount];
                string fileName, fileExtension;
                //取得上传得文件名
                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                if (fileName != String.Empty)
                {
                    //取得文件的扩展名
                    fileExtension = System.IO.Path.GetExtension(fileName);
                    SafeSC.SaveFile("/UploadFiles/" + buser.GetLogin().UserName + "/我的文档/", postedFile, fileName);
                    if (fileCount == 0)
                    {
                        fpath += buser.GetLogin().UserName + "\\我的文档\\" + fileName;
                    }
                    else
                    {
                        fpath += "|" + buser.GetLogin().UserName + "\\我的文档\\" + fileName;
                    }
                }
            }
            return fpath;
        }
    }
}