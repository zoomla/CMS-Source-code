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
using System.Collections.Generic;

namespace ZoomLaCMS.Manage.AddCrm
{
    public partial class ViewCustomer : System.Web.UI.Page
    {
        private B_Client_Basic basicBll = new B_Client_Basic();
        private B_Client_Enterprise bce = new B_Client_Enterprise();
        private B_Client_Penson bcp = new B_Client_Penson();
        private B_User ull = new B_User();
        private XmlDocument myDoc = new XmlDocument();
        private B_ModelField fieldBll = new B_ModelField();
        private B_Model modelBll = new B_Model();
        private string Pro_name = string.Empty;
        private int pid = 0;
        public int ModelID { get { return DataConverter.CLng(Request.QueryString["modelid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                //DropArea.Text = "";// DataConverter.CLng(Areatxt);
                //DropStatusInField.Text = "";// DataConverter.CLng(Statustxt);
                //DropClientField.Text = "";//DataConverter.CLng(Callingtxt);
                //DropValueLevel.Text = "";//DataConverter.CLng(Valuetxt);
                //DropCreditLevel.Text = "";//DataConverter.CLng(Credittxt);
                //DropImportance.Text = "";//DataConverter.CLng(Importancetxt);
                //DropConnectionLevel.Text = "";//DataConverter.CLng(Connectiontxt);
                //DropSourceType.Text = "";//DataConverter.CLng(Sourcetxt);
                //DropPhaseType.Text = "";//DataConverter.CLng(Phasetxt);
                //DropGroupID.Text = "";//DataConverter.CLng(Grouptxt);
                DropCompanySize.Text = "";//DataConverter.CLng(Sizetxt);
                DropManagementForms.Text = "";//DataConverter.CLng(Managementtxt);
                DropEducation.Text = ""; //DataConverter.CLng(Educationtxt);
                DropIncome.Text = ""; // DataConverter.CLng(Incometxt);


                int id = DataConverter.CLng(Request.QueryString["id"]);
                this.pid = id;
                M_Client_Basic binfo = basicBll.GetSelect(id);
                //htmlStr.Text = CreateHtml(selectedDT, true);
                //-----
                this.TxtClientName.Text = binfo.P_name;
                this.Pro_name = binfo.P_name;
                User_Title.InnerHtml = binfo.P_name;
                this.TxtClientNum.Text = binfo.Code;
                this.TxtShortedForm.Text = binfo.P_alias;
                this.ClientSelect.Text = binfo.Client_Upper;
                //this.DropArea.Text = binfo.Client_Area;
                DataTable Calling = GetTableList("Client_Calling");
                try
                {
                    // this.DropClientField.Text = Calling.Rows[DataConverter.CLng(binfo.Client_Calling)]["content"].ToString();
                }
                catch
                { }
                //this.DropValueLevel.Text = binfo.Client_Value;
                //this.DropCreditLevel.Text = binfo.Client_Credit;
                //this.DropImportance.Text = binfo.Client_Importance;
                //this.DropConnectionLevel.Text = binfo.Client_Connection;
                //this.DropSourceType.Text = binfo.Client_Source;
                //this.DropPhaseType.Text = binfo.Client_Phase;
                //this.DropGroupID.Text = binfo.Client_Group;
                this.TxtRemark.Text = binfo.Title;

                //this.TxtCompany.Text = binfo.Unit_Name;
                //this.TxtDepartment.Text = binfo.Unit_Class;
                //this.TxtPosition.Text = binfo.Unit_Post;
                //this.TxtOperation.Text = binfo.Unit_Operation;
                //this.TxtCompanyAddress.Text = binfo.Unit_Address;
                //this.TxtTitle.Text = binfo.Unit_Title;

                ModelHtml.Text = "";
                //--模型字段处理
                var modelMod = modelBll.GetModelById(ModelID);
                if (modelMod != null && !string.IsNullOrEmpty(modelMod.TableName))
                {
                    DataTable contentDt = basicBll.GetContent(modelMod.TableName, binfo.ItemID);
                    ModelHtml.Text = fieldBll.InputallHtml(ModelID, 0, new ModelConfig()
                    {
                        Source = ModelConfig.SType.Admin,
                        ValueDT = contentDt,
                        Mode = ModelConfig.SMode.PreView
                    });
                }

                //binfo.Add_Name = ull.GetLogin().UserName;
                binfo.Add_Date = DateTime.Today;
                binfo.Title = this.TxtRemark.Text;

                string scriptname = "";

                if (binfo.Client_Type == "1")
                {
                    this.Label1.Text = "查看企业客户";
                    this.ClientType.Text = "企业客户";
                    function.Script(this, "window.onload=function(){isPerson('Enterprise_Add');}");
                    DataTable tablesd = bce.SelByCode(binfo.Code);
                    if (tablesd != null && tablesd.Rows.Count > 0)
                    {
                        int flowid = DataConverter.CLng(tablesd.Rows[0]["Flow"].ToString());
                        M_Client_Enterprise enterprise = bce.GetEnterpriseByCode(binfo.Code);
                        this.TxtBankAccount.Text = enterprise.Bank_Code;
                        this.TxtBankOfDeposit.Text = enterprise.Bank_Open;
                        this.DropManagementForms.Text = enterprise.Co_Management;
                        this.DropCompanySize.Text = enterprise.Co_Size;
                        this.DropStatusInField.Text = enterprise.Co_Status;
                        this.TxtClientNum.Text = enterprise.Code;
                        this.TxtTaxNum.Text = enterprise.Tax_Code;
                        this.TxtAnnualSales.Text = enterprise.Sales.ToString();
                        this.TxtRegisteredCapital.Text = enterprise.Reg_Capital.ToString();
                        this.TxtBusinessScope.Text = enterprise.Operation_Bound;
                        this.TxtHomepage1.Text = enterprise.Homepage;
                        this.TxtFax1.Text = enterprise.Fax_phone;
                        this.TxtPhone.Text = enterprise.Phone;
                        this.TxtAddress.Text = enterprise.Message_Address;
                        this.TxtZipCodeW.Text = enterprise.ZipCodeW;// //enterprise.ZipCodeW = Request.Form["TxtZipCodeW"];

                        this.country.Text = enterprise.country;
                        this.province.Text = enterprise.province;
                        this.city.Text = enterprise.city;
                        scriptname = "document.getElementById(\"country\").value = \"" + enterprise.country + "\";";
                        scriptname += "document.getElementById(\"province\").value = \"" + enterprise.province + "\";province_onchange(province,city);";
                        scriptname += "document.getElementById(\"city\").value = \"" + enterprise.city + "\";";
                    }
                }
                else
                {
                    this.Label1.Text = "查看个人客户";
                    this.ClientType.Text = "个人客户";
                    function.Script(this, "window.onload=function(){isPerson('Person_Add');}");
                    DataTable tables = bcp.SelByCode(binfo.Code.Trim());
                    if (tables != null && tables.Rows.Count > 0)
                    {
                        int flowid = DataConverter.CLng(tables.Rows[0]["Flow"].ToString());
                        M_Client_Penson penson = bcp.GetSelect(flowid);
                        this.TxtClientNum.Text = penson.Code;

                        if (penson.Sex == "1")
                        {
                            this.RadlSex.Text = "男";
                        }
                        else if (penson.Sex == "1")
                        {
                            this.RadlSex.Text = "女";
                        }
                        else
                        {
                            this.RadlSex.Text = "保密";
                        }

                        switch (penson.Marriage.Trim())
                        {
                            case "":
                                this.RadlMarriage.Text = "保密";
                                break;
                            case "0":
                                this.RadlMarriage.Text = "保密";
                                break;
                            case "1":
                                this.RadlMarriage.Text = "离婚";
                                break;
                            case "2":
                                this.RadlMarriage.Text = "已婚";
                                break;
                            case "3":
                                this.RadlMarriage.Text = "离异";
                                break;
                        }
                        this.DpkBirthday.Text = penson.Birthday.ToString();
                        this.TxtIDCard.Text = penson.Id_Code;
                        this.TxtNativePlace.Text = penson.Native;
                        this.TxtNation.Text = penson.Nation;
                        this.DropEducation.Text = penson.Education;
                        this.TxtGraduateFrom.Text = penson.Finish_School;
                        this.TxtInterestsOfLife.Text = penson.Life_Love;
                        this.TxtInterestsOfCulture.Text = penson.Cultrue_Love;
                        this.TxtInterestsOfOther.Text = penson.Other_Love;
                        this.TxtInterestsOfSport.Text = penson.Sport_Love;
                        this.TxtInterestsOfAmusement.Text = penson.FUN_Love;
                        this.DropIncome.Text = penson.Income.ToString();
                        this.TxtFamily.Text = penson.Home_Circs;
                        this.TxtAim.Text = penson.Aim_Code;
                        this.TxtFax.Text = penson.Fax_phone;
                        this.TxtAddress.Text = penson.Message_Address;
                        this.TxtOfficePhone.Text = penson.Work_Phone;
                        this.TxtHomePhone.Text = penson.Home_Phone;
                        this.TxtMobile.Text = penson.Telephone;
                        this.TxtPHS.Text = penson.Little_Smart;
                        this.TxtHomepage.Text = penson.Homepage;
                        this.TxtEmail.Text = penson.Email_Address;
                        this.TxtQQ.Text = penson.QQ_Code;
                        this.TxtMSN.Text = penson.MSN_Code;
                        this.TxtUC.Text = penson.UC_Code;
                        this.TxtYahoo.Text = penson.YaoHu_Code;
                        this.TxtICQ.Text = penson.ICQ_Code;
                        this.TxtZipCodeW.Text = penson.ZipCodeW;

                        this.country.Text = penson.country;
                        this.province.Text = penson.province;
                        this.city.Text = penson.city;

                        scriptname = "document.getElementById(\"country\").value = \"" + penson.country + "\";";
                        scriptname += "document.getElementById(\"province\").value = \"" + penson.province + "\";province_onchange(province,city);";
                        scriptname += "document.getElementById(\"city\").value = \"" + penson.city + "\";";
                    }
                }
            }


            //DataTable tt = bpt.SelectByName(this.Pro_name);
            int typeid = DataConverter.CLng(Request.QueryString["type"]);
            if (typeid != 0)
            {

                //tt = bpt.SelectByName(this.Pro_name);
            }
            //if (tt != null)
            //{
            //    tt.DefaultView.Sort = "ID desc";
            //    Bind(tt);
            //}
            Call.SetBreadCrumb(Master, "<li>CRM管理</li><li><a href='CustomerList.aspx?usertype=0'>客户管理</a></li><li>" + Label1.Text + "</li>");
        }
        //单条编辑
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            int id = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Audit":
                    //bpt.ChangeAudit(Convert.ToInt32(e.CommandArgument));
                    Response.Redirect("../AddOn/Projects.aspx");
                    break;
                case "canclepass":
                    function.WriteErrMsg("取消!");
                    break;
                case "manage":
                    Response.Redirect("../AddOn/AddUpdateProject.aspx?ID=" + e.CommandArgument);
                    break;
                case "del":
                    //bool i = bpt.DeleteByGroupID(id);
                    //if (i == true)
                    //    Response.Write("<script language=javascript>alert('删除成功!');location.href='../AddOn/Projects.aspx';</script>");
                    //else
                    //    Response.Write("<script language=javascript>alert('删除失败!');location.href='../AddOn/Projects.aspx';</script>");
                    break;
                case "Comments":
                    Response.Redirect("../AddOn/ProcessesComments.aspx?ProjectID=" + e.CommandArgument);
                    break;
                case "Run":
                    //M_Projects pinfo = bpt.GetSelect(id);
                    //pinfo.ProStatus = 1;
                    //bpt.GetUpdate(pinfo);
                    Response.Redirect("../AddOn/Projects.aspx");
                    break;
                default:
                    break;
            }
        }
        protected void ctl00_CphContent_BtnSave_Click(object sender, EventArgs e)
        {

        }
        protected void ctl00_CphContent_BtnSave0_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='CustomerList.aspx?modelid=" + ModelID + "';</script>");
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
        public string[] provinceList()
        {
            string[] province = new string[35];
            province[0] = "安徽";
            province[1] = "北京";
            province[2] = "重庆";
            province[3] = "福建";
            province[4] = "甘肃";
            province[5] = "广东";
            province[6] = "广西";
            province[7] = "贵州";
            province[8] = "海南";
            province[9] = "河北";
            province[10] = "河南";
            province[11] = "黑龙江";
            province[12] = "湖北";
            province[13] = "湖南";
            province[14] = "江苏";
            province[15] = "江西";
            province[16] = "吉林";
            province[17] = "辽宁";
            province[18] = "内蒙古";
            province[19] = "宁夏";
            province[20] = "青海";
            province[21] = "上海";
            province[22] = "山东";
            province[23] = "山西";
            province[24] = "陕西";
            province[25] = "四川";
            province[26] = "天津";
            province[27] = "新疆";
            province[28] = "西藏";
            province[29] = "云南";
            province[30] = "浙江";
            province[31] = "香港";
            province[32] = "澳门";
            province[33] = "台湾";
            province[34] = "其它";
            return province;
        }
        private string[] cityList()
        {
            string[] cityarr = new string[3407];
            cityarr[0] = "合肥市";
            cityarr[1] = "淮北市";
            cityarr[2] = "淮南市";
            cityarr[3] = "黄山市";
            cityarr[4] = "安庆市";
            cityarr[5] = "蚌埠市";
            cityarr[6] = "巢湖市";
            cityarr[7] = "池州市";
            cityarr[8] = "滁州市";
            cityarr[9] = "六安市";
            cityarr[10] = "马鞍山市";
            cityarr[11] = "宣城市";
            cityarr[12] = "宿州市";
            cityarr[13] = "铜陵市";
            cityarr[14] = "芜湖市";
            cityarr[15] = "阜阳市";
            cityarr[16] = "亳州市";
            cityarr[100] = "北京市";
            cityarr[200] = "重庆市";
            cityarr[201] = "涪陵市";
            cityarr[202] = "黔江市";
            cityarr[203] = "万县市";
            cityarr[300] = "福州市";
            cityarr[301] = "龙岩市";
            cityarr[302] = "南平市";
            cityarr[303] = "宁德市";
            cityarr[304] = "莆田市";
            cityarr[305] = "泉州市";
            cityarr[306] = "三明市";
            cityarr[307] = "厦门市";
            cityarr[308] = "漳州市";
            cityarr[400] = "兰州市";
            cityarr[401] = "甘南藏族自治州";
            cityarr[402] = "定西地区";
            cityarr[403] = "白银市";
            cityarr[404] = "嘉峪关市";
            cityarr[405] = "金昌市";
            cityarr[406] = "酒泉市";
            cityarr[407] = "临夏回族自治州";
            cityarr[408] = "陇南地区";
            cityarr[409] = "平凉市";
            cityarr[410] = "庆阳市";
            cityarr[411] = "天水市";
            cityarr[412] = "武威市";
            cityarr[413] = "张掖市";
            cityarr[500] = "广州市";
            cityarr[501] = "佛山市";
            cityarr[502] = "惠州市";
            cityarr[503] = "东莞市";
            cityarr[504] = "江门市";
            cityarr[505] = "揭阳市";
            cityarr[506] = "潮州市";
            cityarr[507] = "茂名市";
            cityarr[508] = "梅州市";
            cityarr[509] = "清远市";
            cityarr[510] = "汕头市";
            cityarr[511] = "汕尾市";
            cityarr[512] = "深圳市";
            cityarr[513] = "韶关市";
            cityarr[514] = "阳江市";
            cityarr[515] = "河源市";
            cityarr[516] = "云浮市";
            cityarr[517] = "中山市";
            cityarr[518] = "珠海市";
            cityarr[519] = "湛江市";
            cityarr[520] = "肇庆市";
            cityarr[600] = "南宁市";
            cityarr[601] = "防城港市";
            cityarr[602] = "北海市";
            cityarr[603] = "百色市";
            cityarr[604] = "贺州市";
            cityarr[605] = "桂林市";
            cityarr[606] = "来宾市";
            cityarr[607] = "柳州市";
            cityarr[608] = "崇左市";
            cityarr[609] = "钦州市";
            cityarr[610] = "贵港市";
            cityarr[611] = "梧州市";
            cityarr[612] = "河池市";
            cityarr[613] = "玉林市";
            cityarr[700] = "贵阳市";
            cityarr[701] = "毕节地区";
            cityarr[702] = "遵义市";
            cityarr[703] = "安顺市";
            cityarr[704] = "六盘水市";
            cityarr[705] = "黔东南苗族侗族自治州";
            cityarr[706] = "黔南布依族苗族自治州";
            cityarr[707] = "黔西南布依族苗族自治";
            cityarr[708] = "铜仁地区";
            cityarr[800] = "海口市";
            cityarr[801] = "三亚市";
            cityarr[802] = "省直辖行政单位";
            cityarr[900] = "石家庄市";
            cityarr[901] = "邯郸市";
            cityarr[902] = "邢台市";
            cityarr[903] = "保定市";
            cityarr[904] = "张家口市";
            cityarr[905] = "沧州市";
            cityarr[906] = "承德市";
            cityarr[907] = "廊坊市";
            cityarr[908] = "秦皇岛市";
            cityarr[909] = "唐山市";
            cityarr[910] = "衡水市";
            cityarr[1000] = "郑州市";
            cityarr[1001] = "开封市";
            cityarr[1002] = "驻马店市";
            cityarr[1003] = "安阳市";
            cityarr[1004] = "焦作市";
            cityarr[1005] = "洛阳市";
            cityarr[1006] = "濮阳市";
            cityarr[1007] = "漯河市";
            cityarr[1008] = "南阳市";
            cityarr[1009] = "平顶山市";
            cityarr[1010] = "新乡市";
            cityarr[1011] = "信阳市";
            cityarr[1012] = "许昌市";
            cityarr[1013] = "商丘市";
            cityarr[1014] = "三门峡市";
            cityarr[1015] = "鹤壁市";
            cityarr[1016] = "周口市";
            cityarr[1017] = "济源市";
            cityarr[1100] = "哈尔滨市";
            cityarr[1101] = "大庆市";
            cityarr[1102] = "大兴安岭地区";
            cityarr[1103] = "鸡西市";
            cityarr[1104] = "佳木斯市";
            cityarr[1105] = "牡丹江市";
            cityarr[1106] = "齐齐哈尔市";
            cityarr[1107] = "七台河市";
            cityarr[1108] = "双鸭山市";
            cityarr[1109] = "绥化市";
            cityarr[1110] = "伊春市";
            cityarr[1111] = "鹤岗市";
            cityarr[1112] = "黑河市";
            cityarr[1200] = "武汉市";
            cityarr[1201] = "黄冈市";
            cityarr[1202] = "黄石市";
            cityarr[1203] = "恩施土家族苗族自治州";
            cityarr[1204] = "鄂州市";
            cityarr[1205] = "荆门市";
            cityarr[1206] = "荆州市";
            cityarr[1207] = "孝感市";
            cityarr[1208] = "省直辖县级行政单位";
            cityarr[1209] = "十堰市";
            cityarr[1210] = "襄樊市";
            cityarr[1211] = "咸宁市";
            cityarr[1212] = "宜昌市";
            cityarr[1213] = "随州市";
            cityarr[1300] = "长沙市";
            cityarr[1301] = "怀化市";
            cityarr[1302] = "郴州市";
            cityarr[1303] = "常德市";
            cityarr[1304] = "娄底市";
            cityarr[1305] = "邵阳市";
            cityarr[1306] = "湘潭市";
            cityarr[1307] = "湘西土家族苗族自治州";
            cityarr[1308] = "衡阳市";
            cityarr[1309] = "永州市";
            cityarr[1310] = "益阳市";
            cityarr[1311] = "岳阳市";
            cityarr[1312] = "株洲市";
            cityarr[1313] = "张家界市";
            cityarr[1400] = "南京市";
            cityarr[1401] = "淮安市";
            cityarr[1402] = "常州市";
            cityarr[1403] = "连云港市";
            cityarr[1404] = "南通市";
            cityarr[1405] = "徐州市";
            cityarr[1406] = "苏州市";
            cityarr[1407] = "无锡市";
            cityarr[1408] = "盐城市";
            cityarr[1409] = "扬州市";
            cityarr[1410] = "镇江市";
            cityarr[1411] = "泰州市";
            cityarr[1412] = "宿迁市";
            cityarr[1500] = "南昌市";
            cityarr[1501] = "抚州市";
            cityarr[1502] = "赣州市";
            cityarr[1503] = "吉安市";
            cityarr[1504] = "景德镇市";
            cityarr[1505] = "九江市";
            cityarr[1506] = "萍乡市";
            cityarr[1507] = "新余市";
            cityarr[1508] = "上饶市";
            cityarr[1509] = "鹰潭市";
            cityarr[1510] = "宜春市";
            cityarr[1600] = "长春市";
            cityarr[1601] = "白城市";
            cityarr[1602] = "白山市";
            cityarr[1603] = "吉林市";
            cityarr[1604] = "辽源市";
            cityarr[1605] = "四平市";
            cityarr[1606] = "松原市";
            cityarr[1607] = "通化市";
            cityarr[1608] = "延边朝鲜族自治州";
            cityarr[1700] = "沈阳市";
            cityarr[1701] = "大连市";
            cityarr[1702] = "阜新市";
            cityarr[1703] = "抚顺市";
            cityarr[1704] = "本溪市";
            cityarr[1705] = "鞍山市";
            cityarr[1706] = "丹东市";
            cityarr[1707] = "锦州市";
            cityarr[1709] = "辽阳市";
            cityarr[1710] = "盘锦市";
            cityarr[1711] = "铁岭市";
            cityarr[1712] = "营口市";
            cityarr[1713] = "葫芦岛市";
            cityarr[1800] = "呼和浩特市";
            cityarr[1801] = "阿拉善盟";
            cityarr[1802] = "巴彦淖尔盟";
            cityarr[1803] = "包头市";
            cityarr[1804] = "赤峰市";
            cityarr[1805] = "兴安盟";
            cityarr[1806] = "乌兰察布盟";
            cityarr[1807] = "乌海市";
            cityarr[1808] = "锡林郭勒盟";
            cityarr[1809] = "呼伦贝尔盟";
            cityarr[1810] = "伊克昭盟";
            cityarr[1811] = "通辽市";
            cityarr[1900] = "银川市";
            cityarr[1901] = "固原市";
            cityarr[1902] = "石嘴山市";
            cityarr[1903] = "吴忠市";
            cityarr[1904] = "中卫市";
            cityarr[2000] = "西宁市";
            cityarr[2001] = "海东地区";
            cityarr[2002] = "海南藏族自治州";
            cityarr[2003] = "海北藏族自治州";
            cityarr[2004] = "黄南藏族自治州";
            cityarr[2005] = "果洛藏族自治州";
            cityarr[2006] = "玉树藏族自治州";
            cityarr[2007] = "海西蒙古族藏族自治州";
            cityarr[2100] = "上海市";
            cityarr[2200] = "济南市";
            cityarr[2201] = "东营市";
            cityarr[2202] = "滨州市";
            cityarr[2203] = "淄博市";
            cityarr[2204] = "德州市";
            cityarr[2205] = "济宁市";
            cityarr[2206] = "聊城市";
            cityarr[2207] = "临沂市";
            cityarr[2208] = "莱芜市";
            cityarr[2209] = "青岛市";
            cityarr[2210] = "日照市";
            cityarr[2211] = "威海市";
            cityarr[2212] = "泰安市";
            cityarr[2213] = "潍坊市";
            cityarr[2214] = "烟台市";
            cityarr[2215] = "菏泽市";
            cityarr[2216] = "枣庄市";
            cityarr[2300] = "太原市";
            cityarr[2301] = "大同市";
            cityarr[2302] = "晋城市";
            cityarr[2303] = "晋中市";
            cityarr[2304] = "长治市";
            cityarr[2305] = "临汾市";
            cityarr[2306] = "吕梁地区";
            cityarr[2307] = "忻州市";
            cityarr[2308] = "朔州市";
            cityarr[2309] = "阳泉市";
            cityarr[2310] = "运城市";
            cityarr[2400] = "西安市";
            cityarr[2401] = "宝鸡市";
            cityarr[2402] = "安康市";
            cityarr[2403] = "商洛市";
            cityarr[2404] = "铜川市";
            cityarr[2405] = "渭南市";
            cityarr[2406] = "咸阳市";
            cityarr[2407] = "延安市";
            cityarr[2408] = "汉中市";
            cityarr[2409] = "榆林市";
            cityarr[2500] = "成都市";
            cityarr[2501] = "达川市";
            cityarr[2502] = "甘孜藏族自治州";
            cityarr[2503] = "自贡市";
            cityarr[2504] = "阿坝藏族羌族自治州";
            cityarr[2505] = "巴中市";
            cityarr[2506] = "德阳市";
            cityarr[2507] = "广安市";
            cityarr[2508] = "广元市";
            cityarr[2509] = "凉山彝族自治州";
            cityarr[2510] = "乐山市";
            cityarr[2511] = "攀枝花市";
            cityarr[2512] = "南充市";
            cityarr[2513] = "内江市";
            cityarr[2514] = "泸州市";
            cityarr[2515] = "绵阳市";
            cityarr[2516] = "遂宁市";
            cityarr[2517] = "雅安市";
            cityarr[2518] = "宜宾市";
            cityarr[2519] = "眉山市";
            cityarr[2520] = "资阳市";
            cityarr[2600] = "天津市";
            cityarr[2700] = "乌鲁木齐市";
            cityarr[2701] = "喀什地区";
            cityarr[2702] = "克孜勒苏柯尔克孜自治";
            cityarr[2703] = "克拉玛依市";
            cityarr[2704] = "阿克苏地区";
            cityarr[2705] = "阿勒泰地区";
            cityarr[2706] = "巴音郭楞蒙古自治州";
            cityarr[2707] = "哈密地区";
            cityarr[2708] = "博尔塔拉蒙古自治州";
            cityarr[2709] = "昌吉回族自治州";
            cityarr[2710] = "塔城地区";
            cityarr[2711] = "吐鲁番地区";
            cityarr[2712] = "和田地区";
            cityarr[2713] = "石河子市";
            cityarr[2714] = "伊犁哈萨克自治州";
            cityarr[2800] = "拉萨市";
            cityarr[2801] = "阿里地区";
            cityarr[2802] = "昌都市";
            cityarr[2803] = "林芝地区";
            cityarr[2804] = "那曲地区";
            cityarr[2805] = "山南地区";
            cityarr[2806] = "日喀则地区";
            cityarr[2900] = "昆明市";
            cityarr[2901] = "大理白族自治州";
            cityarr[2902] = "昭通市";
            cityarr[2903] = "保山市";
            cityarr[2904] = "德宏傣族景颇族自治州";
            cityarr[2905] = "迪庆藏族自治州";
            cityarr[2906] = "楚雄彝族自治州";
            cityarr[2907] = "临沧地区";
            cityarr[2908] = "丽江市";
            cityarr[2909] = "怒江傈僳族自治州";
            cityarr[2910] = "曲靖市";
            cityarr[2911] = "思茅地区";
            cityarr[2912] = "西双版纳傣族自治州";
            cityarr[2913] = "文山壮族苗族自治州";
            cityarr[2914] = "红河哈尼族彝族自治州";
            cityarr[2915] = "玉溪市";
            cityarr[3000] = "杭州市";
            cityarr[3001] = "嘉兴市";
            cityarr[3002] = "金华市";
            cityarr[3003] = "衢州市";
            cityarr[3004] = "丽水市";
            cityarr[3005] = "宁波市";
            cityarr[3006] = "绍兴市";
            cityarr[3007] = "台州市";
            cityarr[3008] = "温州市";
            cityarr[3009] = "湖州市";
            cityarr[3010] = "舟山市";
            cityarr[3100] = "香港";
            cityarr[3200] = "澳门";
            cityarr[3300] = "台湾";
            cityarr[3400] = "北美洲";
            cityarr[3401] = "南美洲";
            cityarr[3402] = "大洋洲";
            cityarr[3403] = "欧洲";
            cityarr[3404] = "亚洲";
            cityarr[3405] = "非洲";
            cityarr[3406] = "虚拟社团";
            return cityarr;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            Response.Redirect("CustomerManage.aspx?FieldName=Person_Add&menu=edit&id=" + id + "&modelid=" + ModelID);
        }
        //按要求搜索
        protected void BntSearch_Click(object sender, EventArgs e)
        {
            string keyword = Request.Form["TxtADName"];
            //DataTable dt = bpt.SelectBykeyword(keyword);
            //Bind(dt);
            //if (dt != null)
            //{
            //    dt.Dispose();
            //}
        }
        protected bool GetBool(string prostatus)
        {
            if (prostatus == "2")
                return true;
            else
                return false;
        }
        //批量审核
        protected void BtnAudit_Click(object sender, EventArgs e)
        {
        }

        //项目未启动，没负责人
        protected string GetLeader(string leader)
        {
            if (leader != null && leader != "")
            {
                return leader;
            }
            else
                return "暂无";
        }


        //显示审核
        protected string GetAuditEdit(string audit)
        {
            if (audit == "1")
                return "通过";
            else
                return "取消";
        }

        //绑定类型
        protected string GetProType(string typeid)
        {
            //if (bptype.GetSelect(DataConverter.CLng(typeid)).ProjectTypeName == "")
            //    return "类型已删";
            //else
            //    return bptype.GetSelect(Convert.ToInt32(typeid)).ProjectTypeName;
            return "";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //返回进度百分比
        protected string GetLong(string id)
        {
            int i = Convert.ToInt32(id);
            //DataTable t = bps.SelectByProID(i);
            //int line = 0;
            //foreach (DataRow r in t.Rows)
            //{
            //    if (r[5].ToString() == "1")
            //    {
            //        line += DataConverter.CLng(r[4].ToString());
            //    }
            //}
            //if (line > 100) { line = 100; }
            //string li = line.ToString();
            //li += "%";
            //return li;
            return "";
        }

        //绑定审核
        protected string GetAudit(string Audit)
        {
            if (Audit == "1")
            {
                return "<font color=red>×</font>";
            }
            else
            {
                return "<font color=green>√</font>";
            }
        }

        //绑定是否完成
        protected string GetProStatus(string prostatus)
        {
            if (prostatus == "1")
                return "启动";
            else if (prostatus == "2")
                return "完成";
            else if (prostatus == "3")
            {
                return "存档";
            }
            else
                return "未启动";
        }

        //通用分页
        protected void Bind(DataTable dt)
        { }
        public string CreateHtml(DataTable dt, Boolean ReadOnly)
        {
            if (dt == null || dt.Rows.Count < 1) return "";
            string html = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr class='tdbg'><td align='right' class='tdbgleft'>" + dt.Rows[i]["htmlName"].ToString() + "：</td>";
                html += "<td colspan='3'>";
                html += "<label>" + dt.Rows[i]["htmlValue"].ToString() + "</label>";
                html += "</td></tr>";
            }
            return html;
        }
    }
}