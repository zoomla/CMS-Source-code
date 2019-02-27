namespace ZoomLa.WebSite
{
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
    using ZoomLa.Sns;
    using System.Xml;
    using System.Web.Caching;
    using System.Text.RegularExpressions;

    public partial class ColumnList : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Node bnode = new B_Node();
        B_CreateHtml bll;
        string sotdir = SiteConfig.SiteOption.TemplateDir;
        //NodeID
        public int ItemID
        {
            get
            {
                return DataConverter.CLng(B_Route.GetParam("ID",Page));
            }
        }
        public int Cpage { get { int page = DataConverter.CLng(B_Route.GetParam("CPage", Page)); page = page < 1 ? 1 : page; return page; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1)
                function.WriteErrMsg("[产生错误的可能原因：没有指定栏目ID]");
            string url = Request.Url.ToString();
            string[] strarr = url.Split(new char[] { '/' });
            bll = new B_CreateHtml();
            M_Node nodeinfo = bnode.GetNodeXML(ItemID);

            if (nodeinfo.IsNull)
                function.WriteErrMsg("产生错误的可能原因：您访问的栏目不存在");
            string TemplateDir = "";
            if (string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                TemplateDir = nodeinfo.ListTemplateFile;
            else
                TemplateDir = nodeinfo.IndexTemplate;

            if (string.IsNullOrEmpty(TemplateDir))
            {
                function.WriteErrMsg("产生错误的可能原因：节点不存在或未绑定模型模板");
            }
            else
            {
                TemplateDir = "/" + TemplateDir;
                GetNodePreate(nodeinfo.NodeID);
                TemplateDir = sotdir + TemplateDir;
                TemplateDir = Server.MapPath(TemplateDir);
                TemplateDir = TemplateDir.Replace("/", @"\").Replace(@"\\", @"\");
                string Templatestr = FileSystemObject.ReadFile(TemplateDir);
                #region 节点自定义字段
                Regex regexObj = new Regex(@"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}");
                Match matchResults = regexObj.Match(Templatestr);
                string Custom = nodeinfo.Custom;

                if (Custom.IndexOf("{SplitCustom}") > -1)
                {
                    string[] CustArr = Custom.Split(new string[] { "{SplitCustom}" }, StringSplitOptions.RemoveEmptyEntries);

                    while (matchResults.Success)
                    {
                        string NodeItemCount = Regex.Replace(matchResults.Value, @"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}", "$1");
                        int NodeCoustom = DataConverter.CLng(NodeItemCount);
                        string CoustomContent = CustArr[NodeCoustom - 1].ToString();
                        Templatestr = Templatestr.Replace(matchResults.Value, CoustomContent);
                        matchResults = matchResults.NextMatch();
                    }
                }
                #endregion
                string ContentHtml = this.bll.CreateHtml(Templatestr, Cpage, ItemID, "1");//html
                if (SiteConfig.SiteOption.IsSensitivity == 1) { ContentHtml = B_Sensitivity.Process(ContentHtml); }
                Response.Write(ContentHtml);
            }
        }

        private void GetNodePreate(int prentid)
        {
            M_Node nodes = bnode.GetNodeXML(prentid);
            GetMethod(nodes);
            if (nodes.ParentID > 0)
            {
                GetNodePreate(nodes.ParentID);
            }
        }

        private void GetMethod(M_Node nodeinfo)
        {
            if (nodeinfo.Purview != null && nodeinfo.Purview != "")
            {
                string Purview = nodeinfo.Purview;
                if (Purview != null && Purview != "")
                {
                    DataTable auitdt = bnode.GetNodeAuitDT(Purview);
                    DataRow auitdr = auitdt.Rows[0];

                    string View_v = auitdr["View"].ToString();
                    string ViewGroup_v = auitdr["ViewGroup"].ToString();
                    string ViewSunGroup_v = auitdr["ViewSunGroup"].ToString();
                    string input_v = auitdr["input"].ToString();
                    string forum_v = auitdr["forum"].ToString();
                    M_UserInfo uinfos = buser.GetLogin();
                    switch (View_v)
                    {
                        case "allUser"://开放栏目

                            break;
                        case "moreUser"://半开放栏
                            break;
                        case "onlyUser"://认证栏目
                            if (!buser.CheckLogin())
                            {
                                function.WriteErrMsg("该栏目为<b>认证栏目</b>！您必须<a href=\"/User/Login.aspx\" style='color:red' target=\"_blank\">登录</a>才能浏览！");
                            }
                            if (ViewGroup_v != null && ViewGroup_v != "")
                            {
                                string tmparr = "," + ViewGroup_v + ",";
                                switch (uinfos.Status)
                                {
                                    case 0://已认证
                                        if (tmparr.IndexOf("," + uinfos.GroupID.ToString() + ",") == -1)
                                        {
                                            if (tmparr.IndexOf(",-1,") == -1)
                                            {
                                                function.WriteErrMsg("很抱歉！您没有权限浏览该栏目！");
                                            }
                                        }
                                        break;
                                    default://未认证
                                        if (tmparr.IndexOf(",-2,") == -1)
                                        {
                                            function.WriteErrMsg("很抱歉！您没有权限浏览该栏目！");
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                //为空
                                function.WriteErrMsg("很抱歉！您没有权限查看该栏目！");
                            }

                            break;
                    }
                }
            }
        }
    }
}