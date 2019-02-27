namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using SQLDAL.SQL;
    public class B_ExClassgroup
    {
        public B_ExClassgroup()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        private string PK, strTableName;
        private M_ExClassgroup initMod = new M_ExClassgroup();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ExClassgroup GetSelect(int ID)
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
        private M_ExClassgroup SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_ExClassgroup model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.GroupID.ToString(), initMod.GetFieldAndPara(),  GetParameters(model));
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, "GroupID=" + ID);
        }
        public int GetInsert(M_ExClassgroup exClassgroup)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExClassgroup] ([Regulationame],[Regulation],[Ratednumber],[Actualnumber],[Setuptime],[Endtime],[CourseID],[ShiPrice],[LinPrice],[CourseHour],[Presented],[isCou],[ClassID]) VALUES (@Regulationame,@Regulation,@Ratednumber,@Actualnumber,@Setuptime,@Endtime,@CourseID,@ShiPrice,@LinPrice,@CourseHour,@Presented,@isCou,@ClassID);select @@IDENTITY";
            SqlParameter[] cmdParams =  GetParameters(exClassgroup);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        private static SqlParameter[] GetParameters(M_ExClassgroup ExClassgroupinfo)
        {
            SqlParameter[] parameter = new SqlParameter[15];
            parameter[0] = new SqlParameter("@GroupID", SqlDbType.Int, 4);
            parameter[0].Value = ExClassgroupinfo.GroupID;
            parameter[1] = new SqlParameter("@Regulationame", SqlDbType.NVarChar, 50);
            parameter[1].Value = ExClassgroupinfo.Regulationame;
            parameter[2] = new SqlParameter("@Regulation", SqlDbType.NVarChar, 255);
            parameter[2].Value = ExClassgroupinfo.Regulation;
            parameter[3] = new SqlParameter("@Ratednumber", SqlDbType.Int, 4);
            parameter[3].Value = ExClassgroupinfo.Ratednumber;
            parameter[4] = new SqlParameter("@Actualnumber", SqlDbType.Int, 4);
            parameter[4].Value = ExClassgroupinfo.Actualnumber;
            parameter[5] = new SqlParameter("@Setuptime", SqlDbType.DateTime, 8);
            parameter[5].Value = ExClassgroupinfo.Setuptime;
            parameter[6] = new SqlParameter("@Endtime", SqlDbType.DateTime, 8);
            parameter[6].Value = ExClassgroupinfo.Endtime;
            parameter[7] = new SqlParameter("@CourseID", SqlDbType.Int, 4);
            parameter[7].Value = ExClassgroupinfo.CourseID;
            parameter[8] = new SqlParameter("@ShiPrice", SqlDbType.Money);
            parameter[8].Value = ExClassgroupinfo.ShiPrice;
            parameter[9] = new SqlParameter("@LinPrice", SqlDbType.Money);
            parameter[9].Value = ExClassgroupinfo.LinPrice;
            parameter[10] = new SqlParameter("@CourseHour", SqlDbType.Int, 4);
            parameter[10].Value = ExClassgroupinfo.CourseHour;
            parameter[11] = new SqlParameter("@Presented", SqlDbType.Int, 4);
            parameter[11].Value = ExClassgroupinfo.Presented;
            parameter[12] = new SqlParameter("@isCou", SqlDbType.Int, 4);
            parameter[12].Value = ExClassgroupinfo.isCou;
            parameter[13] = new SqlParameter("@ClassID", SqlDbType.Int, 4);
            parameter[13].Value = ExClassgroupinfo.ClassID;
            parameter[14] = new SqlParameter("@Speaker", SqlDbType.Int, 4);
            parameter[14].Value = ExClassgroupinfo.Speaker;
            return parameter;
        }
        public DataTable Select_CourseID(int CourseID)
        {
            string sqlStr = "SELECT * FROM ZL_ExClassgroup WHERE CourseID=@CourseID";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@CourseID",CourseID)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
