using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Globalization;
using System.Collections.Generic;

using System.Xml;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_UserRegisterIP
    {
        public B_UserRegisterIP()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        
        public static M_UserRegisterIP GetRegisterIPAll()
        {
             
            string sqlStr = "SELECT * FROM ZL_UserRegisterIP where Id = 1";

            M_UserRegisterIP registerIP = new M_UserRegisterIP();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr))
            {
                if (reader.Read())
                {
                    registerIP.Id = DataConverter.CLng(reader["Id"].ToString());
                    registerIP.LimitTime = DataConverter.CLng(reader["LimitTime"].ToString());
                    registerIP.IsLimit = DataConverter.CLng(reader["IsLimit"].ToString());
                    registerIP.IsIPpart = DataConverter.CLng(reader["IsIPpart"].ToString());
                    registerIP.BeginIP = reader["BeginIP"].ToString();
                    registerIP.EndIP = reader["EndIP"].ToString();
                }
                reader.Close();
                reader.Dispose();
            }

            return registerIP;
        }

        public static bool UpdateRegisterIP(int LimitTime, int IsLimit, int IsIPpart, string BeginIP, string EndIP)
        {
            string sqltxt = "select * from ZL_UserRegisterIP";
            if (SqlHelper.ExecuteTable(CommandType.Text, sqltxt, null).Rows.Count > 0)
            {
                string sqlStr = "UPDATE ZL_UserRegisterIP SET LimitTime = @LimitTime,IsLimit=@IsLimit,IsIPpart=@IsIPpart,BeginIP=@BeginIP,EndIP=@EndIP where Id = 1";
                SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@LimitTime", SqlDbType.Int),
                new SqlParameter("@IsLimit", SqlDbType.Int),
                new SqlParameter("@IsIPpart", SqlDbType.Int),
                new SqlParameter("@BeginIP", SqlDbType.VarChar),
                new SqlParameter("@EndIP", SqlDbType.VarChar)
            };
                sp[0].Value = LimitTime;
                sp[1].Value = IsLimit;
                sp[2].Value = IsIPpart;
                sp[3].Value = BeginIP;
                sp[4].Value = EndIP;
                return SqlHelper.ExecuteSql(sqlStr, sp);
            }
            else
            {
                string sqlStr = "Insert Into ZL_UserRegisterIP(LimitTime,IsLimit,IsIPpart,BeginIP,EndIP) values(@LimitTime,@IsLimit,@IsIPpart,@BeginIP,@EndIP)";
                SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@LimitTime", SqlDbType.Int),
                new SqlParameter("@IsLimit", SqlDbType.Int),
                new SqlParameter("@IsIPpart", SqlDbType.Int),
                new SqlParameter("@BeginIP", SqlDbType.VarChar),
                new SqlParameter("@EndIP", SqlDbType.VarChar)
            };
                sp[0].Value = LimitTime;
                sp[1].Value = IsLimit;
                sp[2].Value = IsIPpart;
                sp[3].Value = BeginIP;
                sp[4].Value = EndIP;
                return SqlHelper.ExecuteSql(sqlStr, sp);
            }
        }
     
    }
}
