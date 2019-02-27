namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL;
    public class B_IPOperation
    {
        public B_IPOperation()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_IP_para initmod = new M_IP_para();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_IP_para SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_IP_para SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
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
        public bool UpdateByID(M_IP_para model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.IP_ID.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_IP_para model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }
        /// <summary>
        /// 分析客户的IP是属于哪个地方
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="ip_class"></param>
        /// <returns></returns>
        public string ClientArea(string ip, bool ip_class)
        {
            DataTable datatable = searchIP(ip);
            string area = "";
            if (datatable.Rows.Count > 0)
            {
                //ip_class是true 表明返回class_ID;否则返回 class_name
                if (ip_class)
                {
                    area = datatable.Rows[0]["class_ID"].ToString();
                }
                else
                {
                    area = datatable.Rows[0]["pro_name"].ToString() + datatable.Rows[0]["city_name"].ToString();
                }
            }
            return area;
        }
        /// <summary>
        /// 把IP段转化成LONG型的IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public long getSumIp(string ip)
        {
            long IP = 0;                                     //转成255进制的IP
            string[] ipsplit = ip.Split(new char[] { '.' }); //把IP以点为标记分成四段
            int split = 3;                                    //转换成255进制的幂
            for (int i = 0; i < ipsplit.Length; i++)
            {
                int per_IP = Convert.ToInt32(ipsplit[i]);
                IP = IP + Convert.ToInt64(per_IP * Math.Pow(255, split));
                split--;
            }
            return IP;
        }
        /// <summary>
        /// ip是一个以“.”为分隔符的四段，如127.0.0.1
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataTable searchIP(string ip)
        {
            string search = "*";                            //查询符号，如“*”
            string whereEx;                                 //查询条件，如“IP_ID = IP_ID”
            long IP;                                     //转成255进制的IP
            IP = getSumIp(ip);
            whereEx = "where " + IP + ">=startIP and " + IP + "<=endIP ORDER BY class_ID DESC";
            DataTable dt = searchIP(search, whereEx);
            return dt;
        }
        public DataTable searchIP(int class_ID)
        {
            DataTable dt = searchIP("* ", " where class_ID= " + class_ID);
            return dt;
        }
        /// <summary>
        /// 获得IP库所有信息
        /// </summary>
        /// <returns></returns>
        public DataTable searchAllIP()
        {
            string search = "*";
            return searchIP(search, "");
        }
        /// <summary>
        /// 查找省级分类
        /// </summary>
        /// <returns></returns>
        public DataTable searchIP_pro_Class()
        {
            string search = "*";
            string whereEx = " where leadto_ID=0";
            return searchIP_Class(search, whereEx);
        }
        /// <summary>
        /// 查找市级分类
        /// </summary>
        /// <param name="class_ID"></param>
        /// <returns></returns>
        public DataTable searchIP_city_Class(int class_ID)
        {
            string search = "*";
            string whereEx = " where leadto_ID=" + class_ID;
            return search_Class(search, whereEx);
        }
        /// <summary>
        /// 查找class_ID等于传入值的分类记录,返回一个M_IP_class实例
        /// </summary>
        /// <param name="class_ID"></param>
        /// <returns></returns>
        public M_IP_class searchClass(int class_ID)
        {
            M_IP_class m_ip_class = new M_IP_class();
            DataTable dt = search_Class("*", " where class_ID=" + class_ID);
            if (dt.Rows.Count > 0)
            {
                m_ip_class.class_ID = Convert.ToInt32(dt.Rows[0][0].ToString());
                m_ip_class.class_name = dt.Rows[0][2].ToString();
                m_ip_class.leadto_ID = Convert.ToInt32(dt.Rows[0][1].ToString());
            }
            return m_ip_class;
        }
        /// <summary>
        /// 向IP库插入一条IP信息
        /// </summary>
        /// <param name="startIP"></param>
        /// <param name="endIP"></param>
        /// <param name="pro_name"></param>
        /// <param name="city_name"></param>
        /// <param name="class_ID"></param>
        public void insertIP(string startIP, string endIP, string pro_name, string city_name, int class_ID)
        {
            M_IP_para m_IP_para = new M_IP_para();

            m_IP_para.startIP = getSumIp(startIP);
            m_IP_para.endIP = getSumIp(endIP);
            m_IP_para.pro_name = pro_name;
            m_IP_para.city_name = city_name;
            m_IP_para.class_ID = class_ID;
            string sqlStr = "insert into ZL_IPpara (startIP,endIP,pro_name,city_name,class_ID) values (@startIP,@endIP,@pro_name,@city_name,@class_ID)";
            SqlParameter[] Parameters = new SqlParameter[]{
                new SqlParameter("@startIP",SqlDbType.Int),
                new SqlParameter("@endIP",SqlDbType.Int),
                new SqlParameter("@pro_name",SqlDbType.VarChar),
                new SqlParameter("@city_name",SqlDbType.VarChar),
                new SqlParameter("@class_ID",SqlDbType.Int)
            };
            Parameters[0].Value = m_IP_para.startIP;
            Parameters[1].Value = m_IP_para.endIP;
            Parameters[2].Value = m_IP_para.pro_name;
            Parameters[3].Value = m_IP_para.city_name;
            Parameters[4].Value = m_IP_para.class_ID;
            SqlHelper.ExistsSql(sqlStr, Parameters);
        }
        public void insertClass(string class_name, int leadto_ID)
        {
            string sqlStr = "insert into ZL_IPclass (class_name,leadto_ID) values (@class_name,@leadto_ID)";
            SqlParameter[] Parameters = new SqlParameter[]{
                new SqlParameter("@class_name",SqlDbType.NVarChar),
                new SqlParameter("@leadto_ID",SqlDbType.Int)
            };
            Parameters[0].Value = class_name;
            Parameters[1].Value = leadto_ID;
            SqlHelper.ExistsSql(sqlStr, Parameters);
        }
        public DataTable searchAllClass()
        {
            string search = " *";
            string whereEx = " ";
            string sql = "select" + @search + "from ZL_IPclass " + @whereEx;

            DataTable ds = SqlHelper.ExecuteTable(CommandType.Text, sql, null);
            return ds;
        }
        public void updateClass(int class_ID, int leadto_ID, string class_name)
        {
            M_IP_class m_IP_class = new M_IP_class();
            m_IP_class.class_ID = class_ID;
            m_IP_class.class_name = class_name;
            m_IP_class.leadto_ID = leadto_ID;
            string sqlstr = "update ZL_IPclass set leadto_ID=@leadto_ID,class_name=@class_name where class_ID=@class_ID";
            SqlParameter[] Parameters = new SqlParameter[]{
                new SqlParameter("@leadto_ID",SqlDbType.Int),
                new SqlParameter("@class_name",SqlDbType.NVarChar),
                new SqlParameter("@class_ID",SqlDbType.Int)
            };
            Parameters[0].Value = m_IP_class.leadto_ID;
            Parameters[1].Value = m_IP_class.class_name;
            Parameters[2].Value = m_IP_class.class_ID;
            SqlHelper.ExistsSql(sqlstr, Parameters);
        }
        public void deleteClass(int class_ID)
        {
            string sqlStr = "delete from ZL_IPclass where class_ID=@class_ID";
            SqlParameter[] Parameters = new SqlParameter[]{
                new SqlParameter("@class_ID",SqlDbType.Int)
            };
            Parameters[0].Value = class_ID;
            SqlHelper.ExistsSql(sqlStr, Parameters);
        }
        public M_IP_para searchIPByID(int IP_ID)
        {
            M_IP_para m_IP_para = new M_IP_para();
            DataTable dt = searchIP("*", " where IP_ID=" + IP_ID);
            m_IP_para.IP_ID = Convert.ToInt32(dt.Rows[0]["IP_ID"].ToString());
            m_IP_para.class_ID = Convert.ToInt32(dt.Rows[0]["class_ID"].ToString());
            m_IP_para.pro_name = dt.Rows[0]["pro_name"].ToString();
            m_IP_para.city_name = dt.Rows[0]["city_name"].ToString();
            m_IP_para.startIP = Convert.ToInt32(dt.Rows[0]["startIP"].ToString());
            m_IP_para.endIP = Convert.ToInt32(dt.Rows[0]["endIP"].ToString());
            return m_IP_para;
        }
        public void updateIP(M_IP_para m_IP_para)
        {
            string cmd = "update ZL_IPpara set startIP=@startIP,endIp=@endIP,pro_name=@pro_name,city_name=@city_name,class_ID=@class_ID where IP_ID=@IP_ID";
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@startIP", SqlDbType.BigInt),
            new SqlParameter("@endIP", SqlDbType.BigInt),
            new SqlParameter("@pro_name", SqlDbType.VarChar),
            new SqlParameter("@city_name", SqlDbType.VarChar),
            new SqlParameter("@class_ID", SqlDbType.Int),
            new SqlParameter("@IP_ID", SqlDbType.Int),
            };
            parameter[0].Value = m_IP_para.startIP;
            parameter[1].Value = m_IP_para.endIP;
            parameter[2].Value = m_IP_para.pro_name;
            parameter[3].Value = m_IP_para.city_name;
            parameter[4].Value = m_IP_para.class_ID;
            parameter[5].Value = m_IP_para.IP_ID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmd, parameter);
        }
        public void deleIP(int IP_ID)
        {
            string cmd = "delete from ZL_IPpara where IP_ID=@IP_ID";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@IP_ID",SqlDbType.Int)
            };
            parameter[0].Value = IP_ID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmd, parameter);

        }
        public DataTable searchIP(string search, string whereEx)
        {
            string sql = "select" + @search + "from ZL_IPpara " + @whereEx;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public DataTable searchIP_Class(string search, string whereEx)
        {
            string sql = "select" + @search + "from ZL_IPclass " + @whereEx;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public DataTable search_Class(string search, string whereEx)
        {
            string sql = "select" + @search + "from ZL_IPclass " + @whereEx;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
    }
}