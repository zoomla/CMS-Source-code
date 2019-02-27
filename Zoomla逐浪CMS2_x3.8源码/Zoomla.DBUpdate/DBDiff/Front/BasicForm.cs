using DBDiff.BLL;
using DBDiff.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace DBDiff.Front
{
    public partial class BasicForm : Form
    {
        public string tempstr = ""; //错误提示信息
        public string ConnectionString;
        public int stratnum = 1;
        public int DataType = 0;
        public bool cls = true;

        #region 委托类
        public delegate void SetTextBoxValue(string value);
        void SetMyTextBoxValue(string value)
        {
            if (this.msginfo.InvokeRequired)
            {
                SetTextBoxValue objSetTextBoxValue = new SetTextBoxValue(SetMyTextBoxValue);
                IAsyncResult result = this.msginfo.BeginInvoke(objSetTextBoxValue, new object[] { value });
                try
                {
                    objSetTextBoxValue.EndInvoke(result);
                }
                catch
                {
                }
            }
            else
            {
                this.msginfo.Text += value + Environment.NewLine;
                this.msginfo.SelectionStart = this.msginfo.TextLength;
                this.msginfo.ScrollToCaret();
            }
        }
        #endregion
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void msginfo_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
            Application.Exit();
            Application.ExitThread();
        }
        //---------------------------------
        #region
        public BasicForm()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void SqlVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SqlVersion.SelectedIndex == 1)
            {
                SqlVersion.SelectedIndex = 0;
                MessageBox.Show("对不起，SQL2000版仅针对商业订制版提供服务，点确定进放官网授权页面！");
                System.Diagnostics.Process.Start("https://www.z01.com/corp/about/151.shtml");
            }
            else if (SqlVersion.SelectedIndex == 2)
            {
                MessageBox.Show("对不起，Orcle版本仅针对商业订制版提供服务，点确定进放官网授权页面！");
                System.Diagnostics.Process.Start("https://www.z01.com/corp/about/151.shtml");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msginfo.Text = "";
            msginfo.Text = "准备就绪！请输入上面的信息";
            SqlVersion.SelectedIndex = 0;
            this.Show();
        }
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            string cc = "";
            if (cls == true)
            {
                string closeinfo = e.CloseReason.ToString();
                if (closeinfo == "UserClosing")
                {
                    cc = MessageBox.Show("确定退出吗?", "退出询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question).ToString();
                    switch (cc)
                    {
                        case "Yes":
                            cls = false;
                            break;
                        case "No":
                            cls = true;
                            break;
                        case "Cancel":
                            cls = true;
                            break;
                        default:
                            cls = true;
                            break;
                    }
                }
                e.Cancel = cls;
            }
        }
        #region 界面参数
        private void menu_DropDownClosed(object sender, EventArgs e)
        {
            menu.ForeColor = System.Drawing.Color.White;
        }

        private void menu_DropDownOpened(object sender, EventArgs e)
        {
            menu.ForeColor = System.Drawing.Color.SteelBlue;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.z01.com");
        }

 
        #endregion
        #endregion
        //----文件
        private string viewPath = Application.StartupPath + "\\view.sql";
        private string localdbPath = Application.StartupPath + "\\SqlLocalDB.MSI";
        private void BeginUpdateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserID.Text.Trim()) || string.IsNullOrEmpty(Password.Text.Trim()))
            {
                MessageBox.Show("用户名与密码不能为空");
                return;
            }
            //else if (dbList.Text.Equals("数据库连接失败")||dbList.Text.Equals("选择数据库"))
            //{
            //     MessageBox.Show("请先选择数据库");
            //    return;
            //}
            else if (string.IsNullOrEmpty(dbText.Text.Trim()))
            {
                MessageBox.Show("数据库不能为空");
                return;
            }
            if (MessageBox.Show("建议先行备份(如为空将进初始化安装)...", "确认升级!", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            msginfo.ForeColor = Color.Gray;
            msginfo.Text = "开始初始化";//初始化和禁用控件,如数据库列表,开始与停止Button
            updateBar.Value = 0;
            updateBar.Value = 10;
            dbText.Enabled = false;
            beginUpdateBtn.Enabled = false;
            exitBtn.Enabled = false;

            this.setLabelText = this.SetLableText;//为委拖赋值
            this.setUpdateBar = this.SetUpdateBarFunc;
            Thread t = new Thread(BeginWork);//线程版
            t.Start();
        }
        public delegate void setLabelTextDelegate(string infor);
        public delegate void UpdateBar(int value);
        private setLabelTextDelegate setLabelText;
        private UpdateBar setUpdateBar;
        private void SetLableText(string info)
        {
            this.msginfo.Text = info;
        }
        private void SetUpdateBarFunc(int value)
        {
            this.updateBar.Value = value;
        }
        // 进行比较前设置,生成脚本,创建数据库
        public void BeginWork(object res)
        {
            try
            {
                this.msginfo.Invoke(this.setLabelText, new object[] { "开始检测数据库" });
                this.updateBar.Invoke(this.setUpdateBar, new object[] { 35 });
                this.updateBar.Invoke(this.setUpdateBar, new object[] { 50 });
                this.updateBar.Invoke(this.setUpdateBar, new object[] { 65 });
                UpdateDB();
                this.updateBar.Invoke(this.setUpdateBar, new object[] { 100 });
                this.msginfo.Invoke(this.setLabelText, new object[] { "恭喜,数据库升级完成!" });
            }
            finally
            {
                dbText.Enabled = true;
                beginUpdateBtn.Enabled = true;
                exitBtn.Enabled = true;
            }
        }
        private bool UpdateDB()
        {
            SqlHelper.ConnectionString = "Data Source=" + DataSource.Text.Trim() + ";Initial Catalog=" + dbText.Text.Trim() + ";User ID=" + UserID.Text.Trim() + ";Password=" + Password.Text.Trim();
            M_SQL_Connection connMod = new M_SQL_Connection() { constr = SqlHelper.ConnectionString };
            #region 先检测无模型的表
            {
                List<M_DB_Table> tables = new List<M_DB_Table>();
                string[,] tableFields = { { "GroupID", "Int", "4" }, { "UserModel", "Int", "4" } };
                tables.Add(new M_DB_Table("ZL_GroupModel", "", tableFields));
                foreach (M_DB_Table tableMod in tables)
                {
                    CheckAndRepairTable(connMod, tableMod);
                }
            }

            #endregion
            try
            {
                #region 表与字段
                string ignoreStr = "M_Base,M_Label,M_DataList,M_SQLField,M_PageSkins,M_Plat_CompDoc,M_Client_Service,M_Subscribe,M_UserBank";
                string baseType = "ZoomLa.Model.M_Base,ZoomLa.Model.M_Baike";//百科支持版本控制,所以有一个子类
                string[] ignore = ignoreStr.Split(',');
                Assembly models = Assembly.Load("ZoomLa.Model");//装载程序集,通常是以dll为单位
                foreach (Type model in models.GetTypes())
                {
                    if (!model.IsClass) { continue; }
                    if (!baseType.Contains(model.BaseType.FullName)) { continue; }
                    if (ignore.Contains(model.Name)) { continue; }
                    string assembleName = model.Assembly.GetName().Name;
                    string fullName = model.Namespace + "." + model.Name;
                    M_Base classMod = ReflectionHelper.CreateInstance<M_Base>(fullName, assembleName);
                    string[,] fields = classMod.FieldList();
                    if (!string.IsNullOrEmpty(classMod.UP_Tables))
                    {
                        foreach (string tbname in classMod.UP_Tables.Split('|'))
                        {
                            M_DB_Table tableMod = new M_DB_Table(tbname, classMod.PK, fields);
                            CheckAndRepairTable(connMod, tableMod);
                        }
                    }
                    else
                    {
                        M_DB_Table tableMod = new M_DB_Table(classMod.TbName, classMod.PK, fields);
                        CheckAndRepairTable(connMod, tableMod);
                    }
                }
                #endregion
                #region 视图
                StreamWriter sw = new StreamWriter(viewPath, false, Encoding.UTF8);
                sw.Write(Properties.Resources.DataView);
                sw.Close();
                //删除指定视图
                string[] views = ("ZL_User_PlatView,ZL_Guest_BarView,ZL_SearchView,ZL_Order_ShareView,ZL_Exam_ClassView,ZL_Plat_BlogView"+
                    ",ZL_Order_ProSaleView,ZL_Order_PayedView").Split(',');
                foreach (string view in views)
                {
                    try { ExcuteSQL("DROP VIEW " + view, SqlHelper.ConnectionString); }
                    catch { }
                }
                ExecuteSqlScript(SqlHelper.ConnectionString, viewPath);
                //--------------数据维护
                {
                    DataTable dt = SqlHelper.ExecuteTable("SELECT ID FROM ZL_Third_PlatInfo");
                    if (dt.Rows.Count < 1)
                    {
                        string insertSql="";
                        foreach(string flag in "版权印,飞印打印,百度翻译".Split(','))
                        {
                            insertSql += " INSERT INTO [ZL_Third_PlatInfo] ([Name],[APPKey],[APPSecret],[UserName],[UserPwd],[Flag],[ZStatus],[CDate]) VALUES ( N'" + flag + "',N'',N'',N'',N'',N'" + flag + "',0,GETDATE());";
                        }
                        SqlHelper.ExecuteSql(insertSql);
                    }
                }
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Owner, "无法连接到目标数据库" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        /// <summary>
        /// 补充缺失的表和字段
        /// </summary>
        /// <param name="connMod">连结模型</param>
        /// <param name="tableMod">表模型,主键,表名,字段列表</param>
        public static void CheckAndRepairTable(M_SQL_Connection connMod, M_DB_Table tableMod)
        {
            if (string.IsNullOrEmpty(tableMod.TbName)) { return; }
            connMod.tbname = tableMod.TbName;
            M_SQL_Field field = new M_SQL_Field();
            try
            {
                if (!DBHelper.Table_IsExist(connMod))
                {
                    #region 表不存在则新建
                    bool hasPK = false;
                    string sql = "CREATE TABLE [dbo].[" + tableMod.TbName + "]({0})ON [PRIMARY]";
                    string fieldSql = "";
                    for (int i = 0; i < tableMod.FieldList.GetLength(0); i++)
                    {
                        field = new M_SQL_Field()
                        {
                            fieldName = tableMod.FieldList[i, 0].Trim(),
                            fieldType = tableMod.FieldList[i, 1],
                            fieldLen = DataConvert.CLng(tableMod.FieldList[i, 2])
                        };
                        fieldSql += GetFieldSql(field);
                        if (!hasPK && field.fieldName.ToLower().Equals(tableMod.PK.ToLower()))
                        {
                            hasPK = true;
                            //主键为int则自增长
                            if (field.fieldType.ToLower().Equals("int"))
                            {
                                fieldSql += " IDENTITY(1,1) NOT NULL";
                            }
                        }
                        fieldSql += ",";
                    }
                    //设置主键
                    if (hasPK)
                    {
                        fieldSql += "CONSTRAINT [PK_" + tableMod.TbName + "] PRIMARY KEY CLUSTERED ([" + tableMod.PK + "] ASC)ON [PRIMARY]";
                    }
                    sql = string.Format(sql, fieldSql.TrimEnd(','));
                    DBHelper.ExecuteSQL(SqlHelper.ConnectionString, sql);
                    #endregion
                }
                else
                {
                    #region 表存在则对比
                    DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM " + tableMod.TbName + " WHERE 1=2");
                    for (int i = 0; i < tableMod.FieldList.GetLength(0); i++)
                    {
                        field = new M_SQL_Field()
                        {
                            fieldName = tableMod.FieldList[i, 0].Trim(),
                            fieldType = tableMod.FieldList[i, 1],
                            fieldLen = DataConvert.CLng(tableMod.FieldList[i, 2])
                        };
                        //暂时只补充字段,不修改类型
                        if (!dt.Columns.Contains(field.fieldName))
                        {
                            DBHelper.Table_AddField(connMod, field);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex) { MessageBox.Show("更新表:[" + tableMod.TbName + "]时出错,原因:" + ex.Message); }
        }
        public static string GetFieldSql(M_SQL_Field field)
        {
            //[ID] [int] IDENTITY(1,1) NOT NULL,
            string sql = "[" + field.fieldName + "] ";
            switch (field.fieldType.ToLower())
            {
                case "smallint":
                case "int":
                case "money":
                case "ntext":
                case "text":
                case "bit":
                case "smalldatetime":
                case "datetime":
                case "image":
                    sql += "[" + field.fieldType + "]";
                    break;
                default:
                    sql += "[" + field.fieldType + "]" + "(" + field.fieldLen + ") ";
                    break;
            }
            if (!string.IsNullOrEmpty(field.defval)) { sql += " DEFAULT ('" + field.defval + "')"; }
            return sql;
        }
        //数据库下拉列表(disuse)
        private void dbList_Click(object sender, EventArgs e)
        {
            try
            {
                dbList.DataSource = GetDBList(@"Data Source=" + DataSource.Text.Trim() + ";User ID=" + UserID.Text.Trim() + ";Password=" + Password.Text.Trim());// Integrated Security = SSPI;
                dbList.DisplayMember = "Name";
            }
            catch
            {
                //绑定后不能清除与添加,必须先清除绑定
                dbList.DataSource = null;
                dbList.Items.Clear();
                dbList.Items.Add("数据库连接失败");
                dbList.SelectedIndex = 0;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SiteList form = new SiteList();
            form.Show();
        }
        //------------------------DBHelper
        //string mdfSource = Application.StartupPath + "\\ZcmdNewData.mdf";
        //string logSource = Application.StartupPath + "\\ZcmdNewData_log.ldf";
        //AttachDB(mdfSource, logSource, "ZcmdNewData");
        //-----------------数据库相关支持类
        /// <summary>
        /// 数据库是否存在,True存在
        /// </summary>
        public bool Exist(string dbName, string ConnectionString)
        {
            bool flag = false;
            DataTable dt = GetDBList(ConnectionString);
            dt.DefaultView.RowFilter = "name in ('" + dbName + "')";
            if (dt.DefaultView.ToTable().Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 获取DBM中的数据库列表,name,database_id
        /// </summary>
        private DataTable GetDBList(string ConnectionString)
        {
            DataTable dt = new DataTable();
            String connectionString = ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT name,database_id FROM sys.databases ORDER BY Name", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        dt.Load(reader);
                    }
                }
                return dt;
            }
        }
        /// <summary>
        /// 附加数据库
        /// </summary>
        public void AttachDB(string mdfSource, string logSource, string dbName)
        {
            string sql = "exec sp_attach_db @dbname='" + dbName + "',@filename1='" + mdfSource + "',@filename2='" + logSource + "'";
            string strcon = "Server=(local);Integrated Security=SSPI;Initial Catalog=master";
            SqlConnection cn = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        /// <summary>
        /// 创建数据库
        /// </summary>
        protected void CreateDatabase(string dbName, string userID, string passWD)
        {
            string datasourcesa = "(local)";//数据库源
            string datanamesa = dbName;//数据库名称
            string usernamesa = userID;//数据库管理员名称
            string userpwdsa = passWD;//管理员密码
            string dataname1 = dbName;//数据库名称
            //string connstr1 = "Data Source=" + datasourcesa + ";Initial Catalog=" + datanamesa + ";User ID=" + usernamesa + ";Password=" + userpwdsa;
            // SqlConnection connsa1 = Install.Connection(connstr1);
            //创建数据库
            string connectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false", datasourcesa, usernamesa, userpwdsa, "master");
            string commandText = string.Format("CREATE DATABASE [{0}]", datanamesa);
            try
            {
                ExcuteSQL(commandText, connectionString);//执行创建数据库的TSQL；
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 执行SQL语句，用来测试指定数据库是否存在
        /// </summary>
        protected bool ExcuteSQL(string commandText, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, connection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 执行SQL脚本
        /// </summary>
        public bool ExecuteSqlScript(string connectString, string fileName)
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        StringBuilder builder = new StringBuilder();
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine();
                            if (!string.IsNullOrEmpty(str) && str.ToUpper().Trim().Equals("GO"))
                            {
                                break;
                            }
                            builder.AppendLine(str);
                        }
                        command.CommandType = CommandType.Text;
                        command.CommandText = builder.ToString();
                        command.CommandTimeout = 300;
                        //[important],是否抛出异常
                        command.ExecuteNonQuery();

                    }
                }
                catch (SqlException ex)//调试时抛出异常
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
            return true;
        }
        public DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Result");
                    cmd.Parameters.Clear();
                    if (dataSet.Tables.Count > 0)
                    {
                        return dataSet.Tables["Result"];
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 给命令分配一个数据库连接.
            command.Connection = connection;

            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;

            // 设置命令类型.
            command.CommandType = commandType;

            // 分配命令参数
            if (commandParameters != null)
            {
                //AttachParameters(command, commandParameters);
            }
            return;
        }
        //附加升级
        private void InstallDB_Btn_Click(object sender, EventArgs e)
        {
            //InstallDB_Btn.Enabled = false;
            //修改配置
            Microsoft.Web.Administration.ServerManager iis = new Microsoft.Web.Administration.ServerManager();
            IISHelper iisHelp = new IISHelper();
            DataTable dt = new DataTable();
            try { dt = iisHelp.GetWebSiteList(true); }
            catch { MessageBox.Show("读取数据失败,请以[管理员身份运行]升级工具"); return; }
            foreach (DataRow dr in dt.Rows)
            {
                string siteName = dr["SiteName"].ToString();
                try
                {
                    string poolName = iis.Sites[siteName].Applications[0].ApplicationPoolName;
                    Microsoft.Web.Administration.ApplicationPool poolMod = iis.ApplicationPools[poolName];
                    poolMod.ProcessModel.IdentityType = Microsoft.Web.Administration.ProcessModelIdentityType.LocalSystem;
                    poolMod.ManagedPipelineMode = Microsoft.Web.Administration.ManagedPipelineMode.Integrated;
                    poolMod.ManagedRuntimeVersion = "v4.0";
                    iis.CommitChanges();
                }
                catch { MessageBox.Show("站点:[" + siteName + "]无法访问"); return; }
            }
            //开始执行安装
            ExecBatCommand(Application.StartupPath, p =>
            {
                p(@"msiexec /i " + localdbPath);
            }, null, 2);
            //InstallDB_Btn.Enabled = true;
        }
        public static void ExecBatCommand(string workdir, Action<Action<string>> inputAction, DataReceivedEventHandler DataReceived, int exitTime)
        {
            Process pro = null;
            StreamWriter sIn = null;
            StreamReader sOut = null;
            try
            {
                pro = new Process();
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.CreateNoWindow = true;
                pro.StartInfo.WorkingDirectory = workdir.TrimEnd('\\');// @"C:\APPTlp";//等于其开始的默认位置,必须指定,cd跳转无效
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;

                if (DataReceived != null)
                {
                    pro.OutputDataReceived += DataReceived; //(sender, e) => Console.WriteLine(e.Data);
                }
                //pro.ErrorDataReceived += (sender, e) => ZLLog.L("命令行消息返回:" + e.Data);
                pro.Start();
                sIn = pro.StandardInput;
                sIn.AutoFlush = true;

                pro.BeginOutputReadLine();
                inputAction(value => sIn.WriteLine(value));
                //如到期未停止,则强行终止
                if (exitTime < 1) exitTime = 10;
                pro.WaitForExit(exitTime * 1000);

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                if (pro != null && !pro.HasExited)
                    pro.Kill();
                if (sIn != null)
                    sIn.Close();
                if (sOut != null)
                    sOut.Close();
                if (pro != null)
                    pro.Close();
            }
        }

        private void Config_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.RestoreDirectory = false;
            diag.Title = "请选择数据库连接文件";
            diag.Filter = "配置文件|*.config";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ppath = diag.FileName;
                    XmlDocument xmlDoc = new XmlDocument();
                    if (!File.Exists(ppath)) { MessageBox.Show("指定的文件不存在"); return; }
                    xmlDoc.Load(ppath);
                    XmlNodeList nodeList = xmlDoc.SelectNodes("//connectionStrings/add");
                    if (nodeList.Count < 1) { MessageBox.Show("配置文件格式不正确"); }
                    foreach (XmlNode node in nodeList)
                    {
                        if (node.Attributes["name"].Value.Equals("Connection String", StringComparison.InvariantCulture))
                        {
                            string connStr = node.Attributes["connectionString"].Value;
                            if (!connStr.Contains("Data Source") && !string.IsNullOrEmpty(connStr))
                            {
                                connStr = AESDecrypt(connStr);
                            }
                            DataSource.Text = DBHelper.GetAttrByStr(connStr, "Data Source");
                            dbText.Text = DBHelper.GetAttrByStr(connStr, "Initial Catalog");
                            UserID.Text = DBHelper.GetAttrByStr(connStr, "User ID");
                            Password.Text = DBHelper.GetAttrByStr(connStr, "Password");
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("取消加载,原因:"+ex.Message); }
            }
        }
        private static byte[] aes_defiv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string AESDecrypt(string cryedText)
        {
            string key = "aeohheaqhcprswzq";
            if (string.IsNullOrEmpty(cryedText)) { return ""; }
            int length = (cryedText.Length / 2);
            byte[] cipherText = new byte[length];
            for (int index = 0; index < length; index++)
            {
                string substring = cryedText.Substring(index * 2, 2);
                cipherText[index] = Convert.ToByte(substring, 16);
            }
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = aes_defiv;
            byte[] decryptBytes = new byte[cipherText.Length];
            MemoryStream ms = new MemoryStream(cipherText);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(decryptBytes, 0, decryptBytes.Length);
            cs.Close();
            ms.Close();
            return System.Text.Encoding.UTF8.GetString(decryptBytes);
        }

        private void 官方网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.z01.com");
        }

        private void 数据工具Menu_Click(object sender, EventArgs e)
        {
            Process.Start("http://help.z01.com/decry.aspx");
        }

        private void 技术论坛Menu_Click(object sender, EventArgs e)
        {
            Process.Start("http://bbs.z01.com");
        }

        private void 程序下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://z01.com/pub");
        }

        private void 主机服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://z01.com/yun");
        }

        private void 商业购买ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.z01.com/corp/about/listpage.shtml");
        }

        private void 移动开发ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://v.z01.com");
        }
    }
}
