using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.Config
{
    public partial class DataSearch : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                    SPwd.Visible = true;
                else
                    maindiv.Visible = true;
                if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                    function.WriteErrMsg("错误的Url或非法的请求！", "");
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='DataSearch.aspx'>全库搜索</a></li>" + Call.GetHelp(67));
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        SqlConnection conn = new System.Data.SqlClient.SqlConnection(SqlHelper.ConnectionString);
        protected void Button1_Click(object sender, EventArgs e)
        {
            // if (TextBox1.Text == "")
            //   function.WriteErrMsg("请输入要检索的内容", "/manage/Config/test.html");
            //DataTable dt= Sql.SelectAll(TextBox1.Text);

            //Sql.
            //string sql = "declare @str varchar(100) set @str='" + TextBox1.Text + "' declare @s varchar(8000) declare tb cursor local for select s='if exists(select 1 from ['+b.name+'] where ['+a.name+'] like ''%'+@str+'%'') print '' ['+b.name+'].['+a.name+']''' from syscolumns a join sysobjects b on a.id=b.id where b.xtype='U' and a.status>=0 and a.xusertype in(175,239,231,167) open tb fetch next from tb into @s while @@fetch_status=0 begin exec(@s) fetch next from tb into @s end close tb deallocate tb";
            //conn.InfoMessage += new SqlInfoMessageEventHandler(info);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand(sql, conn);
            //cmd.CommandType = CommandType.Text;
            //cmd.ExecuteNonQuery();
            //conn.Close(); 

            // if (dt!=null&&dt.Rows.Count > 0)
            //  {
            //      listBox1.DataSource = dt;
            //      listBox1.DataBind();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    listBox1.Items.Add(dt.Rows[i].ToString());
            //}
            //  }
            // else
            //      function.WriteErrMsg("没有检索到相关内容", "/manage/Config/test.html");

            //listBox1.Items=dt.Rows[i]
            //list.Visible = true;
            ////执行完后就有 MessageBox.Show(r.Message);的结果
            //listBox1.Items.Clear();
            //if (lis.Count > 0)
            //{

            //    for (int i = 0; i < lis.Count; i++)
            //    {
            //        listBox1.Items.Add(lis[i].ToString());
            //    }
            //}
            //else
            //    function.WriteErrMsg("没有检索到相关内容", "/manage/Config/DataSearch.aspx");
            //TextBox1.Text = "";
        }

        //List<string> lis = new List<string>();
        //private void info(object o, SqlInfoMessageEventArgs ar)
        //{
        //    foreach (SqlError r in ar.Errors)
        //    {
        //        lis.Add(r.Message.ToString());
        //    }
        //}
    }
}