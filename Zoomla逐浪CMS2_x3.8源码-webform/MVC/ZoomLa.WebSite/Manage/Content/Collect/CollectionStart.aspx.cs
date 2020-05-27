using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using ZoomLa.BLL.Collect;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollectionStart : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        private B_Node bn = new B_Node();
        private B_CollectionItem bc = new B_CollectionItem();
        //private Thread thread;
        B_Coll_Worker worker = new B_Coll_Worker();
        B_CollectionItem cll = new B_CollectionItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li class='active'><a href='" + Request.RawUrl + "'>开始采集</a></li>");
            }
        }
        public void MyBind(string key = "")
        {
            Egv.DataSource = bc.SelBySwitch(2);
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected string GetMode(string str)
        {
            return bll.GetModelById(DataConverter.CLng(str)).ModelName;
        }
        protected string GetNode(string nodelist)
        {
            string[] nodearr = nodelist.Split(new char[] { ',' });
            string str = "";
            foreach (string node in nodearr)
            {
                if (!string.IsNullOrEmpty(node))
                {
                    str += GetParent(DataConverter.CLng(node)) + "<br />";
                }
            }
            if (str.EndsWith("<br />"))
            {
                str = str.Remove(str.LastIndexOf("<br />"));
            }
            return str;
        }
        private string GetParent(int ParentID)
        {
            string str = "";
            M_Node mn = bn.GetNodeXML(ParentID);
            if (mn.ParentID > 0)
            {
                str = GetParent(mn.ParentID) + "&gt;&gt;" + mn.NodeName;
            }
            else
            {
                str = mn.NodeName;
            }
            return str;
        }
        //开始采集
        protected void btnCollAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] ids = Request.Form["idchk"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    M_CollectionItem mc = bc.GetSelect(Convert.ToInt32(id));
                    mc.Switch = 1;
                    mc.LastTime = DateTime.Now.ToString();
                    bc.GetUpdate(mc);
                    B_Coll_Worker.AddWork(mc);
                }
                B_Coll_Worker.Start();
                Response.Redirect("CollectionStatus.aspx");
            }
        }
    }
}