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
using ZoomLa.Components;
using System.Collections.Generic;
using ZoomLa.SQLDAL;
using System.IO;
using System.Drawing;


public partial class User_Content_ShowContent : System.Web.UI.Page
{
    B_ModelField bfield = new B_ModelField();
    B_Content bc = new B_Content();
    B_Node nodeBll = new B_Node();
    B_Role RLL = new B_Role();
    B_Model bm = new B_Model();
    M_Node nodeMod = new M_Node();
    B_User buser = new B_User();
    public int Gid { get { return Convert.ToInt32(Request.QueryString["GID"]); } }
    public int ModelID { get { return DataConvert.CLng(ModelID_Hid.Value); } set { ModelID_Hid.Value = value.ToString(); } }
    public int NodeID { get { return DataConvert.CLng(ViewState["NodeID"]); } set { ViewState["NodeID"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Gid < 1) function.WriteErrMsg("参数出错");
            M_CommonData mc = bc.SelReturnModel(Gid);
            if (mc == null || mc.IsNull) { function.WriteErrMsg("[" + Gid + "]号文章不存在"); }
            M_ModelInfo modelMod = bm.GetModelById(mc.ModelID);
            M_Node nodeMod = nodeBll.SelReturnModel(mc.NodeID);
            ModelID = mc.ModelID;
            NodeID = mc.NodeID;
            if (modelMod.IsNull) { function.WriteErrMsg("该内容所绑定的模型[" + mc.ModelID + "]不存在"); }
            if (nodeMod.IsNull) { function.WriteErrMsg("该内容所绑定的节点[" + mc.NodeID + "]不存在"); }
            //--------------
           
            NodeName_L.Text = nodeMod.NodeName;
            Gid_L.Text = Gid.ToString();
            Title_L.Text = mc.Title;
            Inputer_L.Text = mc.Inputer;
            Hits_L.Text = mc.Hits.ToString();
            CreateTime_L.Text = mc.CreateTime.ToString();
            UpdateTime_L.Text = mc.UpDateTime.ToString();
            ConStatus_L.Text = ZLEnum.GetConStatus(mc.Status);
            Elite_L.Text = mc.EliteLevel == 1 ? "已推荐" : "未推荐";
            DataTable contentDT = bc.GetContentByItems(mc.TableName, mc.GeneralID);
            Base_L.Text = bfield.InputallHtml(ModelID, NodeID, new ModelConfig()
            {
                ValueDT = contentDT,
                Mode = ModelConfig.SMode.PreView
            });
           
        }
    }
    public void MyBind()
    {

    }
    // 取消审核
    protected void UnAudit_Btn_Click(object sender, EventArgs e)
    {
        M_CommonData commdata = bc.GetCommonData(Gid);
        commdata.Status = 0;
        bc.Update(commdata);
        function.WriteSuccessMsg("操作成功!", "ShowContent.aspx?GID=" + Gid);
    }
    public bool GetRole(string auth) { return true; }
    protected void Button6_Click(object sender, EventArgs e)
    {
        M_CommonData commdata = bc.GetCommonData(Gid);
        commdata.EliteLevel = 0;
        bc.Update(commdata);
        function.WriteSuccessMsg("操作成功!", "ShowContent.aspx?GID=" + Gid);
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
    public string GetOpenView()
    {
        string outstr = " <a href='/Item/" + Gid + ".aspx' target='_blank'><span class='fa fa-eye'></span></a>";
        return outstr;
    }
}