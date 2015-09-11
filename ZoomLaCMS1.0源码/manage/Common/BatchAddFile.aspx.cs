using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
namespace ZoomLa.WebSite.Manage.Common
{
    public partial class BatchAddFile : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.badmin.CheckMulitLogin();
            string NodeId = base.Request.QueryString["NodeID"];
            //this.ViewState["FieldName"] = base.Request.QueryString["FieldName"];
            M_Node nodeinfo = bnode.GetNode(DataConverter.CLng(NodeId));
            string str = nodeinfo.NodeDir + "/{$Year}{$Month}/{$Year}{$Month}{$Day}_#.jpg";
            str = str.Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("{$Day}", DateTime.Now.Day.ToString());
            this.TxtFileName.Text = str;            
        }
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int num = DataConverter.CLng(this.TxtStartNum.Text);
        //    int num1 = DataConverter.CLng(this.TxtEndNum.Text);
        //    string FileName = this.TxtFileName.Text;
            //string AddName = FileAdd.Split(new char[] { '|' })[0];
        //    string file1 = "";
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
        //    for (int i = num; i <= num1; i++)
        //    {
        //        if(string.IsNullOrEmpty(file1))
        //            file1 = FileName.Replace("#", i.ToString());
        //        else
        //            file1 = file1 + "|" + FileName.Replace("#", i.ToString());
        //    }
        //    builder.Append("ReturnUrl(\"" + file1 + "\");");
        //    builder.Append("</script>");
        //    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        //}
    }
}