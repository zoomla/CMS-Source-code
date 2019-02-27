using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Bid : M_Base
    {
        #region 构造函数
        public M_Bid()
        {
        }

        public M_Bid
        (
            int id,
            double Participation,
            double Compliance,
            double Successful,
            double Website
        )
        {
            this.Id = id;
            this.Participation = Participation;
            this.Compliance = Compliance;
            this.Successful = Successful;
            this.Website = Website;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] BidList()
        {
            string[] Tablelist = { "id", "Participation", "Compliance", "Successful", "Website" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 参与者分成
        /// </summary>
        public double Participation { get; set; }
        /// 达标者分成
        /// </summary>
        public double Compliance { get; set; }
        /// <summary>
        /// 中标者
        /// </summary>
        public double Successful { get; set; }
        /// <summary>
        /// 网站商分成
        /// </summary>
        public double Website { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Bid"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Participation","Float","8"},
                                  {"Compliance","Float","8"},
                                  {"Successful","Float","8"},
                                  {"Website","Float","8"}
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public string GetFields()
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
        public string GetParas()
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
            M_Bid model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.Participation;
            sp[2].Value = model.Compliance;
            sp[3].Value = model.Successful;
            sp[4].Value = model.Website;
            return sp;
        }

        public M_Bid GetModelFromReader(SqlDataReader rdr)
        {
            M_Bid model = new M_Bid();
            model.Id = Convert.ToInt32(rdr["id"]);
            model.Participation = ConverToDouble(rdr["Participation"]);
            model.Compliance = ConverToDouble(rdr["Compliance"]);
            model.Successful = ConverToDouble(rdr["Successful"]);
            model.Website = ConverToDouble(rdr["Website"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}