using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public class B_Shopconfig
    {
        public string strTableName, PK;
        private M_Shopconfig initMod = new M_Shopconfig();
        public B_Shopconfig()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }

        #region ID_Shopconfig 成员

        public DataTable GetShopconfiginfo()
        {
            string sqlStr = "select top 1 * from " + strTableName;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        public M_Shopconfig GetShopconfig()
        {
            string sqlStr = "select top 1 * from " + strTableName;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, null))
            {
                if (rdr.Read())
                {
                    return initMod.GetModelFromReader(rdr);
                }
                else
                {
                    return new M_Shopconfig();
                }
            }
        }
        public bool UpdateShopconfig(M_Shopconfig sinfo)
        {
            string str = "Update " + strTableName + " Set IsOpen=@IsOpen,BankInfo=@BankInfo,Anonymity=@Anonymity,Pointcard=@Pointcard,Dummymoney=@Dummymoney,Comment=@Comment,goodpl=@goodpl,	centerpl=@centerpl,	badpl=@badpl,Auditing=@Auditing,ScorePoint=@ScorePoint,ChangeOrder=@ChangeOrder";
            SqlParameter[] sp = sinfo.GetParameters();
            return SqlHelper.ExecuteSql(str, sp);
        }
        public bool AdddateShopconfig(M_Shopconfig sinfo)
        {
            string sqlStr = "insert into ZL_Shopconfig(IsOpen,BankInfo,Anonymity,Pointcard,Dummymoney,Comment,goodpl,centerpl,badpl,Auditing,ScorePoint,ChangeOrder) values (@IsOpen,@BankInfo,@Anonymity,@Pointcard,@Dummymoney,@Comment,@goodpl,@centerpl,@badpl,@Auditing,@ScorePoint,@ChangeOrder)";
            SqlParameter[] sp = sinfo.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        #endregion
    }
}