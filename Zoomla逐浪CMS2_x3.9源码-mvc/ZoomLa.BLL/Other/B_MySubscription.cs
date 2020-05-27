using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Common;

namespace ZoomLa.BLL
{
    public class B_MySubscription
    {
        public string strTableName,PK;
        private M_MySubscription initMod = new M_MySubscription();
        public B_MySubscription()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_MySubscription SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_MySubscription model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MySubscription model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable GetAllMySubscription()
        {
            string sql = "select * from ZL_MySubscription";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public DataTable GetMySubscriptionByTypeAndUserName(string userName, int orderType)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userName", userName) };
            string sql = "select * from ZL_MySubscription where User_Name=@userName and Order_Type=" + orderType;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        public M_MySubscription GetMySubscriptionById(int id)
        {
            string sql = "select * from ZL_MySubscription where id=@id";

            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@id",SqlDbType.Int)
            };
            cmdParam[0].Value = id;
            M_MySubscription mysub = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, cmdParam))
            {
                if (reader.Read())
                {
                    mysub = new M_MySubscription();
                    mysub.Id = DataConverter.CLng(reader["id"]);
                    mysub.Reference_ID = DataConverter.CLng(reader["reference_ID"]);
                    mysub.Order_Time = DataConverter.CDate(reader["order_Time"]);
                    mysub.Period = DataConverter.CLng(reader["period"]);
                    mysub.Start_Date = DataConverter.CDate(reader["start_Date"]);
                    mysub.Order_Type = DataConverter.CLng(reader["order_Type"]);
                    mysub.User_Name = reader["user_Name"].ToString();
                    mysub.Tel = reader["tel"].ToString();
                    mysub.Email = reader["email"].ToString();
                }
                reader.Close();
                reader.Dispose();

            }
            return mysub;
        }

        public int DelSubscriptionById(int id)
        {
            string sql = "delete ZL_MySubscription where id=@id";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@id", id) };
            sp[0].Value = id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
        }

        public void UpdSubscriptionById(M_MySubscription subscription)
        {
            string sql = "update ZL_MySubscription set Reference_ID=@Reference_ID,Period=@Period,Start_Date=@Start_Date,Order_Type=@Order_Type,User_Name=@User_Name,Tel=@Tel,Email=@Email where id=@id";
            SqlParameter[] sp = new SqlParameter[8] {
                new SqlParameter("@Reference_ID",SqlDbType.Int),
                new SqlParameter("@Period",SqlDbType.Int),
                new SqlParameter("@Start_Date ",SqlDbType.DateTime),
                new SqlParameter("@Order_Type",SqlDbType.BigInt),
                new SqlParameter("@User_Name",SqlDbType.NVarChar),
                new SqlParameter("@Tel",SqlDbType.NVarChar),
                new SqlParameter("@Email",SqlDbType.NVarChar),
                new SqlParameter("@id",SqlDbType.BigInt),
            };
            sp[0].Value = subscription.Reference_ID;
            sp[1].Value = subscription.Period;
            sp[2].Value = subscription.Start_Date;
            sp[3].Value = subscription.Order_Type;
            sp[4].Value = subscription.User_Name;
            sp[5].Value = subscription.Tel;
            sp[6].Value = subscription.Email;
            sp[7].Value = subscription.Id;
            SqlHelper.ExecuteSql(sql, sp);
        }
        public int AddSubscription(M_MySubscription mysub)
        {
            int num = 0;
            string sql = "insert into ZL_MySubscription(Reference_ID,Order_Time,Period,Start_Date,Order_Type,User_Name,Tel,Email) values(" + mysub.Reference_ID + ",'" + mysub.Order_Time + "'," + mysub.Period + ",'" + mysub.Start_Date + "'," + mysub.Order_Type + ",'" + mysub.User_Name + "','" + mysub.Tel + "','" + mysub.Email + "')";
            num = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, null));
            return num;
        }

        public int DeleteMySubByIDS(string id)
        {
            string sql = "delete ZL_MySubscription where id in (" + id + ")";
            //SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            //sp[0].Value = id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public DataTable SelMySubscriptionByPeriod(int period)
        {
            string sql = "select * from ZL_MySubscription where Period =" + period;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }

        public DataTable SelMySubscriptionByReferenceID(int id)
        {
            string sql = "select * from ZL_MySubscription where Reference_ID =" + id;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }

        public DataTable SelMySubscriptionByUserName(string name)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            string sql = "select * from ZL_MySubscription where User_Name=@name";

            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        public DataTable SelMySubscriptionByOrderType(int type)
        {
            string sql = "select * from ZL_MySubscription where Order_Type=" + type;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }

        public DataTable SelMySubscriptionByOrderTime(DateTime time1, DateTime time2)
        {
            string sql = "select * from ZL_MySubscription where Order_Time>" + time1 + " and Order_Time<" + time2;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 查询方法根据内容id和用户名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetMySubscriptionName(string name)
        {
            string sql = "select * from ZL_MySubscription where User_Name=@User_Name and Order_Type=4 ";
            M_MySubscription mysub = new M_MySubscription();
            SqlParameter[] sp = new SqlParameter[1]
            {
                new SqlParameter("User_Name",SqlDbType.NVarChar)
            };
            sp[0].Value = name;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public bool UpdateOrderTyleName(M_MySubscription sub)
        {
            string sql = "update ZL_MySubscription set Order_Type=@Order_Type where Reference_ID=@Reference_ID and User_Name=@User_Name ";
            SqlParameter[] sp = new SqlParameter[3]
            {
                new SqlParameter("@Order_Type",SqlDbType.Int),
                new SqlParameter("@Reference_ID",SqlDbType.Int),
                new SqlParameter("@User_Name",SqlDbType.NVarChar)
            };
            sp[0].Value = sub.Order_Type;
            sp[1].Value = sub.Reference_ID;
            sp[2].Value = sub.User_Name;
            return SqlHelper.ExecuteSql(sql, sp);
        }
        //查询是否存在
        public DataTable GetMySubscriptionNameReference_ID(string name, int type)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            string sql = "select * from ZL_MySubscription where User_Name=@name and Reference_ID=" + type;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public void UpdSubscription(M_MySubscription subscription)
        {
            string sql = "update ZL_MySubscription set Reference_ID=@Reference_ID,Period=@Period,Start_Date=@Start_Date,Order_Type=@Order_Type,User_Name=@User_Name,Tel=@Tel,Email=@Email where id=@id";
            SqlParameter[] sp = new SqlParameter[8] {
                new SqlParameter("@Reference_ID",SqlDbType.Int),
                new SqlParameter("@Period",SqlDbType.Int),
                new SqlParameter("@Start_Date ",SqlDbType.DateTime),
                new SqlParameter("@Order_Type",SqlDbType.BigInt),
                new SqlParameter("@User_Name",SqlDbType.NVarChar),
                new SqlParameter("@Tel",SqlDbType.NVarChar),
                new SqlParameter("@Email",SqlDbType.NVarChar),
                new SqlParameter("@id",SqlDbType.BigInt),
            };
            sp[0].Value = subscription.Reference_ID;
            sp[1].Value = subscription.Period;
            sp[2].Value = subscription.Start_Date;
            sp[3].Value = subscription.Order_Type;
            sp[4].Value = subscription.User_Name;
            sp[5].Value = subscription.Tel;
            sp[6].Value = subscription.Email;
            sp[7].Value = subscription.Id;
            SqlHelper.ExecuteSql(sql, sp);
        }
    }
}
