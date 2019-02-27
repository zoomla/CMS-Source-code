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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Content_UnionNode : CustomerPageAction
{
    private B_Node bll = new B_Node();
    private B_Model bllmodel = new B_Model();

    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='" + CustomerPageAction.customPath2 + "Content/NodeManage.aspx'>节点管理</a></li><li class=\"active\">节点合并迁移</li>" + Call.GetHelp(17));
        if (!this.Page.IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            B_ARoleAuth.Check(ZLEnum.Auth.model, "NodeEdit");
            
            this.MainNode.DataSource = this.bll.CreateForListBox(); 
            this.MainNode.DataTextField = "NodeName";
            this.MainNode.DataValueField = "NodeID";
            this.MainNode.DataBind();
            // this.MainNode.Items.Add(new ListItem("根节点", "0"));
            this.MainNode.Items.Insert(0, new ListItem("根节点", "0"));
            this.LstNodes.DataSource = this.bll.CreateForListBox();
            this.LstNodes.DataTextField = "NodeName";
            this.LstNodes.DataValueField = "NodeID";
            this.LstNodes.DataBind();
            this.LstNodes.Items.Insert(0, new ListItem("根节点", "0"));
        }
    }
    protected void EBtnBacthSet_Click(object sender, EventArgs e)
    {
        int mainN = DataConverter.CLng(this.MainNode.SelectedValue);//目标节点
        M_Node node = this.bll.GetNodeXML(mainN);
        string nm = node.ContentModel;

        for (int i = 0; i < this.LstNodes.Items.Count; i++)//源节点[遍历]
        {
            if (this.LstNodes.Items[i].Selected)
            {
                int nodeid = DataConverter.CLng(this.LstNodes.Items[i].Value);
                M_Node node2 = this.bll.GetNodeXML(nodeid);
                string nm2 = node2.ContentModel;
                if (nm2 != null && nm2 != "")
                {
                    if (nm2.IndexOf(',') > -1)//多个模型
                    {
                        string[] modearr = nm2.Split(',');
                        for (int ii = 0; ii < modearr.Length; ii++)
                        {
                            string t1 = "," + nm + ",";
                            if (t1.IndexOf("," + modearr[ii] + ",") == -1)
                            {
                                if (nm != "")
                                {
                                    nm = nm + "," + modearr[ii];
                                }
                                else
                                {
                                    nm = modearr[ii];
                                }
                            }
                        }
                    }
                    else//单个模型
                    {
                        string t1 = "," + nm + ",";
                        if (t1.IndexOf("," + nm2 + ",") == -1)
                        {
                            if (nm != "")
                            {
                                nm = nm + "," + nm2;
                            }
                            else
                            {
                                nm = nm2;
                            }
                        }
                    }
                }

                node.ContentModel = nm;
                bll.UpdateNode(node);
                    
                bll.UnionUp(mainN, nodeid);
                bll.UnionUp2(mainN, nodeid);
                if (CheckBox1.Checked == true)
                {
                    this.bll.DelNode(nodeid);
                }
                bll.UpdateNodeXMLAll();
            }
        }
        function.WriteSuccessMsg("合并成功!");
    }
    //迁移节点
    protected void Button1_Click(object sender, EventArgs e)
    {
        int mainN = DataConverter.CLng(this.MainNode.SelectedValue);
        M_Node node = new M_Node();
        if (mainN != 0)
        {
            node = this.bll.GetNodeXML(mainN);
        }

        for (int i = 0; i < this.LstNodes.Items.Count; i++)
        {
            if (this.LstNodes.Items[i].Selected)
            {
                int nodeid = DataConverter.CLng(this.LstNodes.Items[i].Value);
                if (mainN != nodeid)
                {

                    M_Node node2 = this.bll.GetNodeXML(nodeid);
                    if (mainN != 0)
                    {
                        if (bll.checkChild(nodeid, mainN))
                        {
                            int node1p = node.ParentID;
                            int node1d = node.Depth;
                            int node1c = node.Child;
                            int node2p = node2.ParentID;
                            int node2d = node2.Depth;
                            int node2c = node2.Child;
                            if (node.ParentID == nodeid)
                            {
                                node2.ParentID = mainN;
                            }
                            else
                            {
                                node2.ParentID = node1p;
                            }
                            node.ParentID = node2p;
                            node.Depth = node2d;
                            node.Child = node2c;
                            node2.Depth = node1d;
                            node2.Child = node1c;
                            bll.UpDNode(mainN, nodeid);
                            bll.UpdateNode(node);
                            bll.UpdateNode(node2);

                        }
                        else
                        {
                            bll.SetChildDel(node2.ParentID);
                            node2.Depth = node.Depth + 1;
                            node2.ParentID = mainN;
                            bll.UpdateNode(node2);
                        }
                    }
                    else
                    {
                        bll.SetChildDel(node2.ParentID);
                        node2.Depth = 1;
                        node2.ParentID = 0;
                        bll.UpdateNode(node2);
                    }
                }
            }
        }
        function.WriteSuccessMsg("迁移成功!"); 
    }//迁移节点 End;
    
}