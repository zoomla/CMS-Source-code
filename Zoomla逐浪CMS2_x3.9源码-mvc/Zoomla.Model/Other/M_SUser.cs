using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_SUser
    {
        #region 构造函数
        public M_SUser()
        {
        }

        public M_SUser
        (
            int su_id,
            int SNID,
            string Uname,
            int IDType,
            string IDnum,
            string VinNum,
            int Invoice,
            DateTime InvoiceTime,
            int jinkou,
            string LivingNum,
            string tel,
            string motel,
            string adress,
            string adressgo,
            string postcode,
            DateTime addtime,
            DateTime endtime
        )
        {
            this.su_id = su_id;
            this.SNID = SNID;
            this.Uname = Uname;
            this.IDType = IDType;
            this.IDnum = IDnum;
            this.VinNum = VinNum;
            this.Invoice = Invoice;
            this.InvoiceTime = InvoiceTime;
            this.jinkou = jinkou;
            this.LivingNum = LivingNum;
            this.tel = tel;
            this.motel = motel;
            this.adress = adress;
            this.adressgo = adressgo;
            this.postcode = postcode;
            this.addtime = addtime;
            this.endtime = endtime;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SUserList()
        {
            string[] Tablelist = { "su_id", "SNID", "Uname", "IDType", "IDnum", "VinNum", "Invoice", "InvoiceTime", "jinkou", "LivingNum", "tel", "motel", "adress", "adressgo", "postcode", "addtime", "endtime" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int su_id { get; set; }
        /// <summary>
        /// 号码ID
        /// </summary>
        public int SNID { get; set; }
        /// <summary>
        /// 姓名或单位名称
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// 身份证明名称
        /// </summary>
        public int IDType { get; set; }
        /// <summary>
        /// 身份证明号码
        /// </summary>
        public string IDnum { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string VinNum { get; set; }
        /// <summary>
        /// 发票号码
        /// </summary>
        public int Invoice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InvoiceTime { get; set; }
        /// <summary>
        /// 是否进口
        /// </summary>
        public int jinkou { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LivingNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string motel { get; set; }
        /// <summary>
        /// 住所地址
        /// </summary>
        public string adress { get; set; }
        /// <summary>
        /// 邮寄地址
        /// </summary>
        public string adressgo { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime endtime { get; set; }
        #endregion

        /// <summary>
        /// 表名
        /// </summary>
        public static string TbName = "ZL_SUser";
        /// <summary>
        /// 标识列
        /// </summary>
        public static string PK = "su_id";

        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public static string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"su_id","Int","4"}, 
                                  {"SNID","Int","4"}, 
                                  {"Uname","NVarChar","1000"}, 
                                  {"IDType","Int","4"},
                                  {"IDnum","NVarChar","1000"}, 
                                  {"VinNum","NVarChar","1000"},
                                  {"Invoice","NVarChar","1000"},
                                  {"InvoiceTime","DateTime","8"},
                                  {"jinkou","Int","4"},
                                  {"LivingNum","NVarChar","1000"}, 
                                  {"tel","NVarChar","1000"}, 
                                  {"motel","NVarChar","1000"}, 
                                  {"adress","NVarChar","1000"}, 
                                  {"adressgo","NVarChar","1000"}, 
                                  {"postcode","NVarChar","1000"}, 
                                  {"addtime","DateTime","8"},
                                  {"endtime","DateTime","8"}
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public static string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取参数串
        /// </summary>
        public static string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取字段=参数
        /// </summary>
        public static string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public static SqlParameter[] GetParameters(M_SUser model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.su_id;
            sp[1].Value = model.SNID;
            sp[2].Value = model.Uname;
            sp[3].Value = model.IDType;
            sp[4].Value = model.IDnum;
            sp[5].Value = model.VinNum;
            sp[6].Value = model.Invoice;
            sp[7].Value = model.InvoiceTime;
            sp[8].Value = model.jinkou;
            sp[9].Value = model.LivingNum;
            sp[10].Value = model.tel;
            sp[11].Value = model.motel;
            sp[12].Value = model.adress;
            sp[13].Value = model.adressgo;
            sp[14].Value = model.postcode;
            sp[15].Value = model.addtime;
            sp[16].Value = model.endtime; 
            return sp;
        }
        public static M_SUser GetModelFromReader(SqlDataReader rdr)
        {
            M_SUser model = new M_SUser();
            model.su_id = Convert.ToInt32(rdr["su_id"]);
            model.SNID = Convert.ToInt32(rdr["SNID"]);
            model.Uname = rdr["Uname"].ToString();
            model.IDType = Convert.ToInt32(rdr["IDType"]);
            model.IDnum = rdr["IDnum"].ToString();
            model.VinNum = rdr["VinNum"].ToString();
            model.Invoice = Convert.ToInt32(rdr["Invoice"]);
            model.InvoiceTime = Convert.ToDateTime(rdr["InvoiceTime"]);
            model.jinkou = Convert.ToInt32(rdr["jinkou"]);
            model.LivingNum = rdr["LivingNum"].ToString();
            model.tel = rdr["tel"].ToString();
            model.motel = rdr["motel"].ToString();
            model.adress = rdr["adress"].ToString();
            model.adressgo = rdr["adressgo"].ToString();
            model.postcode = rdr["postcode"].ToString();
            model.addtime = Convert.ToDateTime(rdr["addtime"]);
            model.endtime = Convert.ToDateTime(rdr["endtime"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}