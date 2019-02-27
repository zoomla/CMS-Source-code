using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.BLL.PlanSql;
using ZoomLa.Model.PlanSql;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using ZoomLa.Components;


namespace ZoomLaCMS.Manage.Config
{
    public partial class DevStepList :CustomerPageAction
    {
        B_Plan bplan = new B_Plan();
        M_Plan mplan = new M_Plan();
        B_PlanSql bsql = new B_PlanSql();
        M_PlanSql msql = new M_PlanSql();
        public int DevID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            DevID = Convert.ToInt32(Request["DevID"]);
            string show = Request["showdiv"];
            string addhtml = string.Empty;
            DataTable dt = bsql.SelByPlanID(DevID);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    string st = dt.Rows[i]["txtSql"].ToString();
                    string[] sq = st.Split('|');
                    int w = Convert.ToInt32(dt.Rows[i]["ID"]);
                    addhtml = addhtml + "<div class='divstyle' style='float:left' id=plane" + w + "><div style='height:10px;width:131px;position:absolute;background-color:#9AC7F0'><img src='../../Images/12493942650.gif' style='cursor:pointer' onclick='closediv(" + w + ")' align='right' /> </div><br/><a href='#' onclick='showdiv(" + sq[0] + "," + w + ")'>编辑</a><a href='#' onclick='orderbydiv(" + w + ")'>  运行顺序</a><br/><div id='Lable" + w + "'>" + sq[1] + "</div><div id='orderid" + w + "'>步骤" + dt.Rows[i]["statu"].ToString() + "</div></div><div class='moves'>——></div>";
                }
                int s = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ID"]);
                string sd = dt.Rows[dt.Rows.Count - 1]["txtSql"].ToString();
                string[] sa = sd.Split('|');
                addhtml = addhtml + "<div class='divstyle' style='float:left' id=plane" + s + "><div style='height:10px;width:131px;position:absolute;background-color:#9AC7F0'><img src='../../Images/12493942650.gif' style='cursor:pointer' onclick='closediv(" + s + ")' align='right' /> </div><br/><a href='#' onclick='showdiv(" + sa[0] + "," + s + ")'>编辑</a><a href='#' onclick='orderbydiv(" + s + ")'>  运行顺序</a><br/><div id='Lable" + s + "'>" + sa[1] + "</div><div id='orderid" + s + "'>步骤" + dt.Rows[dt.Rows.Count - 1]["statu"].ToString() + "</div></div>";
            }
            this.ShowDiv.Text = addhtml;
            if (show != "")
            {
                Literal1.Text = show;
            }
        }
        public B_Plan b_Plan = new B_Plan();
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(DevID);
            string Dset = string.Empty;
            DataTable ds = b_Plan.Sel(id);
            string showdiv = string.Empty;
            if (ds.Rows.Count > 0)
            {
                Dset = ds.Rows[0]["DataSet"].ToString();
            }
            DataTable dt = bsql.SelByPlanID(DevID);
            string con = string.Empty;
            if (Dset == "0")
            {
                con = SqlHelper.ConnectionString;
            }
            else
            {
                con = SqlHelper.PlugConnectionString;
            }
            string path = MapPath("/" + SiteConfig.SiteOption.UploadDir + "/T-sql/");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string fileurl = string.Empty;
                    string filesd = dt.Rows[i]["txtSql"].ToString();
                    string[] sp = filesd.Split('|');
                    fileurl = path + sp[1] + ".sql";
                    if (!ExecutionSql(fileurl, con))
                    {
                        showdiv = showdiv + sp[1] + ":执行失败！";
                        Response.Redirect("DevStepList.aspx?DevID=" + id + "&showdiv=" + showdiv);
                        return;
                    }
                    else
                    {
                        showdiv = showdiv + sp[1] + ":执行成功！";
                        //Response.Write("<script>alert('操作成功')</script>");
                    }
                }
            }
            Response.Redirect("DevStepList.aspx?DevID=" + id + "&showdiv=" + showdiv);
        }
        public static bool ExecutionSql(string path, string connectString)
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        StringBuilder builder = new StringBuilder();
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine();
                            if (!string.IsNullOrEmpty(str) && str.ToUpper().Trim().Equals("GO"))
                            {
                                break;
                            }
                            builder.AppendLine(str);
                        }
                        command.CommandType = CommandType.Text;
                        command.CommandText = builder.ToString();
                        command.CommandTimeout = 300;
                        command.ExecuteNonQuery();

                    }
                }
                catch (SqlException)
                {
                    //System.Web.HttpContext.Current.Response.Write(err.ToString());
                    //System.Web.HttpContext.Current.Response.End();
                    return false;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
            return true;
        }
    }
}