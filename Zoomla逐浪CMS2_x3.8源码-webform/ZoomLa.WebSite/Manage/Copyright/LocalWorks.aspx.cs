using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model.Content;
using ZoomLa.PdoApi.CopyRight;

public partial class Manage_Copyright_LocalWorks : System.Web.UI.Page
{
    
    B_Content_CR crBll = new B_Content_CR();
    private string Skey { get { return Request.QueryString["skey"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //copyBll.Redirect_URI = Request.Url.ToString();
        if (!IsPostBack)
        {
            C_CopyRight.CheckLogin();
            MyBind(Skey);
            //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Config.aspx\">版权中心</a></li><li class=\"active\">作品列表[<a href=\"AddWorks.aspx\">添加作品</a>]</li>");
        }
    }
    public void MyBind(string skey = "")
    {
        DataTable dt = null;
        if (string.IsNullOrEmpty(skey))
        {
            dt = crBll.Sel();
        }
        else
        {
            dt = crBll.Search(skey);
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("ondblclick", "window.location.href = 'WorksShow.aspx?id=" + (e.Row.DataItem as DataRowView)["ID"] + "';");
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        C_CopyRight copyBll = new C_CopyRight();
        switch (e.CommandName)
        {
            case "del":
                {
                    M_Content_CR crMod = new M_Content_CR();
                    string result = copyBll.Remove(crMod.WorksID);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                    if ("1".Equals(obj["value"]))
                    {
                        crBll.Del(crMod.ID);
                    }
                    else
                    {
                        function.WriteErrMsg("删除失败:" + result);
                    }
                }
                break;
        }
        MyBind();
    }

    protected void Search_B_Click(object sender, EventArgs e)
    {
        string skey = Skey_T.Text;
        if (!string.IsNullOrEmpty(skey)) { sel_box.Attributes.Add("style", "display:inline;"); }
        else { sel_box.Attributes.Add("style", "display:none;"); }
        MyBind(skey);
    }

}