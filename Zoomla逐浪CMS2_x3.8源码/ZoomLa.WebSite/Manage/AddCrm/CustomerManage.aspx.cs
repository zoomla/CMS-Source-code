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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class manage_AddCRM_CustomerManage : CustomerPageAction
{
    private B_Client_Basic basicBll = new B_Client_Basic();
    private B_Client_Enterprise bce = new B_Client_Enterprise();
    private B_Client_Penson bcp = new B_Client_Penson();
    private B_User ull = new B_User();
    private XmlDocument myDoc;
    private GetDSData ds = new GetDSData();
    private B_Admin badmin = new B_Admin();
    B_User buser = new B_User();
    private M_CrmAuth crmModel = new M_CrmAuth();
    private DataTable authDT = new DataTable();//用来存角色表
    private B_CrmAuth crmBll = new B_CrmAuth();
    private B_ModelField fieldBll = new B_ModelField();
    private B_Model modelBll = new B_Model();
    public int CustID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public int ModelID { get { return DataConverter.CLng(Request.QueryString["modelid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            if (Request.Form["action"].Equals("getInfo"))
            {
                string fbMan = badmin.GetAdminLogin().AdminName;
                string date = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm");
                Response.Write("{\"date\":\"" + date + "\",\"fbman\":\"" + fbMan + "\"}");
            }
            Response.End();
        }

        if (!IsPostBack)
        {
            //---------权限限制
            M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
            FPMan.Text = info.AdminTrueName;
            authDT = crmBll.GetAuthTable(info.RoleList.Split(','));//
            if (string.IsNullOrEmpty(Request.QueryString["menu"]) && !crmBll.IsHasAuth(authDT, "AllowAddClient", info))//如果无添加新用户的权限
            {
                Response.Redirect("~/Prompt/NoPermissions.htm");
                Response.End();
            }
            ViewState["authDT"] = authDT;
            //if (!crmBll.IsHasAuth(authDT, "AssignFPMan", info))//如果无指定跟进人的权限，则不能改变跟进人
            //    FPMandp.Enabled = false;

            liTitle.Text = UC_BI();
            string node_id = Request.QueryString["FieldName"].ToString();
            if (node_id == "Client_All")
            {
                Response.Write("<script>javascript:document.title='客户组别'</script>");
                this.Label1.Text = "客户组别";
            }
            else if (node_id == "Phase_Trace")
            {
                Response.Write("<script>javascript:document.title='客户阶段'</script>");
                this.Label1.Text = "客户阶段";
            }
            //FPMandp.DataSource = buser.Sel();
            //FPMandp.DataValueField = "UserID";
            //FPMandp.DataTextField = "UserName";
            //FPMandp.DataBind();



            ////区域
            //DataTable Area = GetTableList("Client_Area");
            //DropArea.DataSource = Area;
            //DropArea.DataValueField = "sort";
            //DropArea.DataTextField = "content";
            //DropArea.DataBind();
            ////行业
            //DataTable Calling = GetTableList("Client_Calling");
            //DropClientField.DataSource = Calling;
            //DropClientField.DataValueField = "sort";
            //DropClientField.DataTextField = "content";
            //DropClientField.DataBind();
            ////价值评估
            //DataTable Value = GetTableList("Client_Value");
            //DropValueLevel.DataSource = Value;
            //DropValueLevel.DataValueField = "sort";
            //DropValueLevel.DataTextField = "content";
            //DropValueLevel.DataBind();
            ////信用等级
            //DataTable Credit = GetTableList("Client_Credit");
            //DropCreditLevel.DataSource = Credit;
            //DropCreditLevel.DataValueField = "sort";
            //DropCreditLevel.DataTextField = "content";
            //DropCreditLevel.DataBind();
            ////重要程度
            //DataTable Importance = GetTableList("Client_Importance");
            //DropImportance.DataSource = Importance;
            //DropImportance.DataValueField = "sort";
            //DropImportance.DataTextField = "content";
            //DropImportance.DataBind();
            ////关系等级
            //DataTable Connection = GetTableList("Client_Connection");
            //DropConnectionLevel.DataSource = Connection;
            //DropConnectionLevel.DataValueField = "sort";
            //DropConnectionLevel.DataTextField = "content";
            //DropConnectionLevel.DataBind();
            ////客户来源
            //DataTable Source = GetTableList("Client_Source");
            //DropSourceType.DataSource = Source;
            //DropSourceType.DataValueField = "sort";
            //DropSourceType.DataTextField = "content";
            //DropSourceType.DataBind();
            ////阶段
            //DataTable Phase = GetTableList("Client_Phase");
            //DropPhaseType.DataSource = Phase;
            //DropPhaseType.DataValueField = "sort";
            //DropPhaseType.DataTextField = "content";
            //DropPhaseType.DataBind();
            ////客户组别
            //DataTable Group = GetTableList("Client_Group");
            //DropGroupID.DataSource = Group;
            //DropGroupID.DataValueField = "sort";
            //DropGroupID.DataTextField = "content";
            //DropGroupID.DataBind();
            //行业地位
            DataTable Status = GetTableList("Co_Status");
            DropStatusInField.DataSource = Status;
            DropStatusInField.DataValueField = "sort";
            DropStatusInField.DataTextField = "content";
            DropStatusInField.DataBind();
            //公司规模
            DataTable Co_Size = GetTableList("Co_Size");
            DropCompanySize.DataSource = Co_Size;
            DropCompanySize.DataValueField = "sort";
            DropCompanySize.DataTextField = "content";
            DropCompanySize.DataBind();
            //经营状态
            DataTable Management = GetTableList("Co_Management");
            DropManagementForms.DataSource = Management;
            DropManagementForms.DataValueField = "sort";
            DropManagementForms.DataTextField = "content";
            DropManagementForms.DataBind();
            //学历
            DataTable Education = GetTableList("Linkman_Education");
            DropEducation.DataSource = Education;
            DropEducation.DataValueField = "sort";
            DropEducation.DataTextField = "content";
            DropEducation.DataBind();
            //月收入
            DataTable Income = GetTableList("Linkman_Income");
            DropIncome.DataSource = Income;
            DropIncome.DataValueField = "sort";
            DropIncome.DataTextField = "content";
            DropIncome.DataBind();            
            //----------附加字段处理
            //if (Request.QueryString["menu"] != "edit")
            //    htmlStr.Text = CreateHtml(ds.GetAllCrmOption());
            //-----------
            DataSet table = new DataSet();
            table.ReadXml(Server.MapPath("/Config/CRM_Dictionary.xml"));
            DataTable AreaTable = table.Tables["Client_Area"];
            DataTable StatusTable = table.Tables["Co_Status"];
            DataTable CallingTable = table.Tables["Client_Calling"];
            DataTable ValueTable = table.Tables["Client_Value"];
            DataTable CreditTable = table.Tables["Client_Credit"];
            DataTable ImportanceTable = table.Tables["Client_Importance"];
            DataTable ConnectionTable = table.Tables["Client_Connection"];
            DataTable SourceTable = table.Tables["Client_Source"];
            DataTable PhaseTable = table.Tables["Client_Phase"];
            DataTable GroupTable = table.Tables["Client_Group"];
            DataTable SizeTable = table.Tables["Co_Size"];//Co_Size
            DataTable ManagementTable = table.Tables["Co_Management"];
            DataTable EducationTable = table.Tables["Linkman_Education"];
            DataTable IncomeTable = table.Tables["Linkman_Income"];

            #region 定义选项默认值的变量及初始值
            string Areatxt = "";
            string Statustxt = "";
            string Callingtxt = "";
            string Valuetxt = "";
            string Credittxt = "";
            string Importancetxt = "";
            string Connectiontxt = "";
            string Sourcetxt = "";
            string Phasetxt = "";
            string Grouptxt = "";
            string Sizetxt = "";
            string Managementtxt = "";
            string Educationtxt = "";
            string Incometxt = "";
            #endregion

            #region 赋予选项的默认值
            if (AreaTable != null && AreaTable.Rows.Count > 0)
            {
                for (int o = 0; o < AreaTable.Rows.Count; o++)
                {
                    if (AreaTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Areatxt = AreaTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (StatusTable != null && StatusTable.Rows.Count > 0)
            {
                for (int o = 0; o < StatusTable.Rows.Count; o++)
                {
                    if (StatusTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Statustxt = StatusTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (CallingTable != null && CallingTable.Rows.Count > 0)
            {
                for (int o = 0; o < CallingTable.Rows.Count; o++)
                {
                    if (CallingTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Callingtxt = CallingTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (ValueTable != null && ValueTable.Rows.Count > 0)
            {
                for (int o = 0; o < ValueTable.Rows.Count; o++)
                {
                    if (ValueTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Valuetxt = ValueTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (CreditTable != null && CreditTable.Rows.Count > 0)
            {
                for (int o = 0; o < CreditTable.Rows.Count; o++)
                {
                    if (CreditTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Credittxt = CreditTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (ImportanceTable != null && ImportanceTable.Rows.Count > 0)
            {
                for (int o = 0; o < ImportanceTable.Rows.Count; o++)
                {
                    if (ImportanceTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Importancetxt = ImportanceTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (ConnectionTable != null && ConnectionTable.Rows.Count > 0)
            {
                for (int o = 0; o < ConnectionTable.Rows.Count; o++)
                {
                    if (ConnectionTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Connectiontxt = ConnectionTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (SourceTable != null && SourceTable.Rows.Count > 0)
            {
                for (int o = 0; o < SourceTable.Rows.Count; o++)
                {
                    if (SourceTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Sourcetxt = SourceTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (PhaseTable != null && PhaseTable.Rows.Count > 0)
            {
                for (int o = 0; o < PhaseTable.Rows.Count; o++)
                {
                    if (PhaseTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Phasetxt = PhaseTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (GroupTable != null && GroupTable.Rows.Count > 0)
            {
                for (int o = 0; o < GroupTable.Rows.Count; o++)
                {
                    if (GroupTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Grouptxt = GroupTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (SizeTable != null && SizeTable.Rows.Count > 0)
            {
                for (int o = 0; o < SizeTable.Rows.Count; o++)
                {
                    if (SizeTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Sizetxt = SizeTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            if (ManagementTable != null && ManagementTable.Rows.Count > 0)
            {
                for (int o = 0; o < ManagementTable.Rows.Count; o++)
                {
                    if (ManagementTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Managementtxt = ManagementTable.Rows[o]["sort"].ToString();
                    }
                }
            }
            if (EducationTable != null && EducationTable.Rows.Count > 0)
            {
                for (int o = 0; o < EducationTable.Rows.Count; o++)
                {
                    if (EducationTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Educationtxt = EducationTable.Rows[o]["sort"].ToString();
                    }
                }
            }
            if (IncomeTable != null && IncomeTable.Rows.Count > 0)
            {
                for (int o = 0; o < IncomeTable.Rows.Count; o++)
                {
                    if (IncomeTable.Rows[o]["default_"].ToString() == "True")
                    {
                        Incometxt = IncomeTable.Rows[o]["sort"].ToString();
                    }
                }
            }

            #endregion

            //DropArea.SelectedIndex = DataConverter.CLng(Areatxt);
            //DropStatusInField.SelectedIndex = DataConverter.CLng(Statustxt);
            //DropClientField.SelectedIndex = DataConverter.CLng(Callingtxt);
            //DropValueLevel.SelectedIndex = DataConverter.CLng(Valuetxt);
            //DropCreditLevel.SelectedIndex = DataConverter.CLng(Credittxt);
            //DropImportance.SelectedIndex = DataConverter.CLng(Importancetxt);
            //DropConnectionLevel.SelectedIndex = DataConverter.CLng(Connectiontxt);
            //DropSourceType.SelectedIndex = DataConverter.CLng(Sourcetxt);
            //DropPhaseType.SelectedIndex = DataConverter.CLng(Phasetxt);
            //DropGroupID.SelectedIndex = DataConverter.CLng(Grouptxt);
            DropCompanySize.SelectedIndex = DataConverter.CLng(Sizetxt);
            DropManagementForms.SelectedIndex = DataConverter.CLng(Managementtxt);
            DropEducation.SelectedIndex = DataConverter.CLng(Educationtxt);
            DropIncome.SelectedIndex = DataConverter.CLng(Incometxt);

            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "edit")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                //FPBtn.Visible = true;
                M_Client_Basic binfo = basicBll.GetSelect(id);
                this.Label1.Text = "修改个人用户";

                if (!crmBll.IsHasAuth(authDT, "AllCustomer", info))//为防止无权限，直接输客户ID进入该页面
                {
                    if (binfo.FPManID != badmin.GetAdminLogin().AdminId)
                    {
                        Response.Redirect("~/Prompt/NoPermissions.htm");
                        Response.End();
                    }
                }
                //htmlStr.Text = CreateHtml(ds.GetAllCrmOption(), selectedDT);
               // FPMandp.SelectedValue = binfo.FPManID.ToString();//跟进人
                User_Title.InnerHtml = binfo.P_name;
                this.TxtClientName.Value = binfo.P_name;
                this.TxtClientNum.Text = binfo.Code;
                this.TxtShortedForm.Value = binfo.P_alias;
                this.ClientSelect.Value = binfo.Client_Upper;
                //this.DropArea.SelectedItem.Text = binfo.Client_Area;
                //this.DropClientField.SelectedValue = binfo.Client_Calling;
                //this.DropValueLevel.SelectedItem.Text = binfo.Client_Value;
                //this.DropCreditLevel.SelectedItem.Text = binfo.Client_Credit;
                //this.DropImportance.SelectedItem.Text = binfo.Client_Importance;
                //this.DropConnectionLevel.SelectedItem.Text = binfo.Client_Connection;
                //this.DropSourceType.SelectedItem.Text = binfo.Client_Source;
                //this.DropPhaseType.SelectedItem.Text = binfo.Client_Phase;
                //this.DropGroupID.SelectedItem.Text = binfo.Client_Group;
                this.TxtRemark.Value = binfo.Title;

                //this.TxtCompany.Value = binfo.Unit_Name;
                //this.TxtDepartment.Value = binfo.Unit_Class;
                //this.TxtPosition.Value = binfo.Unit_Post;
                //this.TxtOperation.Value = binfo.Unit_Operation;
                //this.TxtCompanyAddress.Value = binfo.Unit_Address;
                //this.TxtTitle.Value = binfo.Unit_Title;
                //--模型字段处理
                //DataTable contentDt = basicBll.GetContent(modelBll.GetModelById(ModelID).TableName,binfo.ItemID);
                //ModelHtml.Text = fieldBll.InputallHtml(ModelID, 0, new ModelConfig()
                //{
                //    Source = ModelConfig.SType.Admin,
                //    ValueDT = contentDt
                //});
                //binfo.Add_Name = ull.GetLogin().UserName;
                binfo.Add_Date = DateTime.Today;
                binfo.Title = this.TxtRemark.Value;

                string scriptname = "";
                if (binfo.Client_Type == "1")
                {
                    DataTable tables = bce.SelByCode(binfo.Code);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int flowid = DataConverter.CLng(tables.Rows[0]["Flow"].ToString());
                        M_Client_Enterprise enterprise = bce.GetSelect(flowid);
                        this.TxtBankAccount.Value = enterprise.Bank_Code;
                        this.TxtBankOfDeposit.Value = enterprise.Bank_Open;
                        this.DropManagementForms.SelectedItem.Text = enterprise.Co_Management;
                        this.DropCompanySize.SelectedItem.Text = enterprise.Co_Size;
                        this.DropStatusInField.SelectedItem.Text = enterprise.Co_Status;
                        this.TxtClientNum.Text = enterprise.Code;
                        this.TxtTaxNum.Value = enterprise.Tax_Code;
                        this.TxtAnnualSales.Value = enterprise.Sales.ToString();
                        this.TxtRegisteredCapital.Value = enterprise.Reg_Capital.ToString();
                        this.TxtBusinessScope.Value = enterprise.Operation_Bound;
                        this.TxtHomepage1.Value = enterprise.Homepage;
                        this.TxtFax1.Value = enterprise.Fax_phone;
                        this.TxtPhone.Value = enterprise.Phone;
                        this.TxtAddress.Value = enterprise.Message_Address;
                        this.TxtZipCodeW.Value = enterprise.ZipCodeW;// //enterprise.ZipCodeW = Request.Form["TxtZipCodeW"];
                        Adrees_Hid.Value = enterprise.province + "," + enterprise.city;
                        this.country.Value = enterprise.country;
                    }
                }
                else if (binfo.Client_Type == "0")
                {
                    DataTable tables = bcp.SelByCode(binfo.Code);
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int flowid = DataConverter.CLng(tables.Rows[0]["Flow"].ToString());
                        M_Client_Penson penson = bcp.GetSelect(flowid);

                        this.TxtClientNum.Text = penson.Code;
                        this.sex.Value = penson.Sex;
                        this.Marriage.Value = penson.Marriage;
                        this.DpkBirthday.Value = penson.Birthday.ToString();
                        this.TxtIDCard.Value = penson.Id_Code;
                        this.TxtNativePlace.Value = penson.Native;
                        this.TxtNation.Value = penson.Nation;
                        this.DropEducation.SelectedItem.Text = penson.Education;
                        this.TxtGraduateFrom.Value = penson.Finish_School;
                        this.TxtInterestsOfLife.Value = penson.Life_Love;
                        this.TxtInterestsOfCulture.Value = penson.Cultrue_Love;
                        this.TxtInterestsOfOther.Value = penson.Other_Love;
                        this.TxtInterestsOfSport.Value = penson.Sport_Love;
                        this.TxtInterestsOfAmusement.Value = penson.FUN_Love;
                        this.DropIncome.SelectedItem.Text = penson.Income.ToString();
                        this.TxtFamily.Value = penson.Home_Circs;

                        this.TxtAim.Value = penson.Aim_Code;
                        this.TxtFax.Value = penson.Fax_phone;
                        this.TxtAddress.Value = penson.Message_Address;// Request.Form["country"].ToString() + "|" + Request.Form["province"] + "|" + Request.Form["city"]; //通讯地址
                        this.TxtOfficePhone.Value = penson.Work_Phone;
                        this.TxtHomePhone.Value = penson.Home_Phone;
                        this.TxtMobile.Value = penson.Telephone;
                        this.TxtPHS.Value = penson.Little_Smart;
                        this.TxtHomepage.Value = penson.Homepage;
                        this.TxtEmail.Value = penson.Email_Address;
                        this.TxtQQ.Value = penson.QQ_Code;
                        this.TxtMSN.Value = penson.MSN_Code;
                        this.TxtUC.Value = penson.UC_Code;
                        this.TxtYahoo.Value = penson.YaoHu_Code;
                        this.TxtICQ.Value = penson.ICQ_Code;
                        this.TxtZipCodeW.Value = penson.ZipCodeW;
                        Adrees_Hid.Value = penson.province + "," + penson.city;
                        this.country.Value = penson.country;

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
                    function.Script(this, "window.onload=function(){isPerson('Enterprise_Add');" + scriptname + "}");
                }
                else
                {
                    this.RadlClientType_1.Checked = true;
                    function.Script(this, "window.onload=function(){isPerson('Person_Add');" + scriptname + "}");
                }
            }
            else
            {
                ModelHtml.Text = fieldBll.InputallHtml(ModelID, 0, new ModelConfig()
                {
                    Source = ModelConfig.SType.Admin,
                });
                this.TxtClientNum.Text = function.GetFileName();
            }
            Call.SetBreadCrumb(Master, "<li>" + liTitle.Text + "</li><li><a href='CustomerList.aspx?usertype=0'>客户管理</a></li><li>" + Label1.Text + "</li>");
        }
    }
    //保存
    protected void ctl00_CphContent_BtnSave_Click(object sender, EventArgs e)
    {
        M_Client_Basic basic = new M_Client_Basic();
        if (CustID > 0)
        {
            basic = basicBll.GetSelect(CustID);
        }
        M_Client_Enterprise enterprise = new M_Client_Enterprise();
        M_Client_Penson penson = new M_Client_Penson();
        string node_id = Request.QueryString["FieldName"].ToString();
        basic.P_name = this.TxtClientName.Value;
        basic.Code = this.TxtClientNum.Text;
        basic.P_alias = this.TxtShortedForm.Value;
        basic.Client_Upper = this.ClientSelect.Value;
        //basic.Client_Area = this.DropArea.SelectedItem.Text.ToString().Trim();
        //basic.Client_Calling = this.DropClientField.SelectedValue;
        //basic.Client_Value = this.DropValueLevel.SelectedItem.Text.ToString().Trim();
        //basic.Client_Credit = this.DropCreditLevel.SelectedItem.Text.ToString().Trim();
        //basic.Client_Importance = this.DropImportance.SelectedItem.Text.ToString().Trim();
        //basic.Client_Connection = this.DropConnectionLevel.SelectedItem.Text.ToString().Trim();
        //basic.Client_Source = this.DropSourceType.SelectedItem.Text.ToString().Trim();
        //basic.Client_Phase = this.DropPhaseType.SelectedItem.Text.ToString().Trim();
        //basic.Client_Group = this.DropGroupID.SelectedItem.Text.ToString().Trim();
        basic.Title = this.TxtRemark.Value;
       // basic.FPManID = DataConverter.CLng(FPMandp.SelectedValue);
        if (this.RadlClientType_0.Checked)//企业用户
        {
            DataTable tablelist = bce.SelByCode(basic.Code);
            if (tablelist != null && tablelist.Rows.Count > 0)
            {
                enterprise = bce.GetEnterpriseByCode(basic.Code);
            }

            basic.Client_Type = "1";
            node_id = "Enterprise_Add";
            enterprise.Bank_Code = this.TxtBankAccount.Value;
            enterprise.Bank_Open = this.TxtBankOfDeposit.Value;
            enterprise.Co_Management = this.DropManagementForms.SelectedItem.Text.ToString().Trim();
            enterprise.Co_Size = this.DropCompanySize.SelectedItem.Text.ToString().Trim();
            enterprise.Co_Status = this.DropStatusInField.SelectedItem.Text.ToString().Trim();
            enterprise.Code = this.TxtClientNum.Text;
            enterprise.Tax_Code = this.TxtTaxNum.Value;
            enterprise.Sales = DataConverter.CDouble((this.TxtAnnualSales.Value == "") ? "0" : this.TxtAnnualSales.Value);
            enterprise.Reg_Capital = DataConverter.CDouble((this.TxtRegisteredCapital.Value == "") ? "0" : this.TxtAnnualSales.Value);
            enterprise.Operation_Bound = this.TxtBusinessScope.Value;
            enterprise.Homepage = this.TxtHomepage1.Value;
            enterprise.Fax_phone = this.TxtFax1.Value;
            enterprise.Phone = this.TxtPhone.Value;
            enterprise.Message_Address = Request.Form["TxtAddress"];// Request.Form["country"].ToString() + "|" + Request.Form["province"] + "|" + Request.Form["city"];//通讯地址
            enterprise.country = country.Value;
            enterprise.province = Request.Form["province"];
            enterprise.city = Request.Form["city"];
            enterprise.ZipCodeW = Request.Form["TxtZipCodeW"];
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
            penson.Sex = this.sex.Value;
            penson.Marriage = this.Marriage.Value;
            penson.Birthday = DataConverter.CDate(this.DpkBirthday.Value);
            penson.Id_Code = this.TxtIDCard.Value;
            penson.Native = this.TxtNativePlace.Value;
            penson.Nation = this.TxtNation.Value;
            penson.Education = this.DropEducation.SelectedItem.Text.ToString().Trim();
            penson.Finish_School = this.TxtGraduateFrom.Value;
            penson.Life_Love = this.TxtInterestsOfLife.Value;
            penson.Cultrue_Love = this.TxtInterestsOfCulture.Value;
            penson.Other_Love = this.TxtInterestsOfOther.Value;
            penson.Sport_Love = this.TxtInterestsOfSport.Value;
            penson.FUN_Love = this.TxtInterestsOfAmusement.Value;
            penson.Income = this.DropIncome.SelectedItem.Text.ToString();
            penson.Home_Circs = this.TxtFamily.Value;
            penson.Aim_Code = this.TxtAim.Value;
            penson.Fax_phone = this.TxtFax.Value;
            penson.Message_Address = Request.Form["TxtAddress"];// Request.Form["country"].ToString() + "|" + Request.Form["province"] + "|" + Request.Form["city"]; //通讯地址
            penson.Work_Phone = this.TxtOfficePhone.Value;
            penson.Home_Phone = this.TxtHomePhone.Value;
            penson.Telephone = this.TxtMobile.Value;
            penson.Little_Smart = this.TxtPHS.Value;
            penson.Homepage = this.TxtHomepage.Value;
            penson.Email_Address = this.TxtEmail.Value;
            penson.QQ_Code = this.TxtQQ.Value;
            penson.MSN_Code = this.TxtMSN.Value;
            penson.UC_Code = this.TxtUC.Value;
            penson.YaoHu_Code = this.TxtYahoo.Value;
            penson.ICQ_Code = this.TxtICQ.Value;
            penson.ZipCodeW = Request.Form["TxtZipCodeW"];
            penson.country = country.Value;
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
        //basic.Unit_Name = this.TxtCompany.Value;
        //basic.Unit_Class = this.TxtDepartment.Value;
        //basic.Unit_Post = this.TxtPosition.Value;
        //basic.Unit_Operation = this.TxtOperation.Value;
        //basic.Unit_Address = this.TxtCompanyAddress.Value;
        //basic.Unit_Title = this.TxtTitle.Value;
        //basic.Add_Name = ull.GetLogin().UserName;

        basic.Add_Date = DateTime.Today;
        basic.Title = this.TxtRemark.Value;
        int itemid= SaveModelData(basic.ItemID);
        basic.ItemID = itemid;
        if (CustID > 0)
        {
            basicBll.GetUpdate(basic);
            function.WriteSuccessMsg("修改成功!", "CustomerList.aspx?ModelID="+ModelID);
        }
        else
        {
            basicBll.insert(basic);
            function.WriteSuccessMsg("添加成功！", "CustomerList.aspx?modelid=" + ModelID);
        }
    }
    //添加模型数据
    public int SaveModelData(int itemid)
    {
        //客户模型操作
        Call commonCall = new Call();
        DataTable dt = fieldBll.SelByModelID(ModelID, false);
        DataTable fieldTable = commonCall.GetDTFromPage(dt, Page, ViewState);
        string tbname= modelBll.GetModelById(ModelID).TableName;
        if (CustID > 0)//修改
        {
            basicBll.UpdateContent(fieldTable, tbname,itemid);
            return itemid;
        }
        return basicBll.AddContent(fieldTable, tbname);

    }
    protected void ctl00_CphContent_BtnSave0_Click(object sender, EventArgs e)
    {
        Response.Write("<script>location.href='CustomerList.aspx';</script>");
    }

    public manage_AddCRM_CustomerManage()
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

    public string UC_BI()
    {
        XmlDocument xmlDoc2 = new XmlDocument();
        xmlDoc2.Load(Server.MapPath("/Config/AppSettings.config"));
        XmlNodeList amde = xmlDoc2.SelectNodes("appSettings/add");
        int val = 0;
        foreach (XmlNode xn in amde)
        {
            if (xn.Attributes["key"].Value.ToString() == "OAconfig")
                val = DataConverter.CLng(xn.Attributes["value"].Value);
        }
        //0、企业办公，1、个人办公，2、政府办公，3、教育办公，4、门户办公
        if (val == 0)
        {
            return "企业办公";
        }
        if (val == 1)
        {
            return "个人办公";
        }
        if (val == 2)
        {
            return "政府办公";
        }
        if (val == 3)
        {
            return "教育办公";
        }
        if (val == 4)
        {
            return "门户办公";
        }
        else
        {
            return "BI应用";
        }
    }

    /// <summary>
    /// 生成html控件字符串(用于新建)
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string CreateHtml(DataTable dt)
    {
        if (dt == null || dt.Rows.Count < 1) return "";
        string html = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if ((dt.Rows[i]["enable"] as string).ToLower().Equals("false")) continue;//不用生成
            string adminname = dt.Rows[i]["AdminName"].ToString();
            if (!string.IsNullOrEmpty(adminname))
            {
                if (!("," + adminname + ",").Contains("," + badmin.GetAdminLogin().AdminName + ",")) { continue; }//管理员名不匹配不生成
            }
            html += "<tr class='tdbg'><td align='right' class='tdbgleft'>" + dt.Rows[i]["displayName"].ToString() + "：</td>";
            html += "<td colspan='3'>";
            string type = dt.Rows[i]["buildMethod"].ToString();
            switch (type)
            {
                case "1":
                    html = CreateDropList(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString());
                    break;
                case "2":
                    html = CreateRadio(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString());
                    break;
                case "3":
                    html = CreateCheckBox(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString());
                    break;
                case "4":
                    html = CreateText(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString());
                    break;
                case "5":
                    html = CreateText(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString(), true);
                    break;
                default:
                    break;
            }
        }
        html += "</tr>";
        return html;
    }
    /// <summary>
    /// 生成html控件字符串,并带默认值(用于修改)
    /// </summary>
    public string CreateHtml(DataTable dt, DataTable selectedDT)
    {
        if (dt == null || dt.Rows.Count < 1) return "";
        else if (selectedDT == null || selectedDT.Rows.Count < 1) { return CreateHtml(dt); }
        string html = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if ((dt.Rows[i]["enable"] as string).ToLower().Equals("false")) continue;//不用生成
            string adminname = dt.Rows[i]["AdminName"].ToString();
            if (!string.IsNullOrEmpty(adminname))
            {
                if (!("," + adminname + ",").Contains("," + badmin.GetAdminLogin().AdminName + ",")) { continue; }//管理员名不匹配不生成
            }
            html += "<tr class='tdbg'><td align='right' class='tdbgleft'>" + dt.Rows[i]["displayName"] as string + "：</td>";
            html += "<td colspan='3'>";
            DataTable temp = GetTableList(dt.Rows[i]["tagName"] as string);
            selectedDT.DefaultView.RowFilter = "HtmlName in ('" + dt.Rows[i]["tagName"] as string + "')";
            DataTable curdt = selectedDT.DefaultView.ToTable();
            string selectedValue = "";
            if (curdt.Rows.Count > 0)
            {
                selectedValue = curdt.Rows[0]["HtmlValue"] as string;
            }
            string type = dt.Rows[i]["buildMethod"] as string;
            switch (type)
            {
                case "1":
                    html = CreateDropList(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString(), selectedValue);
                    break;
                case "2":
                    html = CreateRadio(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString(), selectedValue);
                    break;
                case "3":
                    html = CreateCheckBox(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString(), selectedValue);
                    break;
                case "4":
                    html = CreateText(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString(), selectedValue);
                    break;
                case "5":
                    html = CreateText(html, dt.Rows[i]["tagName"] as string, dt.Rows[i]["option"].ToString(), selectedValue, true);
                    break;
                default:
                    break;
            }
            html += "</td></tr>";
        }
        return html;
    }
    //DropDownList
    public string CreateDropList(string s, string nodeName, string option)
    {
        s += "<select class='form-control text_300' name='" + nodeName + "'>";
        string[] options = option.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in options)
        {
            s += "<option value='" + str + "'>" + str + "</option>";
        }
        s += "</select>";
        return s;
    }
    public string CreateDropList(string s, string nodeName, string option, string selectedValue)
    {
        s += "<select class='form-control text_300' name='" + nodeName + "'>";
        string[] options = option.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in options)
        {
            if (str.Equals(selectedValue))
                s += "<option value='" + str + "' selected='selected'>" + str;
            else s += "<option value='" + str + "'>";
            s += str + "</option>";
        }
        s += "</select>";
        return s;
    }
    //checkbox
    public string CreateCheckBox(string s, string nodeName, string option)
    {
        string[] options = option.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in options)
        {
            s += "<input type='checkbox' name='" + nodeName + "' value='" + str + "'>" + str + "</input>";
        }
        return s;
    }
    public string CreateCheckBox(string s, string nodeName, string option, string selectedValue)
    {
        string[] options = option.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in options)
        {
            if (("," + selectedValue + ",").Contains("," + str + ","))
                s += "<input type='checkbox' name='" + nodeName + "' checked='checked' value='" + str + "'>" + str + "</input>";
            else
                s += "<input type='checkbox' name='" + nodeName + "' value='" + str + "'>" + str + "</input>";
        }
        return s;
    }
    //radio
    public string CreateRadio(string s, string nodeName, string option)
    {
        string[] options = option.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in options)
        {
            s += "<input type='radio' name='" + nodeName + "' value='" + str + "'>" + str + "</input>";
        }
        return s;
    }
    public string CreateRadio(string s, string nodeName, string option, string selectedValue)
    {
        string[] options = option.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in options)
        {
            string value = str;
            if (str.Equals(selectedValue))
                s += "<input type='radio' name='" + nodeName + "' checked='checked' value='" + value + "'>" + value + "</input>";
            else
                s += "<input type='radio' name='" + nodeName + "' value='" + value + "'>" + value + "</input>";
        }
        return s;
    }
    //text,input只有一行，不遍历
    public string CreateText(string s, string nodeName, string option, bool ishtml = false)
    {
        string width = "200", height = "100";
        if (!string.IsNullOrEmpty(option))
        {
            string[] temparr = option.Split(',');
            width = temparr[0];
            height = temparr[1];
        }
        string htmlclass = ishtml ? "ueditor" : "";//带编辑器标识
        s += "<textarea class='" + htmlclass + "' id='txt_" + nodeName + "' name='" + nodeName + "' value='' style='width:" + width + "px;height:" + height + "px' ></textarea>";
        if (ishtml)
        {
            s += Call.GetUEditor("txt_" + nodeName, 2);
        }
        return s;
    }
    public string CreateText(string s, string nodeName, string option, string selectedValue, bool ishtml = false)
    {
        string width = "200", height = "100";
        if (!string.IsNullOrEmpty(option))
        {
            string[] temparr = option.Split(',');
            width = temparr[0];
            height = temparr[1];
        }
        s += "<textarea id='txt_" + nodeName + "' type='text' name='" + nodeName + "' style='width:" + width + "px;height:" + height + "px' >" + selectedValue + "</textarea>";
        if (ishtml)
        {
            s += Call.GetUEditor("txt_" + nodeName, 2);
        }
        return s;
    }
    //提交跟进信息
    protected void EBtnSubmit2_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(startDate.Text.Trim()) > Convert.ToDateTime(endDate.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('跟进时间不能大于下次跟进时间');$('#FPDiv').toggle(); $('#FPGrid').toggle();createTxt();", true);
                return;
            }
        }
        catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('日期格式不符合规范');$('#FPDiv').toggle(); $('#FPGrid').toggle();createTxt();", true); return; }
        //pModel.OwnStatus = 1;//指定其为CRM跟进
        //pModel.TargetID = Request.QueryString["ID"];
        //pModel.ParentID = 0;//指定要回复的上一级贴，这个看情况是否需要
        //pModel.WebCoding = TxtClientNum.Text.Trim();//Code
        //pModel.Name = "";//类似于标题
        //pModel.UserInfo = TxtTContent.Value;//内容
        //pModel.Requirements = Request.QueryString["tid"];//需求,用来作备注
        //pModel.UserID = badmin.GetAdminLogin().AdminId;//保留名与ID，目标员工留职，或单出错，可以知道是谁
        //pModel.FPMan = badmin.GetAdminLogin().AdminName;
        //pModel.ProjectLevel = 0;//这个用来标识是否跟进吧，跟进处理过后,其值改为1
        //pModel.BeginTime = DataConverter.CDate(startDate.Text);//跟进时间
        //pModel.CompletionTime = DataConverter.CDate(endDate.Text);//下次跟进时间
        //pModel.ApplicationTime = DateTime.Now;//添加该条记录的时间
        //pBll.insert(pModel);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');location=location;", true);
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Edit2":
                EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                break;
            case "Save":
                string[] s = e.CommandArgument.ToString().Split(':');
                Update(DataConverter.CLng(s[0]), s[1]);
                break;
            case "Delete2":
                // pBll.DeleteByGroupID(Convert.ToInt32(e.CommandArgument as string));
                EGV.DataBind();
                break;
            case "Cancel":
                EGV.EditIndex = 0;
                break;
            default: break;
        }
    }
    protected void Update(int rowNum, string id)//Update FP Message
    {

        //pModel = pBll.GetSelect(DataConverter.CLng(id));
        //GridViewRow gr = EGV.Rows[rowNum];
        //pModel.UserInfo = ((TextBox)gr.FindControl("editUserinfo")).Text.Trim();
        //pModel.CompletionTime =(DataConverter.CDate(((TextBox)gr.FindControl("editEndDate")).Text.Trim()));
        //pBll.GetUpdate(pModel);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改完成');location=location;", true);
    }
}
