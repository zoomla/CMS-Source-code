using System;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.IO;

namespace ZoomLaCMS
{
    public partial class NodeHot : FrontPage
    {
        B_Sensitivity sll = new B_Sensitivity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) { ErrToClient("[产生错误的可能原因：栏目ID不能为空!]"); }
            M_Node nodeMod = nodeBll.SelReturnModel(ItemID);
            if (nodeMod == null || nodeMod.IsNull) { ErrToClient("[产生错误的可能原因：栏目ID您访问的节点信息不存在!]"); }
            HtmlToClient(nodeMod.HotinfoTemplate);
        }
    }
}