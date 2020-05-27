using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    /// <summary>
    /// PicCateg
    /// 相册数据访问层
    /// </summary>
    public class PicCateg_Logic
    {
        #region 创建相册
        /// <summary>
        /// 创建相册
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public static Guid Add(PicCateg pc)
        {
            try
            {
                pc.ID = Guid.NewGuid();
                string SQLstr = @"INSERT INTO [ZL_Sns_PicCateg]([ID],[PicCategTitle],[PicCategUserID],[TitlePIc],[State],CategTime,PicCategPws)
                                VALUES (@ID,@PicCategTitle,@PicCategUserID,@TitlePIc,@State,@CategTime,@PicCategPws)";
                SqlParameter[] sp = new SqlParameter[7];
                sp[0] = new SqlParameter("@ID", pc.ID);
                sp[1] = new SqlParameter("@PicCategTitle", pc.PicCategTitle);
                sp[2] = new SqlParameter("@PicCategUserID", pc.PicCategUserID);
                sp[3] = new SqlParameter("@TitlePIc", pc.TitlePIc);
                sp[4] = new SqlParameter("@State", pc.State);
                sp[5] = new SqlParameter("@CategTime", DateTime.Now);
                sp[6] = new SqlParameter("@PicCategPws",pc.PicCategPws);
                SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, SQLstr, sp);
                return pc.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改相册信息
        /// <summary>
        /// 修改相册信息
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public static Guid Update(PicCateg pc)
        {
            try
            {
                string SQLstr = @"UPDATE [ZL_Sns_PicCateg] SET [PicCategTitle] = @PicCategTitle,[State]=@State,PicCategPws=@PicCategPws WHERE [ID] = @ID";

                SqlParameter[] sp = new SqlParameter[4];
                sp[0] = new SqlParameter("@ID", pc.ID);
                sp[1] = new SqlParameter("@PicCategTitle", pc.PicCategTitle);
                sp[2] = new SqlParameter("@State", pc.State);
                sp[3] = new SqlParameter("@PicCategPws",pc.PicCategPws);

                int i = SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
                return pc.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 删除相册
        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM [ZL_Sns_PicCateg]
      WHERE ID=@ID";
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@ID", id);

            SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 相册列表
        /// <summary>
        /// 相册列表
        /// </summary>
        /// <param name="callType">访问级别</param>
        /// <param name="intervieweeID">受访问用户ID</param>
        /// <param name="page">分页</param>
        /// <returns>返回相册列表</returns>
        public static List<PicCateg> GetPicCategList(int callType, Guid intervieweeID, PagePagination page)
        {
            try
            {
                string SQLstr = String.Empty;
                if (callType == 1)
                    SQLstr = @"select * from ZL_Sns_PicCateg where PicCategUserID=@PicCategUserID and State=@State";
                else
                    SQLstr = @"select * from ZL_Sns_PicCateg where PicCategUserID=@PicCategUserID and State in (@State)";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@PicCategUserID",intervieweeID),new SqlParameter("@State",callType)};
                List<PicCateg> list = new List<PicCateg>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        PicCateg piccateg = new PicCateg();
                        ReadPicCateg(dr, piccateg);
                        GetPicCategTiele(dr, piccateg);
                        list.Add(piccateg);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #region 所有相册
        /// <summary>
        /// 所有相册
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<PicCateg> GetAllPic(PagePagination page)
        {
            try
            {
                string SQLstr = @"select * from ZL_Sns_PicCateg ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                List<PicCateg> list = new List<PicCateg>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, null))
                {
                    while (dr.Read())
                    {
                        PicCateg piccateg = new PicCateg();
                        ReadPicCateg(dr, piccateg);
                        GetPicCategTiele(dr, piccateg);
                        list.Add(piccateg);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        public static List<PicCateg> GetPicCategList(int callType, int intervieweeID, PagePagination page)
        {
            try
            {
                string SQLstr = String.Empty;
                if (callType == 1)
                    SQLstr = @"select * from ZL_Sns_PicCateg where PicCategUserID=@PicCategUserID and State=@State";
                else
                    SQLstr = @"select * from ZL_Sns_PicCateg where PicCategUserID=@PicCategUserID and State in (@State)";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@PicCategUserID",intervieweeID),new SqlParameter("@State",callType)};
                List<PicCateg> list = new List<PicCateg>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        PicCateg piccateg = new PicCateg();
                        ReadPicCateg(dr, piccateg);
                        GetPicCategTiele(dr, piccateg);
                        list.Add(piccateg);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 我的相册列表
        /// <summary>
        /// 当前用户的相册列表
        /// </summary>
        /// <param name="intervieweeID">受访问用户ID</param>
        /// <param name="page">分页</param>
        /// <returns>返回相册列表</returns>
        public static List<PicCateg> GetMyPicCategList(int intervieweeID, PagePagination page)
        {
            try
            {
                string SQLstr = @"select * from ZL_Sns_PicCateg where PicCategUserID=@PicCategUserID order by CategTime asc ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@PicCategUserID",intervieweeID)};
                List<PicCateg> list = new List<PicCateg>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        PicCateg piccateg = new PicCateg();
                        ReadPicCateg(dr, piccateg);
                        GetPicCategTiele(dr, piccateg);
                        list.Add(piccateg);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取相册的图标
        /// <summary>
        ///获取相册的图标 
        /// </summary>
        /// <param name="piccateg"></param>
        public static void GetPicCategTiele(SqlDataReader dr, PicCateg piccateg)
        {
            try
            {
                if (dr["TitlePIc"] is DBNull)
                {
                    string sqlGetPicCategTiele = @"select top 1 PicUrl from ZL_Sns_PicTure where PicCategID=@PicCategID";
                    SqlParameter[] parameter ={
                new SqlParameter("@PicCategID",piccateg.ID)};
                    using (SqlDataReader dr1 = SqlHelper.ExecuteReader(CommandType.Text, sqlGetPicCategTiele, parameter))
                    {
                        if (dr1.Read())
                        {
                            piccateg.TitlePIcUrl = dr1["PicUrl"].ToString();
                        }
                    }
                }
                else
                {
                    PicTure pt = PicTure_Logic.GetPic(new Guid(dr["TitlePIc"].ToString()));
                    if (pt != null)
                    {
                        piccateg.TitlePIcUrl = pt.PicUrl;
                    }
                    else
                    {
                        string sqlGetPicCategTiele = @"select top 1 PicUrl from PicTure where PicCategID=@PicCategID";
                        SqlParameter[] parameter ={
                         new SqlParameter("@PicCategID",piccateg.ID)};
                        using (SqlDataReader dr2 = SqlHelper.ExecuteReader(CommandType.Text, sqlGetPicCategTiele, parameter))
                        {
                            if (dr2.Read())
                            {
                                piccateg.TitlePIcUrl = dr2["PicUrl"].ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取相册信息
        /// <summary>
        /// 读取相册信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccateg"></param>
        public static void ReadPicCateg(SqlDataReader dr, PicCateg piccateg)
        {
            piccateg.ID = (Guid)dr["ID"];
            piccateg.PicCategTitle = dr["PicCategTitle"].ToString();
            piccateg.PicCategUserID = Convert.ToInt32(dr["PicCategUserID"].ToString ());
            // piccateg.TitlePIc = (Guid)dr["TitlePIc"];
            piccateg.State = int.Parse(dr["State"].ToString());
            piccateg.CategTime = dr["CategTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["CategTime"].ToString());
            piccateg.PicCategPws = dr["PicCategPws"].ToString();
        }
        #endregion

        #region 设置相册首页相片
        /// <summary>
        /// 设置相册首页相片
        /// </summary>
        /// <param name="PicID">相片ID</param>
        /// <param name="CategID">相册ID</param>
        public static void CategFirstPic(Guid PicID, Guid CategID)
        {
            try
            {
                string SQLstr = @"UPDATE [ZL_Sns_PicCateg] SET [TitlePIc] = @TitlePIc WHERE [ID] = @ID";
                SqlParameter[] sp ={ new SqlParameter("@TitlePIc", PicID), new SqlParameter("@ID", CategID) };
                SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询单个相册信息
        /// <summary>
        /// 查询单个相册信息
        /// </summary>
        /// <param name="categid">相册ID</param>
        /// <returns></returns>
        public static PicCateg GetPicCateg(Guid categid)
        {
            try
            {
                PicCateg piccateg = new PicCateg();
                string SQLstr = @"select * from ZL_Sns_PicCateg where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("@ID",categid)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        ReadPicCateg(dr, piccateg);
                    }
                }
                return piccateg;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
