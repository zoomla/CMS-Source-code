using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

//提供两种排序,一种是上移与下移,一种是一次性提交排序
public partial class Plat_Admin_GroupSort : System.Web.UI.Page
{
    B_Plat_Group gpBll = new B_Plat_Group();
    private int Pid { get { return DataConvert.CLng(Request.QueryString["Pid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Pid < 0) { function.WriteErrMsg("参数不正确"); }
            M_User_Plat upMod = B_User_Plat.GetLogin();
            RPT.DataSource = gpBll.SelByCompID(upMod.CompID, Pid);
            RPT.DataBind();
        }
    }
    protected void BatOrder_Btn_Click(object sender, EventArgs e)
    {
        string[] idArr = (Request.Form["idchk"] ?? "").Split(',');
        for (int i = 0; i < idArr.Length; i++)
        {
            int id = Convert.ToInt32(idArr[i]);
            gpBll.UpdateOrder(id, Convert.ToInt32(Request.Form["idtxt_" + id]));
        }
        function.WriteSuccessMsg("排序更新完成");
    }
}