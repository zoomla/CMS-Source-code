using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Common;
using ZoomLa.Model.CreateJS;

public partial class Manage_Content_ReleaseByNode : System.Web.UI.Page
{
    /*
     * 依据传参对节点下内容与栏目进行生成
     */ 
    string NodeID { get { return Request.QueryString["NodeID"]; } }
    B_Release relBll = new B_Release();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NodeID)) { function.WriteErrMsg("未选定需要生成的节点"); }
        M_Release relMod = new M_Release();
        relMod.NodeIDS = NodeID;
        relMod.MyRType = M_Release.RType.ALL;//生成内容
        relBll.Insert(relMod);
        relMod.MyRType = M_Release.RType.NodeIDS;//生成栏目
        relBll.Insert(relMod); 
        Response.Redirect("CreateHtml.aspx");
    }
}