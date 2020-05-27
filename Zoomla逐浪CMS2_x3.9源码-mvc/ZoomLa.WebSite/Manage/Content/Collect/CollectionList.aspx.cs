using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollectionList : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        private B_Node bn = new B_Node();
        private B_CollectionInfo bc = new B_CollectionInfo();
        private B_CollectionItem bci = new B_CollectionItem();
        private B_Content cbll = new B_Content();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li class='active'><a href='" + Request.RawUrl + "'>采集历史记录</a></li>");
            if (!IsPostBack)
            {
                //bci.ExeColl();
                myBind();
            }
        }
        private void myBind()
        {
            DataTable dt = bc.Sel();
            DataView dv = new DataView(dt);
            dv.Sort = "AddTime desc";
            Egv.DataSource = dv;
            Egv.DataBind();
        }
        protected string GetMode(string str)
        {
            return bll.GetModelById(DataConverter.CLng(str)).ModelName;
        }
        protected string GetNode(string node)
        {
            return bn.GetNodeXML(DataConverter.CLng(node)).NodeName;
        }
        protected string GetItemName(string item)
        {
            return bci.GetSelect(DataConverter.CLng(item)).ItemName;
        }
        protected string GetTitle(string titleid)
        {
            B_ModelField dd = new B_ModelField();
            DataTable newinfo = cbll.SelByItemID(Convert.ToInt32(titleid));
            if (newinfo.Rows.Count > 0)
            {
                return newinfo.Rows[0]["Title"].ToString();
            }
            else
            {
                return "";
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            B_Content bcon = new B_Content();
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)lb.NamingContainer;
            M_CollectionInfo mc = bc.GetSelect(DataConverter.CLng(Egv.DataKeys[gvr.RowIndex].Value));
            DataTable dtitem = bcon.GetCommonByItem(mc.ModeID, mc.NodeID, mc.CollID);
            if (dtitem.Rows.Count > 0)
            {
                int gid = DataConverter.CLng(dtitem.Rows[0]["GeneralID"].ToString());
                bcon.DelContent(gid);
                bc.GetDelete(mc.C_IID);
            }
            myBind();
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            B_Content bcon = new B_Content();
            string str = "";
            //删除选定采集信息          
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    str += Egv.DataKeys[i].Value + ",";
                }
            }
            if (str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            SafeSC.CheckIDSEx(str);
            bc.DelByIDS(str);
            myBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            myBind();
        }
    }
}