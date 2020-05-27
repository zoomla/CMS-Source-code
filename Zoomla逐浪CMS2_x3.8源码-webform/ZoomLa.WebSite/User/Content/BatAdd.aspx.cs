using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class test_BatAdd : System.Web.UI.Page
{
    B_ModelField mfieldBll = new B_ModelField();
    B_Model modelBll = new B_Model();
    B_Content contentBll = new B_Content();
    B_Node nodeBll = new B_Node();
    B_User buser = new B_User();
    B_UserPromotions upBll = new B_UserPromotions();
    public int ModelID { get { return DataConvert.CLng(Request["ModelID"]); } }
    public int NodeID { get { return DataConvert.CLng(Request["NodeID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ModelID < 1 || NodeID < 1) { throw new Exception("节点与模型ID不能为空"); }
        M_UserInfo mu = buser.GetLogin();
        M_UserPromotions upsinfo = upBll.GetSelect(NodeID, mu.GroupID);
        if (upsinfo == null || upsinfo.addTo != 1) { throw new Exception("没有添加权限"); }
        //检验该用户是否有该节的添加权限
        if (!IsPostBack)
        {
            IList<string> content = new List<string>();
            DataTable dt = mfieldBll.SelByModelID(ModelID);
            dt.DefaultView.RowFilter = "IslotSize='1'";
            dt = dt.DefaultView.ToTable();
            Call commonCall = new Call();
            DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState, content);
            M_CommonData CData = new M_CommonData();
            CData.Title = Request["title_t"];
            CData.NodeID = NodeID;
            CData.ModelID = ModelID;
            CData.TableName = modelBll.GetModelById(ModelID).TableName;
            string parentTree = "";
            CData.FirstNodeID = nodeBll.SelFirstNodeID(NodeID, ref parentTree);
            CData.ParentTree = parentTree;
            CData.Inputer = mu.UserName;
            CData.Status = (int)ZLEnum.ConStatus.NotSure;
            contentBll.AddContent(table, CData);
            Response.Clear(); Response.Write("1"); Response.Flush(); Response.End();
        }
    }
}