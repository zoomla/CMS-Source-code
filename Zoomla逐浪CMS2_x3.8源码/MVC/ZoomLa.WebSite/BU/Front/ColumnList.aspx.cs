using System;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text.RegularExpressions;

namespace ZoomLaCMS
{
    public partial class ColumnList : FrontPage
    {
        //ItemID==NodeID
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) {function.WriteErrMsg("[产生错误的可能原因：没有指定栏目ID]"); }
            string url = Request.Url.ToString();
            string[] strarr = url.Split(new char[] { '/' });
            M_Node nodeinfo = nodeBll.GetNodeXML(ItemID);
            if (nodeinfo.IsNull) {function.WriteErrMsg("产生错误的可能原因：您访问的栏目不存在"); }
            string TemplateDir = "";
            if (string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                TemplateDir = nodeinfo.ListTemplateFile;
            else
                TemplateDir = nodeinfo.IndexTemplate;
            if (string.IsNullOrEmpty(TemplateDir))
            {
               ErrToClient("产生错误的可能原因：节点不存在或未绑定模型模板");
            }
            else
            {
                HtmlToClient(TemplateDir);
                //#region 节点自定义字段
                //Regex regexObj = new Regex(@"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}");
                //Match matchResults = regexObj.Match(Templatestr);
                //string Custom = nodeinfo.Custom;

                //if (Custom.IndexOf("{SplitCustom}") > -1)
                //{
                //    string[] CustArr = Custom.Split(new string[] { "{SplitCustom}" }, StringSplitOptions.RemoveEmptyEntries);

                //    while (matchResults.Success)
                //    {
                //        string NodeItemCount = Regex.Replace(matchResults.Value, @"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}", "$1");
                //        int NodeCoustom = DataConverter.CLng(NodeItemCount);
                //        string CoustomContent = CustArr[NodeCoustom - 1].ToString();
                //        Templatestr = Templatestr.Replace(matchResults.Value, CoustomContent);
                //        matchResults = matchResults.NextMatch();
                //    }
                //}
                //#endregion
            }
        }
    }
}