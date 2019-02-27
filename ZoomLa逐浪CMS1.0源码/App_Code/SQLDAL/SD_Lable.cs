using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using ZoomLa.IDAL;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.Common;

namespace ZoomLa.SQLDAL
{
    /// <summary>
    /// SD_Lable 的摘要说明
    /// </summary>
    public class SD_Lable : ID_Label
    {
        public SD_Lable()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }       

        #region ID_Label 成员

        void ID_Label.AddLabel(M_Label label)
        {
            string strSql = "PR_Label_Add";
            SqlParameter[] cmdParams = GetParameters(label);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        void ID_Label.UpdateLabel(M_Label label)
        {
            string strSql = "PR_Label_Update";
            SqlParameter[] cmdParams = GetParameters(label);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        void ID_Label.DelLabel(int LabelID)
        {
            string strSql="Delete from ZL_Label where LabelID=@LabelID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@LabelID", SqlDbType.Int) };
            cmdParams[0].Value = LabelID;
            SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        M_Label ID_Label.GetLabel(string LabelName)
        {
            string strSql = "Select * from ZL_Label Where LabelName=@LabelName";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@LabelName", SqlDbType.NVarChar, 50) };
            cmdParams[0].Value = LabelName;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                    return new M_Label(true);
            }
        }

        string ID_Label.GetLabelCateList()
        {
            string strSql = "Select distinct LabelCate from ZL_Label";
            string LabelCateList = "";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (reader.Read())
                {
                    LabelCateList = LabelCateList + reader["LabelCate"].ToString() + ",";
                }
            }
            return LabelCateList;
        }

        DataSet ID_Label.GetLabelAll()
        {
            string strSql = "Select LabelName,LabelType,LabelCate,LabelDesc,LabelParam,LabelTable,LabelField,LabelWhere,LabelOrder,LabelContent,LabelCount,LabelIdentity from ZL_Label Order by LabelID ASC";
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql, null);
        }

        bool ID_Label.IsExist(string LabelName)
        {
            string strSql = "select count(LabelID) from ZL_Label where LabelName=@LabelName";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@LabelName",SqlDbType.NVarChar)
            };
            sp[0].Value = LabelName;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp))>0;
        }

        DataTable ID_Label.GetCateList()
        {
            string strSql = "Select distinct LabelCate from ZL_Label";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        DataTable ID_Label.GetSourceList()
        {
            string strSql = "Select LabelID,LabelName from ZL_Label where LabelType=3";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        M_Label ID_Label.GetLabel(int LabelID)
        {
            string strSql = "Select * from ZL_Label Where LabelID=@LabelID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@LabelID", SqlDbType.NVarChar, 50) };
            cmdParams[0].Value = LabelID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                    return new M_Label(true);
            }
        }

        DataSet ID_Label.GetLabelList(string LabelCate,int PageSize,int CPage)
        {
            string strSql = "PR_GetRecordFromPage";
            SqlParameter[] cmdParam = new SqlParameter[] { 
                new SqlParameter("@TableName",SqlDbType.VarChar),               //表名，可以是多个表 
                new SqlParameter("@Identity",SqlDbType.VarChar),
                new SqlParameter("@Fields",SqlDbType.VarChar),          //要取出的字段，可以是多个表的字段，可以为空，为空表示select *
                new SqlParameter("@sqlWhere",SqlDbType.VarChar),        //条件，可以为空，不用填 where                        
                new SqlParameter("@OrderField",SqlDbType.VarChar),      //排序字段，可以为空，为空默认按主键升序排列，不用填 order by
                new SqlParameter("@pageSize",SqlDbType.Int),            //每页记录数
                new SqlParameter("@pageIndex",SqlDbType.Int)            //当前页，0表示第1页
            };
            cmdParam[0].Value = "ZL_Label";
            cmdParam[1].Value = "LabelID";            
            cmdParam[2].Value = "*";
            if (string.IsNullOrEmpty(LabelCate))
            {
                cmdParam[3].Value = "";
            }
            else
            {
                cmdParam[3].Value = "LabelCate='" + LabelCate + "'";
            }
            cmdParam[4].Value = "LabelID DESC";
            cmdParam[5].Value = PageSize;
            cmdParam[6].Value = CPage-1;            

            return SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, strSql, cmdParam);
        }
        DataTable ID_Label.GetLabelListByCate(string LabelCate)
        {
            string strSql = "select * from ZL_Label where LabelCate=@LabelCate";
            SqlParameter[] cmdParam = new SqlParameter[] {                 
                new SqlParameter("@LabelCate",SqlDbType.NVarChar)
            };
            cmdParam[0].Value = LabelCate;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParam);
        }
        int ID_Label.GetLabelListCount(string LabelCate)
        {
            string strSql = "";
            if (string.IsNullOrEmpty(LabelCate))
            {
                strSql = "select Count(LabelID) from ZL_Label";
            }
            else
            {
                strSql = "select Count(LabelID) from ZL_Label Where LabelCate='" + LabelCate + "'";
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text,strSql,null));
        }
        #endregion
        private static SqlParameter[] GetParameters(M_Label Info)
        {
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@LabelID", SqlDbType.Int),
                new SqlParameter("@LabelName",SqlDbType.NVarChar,50),
                new SqlParameter("@LabelType",SqlDbType.Int),
                new SqlParameter("@LabelCate",SqlDbType.NVarChar,50),
                new SqlParameter("@LabelDesc",SqlDbType.NVarChar,255),
                new SqlParameter("@LabelParam",SqlDbType.NText),
                new SqlParameter("@LabelTable",SqlDbType.NText),
                new SqlParameter("@LabelField",SqlDbType.NText),
                new SqlParameter("@LabelWhere",SqlDbType.NText),
                new SqlParameter("@LabelOrder",SqlDbType.NText),
                new SqlParameter("@LabelContent",SqlDbType.NText),
                new SqlParameter("@LabelCount",SqlDbType.NVarChar,50)
            };
            parameter[0].Value = Info.LabelID;
            parameter[1].Value = Info.LableName;
            parameter[2].Value = Info.LableType;
            parameter[3].Value = Info.LabelCate;
            parameter[4].Value = Info.Desc;
            parameter[5].Value = Info.Param;
            parameter[6].Value = Info.LabelTable;
            parameter[7].Value = Info.LabelField;
            parameter[8].Value = Info.LabelWhere;
            parameter[9].Value = Info.LabelOrder;
            parameter[10].Value = Info.Content;
            parameter[11].Value = Info.LabelCount;
            return parameter;
        }
        private static M_Label GetInfoFromReader(SqlDataReader rdr)
        {
            M_Label label = new M_Label();
            label.LabelID = DataConverter.CLng(rdr["LabelID"]);
            label.LableName = rdr["LabelName"].ToString();
            label.LableType = DataConverter.CLng(rdr["LabelType"]);
            label.LabelCate = rdr["LabelCate"].ToString();
            label.Desc = rdr["LabelDesc"].ToString();
            label.Param = rdr["LabelParam"].ToString();
            label.LabelTable = rdr["LabelTable"].ToString();
            label.LabelField = rdr["LabelField"].ToString();
            label.LabelWhere = rdr["LabelWhere"].ToString();
            label.LabelOrder = rdr["LabelOrder"].ToString();
            label.Content = rdr["LabelContent"].ToString();
            label.LabelCount = rdr["LabelCount"].ToString();
            return label;
        }

        #region ID_Label 成员


        DataTable ID_Label.GetSchemaTable()
        {
            return SqlHelper.GetSchemaTable();
        }

        DataTable ID_Label.GetTableField(string TableName)
        {
            return SqlHelper.GetTableColumn(TableName);
        }

        #endregion
        
    }
}