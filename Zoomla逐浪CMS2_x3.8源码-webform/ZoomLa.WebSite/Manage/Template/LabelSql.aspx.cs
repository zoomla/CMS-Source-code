using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using ZoomLa.SQLDAL.SQL;
using Newtonsoft.Json.Linq;

//为简化逻辑,T1必定不能为空
//同时有效的数据源只能为一个
public partial class Manage_I_Content_LabelSql : CustomerPageAction
{
    B_Label bll = new B_Label();
    B_FunLabel funBll = new B_FunLabel();
    B_Model modBll = new B_Model();
    B_ModelField fieldBll = new B_ModelField();
    B_DataSource dsBll = new B_DataSource();
    public string D1 
    {
        get
        {
            if (DBList_DP.Items.Count < 1) { return ""; }
            return DBList_DP.SelectedValue;
        }
    }
    public string D2
    {
        get
        {
            if (DBList2_DP.Items.Count < 1) { return ""; }
            return DBList2_DP.SelectedValue;
        }
    }
    public string T1
    {
        get
        {
            if (TableList_DP.Items.Count < 1) { return ""; }
            return TableList_DP.SelectedValue;
        }
    }
    public string T2
    {
        get
        {
            if (TableList2_DP.Items.Count < 1) { return ""; }
            return TableList2_DP.SelectedValue;
        }
    }
    //-----------------------------
    public string LabelName { get { return (Request.QueryString["LabelName"] ?? ""); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.HideBread(Master);
            MyBind();
        }
    }
    private void MyBind()
    {
        LabelCate_DP.DataSource = bll.GetLabelCateListXML();
        LabelCate_DP.DataBind();
        //----绑定第二步数据表
        DataTable dsList = dsBll.Sel();
        DBList_DP.Items.Add(new ListItem("系统主数据库", "main"));
        DBList2_DP.Items.Add(new ListItem("系统主数据库", "main"));
        foreach (DataRow dr in dsList.Rows)
        {
            ListItem item = new ListItem("[外]" + dr["DSName"].ToString(), dr["ID"].ToString());
            DBList_DP.Items.Add(item);
            DBList2_DP.Items.Add(item);
        }
        DBList_DP.Items.Add(new ListItem("+添加新数据库", "new"));
        DataTable tableDT = DS_GetByDP(D1).Table_List();
        BindTableListToDP(tableDT, TableList_DP);
        BindTableListToDP(tableDT, TableList2_DP);
        //---最终步
        CustomLabel_DP.DataSource = bll.GetLabelCateListXML();
        CustomLabel_DP.DataBind();
        CustomLabel_DP.Items.Insert(0, new ListItem("选择标签类型", ""));
        lblSys.Text = funBll.GetSysLabel();//系统标签
        lblFun.Text = funBll.GetFunLabel();//扩展函数
        //-----------------绑定已有数据
        if (!string.IsNullOrEmpty(LabelName))
        {
            M_Label labelMod = bll.GetLabelXML(LabelName);
            labelMod.LabelWhere = ClearTableHolder(labelMod.LabelWhere);
            labelMod.LabelField = ClearTableHolder(labelMod.LabelField);
            labelMod.LabelTable = ClearTableHolder(labelMod.LabelTable);
            labelMod.LabelOrder = ClearTableHolder(labelMod.LabelOrder);
            //第一步
            LabelName_T.Text = labelMod.LableName;
            LabelCate_T.Text = labelMod.LabelCate;
            LabelType_Rad.SelectedValue = labelMod.LableType.ToString();
            Desc_T.Text = labelMod.Desc;
            //schema_Text.Text = labelMod.ConnectString;
            //第二步
            string t1 = "", t2 = "";
            if (!string.IsNullOrEmpty(labelMod.DataSourceType) && labelMod.DataSourceType.StartsWith("{"))
            {
                JObject jobj = JsonConvert.DeserializeObject<JObject>(labelMod.DataSourceType);
                DBList_DP.SelectedValue = jobj["ds_m"].ToString(); 
                DBList2_DP.SelectedValue = jobj["ds_m"].ToString();
                t1 = jobj["tb_m"].ToString();
                t2 = jobj["tb_s"].ToString();
                tableDT = DS_GetByDP(D1).Table_List();
                BindTableListToDP(tableDT, TableList_DP);
                BindTableListToDP(tableDT, TableList2_DP);
            }
            else { B_Label.GetT1AndT2(labelMod.LabelTable, ref t1, ref t2); }
            TableList_DP.SelectedValue = t1;
            TableList2_DP.SelectedValue = t2;
            TableList_DP_SelectedIndexChanged(null, null);
            TableList2_DP_SelectedIndexChanged(null, null);
            //是否启用了联接查询
            string joinType = B_Label.GetJoinType(labelMod.LabelTable);
            if (!string.IsNullOrEmpty(joinType))
            {
                SqlJoin_DP.SelectedValue = joinType;
                string on1 = "", on2 = "";
                B_Label.GetOnField(labelMod.LabelTable, ref on1, ref on2);
                OnField_DP.SelectedValue = on1;
                OnField2_DP.SelectedValue = on2;
            }
            SqlTable_T.Text = labelMod.LabelTable;
            SqlField_T.Text = labelMod.LabelField;
            //第三步
            RptParam_Bind(labelMod.Param);
            Param_Hid.Value = labelMod.Param;
            Where_T.Text = labelMod.LabelWhere;
            Order_T.Text = labelMod.LabelOrder;
            PSize_T.Text = labelMod.LabelCount;
            //第四步
            textContent.Text = labelMod.Content;
            Modeltypeinfo.Text = labelMod.Modeltypeinfo;
            Modelvalue.Text = labelMod.Modelvalue;
            setroot.SelectedValue = labelMod.setroot;
            Valueroot.Text = labelMod.Valueroot;
            BoolMode_Chk.Checked = labelMod.IsOpen == 1;
            falsecontent.Text = labelMod.FalseContent;
            bool_addroot_dp.SelectedValue = labelMod.addroot;
            Bread_L.Text = "当前标签：<a href='" + Request.RawUrl + "'>" + labelMod.LableName + "</a>";
        }
        if (string.IsNullOrEmpty(LabelName))
        {
            Bread_L.Text = "添加新标签";
            if (LabelCate_DP.Items.Count > 0) { LabelCate_T.Text = LabelCate_DP.SelectedValue; }
        }
    }
    //-------------------------------------------------------------------------------------------------------//
    #region 第二步
    protected void DBList_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        TableList_DP.Items.Clear();
        FieldList_DP.Items.Clear();
        DataTable dt = GetDBTabeList(DBList_DP.SelectedValue);
        BindTableListToDP(dt, TableList_DP);
    }
    protected void DBList2_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        TableList2_DP.Items.Clear();
        FieldList2_DP.Items.Clear();
        DataTable dt = GetDBTabeList(DBList_DP.SelectedValue);
        BindTableListToDP(dt, TableList2_DP);
    }
    protected void TableList_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tbname = TableList_DP.SelectedValue;
        DataTable dt = DS_GetByDP(D1).Field_List(tbname);
        dt.DefaultView.Sort = "Name";
        FieldList_DP.DataSource = dt;
        FieldList_DP.DataBind();
        OnField_DP.DataSource = dt;
        OnField_DP.DataBind();
        UpdateTableSql();
    }
    protected void TableList2_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tbname = TableList2_DP.SelectedValue;
        DataTable dt = DS_GetByDP(D2).Field_List(tbname); 
        dt.DefaultView.Sort = "Name";
        FieldList2_DP.DataSource = dt;
        FieldList2_DP.DataBind();
        OnField2_DP.DataSource = dt;
        OnField2_DP.DataBind();
        UpdateTableSql();
    }
    protected void SqlTable_Change(object sender, EventArgs e)
    {
        UpdateTableSql();
    }
    //需要查询的字段
    protected void SqlField_Btn_Click(object sender, EventArgs e)
    {
        //单表则不需要前缀,多表则加前缀
        string fields = "";
        if (!string.IsNullOrEmpty(T1))
        {
            string prefix = T1 + ".";
            if (string.IsNullOrEmpty(T2)) { prefix = ""; }
            foreach (ListItem item in FieldList_DP.Items)
            {
                if (item.Selected) { fields += prefix + item.Value + ","; }
            }
        }
        if (!string.IsNullOrEmpty(T2))
        {
            string prefix = T2 + ".";
            if (string.IsNullOrEmpty(T1)) { prefix = ""; }
            foreach (ListItem item in FieldList2_DP.Items)
            {
                if (item.Selected) { fields += prefix + item.Value + ","; }
            }
        }
        fields = fields.TrimEnd(',');
        if (string.IsNullOrEmpty(fields)) { fields = "*"; }
        SqlField_T.Text = fields;
    }
    //根据所选数据源,加载本地或外地数据源
    private DataTable GetDBTabeList(string selected)
    {
        DataTable tableDT = new DataTable();
        switch (selected)
        {
            case "new":
                Response.Redirect("ExternDS/DSAdd.aspx");
                break;
            default://读取外部数据源中的信息
                SqlBase db = DS_GetByDP(selected);
                tableDT = db.Table_List();
                break;
        }
        return tableDT;
    }
    private void UpdateTableSql()
    {
        string sql = "";
        if (string.IsNullOrEmpty(T1) || string.IsNullOrEmpty(T2))//单表
        {
            sql = string.IsNullOrEmpty(T1) ? T2 : T1;
        }
        else
        {
            Join_Div.Visible = true;
            string join = SqlJoin_DP.SelectedValue;
            sql = T1 + " " + join + " " + T2;
            sql += " ON " + T1 + "." + OnField_DP.SelectedValue + "=" + T2 + "." + OnField2_DP.SelectedValue;
        }
        SqlTable_T.Text = sql;
    }
    //为指定的下拉选单,绑定表列表
    private void BindTableListToDP(DataTable tableDT, DropDownList dp)
    {
        tableDT.DefaultView.Sort = "Name";
        dp.DataSource = tableDT;
        dp.DataBind();
        dp.Items.Insert(0, new ListItem("选择一个数据表", ""));
    }
    #endregion
    #region 第三步
    // 添加参数
    protected void BtnAddParam_Click(object sender, EventArgs e)
    {
        string pname = TxtParamName.Text;
        if (pname.ToUpper().Equals("ID")) 
        {
            Script("alert('" + TxtParamName.Text + " 为保留参数，不能添加此参数，请输入其他参数!');");
            return;
        }
        string Param = Param_Hid.Value;
        string strParam = TxtParamName.Text + "," + TxtParamValue.Text + "," + DDLParamType.SelectedValue + "," + TxtParamDesc.Text;
        if (!string.IsNullOrEmpty(HdnTempParam.Value))
        {
            Param = Param.Replace(HdnTempParam.Value, strParam);
        }
        else
        {
            if (!string.IsNullOrEmpty(Param))
            {
                Param = Param + "|" + strParam;
            }
            else
            {
                Param = strParam;
            }
        }
        Param_Hid.Value = Param;
        HdnTempParam.Value = "";
        TxtParamName.Text = "";
        TxtParamValue.Text = "";
        DDLParamType.SelectedValue = "1";
        TxtParamDesc.Text = "";
        BtnAddParam.Text = "添加";
        RptParam_Bind(Param);
    }
    // 写入参数
    private void RptParam_Bind(string Param)
    {
        attlist.Text = ""; attlist1.Text = "";
        DataTable paramTb = new DataTable("labelparam");
        paramTb.Columns.Add(new DataColumn("ParamName", typeof(string)));
        paramTb.Columns.Add(new DataColumn("ParamValue", typeof(string)));
        paramTb.Columns.Add(new DataColumn("ParamType", typeof(string)));
        paramTb.Columns.Add(new DataColumn("ParamDesc", typeof(string)));
        paramTb.Columns.Add(new DataColumn("Param", typeof(string)));
        if (!string.IsNullOrEmpty(Param))
        {
            string[] arrParam = Param.Split('|');
            for (int i = 0; i < arrParam.Length; i++)
            {
                DataRow dr = paramTb.NewRow();
                dr["ParamName"] = arrParam[i].Split(',')[0];
                dr["ParamValue"] = arrParam[i].Split(',')[1];
                dr["ParamType"] = arrParam[i].Split(',')[2];
                dr["ParamDesc"] = arrParam[i].Split(',')[3];
                dr["Param"] = arrParam[i];
                paramTb.Rows.Add(dr);
                attlist.Text = attlist.Text + "<div class='spanfixdiv1' draggable='true' ondragstart='pdrag(event);'  code=\"" + "@" + arrParam[i].Split(',')[0] + "\">" + "@" + arrParam[i].Split(',')[0] + "</div>";
                attlist1.Text = attlist1.Text + "<div class='spanfixdiv1' draggable='true' ondragstart='pdrag(event);' code=\"" + "@" + arrParam[i].Split(',')[0] + "\">" + "@" + arrParam[i].Split(',')[0] + "</div>";
            }
        }
        repParam.DataSource = paramTb;
        repParam.DataBind();
    }
    // repeater 操作
    protected void repParam_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            BtnAddParam.Text = "修改";
            HdnTempParam.Value = e.CommandArgument.ToString();
            TxtParamName.Text = HdnTempParam.Value.Split(',')[0];
            TxtParamValue.Text = HdnTempParam.Value.Split(',')[1];
            DDLParamType.SelectedValue = HdnTempParam.Value.Split(',')[2];
            TxtParamDesc.Text = HdnTempParam.Value.Split(',')[3];
        }
        if (e.CommandName == "Del")
        {
            string sParam = e.CommandArgument.ToString();
            string ParamValue = Param_Hid.Value;
            if (ParamValue == sParam)
            {
                ParamValue = "";
            }
            else
            {
                if (ParamValue.IndexOf(sParam) > 0)
                    ParamValue = ParamValue.Replace("|" + sParam, "");
                else
                    ParamValue = ParamValue.Replace(sParam + "|", "");
            }
            Param_Hid.Value = ParamValue;
            RptParam_Bind(ParamValue);
        }
    }
    protected void Where_Table_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = DS_FieldList(Where_Table_DP);
        dt.DefaultView.Sort = "Name";
        Where_Field_DP.DataSource = dt;
        Where_Field_DP.DataBind();
    }
    protected void Where_Btn_Click(object sender, EventArgs e)
    {
        string tbname = Where_GetTbName(Where_Table_DP);
        string sql = "";
        if (!string.IsNullOrEmpty(Where_T.Text)) { sql += " " + Where_Join_DP.SelectedValue; }
        sql += " " + tbname + "." + Where_Field_DP.SelectedValue;
        switch (Where_OPCode_DP.SelectedIndex)
        {
            case 0:
                sql += "=" + Where_Value_T.Text;
                break;
            case 1:
                sql += ">" + Where_Value_T.Text;
                break;
            case 2:
                sql += "<" + Where_Value_T.Text;
                break;
            case 3:
                sql += ">=" + Where_Value_T.Text;
                break;
            case 4:
                sql += "<=" + Where_Value_T.Text;
                break;
            case 5:
                sql += "<>" + Where_Value_T.Text;
                break;
            case 6:
                sql += " IN (" + Where_Value_T.Text + ")";
                break;
            case 7:
                sql += " LIKE '%" + Where_Value_T.Text + "%'";
                break;
            case 8:
                sql += " NOT IN (" + Where_Value_T.Text + ")";
                break;
        }
        Where_T.Text += sql;
    }
    protected void Order_Table_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = DS_FieldList(Order_Table_DP);
        dt.DefaultView.Sort = "Name";
        Order_Field_DP.DataSource = dt;
        Order_Field_DP.DataBind();
    }
    protected void Order_Btn_Click(object sender, EventArgs e)
    {
        string tbname = Where_GetTbName(Order_Table_DP);
        string sql = tbname + "." + Order_Field_DP.SelectedValue + " " + Order_DP.SelectedValue;
        Order_T.Text = sql;
    }
    //用于order与where下拉选择,返回表名
    private string Where_GetTbName(DropDownList dp)
    {
        string tbname;
        if (dp.SelectedValue.Equals("1"))
        {
            tbname = TableList_DP.SelectedValue;
        }
        else
        {
            tbname = TableList2_DP.SelectedValue;
        }
        return tbname;
    }
    //用于order与where,返回对应数据源-->指定表-->字段
    private DataTable DS_FieldList(DropDownList dp)
    {
        switch (dp.SelectedValue)
        {
            case "1":
                return DS_GetByDP(D1).Field_List(T1);
            case "2":
            default:
                return DS_GetByDP(D2).Field_List(T2);;
        }
    }
    // 参数类型
    public string GetParamType(string value)
    {
        string re = "";
        switch (value)
        {
            case "1":
                re = "普通参数";
                break;
            case "2":
                re = "页面参数";
                break;
            case "3":
                re = "单选参数";
                break;
            case "4":
                re = "多选参数";
                break;
        }
        return re;
    }
    #endregion
    #region Final
    private void SetField_div(string fields)
    {
        string tlp = "<div class=\"list-group-item spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{0}'>{1}</div>";
        string result = string.Format(tlp, "{Repeate}{/Repeate}", "循环函数");
        result += string.Format(tlp, "{ZL:jsq}", "计数器");
        DataTable aliasDT = GetAliasDT();//别名信息
        DataTable fieldDT = new DataTable();//需要展示的字段
        fieldDT.Columns.Add(new DataColumn("name", typeof(string)));
        fieldDT.Columns.Add(new DataColumn("alias", typeof(string)));
        //所有字段
        if (fields.Equals("*"))
        {
            #region 全部字段
            DataTable dt = new DataTable();
            string T1 = TableList_DP.SelectedValue;
            string T2 = TableList2_DP.SelectedValue;
            dt = DS_GetByDP(D1).Field_List(T1);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow fieldDR = fieldDT.NewRow();
                fieldDR["name"] = dr["Name"];
                fieldDR["alias"] = GetAliasFromDT(aliasDT, fieldDR["name"].ToString());
                fieldDT.Rows.Add(fieldDR);
            }
            if (!string.IsNullOrEmpty(T2))
            {
                dt = DS_GetByDP(D2).Field_List(T2);
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow fieldDR = fieldDT.NewRow();
                    fieldDR["name"] = dr["Name"];
                    fieldDR["alias"] = GetAliasFromDT(aliasDT, fieldDR["name"].ToString());
                    fieldDT.Rows.Add(fieldDR);
                }
            }
            #endregion
        }
        else
        {
            #region 选定字段
            string[] fieldArr = fields.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string field in fieldArr)
            {
                string name = "";
                if (field.Contains(".")) {  name = field.Split('.')[1]; }
                else { name = field; }
                DataRow fieldDR = fieldDT.NewRow();
                fieldDR["name"] = name;
                fieldDR["alias"] = GetAliasFromDT(aliasDT, fieldDR["name"].ToString());
                fieldDT.Rows.Add(fieldDR);
            }
            #endregion
        }
        //整合为div
        foreach (DataRow dr in fieldDT.Rows)
        {
            string fname = dr["name"].ToString();
            string alias = string.IsNullOrEmpty(dr["alias"].ToString()) ? "" : "<span style='color:green;margin-left:5px;'>(" + dr["alias"].ToString() + ")</span>";
            if (fname.Contains("."))
            {
                int index = fname.LastIndexOf(".") + 1;
                fname = fname.Substring(index, fname.Length - index);
            }
            result += string.Format(tlp, "{Field=\"" + fname + "\"/}", "{Field=\"" + fname + "\"/}" + alias);
        }
        Field_div.InnerHtml = result;
    }
    private DataTable GetAliasDT()
    {
        DataTable aliasDT = new DataTable();
        //判断表名是否为扩展表,如果是的话,将别名字段抽出
        //if (Regex.IsMatch(T1, "^ZL_[a-zA-Z]_[a-zA-z0-9]{1,}", RegexOptions.IgnoreCase))
        M_ModelInfo modelMod = new M_ModelInfo();
        modelMod = modBll.SelModelByTbName(T1);
        if (modelMod != null) { aliasDT = DBCenter.SelWithField("ZL_ModelField", "FieldName,FieldAlias", "ModelID=" + modelMod.ModelID); }
        modelMod = modBll.SelModelByTbName(T2);
        if (modelMod != null) { aliasDT.Merge(DBCenter.SelWithField("ZL_ModelField", "FieldName,FieldAlias", "ModelID=" + modelMod.ModelID)); }
        return aliasDT;
    }
    //根据字段名,返回对应别名,否则为空
    private string GetAliasFromDT(DataTable aliasDT, string name)
    {
        if (aliasDT.Rows.Count < 1) { return ""; }
        DataRow[] drs = aliasDT.Select("FieldName='" + name + "'");
        if (drs.Length > 0) { return drs[0]["FieldAlias"].ToString(); }
        else { return ""; }
    }
    protected void BoolMode_Chk_CheckedChanged(object sender, EventArgs e)
    {
        if (BoolMode_Chk.Checked == true)
        {
            boolMode_tr.Visible = true;
            boolMode_sp.Visible = true;
            bool_s1_tr.Visible = true;
            switch (Modeltypeinfo.SelectedValue)
            {
                case "计数判断":
                    bool_addroot_dp.Visible = true;
                    Valueroot.Visible = false;
                    setroot.Visible = true;
                    Modelvalue.Visible = true;
                    Label3.Visible = true;
                    if (bool_addroot_dp.SelectedValue == "循环计算")
                    {
                        Label3.Text = "计数器达到条件将自动清零,仅限包含<font color=blue>循环标签</font>有效";
                    }
                    else if (bool_addroot_dp.SelectedValue == "一直累加,仅限包含<font color=blue>循环标签</font>有效")
                    {
                        Label3.Text = "计数器一直累加";
                    }
                    break;
                case "用户登录判断":
                    bool_addroot_dp.Visible = false;
                    Valueroot.Visible = false;
                    setroot.Visible = false;
                    Modelvalue.Visible = false;
                    Label3.Text = "判断用户是否登录";
                    break;
                case "参数判断":
                    bool_addroot_dp.Visible = false;
                    Valueroot.Visible = true;
                    setroot.Visible = true;
                    Modelvalue.Visible = true;
                    Label3.Text = "判断参数是否满足条件";
                    break;
            }
        }
        else
        {
            boolMode_tr.Visible = false;
            boolMode_sp.Visible = false;
            bool_s1_tr.Visible = false;
        }
    }
    #endregion
    #region 步骤Wizard
    protected void Step_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dp = (DropDownList)sender;
        string index = dp.SelectedValue;
        Step_DP.SelectedValue = index;
        Step2_DP.SelectedValue = index;
        Step3_DP.SelectedValue = index;
        Step4_DP.SelectedValue = index;
        WizardStepBase step = Wizard1.WizardSteps[DataConvert.CLng(index)];
        Wizard1.MoveTo(step);
        DoByStepIndex();
    }
    protected void StartNextButton_Click(object sender, EventArgs e)
    {
        string index = DataConvert.CLng(Wizard1.ActiveStepIndex + 1).ToString();
        Step_DP.SelectedValue = index;
        Step2_DP.SelectedValue = index;
        Step3_DP.SelectedValue = index;
        Step4_DP.SelectedValue = index;
        DoByStepIndex();
    }
    // 上一步事件
    protected void PreviousButtonStep3_Click(object sender, EventArgs e)
    {
        string index = DataConvert.CLng(Wizard1.ActiveStepIndex - 1).ToString();
        Step_DP.SelectedValue = index;
        Step2_DP.SelectedValue = index;
        Step3_DP.SelectedValue = index;
        Step4_DP.SelectedValue = index;
        DoByStepIndex();
    }
    private void DoByStepIndex()
    {
        switch (DataConvert.CLng(Step_DP.SelectedValue))
        {
            case 2://排序
                {
                    //-------------
                    DataTable dt = new DataTable();
                    if (!string.IsNullOrEmpty(T1))
                    {
                        dt = DS_GetByDP(D1).Field_List(T1);
                        Where_Table_DP.SelectedIndex = 0;
                        Order_Table_DP.SelectedIndex = 0;
                    }
                    else if (!string.IsNullOrEmpty(T2))
                    {
                        dt = DS_GetByDP(D2).Field_List(T2);
                        Where_Table_DP.SelectedIndex = 1;
                        Order_Table_DP.SelectedIndex = 1;
                    }
                    Where_Field_DP.DataSource = dt;
                    Where_Field_DP.DataBind();
                    Order_Field_DP.DataSource = dt;
                    Order_Field_DP.DataBind();
                }
                break;
            case 3://Final
                SetField_div(SqlField_T.Text);
                Script("InitLabelDrag();InitEditor()");
                break;
        }
    }
    #endregion
    //标签名验证
    protected void S1C1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(LabelName))
        {
            string lblname = args.Value.Trim();

            if (string.IsNullOrEmpty(lblname) || bll.IsExistXML(lblname))
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        else
        {
            args.IsValid = true;
        }
    }
    // 保存标签
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LabelName_T.Text = LabelName_T.Text.Trim();
        if (string.IsNullOrEmpty(SqlTable_T.Text)) { Script("alert('查询表不能为空!');"); return; }
        M_Label labelMod = new M_Label();
        if (!string.IsNullOrEmpty(LabelName)) { labelMod = bll.GetLabelXML(LabelName); }
        if (!LabelName.ToLower().Equals(LabelName_T.Text.ToLower()))
        {
            bll.CheckLabelXML(LabelName_T.Text);
        }
        labelMod.LableName = LabelName_T.Text;
        labelMod.LableType = DataConverter.CLng(LabelType_Rad.SelectedValue);
        labelMod.LabelCate = LabelCate_T.Text;
        labelMod.Desc = Desc_T.Text;
        labelMod.LabelTable = SqlTable_T.Text;
        string sqlFieldStr = SqlField_T.Text;
        labelMod.LabelField = string.IsNullOrEmpty(sqlFieldStr) ? "*" : sqlFieldStr;
        labelMod.Param = Param_Hid.Value;
        labelMod.LabelWhere = Where_T.Text;
        labelMod.LabelOrder = Order_T.Text;
        labelMod.Content = textContent.Text;
        labelMod.LabelCount = PSize_T.Text;
        //不支持跨数据源联结查询
        JObject jobj = new JObject();
        jobj.Add("ds_m", D1); jobj.Add("ds_s", D2); jobj.Add("tb_m", T1); jobj.Add("tb_s", T2);
        labelMod.DataSourceType = JsonConvert.SerializeObject(jobj);
        //标签模型化
        //M_SubLabel subMod = new M_SubLabel();
        //subMod.T1 = DBList_DP.SelectedValue;
        //subMod.T2 = DBList2_DP.SelectedValue;
        //subMod.JoinType = SqlJoin_DP.SelectedValue;
        //subMod.OnField1 = OnField_DP.SelectedValue;
        //subMod.OnField2 = OnField2_DP.SelectedValue;
        //labelMod.ProceParam = JsonConvert.SerializeObject(subMod);
        //---------------------
        if (!string.IsNullOrEmpty(LabelName))
        {
            if (labelMod.LabelID > 0)
            {
                labelMod.Modeltypeinfo = Modeltypeinfo.Text;
                labelMod.Modelvalue = Modelvalue.Text;
                labelMod.setroot = setroot.SelectedValue;
                labelMod.Valueroot = Valueroot.Text;
                labelMod.FalseContent = falsecontent.Text;
                labelMod.addroot = bool_addroot_dp.SelectedValue;
                labelMod.IsOpen = BoolMode_Chk.Checked ? 1 : 0;
            }
            //外部库连接信息(改为修改ID),重要大小写,有labelinfo和LabelInfo
            //DropDownList dr = (DropDownList)WizardStep2.FindControl("DatabaseList");
            //labelMod.DataSourceType = dr.SelectedValue;//所属数据源ID,本地是0不处理
            //LabelInfo.ProceName = ProceName_Text.Text;
            //LabelInfo.ProceParam = ProceParam_Text.Text;
            //labelMod.ConnectString = schema_Text.Text;
            //如果修改了名称
            if (!labelMod.LableName.ToLower().Equals(LabelName.ToLower()))
            {
                bll.DelLabelXML(LabelName);
                bll.AddLabelXML(labelMod);
            }
            else
            {
                bll.UpdateLabelXML(labelMod);
            }
            function.WriteMsgTime("修改成功", "LabelManage.aspx");
        }
        else
        {
            labelMod.LabelNodeID = 0;
            labelMod.Modeltypeinfo = Modeltypeinfo.Text;
            labelMod.Modelvalue = Modelvalue.Text;
            labelMod.setroot = setroot.SelectedValue;
            labelMod.Valueroot = Valueroot.Text;
            labelMod.IsOpen = BoolMode_Chk.Checked ? 1 : 0;
            labelMod.FalseContent = falsecontent.Text;
            labelMod.addroot = bool_addroot_dp.SelectedValue;
            //LabelInfo.ProceName = ProceName_Text.Text;
            //LabelInfo.ProceParam = ProceParam_Text.Text;
            //labelMod.ConnectString = schema_Text.Text;
            bll.AddLabelXML(labelMod);
            function.WriteMsgTime("添加成功", "LabelManage.aspx");
        }
    }
    //引用标签
    protected void UseLable_Click(object sender, EventArgs e)
    {
        Response.Redirect("LabelCallTab.aspx?labelName=" + Server.UrlEncode(LabelName));
    }
    #region 数据源辅助方法 
    //根据下拉选单的选择,加载目标数据源
    private SqlBase DS_GetByDP(string selected)
    {
        return B_DataSource.GetDSByType(selected);
    }
    #endregion
    #region Tools
    ///// <summary>
    ///// 替换内容为占位符dbname-->{table1}
    ///// </summary>
    //private string TableToHolder(string text)
    //{
    //    if (string.IsNullOrEmpty(text)) { return ""; }
    //    text = text.Trim();
    //    text = text.Replace(T1 + ".dbo.", "{table1}.dbo.");
    //    text = text.Replace(T2 + ".dbo.", "{table2}.dbo.");
    //    return text;
    //}
    ///// <summary>
    ///// 将占位符替换为表名{table1}-->dbname
    ///// </summary>
    //private string HolderToTable(string text)
    //{
    //    if (string.IsNullOrEmpty(text)) { return ""; }
    //    text = text.Trim();
    //    text = text.Replace("{table1}.dbo.", T1 + ".dbo.");
    //    text = text.Replace("{table2}.dbo.", T2 + ".dbo.");
    //    return text;
    //}
    private string ClearTableHolder(string text)
    {
        text = text.Replace("{table1}.dbo.", "").Replace("{table2}.dbo.", "");
        return text;
    }
    private void Script(string script)
    {
        ScriptManager.RegisterStartupScript(LabelPanel, LabelPanel.GetType(), "", "<script>" + script + "</script>", false);
    }
    #endregion
}