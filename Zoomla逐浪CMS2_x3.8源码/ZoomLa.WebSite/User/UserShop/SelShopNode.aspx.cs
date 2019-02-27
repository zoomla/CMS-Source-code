using System;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class User_UserShop_SelProType : System.Web.UI.Page
{
    B_Node nodeBll = new B_Node();
    B_Model bmode = new B_Model();
    B_Content conBll = new B_Content();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBll.SelMyStore_Ex();
            class0.DataSource = nodeBll.GetNodeListUserShop(0);
            class0.DataBind();
        }
    }
    protected void class0_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nodeID = Convert.ToInt32(class0.SelectedValue);
        class1.DataSource = nodeBll.GetNodeListUserShop(nodeID);
        class1.DataBind();
        class2.Items.Clear();
        class3.Items.Clear();
        ChangeHref();
    }
    protected void class1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nodeID = Convert.ToInt32(class1.SelectedValue);
        class2.DataSource = nodeBll.GetNodeListUserShop(nodeID);
        class2.DataBind();
        class3.Items.Clear();
        ChangeHref();
    }
    protected void class2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nodeID = Convert.ToInt32(class2.SelectedValue);
        class3.DataSource = nodeBll.GetNodeListUserShop(nodeID);
        class3.DataBind();
        ChangeHref();
    }
    public void ChangeHref()
    {
        string modelID = "";
        int nodeID = 0;
        if (DataConverter.CLng(class3.SelectedValue) > 0)
        {
            nodeID = Convert.ToInt32(class3.SelectedValue);
            modelID = nodeBll.GetNodeXML(nodeID).ContentModel;
        }
        else if (DataConverter.CLng(class2.SelectedValue) > 0)
        {
            nodeID = Convert.ToInt32(class2.SelectedValue);
            modelID = nodeBll.GetNodeXML(nodeID).ContentModel;
        }
        else if (DataConverter.CLng(class1.SelectedValue) > 0)
        {
            nodeID = Convert.ToInt32(class1.SelectedValue);
            modelID = nodeBll.GetNodeXML(nodeID).ContentModel;
        }
        else
        {
            nodeID = Convert.ToInt32(class0.SelectedValue);
            modelID = nodeBll.GetNodeXML(nodeID).ContentModel;
        }
        M_ModelInfo minfo = bmode.GetModelById(Convert.ToInt32(modelID));
        Add_Href.InnerText = "添加" + minfo.ItemName;
        Add_Href.HRef = "AddProduct.aspx?menu=Add&ModelID=" + minfo.ModelID.ToString() + "&Nodeid=" + nodeID;
    }
}