using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;

namespace ZoomLa.Components
{
   public sealed class  ExcelImport
    {
        /// <summary>
        /// 从excel文件中读取数据
        /// </summary>
        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            string fileExtension = Path.Substring(Path.IndexOf(".") + 1);
            if (fileExtension == "xlsx")
                strConn = "Provider='Microsoft.ACE.OLEDB.12.0';" + "Data Source=" + Path + ";" + "Extended Properties=Excel 12.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();
            return ds;
        }
        /// <summary>
        /// 将CSV文件加载到ds中
        /// </summary>
        public void CsvToDataSet(DataSet ds, string csvPath)
        {
            string fileFullName = Path.GetFileName(csvPath);//例:模板.csv
            string folderPath = csvPath.Substring(0, csvPath.LastIndexOf('\\') + 1);//例:E:\Code\Zoomla6x\ZoomLa.WebSite\xls\ 
            string connStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='text;HDR=Yes;IMEX=1'", folderPath);
            string sql = string.Format(@"SELECT * FROM [{0}]", fileFullName);
            OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr);
            da.Fill(ds);
        }
    }
}
