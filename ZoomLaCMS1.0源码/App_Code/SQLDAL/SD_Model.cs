namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using ZoomLa.Model;
    using ZoomLa.Common;

    /// <summary>
    /// SD_Model 的摘要说明
    /// </summary>
    public class SD_Model : ID_Model
    {
        public SD_Model()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region ID_Model 成员
        /// <summary>
        /// 添加模型到数据库
        /// </summary>
        /// <param name="ModelInfo">要添加的模型信息</param>
        /// <returns>返回添加状态信息 成功为true</returns>
        bool ID_Model.Add(M_ModelInfo ModelInfo)
        {
            string strSql = "PR_Model_Add";            
            SqlParameter[] cmdParams = GetParameters(ModelInfo);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        /// <summary>
        /// 添加会员模型
        /// </summary>
        /// <param name="ModelInfo"></param>
        /// <returns></returns>
        bool ID_Model.AddUserModel(M_ModelInfo ModelInfo)
        {
            string strSql = "PR_UserModel_Add";
            SqlParameter[] cmdParams = GetParameters(ModelInfo);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        /// <summary>
        /// 从数据库读取指定ID值的模型信息
        /// </summary>
        /// <param name="ModelID">模型ID</param>
        /// <returns>模型信息</returns>
        M_ModelInfo ID_Model.GetModelInfo(int ModelID)
        {
            string strSql = "Select * from ZL_Model Where ModelID=@ModelID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ModelID", SqlDbType.Int,4) };
            cmdParams[0].Value=ModelID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetModelFromReader(reader);
                }
                else
                    return new M_ModelInfo(true);
            }
        }
        /// <summary>
        /// 更新模型信息
        /// </summary>
        /// <param name="ModelInfo">模型的新信息</param>
        /// <returns>返回更新状态 成功为true</returns>
        bool ID_Model.Update(M_ModelInfo ModelInfo)
        {
            string strSql = "PR_Model_Add";
            SqlParameter[] cmdParams = GetParameters(ModelInfo);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }        
        /// <summary>
        /// 读取内容模型信息集合 提供给页面列表
        /// </summary>
        /// <returns>模型信息列表</returns>
        DataTable ID_Model.ListModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=1";
            DataTable dt=SqlHelper.ExecuteTable(CommandType.Text,strSql,null);
            return dt;
        }
        DataTable ID_Model.ListUserModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=3";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        DataTable ID_Model.ListShopModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=2";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        bool ID_Model.Delete(int ModelID)
        {
            string strSql = "DELETE FROM ZL_Model WHERE ModelId=@ModelID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ModelID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ModelID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        bool ID_Model.DeleteTable(string TableName)
        {
            string strSql = "PR_Model_Del";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@TableName", SqlDbType.NVarChar,255) };
            cmdParams[0].Value = TableName;
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        bool ID_Model.UpdateInput(string InputCode, int ModelID)
        {
            string strSql = "Update ZL_Model Set InputHtml=@InputCode Where ModelID=@ModelID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@InputCode", SqlDbType.NText),
                new SqlParameter("@ModelID", SqlDbType.Int, 4) };
            cmdParams[0].Value = InputCode;
            cmdParams[1].Value = ModelID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        //更新指定模板
        bool ID_Model.UpdateModule(string Template, int ModelID)
        {
            string strSql = "Update ZL_Model Set ContentTemplate=@Template Where ModelID=@ModelID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@Template", SqlDbType.NVarChar,255),
                new SqlParameter("@ModelID", SqlDbType.Int, 4) };
            cmdParams[0].Value = Template;
            cmdParams[1].Value = ModelID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        #endregion
        /// <summary>
        /// 将模型信息的各属性值传递到参数中
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_ModelInfo Info)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@ModelID", SqlDbType.Int, 4),
                new SqlParameter("@ModelName", SqlDbType.NVarChar, 20),
                new SqlParameter("@Description", SqlDbType.NVarChar, 255),
                new SqlParameter("@TableName", SqlDbType.NVarChar, 50),
                new SqlParameter("@ItemName", SqlDbType.NVarChar, 50),
                new SqlParameter("@ItemUnit", SqlDbType.NVarChar, 50),
                new SqlParameter("@ItemIcon", SqlDbType.NVarChar, 50),
                new SqlParameter("@ModelType",SqlDbType.Int)
            };
            parameter[0].Value = Info.ModelID;
            parameter[1].Value = Info.ModelName;
            parameter[2].Value = Info.Description;
            parameter[3].Value = Info.TableName;
            parameter[4].Value = Info.ItemName;
            parameter[5].Value = Info.ItemUnit;
            parameter[6].Value = Info.ItemIcon;
            parameter[7].Value = Info.ModelType;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取模型记录
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns>M_ModelInfo 模型信息</returns>
        private static M_ModelInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_ModelInfo info = new M_ModelInfo();
            info.ModelID = DataConverter.CLng(rdr["ModelID"].ToString());
            info.ModelName = rdr["ModelName"].ToString();
            info.Description = rdr["Description"].ToString();
            info.TableName = rdr["TableName"].ToString();
            info.ItemName = rdr["ItemName"].ToString();
            info.ItemUnit = rdr["ItemUnit"].ToString();
            info.ItemIcon = rdr["ItemIcon"].ToString();
            info.ContentModule = rdr["ContentTemplate"].ToString();
            info.ModelType = DataConverter.CLng(rdr["ModelType"].ToString());
            return info;
        }
    }
}