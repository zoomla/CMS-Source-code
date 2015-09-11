namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;

    /// <summary>
    /// SD_ModelField 的摘要说明
    /// </summary>
    public class SD_ModelField : ID_ModelField
    {

        #region ID_ModelField 成员

        public bool Add(ZoomLa.Model.M_ModelField MField)
        {
            string strSql = "PR_ModelField_Add";
            SqlParameter[] cmdParams = GetParameters(MField);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        public bool Update(ZoomLa.Model.M_ModelField MField)
        {
            string strSql = "PR_ModelField_Update";
            SqlParameter[] cmdParams = GetParameters(MField);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        public bool AddFieldToTable(string TableName, string FieldName, string FieldType, string DefaultValue)
        {
            string strSql = "PR_AddFieldToTable";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@TableName", TableName),
                new SqlParameter("@FieldName", FieldName),
                new SqlParameter("@FieldType", FieldType),
                new SqlParameter("@DefaultValue", DefaultValue)
            };            
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        public bool Del(int FieldId)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@FieldId", SqlDbType.Int,4) };
            cmdParams[0].Value = FieldId;
            return SqlHelper.ExecuteProc("PR_ModelField_Del", cmdParams);
        }

        public bool DelField(string TableName, string FieldName)
        {
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@TableName", SqlDbType.NVarChar,50),
                new SqlParameter("@FieldName", SqlDbType.NVarChar,50)
            };
            cmdParams[0].Value = TableName;
            cmdParams[1].Value = FieldName;
            return SqlHelper.ExecuteProc("PR_DelTableField", cmdParams);
        }
        public bool UpdateOrder(M_ModelField info)
        {
            string strSql = "Update ZL_ModelField Set OrderId=@OrderId Where FieldID=@FieldID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@OrderId", SqlDbType.Int),
                new SqlParameter("@FieldID", SqlDbType.Int)
            };
            cmdParams[0].Value = info.OrderID;
            cmdParams[1].Value = info.FieldID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public bool IsExists(int ModelID, string fieldname)
        {
            string strSql = "select Count(FieldID) from ZL_ModelField where ModelId=@ModelId and FieldName=@FieldName";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@ModelId", SqlDbType.Int,4),
                new SqlParameter("@FieldName", SqlDbType.NVarChar,50)
            };
            cmdParams[0].Value = ModelID;
            cmdParams[1].Value = fieldname;
            return SqlHelper.Exists(CommandType.Text, strSql, cmdParams);
        }
        public int GetMaxOrder(int ModelID)
        {
            string strSql = "select Max(OrderId) from ZL_ModelField where ModelId=@ModelId";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4)
            };
            cmdParam[0].Value = ModelID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        public int GetMinOrder(int ModelID)
        {
            string strSql = "select Min(OrderId) from ZL_ModelField where ModelId=@ModelId";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4)
            };
            cmdParam[0].Value = ModelID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        public int GetPreID(int ModelID, int CurrentID)
        {
            string strSql = "select top 1 FieldID from ZL_ModelField where ModelId=@ModelId and OrderId<@CurrentID order by OrderId Desc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4),
                new SqlParameter("@CurrentID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = ModelID;
            cmdParam[1].Value = CurrentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        public int GetNextID(int ModelID, int CurrentID)
        {
            string strSql = "select Top 1 FieldID from ZL_ModelField where ModelId=@ModelId and OrderId>@CurrentID order by OrderId Asc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4),
                new SqlParameter("@CurrentID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = ModelID;
            cmdParam[1].Value = CurrentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }        
        public M_ModelField GetModelByOrder(int ModelID,int Order)
        {
            string strSql = "select * from ZL_ModelField where ModelId=@ModelId and OrderId=@OrderId";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4),
                new SqlParameter("@OrderId",SqlDbType.Int,4)};
            cmdParams[0].Value = ModelID;
            cmdParams[1].Value = Order;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (sdr.Read())
                {
                    return GetFieldFromReader(sdr);
                }
                else
                {
                    return new M_ModelField(true);
                }
            }
        }
        public M_ModelField GetModelByID(int FieldID)
        {
            string strSql = "select * from ZL_ModelField where FieldID=@FieldID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@FieldID",SqlDbType.Int,4)};
            cmdParams[0].Value = FieldID;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (sdr.Read())
                {
                    return GetFieldFromReader(sdr);
                }
                else
                {
                    return new M_ModelField(true);
                }
            }
        }
        public M_ModelField GetModelByFieldName(int ModelID, string FieldName)
        {
            string strSql = "select * from ZL_ModelField where ModelId=@ModelId and FieldName=@FieldName";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4),
                new SqlParameter("@FieldName",SqlDbType.NVarChar)};
            cmdParams[0].Value = ModelID;
            cmdParams[1].Value = FieldName;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (sdr.Read())
                {
                    return GetFieldFromReader(sdr);
                }
                else
                {
                    return new M_ModelField(true);
                }
            }
        }
        public DataSet GetModelFieldList(int ModelID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ModelID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ModelID;
            string strSql = "select * from ZL_ModelField where ModelID=@ModelID order by OrderID";
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql, cmdParams);
        }
        #endregion
        /// <summary>
        /// 将模型信息的各属性值传递到参数中
        /// </summary>
        /// <param name="administratorInfo"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_ModelField ModelField)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@FieldID", SqlDbType.Int, 4),
                new SqlParameter("@ModelID", SqlDbType.Int, 4),
                new SqlParameter("@FieldName", SqlDbType.NVarChar, 50),
                new SqlParameter("@FieldAlias", SqlDbType.NVarChar, 20),
                new SqlParameter("@FieldTips", SqlDbType.NVarChar, 50),
                new SqlParameter("@FieldType", SqlDbType.NVarChar, 50),
                new SqlParameter("@Description", SqlDbType.NVarChar, 255),
                new SqlParameter("@Content", SqlDbType.NText),
                new SqlParameter("@IsNotNull", SqlDbType.Bit),
                new SqlParameter("@IsSearchForm", SqlDbType.Bit)
            };
            parameter[0].Value = ModelField.FieldID;
            parameter[1].Value = ModelField.ModelID;
            parameter[2].Value = ModelField.FieldName;
            parameter[3].Value = ModelField.FieldAlias;
            parameter[4].Value = ModelField.FieldTips;
            parameter[5].Value = ModelField.FieldType;
            parameter[6].Value = ModelField.Description;
            parameter[7].Value = ModelField.Content;
            parameter[8].Value = ModelField.IsNotNull;
            parameter[9].Value = ModelField.IsSearchForm;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取模型记录
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns>M_ModelField 模型信息</returns>
        private static M_ModelField GetFieldFromReader(SqlDataReader rdr)
        {
            M_ModelField info = new M_ModelField();
            info.FieldID = DataConverter.CLng(rdr["FieldID"]);
            info.ModelID = DataConverter.CLng(rdr["ModelID"]);
            info.FieldName = rdr["FieldName"].ToString();
            info.FieldAlias = rdr["FieldAlias"].ToString();
            info.FieldTips = rdr["FieldTips"].ToString();
            info.Description = rdr["Description"].ToString();
            info.IsNotNull = DataConverter.CBool(rdr["IsNotNull"].ToString());
            info.IsSearchForm = DataConverter.CBool(rdr["IsSearchForm"].ToString());
            info.FieldType = rdr["FieldType"].ToString();
            info.Content = rdr["Content"].ToString();
            info.OrderID = DataConverter.CLng(rdr["OrderID"]);
            return info;
        }
    }
}