using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Components;
using System.Text;

public partial class User_Content_ShowPub : System.Web.UI.Page
{
    private B_ModelField bfield = new B_ModelField();
    B_Content bc = new B_Content();
    B_User bu = new B_User();
    B_Model bmodel = new B_Model();
    B_ModelField bmf = new B_ModelField();
    B_Pub pll = new B_Pub();
    private bool Visible_V = true;
    public string GID = "";
    public int ModelID
    {
        get { return DataConverter.CLng(ViewState["model"]); }
        set { ViewState["model"] = value; }
    }
    public int PageNum
    {
        get { return DataConverter.CLng(ViewState["pagenum"]); }
        set { ViewState["pagenum"] = value; }
    }
    public int PubID
    {
        get { return DataConverter.CLng(ViewState["pubid"]); }
        set { ViewState["pubid"] = value; }
    }
    public string PubTable
    {
        get { return ViewState["table"] + ""; }
        set { ViewState["table"] = value; }
    }
    public int NodeID
    {
        get { return DataConverter.CLng(ViewState["nodeid"]); }
        set { ViewState["nodeid"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? "0" : Request.QueryString["ID"].ToString();

        if (Request.QueryString["menu"] != null)
        {
            string menu = Request.QueryString["menu"];
            string Optimal = Request.QueryString["Optimal"];
            M_Pub pubinfo = pll.GetSelect(DataConverter.CLng(Request.QueryString["pid"]));

            if (menu == "setinfo")
            {
                if (Optimal == "0")
                {
                    pll.UpdatePubModelByOptimal(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    pll.UpdatePubModelById(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    Response.Redirect("ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"]);
                    //Response.Write("<script>location.href='ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"] + "'</script>");
                }
                else if (Optimal == "1")
                {
                    pll.UpdatePubModelOptimal(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    Response.Redirect("ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"]);
                    //Response.Write("<script>location.href='ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"] + "'</script>");
                }
            }
            if (menu == "setdb")
            {
                if (Optimal == "0")
                {
                    pll.Getdb(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    Response.Redirect("ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"]);
                }
                else if (Optimal == "2")
                {
                    pll.UpdatePubModelOptimal(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    Response.Redirect("ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"]);
                }
            }
            if (menu == "setnodb")
            {
                if (Optimal == "0")
                {
                    pll.Getnodb(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    Response.Redirect("ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"]);
                }
                else if (Optimal == "-1")
                {
                    pll.UpdatePubModelOptimal(pubinfo.PubTableName, DataConverter.CLng(Request.QueryString["GID"]));
                    Response.Redirect("ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"]);
                }
            }
        }

        M_CommonData mc = bc.GetCommonData(DataConverter.CLng(GID));
        ModelID = mc.ModelID;
        NodeID = mc.NodeID;
        if (mc.Inputer.Equals(bu.GetLogin().UserName))
        {
            Visible_V = true;
        }
        else
        {
            Visible_V = false;
        }
        hfUsername.Value = mc.Inputer;
        DataTable aas = txtShowGrid();
        DataTable UserData = bc.GetContentByItems(mc.TableName, mc.GeneralID);

        DetailsView1.DataSource = UserData.DefaultView;
        DetailsView1.DataBind();
        string pid = base.Request.QueryString["pid"];
        if (!string.IsNullOrEmpty(pid))
        {
            M_Pub pm = pll.GetSelect(DataConverter.CLng(pid));
            PubID = pm.Pubid;
            ModelID = pm.PubModelID;
            PubTable = pm.PubTableName;
        }
        RepNodeBind(Visible_V);
    }

    public bool GetisLoginUser()
    {
        if (hfUsername.Value.Equals(bu.GetLogin().UserName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RepNodeBind(bool result)
    {
        DataTable UserData = new DataTable();
        M_ModelInfo model = bmodel.GetModelById(ModelID);
        int GID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"].ToString());
        if (result)
        {
            UserData = pll.GetModelPubuPIdAll(0, GID, PubTable);
        }
        else
        {
            UserData = pll.GetModelPubuUserName(0, GID, bu.GetLogin().UserName, PubTable);
        }
        this.Egv.DataSource = UserData;
        if (UserData != null)
        {
            Bind(UserData);
        }
        //this.Egv.DataKeyNames = new string[] { "ID" };
        this.Egv.DataBind();
    }

    public string GetIco(string Optimal)
    {
        string returnstring = "";
        switch (Optimal)
        {
            case "0":
                returnstring = "/User/Images/Good.png";
                break;
            case "1":
                returnstring = "/User/Images/by.png";
                break;
            case "-1":
                returnstring = "/User/Images/Noby.png";
                break;
        }
        return returnstring;
    }

    private void Bind(DataTable dd)
    {
        int CPage, temppage;
        temppage = PageNum;
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }

        DataTable Cll = dd;

        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 10;
        cc.CurrentPageIndex = CPage - 1;
        Egv.DataSource = cc;
        Egv.DataBind();
        Allnum.Text = Cll.DefaultView.Count.ToString();
        int thispagenull = cc.PageCount;//总页数
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//上一页
        int downpagenum = CPage + 1;//下一页
        int Endpagenum = thispagenull;
        if (thispagenull <= CPage)
        {
            downpagenum = thispagenull;
            Downpage.Enabled = false;
        }
        else
        {
            Downpage.Enabled = true;
        }
        if (nextpagenum <= 0)
        {
            nextpagenum = 0;
            Nextpage.Enabled = false;
        }
        else
        {
            Nextpage.Enabled = true;
        }

        Toppage.Text = "<a href=?Currentpage=0&pubid = " + PubID + ">首页</a>";
        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&pubid=" + PubID + ">上一页</a>";
        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&pubid=" + PubID + ">下一页</a>";
        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&pubid=" + PubID + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();

        if (!this.Page.IsPostBack)
        {
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
        }
    }

    private DataTable txtShowGrid()
    {
        DataTable dt = this.bfield.GetModelFieldList(ModelID);
        DetailsView1.AutoGenerateRows = false;
        DataControlFieldCollection dcfc = DetailsView1.Fields;

        dcfc.Clear();
        BoundField bf2 = new BoundField();
        bf2.HeaderText = "ID";
        bf2.DataField = "GeneralID";
        bf2.HeaderStyle.Width = Unit.Percentage(15);
        bf2.HeaderStyle.CssClass = "tdbgleft";
        bf2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Left;

        dcfc.Add(bf2);

        BoundField bf5 = new BoundField();
        bf5.HeaderText = "标题";
        bf5.DataField = "Title";
        bf5.HeaderStyle.CssClass = "tdbgleft";
        bf5.HeaderStyle.Width = Unit.Percentage(15);
        bf5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        dcfc.Add(bf5);

        foreach (DataRow dr in dt.Rows)
        {
            BoundField bf = new BoundField();
            bf.HeaderText = dr["FieldAlias"].ToString();
            bf.DataField = dr["FieldName"].ToString();
            bf.HeaderStyle.Width = Unit.Percentage(15);
            bf.HeaderStyle.CssClass = "tdbgleft";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.HtmlEncode = false;
            dcfc.Add(bf);
        }
        return dt;
    }
    //用户头像
    protected string GetImg(string uid, string uname)
    {
        StringBuilder sb = new StringBuilder();
        M_Uinfo muinfo = bu.GetUserBaseByuserid(DataConverter.CLng(uid));
        sb.Append("<table  width='100%'  height='100px' >");
        sb.Append("    <tr>");
        sb.Append("        <td align='center'>");
        sb.Append("           <img src='" + muinfo.UserFace.Replace("~", "") + "' width='" + ((muinfo.FaceWidth == 0) ? 100 : muinfo.FaceWidth) + "px'  height='" + ((muinfo.FaceHeight == 0) ? 100 : muinfo.FaceHeight) + "px' />");
        sb.Append("        </td>");
        sb.Append("    </tr>");
        sb.Append("    <tr>");
        sb.Append("        <td align='center'>");
        sb.Append("           " + uname);
        sb.Append("        </td>");
        sb.Append("    </tr>");
        sb.Append("</table>");
        return sb.ToString();
    }
    //回复
    protected string GetTable(string id, string cid)
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = pll.GetModelPubuPIdAll(DataConverter.CLng(id), DataConverter.CLng(cid), PubTable);
        if (dt != null && dt.Rows.Count > 0)
        {
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.Append("<table  width='100%'  border='1' cellspacing='0' cellpadding='0'>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("    <tr>");
                sb.Append("        <td align='left'>");
                sb.Append(dt.Rows[i]["PubUserName"] + "&nbsp;[<a href='javascript:void(0);' onclick=\"javascript:window.open('Reply.aspx?ID=" + dt.Rows[i]["ID"] + "&pubid=" + PubID + "','', 'width=600,height=300,resizable=0,scrollbars=yes');\">回复</a>]：" + dt.Rows[i]["PubTitle"] + "<br/>&nbsp;&nbsp;" + dt.Rows[i]["PubContent"] + "<br />" + GetTable(dt.Rows[i]["ID"] + "") + "<br/>" + dt.Rows[i]["PubAddTime"]);
                sb.Append(GetTable(dt.Rows[i]["ID"] + "", cid));
                sb.Append("        </td>");
                sb.Append("    <tr>");
            }
            sb.Append("</table>");
        }
        return sb.ToString();
    }
    protected string GetTable(string id)
    {
        DataTable dt1 = pll.GetPubModeById(DataConverter.CLng(id), PubTable);
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            DataTable dt = bmf.GetModelFieldListall(ModelID);
            StringBuilder builder = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (DataConverter.CLng(dt.Rows[i]["RestoreField"].ToString()) == 1)
                    {
                        builder.Append(dt.Rows[i]["FieldAlias"] + "：" + dt1.Rows[0][dt.Rows[i]["FieldName"].ToString()] + "<br />");
                    }
                }
            }
            return builder.ToString();
        }
        else
        {
            return null;
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        DataListItem dli = (DataListItem)lb.NamingContainer;
        string s = Egv.DataKeys[dli.ItemIndex].ToString();


        if (lb.CommandArgument == "0")
        {
            pll.UpdatePubModelByOptimal(PubTable, DataConverter.CLng(s));
            pll.UpdatePubModelById(PubTable, DataConverter.CLng(s));
            Response.Write("<script>location.href='ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"] + "'</script>");
        }
        else
        {
            pll.UpdatePubModelOptimal(PubTable, DataConverter.CLng(s));
            Response.Write("<script>location.href='ShowPub.aspx?ID=" + Request.QueryString["ID"] + "&pid=" + Request.QueryString["pid"] + "'</script>");
        }
    }
    protected bool GetVisible()
    {
        return Visible_V;
    }
}