using System;
using System.IO;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Content_EditSinglePage : CustomerPageAction
{
    private B_Node bll = new B_Node();
    private B_Pub pll = new B_Pub();
    B_Model bllmodel = new B_Model();
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    public int ParentID { get { return DataConverter.CLng(Request.QueryString["ParentID"]); } }
    public string ViewMode { get { return Request.QueryString["view"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeEdit");
            this.DropDownList1.DataSource = pll.SelectNode(5);
            this.DropDownList1.DataTextField = "PubName";
            this.DropDownList1.DataValueField = "Pubid";
            this.DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("选择绑定", "0"));
            if (NodeID < 1)
            {
                if (ParentID == 0)
                { this.LblNodeName.Text = "根节点"; }
                else
                {
                    M_Node parentMod = this.bll.GetNodeXML(ParentID);
                    this.LblNodeName.Text = parentMod.NodeName;
                }
                function.Script(this, "BindPY();");
            }
            else
            {
                M_Node nodeMod = this.bll.GetNodeXML(NodeID);
                if (nodeMod.IsNull) { function.WriteErrMsg("指定要编辑的节点不存在"); }
                if (nodeMod.ParentID == 0) { this.LblNodeName.Text = "根节点"; }
                else
                {
                    M_Node parentMod = this.bll.GetNodeXML(nodeMod.ParentID);
                    this.LblNodeName.Text = parentMod.NodeName;
                }
                //this.HdnNodeID.Value = mNodeID;
                //this.HdnDepth.Value = node.Depth.ToString();
                //this.HdnParentId.Value = node.ParentID.ToString();
                //this.HdnOrderID.Value = node.OrderID.ToString();
                this.TxtNodeName.Text = nodeMod.NodeName;
                this.TxtNodeDir.Text = nodeMod.NodeDir;
                this.TxtNodePicUrl.Text = nodeMod.NodePic;
                this.TxtTips.Text = nodeMod.Tips;
                this.RBLPosition.SelectedValue = nodeMod.HtmlPosition.ToString();
                this.TxtMetaKeywords.Text = nodeMod.Meta_Keywords;
                this.TxtMetaDescription.Text = nodeMod.Meta_Description;
                this.RBLOpenType.SelectedValue = DataConverter.CLng(nodeMod.OpenNew).ToString();
                //this.TxtSinglepageTemplate_hid.Value = nodeMod.IndexTemplate;
                //-----模板
                TxtTemplate_hid.Value=nodeMod.ListTemplateFile;
                IndexTemplate_hid.Value=nodeMod.IndexTemplate;
                LastinfoTemplate_hid.Value=nodeMod.LastinfoTemplate;
                ProposeTemplate_hid.Value=nodeMod.ProposeTemplate;
                HotinfoTemplate_hid.Value=nodeMod.HotinfoTemplate;
                this.RBLListEx.SelectedValue = nodeMod.ListPageHtmlEx.ToString();
                LastinfoPageEx_Rad.SelectedValue = nodeMod.LastinfoPageEx.ToString();
                HotinfoPageEx.SelectedValue = nodeMod.HotinfoPageEx.ToString();
                ProposePageEx.SelectedValue = nodeMod.ProposePageEx.ToString();
                LastinfoPageEx_Rad.SelectedValue = nodeMod.LastinfoPageEx.ToString();

                this.SafeGuard.SelectedValue = nodeMod.SafeGuard.ToString();
                this.RBLItemOpenType.SelectedValue = nodeMod.ItemOpenType.ToString(); ;
                this.RBLOpenType.SelectedValue = nodeMod.OpenTypeTrue.ToString();
                this.RBLItemOpenType.SelectedValue = nodeMod.ItemOpenTypeTrue.ToString();
                M_Pub pubMod = pll.GetSelectNode(NodeID.ToString());
                if (pubMod.Pubid > 0)
                {
                    this.DropDownList1.SelectedValue = pubMod.Pubid.ToString();
                }

            }
            Call.SetBreadCrumb(Master, "<li>工作台</li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li>" + (ViewMode.Equals("design") ? "<a href='DesignNodeManage.aspx'>动力节点</a>" : "<a href='NodeManage.aspx'>节点管理</a>") + "</li><li class=\"active\">修改单页节点</li>");
        }
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Node nodeMod = new M_Node();
        if (NodeID > 0)
        {
            nodeMod = bll.SelReturnModel(NodeID);
        }
        else
        {
            nodeMod.ParentID = ParentID;
        }
        nodeMod.NodeName = this.TxtNodeName.Text;
        nodeMod.NodeType = 2;
        nodeMod.NodePic = this.TxtNodePicUrl.Text;
        nodeMod.NodeDir = this.TxtNodeDir.Text;
        nodeMod.NodeUrl = "";
        //nodeMod.ParentID = DataConverter.CLng(this.HdnParentId.Value);
        //nodeMod.Depth = DataConverter.CLng(this.HdnDepth.Value);
        //nodeMod.OrderID = DataConverter.CLng(this.HdnOrderID.Value);
        nodeMod.Child = 0;
        nodeMod.Tips = this.TxtTips.Text;
        nodeMod.Description = this.TxtDescription.Text;
        nodeMod.Meta_Keywords = this.TxtMetaKeywords.Text;
        nodeMod.Meta_Description = this.TxtMetaDescription.Text;
        nodeMod.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
        nodeMod.ItemOpenType = true;
        nodeMod.PurviewType = false;
        nodeMod.CommentType = "0";
        nodeMod.HitsOfHot = 0;
        //nodeMod.ListTemplateFile = this.TxtSinglepageTemplate_hid.Value;
        //nodeMod.IndexTemplate = this.TxtSinglepageTemplate_hid.Value;
        //----模板
        nodeMod.ListTemplateFile = TxtTemplate_hid.Value;
        nodeMod.IndexTemplate = IndexTemplate_hid.Value;// Request.Form[TxtIndexTemplate.UniqueID];
        nodeMod.LastinfoTemplate = LastinfoTemplate_hid.Value;
        nodeMod.ProposeTemplate = ProposeTemplate_hid.Value;
        nodeMod.HotinfoTemplate = HotinfoTemplate_hid.Value;
        nodeMod.ContentModel = "";
        nodeMod.ListPageHtmlEx = DataConverter.CLng(this.RBLListEx.SelectedValue);
        nodeMod.ListPageEx = DataConverter.CLng(ListPageEx_Rad.SelectedValue);
        nodeMod.LastinfoPageEx = DataConverter.CLng(LastinfoPageEx_Rad.SelectedValue);
        nodeMod.HotinfoPageEx = DataConverter.CLng(HotinfoPageEx.SelectedValue);
        nodeMod.ProposePageEx = DataConverter.CLng(ProposePageEx.SelectedValue);

        nodeMod.ContentFileEx = 0;
        nodeMod.ContentPageHtmlRule = 0;
        nodeMod.ConsumePoint = 0;
        nodeMod.ConsumeType = 0;
        nodeMod.ConsumeTime = 0;
        nodeMod.ConsumeCount = 0;
        nodeMod.Shares = 0;
        nodeMod.Custom = "";
        nodeMod.NodeListUrl = "";
        nodeMod.SafeGuard = DataConverter.CLng(this.SafeGuard.SelectedValue);
        nodeMod.ItemOpenType = DataConverter.CBool(this.RBLItemOpenType.SelectedValue);
        nodeMod.OpenTypeTrue = this.RBLOpenType.SelectedValue;
        nodeMod.ItemOpenTypeTrue = this.RBLItemOpenType.SelectedValue;
        nodeMod.HtmlPosition = DataConverter.CLng(RBLPosition.SelectedValue);
        nodeMod.SiteConfige = "";
        nodeMod.AuditNodeList = "";
        if (NodeID > 0) { bll.UpdateNode(nodeMod); }
        else { nodeMod.NodeID = bll.AddNode(nodeMod); CreateFile(nodeMod); }
        if (ViewMode.Equals("design")) { function.WriteSuccessMsg("操作成功", CustomerPageAction.customPath2 + "Content/DesignNodeManage.aspx", 3000); }
        else { function.WriteSuccessMsg("操作成功", CustomerPageAction.customPath2 + "Content/NodeManage.aspx", 3000); }
    }
    public void CreateFile(M_Node nodeMod) 
    {
        string UName = this.TxtNodeDir.Text.Trim();
        string Ulook = "";
        if (!string.IsNullOrEmpty(Ulook))
        {
            if (RadioButtonList1.SelectedValue == "0")
            {
                if (Ulook.IndexOf("http://") == -1)
                {
                    Ulook = "http://" + Ulook;
                }
            }
            if (RadioButtonList1.SelectedValue == "1")
            {
                if (Ulook.IndexOf("~/") == -1)
                {
                    Ulook = "~/" + Ulook;
                }
            }
            string USend = "~/ColumnList.aspx?NodeID=" + nodeMod.NodeID;
            #region 文件生成
            USend = Ulook;
            string AbsPath = base.Request.PhysicalApplicationPath;
            if (USend.IndexOf("http://") > -1)
            {
                if (USend.IndexOf("/", 8) > -1 && USend.Length > USend.IndexOf("/", 8) + 1)
                {
                    string USend2 = USend.Substring(USend.IndexOf("/", 8));
                    if (USend2.IndexOf(".") > -1)
                    {
                        USend2 = USend2.Substring(USend2.IndexOf("."));
                    }
                    else
                    {
                        #region 文件生成
                        try
                        {
                            DirectoryInfo info = new DirectoryInfo(AbsPath + USend2 + @"\Default.aspx");
                            if (info.Exists)
                            {
                                // Response.Write("<script>alert('文件以');</script>");
                            }
                            else
                            {
                                FileSystemObject.Create(AbsPath + USend2 + @"\Default.aspx", FsoMethod.File);
                                FileSystemObject.Create(AbsPath + USend2 + @"\Index.aspx", FsoMethod.File);
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            function.WriteErrMsg("发生错误");
                        }
                        catch (UnauthorizedAccessException)
                        {
                            function.WriteErrMsg("发生错误");
                        }
                        #endregion
                    }

                }
            }
            else
            {
                string USend3 = USend.Substring(2);

                if (USend.IndexOf(".") > -1)
                {
                    //  USend = USend.Substring(USend.IndexOf("."));
                    //  Response.Write("<script>alert('" + USend3 + "');</script>");
                }
                else
                {
                    #region 文件生成
                    try
                    {
                        DirectoryInfo info = new DirectoryInfo(AbsPath + USend3);
                        if (info.Exists)
                        {
                            //   Response.Write("<script>alert('aa');</script>");
                        }
                        else
                        {
                            FileSystemObject.Create(AbsPath + USend3 + @"\Default.aspx", FsoMethod.File);
                            FileSystemObject.Create(AbsPath + USend3 + @"\Index.aspx", FsoMethod.File);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        function.WriteErrMsg("发生错误");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        function.WriteErrMsg("发生错误");
                    }
                    #endregion
                }
            }
            #endregion
        }
        if (DropDownList1.SelectedValue != "0")
        {
            M_Pub pm = pll.GetSelect(DataConverter.CLng(DropDownList1.SelectedValue));
            pm.PubNodeID = nodeMod.NodeID.ToString();
            pll.GetUpdate(pm);
        }
    }

    protected void TxtNodeName_TextChanged(object sender, EventArgs e)
    {
        //DataTable NodeName;
        //NodeName = this.bll.CheckNodeName(this.TxtNodeName.Text, DataConverter.CLng(this.HdnDepth.Value), DataConverter.CLng(this.HdnParentId.Value), DataConverter.CLng(this.HdnNodeID.Value));
        //if (NodeName.Rows.Count > 0)
        //{
        //    this.TxtNodeName.Style.Add("color", "red");
        //    EBtnSubmit.Enabled = false;
        //    EBtnSubmit.CssClass = "C_input";
        //    function.WriteErrMsg("同栏目下发现栏目名重复！建议手动修改栏目名");
        //}
        //else
        //{
        EBtnSubmit.Enabled = true;
        this.TxtNodeName.Style.Add("color", "");
        //}
    }
    
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Template/TemplateEdit.aspx?filepath=/" + Server.UrlEncode(this.IndexTemplate_hid.Value) + "");
    }
    protected void TxtNodeDir_TextChanged(object sender, EventArgs e)
    {
        //DataTable NodeDir = this.bll.GetNodeForDirname(this.TxtNodeDir.Text, DataConverter.CLng(this.HdnNodeID.Value));
        //if (NodeDir.Rows.Count > 0)
        //{
        //    this.TxtNodeDir.Style.Add("color", "red");
        //    EBtnSubmit.Enabled = false;
        //    EBtnSubmit.CssClass = "C_input";
        //    function.WriteErrMsg("发现标识名重复，建议定义不同栏目名，请点击确定重新添加节点。");
        //}
        //else
        //{
        EBtnSubmit.Enabled = true;
        this.TxtNodeDir.Style.Add("color", "");
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TxtNodeDir.Enabled == true)
            TxtNodeDir.Enabled = false;
        else
            TxtNodeDir.Enabled = true;
    }
    //模型,是否选中


    protected void Cencel_B_Click(object sender, EventArgs e)
    {
        if (ViewMode.Equals("design")) { Response.Redirect("DesignNodeManage.aspx"); }
        else { Response.Redirect("NodeManage.aspx"); }
    }
}