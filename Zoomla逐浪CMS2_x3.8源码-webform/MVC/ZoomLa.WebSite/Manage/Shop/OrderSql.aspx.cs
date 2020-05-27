using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class OrderSql : CustomerPageAction
    {
        B_User buser = new B_User();
        public int OrderType { get { string type = Request.QueryString["Type"]; if (type == null) { return -100; } else { return DataConvert.CLng(type); } } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'>账单管理</li>");
            }
        }
        protected void MyBind()
        {

        }
        protected string getStatus(string sta)
        {
            string str = "";
            if (sta == "1")
            {
                str = "<span style='color:#F00'>已审核</span>";
            }

            else
            {
                str = "<span style='color:#00F'>未审核</span>";
            }
            return str;
        }
        protected string getRunStatus(string sta)
        {
            string str = "";
            if (sta == "1")
            {
                str = "<span style='color:#F00'>已执行</span>";
            }

            else
            {
                str = "<span style='color:#00F'>未执行</span>";
            }
            return str;
        }
        protected string getPayStatus(string sta)
        {
            string str = "";
            if (sta == "1")
            {
                str = "<span style='color:#F00'>已付款</span>";
            }

            else
            {
                str = "<span style='color:#00F'>未付款</span>";
            }
            return str;
        }
        protected string getStabtn(string sta)
        {
            string str = "";
            if (sta == "1")
            {
                str = "<span style='color:#F00'>取消审核</span>";
            }
            else {

                str = "<span style='color:green'>审核</span>";
            }
            return str;
        }
        protected string getPaybtn(string sta)
        {
            string str = "";
            if (sta == "1")
            {
                str = "<span style='color:#F00'>取消付款</span>";
            }
            else
            {

                str = "<span style='color:green'>付款</span>";
            }
            return str;
        }
        protected string getUserName(int userid)
        {
            return buser.GetUserByUserID(userid).UserName;
        }
        protected void creatfile(string filenames, string writ, string path)
        {
            //string path = MapPath("/UploadFiles/T-sql/");
            //if (!File.Exists(path + filenames + ".sql"))
            //{
            StreamWriter sw = new StreamWriter(File.Create(MapPath(path) + filenames + ".sql"));
            sw.Write(writ);
            sw.Close();

            //}
        }
        protected string getOrderType(string type)
        {
            string str = "";

            if (type == "1")
            {
                str = "新开返利";
            }
            else if (type == "2")
            {
                str = "消费上报";
            }
            return str;
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "window.location.href = 'EditOrderSql.aspx?Type=" + (e.Row.DataItem as DataRowView)["ID"] + "';");
            }
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConvert.CLng(e.CommandArgument);
            //LinkButton StatBtn = (LinkButton)e.Item.FindControl("StatBtn");
            //LinkButton PayBtn = (LinkButton).FindControl("PayBtn");
            DataTable lclData = new DataTable();
            switch (e.CommandName.ToLower())
            {
                case "save":
                    {
                        string sql = lclData.Rows[0]["Sqlstr"].ToString();
                        if (!string.IsNullOrEmpty(sql))
                        {
                            string filename = "";
                            DateTime dt = Convert.ToDateTime(lclData.Rows[0]["AddTime"]);
                            filename = dt.ToString("yyyyMMddHHmmss");
                            string path1 = "T-sql/" + dt.Year.ToString() + "/" + dt.Month.ToString() + "/" + dt.Day.ToString() + "/";
                            string path = "/" + SiteConfig.SiteOption.UploadDir + "/" + path1;
                            try
                            {
                                creatfile(filename, sql, path);
                            }
                            catch
                            {
                                Directory.CreateDirectory(MapPath(path));
                                creatfile(filename, sql, path);
                            }
                            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('生成成功！');</script>");
                            function.WriteSuccessMsg("生成成功");
                        }
                        else
                        {
                            function.WriteErrMsg("无任何SQL语句");
                        }
                    }
                    break;
                case "download":
                    {
                        string sql = lclData.Rows[0]["Sqlstr"].ToString();
                        SafeSC.DownStr(sql, "Script.sql");
                    }
                    break;
                case "del":
                    function.WriteSuccessMsg("删除成功");
                    break;
            }
            MyBind();
        }
        protected void BatAudit_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                foreach (string id in ids.Split(','))
                {
                    function.WriteSuccessMsg("审核成功");
                }
            }
        }
        protected void BatUnAudit_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                function.WriteErrMsg(ids);
                foreach (string id in ids.Split(','))
                {
                    function.WriteSuccessMsg("取消审核成功");
                }
            }
        }
        protected void BatPay_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                foreach (string id in ids.Split(','))
                {
                    function.WriteSuccessMsg("修改成功");
                }
            }
            MyBind();
        }
    }
}