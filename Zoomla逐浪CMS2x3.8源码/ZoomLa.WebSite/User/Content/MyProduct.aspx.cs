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
using ZoomLa.Common;

using ZoomLa.Model;
using ZoomLa.Components;

using System.Xml;
using System.Globalization;

public partial class User_Content_MyProduct : System.Web.UI.Page
{
    private B_Content bll = new B_Content();
    private B_Product proll = new B_Product();
    private B_Node bNode = new B_Node();
    private B_Model bmode = new B_Model();
    private B_User buser = new B_User();
    protected B_ModelField bfield = new B_ModelField();
    //protected B_UserProduct_T buserprT = new B_UserProduct_T();
    protected B_Product buserpro = new B_Product();
    protected B_BindFlolar bindflolar = new B_BindFlolar();
    protected B_Sensitivity sll = new B_Sensitivity();

    public int NodeID;
    public string flag;
    public string KeyWord;
    public M_UserInfo UserInfo;
    protected B_UserPromotions ups = new B_UserPromotions();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            buser.CheckIsLogin();
            //this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
            this.UserInfo = buser.GetLogin();
            if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
            {
                this.NodeID = 0;
            }
            else
            {
                this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
            }
            if (!string.IsNullOrEmpty(Request["method"]))
            {
                if (Request["method"] == "Del" && (!string.IsNullOrEmpty(Request["ID"])))
                {
                    Del(Request["ID"]);
                    Response.Redirect("/User/Content/Myproduct.aspx?NodeID=" + base.Request.QueryString["NodeID"]);
                }
            }
            if (this.NodeID == 0)
                this.NodeID = bNode.GetFirstNode(0);
            this.flag = base.Request.QueryString["type"];
            this.HiddenField2.Value = this.flag;
            this.HiddenField1.Value = this.NodeID.ToString();
            if (this.NodeID != 0)
            {
                GetNodePreate(this.NodeID);
                M_Node nod = this.bNode.GetNodeXML(this.NodeID);
                string ModeIDList = nod.ContentModel;
                string[] ModelID = ModeIDList.Split(',');
                string AddContentlink = "";
 
                for (int i = 0; i < ModelID.Length; i++)
                {
                    
                    AddContentlink = AddContentlink + "<input name=\"btn" + i.ToString() + "\" class=\"btn btn-primary\" type=\"button\" value=\"添加" + this.bmode.GetModelById(DataConverter.CLng(ModelID[i])).ItemName + "\" onclick=\"javascript:window.location.href='AddProduct.aspx?ModelID=" + ModelID[i] + "&NodeID=" + this.NodeID + "';\" />&nbsp;&nbsp;";
                    if (bmode.GetModelById(DataConverter.CLng(ModelID[i])).Islotsize)
                    {
                        AddContentlink = AddContentlink + "<input name=\"btn" + i.ToString() + "\" class=\"btn btn-primary\"  type=\"button\" value=\"批量添加" + this.bmode.GetModelById(DataConverter.CLng(ModelID[i])).ItemName + "\" onclick=\"javascript:window.location.href='Release.aspx?ModelID=" + ModelID[i] + "&NodeID=" + this.NodeID + "';\" />&nbsp;&nbsp;";
                    }
               
                } 
                this.lblAddContent.Text = AddContentlink;
                standardPro();
            }
            else
            {
                //this.lblNodeName.Text = "全部节点";
                this.lblAddContent.Text = "";
            }

            MyBind();
        }
    }

    public bool GetIsRe(string status)
    {
        if (status == "True")
        {
            return true;
        }
        else
            return false;
    }
    public bool GetIsNRe(string status)
    {
        if (status == "True")
        {
            return false;
        }
        else
            return true;
    }

    private void GetNodePreate(int prentid)
    {
        M_Node nodes = bNode.GetNodeXML(prentid);
        GetMethod(nodes);//发布
        //GetMethodShow(nodes);//浏览
        if (nodes.ParentID > 0)
        {
            GetNodePreate(nodes.ParentID);
        }
    }

    private void GetMethod(M_Node nodeinfo)
    {
        if (nodeinfo.Purview != null && nodeinfo.Purview != "")
        {
            DataRow auitdr = bNode.GetNodeAuitDT(nodeinfo.Purview).Rows[0];

            string input_v = auitdr["input"].ToString();
            if (input_v != null && input_v != "")
            {
                string tmparr = "," + input_v + ",";

                switch (this.UserInfo.Status)
                {
                    case 0://已认证
                        if (tmparr.IndexOf("," + this.UserInfo.GroupID.ToString() + ",") == -1)
                        {
                            if (tmparr.IndexOf(",-1,") == -1)
                            {
                                function.WriteErrMsg("很抱歉！您没有权限在该栏目下发布信息！");
                            }
                        }
                        break;
                    default://未认证
                        if (tmparr.IndexOf(",-2,") == -1)
                        {
                            function.WriteErrMsg("很抱歉！您没有权限在该栏目下发布信息！");
                        }
                        break;
                }
            }
            else
            {
                //为空
                function.WriteErrMsg("很抱歉！您没有权限在该栏目下发布信息！");
            }
        }
    }


    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        this.NodeID = DataConverter.CLng(this.HiddenField1.Value);
        //string SearchTitle = Request.Form["TxtSearchTitle"];
        DataTable dt = proll.U_GetProductAll(mu.UserID,NodeID);
        EGV.DataSource = dt;
        this.EGV.DataKeyNames = new string[] { "ID" };
        this.EGV.DataBind();
    }
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        M_UserInfo infos = buser.GetLogin();
        M_UserPromotions upsinfo = ups.GetSelect(this.NodeID, infos.GroupID);

        if (upsinfo.pid != 0)
        {
            if (upsinfo.Deleted != 1)
            {
                function.WriteErrMsg("您所在会员组无删除权限！");
            }
        }
        string ids = Request.Form["idchk"];
        if (string.IsNullOrEmpty(ids)) { function.WriteErrMsg("请先选择需要操作的商品!"); }
        proll.setproduct(5, ids);
        MyBind();
    }
    protected void Del(string ID)
    {
        M_UserInfo infos = buser.GetLogin();
        M_UserPromotions upsinfo = ups.GetSelect(this.NodeID, infos.GroupID);
        if (upsinfo!=null&&upsinfo.pid != 0)
        {
            if (upsinfo.Deleted != 1)
            {
                function.WriteErrMsg("您所在会员组无删除权限！");
            }
        }


        string Id = ID;
        M_Product pmodel = proll.GetproductByid(DataConverter.CLng(Id));
        string title = pmodel.Proname;
        this.proll.DeleteByID(DataConverter.CLng(Id), pmodel);
        MyBind();
    }
    public string GetStatus(string status)
    {
        int s = DataConverter.CLng(status);
        if (s == 0)
            return "待审核";
        if (s == 1)
            return "已审核";
        return "回收站";
    }
    public bool ChkStatus(string status)
    {
        int s = DataConverter.CLng(status);
        if (s == 99)
            return false;
        else
            return true;
    }
    public string GetUrl(string infoid)
    {
        int p = DataConverter.CLng(infoid);
        M_Product cinfo = this.proll.GetproductByid(p);
        if (cinfo.MakeHtml == 1)
            return SiteConfig.SiteInfo.SiteUrl + cinfo.ID.ToString() + ".html";
        else
            return "/Shop.aspx?ID=" + p;
    }
    public string GetCteate(string IsCreate)
    {
        int s = DataConverter.CLng(IsCreate);
        if (s != 1)
            return "<font color=red>×</font>";
        else
            return "<font color=green>√</font>";
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.TxtSearchTitle.Text.Trim()))
        {
            MyBind();
        }
    }
    public string formatcs(string money, string ProClass, string point)
    {
        string outstr;
        double doumoney, tempmoney;
        doumoney = DataConverter.CDouble(money);
        tempmoney = System.Math.Round(doumoney, 2);
        outstr = tempmoney.ToString();
        if (ProClass != "3")
        {
            if (outstr.IndexOf(".") == -1)
            {
                outstr = outstr + ".00";
            }
        }
        else
        {
            outstr = point;
        }
        return outstr;
    }
    public string formatnewstype(string type, string id)
    {
        int Sid = DataConverter.CLng(id);
        M_Product pinfo = proll.GetproductByid(Sid);
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 2)
        {
            restring = "<font color=red>特价</font>";
        }
        //else if (newtype == 1)
        //{
        //    restring = "<font color=blue>正常</font>";
        //}
        else if (newtype == 3)
        {
            restring = "<font color=blue>积分商品</font>";
        }
        if (pinfo.Istrue != 1)
        {
            restring = "<font color=#999999>待审核</font>";
        }
        if (!pinfo.Recycler)
        {
            restring = "<font color=blue>正常</font>";
        }
        else
        {
            restring = "<font color=#999999>已删除</font>";
        }
        return restring;
    }
    public string forisnew(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=green>新</font>";
        }
        else if (newtype == 0)
        {
            restring = "&nbsp;&nbsp;";
        }
        return restring;
    }

    public string forishot(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=red>热</font>";
        }
        else if (newtype == 0)
        {
            restring = "&nbsp;&nbsp;";
        }
        return restring;
    }
    public string forisbest(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=blue>精</font>";
        }
        else if (newtype == 0)
        {
            restring = "&nbsp;&nbsp;";
        }
        return restring;
    }
    public string formattype(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=blue>√</font>";
        }
        else if (newtype == 0)
        {
            restring = "<font color=red>×</font>";
        }
        return restring;
    }
    public string getproimg(string type)
    {
        string restring = "";
        string upload = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";

        if (string.IsNullOrEmpty(type))
        {
            restring = "<img src=../../UploadFiles/nopic.gif border=0 width=60 height=45>";
        }
        if (!string.IsNullOrEmpty(type))
        {
            type = type.ToLower();
        }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            if (type.StartsWith("http://", true, CultureInfo.CurrentCulture) || type.StartsWith("/", true, CultureInfo.CurrentCulture) || type.StartsWith(upload, true, CultureInfo.CurrentCulture))
                restring = "<img src=../../" + type + " width=60 height=45>";
            else
            {
                restring = "<img src=../../" + upload + type + " border=0 width=60 height=45>";
            }
        }
        else
        {
            restring = "<img src=../../UploadFiles/nopic.gif border=0 width=60 height=45>";
        }
        return restring;
    }
    public string GetStendCon(string nodeid, string modeid, string id, string isstend, string usershopid)
    {
        if (string.IsNullOrEmpty(Request["recycle"]) || Request["recycle"] != "1")
        {
            if (DataConverter.CLng(isstend) == 0)  //不是标准商品
            {
                return "<a href='MyProduct.aspx?menu=addModel&NodeID=" + nodeid + "&ModelID=" + modeid + "&id=" + id + "' onclick='return confirm(\"确定添加？\")'>标准商品</a>";
            }
            else   //已经是标准商品
            {
                return "<a href='MyProduct.aspx?menu=DelModel&NodeID=" + nodeid + "&ModelID=" + modeid + "&id=" + id + "&Usershopid=" + usershopid + "' onclick='return confirm(\"确定取消？\")'>取消标准</a>";
            }
        }
        else
            return "";
    }
    public void standardPro()
    {
        string menu;
        menu = Request.QueryString["menu"];
        switch (menu)
        {
            case "DelModel":
                bool result = DelModel(DataConverter.CLng(Request.QueryString["id"]), DataConverter.CLng(Request.QueryString["Usershopid"]));
                if (result)
                {
                    Response.Write("<script language='javascript'>alert('取消成功');location.href='MyProduct.aspx?NodeID=" + this.NodeID + "';</script>");
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('取消失败');location.href='MyProduct.aspx?NodeID=" + this.NodeID + "';</script>");
                }
                break;
        }
    }
    public void AddProduModel()
    {
        //int ModelID = DataConverter.CLng(Request.QueryString["ModelID"]);
        //int NodeID = DataConverter.CLng(Request.QueryString["NodeID"]);
        //DataTable dt = this.bfield.GetModelFieldList(ModelID).Tables[0];
        //Call commonCall = new Call();
        //DataTable table = commonCall.GetDTFromPage(dt, this.Page, ViewState);
        //M_CommonData CCate = new M_CommonData();
        //M_Product mprodu = proll.GetproductByid(DataConverter.CLng(Request.QueryString["id"]));
        //proll.Add(table, mprodu, CCate);
    }
    public bool DelModel(int id, int usershopID)
    {
        M_Product userpro = buserpro.GetproductByid(usershopID);
        if (userpro != null)
        {
            bool result = buserpro.DeleteByID(userpro.ID, userpro);
            bindflolar.DeleteBySid(userpro.ID);
            if (result)
            {
                return proll.GetUpdateInsend(id, 0, 0);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            if (dr["istrue"].ToString().Equals("0"))//待审核下才允许修改
            {
                LinkButton lnk = e.Row.FindControl("Edit_Lnk") as LinkButton;
                lnk.Visible = true;
            }
        }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            M_Product prossinfo = proll.GetproductByid(DataConverter.CLng(e.CommandArgument.ToString()));
            int ModelID = prossinfo.ModelID;
            int NodeID = prossinfo.Nodeid;
            Page.Response.Redirect("AddProduct.aspx?menu=edit&NodeID=" + NodeID.ToString() + "&ModelID=" + ModelID.ToString() + "&ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Del")
        {
            Del(e.CommandArgument.ToString());
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.EGV.PageIndex = e.NewPageIndex;
        this.MyBind();
    }
}
