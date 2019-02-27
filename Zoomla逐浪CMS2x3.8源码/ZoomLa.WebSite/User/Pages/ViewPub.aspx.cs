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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
public partial class User_Pages_ViewPub : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_Admin badmin = new B_Admin();
    private B_ModelField bfield = new B_ModelField();
    private B_Model bmodel = new B_Model();
    private B_Pub bpub = new B_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (Request.Form["DropDownList1"] != null)
            {
                this.Hiddenpagenum.Value = Request.Form["DropDownList1"];
            }
            else
            {
                this.Hiddenpagenum.Value = Request.QueryString["CurrentPage"];
            }

        }
        buser.CheckIsLogin();
        string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
        this.HiddenUserID.Value = buser.GetLogin().UserID.ToString();
        string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"].ToString()) ? "0" : Request.QueryString["Pubid"].ToString();
        string ModelID = bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString();
        M_AdminInfo ad = badmin.GetAdminLogin();
        this.HiddenPubRole.Value = ad.PubRole.ToString();
        //  string guang = Request.QueryString["guang"] == null ? "0" : Request.QueryString["guang"].ToString();
        //  this.HiddenGuang.Value = guang;
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
        this.HiddenTable.Value = model.TableName;
        this.HdnModelID.Value = ModelID.ToString();
        this.HiddenPubid.Value = Pubid;
        Bind();
        int pubtype = bpub.GetSelect(DataConverter.CLng(Pubid)).PubType;
        string pubtypename = "";
        switch (pubtype)
        {
            case 0:
                pubtypename = "评论";
                break;
            case 1:
                pubtypename = "投票";
                break;
            case 2:
                pubtypename = "活动";
                break;
            case 3:
                pubtypename = "留言";
                break;
            case 4:
                pubtypename = "调查";
                break;
            case 5:
                pubtypename = "统计";
                break;
        }
        this.Label1.Text = pubtypename;
       
    }

    private void Bind()
    {
        int CPage, temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }

        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));
        DataTable Cll;
        if(this.HiddenPubRole.Value=="1")
         Cll = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HiddenUserID.Value), 111);
        else
         Cll = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HiddenUserID.Value), 16);

        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 12;
        cc.CurrentPageIndex = CPage - 1;
        gvCard.DataSource = cc;
        gvCard.DataBind();

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

        Toppage.Text = "<a href=?Currentpage=0&pubid=" + this.HiddenPubid.Value + ">首页</a>";
        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&pubid=" + this.HiddenPubid.Value + ">上一页</a>";
        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&pubid=" + this.HiddenPubid.Value + ">下一页</a>";
        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&pubid=" + this.HiddenPubid.Value + ">尾页</a>";
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["Item"];
        if (!String.IsNullOrEmpty(CID) && buser.DelModelInfoAllo(this.HiddenTable.Value, CID))
        {
            Response.Write("<script language=javascript>alert('批量删除成功!');location.href='ViewSmallPub.aspx?pubid=" + this.HiddenPubid.Value + "';</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='ViewSmallPub.aspx?pubid=" + this.HiddenPubid.Value + "';</script>");
        }
    }
    public string showuse(string Pubstart, string id)
    {
       
        if (this.HiddenPubRole.Value == "1")
        {
            int ido = DataConverter.CLng(id);
            int sid = DataConverter.CLng(Pubstart);
            string str = "";
            //<a href="EditPubstart.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>" >审核</a>
            if (sid == 1)
            {
                str = "<a href=EditPubstart.aspx?Pubid=" + this.HiddenPubid.Value + "&ID=" + id + "&menu=true><span style='color:red;'>取消审核</span></a>";
            }
            else
            {
                str = "<a href=EditPubstart.aspx?Pubid=" + this.HiddenPubid.Value + "&ID=" + id + "&menu=false>审核</a>";
            }

            return str;
        }
        else
        {
            return "";
        }
    }

    public string returnlen(object str)
    {
        string intostr = str.ToString();
        return BaseClass.Left(intostr, 20);
    }



    //private void ShowGrid()
    //{
    //    DataTable dt = this.bfield.GetModelFieldListall(DataConverter.CLng(this.HdnModelID.Value)).Tables[0];
    //    Egv.AutoGenerateColumns = false;
    //    DataControlFieldCollection dcfc = Egv.Columns;
    //    dcfc.Clear();
    //    TemplateField tf = new TemplateField();
    //    CheckBox ch = new CheckBox();
    //    tf.HeaderText = "选择";
    //    tf.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    tf.ItemStyle.Width = Unit.Percentage(5);
    //    tf.ItemTemplate = new GenericItem();
    //    tf.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    //Egv.Columns.Add(tf);
    //    dcfc.Add(tf);

    //    BoundField bf12 = new BoundField();
    //    bf12.HeaderText = "ID";
    //    bf12.DataField = "ID";
    //    bf12.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    bf12.HeaderStyle.Width = Unit.Percentage(5);
    //    bf12.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    bf12.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    bf12.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    dcfc.Add(bf12);

    //    //BoundField bf2 = new BoundField();
    //    //bf2.HeaderText = "互动ID ";
    //    //bf2.DataField = "Pubupid";
    //    //bf2.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    //bf2.HeaderStyle.Width = Unit.Percentage(8);
    //    //bf2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //bf2.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    //dcfc.Add(bf2);


    //    //BoundField bf6 = new BoundField();
    //    //bf6.HeaderText = "标题";
    //    //bf6.DataField = "PubTitle";
    //    //bf6.HeaderStyle.Width = Unit.Percentage(27);
    //    //bf6.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //bf6.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //dcfc.Add(bf6);

    //    ButtonField Link3 = new ButtonField();
    //    Link3.ButtonType = ButtonType.Link;
    //    Link3.HeaderText = "标题";
    //    Link3.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    Link3.DataTextField = "PubTitle";
    //    Link3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    Link3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    Link3.ItemStyle.Width = Unit.Percentage(35);
    //    Link3.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    dcfc.Add(Link3);

    //    //BoundField bf3 = new BoundField();
    //    //bf3.HeaderText = "IP地址";
    //    //bf3.DataField = "PubIP";
    //    //bf3.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    //bf3.HeaderStyle.Width = Unit.Percentage(15);
    //    //bf3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //bf3.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    //dcfc.Add(bf3);

    //    BoundField bf4 = new BoundField();
    //    bf4.HeaderText = "参与人数";
    //    bf4.DataField = "Pubnum";
    //    bf4.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    bf4.HeaderStyle.Width = Unit.Percentage(10);
    //    bf4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    bf4.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    dcfc.Add(bf4);


    //    BoundField bf5 = new BoundField();
    //    bf5.HeaderText = "添加时间";
    //    bf5.DataField = "PubAddTime";
    //    bf5.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    bf5.HeaderStyle.Width = Unit.Percentage(12);
    //    bf5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    bf5.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    bf5.DataFormatString = "{0: yyyy-MM-dd}";
    //    dcfc.Add(bf5);


    //    //foreach (DataRow dr in dt.Rows)
    //    //{
    //    //    if (DataConverter.CBool(dr["ShowList"].ToString()))
    //    //    {
    //    //        BoundField bf = new BoundField();
    //    //        bf.HeaderText = dr["FieldAlias"].ToString();
    //    //        bf.DataField = dr["FieldName"].ToString();
    //    //        bf.HeaderStyle.Width = Unit.Percentage(DataConverter.CDouble(dr["ShowWidth"]));
    //    //        bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //        bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    //        bf.HtmlEncode = false;
    //    //        dcfc.Add(bf);
    //    //    }
    //    //}

    //    ButtonField Linka = new ButtonField();
    //    Linka.ButtonType = ButtonType.Link;
    //    Linka.HeaderText = "审核";
    //    Linka.Text = "审核";
    //    Linka.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    Linka.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    Linka.ItemStyle.Width = Unit.Percentage(5);
    //    Linka.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    dcfc.Add(Linka);

    //    ButtonField Link = new ButtonField();
    //    Link.ButtonType = ButtonType.Link;
    //    Link.HeaderText = "修改";
    //    Link.Text = "修改";
    //    Link.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    Link.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    Link.ItemStyle.Width = Unit.Percentage(5);
    //    Link.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    dcfc.Add(Link);


    //    ButtonField Link1 = new ButtonField();
    //    Link1.ButtonType = ButtonType.Link;
    //    Link1.HeaderText = "删除";
    //    Link1.Text = "删除";
    //    Link1.HeaderStyle.BorderColor = System.Drawing.Color.White;
    //    Link1.ItemStyle.Width = Unit.Percentage(5);
    //    Link1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
    //    Link1.ItemStyle.BorderColor = System.Drawing.Color.White;
    //    Link1.CausesValidation = true;
    //    dcfc.Add(Link1);


    //}

    // private void RepNodeBind()
    // {
    //     DataTable UserData;
    //     M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));
    //   //  if (this.HiddenGuang.Value == "guang")
    //         UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HiddenUserID.Value),16);
    //   //  else
    //     //    UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HiddenUserID.Value), 11);
    //     this.Egv.DataSource = UserData;

    //     Bind(UserData);
    //     //this.Egv.DataKeyNames = new string[] { "ID" };
    //     //this.Egv.DataBind();
    // }
    // protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    // {
    //     if (e.Row.RowType == DataControlRowType.DataRow)
    //     {
    //         string ModelID = this.HdnModelID.Value;
    //         string HiddenPubid = this.HiddenPubid.Value;

    //         //DataTable aa = buser.GetUserModeInfo(this.HiddenTable.Value, DataConverter.CLng(e.Row.Cells[1].Text), 12);
    //         //int Pubstart = DataConverter.CLng(aa.Rows[0]["Pubstart"]);

    //         LinkButton btn4 = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 3].Controls[0];
    //         //btn.PostBackUrl = "EditModelInfo.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[0].Text;
    //         btn4.Attributes.Add("href", "EditPubstart.aspx?Pubid=" + HiddenPubid + "&ID=" + e.Row.Cells[1].Text);

    //         //if (Pubstart == 0)
    //         //    btn4.Attributes.Add("Text", "1以审核");
    //         //else
    //         //    btn4.Attributes.Add("Text", "11审核");

    //         LinkButton btn = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
    //         //btn.PostBackUrl = "EditModelInfo.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[0].Text;
    //         btn.Attributes.Add("href", "EditPub.aspx?Pubid=" + HiddenPubid + "&ID=" + e.Row.Cells[1].Text);

    //         LinkButton btn1 = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
    //         btn1.Attributes.Add("href", "Delpub.aspx?Pubid=" + HiddenPubid + "&ID=" + e.Row.Cells[1].Text);
    //         btn1.Attributes.Add("onclick", "javascript:return confirm('你确认要删除此数据吗？');");

    //         LinkButton btn2 = (LinkButton)e.Row.Cells[2].Controls[0];
    //         //   btn2.Attributes.Add("href", "ViewSmallPub.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[1].Text);
    //         btn2.Attributes.Add("onclick", "javascript: window.open('ViewSmallPub.aspx?Pubid=" + HiddenPubid + "&ID=" + e.Row.Cells[1].Text + "', 'newwindow', 'height=500, width=800, top=200, left=150, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); ");
    //         //  btn2.Attributes.Add("target", "_blank");
    //     }
    // }
    //// private void Bind(DataTable dd)
    // {
    //     int CPage, temppage;
    //     temppage = DataConverter.CLng(this.Hiddenpagenum.Value);

    //     CPage = temppage;
    //     if (CPage <= 0)
    //     {
    //         CPage = 1;
    //     }

    //     DataTable Cll = dd;
    //     PagedDataSource cc = new PagedDataSource();
    //     cc.DataSource = Cll.DefaultView;
    //     cc.AllowPaging = true;
    //     cc.PageSize = 12;
    //     cc.CurrentPageIndex = CPage - 1;
    //     Egv.DataSource = cc;
    //     Egv.DataBind();
    //     Allnum.Text = Cll.DefaultView.Count.ToString();
    //     int thispagenull = cc.PageCount;//总页数
    //     int CurrentPage = cc.CurrentPageIndex;
    //     int nextpagenum = CPage - 1;//上一页
    //     int downpagenum = CPage + 1;//下一页
    //     int Endpagenum = thispagenull;
    //     if (thispagenull <= CPage)
    //     {
    //         downpagenum = thispagenull;
    //         Downpage.Enabled = false;
    //     }
    //     else
    //     {
    //         Downpage.Enabled = true;
    //     }
    //     if (nextpagenum <= 0)
    //     {
    //         nextpagenum = 0;
    //         Nextpage.Enabled = false;
    //     }
    //     else
    //     {
    //         Nextpage.Enabled = true;
    //     }
    //     Toppage.Text = "<a href=?Currentpage=0&Pubid = " + this.HiddenPubid.Value + ">首页</a>";
    //     Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&Pubid=" + this.HiddenPubid.Value + ">上一页</a>";
    //     Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&Pubid=" + this.HiddenPubid.Value + ">下一页</a>";
    //     Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&Pubid=" + this.HiddenPubid.Value + ">尾页</a>";
    //     Nowpage.Text = CPage.ToString();
    //     PageSize.Text = thispagenull.ToString();
    //     pagess.Text = cc.PageSize.ToString();


    //     if (!this.Page.IsPostBack)
    //     {
    //         for (int i = 1; i <= thispagenull; i++)
    //         {
    //             DropDownList1.Items.Add(i.ToString());
    //         }
    //     }

    // }
    // protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    // {

    //     foreach (GridViewRow oo in Egv.Rows)
    //     {
    //         ((CheckBox)oo.Cells[0].FindControl("pidCheckbox")).Checked = ((CheckBox)sender).Checked;
    //     }

    // }
    // protected void btn_DeleteRecords_Click(object sender, EventArgs e)
    // {

    //     int IDo;
    //     int ModelID = DataConverter.CLng(this.HdnModelID.Value);

    //     foreach (GridViewRow gvr in Egv.Rows)
    //     {
    //         if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
    //         {
    //             IDo = DataConverter.CLng(gvr.Cells[1].Text);

    //             buser.DelModelInfo(bmodel.GetModelById(ModelID).TableName, IDo);
    //         }
    //     }
    //     //pageneum2 = this.Hiddenpagenum.Value;
    //     //this.Hiddenpagenum.Value = "1";
    //     ShowGrid();
    //     this.RepNodeBind();
    //     //this.Hiddenpagenum.Value = pageneum2;

    // }

}
