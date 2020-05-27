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
using System.Xml;
using ZoomLa.SQLDAL;
using System.Text;
using System.IO;
using System.Data.OleDb;

public partial class manage_AddCRM_CustomerList : CustomerPageAction
{
    private B_Client_Basic bll = new B_Client_Basic();
    private B_Admin badmin = new B_Admin();
    private string xmlPath="~/manage/AddCRM/nameToFieldName.xml";
    private string xmlPath2 = "~/Config/CRM_Dictionary.xml";
    private M_CrmAuth crmModel = new M_CrmAuth();
    private DataTable authDT = new DataTable();//用来存权限信息
    private B_CrmAuth crmBll = new B_CrmAuth();
    private GetDSData dsBll = new GetDSData();

    /// <summary>
   /// nametofieldname.xml
   /// </summary>
    DataSet ds = new DataSet();
    /// <summary>
    /// directory.xml
    /// </summary>
    DataSet ds2 = new DataSet();
    public int UserType { get { return DataConvert.CLng(Request.QueryString["UserType"]); } }
    public int ModelID { get { return DataConvert.CLng(Request.QueryString["modelid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string flag = "";
            Response.Write(flag);
            Response.End();
        }
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
        {
            string code = Request.QueryString["code"];
            bll.GetDeleteByCode(code);//以code为标准删除多表数据
        }
        divImp.Visible = DataConvert.CLng(Request.QueryString["import"]) == 1;
        if (!IsPostBack)
        {
            //---------权限限制(Excel导入权与可查看所有用户权)分页中限制显示哪些
            M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
            authDT = crmBll.GetAuthTable(info.RoleList.Split(','));

            if (!crmBll.IsHasAuth(authDT, "AllowExcel", info))//如果ID不是自己的ID或ID为空则跳转到自己的ID上
            {
                ExcelData.Enabled = false;
                fileImp.Enabled = false;
            }
            MyBind();
            ViewState["adminList"] = badmin.Sel();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='AffairManage.aspx'>" + UC_BI() + "</a></li><li>  <a href='CustomerList.aspx?usertype=0'>" + lang.LF("客户管理") + "</a></span>&nbsp;&nbsp;<a href='CustomerManage.aspx?FieldName=Person_Add&modelid="+ModelID+"'>[" + lang.LF("添加客户") + "]</a> <a href='CustomerList.aspx?import=1'>[导入客户]</a></li>" + Call.GetHelp(47));
        }
    }
    public void MyBind()
    {
        M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
        DataTable dt = new DataTable();
        //switch (UserType)
        //{
        //    case 1:
        //        dt = bll.SelByType(0, "");
        //        break;
        //    case 2:
        //        dt = bll.SelByType(1, "");
        //        break;
        //    default:
        //        dt = bll.SelByType(-1, "");
        //        dt.DefaultView.Sort = "Flow desc";
        //        break;
        //}
        dt = bll.Sel();
        if (!crmBll.IsHasAuth(authDT, "AllCustomer", info))//如果ID不是自己的ID或ID为空则跳转到自己的ID上
        {
            dt.DefaultView.RowFilter = "FPManID =" + info.AdminId;
            dt = dt.DefaultView.ToTable();
            dt.DefaultView.RowFilter = "";
        }
        RPT.DataSource = dt;
        RPT.DataBind();
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
    //-----------------DataTable Helper
    /// <summary>
    /// 将datatable中的数据导入表,导入前，请确定列名，表名必须正确.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public bool WriteDataToDB(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return true;
        }
        else if (string.IsNullOrEmpty(dt.TableName)) //未指定表名
        { return false; }
        string tname = dt.TableName;
        string colNames = "";
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            colNames += dt.Columns[i].ColumnName + ",";
        }
        colNames = colNames.TrimEnd(',');
        string cmd = "";
        string colValues;
        string cmdmode = string.Format("insert into {0}({1}) values({{0}});", tname, colNames);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            colValues = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (dt.Rows[i][j].GetType() == typeof(DBNull))
                {
                    colValues += "NULL,";
                    continue;
                }
                if (dt.Columns[j].DataType == typeof(string))
                    colValues += string.Format("'{0}',", dt.Rows[i][j]);
                else if (dt.Columns[j].DataType == typeof(int) || dt.Columns[j].DataType == typeof(float) || dt.Columns[j].DataType == typeof(double))
                {
                    colValues += string.Format("{0},", dt.Rows[i][j]);
                }
                else if (dt.Columns[j].DataType == typeof(DateTime))
                {
                    colValues += string.Format("cast('{0}' as datetime),", dt.Rows[i][j]);
                }
                else if (dt.Columns[j].DataType == typeof(bool))
                {
                    colValues += string.Format("{0},", dt.Rows[i][j].ToString());
                }
                else
                    colValues += string.Format("'{0}',", dt.Rows[i][j]);
            }
            cmd = string.Format(cmdmode, colValues.TrimEnd(','));
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text,cmd);
            }
            catch { function.WriteErrMsg("插入数据失败,请检查与目标数据库的连接或数据表字段的格式！(如数据库字段类型为int插入值为字符串)"); }
        }//for end;
        return true;

    }
    /// <summary>
    /// 返回dt中的指定列,第二个参为true消除重复行，false不消除
    /// </summary>
    public DataTable GetTableByColumnName(DataTable dt,bool flag,params string[] strColumns)
    {
       
            if (dt == null || dt.Columns.Count < 1) { return null; }
            return dt.DefaultView.ToTable(flag, strColumns);
    }
    /// <summary>
    /// 获取dt中的列名
    /// </summary>
    public string[] GetColumnsName(DataTable dt) 
    {
        if (dt == null || dt.Columns.Count < 1) { return (new string[]{""}); }
        string[] s =new string[dt.Columns.Count];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            s[i] = dt.Columns[i].ColumnName;
        }
        return s;
    }
    //-----------------Excel支持
    public void CsvToDataSet(DataSet ds, string csvPath)
    {
        string fileFullName = Path.GetFileName(csvPath);//例:模板.csv
        string folderPath = csvPath.Substring(0, csvPath.LastIndexOf('\\') + 1);//例:E:\Code\Zoomla6x\ZoomLa.WebSite\xls\ 
        string connStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='text;HDR=Yes;IMEX=1'", folderPath);
        string sql = string.Format(@"SELECT * FROM [{0}]", fileFullName);
        OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr);
        da.Fill(ds);
    }
    protected void ExcelData_Click(object sender, EventArgs e)//从Excel导入数据
    {
        if (!CheckFile(fileImp.FileName))//detect the file is xls or xlsx
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('必须是csv或excel文件');", true);
            return;
        }
        string path = ImportExcel();

        ds.ReadXml(Server.MapPath(xmlPath));//nameToField.xml
        ds2.ReadXml(Server.MapPath(xmlPath2));//CRM_Dictionary.xml
        DataTable salesManDT = GetSalesMan();

        //指定路径,表名
        OleDB ol = new OleDB();
        DataTable dt = new DataTable();
        DataSet csvDS = new DataSet();//csv导入需要
        //为dt赋值,根据后缀名,调用不同的方式读取,支持CSV与Xls,xlsx三种格式

        try
        {
            if (System.IO.Path.GetExtension(fileImp.FileName).ToLower().Equals(".csv"))
            {
                CsvToDataSet(csvDS, path);
                dt = csvDS.Tables[0];
            }
            else//处理excel
            {
                dt = OleDB.Select(path, "select * from" + OleDB.SelectTables(path).Rows[0]["Table_Name"]);//读取第一张表中的数据;//读取第一张表中的数据
            }
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Add_Date", typeof(DateTime));
            dt.Columns.Add("FPManID", typeof(string));//或在另一个循环中添加
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //为其全部加上code与日期
                dt.Rows[i]["Code"] = function.GetFileName();
                dt.Rows[i]["Add_Date"] = DateTime.Now;
            }
            for (int i = 0; i < dt.Rows.Count; )//为其加上跟进人员(即为销售人员的管理员的ID)
            {
                for (int j = 0; j < salesManDT.Rows.Count && i < dt.Rows.Count; j++, i++)
                {
                    dt.Rows[i]["FPManID"] = salesManDT.Rows[j]["AdminID"];

                }
            }

            for (int i = 0; i < dt.Columns.Count; i++)//将列名由Excel的中文转为英文
            {
                ds.Tables[0].DefaultView.RowFilter = "name in ('" + dt.Columns[i].ColumnName + "')";//选出XML中的同名标签
                if (ds.Tables[0].DefaultView.ToTable().Rows.Count > 0)//避免有不匹配的
                {
                    dt.Columns[i].ColumnName = (ds.Tables[0].DefaultView.ToTable().Rows[0]["fieldname"].ToString());//替换为英文
                }
            }

            //准备值 1为检测,2为可为空,3为不需要检测
            for (int i = 1; i < ds2.Tables.Count; i++)//先将ds2中的信息汇总成一张表,XML中的标记名称必须和字段一样
            {
                ds2.Tables[0].Merge(ds2.Tables[i]);
            }
            ds.Tables[0].DefaultView.RowFilter = "1=1";
            ds.Tables[0].DefaultView.RowFilter = "needCheck in('1','2')";
            DataTable needCheckDT = ds.Tables[0].DefaultView.ToTable();//使用fieldName来获取table中的数据,要对比的数据来自ds2(directory)
            //开始检测dt中的数据
            try
            {
                for (int i = 0; i < needCheckDT.Rows.Count; i++)
                {
                    string fieldname = needCheckDT.Rows[i]["fieldname"].ToString();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (needCheckDT.Rows[i]["needCheck"].ToString().Equals("2") && string.IsNullOrEmpty(dt.Rows[j][fieldname].ToString()))//为2可空时的检测
                            continue;
                        ds2.Tables[fieldname].DefaultView.RowFilter = "content in('" + dt.Rows[j][fieldname].ToString() + "')";
                        if (ds2.Tables[fieldname].DefaultView.ToTable().Rows.Count < 1)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('[" + needCheckDT.Rows[i]["name"].ToString() + "]第" + (j + 1).ToString() + "行,值不符合规范!');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex) { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('格式不正确" + ex.Message + "');", true); return; }
        }
        catch (NullReferenceException)//避免用户只是改了后缀名方式上传
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('格式不正确,请确定是否以xls或xlsx格式保存!');", true); return;
        }
        //------从Excel获取的表处理完成,开始插入
        ds.Tables[0].DefaultView.RowFilter = "1=1";//清空下
        DataTable tableNameDT = ds.Tables[0].DefaultView.ToTable(true, new string[] { "tablename" });
        for (int i = 0; i < tableNameDT.Rows.Count; i++)//批量向多表导入数据
        {
            string tableName = tableNameDT.Rows[i]["tablename"].ToString(), columns = "";
            ds.Tables[0].DefaultView.RowFilter = "tablename in ('" + tableName + "')";
            for (int j = 0; j < ds.Tables[0].DefaultView.ToTable().Rows.Count; j++)//准备要插入的列
            {
                columns += (ds.Tables[0].DefaultView.ToTable().Rows[j]["fieldname"] as string) + ",";
            }
            columns += "Code,Add_Date";

            DataTable tempDT = GetTableByColumnName(dt, false, columns.Split(','));
            tempDT.TableName = tableName;
            if (WriteDataToDB(tempDT))
            { }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + tableName + "导入失败,请检测Excel中格式是否正确,是否包含非法字符');", true);
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('导入完成');location=location;", true);
    }
    protected void Template_Click(object sender, EventArgs e)//下载模板
    {
        string xmlPath = Server.MapPath("~/manage/AddCRM/nameToFieldName.xml");
        if (!File.Exists(xmlPath)) { function.WriteErrMsg("模板文件不存在"); }
        DataSet ds = new DataSet();
        ds.ReadXml(xmlPath);
        EduceCSV(ds.Tables[0], "name", "模板");
    }
    /// <summary>
    /// 从表中提取字段，返回CSV文件
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fieldName">要提取的字段</param>
    /// <param name="filename">回馈给用户的CSV文件名</param>
    private void EduceCSV(DataTable dt,string fieldName,string filename)
    {
        string str = "";
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            str += dr[fieldName].ToString();
            i = i + 1;
            if (i < dt.Rows.Count)
            {
                str += ",";
            }
        }
        DataGrid dg = new DataGrid();
        dg.DataSource = dt.DefaultView;
        dg.DataBind();

        Encoding gb = System.Text.Encoding.GetEncoding("GB2312");
        StringWriter sw = new StringWriter();
        sw.WriteLine(str);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename) + ".csv");
        Response.ContentType = "Content-Type:application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sw.ToString());
        Response.End();
    }
    /// <summary>
    /// 将dt中的字段名输出至Excel第一行,将其作为模板返回
    /// </summary>
    private void EduceExcel(DataTable dt, string fieldName, string filename)
    {
        string excelPath = Server.MapPath("~/manage/AddCRM/Template.xlsx");
        //OleDB.ExecuteSql(excelPath, "Drop Table " + OleDB.GetFirstTableName(excelPath));//先清空数据,避免其移除了字段后，出现多余的数据
  
        string sql = "CREATE TABLE " + OleDB.GetFirstTableName(excelPath) + "(";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (string.IsNullOrEmpty(dt.Rows[i][fieldName].ToString())) continue;//如果name为空的话，则跳过
            sql += "[" + (dt.Rows[i][fieldName] as string) + "] VarChar,";
        }
        sql = sql.TrimEnd(','); sql += ")"; //function.WriteErrMsg(sql);
        OleDB.ExecuteSql(excelPath, sql);
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename) + ".xlsx");
        Response.ContentType = "Application/ms-excel";
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.WriteFile(excelPath);
        Response.End();
    }
    private string ImportExcel()//上传后保存，返回保存路径
    {
        string fileName = this.fileImp.FileName;
        string path = Server.MapPath("~" + "/xls/");
        if (!FileSystemObject.IsExist(path, FsoMethod.Folder))
        {
            FileSystemObject.CreateFileFolder(path);
        }
        try
        {
            string fpath = path + fileName;
            //SafeSC.SaveFile(path + fileName, fileImp);
            if (!fileImp.SaveAs(fpath)) { function.WriteErrMsg(fileImp.ErrorMsg); }
            return fpath;
        }
        catch
        {
            return "";
        }
    }
    private bool CheckFile(string fileName) 
    {
        bool flag = false;
        string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
        if (fileExtension.Equals(".csv") || fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")) flag = true;
        return flag;
    }

    //-----------------权限模块
    /// <summary>
    /// 获取管理员中是拥有销售人员权限的人列表
    /// </summary>
    /// <returns></returns>
    public DataTable GetSalesMan()
    {
        DataTable dt = new DataTable();
        dt = badmin.Sel();
        string[] brr = crmBll.GetSalesRoles().Split(',');
        for (int i = 0; i < dt.Rows.Count; i++)//开始筛选
        {
            string[] arr = dt.Rows[i]["AdminRole"].ToString().Split(',');
            if (!StringHelper.IsContain(arr, brr)) { dt.Rows.Remove(dt.Rows[i]); }//不是的移除
        }
        return dt;
    }

    DataTable adminDT = new DataTable();
    public string GetAdminTrueName(string id) 
    {

        if (ViewState["adminList"] == null)
            ViewState["adminList"] = badmin.Sel();
            adminDT = ViewState["adminList"] as DataTable;
        try
        {
            adminDT.DefaultView.RowFilter = "AdminID = "+id;
            return adminDT.DefaultView.ToTable().Rows[0]["adminTrueName"].ToString();
        }
        catch { return ""; }
       
    }
}
