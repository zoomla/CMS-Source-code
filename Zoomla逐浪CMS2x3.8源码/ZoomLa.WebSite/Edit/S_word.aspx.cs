using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Runtime.InteropServices;
using System.Data;

public partial class Edit_S_word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Clear();
        Response.ContentType = "Application/msword";

        HttpContext.Current.Response.Charset = "utf-8";

        string DocType;
        DocType = Request.QueryString["DocType"];
        string ID = Request.QueryString["ID"];
        if (ID == null || ID == "")
        {
            Response.End();
            return;
        }

        //DataTable dt = b_EditWord.Sel(Convert.ToInt32(ID));
        //Response.BinaryWrite((Byte[])dt.Rows[0]["Status"]);   //读取二进制的文件
        //Response.Flush();
        //Response.End();
    }

}