using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.IO;

namespace ZoomLaCMS.Edit
{
    public partial class EditList : System.Web.UI.Page
    {
        public B_User b_User = new B_User();
        //public B_EditWord b_EditWord = new B_EditWord();
        protected void Page_Load(object sender, EventArgs e)
        {
            b_User.CheckIsLogin();
            RepNodeBind();
        }
        private void RepNodeBind()
        {
            int userID = b_User.GetLogin().UserID;
            //this.EGV.DataSource = b_EditWord.SelectByUserID(userID);
            //this.EGV.DataKeyNames = new string[] { "ID" };
            //this.EGV.DataBind();
            //if (this.EGV.Rows.Count > 0)
            //{
            //    this.tt.Style.Add("display", "none");
            //}
        }
        public string GWordN(string content)
        {
            string value = "";
            try
            {
                value = Path.GetFileNameWithoutExtension(content);
            }
            catch (Exception)
            {
                return content;
            }
            return value;
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        /// <summary>
        /// 鼠标滑动变色效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#def2dd'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#f6fdf6'");
            }
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Del")
            {
                //b_EditWord.Del(Convert.ToInt32(e.CommandArgument));
                //RepNodeBind();
            }
            else if (e.CommandName == "Tgao")
            {
                Response.Redirect("Submission.aspx?id=" + e.CommandArgument);
            }
        }
    }
}