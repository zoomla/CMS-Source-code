using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class MIS_ZLOA_ContentManage : System.Web.UI.Page
{
    protected B_OA_Document oaBll = new B_OA_Document();
    protected B_User buser = new B_User();
    protected B_Content bll = new B_Content();
    public string str = "置顶";
    //用户组权限
    protected B_UserPromotions promBll = new B_UserPromotions();
    protected M_UserPromotions promMod = new M_UserPromotions();
    //角色权限
    protected B_UserPurview purBll = new B_UserPurview();
    //客户端存储，具体操作时再验证一次
    private bool TopAuth
    {
        get
        {
            if (ViewState["TopAuth"] == null)
            {
                if (!string.IsNullOrEmpty(buser.GetLogin().UserRole))
                {
                    TopAuth = purBll.Auth("OATop", buser.GetLogin().UserRole, Convert.ToInt32(Request.QueryString["NodeID"]));
                }
                else
                    TopAuth = false;
            }
            return DataConverter.CBool(ViewState["TopAuth"].ToString());
        }
        set { ViewState["TopAuth"] = value; }
    }
    private bool EditAuth
    {
        get
        {
            if (ViewState["EditAuth"] == null)
            {
                if (!string.IsNullOrEmpty(buser.GetLogin().UserRole))
                {
                    EditAuth = purBll.Auth("OAEdit", buser.GetLogin().UserRole, Convert.ToInt32(Request.QueryString["NodeID"]));
                }
                else
                    EditAuth = false;
            }
            return DataConverter.CBool(ViewState["EditAuth"].ToString());
        }
        set { ViewState["EditAuth"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["NodeID"]))
            {
                //显示所有节点
                nodeNameL.Text = "全部内容";
            }
            else
            {
                int nodeID = Convert.ToInt32(Request.QueryString["NodeID"].Trim());
                nodeNameL.Text = GetNodeName();
                //会员组权限
                //promMod = promBll.GetSelect(nodeID, buser.GetLogin().GroupID);
                //if (promMod != null)
                //{
                //    if (promMod.addTo > 0)
                //    {
                //        addBtn.Visible = true;
                //    }
                //    if (promMod.Deleted > 0)
                //    {
                //        batDelBtn.Visible = true;
                //    }
                //}
            }
            //角色权限(置顶,删除)
            DataBind();
        }
    }
    private void DataBind(string keys="")
    {
        DataTable dt = new DataTable();
        //if (string.IsNullOrEmpty(Request.QueryString["NodeID"]))//显示全部
        //{
        //    string nodeIDS = bll.GetNodeIDS(OAConfig.ModelID);
        //    dt = bll.GetContentByNodeS(nodeIDS);
        //}
        //else
        //
            int id = DataConverter.CLng(Request.QueryString["NodeID"]);
            dt = bll.GetContenByNodeOA(id);
        //}
            string key = SearchKey;
        if (!string.IsNullOrEmpty(key))
        {
            dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
            dt = dt.DefaultView.ToTable();
        }
        dt.DefaultView.Sort = "EliteLevel desc,CreateTime desc";
        dt = dt.DefaultView.ToTable();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    private string SearchKey
    {
        get
        {
            return ViewState["SearchKey"] as string;
        }
        set
        {
            ViewState["SearchKey"] = value;
        }

    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        M_CommonData cdataMod = new M_CommonData();
        switch (e.CommandName.ToLower())
        {
            case "read":
                Response.Redirect("ViewContent.aspx?Gid=" + e.CommandArgument.ToString() + "&NodeID=" + Request.QueryString["NodeID"]);
                break;
            case "settop":
                cdataMod = bll.GetCommonData(Convert.ToInt32(e.CommandArgument.ToString()));
                if (cdataMod.EliteLevel == 1)
                    cdataMod.EliteLevel = 0;
                else                    
                    cdataMod.EliteLevel = 1;
                bll.UpdateByID(cdataMod);
                break;
            case "edit1":
                Response.Redirect("AddContent.aspx?GID=" + e.CommandArgument.ToString());
                break;
            case "del2"://本人才拥有删除权限
                cdataMod = bll.GetCommonData(Convert.ToInt32(e.CommandArgument.ToString()));
                if (buser.GetLogin().UserName.Equals(cdataMod.Inputer))
                    bll.SetDel(Convert.ToInt32(e.CommandArgument.ToString()));//进入回收站
                break;
        }
        DataBind();
    }
    public string GetStatus(string status)
    {
        return ZLEnum.GetConStatus(DataConverter.CLng(status));
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            bool isSelf = dr["Inputer"].ToString().Equals(buser.GetLogin().UserName);
            e.Row.Attributes["title"] = "双击查看文章";
            e.Row.Attributes["style"] = "cursor:pointer";
            e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='ViewContent.aspx?Gid={0}'", this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.FindControl("l1").Visible = TopAuth;
            if (EditAuth || isSelf)
                e.Row.FindControl("l3").Visible = true;
            else
                e.Row.FindControl("l3").Visible = false;
            e.Row.FindControl("l4").Visible = isSelf;

        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        SearchKey = searchText.Text;
        DataBind();
    }
    //---------Tool
    public string GetNodeName() 
    {
        string result = "全部内容", r= Request.QueryString["NodeID"].Trim();
        if (!string.IsNullOrEmpty(r))
        {
            result = OACommon.GetNodeID(r, 1) + "中心";
        }
        return result;
    }
    protected void batDelBtn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idChk"]))
        {
            string[] chkArr = Request.Form["idChk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < chkArr.Length; i++)
                this.bll.SetDel(Convert.ToInt32(chkArr[i]));
            DataBind();
        }
    }
    public string GetStr(string editlevel)
    {
        if (editlevel == "1")
            return "取消置顶";
        else
            return "置顶";
    }
}