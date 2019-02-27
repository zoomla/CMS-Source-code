using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;

public partial class User_AddConstPassen : System.Web.UI.Page
{ 
    protected B_User ull = new B_User();
    private B_Client_Enterprise bce = new B_Client_Enterprise();
    private B_Client_Basic bcb = new B_Client_Basic();
    private B_Client_Penson bcp = new B_Client_Penson();
    private GetDSData ds = new GetDSData();
    private XmlDocument myDoc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //区域
            DataTable Area = GetTableList("Client_Area");
            DropArea.DataSource = Area;
            DropArea.DataValueField = "sort";
            DropArea.DataTextField = "content";
            DropArea.DataBind();
            //行业
            DataTable Calling = GetTableList("Client_Calling");
            DropClientField.DataSource = Calling;
            DropClientField.DataValueField = "sort";
            DropClientField.DataTextField = "content";
            DropClientField.DataBind();
            //价值评估
            DataTable Value = GetTableList("Client_Value");
            DropValueLevel.DataSource = Value;
            DropValueLevel.DataValueField = "sort";
            DropValueLevel.DataTextField = "content";
            DropValueLevel.DataBind();
            //信用等级
            DataTable Credit = GetTableList("Client_Credit");
            DropCreditLevel.DataSource = Credit;
            DropCreditLevel.DataValueField = "sort";
            DropCreditLevel.DataTextField = "content";
            DropCreditLevel.DataBind();
            //重要程度
            DataTable Importance = GetTableList("Client_Importance");
            DropImportance.DataSource = Importance;
            DropImportance.DataValueField = "sort";
            DropImportance.DataTextField = "content";
            DropImportance.DataBind();
            //关系等级
            DataTable Connection = GetTableList("Client_Connection");
            DropConnectionLevel.DataSource = Connection;
            DropConnectionLevel.DataValueField = "sort";
            DropConnectionLevel.DataTextField = "content";
            DropConnectionLevel.DataBind();
            //客户来源
            DataTable Source = GetTableList("Client_Source");
            DropSourceType.DataSource = Source;
            DropSourceType.DataValueField = "sort";
            DropSourceType.DataTextField = "content";
            DropSourceType.DataBind();
            //阶段
            DataTable Phase = GetTableList("Client_Phase");
            DropPhaseType.DataSource = Phase;
            DropPhaseType.DataValueField = "sort";
            DropPhaseType.DataTextField = "content";
            DropPhaseType.DataBind();
            //客户组别
            DataTable Group = GetTableList("Client_Group");
            DropGroupID.DataSource = Group;
            DropGroupID.DataValueField = "sort";
            DropGroupID.DataTextField = "content";
            DropGroupID.DataBind();

            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "edit")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                M_Client_Basic binfo = bcb.GetSelect(id);
                this.Label1.Text = "修改个人用户";
 
                //------附加字段修改赋值
              
                this.TxtClientName.Value = binfo.P_name;
                this.TxtClientNum.Text = binfo.Code;
                this.TxtShortedForm.Value = binfo.P_alias; 
                //this.DropArea.SelectedItem.Text = binfo.Client_Area;
                //this.DropClientField.SelectedValue = binfo.Client_Calling;
                //this.DropValueLevel.SelectedItem.Text = binfo.Client_Value;
                //this.DropCreditLevel.SelectedItem.Text = binfo.Client_Credit;
                //this.DropImportance.SelectedItem.Text = binfo.Client_Importance;
                //this.DropConnectionLevel.SelectedItem.Text = binfo.Client_Connection;
                //this.DropSourceType.SelectedItem.Text = binfo.Client_Source;
                //this.DropPhaseType.SelectedItem.Text = binfo.Client_Phase;
                //this.DropGroupID.SelectedItem.Text = binfo.Client_Group;
 
                //binfo.Add_Name = ull.GetLogin().UserName;
                binfo.Add_Date = DateTime.Today;

                string scriptname = "";
                if (binfo.Client_Type == "1")
                {
                    DataTable tables = bce.SelByCode(binfo.Code);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int flowid = DataConverter.CLng(tables.Rows[0]["Flow"].ToString());
                        M_Client_Enterprise enterprise = bce.GetSelect(flowid); 
                        this.TxtClientNum.Text = enterprise.Code; 
                        this.TxtAddress.Value = enterprise.Message_Address;
                        this.TxtZipCodeW.Value = enterprise.ZipCodeW;// //enterprise.ZipCodeW = Request.Form["TxtZipCodeW"];
                        this.TxtPhone.Value = enterprise.Phone;
                        Adrees_Hid.Value = enterprise.province + "," + enterprise.city;
                        this.country.Value = enterprise.country; 
                        scriptname = "document.getElementById(\"country\").value = \"" + enterprise.country + "\";";
                        scriptname += "document.getElementById(\"province\").value = \"" + enterprise.province + "\";province_onchange(province,city);";
                        scriptname += "document.getElementById(\"city\").value = \"" + enterprise.city + "\";";
                    }
                }
                else if (binfo.Client_Type == "0")
                {
                    DataTable tables = bcp.SelByCode(binfo.Code);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int flowid = DataConverter.CLng(tables.Rows[0]["Flow"].ToString());
                        M_Client_Penson penson = bcp.GetSelect(flowid);
                        this.TxtPhone.Value = penson.Telephone;
                        this.TxtClientNum.Text = penson.Code;
                        this.TxtAddress.Value = penson.Message_Address;
                        this.TxtZipCodeW.Value = penson.ZipCodeW;
                        this.TxtIDCard.Value= penson.Id_Code;
                        this.country.Value = penson.country; 
                        Adrees_Hid.Value = penson.province + "," + penson.city;

                        scriptname = "document.getElementById(\"country\").value = \"" + penson.country + "\";";
                        scriptname += "document.getElementById(\"province\").value = \"" + penson.province + "\";province_onchange(province,city);";
                        scriptname += "document.getElementById(\"city\").value = \"" + penson.city + "\";";
                        //province
                        //city
                    }
                }


                if (binfo.Client_Type == "1")
                {
                    this.RadlClientType_0.Checked = true;
                    function.Script(this, "window.onload=function(){initialize();isPerson('Enterprise_Add');" + scriptname + "}");
                }
                else
                {
                    this.RadlClientType_1.Checked = true;
                    function.Script(this, "window.onload=function(){initialize();isPerson('Person_Add');" + scriptname + "}");
                }
            }
            else
                this.TxtClientNum.Text = function.GetFileName();
        }
    }
    protected void ctl00_CphContent_BtnSave_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(Request.QueryString["id"]);

        M_Client_Basic basic = new M_Client_Basic();
        if (id > 0)
        {
            basic = bcb.GetSelect(id);
        }

        M_Client_Enterprise enterprise = new M_Client_Enterprise();
        M_Client_Penson penson = new M_Client_Penson();
        string node_id = Request.QueryString["FieldName"].ToString();
        basic.P_name = this.TxtClientName.Value;
        basic.Code = this.TxtClientNum.Text;
        basic.P_alias = this.TxtShortedForm.Value; 
        //basic.Client_Area = this.DropArea.SelectedItem.Text.ToString().Trim();
        //basic.Client_Calling = this.DropClientField.SelectedValue;
        //basic.Client_Value = this.DropValueLevel.SelectedItem.Text.ToString().Trim();
        //basic.Client_Credit = this.DropCreditLevel.SelectedItem.Text.ToString().Trim();
        //basic.Client_Importance = this.DropImportance.SelectedItem.Text.ToString().Trim();
        //basic.Client_Connection = this.DropConnectionLevel.SelectedItem.Text.ToString().Trim();
        //basic.Client_Source = this.DropSourceType.SelectedItem.Text.ToString().Trim();
        //basic.Client_Phase = this.DropPhaseType.SelectedItem.Text.ToString().Trim();
        //basic.Client_Group = this.DropGroupID.SelectedItem.Text.ToString().Trim();
        basic.FPManID = ull.GetLogin().UserID;
        if (this.RadlClientType_0.Checked)//企业用户
        {
            DataTable tablelist = bce.SelByCode(basic.Code);
            if (tablelist != null && tablelist.Rows.Count > 0)
            {
                enterprise = bce.GetEnterpriseByCode(basic.Code);
            }

            basic.Client_Type = "1";
            node_id = "Enterprise_Add"; 
            enterprise.Code = this.TxtClientNum.Text;

            enterprise.Message_Address = TxtAddress.Value.ToString();// Request.Form["country"].ToString() + "|" + Request.Form["province"] + "|" + Request.Form["city"];//通讯地址
            enterprise.country = Request.Form["country"];
            enterprise.province = Request.Form["province"];
            enterprise.city = Request.Form["city"];
            enterprise.ZipCodeW = TxtZipCodeW.Value.ToString();
            enterprise.Phone = TxtPhone.Value.ToString();
            enterprise.Add_Date = DateTime.Now;
            if (tablelist != null && tablelist.Rows.Count > 0)
            {
                bce.GetUpdate(enterprise);
            }
            else
            {
                bce.GetInsert(enterprise);
            }
        }
        else
        {//个人用户
            DataTable tablelist = bcp.SelByCode(basic.Code);
            if (tablelist != null && tablelist.Rows.Count > 0)
            {
                penson = bcp.GetPenSonByCode(basic.Code);
            }

            basic.Client_Type = "0";
            node_id = "Person_Add";
            penson.Code = this.TxtClientNum.Text;
            penson.Message_Address = TxtAddress.Value.ToString();
            penson.ZipCodeW = TxtZipCodeW.Value.ToString();
            penson.Telephone = TxtPhone.Value.ToString();
            penson.Id_Code = TxtIDCard.Value.ToString();
            penson.country = Request.Form["country"];
            penson.province = Request.Form["province"];
            penson.city = Request.Form["city"];

            if (tablelist != null && tablelist.Rows.Count > 0)
            {
                bcp.GetUpdate(penson);
            }
            else
            {
                bcp.GetInsert(penson);
            }
        }
        //basic.Add_Name = ull.GetLogin().UserName;

        basic.Add_Date = DateTime.Today;
        if (id > 0)
        {
            bcb.GetUpdate(basic);
            //-----更新
            DataTable addDT = ds.GetAllCrmOption();
            //-----附加字段完成
            function.WriteSuccessMsg("修改成功!", "ConstPassen.aspx");
        }
        else
        {
            bcb.insert(basic);
            //-----插入附加字段使用form取附加字段的值
            DataTable addDT = ds.GetAllCrmOption();
            //-----附加字段完成
            function.WriteSuccessMsg("添加成功！", "ConstPassen.aspx");
        }
    }
    public User_AddConstPassen()
    {
        //构造函数
        myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/Config/CRM_Dictionary.xml"));
    }
    public static DataTable MakeTable()
    {
        // Create a DataTable. 
        DataTable table = new DataTable();
        table.Columns.Add("sort", typeof(System.String));
        table.Columns.Add("default_", typeof(System.Boolean));
        table.Columns.Add("enable", typeof(System.Boolean));
        table.Columns.Add("content", typeof(System.String));
        return table;
    }
    public DataTable GetTableList(string node_id)
    {
        XmlNodeList list = myDoc.SelectNodes("/CRM_Dictionary/" + node_id);
        XmlElement xmle = null;
        DataTable dt = MakeTable();
        for (int i = 0; i < list.Count; i++)
        {
            xmle = (XmlElement)list[i];
            DataRow dr = dt.NewRow();
            dr["sort"] = xmle.SelectSingleNode("sort").InnerText;
            dr["default_"] = xmle.SelectSingleNode("default_").InnerText;
            dr["enable"] = xmle.SelectSingleNode("enable").InnerText;
            dr["content"] = xmle.SelectSingleNode("content").InnerText;
            if (dr["enable"].ToString() == "True")
            {
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
}
