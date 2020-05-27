using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ZoomLa.Safe.Helper;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

/// <summary>
/// OfficeHelper Excel与Word等处理,不使用插件
/// </summary>
public class OfficeHelper
{
    /// <summary>
    /// Excel(CSV),后缀名为xls,配合SafeSC.DownStr();
    /// </summary>
    /// 缺点:不能使用xlsx,会出警告
    /// <param name="dt">需要生成为Excel的数据表</param>
    /// <title>自定义标题字段,如为空,则使用dt的字段名为头部.格式:1,2,3</title>
    /// <returns>序列化后字符串</returns>
    public string  ExportExcel(DataTable dt,string columnNames="")
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(columnNames))
        {
            string[] titleArr = columnNames.Split(',');
            foreach (string s in titleArr)
            {
                sb.Append(s + "\t");
            }
        }
        else
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + "\t");
            }
        }
        sb.Append("\n");
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dr[i]+"\t");
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }
    /// <summary>
    /// 从数据库中获取数据,并生成Excel,字段填充后台完成,不要开放给前台用户
    /// </summary>
    /// <param name="tbname">数据库表名</param>
    /// <param name="where">数据库删选条件</param>
    /// <param name="columnNames">要出的列名,无则使用dt的列名</param>
    /// <param name="fields">需要导出的字段列表(!--为安全考虑选择性开放--)</param>
    /// title用地址栏传,其余用Session传,不允许地址栏与Form传值
    /// 示例:SafeSC.DownStr(officeHelp.GetExcelByDB("ZL_User", "","用户名,用户ID", "UserName", "UserID"), title);
    public string GetExcelByDB(string tbname, string where, string fields, string columnNames)
    {
        SafeSC.CheckDataEx(tbname);
        SafeSC.CheckDataEx(fields.Split(','));
        foreach (string s in fields.Split(','))
        {
            if (s.Equals("*"))
                fields = "*,";
            else
                fields += "[" + s + "],";
        }
        fields = fields.TrimEnd(',');
        string sql = "Select " + fields + " From " + tbname + where;
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
        return ExportExcel(dt, columnNames);
    }
    /// <summary>
    /// 从内存表中筛选指定列输出
    /// </summary>
    /// 示例：GetExcelByDT(dt,"UserID,UserName","用户ID,用户名");
    public string GetExcelByDT(DataTable dt, string fields, string columnNames)
    {
        dt = dt.DefaultView.ToTable(false, fields.Split(','));
        return ExportExcel(dt, columnNames);
    }
    /// <summary>
    /// 将CSV文件的数据读取到DataTable中
    /// </summary>
    /// <param name="vpath">CSV文件虚拟路径</param>
    /// <returns>返回读取了CSV数据的DataTable</returns>
    public DataTable OpenCSV(string vpath)
    {
        string ppath = function.VToP(vpath);
        Encoding encoding = new FileEncodeHelper().GetType(ppath);
        DataTable dt = new DataTable();
        FileStream fs = new FileStream(vpath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        StreamReader sr = new StreamReader(fs, encoding);
        //string fileContent = sr.ReadToEnd();
        //encoding = sr.CurrentEncoding;
        //记录每次读取的一行记录
        string strLine = "";
        //记录每行记录中的各字段内容
        string[] aryLine = null;
        string[] tableHead = null;
        //标示列数
        int columnCount = 0;
        //标示是否是读取的第一行
        bool IsFirst = true;
        //逐行读取CSV中的数据
        while ((strLine = sr.ReadLine()) != null)
        {
            if (IsFirst == true)
            {
                tableHead = strLine.Split(',');
                IsFirst = false;
                columnCount = tableHead.Length;
                //创建列
                for (int i = 0; i < columnCount; i++)
                {
                    DataColumn dc = new DataColumn(tableHead[i]);
                    dt.Columns.Add(dc);
                }
            }
            else
            {
                aryLine = strLine.Split(',');
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columnCount; j++)
                {
                    dr[j] = aryLine[j];
                }
                dt.Rows.Add(dr);
            }
        }
        if (aryLine != null && aryLine.Length > 0)
        {
            dt.DefaultView.Sort = tableHead[0] + " " + "asc";
        }

        sr.Close();
        fs.Close();
        return dt;
    }
    //-----Word
    /// <summary>
    /// 将html导出为Word
    /// </summary>
    /// <param name="html">必须以<html>包裹</param>
    /// <param name="vpath">保存路径</param>
    /// <returns></returns>
    public static string W_HtmlToWord(string html, string vpath)
    {
        if (string.IsNullOrEmpty(html)) { throw new Exception("未指定需要生成Word的内容"); }
        if (string.IsNullOrEmpty(vpath)) { throw new Exception("未指定Word保存路径"); }
        string ppath = function.VToP(vpath);
        string pdir = Path.GetDirectoryName(ppath);
        if (!Directory.Exists(pdir)) { Directory.CreateDirectory(pdir); }

        byte[] array = Encoding.UTF8.GetBytes(html);
        MemoryStream stream = new MemoryStream(array);
        using (WordprocessingDocument myDoc = WordprocessingDocument.Create(ppath, WordprocessingDocumentType.Document))
        {
            string altChunkId = "AltChunkId";
            MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
            mainPart.Document = new Document();
            mainPart.Document.Body = new Body();
            var chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Html, altChunkId);
            chunk.FeedData(stream);
            AltChunk altChunk = new AltChunk() { Id = altChunkId };
            mainPart.Document.Append(altChunk);
            mainPart.Document.Save();
            stream.Dispose();
        }
        return vpath;
    }
}