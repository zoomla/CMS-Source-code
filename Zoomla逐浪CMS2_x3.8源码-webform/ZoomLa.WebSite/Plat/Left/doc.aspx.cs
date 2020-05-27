using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;

public partial class Plat_Left_doc : System.Web.UI.Page
{
    B_Plat_File fileBll = new B_Plat_File();
    public string MyVPath
    {
        get
        {
            if (Session["CompDoc_VPath"] == null)
            {
                Session["CompDoc_VPath"] = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Company);
            }
            return Session["CompDoc_VPath"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        MyBind();
    }
    private void MyBind()
    {
        DataTable dt = new DataTable();
        dt = fileBll.SelByVPath(MyVPath);
        RPT.DataSource = dt;
        RPT.DataBind();
        if (dt.Rows.Count < 1) { Response.Clear(); Response.Write("<div class='r_gray'>公司文库中无文件</div>"); }
    }
    public string GetExt()
    {
        switch (Eval("FileType").ToString())
        {
            case "2":
                return "filefolder";
            default:
                return Eval("FileName").ToString();
        }
    }
}