using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

public partial class manage_Site_SiteDataCenter : CustomerPageAction
{
   
    protected IISHelper iisHelper = new IISHelper();
    protected ServerManager iis = new ServerManager();
    protected B_Admin badmin = new B_Admin();
    protected DataTableHelper dtHelper = new DataTableHelper();
    protected DataTable commonModelDT = new DataTable(), articleDT = new DataTable();
    protected string serviceUrl = "/api/sitegroupFunc.asmx";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IdentityAnalogue ia = new IdentityAnalogue();
        ia.CheckEnableSA();
        badmin.CheckIsLogin();

        if (function.isAjax())
        {
            string[] urlArr = Request.Form["value"].Split(',');
            string jsonData = "[";
            if (urlArr.Length > 0 && !Request.Form["value"].Contains("式"))
            {
                for (int i = 0; i < urlArr.Length; i++)
                {
                    if (!urlArr[i].ToLower().Contains("http://"))
                        urlArr[i] = "http://" + urlArr[i];
                   string siteName= GetSiteNameByUrl(urlArr[i]);
                   jsonData += "{\"url\":\""+urlArr[i]+"\",\"siteName\":\""+siteName+"\"},";
                }
                jsonData=jsonData.TrimEnd(',');
                jsonData += "]";
                Response.Write(jsonData);
                Response.Flush(); 
            }
            Response.End();
        }

        if (!IsPostBack)
        {
            siteRepeater.DataSource = iisHelper.GetWebSiteList(true);
            siteRepeater.DataBind();
        }
        Call.HideBread(Master);
    }
    /// <summary>
    /// 全部CommonModel
    /// </summary>
    public static DataTable allCDT = new DataTable();
    /// <summary>
    /// 全部Article
    /// </summary>
    public static DataTable allArt = new DataTable();

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //完成下拉列表的绑定与默认值操作
        ListItem item = new ListItem("未匹配节点,请手动选择", "0");   
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["nodeDT"] == null)
            {
                ViewState["nodeDT"] = SqlHelper.ExecuteTable(CommandType.Text, "select NodeName,NodeID from ZL_Node where " +
                 "(NodeListType!=2 and NodeListType!=3) Order by NodeID", null);
                mainDp1.DataSource = ViewState["nodeDT"] as DataTable;
                mainDp1.DataValueField = "NodeID";
                mainDp1.DataTextField = "NodeName";
                mainDp2.DataSource = ViewState["nodeDT"] as DataTable;
                mainDp2.DataValueField = "NodeID";
                mainDp2.DataTextField = "NodeName";
                mainDp1.DataBind();
                mainDp2.DataBind();
                mainDp1.Items.Insert(0, item);
                mainDp2.Items.Insert(0, item);
                mainDp2.SelectedIndex = 1;
            }

            DropDownList list = (DropDownList)e.Row.FindControl("nodeList");
            if (list == null) { return; }

            DataRowView drv = (DataRowView)e.Row.DataItem;

            string tempID = "";

            DataTable nodeDT = new DataTable();
            nodeDT = ViewState["nodeDT"] as DataTable;

            nodeDT.DefaultView.RowFilter = "nodeName in ('" + drv["NodeName1"].ToString() + "')";
            if (nodeDT.DefaultView.ToTable().Rows.Count > 0)
                tempID = nodeDT.DefaultView.ToTable().Rows[0]["NodeID"].ToString();

            nodeDT.DefaultView.RowFilter = "1=1";

            list.DataSource = nodeDT;//完成绑定与默认值
            list.DataValueField = "NodeID";
            list.DataTextField = "NodeName";
            list.DataBind();
            list.Items.Insert(0, item);
            list.SelectedValue = tempID;

        }
    }
    //Disuse
    public bool CheckIsOK(string url)
    {
        try
        {
            Object obj = InvokeWebSer(url + serviceUrl,
              "SiteGroup",  // 欲调用的WebService的命名空间(如你已经引用,则为引用进入的命名空间)
              "SiteGroupFunc",   // 欲调用的WebService的类名（不包括命名空间前缀）
              "ServicesIsOK",    // 欲调用的WebService的方法名
              new object[] { "" });
            return true;
        }
        catch { return false; }
    }
    //Disuse
    protected void siteUrl1_TextChanged(object sender, EventArgs e)
    {

        //string url = ((TextBox)sender).Text.Trim();

        //if (string.IsNullOrEmpty(url)) return;

        //string remind = "";
        //if (CheckIsOK(url))
        //{
        //    remind = "检测成功,可以正常访问";
        //}
        //else
        //{
        //    remind = "失败,无法访问目标服务器";
        //}
        //if (((TextBox)sender).ID.Equals("siteUrl1"))
        //{
        //    siteLabel1.Text = remind;
        //}
        //else
        //    siteLabel2.Text = remind;

    }

    public string GetDomain(string domain,string port)
    {
        string result = "";
        string ip = string.IsNullOrEmpty(StationGroup.DefaultIP) ? "127.0.0.1" : StationGroup.DefaultIP;//未填写默认IP,则以127.0.0.1获取
        if (string.IsNullOrEmpty(domain))
        {
            result = "http://"+ip+":" + port+"/";
        }
        else if (domain.ToLower().Equals("localhost"))
        { 
            result = "http://"+ip+":" + port + "/"; 
        }
        else
        {
            result = "http://" + domain + ":" + port + "/";
        }
        return result;
    }
    //开始获取数据,单击事件
    protected void beginGet_Click(object sender, EventArgs e)
    {
        //传上值,格式:http://www.z01.com/
        // //未选择,或未输入,不操作
        if (string.IsNullOrEmpty(Request.Form["siteChk"]) && (string.IsNullOrEmpty(urlText.Text.Trim()) || urlText.Text.Trim().Contains("式")))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选择要获取数据的网址!!');", true);
            return;
        }
        string[] urlArr;
        if (!string.IsNullOrEmpty(urlText.Text.Trim()) && !urlText.Text.Trim().Contains("式"))//如果不为空,并且不是默认提示信息
        {
            string temp1 = "";
            string[] temp = urlText.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < temp.Length; i++)
            {
                if(!temp[i].ToLower().Contains("http://"))
                temp[i] = "http://" + temp[i] + "/";//为手输的地址加上http://
                temp1 += temp[i]+",";
            }
            urlArr = (temp1+ Request.Form["siteChk"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            urlArr = Request.Form["siteChk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        ViewState["urlArr"] = urlArr;
        allCDT.Clear();//清空静态表，开始存放数据
        allArt.Clear();
        if (!EGV.Visible)
        {
            EGV.Visible = true;
            EGV2.Visible = false;
            logBtn.Text = "查看日志";
        }
        foreach (string s in urlArr)
        {
            BeginGetData(s);
        }
        EGV.DataSource = allCDT;
        EGV.DataBind();
        count.Text = "获取到"+EGV.Rows.Count+"篇文章";
    }
    //开始获取数据,业务逻辑
    public void BeginGetData(string url)
    {
        try
        {
            //分为三步,获取,插入,回发成功通知
            object obj=new object(), obj2=new object();
            obj = InvokeWebSer(url + serviceUrl,
                            "SiteGroup",  // 欲调用的WebService的命名空间(如你已经引用,则为引用进入的命名空间)
                            "SiteGroupFunc",   // 欲调用的WebService的类名（不包括命名空间前缀）
                            "GetDataTable",    // 欲调用的WebService的方法名
                            new object[] { "SelAllArticle" });
            articleDT = obj as DataTable;
            

            obj2 = InvokeWebSer(url + serviceUrl,
             "SiteGroup", "SiteGroupFunc", "GetDataTable",
             new object[] { "GetAllCommonModel" });
             commonModelDT = obj2 as DataTable;

             if (articleDT == null || articleDT.Rows.Count < 1 || commonModelDT == null || commonModelDT.Rows.Count < 1) return;
            articleDT.TableName = "ZL_C_Article";
            commonModelDT.TableName = "ZL_CommonModel";

            commonModelDT.Columns.Add("SiteSource", typeof(string));
            for (int i = 0; i < commonModelDT.Rows.Count; i++)
            {
                commonModelDT.Rows[i]["SiteSource"] = url;
            }

            allCDT.Merge(commonModelDT);
            allArt.Merge(articleDT);
            string siteName = "";siteName=GetSiteNameByUrl(url);
            
            remind.Text = "<a href=\"javascript:window.open('" + @url + "');void(0);\" style='color:green;margin-top:5px;' title='点击浏览'>" + (string.IsNullOrEmpty(siteName) ? url : siteName) +"(获取成功)</a><br />"+remind.Text;
        }
        catch(Exception) 
        {
            remind.Text = "<span style='color:red;margin-top:5px;' title='请检查目标站点是否逐浪站点,网址是否正确!'>" + url + "获取失败(鼠标移入查看提示)</span><br />"+remind.Text;
        }
    }
    //加入数据库
    protected void btn1_Click(object sender, EventArgs e)
    {
        allArt.TableName = "ZL_C_Article";
        allCDT.TableName = "ZL_CommonModel";
        string[] urlArr= ViewState["urlArr"] as string[];
        //将EGV所做的更改,更新到数据源
        DropDownList list = new DropDownList();
        for (int i = 0; i < EGV.Rows.Count; i++)
        {
            list = (DropDownList)EGV.Rows[i].FindControl("nodeList");

            allCDT.Rows[i]["NodeID"] = list.SelectedValue;
            allCDT.Rows[i]["NodeName1"] = list.SelectedItem.Text; 
            allCDT.Rows[i]["IsCatch"] = "2";//表示是从子站获取的数据
        }
        //Bug问题，我只需要更新两条，但对方把所有的数据都弄过来了
        dtHelper.WriteDataToDB(allArt, dtHelper.GetTaleStruct("ZL_C_Article"), allCDT);
        dtHelper.WriteDataToDB(allCDT, dtHelper.GetTaleStruct("ZL_CommonModel"));
        UpdateStatus(urlArr);
        WriteLog();//写入日志
        allCDT.Clear();
        allArt.Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location=location;", true);
        
    }
    //回发信息，更新子站列表状态
    public void UpdateStatus(params string[] urlArr)
    {
        DataTable urlDT = allCDT.DefaultView.ToTable(true, "SiteSource");
        string ids = "";
        //try
        //{
            //生成成功语句,要求目标执行,主体都在客户端,服务端只管执行语句,返回结果即可
            for (int i = 0; i < urlDT.Rows.Count; i++)
            {
                ids = "";

                allCDT.DefaultView.RowFilter = "SiteSource in ('" + urlDT.Rows[i]["SiteSource"] + "')";
                foreach (DataRow dr in allCDT.DefaultView.ToTable().Rows)
                {
                    ids += dr[0].ToString() + ",";
                }
                ids = ids.TrimEnd(',');
                if (string.IsNullOrEmpty(ids)) continue;//如果数据为空则不执行后面的语句

                InvokeWebSer(urlDT.Rows[i]["SiteSource"].ToString() + serviceUrl,
                                "SiteGroup", "SiteGroupFunc", "UpdateStatus", new object[] { ids });
            }
        //}
        //catch { throw (new Exception(urlDT.Rows[0]["SiteSource"].ToString() + serviceUrl)); }
    }
    protected string GetSiteNameByUrl(string url)
    {
        string result = "";
        try
        {
            object s = InvokeWebSer(url + serviceUrl, "SiteGroup", "SiteGroupFunc", "GetSiteName", new object[] { });
            result = s.ToString();
        }
        catch { }
        return result;
    }
    /*
     * 优点:不用引用,通过网址,目标命名空间,调用WebServices,用于主站向子站请求数据
     * 缺点:使用上不如直接Web引用进来易用,如果只是单个并确定的,最好引用进来
     */
    /// <summary>
    /// 通过反射完成调用
    /// </summary>
    public static object InvokeWebSer(string url, string @namespace, string classname, string methodname, object[] args)
    {
        System.Net.WebClient wc = new System.Net.WebClient();
        string URL = string.Empty;
        if ((url.Substring(url.Length - 5, 5) != null) && ((url.Substring(url.Length - 5, 5).ToLower() != "?wsdl")))
            URL = url + "?WSDL";
        else
            URL = url;
        System.IO.Stream stream = wc.OpenRead(URL);
        System.Web.Services.Description.ServiceDescription sd = System.Web.Services.Description.ServiceDescription.Read(stream);
        System.Web.Services.Description.ServiceDescriptionImporter sdi = new System.Web.Services.Description.ServiceDescriptionImporter();
        sdi.AddServiceDescription(sd, "", "");
        System.CodeDom.CodeNamespace cn = new System.CodeDom.CodeNamespace(@namespace);
        System.CodeDom.CodeCompileUnit ccu = new System.CodeDom.CodeCompileUnit();
        ccu.Namespaces.Add(cn);
        sdi.Import(cn, ccu);

        Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();

        System.CodeDom.Compiler.CompilerParameters cplist = new System.CodeDom.Compiler.CompilerParameters();
        cplist.GenerateExecutable = false;
        cplist.GenerateInMemory = true;
        cplist.ReferencedAssemblies.Add("System.dll");
        cplist.ReferencedAssemblies.Add("System.XML.dll");
        cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
        cplist.ReferencedAssemblies.Add("System.Data.dll");

        System.CodeDom.Compiler.CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);

        if (true == cr.Errors.HasErrors)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
            {
                sb.Append(ce.ToString());
                sb.Append(System.Environment.NewLine);
            }
            throw new Exception(sb.ToString());
        }
        System.Reflection.Assembly assembly = cr.CompiledAssembly;
        Type t = assembly.GetType(@namespace + "." + classname, true, true);
        object obj = Activator.CreateInstance(t);
        System.Reflection.MethodInfo mi = t.GetMethod(methodname);
        return mi.Invoke(obj, args);
    }
    //---------------------------------保存日志
    string path = "~/UploadFiles/Log/";
    string logName = "SiteLog.xml";
    /// <summary>
    /// 写入日志文件,并清空
    /// </summary>
    protected void WriteLog()
    {
        string ppath = Server.MapPath(path + logName);
        DataTable logDT = new DataTable();
        logDT = allCDT.DefaultView.ToTable(false, "SiteSource", "Title", "NodeID", "NodeName1");
        logDT.TableName = "SiteLog";
        logDT.Columns.Add("Date", typeof(string));
        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(path);
        }

        for (int i = 0; i < logDT.Rows.Count; i++)
        {
            logDT.Rows[i]["Date"] = DateTime.Now;
        }
        if (File.Exists(ppath))//如果有日志文件存在,则先载入日志文件
        {
            logDT.Merge(dtHelper.DeserializeDataTable(ppath, true));
            //如果需要限制数据条目或删除1个月之前的数据,在此处理
        }
        dtHelper.SerializeDataTableXml(logDT, ppath);
    }
    protected void logBtn_Click(object sender, EventArgs e)
    {
        if (EGV.Visible)
        {
            EGV.Visible = false;
            EGV2.Visible = true;
            logBtn.Text = "后退";
        }
        else
        {
            Response.Redirect("siteDataCenter.aspx");
        }
    }
}