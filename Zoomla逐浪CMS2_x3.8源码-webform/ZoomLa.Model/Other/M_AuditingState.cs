using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    /// 审核状态
    /// </summary>
    [Serializable]
   public class M_AuditingState:M_Base
    {

       #region 构造方法
       public M_AuditingState()
       {

       }
        #endregion

        #region 属性、字段
        /// <summary>
        /// 状态码
        /// </summary>
        public int StateCode { get; set; }
       /// 状态名称
       /// </summary>
       public string StateName { get; set; }
       /// <summary>
       /// 状态类型
       /// </summary>
       public string StateType { get; set; }
       #endregion
       public override string PK { get { return "stateCode"; } }
       public override string TbName { get { return "ZL_AuditingState"; } }
       public override  string[,] FieldList()
       {
           string[,] Tablelist = {
                                  {"stateCode","Int","4"},
                                  {"stateName","VarChar","50"},
                                  {"stateType","VarChar","50"}
                                 };
           return Tablelist;
       }
       public  string GetFieldAndPara()
       {
           string str = string.Empty;
           string[,] strArr = FieldList();
           for (int i = 0; i < strArr.GetLength(0); i++)
           {
               if (strArr[i, 0] != PK)
               {
                   str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
               }
           }
           return str.Substring(0, str.Length - 1);
       }

       public override SqlParameter[] GetParameters()
       {
           M_AuditingState model = this;
           SqlParameter[] sp = GetSP();
           sp[0].Value = model.StateCode;       
           sp[1].Value = model.StateName;       
           sp[2].Value = model.StateType;       

           return sp;
       }

       public  M_AuditingState GetModelFromReader(SqlDataReader rdr)
       {
           M_AuditingState model = new M_AuditingState();
           model.StateCode = Convert.ToInt32(rdr["stateCode"]);
           model.StateName = ConverToStr(rdr["stateName"]);
           model.StateType =ConverToStr(rdr["stateType"]);
           rdr.Close();
           rdr.Dispose();
           return model;
       }
    }
}
