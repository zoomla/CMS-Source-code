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
namespace ZoomLa.WebSite.Manage.Template
{
    public partial class labelSql : System.Web.UI.Page
    {
        private B_Label bll = new B_Label();
        private B_FunLabel bfun = new B_FunLabel();
        private int m_LabelID;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("LabelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(this.Request.QueryString["LabelID"]))
                {
                    this.m_LabelID = 0;
                }
                else
                {
                    this.m_LabelID = DataConverter.CLng(this.Request.QueryString["LabelID"]);
                }
                this.DDLCate.DataSource = this.bll.GetCateList();
                this.DDLCate.DataTextField = "LabelCate";
                this.DDLCate.DataValueField = "LabelCate";
                this.DDLCate.DataBind();
                this.DDLCate.Items.Insert(0, new ListItem("选择标签类型", ""));

                this.DropLabelType.DataSource = this.bll.GetCateList();
                this.DropLabelType.DataTextField = "LabelCate";
                this.DropLabelType.DataValueField = "LabelCate";
                this.DropLabelType.DataBind();               
                
                this.DropLabelType.Items.Insert(0, new ListItem("选择标签分类", ""));

                this.DbTableDownList.DataSource = this.bll.GetTableName();
                this.DbTableDownList.DataTextField = "TABLE_NAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                ListItem item = new ListItem();
                item.Text = "选择一个数据表";
                this.DbTableDownList.Items.Insert(0, item);

                this.DbTableDownList2.DataSource = this.bll.GetTableName();
                this.DbTableDownList2.DataTextField = "TABLE_NAME";
                this.DbTableDownList2.DataValueField = "TABLE_NAME";
                this.DbTableDownList2.DataBind();
                this.DbTableDownList2.Items.Insert(0, item);

                
                this.LblSysLabel.Text = this.bfun.GetSysLabel();                
                this.LblFunLabel.Text = this.bfun.GetFunLabel();

                if (this.m_LabelID > 0)
                {
                    M_Label labelInfo = this.bll.GetLabel(this.m_LabelID);
                    this.HdnlabelID.Value = this.m_LabelID.ToString();
                    this.TxtLabelName.Text = labelInfo.LableName;
                    this.TxtLabelType.Text = labelInfo.LabelCate;
                    //this.DropLabelType.SelectedValue = labelInfo.LabelCate;
                    this.TxtLabelIntro.Text = labelInfo.Desc;
                    this.HdnParam.Value = labelInfo.Param;
                    RptParam_Bind(labelInfo.Param);
                    this.TxtSqlTable.Text = labelInfo.LabelTable;
                    this.TxtSqlField.Text = labelInfo.LabelField;
                    this.TxtSqlWhere.Text = labelInfo.LabelWhere;
                    this.TxtOrder.Text = labelInfo.LabelOrder;
                    this.textContent.Text = labelInfo.Content;
                    this.TextBox1.Text = labelInfo.LabelCount;
                    this.RBLType.SelectedValue = labelInfo.LableType.ToString();
                    
                    SetLabelColumn(labelInfo.LabelField);
                }
                else
                {                    
                    this.HdnlabelID.Value = "0";
                }
            }
            this.TxtTjValue.Attributes.Add("onmouseup", "dragend2(this)");
            this.TxtTjValue.Attributes.Add("onClick", "savePos(this)");
            this.TxtTjValue.Attributes.Add("onmousemove", "DragPos(this)");
            this.textContent.Attributes.Add("onmouseup", "dragend(this)");
            this.textContent.Attributes.Add("onClick", "savePos(this)");
            this.textContent.Attributes.Add("onmousemove", "DragPos(this)");
            this.TextBox1.Attributes.Add("onmouseup", "dragend3(this)");
            this.TextBox1.Attributes.Add("onClick", "savePos(this)");
            this.TextBox1.Attributes.Add("onmousemove", "DragPos(this)");
        }
        
        protected void DBTableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DbTableDownList.SelectedIndex != 0)
            {
                this.DbFieldDownList.DataSource = this.bll.GetTableField(this.DbTableDownList.SelectedValue);
                this.DbFieldDownList.DataTextField = "fieldname";
                this.DbFieldDownList.DataValueField = "fieldname";
                this.DbFieldDownList.DataBind();
                this.TxtSqlTable.Text = this.DbTableDownList.SelectedValue;
                BindTj();
                BindOrder();
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    this.DbFieldList.DataSource = this.bll.GetTableField(this.DbTableDownList.SelectedValue);
                    this.DbFieldList.DataTextField = "fieldname";
                    this.DbFieldList.DataValueField = "fieldname";
                    this.DbFieldList.DataBind();

                    this.DbFieldList2.DataSource = this.bll.GetTableField(this.DbTableDownList2.SelectedValue);
                    this.DbFieldList2.DataTextField = "fieldname";
                    this.DbFieldList2.DataValueField = "fieldname";
                    this.DbFieldList2.DataBind();
                    this.tj.Visible = true;
                    TableJoin1();
                }
                else
                {
                    this.tj.Visible = false;
                }
            }
            else
            {
                this.DbFieldDownList.Items.Clear();
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    this.TxtSqlTable.Text = this.DbTableDownList2.SelectedValue;
                }
                else
                {
                    this.TxtSqlTable.Text = "";
                }
                this.tj.Visible = false;
            }
        }

        protected void ChangeCate(object sender, EventArgs e)
        {
            string LabelCate = this.DDLCate.SelectedValue;
            DataTable dt = this.bll.GetLabelListByCate(LabelCate);
            string lblLabels = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (DataConverter.CLng(dr["LabelType"]) == 1)
                {
                    lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"1\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                }
                else
                {
                    if (string.IsNullOrEmpty(dr["LabelParam"].ToString()))
                    {
                        lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"1\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                    }
                    else
                    {
                        string Param = dr["LabelParam"].ToString();

                        if (Param.IndexOf("|") < 0)
                        {
                            if (Param.Split(new char[] { ',' })[2] == "2")
                            {
                                lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"1\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"2\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                            }
                        }
                        else
                        {
                            lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"2\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                        }
                    }
                }
            }
            this.LblLabel.Text = lblLabels;
        }

        protected void DBTableDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DbTableDownList2.SelectedIndex != 0)
            {
                this.DbFieldDownList2.DataSource = this.bll.GetTableField(this.DbTableDownList2.SelectedValue);
                this.DbFieldDownList2.DataTextField = "fieldname";
                this.DbFieldDownList2.DataValueField = "fieldname";
                this.DbFieldDownList2.DataBind();
                BindTj();
                BindOrder();
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    this.DbFieldList.DataSource = this.bll.GetTableField(this.DbTableDownList.SelectedValue);
                    this.DbFieldList.DataTextField = "fieldname";
                    this.DbFieldList.DataValueField = "fieldname";
                    this.DbFieldList.DataBind();

                    this.DbFieldList2.DataSource = this.bll.GetTableField(this.DbTableDownList2.SelectedValue);
                    this.DbFieldList2.DataTextField = "fieldname";
                    this.DbFieldList2.DataValueField = "fieldname";
                    this.DbFieldList2.DataBind();
                    this.tj.Visible = true;
                    TableJoin1();
                }
                else
                {
                    this.tj.Visible = false;
                    this.TxtSqlTable.Text = this.DbTableDownList2.SelectedValue;
                }
            }
            else
            {
                this.DbFieldDownList2.Items.Clear();
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    this.TxtSqlTable.Text = this.DbTableDownList.SelectedValue;
                }
                else
                {
                    this.TxtSqlTable.Text = "";
                }
                this.tj.Visible = false;
            }
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAddParam_Click(object sender, EventArgs e)
        {
            string Param = this.HdnParam.Value;
            string strParam = this.TxtParamName.Text + "," + this.TxtParamValue.Text + "," + this.DDLParamType.SelectedValue + "," + this.TxtParamDesc.Text;
            if (!string.IsNullOrEmpty(this.HdnTempParam.Value))
            {
                Param = Param.Replace(this.HdnTempParam.Value, strParam);
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
            this.HdnParam.Value = Param;
            this.HdnTempParam.Value = "";

            this.TxtParamName.Text = "参数名称";
            this.TxtParamValue.Text = "默认值";
            this.DDLParamType.SelectedValue = "1";
            this.TxtParamDesc.Text = "参数说明";
            RptParam_Bind(Param);
        }
        private void RptParam_Bind(string Param)
        {

            DataTable paramTb = new DataTable("labelparam");
            DataColumn myDataColumn;
            DataRow myDataRow;
            this.attlist.Text = "";
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ParamName";
            paramTb.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ParamValue";
            paramTb.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ParamType";
            paramTb.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ParamDesc";
            paramTb.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Param";
            paramTb.Columns.Add(myDataColumn);
            if (!string.IsNullOrEmpty(Param))
            {
                string[] arrParam = Param.Split('|');
                for (int i = 0; i < arrParam.Length; i++)
                {
                    myDataRow = paramTb.NewRow();
                    myDataRow["ParamName"] = arrParam[i].Split(',')[0];
                    myDataRow["ParamValue"] = arrParam[i].Split(',')[1];
                    myDataRow["ParamType"] = arrParam[i].Split(',')[2];
                    myDataRow["ParamDesc"] = arrParam[i].Split(',')[3];
                    myDataRow["Param"] = arrParam[i];
                    paramTb.Rows.Add(myDataRow);
                    this.attlist.Text = this.attlist.Text + "<div class=\"spanfixdiv1\" outtype=\"0\" onclick=\"cit()\" code=\"" + "@" + arrParam[i].Split(',')[0] + "\">" + "@" + arrParam[i].Split(',')[0] + "</div>";
                }
            }
            this.repParam.DataSource = paramTb;
            this.repParam.DataBind();

        }
        /// <summary>
        /// repeater 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repParam_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.HdnTempParam.Value = e.CommandArgument.ToString();
                this.TxtParamName.Text = this.HdnTempParam.Value.Split(',')[0];
                this.TxtParamValue.Text = this.HdnTempParam.Value.Split(',')[1];
                this.DDLParamType.SelectedValue = this.HdnTempParam.Value.Split(',')[2];
                this.TxtParamDesc.Text = this.HdnTempParam.Value.Split(',')[3];
            }
            if (e.CommandName == "Del")
            {
                string sParam = e.CommandArgument.ToString();
                string ParamValue = this.HdnParam.Value;
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
                this.HdnParam.Value = ParamValue;
                RptParam_Bind(ParamValue);
            }
        }
        /// <summary>
        /// 保存标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Label LabelInfo = new M_Label();
                LabelInfo.LabelID = DataConverter.CLng(this.HdnlabelID.Value);
                LabelInfo.LableName = this.TxtLabelName.Text;
                LabelInfo.LableType = DataConverter.CLng(this.RBLType.SelectedValue);
                LabelInfo.LabelCate = this.TxtLabelType.Text;
                LabelInfo.Desc = this.TxtLabelIntro.Text;
                LabelInfo.Param = this.HdnParam.Value;
                LabelInfo.LabelTable = this.TxtSqlTable.Text;
                LabelInfo.LabelField = this.TxtSqlField.Text;
                LabelInfo.LabelWhere = this.TxtSqlWhere.Text;
                LabelInfo.LabelOrder = this.TxtOrder.Text;

                LabelInfo.Content = this.textContent.Text;
                if (string.IsNullOrEmpty(this.TextBox1.Text))
                {
                    LabelInfo.LabelCount = "0";
                }
                else
                {
                    LabelInfo.LabelCount = this.TextBox1.Text;
                }
                if (LabelInfo.LabelID > 0)
                    this.bll.UpdateLabel(LabelInfo);
                else
                    this.bll.AddLabel(LabelInfo);
                Response.Redirect("LabelManage.aspx");
            }
        }
        /// <summary>
        /// 主从表连接
        /// </summary>
        protected void TableJoin(object sender, EventArgs e)
        {
            TableJoin1();
        }
        private void TableJoin1()
        {
            string Table1 = this.DbTableDownList.SelectedValue;
            string Table2 = this.DbTableDownList2.SelectedValue;
            string Field1 = this.DbFieldList.SelectedValue;
            string Field2 = this.DbFieldList2.SelectedValue;
            string joinStr = Table1 + "." + Field1 + "=" + Table2 + "." + Field2;
            string TableStr = this.DbTableDownList.SelectedValue + " " + this.Dbtj.SelectedValue + " " + this.DbTableDownList2.SelectedValue;
            TableStr = TableStr + " on " + joinStr;
            this.TxtSqlTable.Text = TableStr;
        }
        /// <summary>
        /// 查询条件绑定字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BindTableField(object sender, EventArgs e)
        {
            this.BindTj();
        }
        private void BindTj()
        {
            string TableName = "";
            if (this.DDLFTable.SelectedIndex == 0)
            {
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    TableName = this.DbTableDownList.SelectedValue;
                }
            }
            else
            {
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    TableName = this.DbTableDownList2.SelectedValue;
                }
            }
            if (!string.IsNullOrEmpty(TableName))
            {
                this.DDLTjField.Items.Clear();
                this.DDLTjField.DataSource = this.bll.GetTableField(TableName);
                this.DDLTjField.DataTextField = "fieldname";
                this.DDLTjField.DataValueField = "fieldname";
                this.DDLTjField.DataBind();
            }
        }
        /// <summary>
        /// 字段排序绑定字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BindOrderField(object sender, EventArgs e)
        {
            this.BindOrder();
        }
        private void BindOrder()
        {
            string TableName = "";
            if (this.DDLOrderTable.SelectedIndex == 0)
            {
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    TableName = this.DbTableDownList.SelectedValue;
                }
            }
            else
            {
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    TableName = this.DbTableDownList2.SelectedValue;
                }
            }
            if (!string.IsNullOrEmpty(TableName))
            {
                this.DDLOrderField.Items.Clear();
                this.DDLOrderField.DataSource = this.bll.GetTableField(TableName);
                this.DDLOrderField.DataTextField = "fieldname";
                this.DDLOrderField.DataValueField = "fieldname";
                this.DDLOrderField.DataBind();
            }
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string StrJion = " " + this.DDLJointj.SelectedValue + " ";
            string StrTable = "";
            bool addtable = false;
            if (this.DDLFTable.SelectedIndex == 0)
            {
                StrTable = this.DbTableDownList.SelectedValue;
            }
            else
            {
                StrTable = this.DbTableDownList2.SelectedValue;
            }
            if (this.DbTableDownList.SelectedIndex != 0 && this.DbTableDownList2.SelectedIndex != 0)
                addtable = true;
            if (!string.IsNullOrEmpty(this.DDLTjField.SelectedValue))
            {
                string StrField = this.DDLTjField.SelectedValue;
                if (!string.IsNullOrEmpty(this.TxtTjValue.Text))
                {
                    string StrTj = "";
                    if (addtable)
                        StrTj = StrTable + "." + StrField;
                    else
                        StrTj = StrField;
                    switch (this.DDLtj.SelectedIndex)
                    {
                        case 0:
                            StrTj = StrTj + "=" + this.TxtTjValue.Text;
                            break;
                        case 1:
                            StrTj = StrTj + ">" + this.TxtTjValue.Text;
                            break;
                        case 2:
                            StrTj = StrTj + "<" + this.TxtTjValue.Text;
                            break;
                        case 3:
                            StrTj = StrTj + ">=" + this.TxtTjValue.Text;
                            break;
                        case 4:
                            StrTj = StrTj + "<=" + this.TxtTjValue.Text;
                            break;
                        case 5:
                            StrTj = StrTj + "<>" + this.TxtTjValue.Text;
                            break;
                        case 6:
                            StrTj = StrTj + " in (" + this.TxtTjValue.Text + ")";
                            break;
                        case 7:
                            StrTj = StrTj + " like '%" + this.TxtTjValue.Text+"%'";
                            break;
                        case 8:
                            StrTj = StrTj + " not in (" + this.TxtTjValue.Text + ")";
                            break;
                    }
                    if (!string.IsNullOrEmpty(this.TxtSqlWhere.Text))
                    {
                        this.TxtSqlWhere.Text = this.TxtSqlWhere.Text + StrJion + StrTj;
                    }
                    else
                    {
                        this.TxtSqlWhere.Text = StrTj;
                    }
                }
            }
        }
        /// <summary>
        /// 添加字段排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            string StrTable = "";
            bool addtable = false;
            if (this.DDLOrderTable.SelectedIndex == 0)
            {
                StrTable = this.DbTableDownList.SelectedValue;
            }
            else
            {
                StrTable = this.DbTableDownList2.SelectedValue;
            }
            if (this.DbTableDownList.SelectedIndex != 0 && this.DbTableDownList2.SelectedIndex != 0)
                addtable = true;
            if (!string.IsNullOrEmpty(this.DDLOrderField.SelectedValue))
            {
                string StrField = this.DDLOrderField.SelectedValue;
                string StrOrder = "";
                if (addtable)
                {
                    StrOrder = StrTable + "." + StrField + " " + this.DDLOrder.SelectedValue;
                }
                else
                {
                    StrOrder = StrField + " " + this.DDLOrder.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.TxtOrder.Text))
                {
                    this.TxtOrder.Text = this.TxtOrder.Text + "," + StrOrder;
                }
                else
                {
                    this.TxtOrder.Text = StrOrder;
                }
            }
        }

        /// <summary>
        /// 设定查询字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            string FieldStr = "";
            if (this.DbTableDownList.SelectedIndex != 0 && this.DbTableDownList2.SelectedIndex != 0)
            {
                foreach (ListItem n in this.DbFieldDownList.Items)
                {
                    if (n.Selected)
                    {
                        if (!string.IsNullOrEmpty(FieldStr))
                            FieldStr = FieldStr + "," + this.DbTableDownList.SelectedValue + "." + n.Value;
                        else
                            FieldStr = this.DbTableDownList.SelectedValue + "." + n.Value;
                    }
                }
                foreach (ListItem n1 in this.DbFieldDownList2.Items)
                {
                    if (n1.Selected)
                    {
                        if (!string.IsNullOrEmpty(FieldStr))
                            FieldStr = FieldStr + "," + this.DbTableDownList2.SelectedValue + "." + n1.Value;
                        else
                            FieldStr = this.DbTableDownList2.SelectedValue + "." + n1.Value;
                    }
                }
            }
            else
            {
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    foreach (ListItem n in this.DbFieldDownList.Items)
                    {
                        if (n.Selected)
                        {
                            if (!string.IsNullOrEmpty(FieldStr))
                                FieldStr = FieldStr + "," + n.Value;
                            else
                                FieldStr = n.Value;
                        }
                    }
                }
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    foreach (ListItem n1 in this.DbFieldDownList2.Items)
                    {
                        if (n1.Selected)
                        {
                            if (!string.IsNullOrEmpty(FieldStr))
                                FieldStr = FieldStr + "," + n1.Value;
                            else
                                FieldStr = n1.Value;
                        }
                    }
                }
            }
            this.TxtSqlField.Text = FieldStr;
            if (string.IsNullOrEmpty(FieldStr))
            {
                if (this.DbTableDownList.SelectedIndex != 0 && this.DbTableDownList2.SelectedIndex != 0)
                {
                    FieldStr = this.DbTableDownList.SelectedValue + ".*," + this.DbTableDownList.SelectedValue + ".*";
                    this.TxtSqlField.Text = FieldStr;
                }
                else
                {
                    this.TxtSqlField.Text = "*";
                    string tablename = "";
                    if (this.DbTableDownList.SelectedIndex != 0)
                    {
                        tablename = this.DbTableDownList.SelectedValue;
                    }
                    else
                    {
                        tablename = this.DbTableDownList2.SelectedValue;
                    }
                    DataTable dt = this.bll.GetTableField(tablename);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(FieldStr))
                            FieldStr = FieldStr + dr["fieldname"].ToString();
                        else
                            FieldStr = FieldStr + "," + dr["fieldname"].ToString();
                    }
                }
            }
            SetLabelColumn(FieldStr);
        }
        private void SetLabelColumn(string sField)
        {
            string[] arrField = sField.Split(',');
            string result = "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code=\"{Repeate}{/Repeate}\">{Repeate}{/Repeate}</div>";            
            for (int i = 0; i < arrField.Length; i++)
            {
                if (arrField[i].IndexOf('.') > 0)
                {
                    if (arrField[i].Split('.')[1] == "*")
                    {
                        DataTable dt = this.bll.GetTableField(arrField[i].Split('.')[0]);
                        foreach (DataRow dr in dt.Rows)
                        {
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{Field=\"" + dr["fieldname"].ToString() + "\"/}'>{Field=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                        }
                    }
                    else
                    {
                        result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{Field=\"" + arrField[i].Split('.')[1] + "\"/}'>{Field=\"" + arrField[i].Split('.')[1] + "\"/}</div>";
                    }
                }
                else
                {
                    if (arrField[i] == "*")
                    {
                        DataTable dt = this.bll.GetTableField(this.TxtSqlTable.Text);
                        foreach (DataRow dr in dt.Rows)
                        {
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{Field=\"" + dr["fieldname"].ToString() + "\"/}'>{Field=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                        }
                    }
                    else
                    {
                        result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{Field=\"" + arrField[i] + "\"/}'>{Field=\"" + arrField[i] + "\"/}</div>";
                    }
                }
            }
            this.LblColumn.Text = result;
        }
        public string GetParamType(string value)
        {
            string re = "";
            if (value == "1")
                re = "普通参数";
            else
                re = "页面参数";
            return re;
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.HdnlabelID.Value == "0")
            {
                string lblname = args.Value.Trim();

                if (string.IsNullOrEmpty(lblname) || this.bll.IsExist(lblname))
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
            {
                args.IsValid = true;
            }
        }
}
}