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
using ZoomLa.Components;

namespace ZoomLaCMS.Guest.Ask
{
    public partial class SearchList : System.Web.UI.Page
    {
        protected B_Ask b_Ask = new B_Ask();//问题BLL
        protected B_User b_User = new B_User();//基本用户BLl
        protected B_GuestAnswer b_Ans = new B_GuestAnswer();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strWhere = Request.QueryString["strWhere"] as string;
                string name = b_User.GetLogin().UserName;
                if (!IsPostBack)
                {
                    Bindpage();
                }
            }
            catch (Exception)
            { }
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
            string strWhere = Server.HtmlEncode(Request["strWhere"]);
            if (!string.IsNullOrEmpty(strWhere))
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + strWhere + "%") };
                dt = b_Ask.Sel(" (Qcontent like @key or Supplyment like @key or QueType like @key) and Status=2", "AddTime desc", sp);
            }
            else
            {

                dt = b_Ask.Sel("Status=2", "AddTime desc", null);
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
            Toppage.Text = "<a href=?strWhere=" + strWhere + "&Page=0>首页</a>";
            Nextpage.Text = "<a href=?strWhere=" + strWhere + "&Page=" + nextpagenum.ToString() + ">上页</a>";
            Downpage.Text = "<a href=?strWhere=" + strWhere + "&Page=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?strWhere=" + strWhere + "&Page=" + Endpagenum.ToString() + ">尾页</a>";
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
                return " ";
        }

        protected void repSearch_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            Label Isname = (Label)e.Item.FindControl("Isname");
            if (rowv["isNi"].ToString() == "1")
            { Isname.Text = "匿名"; }
        }
    }
}