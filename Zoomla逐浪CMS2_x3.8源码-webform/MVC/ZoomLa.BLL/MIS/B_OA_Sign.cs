﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_OA_Sign
    {
        public string TbName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_OA_Sign model = new M_OA_Sign();
        public B_OA_Sign() 
        {
            TbName = model.TbName;
            PK = model.PK;
        }
        //-----------------Insert
        /// <summary>
       /// 添加一条新的数据
       /// </summary>
       /// <param name="appID">申请ID</param>
       /// <param name="proID">使用的流程ID</param>
       /// <param name="userID">审批人</param>
       /// <param name="result">审批结果:0未审,2未通过,99成功</param>
       /// <param name="remind">备注</param>
       /// <param name="createDate">创建日期</param>
       /// <returns></returns>
        public int Insert(M_OA_Sign model)
        {
            return Sql.insert(TbName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        //-----------------Retrieve
        public DataTable SelAll() 
        {
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select a.*,b.UserName From "+TbName+" as a Left Join ZL_User as b On a.OwnUserID=b.UserID");
            return dt;
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据申请单ID,查看结果
        /// </summary>
        public DataTable SelByUserID(int userID) 
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userID", userID) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where OwnUserID=@userID", sp);
        }
        public M_OA_Sign SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_OA_Sign SelModelByUserID(int userID) 
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, "UserID", userID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_OA_Sign SelByPwd(int id, string pwd)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("pwd", pwd) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE ID=" + id + " AND SignPwd=@pwd",sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else { return null; }
            }
        }
        //----------------Update
        public bool UpdateByID(M_OA_Sign model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
        //----------------Delete
        public bool DelByID(int id) 
        {
           return Sql.Del(TbName,id);
        }
        /// <summary>
        /// 是否拥有对应权限
        /// </summary>
        /// <param name="authName">需验证的权限</param>
        /// <param name="authArr">用户所拥有的权限</param>
        public bool HasAuth(string authName,string[] authArr )
        {
            return authArr.Select(p => p).Contains(authName);
        }
    }
}
