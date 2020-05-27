using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.Components;

public partial class User_Order_ShareList : System.Web.UI.Page
{
    B_Order_Share shareBll = new B_Order_Share();
    B_User buser = new B_User();
    B_Group groupBll = new B_Group();
    B_Admin badmin = new B_Admin();
    private DataTable GroupDT { get { return ViewState["GroupDT"] as DataTable; } set { ViewState["GroupDT"] = value; } }
    public int replyPsize = 5;
    public int CPage { get { return PageCommon.GetCPage(); } }
    private int ProID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
    //admin
    public string Mode { get { return Request.QueryString["Mode"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Mode.ToLower().Equals("admin"))
        {
            B_Admin.CheckIsLogged();
            
        }
        #region AJAX请求
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "-1";
            switch (action)
            {
                case "reply"://回复,不需要购买也可,但必须登录
                    {
                        string msg = Request.Form["msg"];
                        int pid = DataConvert.CLng(Request.Form["ID"]);
                        int rid = DataConvert.CLng(Request.Form["rid"]);
                        int proid = DataConvert.CLng(Request.Form["proid"]);
                        if (pid < 1 || rid < 1 || string.IsNullOrEmpty(msg)) break;
                        M_UserInfo mu = buser.GetLogin();
                        M_Order_Share replyMod = shareBll.SelReturnModel(rid);
                        M_Order_Share shareMod = new M_Order_Share();
                        shareMod.UserID = mu.UserID;
                        shareMod.Pid = pid;
                        shareMod.MsgContent = msg;
                        shareMod.ReplyID = rid;
                        shareMod.ProID = proid;
                        shareMod.ReplyUid = replyMod.UserID;
                        shareBll.Insert(shareMod);
                        result = "1";
                    }
                    break;
                case "del":
                    {
                        int id = Convert.ToInt32(Request.Form["id"]);
                        shareBll.Del(id);
                        result = "1";
                    }
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        #endregion
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        int psize = 10, itemCount = 0;
        GroupDT = groupBll.Sel();
        DataTable dt = shareBll.SelByPid(psize, CPage, out itemCount, 0, ProID);
        RPT.DataSource = dt;
        RPT.DataBind();
        if (dt.Rows.Count < 1) { comments.Visible = false; nodata_div.Visible = true; }
        MsgPage_L.Text = PageCommon.CreatePageHtml(PageCommon.GetPageCount(psize, itemCount), CPage, psize);
    }
    protected void MsgRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
            //加载用户回复列表
            Literal replyList = e.Item.FindControl("ReplyList_L") as Literal;
            DataRowView dr = e.Item.DataItem as DataRowView;
            int pid = Convert.ToInt32(dr["ID"]);
            if (Mode.ToLower().Equals("admin"))
            {
                function.Script(this, "HideReply();");
                HtmlButton btnhtml = e.Item.FindControl("Del_Btn") as HtmlButton;
                btnhtml.Attributes.Add("data-id", pid.ToString());
                btnhtml.Visible = true;
                HtmlAnchor ahtml = e.Item.FindControl("Edit_A") as HtmlAnchor;
                ahtml.HRef = "/"+SiteConfig.SiteOption.ManageDir + "/Shop/EditShare.aspx?id=" + pid;
                ahtml.Visible = true;
            }
            StringWriter sw = new StringWriter();
            Server.Execute("/User/Order/ShareReply.aspx?pid=" + pid + "&psize=" + replyPsize + "&page=1&mode=admin", sw);
            replyList.Text = Regex.Match(sw.ToString(), "(?<=(start>))[.\\s\\S]*?(?=(</start))", RegexOptions.IgnoreCase).Value;
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        GroupDT = null;
    }
    //---------------------------------------
    //评星
    public string GetStar()
    {
        string result = "";
        int score = Convert.ToInt32(Eval("Score"));
        for (int i = 0; i < score; i++)
        {
            result += "<i class='staricon fa fa-star'></i>";
        }
        for (int i = 0; i < 5 - score; i++)
        {
            result += "<i class='staricon fa fa-star-o'></i>";
        }
        return result;
    }
    //用户名或匿名
    public string GetUserName()
    {
        switch (DataConvert.CLng(Eval("IsAnonymous")))
        {
            case 1:
                return "匿名用户";
            case 0:
            default:
                return B_User.GetUserName(Eval("CHoney"),Eval("CUName"));
        }
    }
    public string GetGroupName()
    {
        if (DataConvert.CLng(Eval("IsAnonymous")) == 1) { return ""; }
        int gid = DataConvert.CLng(Eval("GroupID"));
        DataRow[] dr = GroupDT.Select("GroupID=" + gid);
        if (dr.Length > 0)
        {
            return dr[0]["GroupName"].ToString();
        }
        return "";
    }
    //显示晒单图片
    public string GetImgs()
    {
        string result = "";
        string tlp = "<span class='imgsp'><img src='{0}' class='img80' onerror=\"this.src='/Images/nopic.gif'\"/></span>";
        string imgs = Eval("Imgs", "");
        if (!string.IsNullOrEmpty(imgs))
        {
            foreach (string img in imgs.Split('|'))
            {
                if (SafeSC.IsImage(img))
                { result += string.Format(tlp, img); }
            }
        }
        return result;
    }
}