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
using System.Collections.Generic;
using System.Xml;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.Template
{
    public partial class labelSqlOne : CustomerPageAction
    {
        private B_Label bll = new B_Label();
        private B_FunLabel bfun = new B_FunLabel();
        private string m_LabelName;
        private object dbConnectionString;
        private object dbConnectionString2;

        private string txt_DbTableDownList;
        private string txt_DbTableDownList2;
        private string txt_DatabaseList;
        private string txt_DatabaseList2;

        string ConnectionString = SqlHelper.ConnectionString;
        string PlugConnectionString = SqlHelper.PlugConnectionString;
        protected string dataname = "";
        protected string dataname2 = "";

        protected string T1 = "";
        protected string T2 = "";

        public string OldName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //LabelName.ForeColor = System.Drawing.Color.Red;
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();

            #region 初始化值

            dataname = DBHelper.GetAttrByStr(SqlHelper.ConnectionString, "Initial Catalog");
            dataname2 = DBHelper.GetAttrByStr(SqlHelper.PlugConnectionString, "Initial Catalog");
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, SqlHelper.ConnectionString);
            tablelist.Add(this.dataname2, SqlHelper.PlugConnectionString);

            if (RBLType.SelectedValue == "4")
            {
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator3.Visible = true;
            }
            else
            {
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator3.Visible = false;
            }
            if (string.IsNullOrEmpty(this.Request.QueryString["LabelName"]))
            {
                LabelName.Text = "添加标签";
            }
            else
            {
                this.m_LabelName = this.Request.QueryString["LabelName"];
            }

            if (!this.Page.IsPostBack)
            {

                this.DatabaseList.Items.Add(new ListItem("主数据库", dataname));
                this.DatabaseList.Items.Add(new ListItem("从数据库", dataname2));

                this.DatabaseList2.Items.Add(new ListItem("主数据库", dataname));
                this.DatabaseList2.Items.Add(new ListItem("从数据库", dataname2));

                #region 标签分类
                this.DDLCate.DataSource = this.bll.GetLabelCateListXML();
                this.DDLCate.DataTextField = "Name";
                this.DDLCate.DataValueField = "Name";
                this.DDLCate.DataBind();
                this.DDLCate.Items.Insert(0, new ListItem("选择标签类型", ""));

                this.DropLabelType.DataSource = this.bll.GetLabelCateListXML();
                this.DropLabelType.DataTextField = "Name";
                this.DropLabelType.DataValueField = "Name";
                this.DropLabelType.DataBind();
                #endregion

                this.LblSysLabel.Text = this.bfun.GetSysLabel();//系统标签
                this.LblFunLabel.Text = this.bfun.GetFunLabel();//扩展函数

                if (this.m_LabelName != null && this.m_LabelName != "")
                {
                    M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);
                    this.HdnlabelID.Value = labelInfo.LabelID.ToString();

                    this.TxtLabelName.Text = labelInfo.LableName;//标签名称
                    this.TxtLabelType.Text = labelInfo.LabelCate;//标签分类
                    LabelName.Text = "当前标签：" + labelInfo.LableName.ToString();
                    this.TxtLabelIntro.Text = labelInfo.Desc;//说明
                    this.HdnParam.Value = labelInfo.Param;//参数
                    RptParam_Bind(labelInfo.Param);//写入参数

                    string LabelTable = labelInfo.LabelTable;
                    LabelTable = LabelTable.Replace("{table1}.dbo.", dataname + ".dbo.");
                    LabelTable = LabelTable.Replace("{table2}.dbo.", dataname2 + ".dbo.");
                    this.TxtSqlTable.Text = LabelTable;//表名

                    string LabelField = labelInfo.LabelField;
                    LabelField = LabelField.Replace("{table1}.dbo.", dataname + ".dbo.");
                    LabelField = LabelField.Replace("{table2}.dbo.", dataname2 + ".dbo.");
                    this.TxtSqlField.Text = LabelField;//字段

                    string LabelWhere = labelInfo.LabelWhere;
                    LabelWhere = LabelWhere.Replace("{table1}.dbo.", dataname + ".dbo.");
                    LabelWhere = LabelWhere.Replace("{table2}.dbo.", dataname2 + ".dbo.");
                    this.TxtSqlWhere.Text = LabelWhere;//条件

                    string LabelOrder = labelInfo.LabelOrder;
                    LabelOrder = LabelOrder.Replace("{table1}.dbo.", dataname + ".dbo.");
                    LabelOrder = LabelOrder.Replace("{table2}.dbo.", dataname2 + ".dbo.");
                    this.TxtOrder.Text = LabelOrder;//排序

                    this.textContent.Text = labelInfo.Content;//标签内容
                    this.TextBox1.Text = labelInfo.LabelCount;//查询数量
                    this.RBLType.SelectedValue = labelInfo.LableType.ToString();//标签类型

                    //BindTj(this.dbConnectionString.ToString());

                    if (labelInfo.LabelField != "")
                    {
                        SetLabelColumn(labelInfo.LabelField);//置入标签
                    }

                    #region 存在多个库
                    string labeltable = labelInfo.LabelTable;
                    string table2 = "";
                    string labelfriest = "";

                    if (labeltable.IndexOf(" ") > -1)
                    {
                        string[] labelarr = labeltable.Split(new string[] { " " }, StringSplitOptions.None);
                        if (labelarr.Length > 0)
                        {
                            labelfriest = labelarr[0];//不存在圆点

                            if (labelfriest.IndexOf(".") == -1)
                            {
                                labelfriest = dataname + ".dbo." + labelfriest;
                            }

                            if (labeltable.IndexOf(" join ") > -1)
                            {
                                string[] labeljoin = labeltable.Split(new string[] { "join" }, StringSplitOptions.None);
                                if (labeljoin[1].IndexOf(" on ") > -1)
                                {
                                    string[] joinlabel = labeljoin[1].Split(new string[] { " on " }, StringSplitOptions.None);
                                    table2 = joinlabel[0];//不存在圆点
                                    if (table2.IndexOf(".") == -1)
                                    {
                                        table2 = dataname + ".dbo." + table2.Trim();
                                    }
                                }
                            }

                            #region 默认选择库
                            if (labelfriest.IndexOf("{table1}") > -1)
                            {
                                DatabaseList.SelectedIndex = 0;
                            }

                            if (labelfriest.IndexOf("{table2}") > -1)
                            {
                                DatabaseList.SelectedIndex = 1;
                            }

                            if (table2.IndexOf("{table1}") > -1)
                            {
                                DatabaseList2.SelectedIndex = 0;
                            }

                            if (table2.IndexOf("{table2}") > -1)
                            {
                                DatabaseList2.SelectedIndex = 1;
                            }
                            #endregion

                            labelfriest = labelfriest.Replace("{table1}.dbo.", dataname + ".dbo.");
                            labelfriest = labelfriest.Replace("{table2}.dbo.", dataname2 + ".dbo.");

                            table2 = table2.Replace("{table1}.dbo.", dataname + ".dbo.").Trim();
                            table2 = table2.Replace("{table2}.dbo.", dataname2 + ".dbo.").Trim();

                            //this.DbTableDownList.Items[0].Selected = true;
                            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "script2", "selectvalue('" + labelfriest + "','" + table2 + "');", true);
                        }
                    }
                    else
                    {
                        if (labeltable != "")
                        {
                            if (labeltable.IndexOf(".") == -1)
                            {
                                labeltable = dataname + ".dbo." + labeltable;
                            }

                            #region 默认选择库
                            if (labeltable.IndexOf("{table1}") > -1)
                            {
                                DatabaseList.SelectedIndex = 0;
                            }
                            if (labeltable.IndexOf("{table2}") > -1)
                            {
                                DatabaseList.SelectedIndex = 1;
                            }
                            #endregion

                            labelfriest = labeltable.Replace("{table1}.dbo.", dataname + ".dbo.");
                            labelfriest = labelfriest.Replace("{table2}.dbo.", dataname2 + ".dbo.");
                            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "script2", "selectvalue('" + labeltable + "','');", true);
                        }
                    }

                    T1 = labelfriest;
                    T2 = table2;

                    changedbtabledownlist();//绑定字段

                    #endregion
                }
                else
                {
                    changedbtabledownlist();
                    this.HdnlabelID.Value = "0";
                }

                if (this.Modelvalue.Text == "")
                {
                    this.Modelvalue.Text = "0";
                }

                #region 读取判断函数
                if (this.m_LabelName != null && this.m_LabelName != "")
                {
                    M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);
                    if (labelInfo.IsOpen == 1)
                    {
                        this.boolmodel.Checked = true;
                    }
                    this.addroot.SelectedValue = labelInfo.addroot;
                    this.falsecontent.Text = labelInfo.FalseContent;
                    this.Modeltypeinfo.Text = labelInfo.Modeltypeinfo;
                    this.Modelvalue.Text = labelInfo.Modelvalue;
                    this.setroot.SelectedValue = labelInfo.setroot;
                    this.Valueroot.Text = labelInfo.Valueroot;

                }
                #endregion

                if (this.T1 != "")
                {
                    this.DbTableDownList.SelectedValue = this.T1;
                }

                if (this.T2 != "")
                {
                    this.DbTableDownList2.SelectedValue = this.T2;
                }

                //changedbtabledownlist();//绑定字段

                #region 读取主字段
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    DataTable tabledlist = null;
                    if (DatabaseList.SelectedIndex == 0)
                    {
                        tabledlist = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                    }
                    else
                    {
                        tabledlist = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString2);
                    }

                    tabledlist.DefaultView.Sort = "fieldname asc";
                    this.DbFieldDownList.DataSource = tabledlist;
                    this.DbFieldDownList.DataTextField = "fieldname";
                    this.DbFieldDownList.DataValueField = "fieldname";
                    this.DbFieldDownList.DataBind();
                    this.DbFieldDownList.Items.Insert(0, new ListItem("*", "*"));
                    for (int i = 0; i < this.DbFieldDownList.Items.Count; i++)
                    {
                        DbFieldDownList.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, this.DbFieldDownList.Items[i].Text);
                    }

                    //this.TxtSqlTable.Text = this.DbTableDownList.SelectedValue;

                    if (this.DbTableDownList.SelectedIndex != 0)
                    {
                        DataTable t1 = null;
                        if (DatabaseList.SelectedIndex == 0)
                        {
                            t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                        }
                        else
                        {
                            t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString2);
                        }
                        this.DbFieldList.DataSource = t1;
                        t1.DefaultView.Sort = "fieldname asc";
                        this.DbFieldList.DataTextField = "fieldname";
                        this.DbFieldList.DataValueField = "fieldname";
                        this.DbFieldList.DataBind();
                        for (int i = 0; i < DbFieldList.Items.Count; i++)
                        {
                            DbFieldList.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, DbFieldList.Items[i].Text);
                        }

                        this.tj.Visible = true;

                        if (m_LabelName != "")
                        {
                            M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);
                            string labtletable = labelInfo.LabelTable;

                            if (labtletable.IndexOf("left join") > -1)
                            {
                                Dbtj.SelectedIndex = 0;
                            }

                            if (labtletable.IndexOf("Inner join") > -1)
                            {
                                Dbtj.SelectedIndex = 1;
                            }

                            if (labtletable.IndexOf("outer join") > -1)
                            {
                                Dbtj.SelectedIndex = 2;
                            }

                            if (labtletable.IndexOf("right join") > -1)
                            {
                                Dbtj.SelectedIndex = 3;
                            }

                            if (labtletable != "")
                            {
                                if (labtletable.IndexOf(" on ") > -1)
                                {
                                    string[] labelarr = labtletable.Split(new string[] { " on " }, StringSplitOptions.None);
                                    string la1 = labelarr[1];
                                    if (la1.IndexOf("=") > -1)
                                    {
                                        string[] listars = la1.Split(new string[] { "=" }, StringSplitOptions.None);
                                        string a1 = listars[0];
                                        string a2 = listars[1].Trim();
                                        if (a1.IndexOf(".") > -1)
                                        {
                                            string[] b1 = a1.Split(new string[] { "." }, StringSplitOptions.None);
                                            string c1 = b1[b1.Length - 1].ToString();
                                            try
                                            {
                                                DbFieldList.SelectedValue = c1;
                                            }
                                            catch { }
                                        }

                                        if (a2.IndexOf(".") > -1)
                                        {
                                            string[] b2 = a2.Split(new string[] { "." }, StringSplitOptions.None);
                                            string c2 = b2[b2.Length - 1].ToString();
                                            try
                                            {
                                                DbFieldList2.SelectedValue = c2;
                                            }
                                            catch { }
                                        }
                                    }
                                    //this.Literal1.Text = labelarr[1];
                                }
                            }
                            //DbFieldList.SelectedIndex = 2;
                        }
                    }
                    else
                    {
                        this.tj.Visible = false;
                    }
                }
                else
                {
                    this.DbFieldDownList.Items.Clear();
                    this.tj.Visible = false;
                }
                #endregion

                #region 读取从字段
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    DataTable tt2 = null;
                    if (this.DatabaseList2.SelectedIndex == 0)
                    {
                        tt2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString);
                    }
                    else
                    {
                        tt2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString2);
                    }

                    tt2.DefaultView.Sort = "fieldname asc";
                    this.DbFieldDownList2.DataSource = tt2;
                    this.DbFieldDownList2.DataTextField = "fieldname";
                    this.DbFieldDownList2.DataValueField = "fieldname";
                    this.DbFieldDownList2.DataBind();
                    for (int i = 0; i < DbFieldDownList2.Items.Count; i++)
                    {
                        DbFieldDownList2.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DbFieldDownList2.Items[i].Text);
                    }
                    this.DbFieldDownList2.Items.Insert(0, new ListItem("*", "*"));

                    //BindOrder();

                    if (this.DbTableDownList2.SelectedIndex != 0)
                    {
                        DataTable ttt2 = null;
                        if (DatabaseList2.SelectedIndex == 0)
                        {
                            ttt2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString);
                        }
                        else
                        {
                            ttt2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString2);
                        }

                        ttt2.DefaultView.Sort = "fieldname asc";
                        this.DbFieldList2.DataSource = ttt2;
                        this.DbFieldList2.DataTextField = "fieldname";
                        this.DbFieldList2.DataValueField = "fieldname";
                        this.DbFieldList2.DataBind();
                        for (int ii = 0; ii < DbFieldList2.Items.Count; ii++)
                        {
                            DbFieldDownList2.Items[ii].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DbFieldList2.Items[ii].Text);
                        }

                        this.tj.Visible = true;
                        //TableJoin1();
                    }
                    else
                    {
                        this.tj.Visible = false;
                        //this.TxtSqlTable.Text = this.DbTableDownList2.SelectedValue;
                    }
                }
                else
                {
                    this.DbFieldDownList2.Items.Clear();
                    this.tj.Visible = false;
                }
                #endregion

                #region 字段条件
                if (this.m_LabelName != null && this.m_LabelName != "")
                {
                    M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);

                    if (labelInfo.LabelWhere.IndexOf("{table") > -1)
                    {
                        if (labelInfo.LabelWhere.IndexOf("{table1}.") > -1)
                        {
                            this.DDLFTable.SelectedIndex = 0;
                            DataTable t1 = null;
                            if (DatabaseList.SelectedIndex == 0)
                            {
                                t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                            }
                            else
                            {
                                t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString2);
                            }
                            this.DDLTjField.DataSource = t1;
                            t1.DefaultView.Sort = "fieldname asc";
                            this.DDLTjField.DataTextField = "fieldname";
                            this.DDLTjField.DataValueField = "fieldname";
                            this.DDLTjField.DataBind();
                            for (int i = 0; i < DbFieldList.Items.Count; i++)
                            {
                                DDLTjField.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, DbFieldList.Items[i].Text);
                            }
                        }

                        if (labelInfo.LabelWhere.IndexOf("{table2}.") > -1)
                        {
                            this.DDLFTable.SelectedIndex = 1;

                            DataTable t1 = null;

                            if (DatabaseList2.SelectedIndex == 0)
                            {
                                t1 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString);
                            }
                            else
                            {
                                t1 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString2);
                            }

                            this.DDLTjField.DataSource = t1;
                            t1.DefaultView.Sort = "fieldname asc";
                            this.DDLTjField.DataTextField = "fieldname";
                            this.DDLTjField.DataValueField = "fieldname";
                            this.DDLTjField.DataBind();

                            for (int i = 0; i < DDLFTable.Items.Count; i++)
                            {
                                DDLTjField.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DDLFTable.Items[i].Text);
                            }
                        }
                    }
                    else
                    {
                        DataTable t1 = null;


                        t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);


                        this.DDLTjField.DataSource = t1;
                        t1.DefaultView.Sort = "fieldname asc";
                        this.DDLTjField.DataTextField = "fieldname";
                        this.DDLTjField.DataValueField = "fieldname";
                        this.DDLTjField.DataBind();

                        this.TxtSqlWhere.Text = labelInfo.LabelWhere;
                        //Response.Write(labelInfo.LabelWhere);
                        this.DDLFTable.SelectedIndex = 0;
                    }
                }
                #endregion

                BindOrder();

                //DBTableDownList_SelectedIndexChanged();
            }

            #region 字段注释
            for (int i = 0; i < DbFieldDownList.Items.Count; i++)
            {
                DbFieldDownList.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, DbFieldDownList.Items[i].Text);
            }

            for (int i = 0; i < DbFieldDownList2.Items.Count; i++)
            {
                DbFieldDownList2.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DbFieldDownList2.Items[i].Text);
            }
            #endregion

            #region 允许拖入参数
            this.TxtTjValue.Attributes.Add("onmouseup", "dragend2(this)");
            this.TxtTjValue.Attributes.Add("onClick", "savePos(this)");
            this.TxtTjValue.Attributes.Add("onmousemove", "DragPos(this)");

            this.textContent.Attributes.Add("onmouseup", "dragend(this)");
            this.textContent.Attributes.Add("onClick", "savePos(this)");
            this.textContent.Attributes.Add("onmousemove", "DragPos(this)");

            this.TextBox1.Attributes.Add("onmouseup", "dragend3(this)");
            this.TextBox1.Attributes.Add("onClick", "savePos(this)");
            this.TextBox1.Attributes.Add("onmousemove", "DragPos(this)");
            #endregion

            #region 使用判断函数
            if (boolmodel.Checked == true)
            {
                this.isbool.Visible = true;
                this.Label8.Visible = true;
                this.s1.Visible = true;
                this.s2.Visible = true;

                switch (Modeltypeinfo.SelectedValue)
                {
                    case "计数判断":
                        this.addroot.Visible = true;
                        this.Valueroot.Visible = false;
                        this.setroot.Visible = true;
                        this.Modelvalue.Visible = true;
                        this.Label3.Visible = true;
                        if (this.addroot.SelectedValue == "循环计算")
                        {
                            this.Label3.Text = "计数器达到条件将自动清零,仅限包含<font color=blue>循环标签</font>有效";
                        }
                        else if (this.addroot.SelectedValue == "一直累加,仅限包含<font color=blue>循环标签</font>有效")
                        {
                            this.Label3.Text = "计数器一直累加";
                        }
                        //this.RequiredFieldValidator2.Enabled = true;
                        break;
                    case "用户登陆判断":
                        this.addroot.Visible = false;
                        this.Valueroot.Visible = false;
                        this.setroot.Visible = false;
                        this.Modelvalue.Visible = false;
                        //this.RequiredFieldValidator2.Enabled = false;
                        this.Label3.Text = "判断用户是否登陆";
                        break;
                    case "参数判断":
                        this.addroot.Visible = false;
                        this.Valueroot.Visible = true;
                        this.setroot.Visible = true;
                        this.Modelvalue.Visible = true;
                        this.Label3.Text = "判断参数是否满足条件";
                        //this.RequiredFieldValidator2.Enabled = true;
                        break;
                }
            }
            else
            {
                this.Label8.Visible = false;
                this.s1.Visible = false;
                this.s2.Visible = false;
                this.isbool.Visible = false;
            }
            #endregion

            txt_DbTableDownList = DbTableDownList.SelectedValue;//主表
            txt_DbTableDownList2 = DbTableDownList2.SelectedValue;//从表
            txt_DatabaseList = DatabaseList.SelectedValue;//主库名
            txt_DatabaseList2 = DatabaseList2.SelectedValue;//从库名
            #endregion
        }

        /// <summary>
        /// 绑定字段
        /// </summary>
        private void changedbtabledownlist()
        {
            ListItem item = new ListItem();
            item.Text = "选择一个数据表";
            item.Value = "";

            ListItem item1 = new ListItem();
            item1.Text = "选择一个数据表";
            item1.Value = "";


            if (DatabaseList.SelectedIndex == 0)//主表主库绑定字段
            {
                DataTable listtable = this.bll.GetTableName(this.ConnectionString);
                this.DropLabelType.Items.Insert(0, new ListItem("选择标签分类", ""));
                listtable.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList.DataSource = listtable;
                this.DbTableDownList.DataTextField = "TABLENAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                this.DbTableDownList.Items.Insert(0, item);
                BindOptiontitle(listtable, DbTableDownList);//绑定注释
            }
            else if (DatabaseList.SelectedIndex == 1)//主表从库绑定字段
            {
                DataTable listtable = this.bll.GetTableName(this.PlugConnectionString);
                this.DropLabelType.Items.Insert(0, new ListItem("选择标签分类", ""));
                listtable.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList.DataSource = listtable;
                this.DbTableDownList.DataTextField = "TABLENAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                this.DbTableDownList.Items.Insert(0, item);
                BindOptiontitle(listtable, DbTableDownList);//绑定注释
            }

            if (DatabaseList2.SelectedIndex == 0)//从表主库绑定字段
            {
                DataTable listtable2 = this.bll.GetTableName(this.ConnectionString);
                listtable2.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList2.DataSource = listtable2;
                this.DbTableDownList2.DataTextField = "TABLENAME";
                this.DbTableDownList2.DataValueField = "TABLE_NAME";
                this.DbTableDownList2.DataBind();
                this.DbTableDownList2.Items.Insert(0, item1);
                BindOptiontitle(listtable2, DbTableDownList2);//绑定注释
            }

            if (DatabaseList2.SelectedIndex == 1)//从表从库绑定字段
            {
                DataTable listtable2 = this.bll.GetTableName(this.PlugConnectionString);
                listtable2.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList2.DataSource = listtable2;
                this.DbTableDownList2.DataTextField = "TABLENAME";
                this.DbTableDownList2.DataValueField = "TABLE_NAME";
                this.DbTableDownList2.DataBind();
                this.DbTableDownList2.Items.Insert(0, item1);
                BindOptiontitle(listtable2, DbTableDownList2);//绑定注释
            }
        }

        /// <summary>
        /// 绑定表名注释
        /// </summary>
        /// <param name="listtable"></param>
        /// <param name="DbTableDownListslist"></param>
        private void BindOptiontitle(DataTable listtable, DropDownList DbTableDownListslist)
        {
            for (int i = 0; i < DbTableDownListslist.Items.Count; i++)
            {
                string itemtitle = "";
                string itemtitlestr = DbTableDownListslist.Items[i].Text;

                if (itemtitlestr.IndexOf("ZL_AddRessList") >= 0)
                {
                    itemtitle = "广告";
                }
                else if (itemtitlestr.IndexOf("ZL_Advertisement") >= 0)
                {
                    itemtitle = "广告内容表";
                }
                else if (itemtitlestr.IndexOf("ZL_AdZone") >= 0)
                {
                    itemtitle = "广告版位表";
                }
                else if (itemtitlestr.IndexOf("ZL_Answer") >= 0)
                {
                    itemtitle = "问卷调查表";
                }
                else if (itemtitlestr.IndexOf("ZL_Answer_Recode") >= 0)
                {
                    itemtitle = "问卷回答表";
                }
                else if (itemtitlestr.IndexOf("ZL_Author") >= 0)
                {
                    itemtitle = "说明：作者表，存储作者信息";
                }
                else if (itemtitlestr.IndexOf("ZL_Bbscate") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Bbstips") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_BindPro") >= 0)
                {
                    itemtitle = "绑定商品销售表";
                }
                else if (itemtitlestr.IndexOf("ZL_CallNote") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Card") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Cart") >= 0)
                {
                    itemtitle = "购物车";
                }
                else if (itemtitlestr.IndexOf("ZL_CartPro") >= 0)
                {
                    itemtitle = "购物车详细列表";
                }
                else if (itemtitlestr.IndexOf("ZL_ClassRoom") >= 0)
                {
                    itemtitle = "班级";
                }
                else if (itemtitlestr.IndexOf("ZL_ClientRequire") >= 0)
                {
                    itemtitle = "说明：客户需求表";
                }
                else if (itemtitlestr.IndexOf("ZL_Comment") >= 0)
                {
                    itemtitle = "说明：评论 信息表发表的评论";
                }
                else if (itemtitlestr.IndexOf("ZL_Commodities") >= 0)
                {
                    itemtitle = "商品表";
                }
                else if (itemtitlestr.IndexOf("ZL_CommonModel") >= 0)
                {
                    itemtitle = "内容表";
                }
                else if (itemtitlestr.IndexOf("ZL_CompanyResume") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ContentHis") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Correct") >= 0)
                {
                    itemtitle = "说明：纠错反馈信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_Datadic") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Datadiccategory") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Delivier") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_DownServer") >= 0)
                {
                    itemtitle = "说明：镜像服务器表";
                }
                else if (itemtitlestr.IndexOf("ZL_Examinee") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Favorite") >= 0)
                {
                    itemtitle = "收藏夹";
                }
                else if (itemtitlestr.IndexOf("ZL_Grade") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_GradeCate") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Group") >= 0)
                {
                    itemtitle = "用户组";
                }
                else if (itemtitlestr.IndexOf("ZL_GroupFieldPermissions") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_GroupModel") >= 0)
                {
                    itemtitle = "会员模型";
                }
                else if (itemtitlestr.IndexOf("ZL_Guestbook") >= 0)
                {
                    itemtitle = "留言本";
                }
                else if (itemtitlestr.IndexOf("ZL_Guestcate") >= 0)
                {
                    itemtitle = "留言类别";
                }
                else if (itemtitlestr.IndexOf("ZL_JSTemplate") >= 0)
                {
                    itemtitle = "说明：广告模板脚本表";
                }
                else if (itemtitlestr.IndexOf("ZL_Keywords") >= 0)
                {
                    itemtitle = "说明：关键字表";
                }
                else if (itemtitlestr.IndexOf("ZL_Label") >= 0)
                {
                    itemtitle = "标签表";
                }
                else if (itemtitlestr.IndexOf("ZL_Log") >= 0)
                {
                    itemtitle = "日志表";
                }
                else if (itemtitlestr.IndexOf("ZL_MailIdiograph") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_MailInfo") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_MailManage") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Manager") >= 0)
                {
                    itemtitle = "管理员表";
                }
                else if (itemtitlestr.IndexOf("ZL_Manufacturers") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Message") >= 0)
                {
                    itemtitle = "说明：短消息表 存储短消息";
                }
                else if (itemtitlestr.IndexOf("ZL_Model") >= 0)
                {
                    itemtitle = "模型信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_ModelField") >= 0)
                {
                    itemtitle = "模型字段表";
                }
                else if (itemtitlestr.IndexOf("ZL_Node") >= 0)
                {
                    itemtitle = "节点表";
                }
                else if (itemtitlestr.IndexOf("ZL_Node_ModelTemplate") >= 0)
                {
                    itemtitle = "说明：关键字表";
                }
                else if (itemtitlestr.IndexOf("ZL_Online") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Orderinfo") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_PageStyle") >= 0)
                {
                    itemtitle = "黄页注册样式表";
                }
                else if (itemtitlestr.IndexOf("ZL_PageTemplate") >= 0)
                {
                    itemtitle = "黄页栏目样式表";
                }
                else if (itemtitlestr.IndexOf("ZL_Payment") >= 0)
                {
                    itemtitle = "支付信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_PayPlat") >= 0)
                {
                    itemtitle = "支付接口表";
                }
                else if (itemtitlestr.IndexOf("ZL_Present") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Project") >= 0)
                {
                    itemtitle = "说明：需求项目表";
                }
                else if (itemtitlestr.IndexOf("ZL_ProjectAffairs") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ProjectCategory") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ProjectDiscuss") >= 0)
                {
                    itemtitle = "说明：项目执行讨论表";
                }
                else if (itemtitlestr.IndexOf("ZL_ProjectField") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ProjectWork") >= 0)
                {
                    itemtitle = "说明：项目节点表";
                }
                else if (itemtitlestr.IndexOf("ZL_Promotions") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Pub") >= 0)
                {
                    itemtitle = "互动信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_Question") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ReadResume") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Result") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Role") >= 0)
                {
                    itemtitle = "管理员角色";
                }
                else if (itemtitlestr.IndexOf("ZL_RolePermissions") >= 0)
                {
                    itemtitle = "管理员角色权限";
                }
                else if (itemtitlestr.IndexOf("ZL_RoomActive") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_RoomActiveJoin") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_RoomMessage") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_RoomNotify") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Scheme") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_SchemeInfo") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_School") >= 0)
                {
                    itemtitle = "学校";
                }
                else if (itemtitlestr.IndexOf("ZL_ShopBrand") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ShopCommentary") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ShopCompete") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Shopconfig") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ShopGrade") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_BlogStyleTable") >= 0)
                {
                    itemtitle = "说明：SNS模板设置表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_blogTable") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_BookTable") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_CarConfig") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_Carlist") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_CarLog") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_ChatLog") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_CollectTable") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_CommendCommentOn") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_CommentAll") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_FileShare") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_GatherStrain") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_GSHuatee") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_GSReverCricicism") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_GSRoom") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_GSType") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_HomeCollocate") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_HomeHeadCollocate") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_Log") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_LotMessage") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_LotNote") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_Memo") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_Messageboard") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_MyCar") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_MyPose") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_PicCateg") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_ProductTable") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_ProductTypetable") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_ReplayLog") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_SystemLog") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_User_R_Module") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_UserLog") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_UserLogType") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_UserMoreinfo") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Sns_UserShopProduct") >= 0)
                {
                    itemtitle = "SNS信息类表";
                }
                else if (itemtitlestr.IndexOf("ZL_Source") >= 0)
                {
                    itemtitle = "说明：来源表";
                }
                else if (itemtitlestr.IndexOf("ZL_SpecCate") >= 0)
                {
                    itemtitle = "说明：专题类别表 存储专题类别信息";
                }
                else if (itemtitlestr.IndexOf("ZL_Special") >= 0)
                {
                    itemtitle = "说明：专题表";
                }
                else if (itemtitlestr.IndexOf("ZL_SpecInfo") >= 0)
                {
                    itemtitle = "说明：专题信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_Stock") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_StoreStyleTable") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Student") >= 0)
                {
                    itemtitle = "学生信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_Subscribe") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_Survey") >= 0)
                {
                    itemtitle = "问卷调查表";
                }
                else if (itemtitlestr.IndexOf("ZL_Trademark") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_URLRewriter") >= 0)
                {
                    itemtitle = "URL转发重写表";
                }
                else if (itemtitlestr.IndexOf("ZL_User") >= 0)
                {
                    itemtitle = "会员信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_UserAlipayTable") >= 0)
                {
                    itemtitle = "支付宝信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_UserBase") >= 0)
                {
                    itemtitle = "会员扩展信息表";
                }
                else if (itemtitlestr.IndexOf("ZL_UserCart") >= 0)
                {
                    itemtitle = "购物车表";
                }
                else if (itemtitlestr.IndexOf("ZL_UserCartPro") >= 0)
                {
                    itemtitle = "购物车商品表";
                }
                else if (itemtitlestr.IndexOf("ZL_UserExpDomP") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserExpHis") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserFriendGroup") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserFriendTable") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserOrderinfo") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserShop") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserStock") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserStoreTable") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_UserStoreTypeTable") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_ValidityPeriod") >= 0)
                {
                    itemtitle = "";
                }
                else if (itemtitlestr.IndexOf("ZL_WorkRole") >= 0)
                {
                    itemtitle = "说明：项目执行角色表";
                }
                else if (itemtitlestr.IndexOf("ZL_Zone_Advertisement") >= 0)
                {
                    itemtitle = "说明：广告版位表";
                }
                else if (itemtitlestr.IndexOf("ZL_C_") >= 0)
                {
                    itemtitle = "内容模型";
                }
                else if (itemtitlestr.IndexOf("ZL_P_") >= 0)
                {
                    itemtitle = "商品模型";
                }
                else if (itemtitlestr.IndexOf("ZL_Page_") >= 0)
                {
                    itemtitle = "黄页模型";
                }
                else if (itemtitlestr.IndexOf("ZL_U_") >= 0)
                {
                    itemtitle = "会员模型";
                }
                else if (itemtitlestr.IndexOf("ZL_Pub_") >= 0)
                {
                    itemtitle = "互动模型";
                }
                else if (itemtitlestr.IndexOf("ZL_S_") >= 0)
                {
                    itemtitle = "店铺商品模型";
                }
                else if (itemtitlestr.IndexOf("ZL_Store_") >= 0)
                {
                    itemtitle = "店铺申请模型";
                }
                if (itemtitle == "")
                {
                    itemtitle = "未知";
                }

                DbTableDownListslist.Items[i].Attributes["title"] = itemtitle;
                //DbTableDownList2.Items[i].Attributes["title"] = itemtitle;

                itemtitle = "";
            }
        }

        /// <summary>
        /// 主表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DBTableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            string databaselistvalue = txt_DatabaseList;//Request.Form["DatabaseList"];
            string databaselistvalue2 = txt_DatabaseList2; // Request.Form["DatabaseList2"];

            string Conection = tablelist[databaselistvalue].ToString();
            string Conection2 = tablelist[databaselistvalue2].ToString();

            this.dbConnectionString = Conection;
            this.dbConnectionString2 = Conection2;

            if (this.DbTableDownList.SelectedIndex != 0)
            {
                DataTable tabledlist = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                tabledlist.DefaultView.Sort = "fieldname asc";
                this.DbFieldDownList.DataSource = tabledlist;
                this.DbFieldDownList.DataTextField = "fieldname";
                this.DbFieldDownList.DataValueField = "fieldname";
                this.DbFieldDownList.DataBind();
                this.DbFieldDownList.Items.Insert(0, new ListItem("*", "*"));
                for (int i = 0; i < this.DbFieldDownList.Items.Count; i++)
                {
                    DbFieldDownList.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, this.DbFieldDownList.Items[i].Text);
                }

                this.TxtSqlTable.Text = this.DbTableDownList.SelectedValue;

                BindTj(this.dbConnectionString.ToString());
                BindOrder();

                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    DataTable t1 = null;
                    if (DatabaseList2.SelectedIndex == 0)
                    {
                        t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                    }
                    else
                    {
                        t1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString2);
                    }

                    this.DbFieldList.DataSource = t1;
                    t1.DefaultView.Sort = "fieldname asc";
                    this.DbFieldList.DataTextField = "fieldname";
                    this.DbFieldList.DataValueField = "fieldname";
                    this.DbFieldList.DataBind();
                    for (int i = 0; i < DbFieldList.Items.Count; i++)
                    {
                        DbFieldList.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, DbFieldList.Items[i].Text);
                    }

                    DataTable t2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString2);
                    this.DbFieldList2.DataSource = t2;
                    t2.DefaultView.Sort = "fieldname asc";
                    this.DbFieldList2.DataTextField = "fieldname";
                    this.DbFieldList2.DataValueField = "fieldname";
                    this.DbFieldList2.DataBind();
                    for (int i = 0; i < DbFieldList2.Items.Count; i++)
                    {
                        DbFieldList2.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DbFieldList2.Items[i].Text);
                    }
                    this.tj.Visible = true;

                    TableJoin1();

                    if (m_LabelName != "")
                    {
                        M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);
                        string labtletable = labelInfo.LabelTable;

                        if (labtletable.IndexOf("left join") > -1)
                        {
                            Dbtj.SelectedIndex = 0;
                        }

                        if (labtletable.IndexOf("Inner join") > -1)
                        {
                            Dbtj.SelectedIndex = 1;
                        }

                        if (labtletable.IndexOf("outer join") > -1)
                        {
                            Dbtj.SelectedIndex = 2;
                        }

                        if (labtletable.IndexOf("right join") > -1)
                        {
                            Dbtj.SelectedIndex = 3;
                        }

                        if (labtletable != "")
                        {
                            if (labtletable.IndexOf(" on ") > -1)
                            {
                                string[] labelarr = labtletable.Split(new string[] { " on " }, StringSplitOptions.None);
                                string la1 = labelarr[1];
                                if (la1.IndexOf("=") > -1)
                                {
                                    string[] listars = la1.Split(new string[] { "=" }, StringSplitOptions.None);
                                    string a1 = listars[0];
                                    string a2 = listars[1].Trim();
                                    if (a1.IndexOf(".") > -1)
                                    {
                                        string[] b1 = a1.Split(new string[] { "." }, StringSplitOptions.None);
                                        string c1 = b1[b1.Length - 1].ToString();
                                        try
                                        {
                                            DbFieldList.SelectedValue = c1;
                                        }
                                        catch { }
                                    }

                                    if (a2.IndexOf(".") > -1)
                                    {
                                        string[] b2 = a2.Split(new string[] { "." }, StringSplitOptions.None);
                                        string c2 = b2[b2.Length - 1].ToString();
                                        try
                                        {
                                            DbFieldList2.SelectedValue = c2;
                                        }
                                        catch { }
                                    }
                                }
                                //this.Literal1.Text = labelarr[1];
                            }
                        }
                        //DbFieldList.SelectedIndex = 2;
                    }
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
        /// <summary>
        /// 标签列举
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangeCate(object sender, EventArgs e)
        {
            string LabelCate = this.DDLCate.SelectedValue;

            DataTable dt = this.bll.SelAllLabel(LabelCate);
            string lblLabels = "";
            foreach (DataRow dr in dt.Rows)
            {
                M_Label labelinfo = this.bll.GetLabelXML(dr["LabelName"].ToString().Split('.')[0].ToString());


                if (DataConverter.CLng(labelinfo.LableType) == 1)//静态标签
                {
                    lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelHtml.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"1\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                }
                else if (DataConverter.CLng(labelinfo.LableType) == 3)//数据源
                {
                    if (string.IsNullOrEmpty(labelinfo.Param))
                    {
                        lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"3\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                    }
                    else
                    {
                        string Param = labelinfo.Param;

                        if (Param.IndexOf("|") < 0)
                        {
                            if (Param.Split(new char[] { ',' })[2] == "2")
                            {
                                lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"3\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                            }
                            else
                            {
                                //带参数数据源
                                lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"2\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                            }
                        }
                        else
                        {
                            string[] dd = Param.Split(new char[] { '|' });
                            int tempd = 0;
                            for (int cd = 0; cd < dd.Length; cd++)
                            {
                                if (dd[cd].Split(new char[] { ',' })[2] == "2")
                                {
                                    tempd = 1;
                                }
                                else
                                {
                                    tempd = 0;
                                }
                            }

                            if (tempd > 0)
                            {
                                lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"3\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"4\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                            }
                            //lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"4\" onclick=\"cit(this)\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";

                        }
                    }
                }
                else if (DataConverter.CLng(labelinfo.LableType) >= 5)//分页标签
                {
                    if (DataConverter.CLng(labelinfo.LableType) == 5)
                    {
                        lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('PageLabel.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"5\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                    }
                    else
                    {
                        lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('PageLabel.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName.ToString()) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"6\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                    }
                }
                else if (DataConverter.CLng(labelinfo.LableType) < 5)
                {
                    if (string.IsNullOrEmpty(labelinfo.Param))
                    {
                        lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('PageLabel.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"1\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                    }
                    else
                    {
                        string Param = labelinfo.Param;

                        if (Param.IndexOf("|") < 0)
                        {
                            if (Param.Split(new char[] { ',' })[2] == "2")
                            {
                                lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"1\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"2\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                            }
                        }
                        else
                        {
                            lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"2\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                        }
                    }
                }
                else
                {
                    lblLabels = lblLabels + "<div class=editdiv><a onclick=opentitle('LabelSqlOne.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签[ESC键退出当前操作]') href=\"javascript:void(0)\" title=\"修改标签\"><img src='/Images/l_edit.jpg' border='0' /></a></div><div class=\"spanfixdivchechk\" outtype=\"3\" onclick=\"cit(this)\" code=\"" + labelinfo.LableName + "\">" + labelinfo.LableName + "</div>";
                }
            }
            this.LblLabel.Text = lblLabels;
        }
        /// <summary>
        /// 从表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DBTableDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            string databaselistvalue = DatabaseList.SelectedValue;// Request.Form["DatabaseList"];
            string databaselistvalue2 = DatabaseList2.SelectedValue;// Request.Form["DatabaseList2"];

            string Conection = tablelist[databaselistvalue].ToString();
            string Conection2 = tablelist[databaselistvalue2].ToString();

            this.dbConnectionString = Conection;
            this.dbConnectionString2 = Conection2;

            if (this.DbTableDownList2.SelectedIndex != 0)
            {
                DataTable tt2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString2);
                tt2.DefaultView.Sort = "fieldname asc";
                this.DbFieldDownList2.DataSource = tt2;
                this.DbFieldDownList2.DataTextField = "fieldname";
                this.DbFieldDownList2.DataValueField = "fieldname";
                this.DbFieldDownList2.DataBind();
                for (int i = 0; i < DbFieldDownList2.Items.Count; i++)
                {
                    DbFieldDownList2.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DbFieldDownList2.Items[i].Text);
                }
                this.DbFieldDownList2.Items.Insert(0, new ListItem("*", "*"));
                BindTj(this.dbConnectionString2.ToString());
                BindOrder();

                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    DataTable ttt1 = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                    ttt1.DefaultView.Sort = "fieldname asc";
                    this.DbFieldList.DataSource = ttt1;
                    this.DbFieldList.DataTextField = "fieldname";
                    this.DbFieldList.DataValueField = "fieldname";
                    this.DbFieldList.DataBind();
                    for (int ii = 0; ii < DbFieldList.Items.Count; ii++)
                    {
                        DbFieldList.Items[ii].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, DbFieldList.Items[ii].Text);
                    }
                    DataTable ttt2 = this.bll.GetTableField(this.DbTableDownList2.SelectedValue, this.dbConnectionString2);
                    ttt2.DefaultView.Sort = "fieldname asc";
                    this.DbFieldList2.DataSource = ttt2;
                    this.DbFieldList2.DataTextField = "fieldname";
                    this.DbFieldList2.DataValueField = "fieldname";
                    this.DbFieldList2.DataBind();
                    for (int ii = 0; ii < DbFieldList2.Items.Count; ii++)
                    {
                        DbFieldDownList2.Items[ii].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList2.SelectedValue, DbFieldList2.Items[ii].Text);
                    }
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

        //protected void DBTableDownListall_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList s = (DropDownList)sender;

        //    string SendClientid = s.ClientID;
        //    string truespit = "";
        //    string[] tempclid = SendClientid.Split(new string[] { "_" }, StringSplitOptions.None);
        //    if (tempclid.Length > 2)
        //    {
        //        truespit = tempclid[1].ToString();
        //    }

        //    string ctemps = "Repeater1$" + truespit + "$DbFieldDownList2";
        //    ListBox listboxs = (ListBox)this.form1.FindControl(ctemps);

        //    if (s.SelectedIndex != 0)
        //    {
        //        DataTable tt2 = this.bll.GetTableField(s.SelectedValue, this.dbConnectionString2);
        //        tt2.DefaultView.Sort = "fieldname asc";
        //        listboxs.DataSource = tt2;
        //        listboxs.DataTextField = "fieldname";
        //        listboxs.DataValueField = "fieldname";
        //        listboxs.DataBind();

        //        for (int i = 0; i < listboxs.Items.Count; i++)
        //        {
        //            listboxs.Items[i].Attributes["title"] = bll.GetTablecolumn(s.SelectedValue, listboxs.Items[i].Text);
        //        }
        //        BindTj(this.dbConnectionString.ToString());//绑定查询条件
        //        BindOrder();
        //        if (this.TxtSqlTable.Text != "")
        //        {
        //            DataTable ttt1 = this.bll.GetTableField(s.SelectedValue, this.dbConnectionString);
        //            ttt1.DefaultView.Sort = "fieldname asc";
        //            listboxs.DataSource = ttt1;
        //            listboxs.DataTextField = "fieldname";
        //            listboxs.DataValueField = "fieldname";
        //            listboxs.DataBind();
        //            for (int ii = 0; ii < listboxs.Items.Count; ii++)
        //            {
        //                listboxs.Items[ii].Attributes["title"] = bll.GetTablecolumn(s.SelectedValue, listboxs.Items[ii].Text);
        //            }
        //            DataTable ttt2 = this.bll.GetTableField(s.SelectedValue, this.dbConnectionString2);
        //            ttt2.DefaultView.Sort = "fieldname asc";
        //            listboxs.DataSource = ttt2;
        //            listboxs.DataTextField = "fieldname";
        //            listboxs.DataValueField = "fieldname";
        //            listboxs.DataBind();
        //            for (int ii = 0; ii < listboxs.Items.Count; ii++)
        //            {
        //                listboxs.Items[ii].Attributes["title"] = bll.GetTablecolumn(s.SelectedValue, listboxs.Items[ii].Text);
        //            }
        //            this.tj.Visible = true;
        //            TableJoin1();
        //        }
        //        else
        //        {
        //            this.tj.Visible = false;
        //            this.TxtSqlTable.Text = s.SelectedValue;
        //        }
        //    }
        //    else
        //    {
        //        listboxs.Items.Clear();
        //        if (s.SelectedIndex != 0)
        //        {
        //            this.TxtSqlTable.Text = s.SelectedValue;
        //        }
        //        else
        //        {
        //            this.TxtSqlTable.Text = "";
        //        }
        //        this.tj.Visible = false;
        //    }
        //}

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAddParam_Click(object sender, EventArgs e)
        {
            if (this.TxtParamName.Text.ToLower() == "id")
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + this.TxtParamName.Text + "为保留参数，不能添加此参数，请输入其他参数!');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script2", "<script>alert('" + this.TxtParamName.Text + "为保留参数，不能添加此参数，请输入其他参数!');</script>");
            }
            else
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
                BtnAddParam.Text = "添加";
            }
        }

        /// <summary>
        /// 写入参数
        /// </summary>
        /// <param name="Param"></param>
        private void RptParam_Bind(string Param)
        {
            DataTable paramTb = new DataTable("labelparam");
            DataColumn myDataColumn;
            DataRow myDataRow;
            this.attlist.Text = "";
            this.attlist1.Text = "";
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
                    this.attlist.Text = this.attlist.Text + "<div class=\"spanfixdiv1\" outtype=\"0\" onclick=\"cit(this)\" code=\"" + "@" + arrParam[i].Split(',')[0] + "\">" + "@" + arrParam[i].Split(',')[0] + "</div>";
                    this.attlist1.Text = this.attlist1.Text + "<div class=\"spanfixdiv1\" outtype=\"0\" onclick=\"cit(this)\" code=\"" + "@" + arrParam[i].Split(',')[0] + "\">" + "@" + arrParam[i].Split(',')[0] + "</div>";
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
                BtnAddParam.Text = "修改";
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

            if (this.TxtSqlTable.Text == "")
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('查询表不能为空!');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('查询表不能为空!');</script>");
            }
            else
            {
                if (this.Page.IsValid)
                {
                    M_Label LabelInfo = new M_Label();
                    LabelInfo.LabelID = DataConverter.CLng(this.HdnlabelID.Value);
                    LabelInfo.LableName = this.TxtLabelName.Text;
                    M_Label Info = this.bll.GetLabelXML(this.m_LabelName);
                    if (Info.LableName.ToString() != this.TxtLabelName.Text)
                    {
                        this.bll.CheckLabelXML(this.TxtLabelName.Text);
                    }
                    LabelInfo.LableType = DataConverter.CLng(this.RBLType.SelectedValue);
                    LabelInfo.LabelCate = this.TxtLabelType.Text;
                    LabelInfo.Desc = this.TxtLabelIntro.Text;
                    LabelInfo.Param = this.HdnParam.Value;

                    string TxtSqlTable = this.TxtSqlTable.Text;
                    TxtSqlTable = TxtSqlTable.Replace(this.dataname + ".dbo.", "{table1}.dbo.");
                    TxtSqlTable = TxtSqlTable.Replace(this.dataname2 + ".dbo.", "{table2}.dbo.");
                    LabelInfo.LabelTable = TxtSqlTable;//?

                    string TxtSqlField = this.TxtSqlField.Text;
                    TxtSqlField = TxtSqlField.Replace(this.dataname + ".dbo.", "{table1}.dbo.");
                    TxtSqlField = TxtSqlField.Replace(this.dataname2 + ".dbo.", "{table2}.dbo.");
                    LabelInfo.LabelField = TxtSqlField;//?

                    string LabelWhere = this.TxtSqlWhere.Text;//?
                    LabelWhere = LabelWhere.Replace(this.dataname + ".dbo.", "{table1}.dbo.");
                    LabelWhere = LabelWhere.Replace(this.dataname2 + ".dbo.", "{table2}.dbo.");
                    LabelInfo.LabelWhere = LabelWhere;//?

                    string LabelOrder = this.TxtOrder.Text;//?  
                    LabelOrder = LabelOrder.Replace(this.dataname + ".dbo.", "{table1}.dbo.");
                    LabelOrder = LabelOrder.Replace(this.dataname2 + ".dbo.", "{table2}.dbo.");
                    LabelInfo.LabelOrder = LabelOrder;//?

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
                    {
                        int Rlabelid = LabelInfo.LabelID;
                        if (Rlabelid > 0)
                        {
                            LabelInfo.LabelID = Rlabelid;
                            LabelInfo.Modeltypeinfo = this.Modeltypeinfo.Text;
                            LabelInfo.Modelvalue = this.Modelvalue.Text;
                            LabelInfo.setroot = this.setroot.SelectedValue;
                            if (this.Valueroot.Text == "这里放入标签")
                            {
                                LabelInfo.Valueroot = "";
                            }
                            else
                            {
                                LabelInfo.Valueroot = this.Valueroot.Text;
                            }
                            LabelInfo.FalseContent = this.falsecontent.Text;
                            LabelInfo.addroot = this.addroot.SelectedValue;
                            LabelInfo.IsOpen = this.boolmodel.Checked ? 1 : 0;
                        }
                        this.bll.UpdateLabelXML(LabelInfo);
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('修改成功!');location.href='LabelManage.aspx';", true);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script2", "<script>alert('修改成功!');location.href='LabelManage.aspx';</script>");

                    }
                    else
                    {
                        LabelInfo.LabelNodeID = 0;
                        LabelInfo.Modeltypeinfo = this.Modeltypeinfo.Text;
                        LabelInfo.Modelvalue = this.Modelvalue.Text;
                        LabelInfo.setroot = this.setroot.SelectedValue;
                        LabelInfo.Valueroot = this.Valueroot.Text;
                        LabelInfo.IsOpen = this.boolmodel.Checked ? 1 : 0;
                        LabelInfo.FalseContent = this.falsecontent.Text;
                        LabelInfo.addroot = this.addroot.SelectedValue;
                        this.bll.AddLabelXML(LabelInfo);
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('添加成功!');location.href='LabelManage.aspx';", true);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script2", "<script>alert('添加成功!');location.href='LabelManage.aspx';</script>");

                    }
                }
            }
        }
        /// <summary>
        /// 主从表连接
        /// </summary>
        protected void TableJoin(object sender, EventArgs e)
        {
            TableJoin1();
        }

        /// <summary>
        /// 查询表的信息
        /// </summary>
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
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            string DataName = "";

            DropDownList datalist = (DropDownList)this.FindControl("DatabaseList");
            DropDownList datalist2 = (DropDownList)this.FindControl("DatabaseList2");

            DropDownList tabledownlist = (DropDownList)this.FindControl("DbTableDownList");
            DropDownList tabledownlist2 = (DropDownList)this.FindControl("DbTableDownList2");


            if (DDLFTable.SelectedIndex == 0)
            {
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    DataName = datalist.SelectedValue;
                }
            }
            else
            {
                if (tabledownlist2.SelectedIndex != 0)
                {
                    DataName = datalist2.SelectedValue;
                }
            }

            if (tablelist[DataName] != null)
            {
                this.BindTj(tablelist[DataName].ToString());
            }
        }
        /// <summary>
        /// 绑定查询条件
        /// </summary>
        /// <param name="ConnectionString"></param>
        private void BindTj(string ConnectionString)
        {
            string TableName = "";
            if (this.DDLFTable.SelectedIndex == 0)
            {
                if (this.DbTableDownList.SelectedIndex != 0)
                {
                    TableName = txt_DbTableDownList;
                }
            }
            else
            {
                if (this.DbTableDownList2.SelectedIndex != 0)
                {
                    TableName = txt_DbTableDownList2;
                }
            }

            if (!string.IsNullOrEmpty(TableName))
            {
                this.DDLTjField.Items.Clear();
                DataTable ddd1 = this.bll.GetTableField(TableName, ConnectionString);
                ddd1.DefaultView.Sort = "fieldname asc";
                this.DDLTjField.DataSource = ddd1;
                this.DDLTjField.DataTextField = "fieldname";
                this.DDLTjField.DataValueField = "fieldname";
                this.DDLTjField.DataBind();
                for (int ii = 0; ii < DDLTjField.Items.Count; ii++)
                {
                    DDLTjField.Items[ii].Attributes["title"] = bll.GetTablecolumn(TableName, DDLTjField.Items[ii].Text);
                }
            }
        }
        /// <summary>
        /// 字段排序绑定字段==
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BindOrderField(object sender, EventArgs e)
        {
            this.BindOrder();
        }

        private void BindOrder()
        {
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            string TableName = "";
            /////////////////////////////////////////////////////
            if (this.DDLOrderTable.SelectedIndex == 0)//主从表的下拉
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
            ///////////////////////////////////////////////////

            string Tempsli = TableName;

            string[] tmpname;
            string truetablename = "";
            if (TableName.IndexOf(".dbo.") > -1)
            {
                tmpname = TableName.Split(new string[] { ".dbo." }, StringSplitOptions.RemoveEmptyEntries);
                truetablename = tmpname[1];
                TableName = tmpname[0];
            }
            else
            {
                if (TableName.IndexOf('.') > -1)
                {
                    string[] temptablename = TableName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    truetablename = temptablename[1];
                }
                else
                {
                    truetablename = TableName;
                }
            }

            if (!string.IsNullOrEmpty(truetablename))
            {
                this.DDLOrderField.Items.Clear();
                DataTable cc = this.bll.GetTableField(truetablename, tablelist[TableName].ToString());
                cc.DefaultView.Sort = "fieldname asc";
                this.DDLOrderField.DataSource = cc;
                this.DDLOrderField.DataTextField = "fieldname";
                this.DDLOrderField.DataValueField = "fieldname";
                this.DDLOrderField.DataBind();

                for (int ii = 0; ii < DDLOrderField.Items.Count; ii++)
                {
                    DDLOrderField.Items[ii].Attributes["title"] = bll.GetTablecolumn(TableName, DDLOrderField.Items[ii].Text);
                }
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
                            StrTj = StrTj + " like '%" + this.TxtTjValue.Text + "%'";
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
            if (string.IsNullOrEmpty(FieldStr))
            {
                if (this.DbTableDownList.SelectedIndex != 0 && this.DbTableDownList2.SelectedIndex != 0)
                {
                    FieldStr = this.DbTableDownList.SelectedValue + ".*," + this.DbTableDownList2.SelectedValue + ".*";
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
                    DataTable dt = this.bll.GetTableField(tablename, this.dbConnectionString);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(FieldStr))
                            FieldStr = FieldStr + dr["fieldname"].ToString();
                        else
                            FieldStr = FieldStr + "," + dr["fieldname"].ToString();
                    }
                }
            }
            else
            {
                this.TxtSqlField.Text = FieldStr;
            }
            SetLabelColumn(FieldStr);
            //this.Label9.Text = FieldStr.ToString();
        }
        /// <summary>
        /// 置入标签
        /// </summary>
        /// <param name="sField"></param>
        private void SetLabelColumn(string sField)
        {
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            sField = sField.Replace("{table1}.dbo.", this.dataname + ".dbo.");
            sField = sField.Replace("{table2}.dbo.", this.dataname2 + ".dbo.");

            string[] arrField = sField.Split(',');

            string result = "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code=\"{Repeate}{/Repeate}\">循环函数</div>";
            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code=\"{ZL:jsq}\">计数器</div>";
            for (int i = 0; i < arrField.Length; i++)
            {
                if (arrField[i].IndexOf('.') > 0)
                {
                    if (arrField[i].Split('.').Length < 4)
                    {
                        if (arrField[i].Split('.')[1] == "*")
                        {
                            DataTable dt = this.bll.GetTableField(arrField[i].Split('.')[0], ConnectionString);
                            foreach (DataRow dr in dt.Rows)
                            {
                                result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{Field=\"" + dr["fieldname"].ToString() + "\"/}' title='" + bll.GetTablecolumn(arrField[i].Split('.')[0], dr["fieldname"].ToString()) + "'>{Field=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                            }
                        }
                        else
                        {
                            //Label4.Text = arrField[i];
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{Field=\"" + arrField[i].Split('.')[1] + "\"/}' title='" + bll.GetTablecolumn(arrField[i].Split('.')[0], arrField[i].Split('.')[1]) + "'>{Field=\"" + arrField[i].Split('.')[1] + "\"/}</div>";
                        }
                    }
                    else
                    {
                        if (arrField[i].Split('.')[3] == "*")
                        {
                            DataTable dt = this.bll.GetTableField(arrField[i].Split('.')[2], tablelist[arrField[i].Split('.')[0]]);
                            foreach (DataRow dr in dt.Rows)
                            {
                                string vv1 = arrField[i];
                                if (vv1.IndexOf("*") > -1)
                                {
                                    vv1 = vv1.Replace(".*", "");
                                }
                                result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{Field=\"" + dr["fieldname"].ToString() + "\"/}' title='" + bll.GetTablecolumn(vv1, dr["fieldname"].ToString()) + "'>{Field=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                            }
                        }
                        else
                        {
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{Field=\"" + arrField[i].Split('.')[3] + "\"/}'>{Field=\"" + arrField[i].Split('.')[3] + "\"/}</div>";
                        }
                    }
                }
                else
                {
                    IList<string> Filedlist = new List<string>();
                    if (arrField[i] == "*")
                    {
                        string tablename = "";
                        string linkstr = this.TxtSqlTable.Text;
                        //tablelist.Add(dataname, ConnectionString);
                        //tablelist.Add(dataname2, PlugConnectionString);

                        string connectionstr = ConnectionString;
                        string hdst = "";
                        if (linkstr.IndexOf(".") > -1)
                        {
                            string[] linkarr = linkstr.Split(new string[] { "." }, StringSplitOptions.None);
                            tablename = linkarr[linkarr.Length - 1];
                            hdst = linkarr[0];
                            connectionstr = tablelist[hdst].ToString();
                        }

                        DataTable dt = this.bll.GetTableField(tablename, connectionstr);

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (Filedlist.IndexOf(dr["fieldname"].ToString()) == -1)
                            {
                                //this.Label9.Text += arrField[i] + "|";
                                Filedlist.Add(dr["fieldname"].ToString());
                                result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{Field=\"" + dr["fieldname"].ToString() + "\"/}' title='" + bll.GetTablecolumn(this.TxtSqlTable.Text, dr["fieldname"].ToString()) + "'>{Field=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                            }
                        }
                    }
                    else
                    {
                        if (Filedlist.IndexOf(arrField[i]) == -1)
                        {
                            //this.Label9.Text += arrField[i]+"|";
                            Filedlist.Add(arrField[i]);
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{Field=\"" + arrField[i] + "\"/}'>{Field=\"" + arrField[i] + "\"/}</div>";
                        }
                    }
                }
            }
            this.LblColumn.Text = result;
        }
        /// <summary>
        /// 置入参数类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.HdnlabelID.Value == "0")
            {
                string lblname = args.Value.Trim();

                if (string.IsNullOrEmpty(lblname) || this.bll.IsExistXML(lblname))
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
            {
                args.IsValid = true;
            }
        }

        /// <summary>
        /// 主表更换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            string databaselistvalue = DatabaseList.SelectedValue;

            string Conection1 = tablelist[databaselistvalue].ToString();

            DataTable listtables1 = this.bll.GetTableName(Conection1);

            this.DbTableDownList.DataSource = listtables1;
            this.DbTableDownList.DataTextField = "TABLENAME";
            this.DbTableDownList.DataValueField = "TABLE_NAME";
            this.DbTableDownList.DataBind();

            ListItem item = new ListItem();
            item.Text = "选择一个数据表";
            this.DbTableDownList.Items.Insert(0, item);

            dbConnectionString = Conection1;
        }
        /// <summary>
        /// 从表更换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TableDownList_SelectedIndexChanged2(object sender, EventArgs e)
        {
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            string databaselistvalue2 = DatabaseList2.SelectedValue;

            string Conection2 = tablelist[databaselistvalue2].ToString();

            DataTable listtables2 = this.bll.GetTableName(Conection2);

            ListItem item = new ListItem();
            item.Text = "选择一个数据表";

            this.DbTableDownList2.DataSource = listtables2;
            this.DbTableDownList2.DataTextField = "TABLENAME";
            this.DbTableDownList2.DataValueField = "TABLE_NAME";
            this.DbTableDownList2.DataBind();
            this.DbTableDownList2.Items.Insert(0, item);
            dbConnectionString2 = Conection2;
        }
        /// <summary>
        /// 下拉选择步骤执行
        /// </summary>
        protected void Wizard1_FinishButtonClick()
        {
            //if (WizardStep1.Visible == true)
            //{ 
            Hashtable tablelist = new Hashtable();
            tablelist.Add(this.dataname, this.ConnectionString);
            tablelist.Add(this.dataname2, this.PlugConnectionString);

            if (RBLType.SelectedValue == "4")
            {
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator3.Visible = true;
            }
            else
            {
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator3.Visible = false;
            }

            if (string.IsNullOrEmpty(this.Request.QueryString["LabelnName"]))
            {
                this.m_LabelName = "";
            }
            else
            {
                this.m_LabelName = this.Request.QueryString["LabelnName"];
            }

            if (this.m_LabelName != "")
            {
                M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);

                string LabelTable = labelInfo.LabelTable;
                LabelTable = LabelTable.Replace("{table1}.dbo.", this.dataname + ".dbo.");
                LabelTable = LabelTable.Replace("{table2}.dbo.", this.dataname2 + ".dbo.");
                this.TxtSqlTable.Text = LabelTable;

                string LabelField = labelInfo.LabelField;
                LabelField = LabelField.Replace("{table1}.dbo.", this.dataname + ".dbo.");
                LabelField = LabelField.Replace("{table2}.dbo.", this.dataname2 + ".dbo.");
                this.TxtSqlField.Text = LabelField;


                string LabelWhere = labelInfo.LabelWhere;
                LabelWhere = LabelWhere.Replace("{table1}.dbo.", this.dataname + ".dbo.");
                LabelWhere = LabelWhere.Replace("{table2}.dbo.", this.dataname2 + ".dbo.");
                this.TxtSqlWhere.Text = LabelWhere;

                string LabelOrder = labelInfo.LabelOrder;
                LabelOrder = LabelOrder.Replace("{table1}.dbo.", this.dataname + ".dbo.");
                LabelOrder = LabelOrder.Replace("{table2}.dbo.", this.dataname2 + ".dbo.");
                this.TxtOrder.Text = LabelOrder;

                //this.textContent.Text = FileSystemObject.ReadFile(this.FilePath);

                this.textContent.Text = labelInfo.Content;
                this.TextBox1.Text = labelInfo.LabelCount;
                this.RBLType.SelectedValue = labelInfo.LableType.ToString();

                if (labelInfo.LabelField != "")
                {
                    SetLabelColumn(labelInfo.LabelField);
                }

                string labeltable = labelInfo.LabelTable;
                string table2 = "";
                string labelfriest = "";
                if (labeltable.IndexOf(" ") > -1)
                {
                    string[] labelarr = labeltable.Split(new string[] { " " }, StringSplitOptions.None);
                    if (labelarr.Length > 0)
                    {
                        labelfriest = labelarr[0];//不存在点

                        if (labelfriest.IndexOf(".") == -1)
                        {
                            labelfriest = dataname + ".dbo." + labelfriest;
                        }

                        if (labeltable.IndexOf(" join ") > -1)
                        {
                            string[] labeljoin = labeltable.Split(new string[] { "join" }, StringSplitOptions.None);
                            if (labeljoin[1].IndexOf(" on ") > -1)
                            {
                                string[] joinlabel = labeljoin[1].Split(new string[] { " on " }, StringSplitOptions.None);
                                table2 = joinlabel[0];//不存在点
                                if (table2.IndexOf(".") == -1)
                                {
                                    table2 = dataname + ".dbo." + table2.Trim();
                                }
                            }
                        }
                        if (labelfriest.IndexOf("{table1}") > -1)
                        {
                            DatabaseList.SelectedIndex = 0;
                        }

                        if (labelfriest.IndexOf("{table2}") > -1)
                        {
                            DatabaseList.SelectedIndex = 1;
                        }

                        if (table2.IndexOf("{table1}") > -1)
                        {
                            DatabaseList2.SelectedIndex = 0;
                        }

                        if (table2.IndexOf("{table2}") > -1)
                        {
                            DatabaseList2.SelectedIndex = 1;
                        }

                        changedbtabledownlist();

                        labelfriest = labelfriest.Replace("{table1}.dbo.", this.dataname + ".dbo.");
                        labelfriest = labelfriest.Replace("{table2}.dbo.", this.dataname2 + ".dbo.");

                        table2 = table2.Replace("{table1}.dbo.", dataname + ".dbo.").Trim();
                        table2 = table2.Replace("{table2}.dbo.", dataname2 + ".dbo.").Trim();
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "script2", "selectvalue('" + labelfriest + "','" + table2 + "');", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script2", "<script>selectvalue('" + labelfriest + "','" + table2 + "');</script>");
                    }
                }
                else
                {
                    if (labeltable != "")
                    {
                        if (labeltable.IndexOf(".") == -1)
                        {
                            labeltable = dataname + ".dbo." + labeltable;
                        }

                        if (labeltable.IndexOf("{table1}") > -1)
                        {
                            DatabaseList.SelectedIndex = 0;
                        }
                        if (labeltable.IndexOf("{table2}") > -1)
                        {
                            DatabaseList.SelectedIndex = 1;
                        }

                        changedbtabledownlist();

                        labeltable = labeltable.Replace("{table1}.dbo.", dataname + ".dbo.");
                        labeltable = labeltable.Replace("{table2}.dbo.", dataname2 + ".dbo.");

                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "script2", "selectvalue('" + labeltable + "','');", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script2", "<script>selectvalue('" + labeltable + "','');</script>");
                    }
                }
            }
            else
            {
                changedbtabledownlist();
                this.HdnlabelID.Value = "0";
            }

            if (this.Modelvalue.Text == "")
            {
                this.Modelvalue.Text = "0";
            }


            if (m_LabelName != "")
            {
                M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);

                if (labelInfo.LabelID > 0)
                {
                    this.addroot.SelectedValue = labelInfo.addroot;
                    this.falsecontent.Text = labelInfo.FalseContent;
                    this.Modeltypeinfo.Text = labelInfo.Modeltypeinfo;
                    this.Modelvalue.Text = labelInfo.Modelvalue;
                    this.setroot.SelectedValue = labelInfo.setroot;
                    this.Valueroot.Text = labelInfo.Valueroot;
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

            if (boolmodel.Checked == true)
            {
                this.isbool.Visible = true;
                //this.RequiredFieldValidator2.Enabled = true;
                this.Label8.Visible = true;
                this.s1.Visible = true;
                this.s2.Visible = true;

                switch (Modeltypeinfo.SelectedValue)
                {
                    case "计数判断":
                        this.addroot.Visible = true;
                        this.Valueroot.Visible = false;
                        this.setroot.Visible = true;
                        this.Modelvalue.Visible = true;
                        this.Label3.Visible = true;
                        if (this.addroot.SelectedValue == "循环计算")
                        {
                            this.Label3.Text = "计数器达到条件将自动清零,仅限包含<font color=blue>循环标签</font>有效";
                        }
                        else if (this.addroot.SelectedValue == "一直累加,仅限包含<font color=blue>循环标签</font>有效")
                        {
                            this.Label3.Text = "计数器一直累加";
                        }
                        //this.RequiredFieldValidator2.Enabled = true;
                        break;
                    case "用户登陆判断":
                        this.addroot.Visible = false;
                        this.Valueroot.Visible = false;
                        this.setroot.Visible = false;
                        this.Modelvalue.Visible = false;
                        //this.RequiredFieldValidator2.Enabled = false;
                        this.Label3.Text = "判断用户是否登陆";
                        break;
                    case "参数判断":
                        this.addroot.Visible = false;
                        this.Valueroot.Visible = true;
                        this.setroot.Visible = true;
                        this.Modelvalue.Visible = true;
                        this.Label3.Text = "判断参数是否满足条件";
                        //this.RequiredFieldValidator2.Enabled = true;
                        break;
                }
            }
            else
            {
                this.Label8.Visible = false;
                this.s1.Visible = false;
                this.s2.Visible = false;
                this.isbool.Visible = false;
            }
            //}
        }
        ///// <summary>
        ///// 第一步下拉列表事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int index = DataConverter.CLng(DropDownList1.SelectedValue);
        //    this.DropDownList1.SelectedIndex = index - 1;
        //    this.DropDownList2.SelectedIndex = index - 1;
        //    this.DropDownList3.SelectedIndex = index - 1;
        //    this.DropDownList4.SelectedIndex = index - 1;
        //    WizardStepBase step = Wizard1.WizardSteps[index - 1];
        //    //if ((index - 1) == 1) { Wizard1_FinishButtonClick(); }
        //    Wizard1.MoveTo(step);
        //}
        ///// <summary>
        ///// 第二步下拉列表事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int index = DataConverter.CLng(DropDownList2.SelectedValue);
        //    this.DropDownList1.SelectedIndex = index - 1;
        //    this.DropDownList2.SelectedIndex = index - 1;
        //    this.DropDownList3.SelectedIndex = index - 1;
        //    this.DropDownList4.SelectedIndex = index - 1;
        //    WizardStepBase step = Wizard1.WizardSteps[index - 1];
        //    //if ((index - 1) == 1) { Wizard1_FinishButtonClick(); }
        //    Wizard1.MoveTo(step);
        //}
        ///// <summary>
        ///// 第三步下拉列表事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int index = DataConverter.CLng(DropDownList3.SelectedValue);
        //    this.DropDownList1.SelectedIndex = index - 1;
        //    this.DropDownList2.SelectedIndex = index - 1;
        //    this.DropDownList3.SelectedIndex = index - 1;
        //    this.DropDownList4.SelectedIndex = index - 1;
        //    //if ((index - 1) == 1) { Wizard1_FinishButtonClick(); }
        //    WizardStepBase step = Wizard1.WizardSteps[index - 1];
        //    Wizard1.MoveTo(step);
        //}
        ///// <summary>
        ///// 第四步下拉列表事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int index = DataConverter.CLng(DropDownList4.SelectedValue);
        //    this.DropDownList1.SelectedIndex = index - 1;
        //    this.DropDownList2.SelectedIndex = index - 1;
        //    this.DropDownList3.SelectedIndex = index - 1;
        //    this.DropDownList4.SelectedIndex = index - 1;
        //    WizardStepBase step = Wizard1.WizardSteps[index - 1];
        //    //if ((index - 1) == 1) { Wizard1_FinishButtonClick(); }
        //    Wizard1.MoveTo(step);
        //}
        ///// <summary>
        ///// 下一步事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void StartNextButton_Click(object sender, EventArgs e)
        //{
        //    if (Wizard1.ActiveStepIndex == 3)
        //    {
        //        WizardStepBase step = Wizard1.WizardSteps[2];
        //        //if ((index - 1) == 1) { Wizard1_FinishButtonClick(); }
        //        Wizard1.MoveTo(step);
        //    }
        //    else
        //    {
        //        this.DropDownList1.SelectedIndex = Wizard1.ActiveStepIndex + 1;
        //        this.DropDownList2.SelectedIndex = Wizard1.ActiveStepIndex + 1;
        //        this.DropDownList3.SelectedIndex = Wizard1.ActiveStepIndex + 1;
        //        this.DropDownList4.SelectedIndex = Wizard1.ActiveStepIndex + 1;
        //    }
        //    //if ((Wizard1.ActiveStepIndex + 1) == 1) { Wizard1_FinishButtonClick(); }
        //}
        ///// <summary>
        ///// 上一步事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void PreviousButtonStep3_Click(object sender, EventArgs e)
        //{
        //    this.DropDownList1.SelectedIndex = Wizard1.ActiveStepIndex - 1;
        //    this.DropDownList2.SelectedIndex = Wizard1.ActiveStepIndex - 1;
        //    this.DropDownList3.SelectedIndex = Wizard1.ActiveStepIndex - 1;
        //    this.DropDownList4.SelectedIndex = Wizard1.ActiveStepIndex - 1;
        //    //if ((Wizard1.ActiveStepIndex - 1) == 1) { Wizard1_FinishButtonClick(); }
        //}
    }
}