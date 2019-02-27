using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.OA
{
    public partial class PreViewProg : System.Web.UI.Page
    {
        protected B_UserBaseField ubBll = new B_UserBaseField();
        protected B_User buser = new B_User();
        protected M_MisProcedure proMod = new M_MisProcedure();
        protected B_MisProcedure proBll = new B_MisProcedure();
        protected B_MisProLevel stepBll = new B_MisProLevel();
        public string proID;
        protected void Page_Load(object sender, EventArgs e)
        {
            proID = Request.QueryString["proID"];
            if (string.IsNullOrEmpty(proID))
                function.WriteErrMsg("请先选定流程!!!");
            if (!IsPostBack)
            {
                DataBind();
                ClearSession();
            }
        }
        private void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            dt = stepBll.SelByProID(proID);
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "StepName like '%" + key + "%'";
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        public string GetUserInfo(object u)
        {
            string[] uidArr = u.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "";
            switch (OAConfig.UNameConfig)
            {
                case "1":
                    result = buser.GetUserNameByIDS(u.ToString());
                    break;
                case "2":
                    foreach (string uid in uidArr)
                    {
                        result = GetTrueName(uid, UserBaseDT) + "(" + GetHoneyName(uid, UserBaseDT) + "),";
                    }
                    break;
                case "3":
                    foreach (string uid in uidArr)
                    {
                        result = GetTrueName(uid, UserBaseDT) + ",";
                    }
                    break;
                case "4":
                    foreach (string uid in uidArr)
                    {
                        result = GetTrueName(uid, UserBaseDT) + "(" + uid + "),";
                    }
                    break;
            }
            return result.Length > 13 ? result.Substring(0, 12) + "..." : result;
        }
        //HoneyName,TrueName
        public DataTable UserBaseDT
        {
            get
            {
                if (Session["UserBaseDT"] == null)
                    Session["UserBaseDT"] = ubBll.SelAll();
                return Session["UserBaseDT"] as DataTable;
            }
        }
        //UserName,ID
        public DataTable UserNameDT
        {
            get
            {
                if (Session["UserNameDT"] == null)
                    Session["UserNameDT"] = buser.Sel();
                return Session["UserNameDT"] as DataTable;
            }

        }
        public string GetTrueName(string uid, DataTable dt)
        {
            dt.DefaultView.RowFilter = "UserID = " + uid;
            return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["TrueName"].ToString() : "";
        }
        public string GetHoneyName(string uid, DataTable dt)
        {
            dt.DefaultView.RowFilter = "UserID = " + uid;
            return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["HoneyName"].ToString() : "";
        }
        public string GetUserName(string uid, DataTable dt)
        {
            dt.DefaultView.RowFilter = "UserID = " + uid;
            return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["UserName"].ToString() : "";
        }
        public void ClearSession()
        {
            Session["UserBaseDT"] = null;
            Session["UserNameDT"] = null;
        }
        public string GetHQoption(string hQoption)
        {
            switch (hQoption)
            {
                case "0":
                    return "任意一人即可";
                case "1":
                    return "必须全部审核";
                default:
                    return hQoption;
            }
        }
        public string GetQzzjoption(string qzzjoption)
        {
            switch (qzzjoption)
            {
                case "0":
                    return "不允许";
                case "1":
                    return "允许";
                default:
                    return "";
            }
        }
        public string GetHToption(string hGetHToption)
        {
            switch (hGetHToption)
            {
                case "0":
                    return "不允许";
                case "1":
                    return "允许回退上一步骤";
                case "2":
                    return "允许回退之前步骤";
                default:
                    return "";
            }
        }
    }
}