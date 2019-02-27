using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_BindPro:M_Base
    {
        #region 构造函数
        public M_BindPro()
        {
            this.ProName = string.Empty;
            this.Rtype = string.Empty;
        }

        public M_BindPro
        (
            int ID,
            int BindProID,
            int ProID,
            string ProName,
            double Jiage,
            int Pronum,
            string Rtype
        )
        {
            this.ID = ID;
            this.BindProID = BindProID;
            this.ProID = ProID;
            this.ProName = ProName;
            this.Jiage = Jiage;
            this.Pronum = Pronum;
            this.Rtype = Rtype;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] BindProList()
        {
            string[] Tablelist = { "ID", "BindProID", "ProID", "ProName", "Jiage", "Pronum", "Rtype" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 绑定商品ID
        /// </summary>
        public int BindProID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double Jiage { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Pronum { get; set; }
        public string Rtype { get; set; }
        #endregion
        public override string TbName { get { return "ZL_BindPro"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"BindProID","Int","4"},
                                  {"ProID","Int","4"},
                                  {"ProName","NVarChar","255"}, 
                                  {"Jiage","Money","8"},
                                  {"Pronum","Int","4"},
                                 };
            return Tablelist;
        }
        public string GetFieldAndPara()
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
        public override SqlParameter[] GetParameters()
        {
            M_BindPro model = this;
            SqlParameter[] sp = this.GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.BindProID;
            sp[2].Value = model.ProID;
            sp[3].Value = model.ProName;
            sp[4].Value = model.Jiage;
            sp[5].Value = model.Pronum;
            return sp;
        }

        public M_BindPro GetModelFromReader(SqlDataReader rdr)
        {
            M_BindPro model = new M_BindPro();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.BindProID = ConvertToInt(rdr["BindProID"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.ProName = ConverToStr(rdr["ProName"]);
            model.Jiage = ConverToDouble(rdr["Jiage"]);
            model.Pronum = ConvertToInt(rdr["Pronum"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}


