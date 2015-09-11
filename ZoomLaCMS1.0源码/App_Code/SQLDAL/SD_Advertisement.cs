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
    using System.Data.SqlClient;
    using System.Collections;
using System.Collections.Generic;
    using ZoomLa.Common;

    /// <summary>
    /// Advertisement 的摘要说明
    /// </summary>
    public class SD_Advertisement:ID_Advertisement
    {
        public SD_Advertisement()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <param name="StrZoneID"></param>
        /// <returns></returns>
        public bool Advertisement_Add(M_Advertisement Advertisement)
        {
            SqlParameter[] parameter = this.GetParameter(Advertisement);
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_Advertisement_Add", parameter)>0);            
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        public bool Advertisement_CancelPassed(string StrAdId)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAdId", SqlDbType.Int);
            parameter[0].Value = DataConverter.CLng(StrAdId);
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_Advertisement_CancelPassed", parameter)>0);
        }
        /// <summary>
        /// 设为审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        public bool Advertisement_SetPassed(string StrAdId)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAdId", SqlDbType.Int);
            parameter[0].Value = DataConverter.CLng(StrAdId);
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_Advertisement_Passed", parameter)>0);
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <param name="StrZoneID"></param>
        /// <returns></returns>
        public bool Advertisement_Update(M_Advertisement Advertisement)
        {
            SqlParameter[] parameter = this.GetParameter(Advertisement);            
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_Advertisement_Update", parameter)>0);
        }        
        /// <summary>
        /// 读取所有广告
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllAdvertisementList()
        {
            string sql = "select * from ZL_Advertisement";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }       
        /// <summary>
        /// 获取广告的最大ID
        /// </summary>
        /// <returns></returns>
        public int MaxID()
        {
            string sql = "select max(ADID) from ZL_Advertisement";
            return (DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, sql, null)) + 1);
        }
        /// <summary>
        /// 获取某个版位的所有广告ID的数组
        /// </summary>
        /// <param name="ZoneId"></param>
        /// <returns></returns>
        public int[] getAdIds(int ZoneId)
        {
            string sql = "select ADID from zl_Zone_Advertisement where ZoneID=@ZoneID";
            SqlParameter []parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ZoneID", SqlDbType.Int);
            parameter[0].Value = ZoneId;
            int[] arrADID = null;
            DataTable dtADID = SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
            for(int i=0;i<dtADID.Rows.Count;i++)
            {
                arrADID[i] = DataConverter.CLng(dtADID.Rows[i][0]);
            }
            return arrADID;
        }
        /// <summary>
        /// 获取某个广告的详细信息
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public M_Advertisement Advertisement_GetAdvertisementByid(int adid)
        {
            string sql = "select * from ZL_Advertisement where ADID=@ADID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ADID", SqlDbType.Int);
            parameter[0].Value = adid;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (sdr.Read())
                {
                    return this.GetInfoFromReader(sdr);
                }
                else
                {
                    return new M_Advertisement(true);
                }
            } 
        }
        /// <summary>
        /// 删除某个广告
        /// </summary>
        /// <param name="strADID"></param>
        /// <returns></returns>
        public bool Advertisement_Delete(string strADID)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@strAdId", SqlDbType.Int,4);
            parameter[0].Value = DataConverter.CLng(strADID.Trim());
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_Advertisement_remove", parameter)>0);            
        }
        /// <summary>
        /// 获取通过审核的某版位的广告列表
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public IList<M_Advertisement> GetADList(int zoneId)
        {
            string strSql = "SELECT * FROM ZL_Advertisement Where Passed=@Passed AND (ADID in (Select ADID From ZL_Zone_Advertisement Where ZoneID=@ZoneID)) ORDER BY Priority DESC,ADID DESC";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@Passed", SqlDbType.Bit),
                new SqlParameter("@ZoneId", SqlDbType.Int)
            };
            cmdParams[0].Value=1;
            cmdParams[1].Value= zoneId;
            IList<M_Advertisement> list = new List<M_Advertisement>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(this.GetInfoFromReader(reader));
                }
            }
            return list;
        }
        /// <summary>
        /// 复制某个广告
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public bool Advertisement_Copy(int adid)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ad", SqlDbType.Int);
            parameter[0].Value = adid;           
            return (SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "dt_advertisement_copy", parameter)>0);            
        }
        /// <summary>
        /// 获取某个版位内广告的详细信息
        /// </summary>
        /// <param name="zoneid"></param>
        /// <returns></returns>
        public DataTable Advertisement_GetAdvertisementByZoneid(int zoneid)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            string sql = "select * from zl_advertisement where adid in (select adid from zl_zone_advertisement where zoneid=@zoneid)";
            parameter[0] = new SqlParameter("@zoneid", SqlDbType.Int);
            parameter[0].Value = zoneid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
        }
        /// <summary>
        /// 指定条件查询
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public DataTable Advertisement_SelectByCondition(string con)
        {
            string str = "select * from zl_advertisement " +con ;
           return  SqlHelper.ExecuteTable(CommandType.Text, str, null);
        }
        /// <summary>
        /// 插入关联信息
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="ADId"></param>
        /// <returns></returns>
        public bool Add_Zone_Advertisement(int zoneID, int ADId)
        {          
            string strinsert = "insert ZL_Zone_Advertisement values(" + zoneID.ToString() + "," + ADId.ToString() + ")";
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, strinsert, null)) > 0;
        }
        /// <summary>
        /// 获取某广告所属的所有版位ID组成的字符串
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public string GetZoneIDsByAdvID(int ADID)
        {
            string strSql = "Select ZoneID from ZL_Zone_Advertisement where ADID=@ADID Order by ZoneID ASC";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@ADID",SqlDbType.Int)
            };
            sp[0].Value = ADID;
            string re = "";
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                while (rdr.Read())
                {
                    if (string.IsNullOrEmpty(re))
                        re = rdr[0].ToString();
                    else
                        re = re + "," + rdr[0].ToString();
                }
            }
            return re;
        }
        /// <summary>
        /// 是否存在指定版位ID和广告ID的关联信息
        /// </summary>
        /// <param name="ZoneID"></param>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public bool IsExistZoneAdv(int ZoneID, int ADID)
        {
            string strSql = "select Count(*) from ZL_Zone_Advertisement where ZoneID=@ZoneID and ADID=@ADID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@ZoneID",SqlDbType.Int),
                new SqlParameter("@ADID",SqlDbType.Int)
            };
            sp[0].Value = ZoneID;
            sp[1].Value = ADID;
            return (DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp)) > 0);
        }
        private SqlParameter[] GetParameter(M_Advertisement adv)
        {
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@ADID", SqlDbType.Int),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@ADType",SqlDbType.Int),
                new SqlParameter("@ADName",SqlDbType.NVarChar,100),
                new SqlParameter("@ImgUrl",SqlDbType.NVarChar,255),
                new SqlParameter("@ImgWidth",SqlDbType.Int),
                new SqlParameter("@ImgHeight",SqlDbType.Int),
                new SqlParameter("@FlashWmode",SqlDbType.Int),
                new SqlParameter("@ADIntro",SqlDbType.NText),
                new SqlParameter("@LinkUrl",SqlDbType.NVarChar,255),
                new SqlParameter("@LinkTarget",SqlDbType.Int),
                new SqlParameter("@LinkAlt",SqlDbType.NVarChar,255),
                new SqlParameter("@Priority",SqlDbType.Int),
                new SqlParameter("@Setting",SqlDbType.NText),
                new SqlParameter("@CountView",SqlDbType.Bit),
                new SqlParameter("@Views",SqlDbType.Int),
                new SqlParameter("@CountClick",SqlDbType.Bit),
                new SqlParameter("@Clicks",SqlDbType.Int),
                new SqlParameter("@Passed",SqlDbType.Int),
                new SqlParameter("@OverdueDate",SqlDbType.DateTime)
            };

            sp[0].Value = adv.AdId;
            sp[1].Value = adv.UserId;
            sp[2].Value = adv.AdType;
            sp[3].Value = adv.AdName;
            sp[4].Value = adv.ImgUrl;
            sp[5].Value = adv.ImgWidth;
            sp[6].Value = adv.ImgHeight;
            sp[7].Value = adv.FlashWmode;
            sp[8].Value = adv.ADIntro;
            sp[9].Value = adv.LinkUrl;
            sp[10].Value = adv.LinkTarget;
            sp[11].Value = adv.LinkAlt;
            sp[12].Value = adv.Priority;
            sp[13].Value = adv.Setting;
            sp[14].Value = adv.CountView;
            sp[15].Value = adv.Views;
            sp[16].Value = adv.CountClick;
            sp[17].Value = adv.Clicks;
            sp[18].Value = adv.Passed;
            sp[19].Value = adv.OverdueDate;
            return sp;
        }
        private M_Advertisement GetInfoFromReader(SqlDataReader rdr)
        {
            M_Advertisement info = new M_Advertisement();
            info.AdId = DataConverter.CLng(rdr["ADID"]);
            info.UserId = DataConverter.CLng(rdr["UserID"]);
            info.AdType = DataConverter.CLng(rdr["ADType"]);
            info.AdName = rdr["ADName"].ToString();
            info.ImgUrl = rdr["ImgUrl"].ToString();
            info.ImgWidth = DataConverter.CLng(rdr["ImgWidth"]);
            info.ImgHeight = DataConverter.CLng(rdr["ImgHeight"]);
            info.FlashWmode = DataConverter.CLng(rdr["FlashWmode"]);
            info.ADIntro = rdr["ADIntro"].ToString();
            info.LinkUrl = rdr["LinkUrl"].ToString();
            info.LinkTarget = DataConverter.CLng(rdr["LinkTarget"]);
            info.LinkAlt = rdr["LinkAlt"].ToString();
            info.Priority = DataConverter.CLng(rdr["Priority"]);
            info.Setting = rdr["Setting"].ToString();
            info.CountView = DataConverter.CBool(rdr["CountView"].ToString());
            info.Views = DataConverter.CLng(rdr["Views"]);
            info.CountClick = DataConverter.CBool(rdr["CountClick"].ToString());
            info.Clicks = DataConverter.CLng(rdr["Clicks"]);
            info.Passed = DataConverter.CBool(rdr["Passed"].ToString());
            info.OverdueDate = DataConverter.CDate(rdr["OverdueDate"]);
            return info;
        }
    }
}