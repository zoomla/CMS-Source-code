using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Components;

namespace ZoomLaCMS.Guest.Ask
{
    public partial class Success : System.Web.UI.Page
    {
        B_User buser = new B_User();
        public void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!buser.CheckLogin())
                {
                    if (!GuestConfig.GuestOption.WDOption.IsLogin)
                    {
                        main.Visible = false;
                        NoUser_div.Visible = true;
                    }
                }
            }
        }
        public string GetCate(string CateID)
        {
            string re = "";
            return re;
        }
        protected string getstyle()
        {
            if (buser.CheckLogin())
            {
                return "display:inherit";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inherit";
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

        //<summary>
        //取得最佳回答采纳率
        //</summary>
        //<returns></returns>
        public string getAdoption()
        {
            double adopCount = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer where Status=1", null).Rows[0][0]);
            double count = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer", null).Rows[0][0]);
            return ((adopCount / count) * 100).ToString("0.00") + "%";
        }
    }
}