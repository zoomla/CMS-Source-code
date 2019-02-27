using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;

public partial class WebUserControlCollect : System.Web.UI.UserControl
{
    #region 业务对象
    CollectTableBLL ctbll = new CollectTableBLL();
    #endregion

    public int stype
    {
        get
        {
            if (ViewState["stype"] != null)
                return int.Parse(ViewState["stype"].ToString());
            else
                return 100;
        }
        set
        {
            ViewState["stype"] = value;
        }
    }

    public Guid ByID
    {
        get
        {
            if (ViewState["ByID"] != null)
                return new Guid(ViewState["ByID"].ToString());
            else return Guid.Empty;
        }
        set
        {
            ViewState["ByID"] = value;
        }
    }

    private Guid cID
    {
        get
        {
            if (ViewState["cID"] != null)
                return new Guid(ViewState["cID"].ToString());
            else return Guid.Empty;
        }
        set
        {
            ViewState["cID"] = value;
        }
    }

    public int userID
    {
        get
        {
            if (ViewState["userID"] != null)
                return int.Parse(ViewState["userID"].ToString());
            else return 0;
        }
        set
        {
            ViewState["userID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCollect();
        }
    }

    protected void Hfid_ValueChanged(object sender, EventArgs e)
    {
        GetCollect();
    }

    //读取收藏状态
    private void GetCollect()
    {
        this.LinkButton1.Visible = false;
        string[] bstate ={ "想读", "在读", "读过", "想看", "在看", "看过", "想听", "在听", "听过","想去","在途中","去过" };
        string[] btype ={ "这本书", "这部电影", "这首歌曲","这个旅途" };
        string message = "";
        int j=0;

        CollectTable ct = ctbll.GetCollectByID(userID, ByID);
        if (ct.ID == Guid.Empty)
        {
            j = stype * 3;
            for (int i = 0; i < 3; i++)
            {

                message += "<a href=\"javascript:showPopWin('收藏" + btype[stype] + "','AjaxCollect.aspx?stype=" + stype + "&state=" + j + "&Byid=" + ByID + "&Math.random()',400,300, refpage,true)\"><" + bstate[j] + "></a>&nbsp; &nbsp;";
                j++;
            }
        }
        else
        {
            ViewState["cID"] = ct.ID.ToString();
            j = int.Parse(ct.Cstate.ToString());
            this.LinkButton1.Visible = true;
            message = "我" + bstate[j] + btype[stype] + "&nbsp; &nbsp;<a href=\"javascript:showPopWin('修改收藏信息','AjaxCollectEdit.aspx?Byid=" + cID + "&Math.random()',400,300, refpage,true)\"><修改></a>&nbsp;&nbsp;";
        }
        this.Label1.Text = message;

    }


    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        ctbll.DelCollect(cID);
        GetCollect();
    }
}
