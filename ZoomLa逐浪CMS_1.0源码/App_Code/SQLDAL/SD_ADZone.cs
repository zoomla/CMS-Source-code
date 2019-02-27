namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Data.SqlClient;

    /// <summary>
    /// SD_ADZone 的摘要说明
    /// </summary>
    public class SD_ADZone:ID_Adzone
    {
        public SD_ADZone()
        {
            // 
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region ID_Adzone 成员
        /// <summary>
        /// 设置版位激活
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        bool ID_Adzone.ADZone_Active(int ZoneId)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameter[0].Value = ZoneId;
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_ADZone_Active", parameter)>0);
        }
        /// <summary>
        /// 批量激活
        /// </summary>
        /// <param name="ZoneIds"></param>
        /// <returns></returns>
        bool ID_Adzone.BatchActive(string ZoneIds)
        {
            string strSql = "Update ZL_AdZone Set Active=1 Where ZoneID in (" + ZoneIds + ")";
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null) > 0);
        }
        /// <summary>
        /// 添加版位
        /// </summary>
        /// <param name="adZone"></param>
        /// <returns></returns>
        bool ID_Adzone.ADZone_Add(M_Adzone adZone)
        {
            SqlParameter[] parameter = null;
            this.GetAdzoneParameter(adZone,ref parameter);
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_ADZone_Add", parameter)>0);            
        }
        /// <summary>
        /// 查询某个版位的广告列表
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        DataTable ID_Adzone.ADZone_GetExportList(int ZoneId)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ZoneID", SqlDbType.Int , 4);
            parameter[0].Value = ZoneId;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, "dt_ADZone_GetExportList", parameter);
        }
        /// <summary>
        /// 统计某个版位的广告数
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        int ID_Adzone.ADZone_Count(int ZoneId)
        {
            string str = "select count(*) from ZL_Zone_Advertisement where ZoneID=" + ZoneId.ToString();
            return SqlHelper.ObjectToInt32( SqlHelper.ExecuteScalar(CommandType.Text, str, null));
        }
        /// <summary>
        /// 修改版位
        /// </summary>
        /// <param name="adZone"></param>
        /// <returns></returns>
        bool ID_Adzone.ADZone_Update(Model.M_Adzone adZone)
        {
            SqlParameter[] parameter = null;
            this.GetAdzoneParameter(adZone, ref parameter);
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_ADZone_Upadte", parameter)>0);
        }  
        /// <summary>
        /// 暂停版位活动
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        bool ID_Adzone.ADZone_Pause(int ZoneId)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameter[0].Value = ZoneId;
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_ADZone_Pause", parameter)>0);            
        }
        
        /// <summary>
        /// 获取所有版位
        /// </summary>
        /// <returns></returns>
        DataTable ID_Adzone.ADZone_GetAll()
        {
            string sql = "select * from ZL_AdZone";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 获取某个广告所属第一个版位
        /// </summary>
        /// <param name="adverseId"></param>
        /// <returns></returns>
        M_Adzone ID_Adzone.getAdzone(int advId)
        {
            string sql = "select * from ZL_AdZone where ZoneID=(select top 1 ZoneID  from  ZL_Zone_Advertisement where ADID=@ADID order by ZoneID asc)";
            SqlParameter []parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ADId", SqlDbType.Int);
            parameter[0].Value=advId;
            using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (drd.Read())
                {
                    return GetInfoFromReader(drd);
                }
                else
                {
                    return new M_Adzone(true);
                }
            }
        }
        /// <summary>
        /// 获取广告所属版位的ID数组
        /// </summary>
        /// <param name="adverseId"></param>
        /// <returns></returns>
        int[] ID_Adzone.getAdzoneS(int advId)
        {
            string sql = "select *  from ZL_Zone_Advertisement where ADID=@ADID ";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ADId", SqlDbType.Int);
            parameter[0].Value = advId;
            
            DataTable dtADID = SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
            int[] ADID = new int[dtADID.Rows.Count];
            for (int i = 0; i < dtADID.Rows.Count; i++)
            { 
              ADID[i]=DataConverter.CLng(dtADID.Rows[i]["ZoneID"]);
            }
            return ADID;
        }
        /// <summary>
        /// 获取最大的版位ID
        /// </summary>
        /// <returns></returns>
        int ID_Adzone.ADZone_MaxID()
        {
            string sql = "select max(zoneid) from ZL_AdZone";
            int i;
            try
            {
                i =DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, sql, null))+1;
            }
            catch
            {
                i = 0;
            }
            return i;
        }
        /// <summary>
        /// 删除版位
        /// </summary>
        /// <param name="strZoneId"></param>
        /// <returns></returns>
        bool ID_Adzone.ADZone_Remove(int ZoneId)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameter[0].Value =ZoneId;
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_ADZone_Remove", parameter)>0);
        }

        /// <summary>
        /// 删除某个版位里的广告
        /// </summary>
        /// <param name="adzoneid"></param>
        void ID_Adzone.ADZone_Clear(int adzoneid)
        {
            string sql = "delete from ZL_Zone_Advertisement where ZoneID=@zoneid";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@zoneid", SqlDbType.Int);
            parameter[0].Value = adzoneid;
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }
        /// <summary>
        /// 复制版位
        /// </summary>
        /// <param name="adzoneid"></param>
        /// <returns></returns>
        bool ID_Adzone.ADZone_Copy(int adzoneid)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@zone", SqlDbType.Int);
            parameter[0].Value = adzoneid;
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_adzone_copy", parameter)>0);            
        }
        /// <summary>
        /// 获取指定版位的详细信息
        /// </summary>
        /// <param name="adzoneid"></param>
        /// <returns></returns>
        M_Adzone ID_Adzone.getAdzoneByZoneId(int adzoneid)
        {
            string sql = "select * from ZL_AdZone where ZoneId=@zoneid";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@zoneid", SqlDbType.Int);
            parameter[0].Value = adzoneid;
            using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (drd.Read())
                {
                    return GetInfoFromReader(drd);
                }
                else
                {
                    return new M_Adzone(true);
                }
            }
        }        
        /// <summary>
        /// 条件查询指定的版位
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        DataTable ID_Adzone.ADZone_ByCondition(string con)
        {
            string str = "select * from ZL_AdZone "+con ;
            return SqlHelper.ExecuteTable(CommandType.Text, str, null);        
        }
        /// <summary>
        /// 批量暂停
        /// </summary>
        /// <param name="ZoneIDs"></param>
        /// <returns></returns>
        bool ID_Adzone.BatchPause(string ZoneIDs)
        {
            string strSql = "Update ZL_AdZone Set Active=0 Where ZoneID in (" + ZoneIDs + ")";
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null) > 0);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ZoneIds"></param>
        /// <returns></returns>
        bool ID_Adzone.BatchRemove(string ZoneIds)
        {
            string strSql = "Delete from ZL_AdZone Where ZoneID in (" + ZoneIds + ")";
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null) > 0);
        }        
        /// <summary>
        /// 删除某个广告所在的版位
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        bool ID_Adzone.Delete_ADZone_Ad(string ADID)
        {
            string str = "delete from ZL_Zone_Advertisement where ADID=" + ADID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, str, null) > 0;
        }

        #endregion
        private void GetAdzoneParameter(M_Adzone adzone, ref SqlParameter[] parameter)
        {
            parameter = new SqlParameter[] {
                new SqlParameter("@ZoneId", SqlDbType.Int),
                new SqlParameter("@ZoneName", SqlDbType.NVarChar, 100),
                new SqlParameter("@ZoneJSName", SqlDbType.NVarChar, 100),
                new SqlParameter("@ZoneIntro", SqlDbType.NVarChar, 255),
                new SqlParameter("@ZoneType", SqlDbType.Int ,4),
                new SqlParameter("@DefaultSetting", SqlDbType.Bit),
                new SqlParameter("@ZoneSetting", SqlDbType.NVarChar, 255),
                new SqlParameter("@ZoneWidth", SqlDbType.Int),
                new SqlParameter("@ZoneHeight", SqlDbType.Int),
                new SqlParameter("@Active", SqlDbType.Bit),
                new SqlParameter("@ShowType", SqlDbType.Int),
                new SqlParameter("@UpdateTime", SqlDbType.DateTime)
            };
            parameter[0].Value = adzone.ZoneID;
            parameter[1].Value = adzone.ZoneName;
            parameter[2].Value = adzone.ZoneJSName;
            parameter[3].Value = adzone.ZoneIntro;
            parameter[4].Value = adzone.ZoneType;
            parameter[5].Value = adzone.DefaultSetting;
            parameter[6].Value = adzone.ZoneSetting;
            parameter[7].Value = adzone.ZoneWidth;
            parameter[8].Value = adzone.ZoneHeight;
            parameter[9].Value = adzone.Active;
            parameter[10].Value = adzone.ShowType;
            parameter[11].Value = adzone.UpdateTime;
        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private M_Adzone GetInfoFromReader(SqlDataReader rdr)
        {
            M_Adzone info = new M_Adzone();
            info.ZoneID = DataConverter.CLng(rdr["ZoneID"]);
            info.ZoneName = rdr["ZoneName"].ToString();
            info.ZoneJSName = rdr["ZoneJSName"].ToString();
            info.ZoneIntro = rdr["ZoneIntro"].ToString();
            info.ZoneType = DataConverter.CLng(rdr["ZoneType"]);
            info.DefaultSetting = DataConverter.CBool(rdr["DefaultSetting"].ToString());
            info.ZoneSetting = rdr["ZoneSetting"].ToString();
            info.ZoneHeight = DataConverter.CLng(rdr["ZoneHeight"]);
            info.ZoneWidth = DataConverter.CLng(rdr["ZoneWidth"]);
            info.Active = DataConverter.CBool(rdr["Active"].ToString());
            info.ShowType = DataConverter.CLng(rdr["ShowType"]);
            info.UpdateTime = DataConverter.CDate(rdr["UpdateTime"]);
            return info;
        }
    }
}