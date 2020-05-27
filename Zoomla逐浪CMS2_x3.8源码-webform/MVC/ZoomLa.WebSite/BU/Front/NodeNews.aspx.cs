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
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text.RegularExpressions;
using System.Xml;

namespace ZoomLaCMS
{
    public partial class NodeNews :FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) { ErrToClient("[未指定栏目ID]"); }
            M_Node nodeinfo = nodeBll.GetNodeXML(ItemID);
            if (nodeinfo.IsNull) { ErrToClient("[产生错误的可能原因：您访问的节点信息不存在!]"); }
            HtmlToClient(nodeinfo.LastinfoTemplate);
        }
    }
}