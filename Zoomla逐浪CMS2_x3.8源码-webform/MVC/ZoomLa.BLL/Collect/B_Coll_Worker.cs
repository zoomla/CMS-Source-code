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
        public static B_Content conBll = new B_Content();
        public static HtmlHelper htmlHelp = new HtmlHelper();
        //采集日志
        public static List<M_Coll_WorkLog> workLog = new List<M_Coll_WorkLog>();
        //正常进行采集的工作
        public static List<M_CollectionItem> workList = new List<M_CollectionItem>();
        //为false则停止工作
        public static bool work_switcher = false;
        //该次采集到的文章数
        public static int work_count = 0;
        private const int ERROR = 1;
        private delegate void BeginCollContentFunc();
        private static void BeginCollContent() 
        {
            B_Model modBll = new B_Model();
            B_ModelField fdBll = new B_ModelField();
            B_CollectionItem itemBll = new B_CollectionItem();
            foreach (M_CollectionItem item in workList)
            {
                if (B_Coll_Worker.work_switcher == false) { break; }
                if (string.IsNullOrEmpty(item.LinkList)) { WriteLog("项目[" + item.ItemName + "]取消,原因:未指定需要采集的列表URL,LinkList"); continue; }
                if (string.IsNullOrEmpty(item.InfoPageSettings)) { WriteLog("项目[" + item.ItemName + "]取消,原因:未指定字段采集规则"); continue; }
                if (string.IsNullOrEmpty(item.CollUrl)) { WriteLog("项目[" + item.ItemName + "]取消,原因:未指定采集来源网址CollUrl"); continue; }
                M_ModelInfo modMod = modBll.SelReturnModel(item.ModeID);
                DataTable fieldDT = fdBll.SelByModelID(item.ModeID);
                foreach (string url in item.LinkList.Split('\n'))
                {
                    if (B_Coll_Worker.work_switcher == false) { WriteLog("采集项目[" + item.ItemName + "]已停止", ERROR); break; }
                    if (string.IsNullOrEmpty(url)) { continue; }
                    WriteLog("正在采集[" + item.ItemName + "]的目标网址[" + url + "]");
                    CollectByUrl(url, item, modMod, fieldDT);
                }
                DBCenter.UpdateSQL(item.TbName, "Switch=2", "CItem_ID=" + item.CItem_ID);
            }
            WriteLog("采集完成,共采集到[" + work_count + "]篇内容",99);
            work_switcher = false;
            work_count = 0;
            workList.Clear();
        }
        public static void Start()
        {
            B_Coll_Worker.work_switcher = true;
            BeginCollContentFunc func = BeginCollContent;
            func.BeginInvoke(null, null);
        }
        public static void AddWork(M_CollectionItem item)
        {
            if (workList.FirstOrDefault(p => p.CItem_ID == item.CItem_ID) != null) { }
            else { workList.Add(item); }
        }
        //将内容写入指定的节点
        private static void CollectByUrl(string url, M_CollectionItem item, M_ModelInfo modMod, DataTable fieldDT)
        {
            string html = htmlHelp.GetHtmlFromSite(StrHelper.UrlDeal(url));
            if (string.IsNullOrEmpty(html)) { WriteLog("项目[" + item.ItemName + "]采集到的目标网址[" + url + "]的值为空",ERROR); return; }
            M_CommonData conMod = new M_CommonData();
            conMod.ModelID = item.ModeID;
            conMod.NodeID = DataConvert.CLng(item.NodeID);
            conMod.TableName = modMod.TableName;
            conMod.Status = (int)ZLEnum.ConStatus.Audited;
            conMod.FirstNodeID = 0;
            conMod.UpDateType = 2;
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            List<M_Coll_FieldFilter> filterList = JsonConvert.DeserializeObject<List<M_Coll_FieldFilter>>(item.InfoPageSettings);
            //系统字段处理
            conMod.Title = GetValueFromHtml(html, "Title", filterList);
            conMod.Inputer = GetValueFromHtml(html, "Inputer", filterList);
            conMod.CreateTime = DataConvert.CDate(GetValueFromHtml(html, "CreateTime", filterList));
            conMod.UpDateTime = DataConvert.CDate(GetValueFromHtml(html, "UpDateTime", filterList));
            conMod.Hits = DataConvert.CLng(GetValueFromHtml(html, "Hits", filterList));
            conMod.EliteLevel = DataConvert.CLng(GetValueFromHtml(html, "EliteLevel", filterList));
            //扩展字段处理
            foreach (DataRow field in fieldDT.Rows)
            {
                DataRow dr = table.NewRow();
                dr["FieldName"] = field["FieldName"];
                dr["FieldType"] = field["FieldType"];
                dr["FieldValue"] = GetValueFromHtml(html, field["FieldName"].ToString(), filterList);
                //对值再处理,避免取错值
                switch (dr["FieldType"].ToString())
                {
                    case "DateType":
                        dr["FieldValue"] = DataConvert.CDate(dr["FieldValue"]);
                        break;
                }
                table.Rows.Add(dr);
            }
            try
            {
                conMod.GeneralID = conBll.AddContent(table, conMod);
                WriteLog("项目[" + item.ItemName + "]对[" + url + "]的采集完成,ID=" + conMod.GeneralID);
            }
            catch (Exception ex) { WriteLog("项目[" + item.ItemName + "]对[" + url + "]的采集,在写入数据库时出错,原因:" + ex.Message, ERROR); }
           
            //----------
            work_count++;
        }
        //从html中抓取所需的值
        private static string GetValueFromHtml(string html,string fieldName,List<M_Coll_FieldFilter> filterList)
        {
            M_Coll_FieldFilter filter = filterList.FirstOrDefault(p => p.field.Equals(fieldName));
            if (filter == null) { WriteLog("未指定字段[" + fieldName + "]的赋值条件",ERROR); return ""; }
            string value = "";
            switch (filter.colltype)
            {
                case "1":
                    value = "";
                    break;
                case "2":
                    value = filter.collvalue;
                    break;
                case "3":
                    if (string.IsNullOrEmpty(filter.collvalue)) { WriteLog("字段[" + filter.field + "]设定使用正则抓取,但却未设定正则规则",ERROR); return ""; }
                    value = htmlHelp.GetByFilter(html, JsonConvert.DeserializeObject<FilterModel>(filter.collvalue));
                    break;
            }
            return value;
        }
        public static void WriteLog(string msg, int logType = 0)
        {
            workLog.Add(new M_Coll_WorkLog()
            {
                msg = msg,
                logType = logType
            });
        }
    }
    public class M_Coll_WorkLog
    {
        public string msg = "";
        public DateTime cdate = DateTime.Now;
        //0:常规日志,1:错误日志,99:单个采集完成日志
        public int logType = 0;
        public M_Coll_WorkLog() { cdate = DateTime.Now; }
    }
    public class M_Coll_FieldFilter
    {
        public string field = "";
        public string colltype = "";
        public string collvalue = "";
    }
}
