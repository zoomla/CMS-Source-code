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

public partial class manage_Shop_AgioCommodityShow : CustomerPageAction
{

    B_Product pll = new B_Product();
    B_BindPro bll = new B_BindPro();
    B_Scheme bs = new B_Scheme();
    B_Node bn = new B_Node();
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        
        string KeyWord="";
        string str="";
        if (Request.QueryString["KeyWord"] != null && Request.QueryString["KeyType"]!=null)
        {
            KeyWord = Request.QueryString["KeyWord"];
            if (!string.IsNullOrEmpty(KeyWord))
            {
                str = bs.GetSelect(int.Parse(KeyWord)).SList;
                if (Request.QueryString["KeyType"] == "1")
                {
                    Page_list(pll.GetProWhere(str));
                    tdnode.Visible = false;
                    tr1.Visible = true;
                }
                else
                {
                    string[] listarr = str.Split(new string[] { "," }, StringSplitOptions.None);
                    string[][] x=new string[listarr.Length][];
                    foreach(string s in listarr)
                    {
                        M_Node mn=bn.GetNodeXML(DataConverter.CLng(s));

                        tdnode.InnerHtml += GetIcon(mn.NodeName, mn.NodeID.ToString(), mn.Depth.ToString(), mn.NodeType.ToString()) + "<br/>";
                    }
                    tdnode.Visible = true;
                    tr1.Visible = false;
                }
            }
            
        }
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li>商品管理</li>");
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        Pagetable.DataSource = Cll;
        Pagetable.DataBind();
    }
    #endregion

    public string getproimg(string type)
    {
        string restring;
        restring = "";

        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            restring = "<img src=../../" + type + " border=0 width=60 height=45>";
        }
        else
        {
            restring = "<img src=../../UploadFiles/nopic.gif border=0 width=60 height=45>";
        }
        return restring;

    }

    public string GetIcon(string NodeName, string NodeID, string Depth, string NodeType)
    {
        string outstr = "";
        int Dep = DataConverter.CLng(Depth);
        int nodetype = DataConverter.CLng(NodeType);

        if (Dep > 0)
        {
            for (int i = 1; i <= Dep - 1; i++)
            {
                //outstr = outstr + "|";
                outstr = outstr + "<img src=\"/Images/TreeLineImages/tree_line4.gif\" border=\"0\" width=\"19\" height=\"20\" />";
            }
            outstr = outstr + "<img src=\"/Images/TreeLineImages/t.gif\" border=\"0\" />";
        }
        if (nodetype == 0 || nodetype == 1)
        {
            outstr = outstr + "<img src=\"/Images/TreeLineImages/plus.gif\" border=\"0\" />";
            outstr = outstr + "<span>" + NodeName + "</span>";
        }

        return outstr;
    }
}
