using System;
using System.Data;
using System.Configuration;
using System.Web;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using System.Xml;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Content;
using System.IO;
using System.Text.RegularExpressions;

namespace ZoomLa.BLL
{
    public class B_Label
    {
        //虚拟目录,物理目录,物理文件名,系统标签文件名
        public string vdir = "/" + SiteConfig.SiteOption.TemplateDir.Trim('/') + "/Label/";
        public string dir,dirfilename;
        public string sysLabelFile = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\SysLabel.label";
        private M_Label NullMod = new M_Label(true);
        public B_Label()
        {
            dir = function.VToP(vdir);
            dirfilename = dir + "Directory.label";
            if (!File.Exists(dirfilename))
            {
                SafeSC.CreateDir(vdir);
                DataSet ds = new DataSet("NewDataSet");
                ds.WriteXml(dirfilename);
            }
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        public bool AddLabelXML(M_Label label)
        {
            if (string.IsNullOrEmpty(label.LableName)) { label.LableName = "随机" + DateTime.Now.ToString("yyyyMMdd") + "_" + function.GetRandomString(6); }
            if (string.IsNullOrEmpty(label.LabelCate)) { label.LabelCate = "未分类"; }
            DataTable labelDT = GetInfoFromModel(label);
            DataTable directoryDT = GetDirectoryXML();
            string filename = dir + label.LabelCate + @"\" + label.LableName + ".label";
            ExistLabelDir(label);
            if (directoryDT.Columns.Count > 0)
            {
                directoryDT.DefaultView.RowFilter = "LabelName='" + label.LableName + "'";
            }
            directoryDT = directoryDT.DefaultView.ToTable();
            if (directoryDT.Rows.Count > 0)
            {
                function.WriteErrMsg("已存在该标签[" + label.LableName + "],请使用其他表签名!");
            }
            directoryDT.Dispose();
            DataSet settale = new DataSet("NewDataSet");
            settale.Tables.Add(labelDT);
            settale.WriteXml(filename);
            //更新directorylabel(这里应切换成BLL返回table,避免[NewDataSet]中无任何标签引起报错)
            DataSet dirDS = FileSystemObject.ReadXML(dirfilename, "NewDataSet");
            DataRow rows = dirDS.Tables[0].NewRow();
            rows["LabelID"] = labelDT.Rows[0]["LabelID"];
            rows["LabelName"] = label.LableName;
            rows["LabelType"] = label.LableType;
            rows["LabelCate"] = label.LabelCate;
            dirDS.Tables[0].Rows.Add(rows);
            dirDS.WriteXml(dirfilename);
            return true;
        }
        /// <summary>
        /// 更新xml标签
        /// </summary>
        public void UpdateLabelXML(M_Label label)
        {
            if (string.IsNullOrEmpty(label.LableName)) { label.LableName = "随机命名" + DateTime.Now.ToString("yyyyMMdd") + "_" + function.GetRandomString(6); }
            ExistLabelDir(label);
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode nodelist = doc.SelectSingleNode("//NewDataSet/Table[LabelName='" + label.LableName + "']");
            if (nodelist == null) { throw (new Exception("[" + label.LableName + "]不存在")); }
            string oldPath = (nodelist["LabelCate"].InnerText + "\\" + nodelist["LabelName"].InnerText).Replace(" ", "").ToLower();
            string newPath = (label.LabelCate + "\\" + label.LableName).ToLower();
            //--------------------
            DataTable labelDT = GetInfoFromModel(label);
            DataSet newset = new DataSet("NewDataSet");
            newset.Tables.Add(labelDT);
            string filename = dir + label.LabelCate + @"\" + label.LableName + ".label";
            DataSet ds = FileSystemObject.ReadXML(dirfilename, "NewDataSet");
            if (ds.Tables.Count <= 0) { ds.Tables.Add(new DataTable("Table")); }
            DataTable dsDT = ds.Tables[0];
            if (dsDT.Columns.Count > 0) { dsDT.DefaultView.RowFilter = "LabelName='" + label.LableName + "'"; }
            dsDT = dsDT.DefaultView.ToTable();
            newset.WriteXml(filename);
            //newset.Tables.Add(filename);
            if (!newPath.Equals(oldPath))//修改了名称或类别
            {
                FileSystemObject.Delete(dir + oldPath + ".label",FsoMethod.File);
                string oldfolder = dir + nodelist["LabelCate"].InnerText;
                DataTable filelist = FileSystemObject.GetFileList(oldfolder);
                if (filelist == null || filelist.Rows.Count < 1)
                {
                    FileSystemObject.Delete(oldfolder, FsoMethod.Folder);
                }
            }
            nodelist["LabelName"].InnerText = label.LableName;
            nodelist["LabelCate"].InnerText = label.LabelCate;
            nodelist["LabelType"].InnerText = label.LableType.ToString();
            doc.Save(dirfilename);
        }
        public void DelLabelXML(string LabelName)
        {
            LabelName = LabelName.Replace(" ", "");
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode nodelist = doc.SelectSingleNode("//NewDataSet/Table[LabelName='" + LabelName + "']");
            DelLabel(doc, nodelist);
        }
        public void DelLabelXML(int LabelID)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode node = doc.SelectSingleNode("//NewDataSet/Table[LabelID='" + LabelID.ToString() + "']");
            DelLabel(doc, node);
        }
        private void DelLabel(XmlDocument doc, XmlNode nodelist)
        {
            if (nodelist == null) { return; }
            string nodefile = dir + nodelist["LabelCate"].InnerText + @"\" + nodelist["LabelName"].InnerText + ".label";
            if (File.Exists(nodefile))
            { FileSystemObject.Delete(nodefile, FsoMethod.File); }
            string filefolder = dir + nodelist["LabelCate"].InnerText;
            if (Directory.Exists(filefolder))
            {
                DataTable filelist = FileSystemObject.GetFileList(filefolder);
                if (filelist == null || filelist.Rows.Count < 1)
                {
                    FileSystemObject.Delete(filefolder, FsoMethod.Folder);
                }
            }
            nodelist.ParentNode.RemoveChild(nodelist);
            doc.Save(dirfilename);
        }
        /// <summary>
        /// 根据标签ID读取标签XML
        /// </summary>
        public M_Label GetLabelXML(int LabelID)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode nodeinfo = doc.SelectSingleNode("//NewDataSet/Table[LabelID='" + LabelID + "']");
            return GetLabelByNode(nodeinfo);
        }
        //根据筛选到的节点,返回标签
        private M_Label GetLabelByNode(XmlNode node)
        {
            if (node == null)
            {
                return NullMod;
            }
            else
            {
                string labelPath = dir + node["LabelCate"].InnerText + @"\" + node["LabelName"].InnerText + ".label";
                if (!File.Exists(labelPath)) { return NullMod; }
                DataSet tempset = new DataSet();
                tempset.ReadXml(labelPath);
                DataTable dt = tempset.Tables[0];
                return M_Label.GetInfoFromDataTable(node, dt);
            }
        }
        /// <summary>
        /// 根据标签名称读取标签XML
        /// </summary>
        public M_Label GetLabelXML(string LabelName)
        {
            LabelName = (LabelName ?? "").Replace(" ", "");
            if (string.IsNullOrEmpty(LabelName)) { return NullMod; }
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode nodeinfo = doc.SelectSingleNode("//NewDataSet/Table[LabelName='" + LabelName + "']");
            return GetLabelByNode(nodeinfo);
        }
        /// <summary>
        /// 将模型转化为DataTable,为空则空结构
        /// </summary>
        private DataTable GetLabelStruct(M_Label labelMod = null)
        {
            DataTable table = new DataTable("Table");
            table.Columns.Add(new DataColumn("LabelID", typeof(int)));
            table.Columns.Add(new DataColumn("LabelName", typeof(string)));
            table.Columns.Add(new DataColumn("LabelType", typeof(int)));
            table.Columns.Add(new DataColumn("LabelCate", typeof(string)));
            table.Columns.Add(new DataColumn("LabelDesc", typeof(string)));
            table.Columns.Add(new DataColumn("LabelParam", typeof(string)));
            table.Columns.Add(new DataColumn("LabelTable", typeof(string)));
            table.Columns.Add(new DataColumn("LabelField", typeof(string)));
            table.Columns.Add(new DataColumn("LabelWhere", typeof(string)));
            table.Columns.Add(new DataColumn("LabelOrder", typeof(string)));
            table.Columns.Add(new DataColumn("LabelContent", typeof(string)));
            table.Columns.Add(new DataColumn("LabelCount", typeof(string)));
            table.Columns.Add(new DataColumn("LabelAddUser", typeof(int)));
            table.Columns.Add(new DataColumn("LabelNodeID", typeof(int)));
            table.Columns.Add(new DataColumn("Modeltypeinfo", typeof(string)));
            table.Columns.Add(new DataColumn("addroot", typeof(string)));
            table.Columns.Add(new DataColumn("setroot", typeof(string)));
            table.Columns.Add(new DataColumn("Modelvalue", typeof(string)));
            table.Columns.Add(new DataColumn("Valueroot", typeof(string)));
            table.Columns.Add(new DataColumn("IsOpen", typeof(int)));
            table.Columns.Add(new DataColumn("FalseContent", typeof(string)));
            table.Columns.Add(new DataColumn("DataSourceType", typeof(string)));
            table.Columns.Add(new DataColumn("ConnectString", typeof(string)));
            table.Columns.Add(new DataColumn("ProceName", typeof(string)));
            table.Columns.Add(new DataColumn("ProceParam", typeof(string)));
            if (labelMod != null)
            {
                DataRow row = table.NewRow();
                row[0] = labelMod.LabelID;
                row[1] = labelMod.LableName;
                row[2] = DataConverter.CLng(labelMod.LableType);
                row[3] = labelMod.LabelCate;
                row[4] = labelMod.Desc;
                row[5] = labelMod.Param;
                row[6] = labelMod.LabelTable;
                row[7] = labelMod.LabelField;
                row[8] = labelMod.LabelWhere;
                row[9] = labelMod.LabelOrder;
                row[10] = labelMod.Content;
                row[11] = labelMod.LabelCount;
                row[12] = DataConverter.CLng(labelMod.LabelAddUser);
                row[13] = DataConverter.CLng(labelMod.LabelNodeID);
                row[14] = labelMod.Modeltypeinfo;
                row[15] = labelMod.addroot;
                row[16] = labelMod.setroot;
                row[17] = labelMod.Modelvalue;
                row[18] = labelMod.Valueroot;
                row[19] = DataConverter.CLng(labelMod.IsOpen);
                row[20] = labelMod.FalseContent;
                row[21] = labelMod.DataSourceType;
                row[22] = labelMod.ConnectString;
                row[23] = labelMod.ProceName;
                row[24] = labelMod.ProceParam;
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 将模型转化为DataTable,用于存入文件
        /// </summary>
        /// <param name="Rowsinfo">DataTable</param>
        public DataTable GetInfoFromModel(M_Label labelMod)
        {
            DataTable dirtable = GetDirectoryXML();
            if (labelMod.LabelID < 1)
            {
                //其是字符串类型,需转为int
                dirtable.Columns.Add("LabelIDs", typeof(int));
                for (int i = 0; i < dirtable.Rows.Count; i++)
                {
                    dirtable.Rows[i]["LabelIDs"] = dirtable.Rows[i]["LabelID"];
                }
                dirtable.Columns.Remove("LabelID");
                dirtable.Columns["LabelIDs"].ColumnName = "LabelID";
                dirtable.Columns["LabelID"].SetOrdinal(0);
                labelMod.LabelID = (DataConverter.CLng(dirtable.Compute("max(LabelID)", "")) + 1);
            }
            return GetLabelStruct(labelMod);
        }
        public DataSet GetAllSysLabelXML()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(sysLabelFile);
            return ds;
        }
        public DataSet GetLabelSelectXML(string lblids)
        {
            DataSet newset = FileSystemObject.ReadXML(dirfilename, "NewDataSet");
            DataTable tables = newset.Tables[0];
            tables.DefaultView.RowFilter = "LabelID in (" + lblids + ")";
            tables = tables.DefaultView.ToTable();
            tables.DefaultView.Sort = "LabelID ASC";
            tables = tables.DefaultView.ToTable();
            return newset;
        }
        /// <summary>
        /// 近回标签列表(Directory.label)
        /// </summary>
        public DataTable GetDirectoryXML()
        {
            DataSet labeltable = FileSystemObject.ReadXML(dirfilename, "NewDataSet");
            if (labeltable.Tables.Count <= 0)
            {
                labeltable.Tables.Add(new DataTable("Table"));
            }
            return labeltable.Tables[0];
        }
        //------------------------------------------------------------
        /// <summary>
        /// 获取全部标签,用于分页
        /// </summary>
        /// <param name="labelCate">标签类别</param>
        /// <param name="labelName">标签名称</param>
        /// <returns></returns>
        public DataTable SelAllLabel(string labelCate = "", string labelName = "")
        {
            DataSet labelDS = FileSystemObject.ReadXML(dirfilename, "NewDataSet");
            if (labelDS.Tables.Count < 1) { return new DataTable(); }
            DataTable dt = labelDS.Tables[0];
            dt.Columns.Add("LabelIDs", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["LabelIDs"] = dt.Rows[i]["LabelID"];
            }
            dt.Columns.Remove("LabelID");
            dt.Columns["LabelIDs"].ColumnName = "LabelID";
            dt.Columns["LabelID"].SetOrdinal(0);
            string filter = "";
            if (!string.IsNullOrEmpty(labelCate))
            {
                filter = "LabelCate='" + labelCate + "'";
            }
            if (!string.IsNullOrEmpty(labelName))
            {
                if (!string.IsNullOrEmpty(filter)) { filter = filter += " AND "; }
                filter = "LabelName like '%" + labelName + "%'";
            }
            dt.DefaultView.RowFilter = filter;
            dt.DefaultView.Sort = "LabelID Desc";
            dt = dt.DefaultView.ToTable();
            return dt;
        }
        public string GetTablecolumn(string tablename, string columnname)
        {
            return SqlHelper.GetTablecolumn(tablename, columnname);
        }
        /// <summary>
        /// 获得分类列表xml
        /// </summary>
        public DataTable GetLabelCateListXML()
        {
            DataTable tableinfo = FileSystemObject.GetDirectoryInfoflo(dir, FsoMethod.Folder);
            tableinfo.DefaultView.Sort = "createTime desc";
            return tableinfo;
        }
        ///////////////////////////////////////////数据库方法////////////////////////////////
        public DataTable GetSourceLabelXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNodeList lablelist = doc.SelectNodes("//NewDataSet/Table[LabelType='3']");
            DataTable sourchtable = new DataTable();
            sourchtable.Columns.Add("LabelID", typeof(int));
            sourchtable.Columns.Add("LabelName", typeof(string));
            sourchtable.Columns.Add("LabelNodeID", typeof(int));
            if (lablelist != null)
            {
                for (int i = 0; i < lablelist.Count; i++)
                {
                    string labelpath = dir + lablelist[i]["LabelCate"].InnerText + @"\" + lablelist[i]["LabelName"].InnerText + ".label";
                    if (!File.Exists(labelpath)) { continue; }
                    XmlDocument readinfo = new XmlDocument();
                    DataRow rows = sourchtable.NewRow();
                    rows["LabelID"] = lablelist[i]["LabelID"].InnerText;
                    rows["LabelName"] = lablelist[i]["LabelName"].InnerText;
                    readinfo.Load(labelpath);
                    XmlNode labelcontent = readinfo.SelectSingleNode("//NewDataSet/Table");
                    try
                    {
                        rows["LabelNodeID"] = labelcontent["LabelNodeID"] == null ? "0" : labelcontent["LabelNodeID"].InnerText;
                    }
                    catch
                    {
                        DataSet newset = new DataSet();
                        newset.ReadXml(labelpath);
                        newset.DataSetName = "NewDataSet";
                        newset.WriteXml(labelpath);
                    }
                    readinfo.Load(labelpath);
                    labelcontent = readinfo.SelectSingleNode("//NewDataSet/Table");
                    if (rows.Table.Columns.IndexOf("LabelNodeID") == -1)
                    {
                        rows.Table.Columns.Add("LabelNodeID", typeof(string));
                        rows["LabelNodeID"] = null;
                    }
                    labelcontent = null;
                    sourchtable.Rows.Add(rows);
                }
            }
            return sourchtable;
        }
        /// <summary>
        /// 标准模式下查询表名
        /// </summary>
        public DataTable GetTableName(string ConnectionString)
        {
            return SqlHelper.GetSchemaTable(ConnectionString);
        }
        /// <summary>
        /// 站群模式下查询表名
        /// </summary>
        public DataTable GetTableName2(string ConnectionString)
        {
            return SqlHelper.GetSchemaTable2(ConnectionString);
        }
        public DataTable GetTableField(string tbname)
        {
            return SqlHelper.GetTableColumn(tbname,SqlHelper.ConnectionString);
        }
        public DataTable GetTableField(string TableName, object dbConnectionString)
        {
            return SqlHelper.GetTableColumn(TableName, dbConnectionString);
        }
        public int GetLabelListCountXML(string LabelCate, string LabelName)
        {
            DataSet labeltable = FileSystemObject.ReadXML(dirfilename, "NewDataSet");
            DataTable returntable = labeltable.Tables[0];
            labeltable.Dispose();
            string fileter = "";
            if (LabelName != null && LabelName != "")
            {
                fileter = "LabelName like '%" + LabelName + "%'";
            }
            if (LabelCate != null && LabelCate != "")
            {
                if (fileter == "")
                {
                    returntable.DefaultView.RowFilter = "LabelCate='" + LabelCate + "'";
                }
                else
                {
                    returntable.DefaultView.RowFilter = "LabelCate='" + LabelCate + "' and " + fileter;
                }
            }
            else
            {
                if (fileter != "")
                {
                    returntable.DefaultView.RowFilter = fileter;
                }
            }
            return returntable.DefaultView.Count;
        }
        /// <summary>
        /// 根据标签名称判断是否存在该标签
        /// </summary>
        public void CheckLabelXML(string LabelName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode nodeinfo = doc.SelectSingleNode("//NewDataSet/Table[LabelName='" + LabelName + "']");
            if (nodeinfo != null)
            {
                function.WriteErrMsg("已存在该标签 [" + LabelName + "],请使用其他名称！");
                return;
            }
        }
        public bool IsExistXML(string LabelName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dirfilename);
            XmlNode nodeinfo = doc.SelectSingleNode("//NewDataSet/Table[LabelName='" + LabelName + "']");
            if (nodeinfo != null)
            {
                string filename = dir + nodeinfo["LabelCate"].InnerText + @"\" + LabelName + ".label";
                return FileSystemObject.IsExist(filename, FsoMethod.File);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取标签所在的路径
        /// </summary>
        public static string GetLabelVPath(M_Label label)
        {
            B_Label labelBll = new B_Label();
            return labelBll.vdir + label.LabelCate + "/" + label.LableName + ".label";
        }
        /// <summary>
        /// 验证目录是否存在
        /// </summary>
        private void ExistLabelDir(M_Label label)
        {
            if (!FileSystemObject.IsExist(dir + label.LabelCate, FsoMethod.Folder))
            {
                FileSystemObject.CreateFileFolder(dir + label.LabelCate);
            }
        }
        //-----标签建立，用于标签助手,旧和新的
        /// <summary>
        /// 字段创建方法,用于系统SQL
        /// </summary>
        public string SetLabelColumn(string sField, string TableName, string LabelName, string connStr)
        {
            string tlp = "<div class=\"list-group-item spanfixdiv\" outtype=\"0\" onclick=\"cit(this)\" code='{SField sid=\"" + LabelName + "\" FD=\"@fd\" page=\"0\"/}'>{SField FD=\"@fd\"/}</div>";
            string T1 = "", T2 = "", result = "";
            B_Label.GetT1AndT2(TableName, ref T1, ref T2);
            //扩展支持 sField:ZL_CommonModel.*,ZL_C_Announce.*
            if (sField.Equals("*") || sField.EndsWith("*"))
            {
                DataTable dt = DBCenter.DB.Field_List(T1);
                if (!string.IsNullOrEmpty(T2))
                {
                    DataTable dt2 = DBCenter.DB.Field_List(T2);
                    if (dt2 != null && dt2.Rows.Count > 0) { dt.Merge(dt2); }
                }
                dt = dt.DefaultView.ToTable(true);
                foreach (DataRow dr in dt.Rows)
                {
                    result += tlp.Replace("@fd", dr["Name"].ToString());
                }
            }
            else
            {
                string[] fieldArr = sField.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string field in fieldArr)
                {
                    string name = "";
                    if (field.Contains(".")) { name = field.Split('.')[1]; }
                    else { name = field; }
                    result += tlp.Replace("@fd", name);
                }
            }
            return result;
        }
        /// <summary>
        /// 用于标签助手等,创建Div
        /// </summary>
        public string CreateLabelHtml(DataTable dt)
        {
            string lblLabels = "";
            //{0}:outtype,{1}:LabelName,{2}:Server.UrlEncode(labelinfo.LableName),
            string labelTlp = "<div outtype='{0}' code='{1}' onclick='cit(this)' class='spanfixdivchechk text-left'>"
                + "<a onclick=opentitle('LabelSql.aspx?LabelName={2}','修改标签') href='javascript:;' title='修改标签'><span class='fa fa-edit'>"
                + "</span></a><span outtype='{0}' code='{1}'>{1}</span></div>";
            foreach (DataRow dr in dt.Rows)
            {
                M_Label labelinfo = GetLabelXML(dr["LabelName"].ToString().Split('.')[0].ToString());
                switch (labelinfo.LableType)
                {
                    case 1://静态标签
                        lblLabels = lblLabels + string.Format(labelTlp, 1, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                        break;
                    case 3://数据源
                        if (string.IsNullOrEmpty(labelinfo.Param))
                        {
                            lblLabels = lblLabels + string.Format(labelTlp, 3, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                        }
                        else
                        {
                            string Param = labelinfo.Param;
                            if (Param.IndexOf("|") < 0)
                            {
                                if (Param.Split(new char[] { ',' })[2] == "2")
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 3, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                                else
                                {
                                    //带参数数据源
                                    lblLabels = lblLabels + string.Format(labelTlp, 2, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                            }
                            else
                            {
                                string[] dd = Param.Split(new char[] { '|' });
                                int tempd = 0;
                                for (int cd = 0; cd < dd.Length; cd++)
                                {
                                    tempd = dd[cd].Split(new char[] { ',' })[2] == "2" ? 1 : 0;
                                }
                                if (tempd > 0)
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 3, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                                else
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 4, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                            }
                        }
                        break;
                    case 5:
                        if (DataConverter.CLng(labelinfo.LableType) == 5)
                        {
                            lblLabels = lblLabels + string.Format(labelTlp, 5, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                        }
                        else
                        {
                            lblLabels = lblLabels + string.Format(labelTlp, 6, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                        }
                        break;
                    default://动态标签
                        if (string.IsNullOrEmpty(labelinfo.Param))
                        {
                            lblLabels = lblLabels + string.Format(labelTlp, 1, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                        }
                        else
                        {
                            string Param = labelinfo.Param;
                            if (Param.IndexOf("|") < 0)
                            {
                                if (Param.Split(new char[] { ',' })[2] == "2")
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 1, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                                else
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 2, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                            }
                            else
                            {
                                string[] arrstd = Param.Split(new string[] { "|" }, StringSplitOptions.None);
                                bool istdsd = false;
                                foreach (string sd in arrstd)
                                {
                                    if (DataConverter.CLng(sd.Split(new string[] { "," }, StringSplitOptions.None)[2]) == 1)
                                    {
                                        istdsd = istdsd || true;
                                    }
                                }
                                if (istdsd)
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 2, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                                else
                                {
                                    lblLabels = lblLabels + string.Format(labelTlp, 1, labelinfo.LableName, HttpUtility.UrlEncode(labelinfo.LableName));
                                }
                            }
                        }
                        break;
                }
            }
            return lblLabels;
        }
        /// <summary>
        /// 根据类型返回标签类型名称
        /// </summary>
        public string GetLabelType(int type)
        {
            switch (type)
            {
                case 1:
                    return "静态标签";
                case 2:
                    return "动态标签";
                case 3:
                    return "数据源标签";
                case 4:
                    return "分页列表标签";
                case 5:
                case 6:
                    return "分页标签";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 用于标签和底层页面,解析字符串获取主表和次表
        /// </summary>
        public static void GetT1AndT2(string source, ref string t1, ref string t2)
        {
            //string source = "{table1}.dbo.ZL_CommonModel";
            //string source = "{table1}.dbo.ZL_CommonModel left join {table1}.dbo.ZL_C_Article on {table1}.dbo.ZL_CommonModel.ItemID={table1}.dbo.ZL_C_Article.ID";
            //string source = "ZL_Accountinfo LEFT JOIN ZL_Accountinfo ON ZL_3DMusic.AddTime=ZL_Accountinfo.Account";
            //string source = "{table1}.dbo.ZL_CommonModel left join {table1}.dbo.ZL_C_Article on {table1}.dbo.ZL_CommonModel.ItemID={table1}.dbo.ZL_C_Article.ID";
            if (!source.Contains("="))
            {
                t1 = GetLastStr(source);
            }
            else
            {
                string[] infos = source.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                t1 = GetLastStr(infos[0]);
                if (infos.Length > 3) { t2 = GetLastStr(infos[3]); }
            }
        }
        /// <summary>
        /// True包含ON连接,取=号两边的字段值
        /// </summary>
        public static bool GetOnField(string source, ref string on1, ref string on2)
        {
            string on = " ON ";
            if (!source.ToUpper().Contains(on)) { return false; }
            string fields = Regex.Split(source, on, RegexOptions.IgnoreCase)[1].Replace(" ", "");//ZL_3DMusic.AddTime=ZL_Accountinfo.Account
            on1 = fields.Split('=')[0];
            on2 = fields.Split('=')[1];
            on1 = GetLastStr(on1);
            on2 = GetLastStr(on2);
            return true;
        }
        public static string GetJoinType(string text)
        {
            text = text.ToUpper().Trim();
            if (text.Contains("LEFT JOIN")) { return "LEFT JOIN"; }
            else if (text.Contains("RIGHT JOIN")) { return "RIGHT JOIN"; }
            else if (text.Contains("INNER JOIN")) { return "INNER JOIN"; }
            else if (text.Contains("OUTER JOIN")) { return "OUTER JOIN"; }
            else { return ""; }
        }
        /// <summary>
        /// 获取.之后的最后一个字符串
        /// </summary>
        public static string GetLastStr(string source)
        {
            if (string.IsNullOrEmpty(source)) { return ""; }
            int index = source.LastIndexOf(".") + 1;
            string result = source.Substring(index, source.Length - index);
            return result;
        }
        #region disuse
        public string GetLabelHtml(string sLabel)
        {
            return "";
        }
        public string ReplaceLabel(string Template)
        {
            return "";
        }
        #endregion
    }
}