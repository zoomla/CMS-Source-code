namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Collections;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;
    using System.Web;
    using System.Data.SqlClient;
    using System.Data.Common;
    using SQLDAL.SQL;
    /// <summary>
    /// B_Advertisement 的摘要说明
    /// </summary>
    public class B_Advertisement
    {
        public static string strTableName = "ZL_Advertisement",PK = "ADID";
        private M_Advertisement initMod = new M_Advertisement();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Advertisement SelReturnModel(int ID)
        {
            using (DbDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_Advertisement model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ADID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public static bool Advertisement_Delete(int ID)
        {
            return Sql.Del(strTableName, "ADID=" + ID);
        }
        public int insert(M_Advertisement model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public static bool Advertisement_Add(M_Advertisement Advertisement)
        {
            return Sql.insert(strTableName, Advertisement.GetParameters(), BLLCommon.GetParas(Advertisement), BLLCommon.GetFields(Advertisement)) > 0;
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        public static bool Advertisement_CancelPassed(string StrAdId)
        {
            string sqlStr = "Update " + strTableName + " Set Passed=0 Where ADID=@StrAdId";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("StrAdId", StrAdId) };
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        /// <summary>
        /// 设为审核
        /// </summary>
        /// <param name="StrAdId"></param>
        /// <returns></returns>
        public static bool Advertisement_SetPassed(string StrAdId)
        {
            string sqlStr = "Update " + strTableName + " Set Passed=1 Where ADID=@StrAdId";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("StrAdId", StrAdId) };
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="Advertisement"></param>
        /// <returns></returns>
        public static bool Advertisement_Update(M_Advertisement Advertisement)
        {
            return Sql.UpdateByIDs(strTableName, PK, Advertisement.ADID.ToString(), BLLCommon.GetFieldAndPara(Advertisement), Advertisement.GetParameters());
        }
        /// <summary>
        /// 获取通过审核的某版位的广告列表
        /// </summary>
        public DataTable GetTableADList(int zoneId)
        {
            string strSql = "SELECT * FROM ZL_Advertisement Where (ADID in (Select ADID From ZL_Zone_Advertisement Where ZoneID=@ZoneID)) ORDER BY Priority DESC,ADID DESC";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@Passed", SqlDbType.Bit),
                new SqlParameter("@ZoneId", SqlDbType.Int)
            };
            cmdParams[0].Value = 1;
            cmdParams[1].Value = zoneId;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
        }
        /// <summary>
        /// 获取通过审核的某版位的广告列表
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public static IList<M_Advertisement> GetADList(int zoneId)
        {
            string strSql = "SELECT * FROM ZL_Advertisement Where Passed=@Passed AND (ADID in (Select ADID From ZL_Zone_Advertisement Where ZoneID=@ZoneID)) ORDER BY Priority DESC,ADID DESC";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@Passed", SqlDbType.Bit),
                new SqlParameter("@ZoneId", SqlDbType.Int)
            };
            cmdParams[0].Value = 1;
            cmdParams[1].Value = zoneId;
            IList<M_Advertisement> list = new List<M_Advertisement>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(GetInfoFromReader(reader));
                    }
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            return list;
        }

        /// <summary>
        /// 读取所有广告
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAdvertisementList()
        {
            string sql = "select * from ZL_Advertisement order by ADID desc ";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 获取广告的最大ID
        /// </summary>
        /// <returns></returns>
        public static int MaxID()
        {
            string sql = "select max(ADID) from ZL_Advertisement";
            return (DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, sql, null)) + 1);
        }
        /// <summary>
        /// 获取某个版位的所有广告ID的数组
        /// </summary>
        /// <param name="adverseId"></param>
        /// <returns></returns>
        public static int[] getAdIds(int adverseId)
        {
            string sql = "select ADID from zl_Zone_Advertisement where ZoneID=@ZoneID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ZoneID", SqlDbType.Int);
            parameter[0].Value = adverseId;
            int[] arrADID = null;
            DataTable dtADID = SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
            for (int i = 0; i < dtADID.Rows.Count; i++)
            {
                arrADID[i] = DataConverter.CLng(dtADID.Rows[i][0]);
            }
            return arrADID;
        }
        /// <summary>
        /// 删除某个广告
        /// </summary>
        /// <param name="strADID"></param>
        /// <returns></returns>

        /// <summary>
        /// 获取某个广告的详细信息
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static M_Advertisement Advertisement_GetAdvertisementByid(int adid)
        {
            string sql = "select * from ZL_Advertisement where ADID=@ADID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@ADID", SqlDbType.Int);
            parameter[0].Value = adid;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (sdr.Read())
                {
                    return GetInfoFromReader(sdr);
                }
                else
                {
                    return new M_Advertisement();
                }
            }
        }
        /// <summary>
        /// 复制某个广告
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static bool Advertisement_Copy(int adid)
        {
            M_Advertisement model = new M_Advertisement();
            B_Advertisement bll = new B_Advertisement();
            model = bll.SelReturnModel(adid);
            model.ADName = "复制" + model.ADName;
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        /// <summary>
        /// 获取某个版位内广告的列表
        /// </summary>
        /// <param name="zoneid"></param>
        /// <returns></returns>
        public static DataTable Advertisement_GetAdvertisementByZoneid(int zoneid)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            string sql = "select * from zl_advertisement where adid in (select adid from zl_zone_advertisement where zoneid=@zoneid)";
            parameter[0] = new SqlParameter("@zoneid", SqlDbType.Int);
            parameter[0].Value = zoneid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// 插入关联信息
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="ADId"></param>
        /// <returns></returns>
        public static bool Add_Zone_Advertisement(int zoneID, int ADId)
        {
            string strinsert = "insert ZL_Zone_Advertisement values(" + zoneID.ToString() + "," + ADId.ToString() + ")";
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, strinsert, null)) > 0;
        }
        /// <summary>
        /// 获取某广告所属的所有版位ID组成的字符串
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public static string GetZoneIDByAd(int ADID)
        {
            return GetZoneIDsByAdvID(ADID);


        }
        public static string GetZoneIDsByAdvID(int ADID)
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
                rdr.Close();
                rdr.Dispose();
            }
            return re;
        }
        /// <summary>
        /// 是否存在指定版位ID和广告ID的关联信息
        /// </summary>
        /// <param name="ZoneID"></param>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public static bool IsExistZoneAdv(int ZoneID, int ADID)
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
        /// <summary>
        ///根据广告类型，查找广告
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetAdvByType(int type)
        {
            DataTable dt = new DataTable();
            string cmd = @"select * from ZL_Advertisement where ADType=@type order by Priority DESC, ADID desc";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@type", SqlDbType.Int) };
            parameter[0].Value = type;
            dt = SqlHelper.ExecuteTable(CommandType.Text, cmd, parameter);
            return dt;
        }
        public DataTable GetAdvByTypeAndZoneId(int type, int zoneId)
        {
            string str = "SELECT * FROM ZL_Advertisement Where (ADID in (Select ADID From ZL_Zone_Advertisement Where ZoneID=" + zoneId + ")) And ADType=" + type + " ORDER BY Priority DESC,ADID DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, str);
        }


        public static string GetAdContent(M_Advertisement advertisementInfo)
        {
            StringBuilder builder2;
            StringBuilder builder = new StringBuilder();
            switch (advertisementInfo.ADType)
            {
                case 1:
                    {
                        builder2 = new StringBuilder();
                        builder2.Append("<img src=\"");
                        string imgUrl = advertisementInfo.ImgUrl;
                        if (((!imgUrl.StartsWith("/") && !imgUrl.StartsWith("~/")) && !imgUrl.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) && !imgUrl.StartsWith("https", StringComparison.CurrentCultureIgnoreCase))
                        {
                            builder2.Append(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath));
                            //builder2.Append(VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir));
                            builder2.Append(imgUrl);
                            break;
                        }
                        builder2.Append(imgUrl);
                        break;
                    }
                case 2:
                    builder.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\"");
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder.Append(" width=\"");
                        builder.Append(advertisementInfo.ImgWidth);
                        builder.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder.Append("  height=\"");
                        builder.Append(advertisementInfo.ImgHeight);
                        builder.Append("\"");
                    }
                    builder.Append("><param name=\"movie\" value=\"");
                    builder.Append(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath));
                    builder.Append(advertisementInfo.ImgUrl);
                    builder.Append("\">");
                    if (advertisementInfo.FlashWmode == 1)
                    {
                        builder.Append("<param name=\"wmode\" value=\"transparent\">");
                    }
                    builder.Append("<param name=\"quality\" value=\"autohigh\">");
                    builder.Append("<embed src=\"");
                    builder.Append(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath));
                    //builder.Append(VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir));
                    builder.Append(advertisementInfo.ImgUrl);
                    builder.Append("\" quality=\"autohigh\"  pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\"");
                    if (advertisementInfo.FlashWmode == 1)
                    {
                        builder.Append(" wmode=\"transparent\" ");
                    }
                    if (advertisementInfo.ImgWidth > 0)
                    {
                        builder.Append(" width=\"");
                        builder.Append(advertisementInfo.ImgWidth);
                        builder.Append("\"");
                    }
                    if (advertisementInfo.ImgHeight > 0)
                    {
                        builder.Append("  height=\"");
                        builder.Append(advertisementInfo.ImgHeight);
                        builder.Append("\"");
                    }
                    builder.Append("></embed></object>");
                    goto Label_04B5;

                case 3:
                    builder.Append(advertisementInfo.ADIntro);
                    goto Label_04B5;

                case 4:
                    builder.Append(advertisementInfo.ADIntro);
                    goto Label_04B5;

                case 5:
                    builder.Append("<iframe id=\"AD_");
                    builder.Append(advertisementInfo.ADID);
                    builder.Append("\" marginwidth=0 marginheight=0 hspace=0 vspace=0 frameborder=0 scrolling=no width=100% height=100% src=\"");
                    builder.Append(advertisementInfo.ADIntro);
                    builder.Append("\">AD</iframe>");
                    goto Label_04B5;

                default:
                    goto Label_04B5;
            }
            builder2.Append("\"");
            if (advertisementInfo.ImgWidth > 0)
            {
                builder2.Append(" width=\"");
                builder2.Append(advertisementInfo.ImgWidth);
                builder2.Append("\"");
            }
            if (advertisementInfo.ImgHeight > 0)
            {
                builder2.Append("  height=\"");
                builder2.Append(advertisementInfo.ImgHeight);
                builder2.Append("\"");
            }
            builder2.Append(" border=\"0\"></img>");
            if (!string.IsNullOrEmpty(advertisementInfo.LinkUrl))
            {
                builder.Append("<a href=\"");
                builder.Append(advertisementInfo.LinkUrl);
                builder.Append("\"");
                if (advertisementInfo.LinkTarget == 0)
                {
                    builder.Append(" target=\"_self\"");
                }
                else
                {
                    builder.Append(" target=\"_blank\"");
                }
                builder.Append(" title=\"");
                builder.Append(advertisementInfo.LinkAlt);
                builder.Append("\"");
                builder.Append(">");
                builder.Append(builder2.ToString());
                builder.Append("</a>");
            }
            else
            {
                builder = builder2;
            }
        Label_04B5:
            return builder.ToString();
        }

        /// <summary>
        ///   根据用户id查询剩余天数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static M_Advertisement GetInfoOverdueDate(int userid)
        {
            string sql = "select * from ZL_Advertisement where userId=@UserId ";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@UserId", SqlDbType.Int);
            parameter[0].Value = userid;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (sdr.Read())
                {
                    return GetInfoFromReader(sdr);
                }
                else
                {
                    return new M_Advertisement();
                }
            }

        }

        /// <summary>
        /// 读取会员已过期所有广告
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAdvertisementDateList(string size)
        {
            string sql = "select * from dbo.ZL_Advertisement where OverdueDate @size" + DateTime.Now + "'" + " order by ADID desc ";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("size",size) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        private static M_Advertisement GetInfoFromReader(SqlDataReader rdr)
        {
            M_Advertisement info = new M_Advertisement();
            info.ADID = DataConverter.CLng(rdr["ADID"].ToString());
            info.UserID = DataConverter.CLng(rdr["UserID"].ToString());
            info.ADType = DataConverter.CLng(rdr["ADType"].ToString());
            info.ADName = rdr["ADName"].ToString();
            info.ImgUrl = rdr["ImgUrl"].ToString();
            info.ImgWidth = DataConverter.CLng(rdr["ImgWidth"].ToString());
            info.ImgHeight = DataConverter.CLng(rdr["ImgHeight"].ToString());
            info.FlashWmode = DataConverter.CLng(rdr["FlashWmode"].ToString());
            info.ADIntro = rdr["ADIntro"].ToString();
            info.LinkUrl = rdr["LinkUrl"].ToString();
            info.LinkTarget = DataConverter.CLng(rdr["LinkTarget"].ToString());
            info.LinkAlt = rdr["LinkAlt"].ToString();
            info.Priority = DataConverter.CLng(rdr["Priority"].ToString());
            info.Setting = rdr["Setting"].ToString();
            info.CountView = DataConverter.CBool(rdr["CountView"].ToString());
            info.Views = DataConverter.CLng(rdr["Views"].ToString());
            info.CountClick = DataConverter.CBool(rdr["CountClick"].ToString());
            info.Clicks = DataConverter.CLng(rdr["Clicks"].ToString());
            info.Passed = DataConverter.CBool(rdr["Passed"].ToString());
            info.OverdueDate = DataConverter.CDate(rdr["OverdueDate"].ToString());
            info.ImgUrl1 = rdr["ImgUrl1"].ToString();
            info.ImgHeight1 = DataConverter.CLng(rdr["ImgHeight1"].ToString());
            info.ImgWidth1 = DataConverter.CLng(rdr["ImgWidth1"].ToString());
            info.LinkUrl1 = rdr["LinkUrl1"].ToString();
            info.LinkTarget1 = DataConverter.CLng(rdr["LinkTarget1"].ToString());
            info.LinkAlt1 = rdr["LinkAlt1"].ToString();
            info.ADIntro1 = rdr["ADIntro1"].ToString();
            info.FlashWmode1 = DataConverter.CLng(rdr["FlashWmode1"].ToString());
            return info;
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