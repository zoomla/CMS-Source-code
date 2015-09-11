namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using System.Xml.Serialization;
    using ZoomLa.IDAL;
    using System.IO;
    using ZoomLa.Common;

    /// <summary>
    /// 管理员工作台配置
    /// </summary>
    public class SD_AdminProfile :ID_AdminProfile
    {
        private Serialize<M_AdminProfile> adminProfileSer = new Serialize<M_AdminProfile>();
        public SD_AdminProfile()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public void Add(M_AdminProfile adminProileInfo)
        {
            SqlParameter[] cmdParams = new SqlParameter[] 
            { 
                new SqlParameter("@AdminName",SqlDbType.NVarChar,50),
                new SqlParameter("@PersonalSetting",SqlDbType.NText,16)
            };
            cmdParams[0].Value = adminProileInfo.AdminName;
            string str = this.adminProfileSer.SerializeField(adminProileInfo);
            cmdParams[1].Value=str;
            SqlHelper.ExecuteSql("INSERT INTO ZL_AdminProfile (AdminName,PersonalSetting) VALUES (@AdminName,@PersonalSetting)", cmdParams);
        }

        public bool ExistsAdminName(string adminName)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@AdminName",SqlDbType.NVarChar,50) };
            cmdParams[0].Value=adminName;
            return SqlHelper.ExistsSql("SELECT Count(*) FROM ZL_AdminProfile WHERE AdminName=@AdminName", cmdParams);
        }

        public M_AdminProfile GetAdminProfile(string adminName)
        {
            M_AdminProfile info = null;
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@AdminName", SqlDbType.NVarChar, 50) };
            cmdParams[0].Value = adminName;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,"SELECT * FROM ZL_AdminProfile WHERE AdminName=@AdminName", cmdParams))
            {
                if (reader.Read())
                {
                    info = this.adminProfileSer.DeserializeField(reader["PersonalSetting"].ToString());
                    info.AdminName = reader["AdminName"].ToString();                    
                    return info;
                }
                return new M_AdminProfile();
            }
        }

        public void Update(M_AdminProfile adminProileInfo)
        {
            SqlParameter[] cmdParams = new SqlParameter[] 
            { 
                new SqlParameter("@AdminName",SqlDbType.NVarChar,50),
                new SqlParameter("@PersonalSetting",SqlDbType.NText,16)
            };
            cmdParams[0].Value= adminProileInfo.AdminName;
            string str = this.adminProfileSer.SerializeField(adminProileInfo);
            cmdParams[1].Value= str;
            SqlHelper.ExecuteSql("UPDATE PE_AdminProfile SET PersonalSetting=@PersonalSetting WHERE AdminName=@AdminName", cmdParams);
        }
    }
}