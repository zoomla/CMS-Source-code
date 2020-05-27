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


using ZoomLa.BLL;public partial class WebUserControlComment : System.Web.UI.UserControl
{
    #region 业务对象
    B_User ubll = new B_User();
    commentbll cbll = new commentbll();
    UserTableBLL utbll = new UserTableBLL();
    #endregion

    public Guid GetcbID;
    public int GetcuID;
    public int Gettype;

    private Guid cbID
    {
        get
        {
            return new Guid(GetcbID.ToString());

        }
        set
        {
            GetcbID = value;
        }
    }

    private int cuID
    {
        get
        {
            return GetcuID;
        }
        set
        {
            GetcuID = value;
        }
    }

    private int type
    {
        get
        {
            return (int)Gettype;
        }
        set
        {
            Gettype = value;
        }
    }

    public string PageName
    {
        get
        {
            if (ViewState["pagename"]==null)
                return string.Empty;
            else
                return ViewState["pagename"].ToString();
        }
        set
        {
            ViewState["pagename"] = value;
        }
    }

    public int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] != null)
                return int.Parse(ViewState["CurrentPage"].ToString());
            else return 0;
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Egv.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    public void DataBind(string key="")
    {
        Egv.DataSource = cbll.GetCommentBycbyID(cbID);
        Egv.DataBind();
    }
    protected void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = Egv.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = Egv.PageSize;
        }
        Egv.PageSize = pageSize;
        Egv.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del1":
                cbll.Delcommant(new Guid(e.CommandArgument.ToString()));
                break;
        }
        DataBind();
    }
    #region 页面调用的方法
    protected void commentBtn_Click(object sender, EventArgs e)
    {
        CommentAll ca = new CommentAll();
        ca.CbyID = cbID;
        ca.UserID = cuID;
        ca.Ctitle = this.titletxt.Text;
        ca.Ccontent = this.TextArea1.Value;
        ca.CbyType = type;
        cbll.Insertcomment(ca);
        DataBind();

    }
    protected string getusername(string utid)
    {
        return ubll.GetUserByUserID(int.Parse(utid)).UserName;
    }
    protected string getuserpic(string utid)
    {
        return ubll.GetUserBaseByuserid(int.Parse(utid)).UserFace;
    }
    #endregion
    
}