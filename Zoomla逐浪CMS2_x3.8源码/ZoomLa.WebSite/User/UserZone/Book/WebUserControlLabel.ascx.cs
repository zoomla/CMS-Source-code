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
using ZoomLa.Sns.BLL;

public partial class WebUserControlLabel : System.Web.UI.UserControl
{
    #region 业务对象
    CollectTableBLL ctbll = new CollectTableBLL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCollectLabelName();
        }
    }

    #region 页面调用方法
    public int Allstype
    {
        get
        {
            if (ViewState["Allstype"] != null)
                return int.Parse(ViewState["Allstype"].ToString());
            else return 100;
        }
        set
        {
            ViewState["Allstype"] = value;
        }
    }

    //绑定标签
    private void GetCollectLabelName()
    {
        string slit = ctbll.GetAllLabelName(Allstype);
        string[] slis = slit.Split(new Char[] { ' ' });
        ArrayList list = new ArrayList();
        foreach (String itemValue in slis)
        {
            list.Add(itemValue);
        }
        DataList1.DataSource = list;
        DataList1.DataBind();

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        Page.Response.Redirect(@"~\User\UserZone\Book\ShowLabelName.aspx?labelname=" + lb.Text + "&stype=" + Allstype);
    }

    #endregion
}