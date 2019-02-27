using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Text;
using ZoomLa.Model.Page;
using ZoomLa.BLL.Page;
public partial class User_Pages_addnode : System.Web.UI.Page
{
    protected B_User ull = new B_User();
    protected B_PageReg pll = new B_PageReg();
    protected B_Node nll = new B_Node();
    protected B_Templata tll = new B_Templata();
    protected M_UserInfo uinfo;
    public int TempID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SiteConfig.YPage.UserCanNode) { function.WriteErrMsg("不允许自建栏目!"); }
        this.uinfo=ull.GetLogin();
        if (!IsPostBack)
        {
            DataTable alltable = nll.Sel();
            this.ClassNode.DataSource = alltable;
            this.ClassNode.DataValueField = "NodeID";
            this.ClassNode.DataTextField = "NodeName";
            this.ClassNode.DataBind();

            this.nodetxt.Text = "添加栏目";
            if (Request.QueryString["menu"] != null)
            {
                string menu = Request.QueryString["menu"].ToString();
                if (menu == "edit")
                {
                    this.nodetxt.Text = "修改栏目";
                    M_Templata tinfo = tll.Getbyid(TempID);
                    this.tid.Value = tinfo.TemplateID.ToString();
                    this.Title.Text = tinfo.TemplateName;
                    this.NodeOrder.Text = tinfo.OrderID.ToString();
                    this.RadioButtonList1.SelectedValue = tinfo.IsTrue.ToString();
                    this.PageMetakeyinfo.Text = tinfo.PageMetakeyinfo.ToString();
                    this.keyword.Text = tinfo.PageMetakeyword.ToString();
                    this.Nodeimgtext.Text = tinfo.Nodeimgtext.ToString();
                    this.ClassNode.SelectedValue = tinfo.ParentNode.ToString();
                    this.ClassNode.Items.Insert(0, "选择捆绑节点");
                }
            }
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        M_PageReg pageinfo = pll.GetSelectByUserID(uinfo.UserID);
        M_Templata lata = new M_Templata();
        if (Request.Form["tid"] != "")
        {
            lata = tll.Getbyid(DataConverter.CLng(Request.Form["tid"]));
        }

        lata.UserID = this.uinfo.UserID;
        lata.Username = this.uinfo.UserName;
        lata.Addtime = DateTime.Now;
        lata.ContentFileEx = "html";
        lata.IsTrue = DataConverter.CLng(this.RadioButtonList1.SelectedValue);
        lata.NodeFileEx = "html";
        lata.OpenType = "_blank";
        lata.OrderID = DataConverter.CLng(this.NodeOrder.Text);
        lata.PageMetakeyinfo = PageMetakeyinfo.Text;
        lata.PageMetakeyword = keyword.Text;
        lata.TemplateName = this.Title.Text;
        lata.Identifiers = GetChineseFirstChar(this.Title.Text);
        lata.TemplateType = 2;
        lata.TemplateUrl = "";
        int selectnodeid = DataConverter.CLng(ClassNode.SelectedValue);
        lata.ParentNode = selectnodeid;
        lata.Modelinfo = nll.GetNodeXML(selectnodeid).ContentModel;
        lata.UserGroup = pageinfo.NodeStyle.ToString();
        lata.Nodeimgtext = Nodeimgtext.Text;

        if (TempID<=0)
        {
            tll.Add(lata);
            function.WriteSuccessMsg("修改成功!", "ClassManage.aspx");
        }
        else
        {
            lata.TemplateID = TempID;
            tll.Update(lata);
            function.WriteSuccessMsg("修改成功!", "ClassManage.aspx");
        }

    }

    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClassManage.aspx");
    }

    /// <summary>
    /// 中文转换声母
    /// </summary>
    /// <param name="chineseStr"></param>
    /// <returns></returns>
    public string GetChineseFirstChar(string chineseStr)
    {
        StringBuilder sb = new StringBuilder();
        int length = chineseStr.Length;
        for (int i = 0; i < length; i++)
        {
            string chineseChar = chineseStr[i].ToString();
            sb.Append(GetpyChar(chineseChar));
        }
        return sb.ToString().ToUpper();
    }

    /// <summary>
    /// 获得拼音
    /// </summary>
    /// <param name="cn"></param>
    /// <returns></returns>
    public string GetpyChar(string cn)
    {
        #region 处理过程
        byte[] arrCN = Encoding.Default.GetBytes(cn);
        if (arrCN.Length > 1)
        {
            int area = (short)arrCN[0];
            int pos = (short)arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                }
            }
            return cn;
        }
        else return cn;
        #endregion
    }
}
