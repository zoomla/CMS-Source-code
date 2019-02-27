using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model.Exam;
namespace ZoomLaCMS.Manage.Exam.News
{
    public partial class News : System.Web.UI.Page
    {
        M_Content_Publish pubMod = new M_Content_Publish();
        B_Content_Publish pubBll = new B_Content_Publish();
        B_Publish_Node nodebll = new B_Publish_Node();
        public int Pid { get { return Convert.ToInt32(Request.QueryString["Pid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='News.aspx' style='margin-right:10px;'>数字出版</a>[<a onclick='ClearHid();' id='add_href' data-toggle='modal' data-target='#add_div'>添加报纸</a>][<a id='add_href2' data-toggle='modal' data-target='#add_node'>添加分类</a>]</li>");
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = null;
            dt = nodebll.Sel();
            foreach (DataRow item in dt.Rows)
            {
                ListItem li = new ListItem(item["NodeName"].ToString(), item["ID"].ToString());
                ParentIDS_D.Items.Add(li);
                NodeList_D.Items.Add(li);
            }
        }

        protected void Add_Btn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(CurID_Hid.Value))
            {
                pubMod.NewsName = NewsName_T.Text;
                pubMod.Pid = 0;
                pubMod.Nid = Convert.ToInt32(NodeList_D.SelectedValue);
                pubBll.Insert(pubMod);
            }
            else
            {
                pubMod = pubBll.SelReturnModel(Convert.ToInt32(CurID_Hid.Value));
                pubMod.NewsName = NewsName_T.Text;
                pubBll.UpdateByID(pubMod);
            }
            MyBind();
        }
        protected void AddNode_Btn_Click(object sender, EventArgs e)
        {
            M_Publish_Node nodemodel = new M_Publish_Node();
            nodemodel.CDate = DateTime.Now;
            nodemodel.NodeName = NodeName_T.Text;
            nodemodel.Pid = Convert.ToInt32(ParentIDS_D.SelectedValue);
            nodebll.Insert(nodemodel);
            Response.Redirect(Request.RawUrl);
        }
    }
}