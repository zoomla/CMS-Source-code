using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using ZoomLa.SQLDAL.SQL;
/// <summary>
/// B_ADZone 的摘要说明
/// </summary>
public class B_ADZone
{
    public string TbName, PK;
    public M_Adzone initMod = new M_Adzone();
    public B_ADZone()
    {
        TbName = initMod.TbName;
        PK = initMod.PK;
    }
    public DataTable Sel(int ID)
    {
        return Sql.Sel(TbName, PK, ID);
    }
    public M_Adzone SelReturnModel(int ID)
    {
        using (DbDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
    private M_Adzone SelReturnModel(string strWhere)
    {
        using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
        return Sql.Sel(TbName);
    }
    public PageSetting SelPage(int cpage, int psize)
    {
        string where = "1=1 ";
        PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
        DBCenter.SelPage(setting);
        return setting;
    }
    public bool UpdateByID(M_Adzone model)
    {
        return Sql.UpdateByIDs(TbName, PK, model.ZoneID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
    }
    public bool Del(int ID)
    {
        return Sql.Del(TbName, ID);
    }
    public int insert(M_Adzone model)
    {
        return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
    }
    /// <summary>
    /// 激活版位
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static bool ADZone_Active(int ID)
    {
         M_Adzone initMod = new M_Adzone();
         string sql = "Update " + initMod.TbName + " Set Active=1 Where ZoneID=" + ID;
         return SqlHelper.ExecuteSql(sql);
    }
    /// <summary>
    /// 批量激活
    /// </summary>
    /// <param name="ZoneIds"></param>
    /// <returns></returns>
    public static bool BatchActive(string ids)
    {
        SafeSC.CheckIDSEx(ids);
        return (SqlHelper.ExecuteNonQuery(CommandType.Text, "Update ZL_AdZone Set Active=1 Where ZoneID in (" + ids + ")") > 0);
    }
    /// <summary>
    /// 添加版位
    /// </summary>
    /// <param name="adZone"></param>
    /// <returns></returns>
    public static bool ADZone_Add(M_Adzone adZone)
    {
        if (!ADZone_Adds(adZone))
        {
            return false;
        }
        else
        {
            CreateJS(adZone.ZoneID.ToString());
        }
        return true;
    }

    /// <summary>
    /// 添加版位
    /// </summary>
    /// <param name="adZone"></param>
    /// <returns></returns>
    static bool ADZone_Adds(M_Adzone model)
    {
        Sql.insert(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        return true;
    }
    /// <summary>
    /// 创建JS文件
    /// </summary>
    /// <param name="id"></param>
    public static void CreateJS(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            string[] strArray = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            B_ADZoneJs adjs = new B_ADZoneJs();
            for (int i = 0; i < strArray.Length; i++)
            {
                M_Adzone adZoneById = getAdzoneByZoneId(DataConverter.CLng(strArray[i]));
                //if (adZoneById.Active)
                //{
                IList<M_Advertisement> aDList = B_Advertisement.GetADList(adZoneById.ZoneID);
                if (aDList.Count >= 0)
                {
                    adjs.CreateJS(adZoneById, aDList);
                }
                //}
            }
        }
    }
    /// <summary>
    /// 查询某个版位的广告列表
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static DataTable ADZone_GetExportList(string ID)
    {
        string sql = "select * from dbo.ZL_Advertisement where ADID in(select ADID from dbo.ZL_Zone_Advertisement where ZoneID=" + Convert.ToInt32(ID) + " and dbo.ZL_Zone_Advertisement.ADID=dbo.ZL_Advertisement.ADID )";
        return SqlHelper.ExecuteTable(CommandType.Text,sql);
    }
    /// <summary>
    /// 修改版位
    /// </summary>
    /// <param name="adZone"></param>
    /// <returns></returns>
    public static bool ADZone_Update(M_Adzone adZone)
    {
        if (!ADZone_Updates(adZone))
        {
            return false;
        }
        else
        {
            CreateJS(adZone.ZoneID.ToString());
        }
        return true;
    }

    /// <summary>
    /// 修改版位
    /// </summary>
    /// <param name="adZone"></param>
    /// <returns></returns>
    public static bool ADZone_Updates(M_Adzone model)
    {
        return Sql.UpdateByIDs(model.TbName, model.PK, model.ZoneID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
    }
    public static bool Chart_Update(ChartCall Chart)
    {
        return false;
    }
    private static void GetChartParameter(ChartCall chart, ref SqlParameter[] parameter)
    {
        parameter = new SqlParameter[] {
                new SqlParameter("@ChartID", SqlDbType.Int,4),
                new SqlParameter("@ChartTitle", SqlDbType.NVarChar, 255),
                new SqlParameter("@ChartType", SqlDbType.NVarChar, 255),
                new SqlParameter("@ChartUnit", SqlDbType.NVarChar, 255),
                new SqlParameter("@ChartWidth", SqlDbType.Int ,4),
                new SqlParameter("@ChartHeight", SqlDbType.Int,4),
                new SqlParameter("@CharData",SqlDbType.NText)
            };
        parameter[0].Value = chart.ChartID;
        parameter[1].Value = chart.ChartTitle;
        parameter[2].Value = chart.ChartType;
        parameter[3].Value = chart.ChartUnit;
        parameter[4].Value = chart.ChartWidth;
        parameter[5].Value = chart.ChartHeight;
        parameter[6].Value = chart.CharData;
    }
    public static bool ADZone_Pause(string ID)
    {
        string sql = "update dbo.ZL_AdZone set Active=0 where ZoneID=" +  Convert.ToInt32(ID);
        return SqlHelper.ExecuteSql(sql);
    }
    /// <summary>
    /// 批量暂停
    /// </summary>
    /// <param name="ZoneIDs"></param>
    /// <returns></returns>
    public static bool BatchPause(string ZoneIDs)
    {
        SafeSC.CheckIDSEx(ZoneIDs);
        return (SqlHelper.ExecuteNonQuery(CommandType.Text, "Update ZL_AdZone Set Active=0 Where ZoneID in (" + ZoneIDs + ")") > 0);
    }
    /// <summary>
    /// 获取所有版位
    /// </summary>
    /// <returns></returns>
    public static DataTable ADZone_GetAll()
    {
        string sql = "select * from ZL_AdZone order by ZoneID desc";
        return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
    }
    /// <summary>
    /// 获取某个广告所属第一个版位
    /// </summary>
    /// <param name="adverseId"></param>
    /// <returns></returns>
    public static M_Adzone getAdzones(int adverseId)
    {
        M_Adzone model=new M_Adzone();
        string sql = "select * from ZL_AdZone where ZoneID=(select top 1 ZoneID  from  ZL_Zone_Advertisement where ADID=@ADID order by ZoneID desc)";
        SqlParameter[] parameter = new SqlParameter[1];
        parameter[0] = new SqlParameter("@ADId", SqlDbType.Int);
        parameter[0].Value = adverseId;
        using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
        {
            if (drd.Read())
            {
                return model.GetModelFromReader(drd);
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
    public static int[] getAdzoneS(int adverseId)
    {
        string sql = "select *  from ZL_Zone_Advertisement where ADID=@ADID ";
        SqlParameter[] parameter = new SqlParameter[1];
        parameter[0] = new SqlParameter("@ADId", SqlDbType.Int);
        parameter[0].Value = adverseId;

        DataTable dtADID = SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
        int[] ADID = new int[dtADID.Rows.Count];
        for (int i = 0; i < dtADID.Rows.Count; i++)
        {
            ADID[i] = DataConverter.CLng(dtADID.Rows[i]["ZoneID"]);
        }
        return ADID;
    }
    /// <summary>
    /// 获取最大的版位ID
    /// </summary>
    /// <returns></returns>
    public static int ADZone_MaxID()
    {
        string sql = "select max(zoneid) from ZL_AdZone";
        int i;
        try
        {
            i = DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, sql, null)) + 1;
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
    public static bool ADZone_Remove(string ID)
    {
        M_Adzone model = new M_Adzone();
        return Sql.Del(model.TbName, model.PK + "=" +  Convert.ToInt32(ID));
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ZoneIds"></param>
    /// <returns></returns>
    public static bool BatchRemove(string ZoneIds)
    {
        SafeSC.CheckIDSEx(ZoneIds);
        return (SqlHelper.ExecuteNonQuery(CommandType.Text, "Delete from ZL_AdZone Where ZoneID in (" + ZoneIds + ")") > 0);
    }
    /// <summary>
    /// 清空版位
    /// </summary>
    /// <param name="adzoneid"></param>
    public static bool ADZone_Clear(int adzoneid)
    {
        string sql = "delete from ZL_Zone_Advertisement where ZoneID=@zoneid";
        SqlParameter[] parameter = new SqlParameter[1];
        parameter[0] = new SqlParameter("@zoneid", SqlDbType.Int);
        parameter[0].Value = adzoneid;
        return SqlHelper.ExecuteSql(sql, parameter);
    }
    /// <summary>
    /// 复制版位
    /// </summary>
    /// <param name="adzoneid"></param>
    /// <returns></returns>
    public static int ADZone_Copy(int adzoneid)
    {
        SqlParameter[] parameter = new SqlParameter[1];
        parameter[0] = new SqlParameter("@zone", SqlDbType.Int);
        parameter[0].Value = adzoneid;
        return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "PR_adzone_copy", parameter));
    }
    /// <summary>
    /// 获取指定版位的详细信息
    /// </summary>
    /// <param name="adzoneid"></param>
    /// <returns></returns>
    public static M_Adzone getAdzoneByZoneId(int adzoneid)
    {
        M_Adzone model = new M_Adzone();
        string sql = "select * from ZL_AdZone where ZoneId=@zoneid";
        SqlParameter[] parameter = new SqlParameter[1];
        parameter[0] = new SqlParameter("@zoneid", SqlDbType.Int);
        parameter[0].Value = adzoneid;
        using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
        {
            if (drd.Read())
            {
                return model.GetModelFromReader(drd);
            }
            else
            {
                return new M_Adzone(true);
            }
        }
    }

    private static ChartCall GetChartInfo(SqlDataReader rdr)
    {
        ChartCall info = new ChartCall();
        info.ChartTitle = rdr["ChartTitle"].ToString();
        info.ChartType = rdr["ChartType"].ToString();
        info.ChartUnit = rdr["ChartUnit"].ToString();
        info.ChartHeight = DataConverter.CLng(rdr["ChartHeight"]);
        info.ChartWidth = DataConverter.CLng(rdr["ChartWidth"]);
        info.CharData = rdr["CharData"].ToString();
        rdr.Close();
        return info;
    }

    /// <summary>
    /// 条件查询指定的版位
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public static DataTable ADZone_ByCondition(string con,SqlParameter[] sp=null)
    {
        string str = "select * from ZL_AdZone " + con;
        return SqlHelper.ExecuteTable(CommandType.Text, str, sp);
    }

    /// <summary>
    /// 统计某个版位的广告数
    /// </summary>
    /// <param name="strZoneId"></param>
    /// <returns></returns>
    public static int ADZone_Count(int strZoneId)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("strZoneId", strZoneId) };
        string str = "select count(*) from ZL_Zone_Advertisement where ZoneID=@strZoneId";
        return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, str, sp));
    }
    /// <summary>
    /// 删除某个广告所在的版位
    /// </summary>
    /// <param name="ADID"></param>
    /// <returns></returns>
    public static bool Delete_ADZone_Ad(string ADID)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ADID", ADID) };
        string str = "delete from ZL_Zone_Advertisement where ADID=@ADID";
        return SqlHelper.ExecuteNonQuery(CommandType.Text, str, sp) > 0;
    }
    /// <summary>
    /// 根据版位名获得版位
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DataTable GetZoneName(string name)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", "%" + name + "%") };
        string sql = "select * from ZL_AdZone where ZoneName like @name OR ZoneID like @name";
        return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
    }

    /// <summary>
    /// 使用单选按钮输出版位ID
    /// </summary>
    /// <returns></returns>
    public static DataTable ADZone_ID()
    {
        string sql = "select * from ZL_AdZone where Sales=1";
        return SqlHelper.ExecuteTable(CommandType.Text, sql);
    }
    private static ChartCall GetChartInfos(SqlDataReader rdr)
    {
        ChartCall info = new ChartCall();
        info.ChartTitle = rdr["ChartTitle"].ToString();
        info.ChartType = rdr["ChartType"].ToString();
        info.ChartUnit = rdr["ChartUnit"].ToString();
        info.ChartHeight = DataConverter.CLng(rdr["ChartHeight"]);
        info.ChartWidth = DataConverter.CLng(rdr["ChartWidth"]);
        info.CharData = rdr["CharData"].ToString();
        rdr.Close();
        return info;
    }

    public static DataTable SelectChart()
    {
        return null;
    }
    public static DataTable Select_Bytype(string con)
    {
        return null;
    }
    public static bool Chart_Remove(string id)
    {
        return true;
    }
    public static ChartCall getChartByChartID(int ChartID) { return new ChartCall(); }
}
