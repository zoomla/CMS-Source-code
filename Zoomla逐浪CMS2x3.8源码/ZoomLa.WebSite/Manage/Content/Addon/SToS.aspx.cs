using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class test_SToS : CustomerPageAction
{
    B_Node nodeBll = new B_Node();
    B_ModelField fieldBll = new B_ModelField();
    B_Model modBll = new B_Model();
    B_Admin badmin = new B_Admin();
    private DataTable SNodeDT { get { return (DataTable)ViewState["SNodeDT"]; } set { ViewState["SNodeDT"] = value; } }
    private DataTable SModelDT { get { return (DataTable)ViewState["SModelDT"]; } set { ViewState["SModelDT"] = value; } }
    private DataTable TModelDT { get { return (DataTable)ViewState["TModelDT"]; } set { ViewState["TModelDT"] = value; } }
    private string conn { get { return conn_hid.Value; } set { conn_hid.Value = value; } }
    public string NodeTlp = "<li {6}>{5}<label><input type='radio' name='Tid_Rad' value='{0}' /> {4}<span>{1}</span></label> "//本站节点模板
        +"<span>(文章数：<span class='rd_red'>{2}</span>)</span> <span>(所属模型：{3})</span>"
        +"</li>";
    public string SnodeTlp = "<li {6}>{5}<label><input type='checkbox' name='Sid_Chk' value='{0}' /> {4}<span>{1}</span></label> "//外部节点模板
        +"<span>(文章数：<span class='rd_red'>{2}</span>)</span> <span>(所属模型：{3})</span>"
        +"</li>";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li class='active dropdown'>站点拷贝工具</li>");
            if (!badmin.CheckSPwd(Session["Spwd"] as string))
            {
                SPwd.Visible = true;
                return;
            }
            else
            {
                maindiv.Visible = true;
            }
            MyBind();
        }
    }
    public void MyBind() 
    {
        string constr = SqlHelper.ConnectionString;
        TIP_T.Text = StrHelper.GetAttrByStr(constr, "Data Source");
        TDBName_T.Text = StrHelper.GetAttrByStr(constr, "Initial Catalog");
        TUName_T.Text = StrHelper.GetAttrByStr(constr, "User ID");
        //TPwd_T.Text = GetconAttr(constr, "Password");
    }
    public void BindData()
    {
        conn = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", SIP_T.Text, SDBName_T.Text, SUName_T.Text, SPwd_T.Text);
        string fields = "NodeID,NodeName,ParentID,Depth,ContentModel,";
        fields += "(SELECT Count(*) FROM ZL_CommonModel WHERE NodeID=A.NodeID) AS ArticleCount";
        string sql = "SELECT " + fields + " FROM ZL_Node A ORDER BY OrderID ASC";
        string modelsql = "SELECT ModelID,ModelName,TableName,ModelType FROM ZL_Model";
        SModelDT = SqlHelper.ExecuteTable(conn, CommandType.Text, modelsql);
        SNodeDT = SqlHelper.ExecuteTable(conn, CommandType.Text, sql, null);
        TModelDT = SqlHelper.ExecuteTable(CommandType.Text, modelsql);
        Nodes_Li.Text = GetNodes(SqlHelper.ExecuteTable(CommandType.Text, sql),0,0,NodeTlp);
        SNodes_Li.Text = GetNodes(SqlHelper.ExecuteTable(conn, CommandType.Text, sql, null), 0, 0, SnodeTlp);
    }
    public string GetNodes(DataTable dt,int depth, int pid,string tlp)
    {
        string result = "", pre = "<img src='/Images/TreeLineImages/t.gif' border='0'>", span = "<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20'>";
        DataRow[] dr = dt.Select("ParentID='" + pid + "'");
        depth++;
        for (int i = 1; i < depth; i++)
        {
            pre = span + pre;
        }
        for (int i = 0; i < dr.Length; i++)
        {
            if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["NodeID"]) + "'").Length > 0)
            {
                result += string.Format(tlp, dr[i]["NodeID"], dr[i]["NodeName"], dr[i]["ArticleCount"], GetModelStr(dr[i]["ContentModel"].ToString()), "<span class='icon fa fa-folder'></span>", pre, "class='parent_node'");
                result += "<ul class='tvNav tvNav_ul list-unstyled' style='display:none;'>" + GetNodes(dt, depth, Convert.ToInt32(dr[i]["NodeID"]),tlp) + "</ul>";
            }
            else
            {
                result += string.Format(tlp, dr[i]["NodeID"], dr[i]["NodeName"], dr[i]["ArticleCount"], GetModelStr(dr[i]["ContentModel"].ToString()), "", pre, "");
            }
        }
        return result;
    }
    protected void Next_Btn_Click(object sender, EventArgs e)
    {
        if (StrHelper.StrNullCheck(SIP_T.Text, SDBName_T.Text, SUName_T.Text, SPwd_T.Text)) { function.Script(this, "alert('不能为空值');"); MyBind(); return; }
        step1.Visible = false;
        step2.Visible = true;
        BindData();
    }
    protected void Pre_Btn_Click(object sender, EventArgs e)
    {
        step1.Visible = true;
        step2.Visible = false;
        MyBind();
    }
    //开始检测,准备相关的数据好拷贝
    protected void Check_Btn_Click(object sender, EventArgs e)
    {
        string snids = Request.Form["Sid_Chk"];
        int tnid = DataConvert.CLng(Request.Form["Tid_Rad"]);
        if (string.IsNullOrEmpty(snids) || tnid < 1) { function.WriteErrMsg("来源与目标节点不能为空"); }
        M_Node nodeMod = nodeBll.SelReturnModel(tnid);
        SNode_L.Text = GetNodeStr(snids);
        TNode_L.Text = nodeMod.NodeName;
        //---显示有哪些模型需要对应拷贝
        SModelDT.DefaultView.RowFilter = "";
        SNodeDT.DefaultView.RowFilter = "";
        ModelRPT.DataSource = GetModelDTByNodes(snids,SNodeDT,SModelDT);
        ModelRPT.DataBind();
        //-----------------------------
        snids_hid.Value = snids;
        tnid_hid.Value = tnid.ToString();
        step2.Visible = false;
        step3.Visible = true;
    }
    //确定开始拷贝
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        DBCopy(snids_hid.Value, Convert.ToInt32(tnid_hid.Value), modelstr_hid.Value.TrimEnd(','));
        function.WriteSuccessMsg("节点数据迁移完成", "SToS.aspx");
    }
    //根据模型ID，获取模型名称
    public string GetModelStr(string modelids, int type = 1)
    {
        string result = "";
        modelids = modelids.Trim(',');
        if (string.IsNullOrEmpty(modelids)) return result;
        DataTable dt = SModelDT;
        if (type != 1) { dt = TModelDT; }
        dt.DefaultView.RowFilter = "ModelID IN(" + modelids + ")";
        foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
        {
            result += dr["ModelName"] + ",";
        }
        return result.TrimEnd(',');
    }
    public string GetNodeStr(string nodeids)
    {
        string result = "";
        nodeids = nodeids.Trim(',');
        if (string.IsNullOrEmpty(nodeids)) return result;
        SNodeDT.DefaultView.RowFilter = "NodeID IN(" + nodeids + ")";
        foreach (DataRow dr in SNodeDT.DefaultView.ToTable().Rows)
        {
            result += dr["NodeName"] + ",";
        }
        return result.TrimEnd(',');
    }
    //根据节点IDS,获取到模型表
    public DataTable GetModelDTByNodes(string nids, DataTable nodeDT, DataTable modelDT)
    {
        string modelids = "";
        nodeDT.DefaultView.RowFilter = "NodeID IN(" + nids + ")";
        foreach (DataRow dr in nodeDT.DefaultView.ToTable().Rows)
        {
            modelids += dr["ContentModel"] + ",";
        }
        modelids = modelids.Replace(",,", ",").Trim(',');
        nodeDT.DefaultView.RowFilter = "";
        modelDT.DefaultView.RowFilter = "ModelID IN(" + modelids + ")";
        return modelDT;
    }
    protected void ModelRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList dp = e.Item.FindControl("Model_DP") as DropDownList;
            dp.DataSource = TModelDT;
            dp.DataBind();
            dp.Items.Insert(0, new ListItem("请选择模型", ""));
        }
    }
    /*----------------------Core---------------------------*/
    /// <summary>
    /// 节点数据对拷,注意必须保持主表,Commonmodel中的字段一致
    /// </summary>
    /// <param name="snids">来源NodeIDS</param>
    /// <param name="tnid">目标NodeID</param>
    public void DBCopy(string snids,int tnid,string modelstr) 
    {
        string[] modelarr = modelstr.Split(',');
        //1:1,2:2,模型的对应关系字符串
        SyncModel(modelstr);
        //模型已匹配，开始准备数据
        DataTable condt = SqlHelper.ExecuteTable(conn, CommandType.Text, "SELECT TableName AS tbname, * FROM ZL_CommonModel WHERE NODEID IN(" + snids + ")");
        for (int i = 0; i < modelarr.Length; i++)
        {
            int smid = Convert.ToInt32(modelarr[i].Split(':')[0]);
            int tmid = Convert.ToInt32(modelarr[i].Split(':')[1]);
            string tbname = TModelDT.Select("ModelID='" + tmid + "'")[0]["TableName"].ToString();
            SetModelInfo(condt, smid, tmid, tbname);
        }
        for (int i = 0; i < condt.Rows.Count; i++)
        {
            DataRow dr = condt.Rows[i];
            dr["NodeID"] = tnid;
            dr["ItemID"] = AddAddition(Convert.ToInt32(dr["ItemID"]),dr["tbname"].ToString(),dr["TableName"].ToString());
        }
      
        //拷贝
        CopyToCommonModel(condt);
        function.WriteSuccessMsg("拷贝完成,共拷贝了" + condt.Rows.Count + "篇内容","",0);
    }
    private void SetModelInfo(DataTable condt, int smid, int tmid, string tbname)
    {
        DataRow[] drs = condt.Select("ModelID='"+smid+"'");
        for (int i = 0; i < drs.Length; i++)
        {
            drs[i]["ModelID"] = tmid;
            drs[i]["TableName"] = tbname;
        }
    }
    /// <summary>
    /// 同步模型
    /// </summary>
    /// <param name="models">a:b,c:d</param>
    public void SyncModel(string models)
    {
        string[] arr = models.Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            int smid = Convert.ToInt32(arr[i].Split(':')[0]);
            int tmid = Convert.ToInt32(arr[i].Split(':')[1]);
            M_ModelInfo model = modBll.GetModelById(tmid);
            //模型对比,看要加哪些字段进去
            string fieldsql = "SELECT * FROM ZL_ModelField Where Sys_type=0 AND ModelID={0} ";
            DataTable smfieldDT = SqlHelper.ExecuteTable(conn, CommandType.Text, string.Format(fieldsql, smid));
            DataTable tmfieldDT = SqlHelper.ExecuteTable(CommandType.Text, string.Format(fieldsql, tmid));
            foreach (DataRow dr in smfieldDT.Rows)
            {
                //后期加入类型判断，现仅判断是否有该字段
                if (tmfieldDT.Select("FieldName='" + dr["FieldName"]+"'").Length < 1)
                {
                    AddField(dr, model);
                }
            }
        }
    }
    //添加一个字段
    private void AddField(DataRow dr, M_ModelInfo model)
    {
        M_ModelField fieldMod = new M_ModelField().GetModelFromReader(dr);
        fieldMod.ModelID = model.ModelID;
        fieldMod.FieldID = 0;
        fieldBll.AddModelField(model, fieldMod);
        //fieldBll.Add(fieldMod);
        //fieldBll.AddFieldCopy(model.TableName, fieldMod.FieldName, fieldMod.FieldType, "", "");//加入表
    }
    /// <summary>
    /// 主表数据拷贝,仅负责拷贝上层必须将nodeid,itemid,nodename,modelid等弄准确
    /// </summary>
    /// <param name="dt">需拷入的数据表</param>
    /// <param name="nodeid">目标节点ID</param>
    private void CopyToCommonModel(DataTable dt)
    {
        // //获取回所有的GeneralID
        using (SqlBulkCopy bulk = new SqlBulkCopy(SqlHelper.ConnectionString))
        {
            bulk.BatchSize = 1000;
            bulk.DestinationTableName = "ZL_CommonModel";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colname=dt.Columns[i].ColumnName;
                if (colname.Equals("tbname") || colname.ToLower().Equals("articleid")) continue;
                bulk.ColumnMappings.Add(colname, colname);
            }
            //bulk.SqlRowsCopied += new SqlRowsCopiedEventHandler(bulkCopy_SqlRowsCopied);
            bulk.WriteToServer(dt);
        }
    }
    /// <summary>
    /// ID,来源表名,目标表名
    /// </summary>
    /// <param name="itemid"></param>
    /// <param name="stbname"></param>
    /// <param name="ttbname"></param>
    /// <returns></returns>
    private int AddAddition(int itemid,string stbname,string ttbname) 
    {
        //后期改为将附加表中的值都存入内存的hashmap中,增加效率
        string sql = "SELECT * FROM " + stbname + " WHERE ID=" + itemid;
        DataTable dt = SqlHelper.ExecuteTable(conn, CommandType.Text, sql);
        if (dt.Rows.Count > 0)
        {
            string insertsql = "Insert Into " + ttbname + "({0})VALUES({1});";
            string fields = "",values="";
            SqlParameter[] sp=new SqlParameter[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colname=dt.Columns[i].ColumnName;
                sp[i] = new SqlParameter(colname, dt.Rows[0][colname]);
                if (colname.ToLower().Equals("id") || colname.ToLower().Equals("articleid")) continue;
                fields += colname+",";
                values += "@" + colname + ",";
            }
            fields = fields.TrimEnd(','); values = values.TrimEnd(',');
            return Sql.insertID(ttbname, sp, values, fields);
        }
        else { return 0; }
    }
    protected void PreTo2_Btn_Click(object sender, EventArgs e)
    {
        step3.Visible = false;
        step2.Visible = true;
    }
}