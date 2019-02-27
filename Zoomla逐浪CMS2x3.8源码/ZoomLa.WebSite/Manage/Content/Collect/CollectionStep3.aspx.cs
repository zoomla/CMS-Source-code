namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Data;

    public partial class CollectionStep3 : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        private B_CollectionItem bc = new B_CollectionItem();
        protected B_ModelField bfield = new B_ModelField();
        protected string title = "添加设置采集第三步";
        protected string type = "添加采集项目";
        private int ItemID
        {
            get
            {
                if (ViewState["itemid"] == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(ViewState["itemid"].ToString());
                }
            }
            set { ViewState["itemid"] = value; }
        }
        private string InfoPageSettings
        {
            get
            {
                if (ViewState["pageset"] != null)
                    return ViewState["pageset"].ToString();
                else
                    return "";
            }
            set { ViewState["pageset"] = value; }
        }
        private string Action
        {
            get { return ViewState["action"].ToString(); }
            set { ViewState["action"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Action"] != null && Request.QueryString["ItemId"] != null)
            {
                Action = Request.QueryString["Action"].ToString();
                ItemID = DataConverter.CLng(Request.QueryString["ItemID"].ToString());
                if (Request.QueryString["Action"].ToString() == "Modify")
                {
                    title = "<a title=\"采集项目设置\" href=\"CollectionStep1.aspx?Action=Modify&ItemId=" + ItemID + " \">采集项目设置</a> >> <a title=\"列表页采集设置\" href=\"CollectionStep2.aspx?Action=Modify&amp;ItemID=" + ItemID + "\">列表页采集设置</a> >> <span style='color:red;'>内容页采集设置</span>";
                    type = "内容页采集设置";
                }

                M_CollectionItem mc = bc.GetSelect(ItemID);
                lblItemName.Text = mc.ItemName;
                InfoPageSettings = mc.InfoPageSettings;
                if (!IsPostBack)
                {
                    DataTable dt = bfield.GetCollFieldList();
                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                    DataList2.DataSource = bfield.GetModelFieldList(mc.ModeID);
                    DataList2.DataBind();
                    Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li><a href='CollectionManage.aspx'>项目管理</a></li><li class='active'>添加设置采集第三步</li>");
                }
            }
        }
        protected string GetHtml(string Alias, string Name, string Type, string Content, string Description, string ModelID)
        {
            bool b = true;
            string htmlstr = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td style=\"width: 15%\" >" + Alias + "</td>";
            string sd = "";
            if (Type == "OptionType" || Type == "ListBoxType" || Type == "ColorType")
            {
                sd = "disabled=\"disabled\"";
            }
            DataSet ds = function.XmlToTable(InfoPageSettings);//规则为空的情况下报错?
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    //是否是当前字段设置的XML节点
                    if (dt.TableName == Name)
                    {
                        b = false;
                        //是否是使用默认值
                        if (dt.Columns[0].ColumnName == Name + "_Default")
                        {
                            htmlstr += "<td style=\"width: 150px\"><label><input id=\"rd_" + Name + "_1\" type=\"radio\" name=\"list_" + Name + "\" value=\"1\" checked=\"checked\"/>使用字段默认值</label></td>";
                        }
                        else
                        {
                            htmlstr += "<td style=\"width: 150px\"><label><input id=\"rd_" + Name + "_1\" type=\"radio\" name=\"list_" + Name + "\" value=\"1\" />使用字段默认值</label></td>";
                        }
                        //是否是指定值
                        if (dt.Columns[0].ColumnName == Name + "_Appoint")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DataTable dtx = new DataTable();
                                dtx.Columns.Add(new DataColumn(Name, System.Type.GetType("System.String")));
                                DataRow dr2 = dtx.NewRow();
                                dr2["" + Name + ""] = dr[Name + "_Appoint"].ToString();
                                htmlstr += "<td style=\"width: 100px\"><label><input id=\"rd_" + Name + "_2\" type=\"radio\" name=\"list_" + Name + "\"  checked=\"checked\" value=\"2\" />使用指定值</label></td><td>" + bc.ShowStyleField(Alias, Name, Type, Content, Description, int.Parse(ModelID), dr2) + "</td>";
                            }
                        }
                        else
                        {
                            htmlstr += "<td style=\"width: 100px\"><label><input id=\"rd_" + Name + "_2\" type=\"radio\" name=\"list_" + Name + "\"  value=\"2\" />使用指定值</label></td><td>" + bc.ShowStyleField(Alias, Name, Type, Content, Description, int.Parse(ModelID), null) + "</td>";
                        }
                        //是否是使用规则
                        if (dt.Columns[0].ColumnName == Name + "_Id")
                        {
                            foreach (DataTable dtx in ds.Tables)
                            {
                                if (dtx.TableName != Name + "_CollConfig") continue;
                                foreach (DataRow dr in dtx.Rows)
                                {
                                    htmlstr += "<td style=\"width: 100px\"><input id=\"hd_s_" + Name + "\" name=\"hd_s_" + Name + "\" type=\"hidden\" value='" + dr["FieldStart"] + "' /><input id=\"rd_" + Name + "_3\" type=\"radio\" name=\"list_" + Name + "\" checked=\"checked\" value=\"3\" />使用采集规则</td><td style=\"width: 100px\"><input id=\"butConfig1\" type=\"button\"  class=\"btn btn-primary\"  value=\"设置采集规则\" onclick=\"showpage('" + ItemID + "','" + Name + "','" + Server.UrlEncode(Alias) + "');\" /></td>";
                                }
                            }
                        }
                        else
                        {
                            htmlstr += "<td style=\"width: 100px\"><input id=\"hd_s_" + Name + "\" name=\"hd_s_" + Name + "\" type=\"hidden\" /><input id=\"rd_" + Name + "_3\" type=\"radio\" name=\"list_" + Name + "\" value=\"3\" " + sd + " />使用采集规则</td><td style=\"width: 100px\"><input id=\"butConfig1\" type=\"button\"   class=\"btn btn-primary\"  value=\"设置采集规则\"  " + sd + " onclick=\"showpage('" + ItemID + "','" + Name + "','" + Server.UrlEncode(Alias) + "');\" /></td>";
                        }
                    }
                }
            }
            if (b)
            {
                htmlstr += "<td style=\"width: 150px\"><label><input id=\"rd_" + Name + "_1\" type=\"radio\" name=\"list_" + Name + "\" value=\"1\" checked=\"checked\"/>使用字段默认值</label></td>";
                htmlstr += "<td style=\"width: 100px\"><label><input id=\"rd_" + Name + "_2\" type=\"radio\" name=\"list_" + Name + "\"  value=\"2\" />使用指定值</label></td><td>" + bc.ShowStyleField(Alias, Name, Type, Content, Description, int.Parse(ModelID), null) + "</td>";
                htmlstr += "<td style=\"width: 100px\"><input id=\"hd_s_" + Name + "\" name=\"hd_s_" + Name + "\" type=\"hidden\" /><input id=\"hd_e_" + Name + "\" name=\"hd_e_" + Name + "\" type=\"hidden\"  /><input id=\"hd_p_" + Name + "\" name=\"hd_p_" + Name + "\" type=\"hidden\"  /><input id=\"rd_" + Name + "_3\" type=\"radio\" name=\"list_" + Name + "\" value=\"3\" " + sd + " />使用采集规则</td><td style=\"width: 100px\"><input id=\"butConfig1\" type=\"button\"   class=\"btn btn-primary\"  value=\"设置采集规则\"  " + sd + " onclick=\"showpage('" + ItemID + "','" + Name + "','" + Server.UrlEncode(Alias) + "');\" /></td>";
            }
            htmlstr += "</tr></table>";
            return htmlstr;
        }
        //上一步
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CollectionStep2.aspx?Action=" + Action + "&itemid=" + ItemID);
        }
        //保存
        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "";
            M_CollectionItem mc = bc.GetSelect(ItemID);
            DataTable dt = bfield.GetCollFieldList();
            str = SetPageSet(dt);
            str += SetPageSet(bfield.GetModelFieldList(mc.ModeID));
            mc.Switch = 2;
            mc.InfoPageSettings = str;
            bc.GetUpdate(mc);
            function.WriteSuccessMsg("采集项目已配置成功", "CollectionManage.aspx");
        }
        private string SetPageSet(DataTable dt)
        {
            string xml = "";
            foreach (DataRow dr in dt.Rows)
            {
                xml += "<" + dr["FieldName"].ToString() + ">";
                string FieldNamevalue = "";//
                if (Request.Form["txt_" + dr["FieldName"]] != null)
                {
                    FieldNamevalue = Request.Form["txt_" + dr["FieldName"].ToString()];

                    if (FieldNamevalue != "")
                    {
                        FieldNamevalue = FieldNamevalue.Replace("<", "&lt;");
                        FieldNamevalue = FieldNamevalue.Replace(">", "&gt;");
                        FieldNamevalue = FieldNamevalue.Replace("&", "&amp;");
                    }
                }
                switch (Request.Form["list_" + dr["FieldName"].ToString()])
                {
                    case "1":
                        xml += "<" + dr["FieldName"].ToString() + "_Default></" + dr["FieldName"].ToString() + "_Default>";
                        break;
                    case "2":
                        xml += "<" + dr["FieldName"].ToString() + "_Appoint>" + FieldNamevalue + "</" + dr["FieldName"].ToString() + "_Appoint>";
                        break;
                    case "3":
                        xml += "<" + dr["FieldName"].ToString() + "_CollConfig>";
                        string s = Request.Form["hd_s_" + dr["FieldName"].ToString()];
                        xml += "<FieldStart>" + s + "</FieldStart>";
                        xml += "</" + dr["FieldName"].ToString() + "_CollConfig>";
                        break;
                }
                xml += "</" + dr["FieldName"].ToString() + ">";
            }
            return xml;
        }
    }
}