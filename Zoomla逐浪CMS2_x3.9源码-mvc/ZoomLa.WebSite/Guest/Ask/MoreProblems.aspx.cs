using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLaCMS.Guest.Ask
{
    public partial class MoreProblems : System.Web.UI.Page
    {
        protected B_Ask b_Ask = new B_Ask();//问题BLL
        protected B_User b_User = new B_User();//基本用户BLl
        protected B_GuestAnswer banswer = new B_GuestAnswer();
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = b_User.GetLogin().UserName;
            if (!IsPostBack)
            {

            }
            if (b_User.CheckLogin())
            {

            }
            else
            {
                Response.Redirect("/User/Login.aspx?ReturnUrl=/Guest/Ask/Add.aspx");

            }
            if (!IsPostBack)
            {
                Bindpage();
            }
        }
        protected void Bindpage()
        {
            int CPage;
            int temppage;
            if (Request.Form["DropDownList1"] != null)
            {
                temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
            }
            else
            {
                temppage = Convert.ToInt32(Request.QueryString["Page"]);
            }

            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }
            DataTable dt;
            string type = Request["type"];
            switch (type)
            {
                case "0": dt = Sql.Sel("zl_ask", "Elite=1", "ID desc"); break;
                case "1": dt = Sql.Sel("zl_ask", "", "ID desc"); break;
                case "2": dt = Sql.Sel("zl_ask", " Score<>0", ""); break;
                default: dt = Sql.Sel("zl_ask", "", "ID desc"); break;

            }

            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = dt.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 10;
            cc.CurrentPageIndex = CPage - 1;

            repSearch.DataSource = cc;
            repSearch.DataBind();
            AllNum.Text = dt.DefaultView.Count.ToString();
            int thispagenull = cc.PageCount;
            int CurrentPage = cc.CurrentPageIndex;
            int nextpagenum = CPage - 1;
            int downpagenum = CPage + 1;
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
            Toppage.Text = "<a href=?type=" + Request["type"] + "&Page=0>首页</a>";
            Nextpage.Text = "<a href=?type=" + Request["type"] + "&Page=" + nextpagenum.ToString() + ">上页</a>";
            Downpage.Text = "<a href=?type=" + Request["type"] + "&Page=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?type=" + Request["type"] + "&Page=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            this.Lable1.Text = cc.PageSize.ToString();
            if (!this.Page.IsPostBack)
            {
                for (int i = 1; i <= thispagenull; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }
            }


        }
        protected string getanswer(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            string answer;
            DataTable dt = Sql.Sel("ZL_GuestAnswer", " QueId=@id", "", sp);
            if (dt.Rows.Count > 0)
            {
                answer = (dt.Rows[0]["Content"]).ToString();
                return answer;
            }
            else
                return " ";
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindpage();
        }
        protected string gettype(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            DataTable dt = Sql.Sel("zl_grade", " GradeID=@id", "", sp);
            if (dt.Rows.Count > 0)
            {
                string name;
                name = (dt.Rows[0]["GradeName"]).ToString();
                return name;

            }
            else
                return "";

        }
        protected int getcount(string id)
        {
            if (id != "")
            {
                int number;
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
                DataTable dt = banswer.Sel(" QueId=@id", "", sp);
                if (dt.Rows.Count > 0)
                {
                    number = dt.Rows.Count;
                    return number;
                }
                else return 0;
            }
            else return 0;
        }
        public string GetCate(string CateID)
        {
            string re = "";
            return re;
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
            return SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_Ask where Status=1 ", null).Rows[0][0].ToString();
        }
        /// <summary>
        /// 取待解决问题总数
        /// </summary>
        /// <returns></returns>
        public string getSolvingCount()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_Ask where Status=0 ", null).Rows[0][0].ToString();
        }
        /// <summary>
        /// 取得用户总数
        /// </summary>
        /// <returns></returns>
        public string getUserCount()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_User ", null).Rows[0][0].ToString();
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
            double adopCount = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer where Status=1", null).Rows[0][0]);
            double count = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer", null).Rows[0][0]);
            return ((adopCount / count) * 100).ToString("0.00") + "%";
        }
    }
}