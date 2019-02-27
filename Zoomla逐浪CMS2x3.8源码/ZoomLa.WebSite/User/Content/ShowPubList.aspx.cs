using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text;
using System.Xml;

public partial class User_Content_ShowPubList : System.Web.UI.Page
{
    private B_ModelField bfield = new B_ModelField();
    B_Content bc = new B_Content();
    B_User bu = new B_User();
    B_Model bmodel = new B_Model();
    B_ModelField bmf = new B_ModelField();
    B_Pub pll = new B_Pub();
    B_Node bnode = new B_Node();
    protected B_UserPromotions ups = new B_UserPromotions();
    private B_User buser = new B_User();
    //private bool Visible_V = true;
    public int ModelID
    {
        get { return DataConverter.CLng(ViewState["model"]); }
        set { ViewState["model"] = value; }
    }
    public int PageNum
    {
        get { return DataConverter.CLng(ViewState["pagenum"]); }
        set { ViewState["pagenum"] = value; }
    }
    public int PubID
    {
        get { return DataConverter.CLng(ViewState["pubid"]); }
        set { ViewState["pubid"] = value; }
    }
    public string PubTable
    {
        get { return ViewState["table"] + ""; }
        set { ViewState["table"] = value; }
    }
    public int NodeID
    {
        get { return DataConverter.CLng(ViewState["nodeid"]); }
        set { ViewState["nodeid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
        string GID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? "0" : Request.QueryString["ID"].ToString();
        M_CommonData mc = bc.GetCommonData(DataConverter.CLng(GID));
        ModelID = mc.ModelID;
        NodeID = mc.NodeID;

        GetNodePreate(NodeID);
        //if (mc.Inputer.Equals(bu.GetLogin().UserName))
        //{
        //    Visible_V = true;
        //}
        //else
        //{
        //    Visible_V = false;
        //}

        M_UserInfo infos = buser.GetLogin();
        M_UserPromotions upsinfo = ups.GetSelect(NodeID, infos.GroupID);

        if (upsinfo.pid != 0)
        {
            if (upsinfo.look != 1)
            {
                function.WriteErrMsg("您所在会员组无查看权限！");
            }
        }

        DataTable aas = txtShowGrid();
        DataTable UserData = bc.GetContentByItems(mc.TableName, mc.GeneralID);
        DetailsView1.DataSource = UserData.DefaultView;
        DetailsView1.DataBind();


        DataTable newDt = new DataTable();
        newDt.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
        newDt.Columns.Add(new DataColumn("PubName", System.Type.GetType("System.String")));
        newDt.Columns.Add(new DataColumn("GID", System.Type.GetType("System.Int32")));
        newDt.Columns.Add(new DataColumn("Num", System.Type.GetType("System.Int32")));
        newDt.Columns.Add(new DataColumn("PID", System.Type.GetType("System.Int32")));
        DataTable dt = pll.GetPubModelPublic();
        if (dt != null && dt.Rows.Count > 0)
        {
            int index=0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable pdt = pll.GetModelPubuPIdAll(mc.GeneralID, dt.Rows[i]["PubTableName"] + "");
                if (pdt != null && pdt.Rows.Count > 0)
                {
                    DataRow dr = newDt.NewRow();
                    dr[0] = index++;
                    dr[1] = dt.Rows[i]["PubName"] + "";
                    dr[2] = mc.GeneralID;
                    dr[3] = pdt.Rows.Count;
                    dr[4] = DataConverter.CLng(dt.Rows[i]["Pubid"]);
                    newDt.Rows.Add(dr);
                }
            }
        }
        //Egv.DataSource = newDt;
        //Egv.DataBind();

    }

    private DataTable txtShowGrid()
    {
        DataTable dt = this.bfield.GetModelFieldList(ModelID);
        DetailsView1.AutoGenerateRows = false;
        DataControlFieldCollection dcfc = DetailsView1.Fields;

        dcfc.Clear();
        BoundField bf2 = new BoundField();
        bf2.HeaderText = "ID";
        bf2.DataField = "GeneralID";
        bf2.HeaderStyle.Width = Unit.Percentage(15);
        bf2.HeaderStyle.CssClass = "tdbgleft";
        bf2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Left;

        dcfc.Add(bf2);

        BoundField bf5 = new BoundField();
        bf5.HeaderText = "标题";
        bf5.DataField = "Title";
        bf5.HeaderStyle.CssClass = "tdbgleft";
        bf5.HeaderStyle.Width = Unit.Percentage(15);
        bf5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        dcfc.Add(bf5);

        foreach (DataRow dr in dt.Rows)
        {
            BoundField bf = new BoundField();
            bf.HeaderText = dr["FieldAlias"].ToString();
            bf.DataField = dr["FieldName"].ToString();
            bf.HeaderStyle.Width = Unit.Percentage(15);
            bf.HeaderStyle.CssClass = "tdbgleft";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.HtmlEncode = false;
            dcfc.Add(bf);
        }
        return dt;
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
        M_UserInfo uinfos = bu.GetLogin();
        if (nodeinfo.Purview != null && nodeinfo.Purview != "")
        {
            DataRow auitdr = bnode.GetNodeAuitDT(nodeinfo.Purview).Rows[0];
            string input_v = auitdr["input"].ToString();
            if (input_v != null && input_v != "")
            {
                string tmparr = "," + input_v + ",";

                switch (uinfos.Status)
                {
                    case 0://已认证
                        if (tmparr.IndexOf("," + uinfos.GroupID.ToString() + ",") == -1)
                        {
                            if (tmparr.IndexOf(",-1,") == -1)
                            {
                                //function.WriteErrMsg("很抱歉！您没有权限在该栏目下发布信息！");//20110919
                            }
                        }
                        break;
                    default://未认证
                        if (tmparr.IndexOf(",-2,") == -1)
                        {
                            //function.WriteErrMsg("很抱歉！您没有权限在该栏目下发布信息！");//20110919
                        }
                        break;
                }
            }
            else
            {
                //为空
                //function.WriteErrMsg("很抱歉！您没有权限在该栏目下发布信息！");//20110919
            }
        }
    }
}
