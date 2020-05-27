using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Guest.Ask
{
    public partial class Classification : System.Web.UI.Page
    {
        protected B_User b_User = new B_User();//基本用户BLl
        protected M_UserInfo m_UserInfo = new M_UserInfo();
        protected B_Ask b_Ask = new B_Ask();//问题BLL
        protected M_Ask m_Ask = new M_Ask();
        protected string cateid = "";//大类ID
        protected string catename = "";//大类名称
        protected string gradeid = "";//小类ID
        public DataTable dt3 = new DataTable();//问题列表
        protected B_GuestAnswer b_Ans = new B_GuestAnswer();
        protected void Page_Load(object sender, EventArgs e)
        {
            RPT.SPage = MyBind;
            M_UserInfo info = b_User.GetLogin();
            if (!IsPostBack)
            {
                RPT.DataBind();
            }
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
            return b_Ans.IsExistInt(2).ToString();
        }
        /// <summary>
        /// 取待解决问题总数
        /// </summary>
        /// <returns></returns>
        public string getSolvingCount()
        {
            return b_Ans.IsExistInt(1).ToString();
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
        /// <summary>
        /// 绑定问题分类
        /// </summary>
        private DataTable MyBind(int pageSize, int pageIndex)
        {
            int itemCount = 0;
            string type = "0";
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                type = Server.HtmlEncode(Request.QueryString["type"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ParentID"]))  //点击的是小类
            {
                cateid = Server.HtmlEncode(Request.QueryString["ParentID"]);
                M_Grade grademod = B_GradeOption.GetGradeOption(DataConvert.CLng(Request.QueryString["ParentID"]));
                catename = grademod.GradeName;
                DataTable dt2 = B_GradeOption.GetGradeList(2, DataConvert.CLng(Request.QueryString["ParentID"]));
                Repeater1.DataSource = dt2;
                Repeater1.DataBind();
                if (!string.IsNullOrEmpty(Request.QueryString["GradeID"]))
                {
                    gradeid = Server.HtmlEncode(Request.QueryString["GradeID"]);
                }

                SqlParameter[] sp = new SqlParameter[]
                {
                new SqlParameter("@gradeid",gradeid)
                };
                //dt3 = b_Ask.SelPage(type, pageSize, pageIndex, out itemCount, QueType: gradeid);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["GradeID"]))   //点击的是大类
            {
                cateid = Server.HtmlEncode(Request.QueryString["GradeID"]);
                M_Grade grademod = B_GradeOption.GetGradeOption(DataConvert.CLng(Request.QueryString["GradeID"]));
                catename = grademod.GradeName;
                DataTable dt2 = B_GradeOption.GetGradeList(2, DataConvert.CLng(Request.QueryString["GradeID"]));
                Repeater1.DataSource = dt2;
                Repeater1.DataBind();
                DataTable dtChild = B_GradeOption.GetGradeList(2, DataConvert.CLng(cateid));
                List<int> cateids = new List<int>();
                cateids.Add(DataConvert.CLng(cateid));
                foreach (DataRow dr in dtChild.Rows)
                {
                    cateids.Add(DataConvert.CLng(dr["GradeID"]));
                }
                //dt3 = b_Ask.Sel(strWhere + " and " + str, " AddTime desc", sp);//str与strwhere未污染
                //dt3 = b_Ask.SelPage(type, pageSize, pageIndex, out itemCount,Cateids:cateids);
            }
            else
            {
                catename = "全部分类";
                DataTable dt = B_GradeOption.GetGradeList(2, 0);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                //dt3 = b_Ask.Sel(str, " AddTime desc", null);
                //dt3 = b_Ask.SelPage(type, pageSize, pageIndex, out itemCount);
            }
            RPT.ItemCount = itemCount;
            return dt3;
        }
        /// <summary>
        /// 绑定回答数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowv = e.Item.DataItem as DataRowView;
            HtmlGenericControl aCount = (HtmlGenericControl)e.Item.FindControl("aCount");
            if (aCount != null)
            {
                aCount.InnerHtml = b_Ans.GetAnserCountByQid(DataConvert.CLng(rowv["ID"])) + "回答";
            }

        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            HtmlGenericControl spGrade = (HtmlGenericControl)e.Item.FindControl("spGrade");
            if (catename == "全部分类")
            {
                DataTable dtChild = B_GradeOption.GetGradeList(2, DataConvert.CLng(rowv["GradeID"]));
                string ids = "";
                foreach (DataRow dr in dtChild.Rows)
                {
                    ids += dr["GradeID"] + ",";
                }
                string count = b_Ask.GetCountByQueType(ids.Trim(',')).ToString();
                spGrade.InnerHtml = "<a href='Classification.aspx?GradeID=" + rowv["GradeID"] + "'>" + rowv["GradeName"] + "</a>" + "(" + count + ")";
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["GradeID"]))
                {
                    gradeid = Server.HtmlEncode(Request.QueryString["GradeID"]);
                }
                if (rowv["GradeID"].ToString() == gradeid)
                {
                    spGrade.InnerHtml = "<a style='color:black;' href='Classification.aspx?ParentID=" + cateid + "&GradeID=" + rowv["GradeID"] + "'>" + rowv["GradeName"] + "</a>";
                }
                else
                {
                    spGrade.InnerHtml = "<a href='Classification.aspx?ParentID=" + cateid + "&GradeID=" + rowv["GradeID"] + "'>" + rowv["GradeName"] + "</a>";
                }
                string count = b_Ask.GetCountByQueType(rowv["GradeID"].ToString()).ToString();
                spGrade.InnerHtml += "(" + count + ")";
            }
        }

        //切字
        public string GetLeftString(string str, int length)
        {
            if (str.Length <= length)
                return str;
            return str.Substring(0, length) + "..."; ;
        }
    }
}