using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Collect
{
    public class B_Coll_Worker
    {
        public static string CollectLog = "";//采集日志
        public static bool StopColl = false;
        private B_Content conBll = new B_Content();
        private B_Create bc;
        private HtmlHelper htmlHelper = new HtmlHelper();
        private Thread m_CreateThread;
        private System.Timers.Timer MT;
        //private double Interval = 1000 * 10;//10秒
        private double Siteworktime = 1000 * 5;//3秒 
        private int AllInNum = 0;//当前采集数量
        private string basePath = AppDomain.CurrentDomain.BaseDirectory;
        private string makePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\MakeSendList.config";//生成
        private B_CollectionItem citemBll = new B_CollectionItem();
        public B_Coll_Worker()
        {
             try
            {
                bc = new B_Create();
            }
            catch { }
        }
        #region 线程操作
        /// <summary>
        /// 创建过程
        /// </summary>
        public void CreateCollectionProc()
        {
            //if (System.Web.HttpContext.Current.Application["MailSends"] == null)
            //{
            //    this.StartCreate();
            //    CreateId = Guid.NewGuid().ToString();
            //    System.Web.HttpContext.Current.Application["MailSends"] = this;
            //}
            //else
            //{
            //    B_MailManage dd = (B_MailManage)System.Web.HttpContext.Current.Application["MailSends"];
            //    System.Threading.Thread tdd = dd.CreateThread;
            //    if (tdd.ThreadState.ToString() == "Stopped")
            //    {
            //        System.Web.HttpContext.Current.Application["MailSends"] = null;
            //        this.StartCreate();
            //        CreateId = Guid.NewGuid().ToString();
            //        System.Web.HttpContext.Current.Application["MailSends"] = this;
            //    }
            //    //StartCreate
            //}
        }

        /// <summary>
        /// 建立新的线程
        /// </summary>
        private void StartCreate()
        {
            this.m_CreateThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.Work));

            this.m_CreateThread.IsBackground = true;
            try
            {
                this.m_CreateThread.Start();
            }
            catch
            {
            }
            finally
            {
                this.m_CreateThread.Abort();
            }
        }

        public Thread CreateThread
        {
            get
            {
                return this.m_CreateThread;
            }
            set
            {
                this.m_CreateThread = value;
            }
        }

        public void Work()
        {
            try
            {
                Senders();
                //Sitework();
            }
            catch
            {
            }
            finally
            {
                //Thread.Sleep(0);
            }
        }

        private void Sitework()
        {
            MT = new System.Timers.Timer(Siteworktime);
            MT.Elapsed += new System.Timers.ElapsedEventHandler(SiteWorkEvent);
            MT.Enabled = true;
        }

        private void SiteWorkEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            //开始工作
            //try
            //{
            //    MT.Interval = 999999;//暂停定时器
            //    SiteWorkput();
            //}
            //finally
            //{
            //    MT.Interval = Interval;//重启启动定时器
            //}
        }

        private void Senders()
        {
            //MT = new System.Timers.Timer(Interval);
            //MT.Elapsed += new System.Timers.ElapsedEventHandler(MTimedEvent);
            //MT.Enabled = true;
        }

        public void SiteWorkput()
        {
            //B_SendList bsl = new B_SendList();
            //bsl.SendAll();
        }
        //---------------生成静态HTML,用于内容发布生成,等(内容管理处应该也是这),改为用静态变量,便于获取其状态
        public static Thread makefilethread;

        protected static string m_makefileing = "";
        public static string makefileing
        {
            set { m_makefileing = value; }
            get { return m_makefileing; }
        }

        protected static int m_makefilenum;
        public static int makefilenum
        {
            set { m_makefilenum = value; }
            get { return m_makefilenum; }
        }
        protected static IList<string> m_makefilelist = new List<string>();
        public static IList<string> makefilelist
        {
            set { m_makefilelist = value; }
            get { return m_makefilelist; }
        }
        /// <summary>
        /// 生成文件
        /// </summary>
        public void MakeFile()
        {
            makefileing = "";
            makefilenum = 0;
            #region 生成文件
            if (FileSystemObject.IsExist(makePath, FsoMethod.File))//判断是否存在任务文件
            {
                string SaleList = FileSystemObject.ReadFile(makePath);
                if (!string.IsNullOrEmpty(SaleList))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(SaleList);
                    XmlNode docxn = doc.SelectSingleNode("xml");
                    XmlNodeList doclist = docxn.ChildNodes;

                    //判断生成XML中是否有内容
                    if (doclist.Count > 0)
                    {
                        foreach (XmlNode xn in doclist)
                        {
                            XmlElement xe = (XmlElement)xn;
                            XmlNodeList xmt = xe.SelectNodes("Maketime");//生成时间
                            XmlNodeList xfd = xe.SelectNodes("FileDir");//文件路径
                            //HttpContext.Current.Response.write("<script>alert('是');</script>");
                            if (xmt.Count > 0 && xfd.Count > 0)
                            {
                                //判断时间是否已经到时
                                if (DataConverter.CDate(xmt[0].InnerText) <= DateTime.Now)
                                {
                                    //判断XML中的第二个节点是否是地址并且这个地址中有值
                                    if (!string.IsNullOrEmpty(xfd[0].InnerText.ToString()))
                                    {
                                        string xPaht = (basePath.Trim() + xfd[0].InnerText.ToString()).Replace("\n", "").Replace("\r", "").Replace(@"\/", "/").Replace(@"\\", "/");
                                        //判断这个任务执行的XML文件是否存在
                                        if (FileSystemObject.IsExist(xPaht, FsoMethod.File))
                                        {
                                            //读取出XML中的所有节点
                                            string xml = FileSystemObject.ReadFile(xPaht);
                                            FileSystemObject.Delete(xPaht, FsoMethod.File);

                                            //判断文件中是否有内容
                                            if (!string.IsNullOrEmpty(xml))
                                            {
                                                XmlDocument xmldoc = new XmlDocument();
                                                xmldoc.LoadXml(xml);
                                                XmlNodeList ContentNodes = xmldoc.SelectNodes("XmlContent");

                                                docxn.RemoveChild(xn);//删除节点
                                                doc.Save(makePath);//保存文件
                                                foreach (XmlNode ContentNotesItem in ContentNodes)
                                                {
                                                    XmlElement Itemslist = (XmlElement)ContentNotesItem;
                                                    XmlNodeList xType = Itemslist.SelectNodes("Type");
                                                    XmlNodeList xInfo = Itemslist.SelectNodes("InfoID");
                                                    XmlNodeList xnid = Itemslist.SelectNodes("NodeID");

                                                    //判断XmlContent节点下是否还有节点，并且第一个Type节点中不是空值
                                                    if (xType.Count > 0)
                                                    {
                                                        //获取这个节点的值，判断是执行哪种类型的生成任务
                                                        string InfoId = "";
                                                        if (xInfo.Count > 0)
                                                        {
                                                            InfoId = xInfo[0].InnerText;//InfoID值
                                                        }
                                                        int nid = 0;
                                                        if (xnid.Count > 0)
                                                        {
                                                            nid = DataConverter.CLng(xnid[0].InnerText);//NodeID值
                                                        }
                                                        //B_MailManage.makefilelist.Add(xType[0].InnerText);
                                                        switch (xType[0].InnerText)//发布类型
                                                        {
                                                            case "index":// 发布站点主页

                                                                if (xnid.Count <= 0)
                                                                {
                                                                    bc.CreatePageIndex();
                                                                }
                                                                break;
                                                            case "infoall":// 发布所有内容
                                                                if (xnid.Count <= 0)
                                                                {
                                                                    bc.CreateInfo();
                                                                }
                                                                else
                                                                {
                                                                    bc.CreateInfo(nid);
                                                                }
                                                                break;
                                                            //case "infoid":// 按ID发布内容
                                                            //    bc.CreateInfoByIdStr(InfoId);
                                                            //    break;
                                                            case "lastinfocount":// 发布最新数量的内容
                                                                bc.CreateLastInfoRecord(InfoId);
                                                                break;
                                                            case "infodate":// 按日期发布内容
                                                                if (xnid.Count <= 0)
                                                                {
                                                                    bc.CreateInfoDate(InfoId);
                                                                }
                                                                else
                                                                {
                                                                    bc.CreateInfoDate(InfoId, nid);
                                                                }
                                                                break;
                                                            case "infocolumn":// 按栏目发布内容
                                                                bc.CreateInfoColumn(InfoId);
                                                                break;
                                                            case "columnall":// 发布所有栏目页
                                                                bc.CreateColumnAll();
                                                                break;
                                                            case "columnbyid":// 发布选定的栏目页
                                                                bc.CreateColumnByID(InfoId);
                                                                break;
                                                            case "single":// 发布所有单页
                                                                bc.CreateSingle();
                                                                break;
                                                            case "singlebyid":// 发布选定的单页
                                                                bc.CreateSingleByID(InfoId);
                                                                break;
                                                            case "special":// 发布选定的专题
                                                                bc.CreateSpecial(InfoId);
                                                                break;
                                                            case "contentbyid"://发布选定的内容页
                                                                bc.CreateContentColumn(DataConverter.CLng(InfoId));
                                                                break;
                                                            case "createann":// 发布选定的多个内容页
                                                                bc.createann(InfoId);
                                                                break;
                                                        }
                                                    }
                                                }
                                                //删除文件
                                                makefilelist.Add("生成完成!");
                                                Sitework();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    doc.RemoveAll();
                }
                //FileSystemObject.Delete(makePaht, FsoMethod.File);
            }
            #endregion
        }

        #region 操作预审核内容xml文件
        private List<M_Audit> ReadXml()
        {
            return null;
        }
        /// <summary>
        /// 删除xml节点
        /// </summary>
        private void DeleteXmlNode(int id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory + "Config/AuditData.xml";
            xmlDoc.Load(path);
            XmlNodeList xnl = xmlDoc.SelectSingleNode("content").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl[i].Attributes["id"].Value == id.ToString())
                {
                    xnl[i].ParentNode.RemoveChild(xnl[i]);
                }
            }
            xmlDoc.Save(path);
        }

        #endregion

        /// <summary>
        /// 修改内容预审核状态（将待审核修改为已审）
        /// </summary>
        public void UpdateContentState()
        {
            List<M_Audit> audits = ReadXml();
            if (audits != null && audits.Count > 0)
            {
                foreach (M_Audit au in audits)
                {
                    if (au != null && au.Id > 0)
                    {
                        if (au.EndTime < DateTime.Now)  //过期则删除xml节点
                        {
                            DeleteXmlNode(au.Id);
                        }
                        if (au.BeginTime < DateTime.Now && DateTime.Now < au.EndTime)  //存在则修改内容审核状态
                        {
                            conBll.UpdateState(au.NodeId);
                            DeleteXmlNode(au.Id);
                        }
                    }
                }
                audits.Clear();
            }
        }
        public void fSiteWork()
        {
            //B_SendList bsl = new B_SendList();
            //bsl.SendAll();
        }

        public string CreateId
        {
            get;
            set;
        }

        public string url
        {
            get;
            set;
        }

        public int m_newnum
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }
        #region 采集过程

        public void progress()
        {
            StopColl = false;
            this.ExeColl();
        }

        private string Fromatstr(string str)
        {
            if (str != "")
            {
                str = Regex.Replace(str, @"[\r\n]|[ \t]*", "");
                return BaseClass.Htmlcode(str);
            }
            else
            {
                return "";
            }
        }

        //执行采集
        public int ExeColl()
        {
            DataTable dt = citemBll.SelBySwitch(1);//需要采集的列表
            int num = 0;
            m_newnum = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (StopColl) break;
                if (!string.IsNullOrEmpty(dr["LinkList"] as string)) //判断是否有需要采集的超链接
                {
                    m_newnum = GetPage(dr);
                    M_CollectionItem mc = citemBll.GetSelect(DataConverter.CLng(dr["CItem_ID"]));
                    mc.Switch = 2;
                    citemBll.GetUpdate(mc);
                    CollectLog += "项目：" + ItemName + "已完成采集.\r\n";
                    num++;
                }
            }
            if (dt.Rows.Count == num && dt.Rows.Count != 0)
            {
                ItemName = "所有项目";
            }
            this.m_newnum = m_newnum;
            return m_newnum;
        }

        //获取内容页信息,写入数据库
        public void PageInfo(DataRow drs, string remoteUrl, string info)
        {
            if (!string.IsNullOrEmpty(remoteUrl))
            {
                B_ModelField bfield = new B_ModelField();
                B_CollectionInfo bcollect = new B_CollectionInfo();
                B_Model modBll = new B_Model();
                string strhtml = htmlHelper.GetHtmlFromSite(remoteUrl);
                if (string.IsNullOrEmpty(strhtml)) //取到的远程信息为空,则直接停止
                {
                    CollectLog += "(信息为空)"+remoteUrl + ",";
                    return;
                }
                string[] nodeArr = drs["NodeID"].ToString().Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < nodeArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(nodeArr[i]))//节点列表
                    {
                        string sql = "Select * From ZL_CollectionInfo Where OldUrl=@url And NodeID=@nodeID";
                        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("url", remoteUrl), new SqlParameter("nodeID", nodeArr[i]) }; //采集
                        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
                        if (dt.Rows.Count == 0)//readto&&dt.Rows.Count == 0
                        {
                            /*查询字段*/
                            int modeid = DataConverter.CLng(drs["ModeID"]);
                            DataTable dtx = bfield.GetModelFieldList(modeid);
                            DataTable table = new DataTable();
                            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
                            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
                            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
                            try
                            {
                                foreach (DataRow dr in dtx.Rows)//扩展字段
                                {
                                    DataRow row = table.NewRow();
                                    row[0] = dr["FieldName"].ToString();
                                    row[1] = dr["FieldType"].ToString();
                                    row[2] = Insertinfo(info, strhtml, dr["FieldName"].ToString(), remoteUrl);
                                    table.Rows.Add(row);
                                }

                                M_CommonData CData = new M_CommonData();

                                CData.NodeID = DataConverter.CLng(nodeArr[i]);
                                CData.ModelID = modeid;
                                CData.TableName = modBll.GetModelById(modeid).TableName;
                                CData.Title = Insertinfo(info, strhtml, "Title", remoteUrl);
                                CData.Status = 99;
                                CData.Inputer = Insertinfo(info, strhtml, "author", remoteUrl);
                                CData.Hits = DataConvert.CLng(Insertinfo(info, strhtml, "Hits", remoteUrl));
                                CData.EliteLevel = DataConverter.CLng(Insertinfo(info, strhtml, "EliteLevel", remoteUrl));
                                CData.InfoID = "";
                                CData.SpecialID = "";
                                CData.FirstNodeID = GetFriestNode(DataConverter.CLng(nodeArr[i]));
                                CData.TagKey = Insertinfo(info, strhtml, "TagKey", remoteUrl);
                                CData.Template = Insertinfo(info, strhtml, "Template", remoteUrl);
                                CData.CreateTime = DateTime.Now;
                                CData.UpDateTime = DateTime.Now;
                                //tempdt.Add(table); tempmd.Add(CData);
                                int newID = conBll.AddContent(table, CData);//插入信息给两个表，主表和从表:CData-主表的模型，table-从表
                                //M_CollectionInfo mc = new M_CollectionInfo();
                                //mc.AddTime = DateTime.Now.ToString();
                                //mc.ItemID = DataConverter.CLng(drs["CItem_ID"].ToString());
                                //mc.OldUrl = remoteUrl;
                                //mc.CollID = newID;
                                //mc.NodeID = DataConverter.CLng(nodeArr[i]);
                                //mc.ModeID = modeid;
                                //mc.State = 1;
                                //bcollect.GetInsert(mc);
                                this.AllInNum = this.AllInNum + 1;
                                this.m_newnum = this.m_newnum + 1;
                                CollectLog += "(采集成功)" + remoteUrl + ",";
                            }
                            catch (Exception ee)
                            {
                                CollectLog += "(采集异常" + ee.Message + ")"+remoteUrl + ",";
                                //B_MailManage.makefilelist.Add(ee.StackTrace + "/r/n" + ee.Source + "/r/n" + ee.Message);
                            }
                        }
                    }//if end;
                    Thread.Sleep(10);
                }//for end;
            }
        }
        //public static List<DataTable> tempdt = new List<DataTable>();
        //public static List<M_CommonData> tempmd = new List<M_CommonData>();
        /// <summary>
        /// 获得第一级节点ID
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public int GetFriestNode(int NodeID)
        {
            GetNo(NodeID);
            return FNodeID;
        }

        protected int FNodeID = 0;

        public void GetNo(int NodeID)
        {
            B_Node bnode = new B_Node();
            M_Node nodeinfo = bnode.GetNodeXML(NodeID);
            int ParentID = nodeinfo.ParentID;
            if (DataConverter.CLng(nodeinfo.ParentID) > 0)
            {
                GetNo(nodeinfo.ParentID);
            }
            else
            {
                FNodeID = nodeinfo.NodeID;
            }
        }

        /// <summary>
        /// 优先默认值，再是采集规则,来源为一个字符串Xml,字段名_Default,Title_Default,示例://Insertinfo(info, strhtml, "Hits", remoteUrl)
        /// </summary>
        /// <param name="info">drs["InfoPageSettings"]</param>
        /// <param name="strhtml">远程Html源码</param>
        /// <param name="tablefiled">字段名</param>
        /// <param name="url"></param>
        public string Insertinfo(string info, string strhtml, string tablefiledname, string url)
        {
            string result = "";
            htmlHelper.baseurl = StrHelper.GetUrlRoot(url);
            if (string.IsNullOrEmpty(info)) return result;
            DataSet ds = new DataSet();//EC.GetRemoteObj.XmlToTable(info);//获取列表规则
            if (ds.Tables.Count < 1) return result;
            foreach (DataTable dt in ds.Tables)//规则里的数据
            {
                if (!dt.TableName.Equals(tablefiledname)) continue;
                if (dt.Columns[0].ColumnName == tablefiledname + "_Default")//是否是使用默认值
                {
                    result = "默认值";
                }
                else if (dt.Columns[0].ColumnName == tablefiledname + "_Appoint") //是否是指定值
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        result = dr[tablefiledname + "_Appoint"].ToString();
                        result = result.Replace("{geturl}", url);
                    }
                }
                else if (dt.Columns[0].ColumnName == tablefiledname + "_Id")//是否是使用规则
                {
                    result = SetField(info, tablefiledname, strhtml);
                }
                Thread.Sleep(10);
            }
            return result;
        }
        // 获取分页代码
        private int GetPage(DataRow drs)
        {
            if (StopColl) { return 0; }
            string PageSettings = drs["PageSettings"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
            string[] linkArr=drs["LinkList"].ToString().Split(',');
            int nums = 0;
            StringBuilder strurl = new StringBuilder();
            //判断是否存在分页规则和索取页面是否存在
            if (!string.IsNullOrEmpty(PageSettings))
            {
                DataSet ds =new DataSet() ;//EC.GetRemoteObj.XmlToTable(PageSettings);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        switch (dr["PageType"].ToString())
                        {
                            //不分页
                            case "1":
                                foreach (string link in linkArr)
                                {
                                    PageInfo(drs, link, drs["InfoPageSettings"].ToString());
                                }
                                break;
                            ////从源代码中获取下一页的URL 
                            //case "2":
                            //    if (dr["PageNextBegin"].ToString() != "" && dr["PageNextEnd"].ToString() != "")
                            //    {
                            //        checkurl = checkList(dr["PageNextBegin"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), dr["PageNextEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), strhtml);
                            //        strurl = new StringBuilder();
                            //        strurl.Append(orderUrl(checkurl));
                            //        //LinkTop,string LinkButton


                            //        PageUrl(readto, drs, SetUrl(ssr.Rows[0]["LinkTop"].ToString(), ssr.Rows[0]["LinkButton"].ToString(), drs["CollUrl"].ToString(), strurl.ToString()), ListSettings, info);
                            //        if (!string.IsNullOrEmpty(checkurl))
                            //        {
                            //            nums = nums + GetPage(readto, drs, PageSettings, checkurl, ListSettings, info);
                            //        }
                            //    }
                            //    break;
                            ////批量指定分页URL代码
                            //case "3":
                            //    for (int i = DataConverter.CLng(dr["PageBeginNum"].ToString()); i <= DataConverter.CLng(dr["PageEndNum"].ToString()); i++)
                            //    {
                            //        strurl = new StringBuilder();
                            //        strurl.Append(dr["PageUrl"].ToString().Replace("{$ID}", i.ToString()) + "\n\r");
                            //        PageUrl(readto, drs, strurl.ToString(), ListSettings, info);
                            //        nums = nums + GetPage(readto, drs, PageSettings, strurl.ToString(), ListSettings, info);
                            //    }
                            //    break;
                            ////手动添加分页URL代码 
                            //case "4":
                            //    strurl.Append(dr["PageInfo"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"));

                            //    string[] surl = strurl.ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            //    foreach (string s in surl)
                            //    {
                            //        PageUrl(readto, drs, s, ListSettings, info);
                            //        nums = nums + GetPage(readto, drs, PageSettings, SetUrl(ssr.Rows[0]["LinkTop"].ToString(), ssr.Rows[0]["LinkButton"].ToString(), drs["CollUrl"].ToString(), s), ListSettings, info);
                            //    }
                            //    break;
                            ////从源代码中获取分页URL
                            //case "5":
                            //    if (dr["PageDivBegin"].ToString() != "" && dr["PageDivEnd"].ToString() != "")
                            //    {
                            //        strurl = new StringBuilder();
                            //        strurl.Append(checkList(dr["PageDivBegin"].ToString(), dr["PageDivEnd"].ToString(), strhtml));
                            //    }
                            //    if (dr["PageUrlBegin"].ToString() != "" && dr["PageUrlEnd"].ToString() != "")
                            //    {
                            //        strurl = new StringBuilder();
                            //        strurl.Append(checkList(dr["PageUrlBegin"].ToString(), dr["PageUrlEnd"].ToString(), strurl.ToString()));
                            //        strurl = new StringBuilder();
                            //        strurl.Append(orderUrl(strurl.ToString()));

                            //        PageUrl(readto, drs, url, ListSettings, info);
                            //        string[] su = strurl.ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            //        if (!string.IsNullOrEmpty(strurl.ToString()))
                            //        {
                            //            PageUrl(readto, drs, SetUrl(ssr.Rows[0]["LinkTop"].ToString(), ssr.Rows[0]["LinkButton"].ToString(), drs["CollUrl"].ToString(), su[0]), ListSettings, info);
                            //            nums = nums + GetPage(readto, drs, PageSettings, SetUrl(ssr.Rows[0]["LinkTop"].ToString(), ssr.Rows[0]["LinkButton"].ToString(), drs["CollUrl"].ToString(), su[0]), ListSettings, info);
                            //        }
                            //    }
                            //    break;
                        }
                        Thread.Sleep(0);
                    }
                }
            }
            return nums;
        }
        /// <summary>
        /// 整理URL
        /// </summary>
        /// <param name="Url">列表页URL</param>
        /// <param name="str"></param>
        /// <returns></returns>
        private string SetUrl(string LinkTop, string LinkButton, string Url, string str)
        {
            StringBuilder strurl = new StringBuilder();
            //切割字符串地址
            string[] UrlArr = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (UrlArr.Length > 0)
            {
                //循环地址
                foreach (string ustr in UrlArr)
                {
                    string s = ustr.Replace("\r", "");
                    strurl.Append(GetStr(LinkTop, LinkButton, Url, s));
                }
            }
            else
            {
                strurl = new StringBuilder();
                strurl.Append(GetStr(LinkTop, LinkButton, Url, str));
            }
            return strurl.ToString();
        }

        private static string GetStr(string LinkTop, string LinkButton, string Url, string ustr)
        {
            StringBuilder strurl = new StringBuilder();
            if (ustr.IndexOf("http://") < 0)
            {
                //切割内容页面地址
                string[] urlinfo = ustr.Split(new char[] { '/' });
                //切割列表页地址
                string[] infoarr = Url.Split(new char[] { '/' });

                if (LinkTop != "")
                {
                    strurl = new StringBuilder();
                    strurl.Append(LinkTop + ustr + LinkButton);
                }
                else
                {
                    if (urlinfo[0] != "")
                    {
                        int i = 0;
                        //循环切割后的地址字符
                        foreach (string s in urlinfo)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                i++;
                            }
                        }
                        if (i > 1)
                        {
                            i--;
                        }
                        for (int j = 0; j < infoarr.Length - i; j++)
                        {
                            strurl.Append(infoarr[j].ToString() + "/");
                        }
                        strurl.Append(urlinfo[urlinfo.Length - 1] + "\n\r");
                    }
                    else
                    {
                        strurl = new StringBuilder();
                        strurl.Append("http://" + infoarr[2] + ustr);
                    }
                }
            }
            else
            {
                strurl.Append(LinkTop + ustr + LinkButton + "\n\r");
            }
            return strurl.ToString();
        }
        /// <summary>
        /// 删除重复地址?
        /// </summary>
        /// <param name="strurl"></param>
        /// <returns></returns>
        private string orderUrl(string strurl)
        {
            StringBuilder strtemp = new StringBuilder();
            strtemp.Append(strurl);

            string[] str = strurl.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = new string[str.Length];
            strurl = "";
            for (int i = 0; i < str.Length; i++)
            {
                bool b = true;
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str[i] == str2[j])
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    str2[i] = str[i];
                    strtemp.Append(str[i] + "\n\r");
                }
            }
            return strurl;
        }

        /// <summary>
        /// 获取设置内容页规则
        /// </summary>
        /// <param name="config">配置规则</param>
        /// <param name="IName">字段名</param>
        /// <param name="htmlstr">Html字符串</param>
        /// <returns>内容</returns>
        private string SetField(string config, string IName, string htmlstr)
        {
            string result = "";
            //将XML设置成DataSet
            if (string.IsNullOrEmpty(config)) return result;
            DataSet ds = new DataSet();//EC.GetRemoteObj.XmlToTable(config);
            if (ds.Tables.Count < 1) return result;
            //获得表
            foreach (DataTable dt in ds.Tables)
            {
                //是否是当前字段设置的XML节点
                if (dt.TableName == IName && dt.Columns[0].ColumnName == IName + "_Id")
                {
                    //是否是使用规则
                    foreach (DataTable dtx in ds.Tables)
                    {
                        if (dtx.TableName == IName + "_CollConfig")
                        {
                            foreach (DataRow dr in dtx.Rows)
                            {
                                //获取内容,图片下载
                                FilterModel model = JsonConvert.DeserializeObject<FilterModel>(dr["FieldStart"].ToString());
                                result = htmlHelper.GetByFilter(htmlstr, model);
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
            return result;
        }

        #endregion
        #endregion
    }
}
