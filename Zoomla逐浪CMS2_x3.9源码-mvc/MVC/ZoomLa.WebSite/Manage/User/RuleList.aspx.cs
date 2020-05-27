using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.User
{
    public partial class RuleList : CustomerPageAction
    {
        B_Permission pll = new B_Permission();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            string KeyWord = Request.Form["TxtKeyWord"];
            string KeyWords = Request.QueryString["KeyWord"];
            if (string.IsNullOrEmpty(KeyWord) && !string.IsNullOrEmpty(KeyWords)) { KeyWord = KeyWords; }

            DataTable perminfo = pll.Select_All();

            if (!string.IsNullOrEmpty(KeyWord))
            {
                perminfo = pll.SelByRole(KeyWord);
            }

            Page_list(perminfo);
        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            Pagetable.DataSource = Cll;
            Pagetable.DataBind();
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);

            if (id > 0)
            {
                string Item = Request.Form["Item"] == null ? "" : Request.Form["Item"];
                if (Item != "")
                {
                    if (Item.IndexOf(",") > -1)
                    {
                        string[] str = Item.Split(new string[] { "," }, StringSplitOptions.None);
                        for (int i = 0; i < str.Length; i++)
                        {
                            int spid = DataConverter.CLng(str[i]);
                            M_Permission pinfo = pll.GetSelect(spid);
                            string Ugroup = pinfo.UserGroup;
                            if (Ugroup != "")//不为空
                            {
                                //判断是否存在
                                string newgroup = Ugroup;
                                if (newgroup.IndexOf("," + id + ",") < 0)
                                {
                                    Ugroup = Ugroup + "," + id + ",";
                                }
                            }
                            else //为空
                            {
                                Ugroup = "," + id.ToString() + ",";
                            }
                            pinfo.UserGroup = Ugroup;
                            pll.GetUpdate(pinfo);
                        }
                    }
                    else
                    {
                        int spid = DataConverter.CLng(Item);
                        M_Permission pinfo = pll.GetSelect(spid);
                        string Ugroup = pinfo.UserGroup;
                        if (Ugroup != "")//不为空
                        {
                            string newgroup = Ugroup;
                            if (newgroup.IndexOf("," + id + ",") < 0)
                            {
                                Ugroup = Ugroup + "," + id + ",";
                            }
                        }
                        else //为空
                        {
                            Ugroup = "," + id.ToString() + ",";
                        }
                        pinfo.UserGroup = Ugroup;
                        pll.GetUpdate(pinfo);
                    }
                }
            }
            Response.Write("<script language=javascript>window.close();opener.location.reload();</script>");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.close();</script>");
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string KeyWord = Request.Form["TxtKeyWord"];
            DataTable perminfo = pll.SelByRole(KeyWord);
            Page_list(perminfo);
        }
    }
}