using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model.AdSystem
{
    public class M_Adbuy:M_Base
    {
        #region 定义字段

        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ADID { get; set; }
        /// <summary>

        /// </summary>

        public int UID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 
        /// </summary>

        /// 
        /// </summary>
        public int ShowTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public DateTime Ptime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        public string Files { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Audit { get; set; }
        /// <summary>

        #endregion
        public M_Adbuy()
        {
            this.Time = DateTime.Now;
            this.Ptime = DateTime.Now;
            this.Content = string.Empty;
            this.Files = string.Empty;
        }
        public override string TbName
        {
            get { return "ZL_Adbuy"; }
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"ADID","Int","4"},
                                  {"UID","Int","4"}, 
                                  {"Time","DateTime","8"},
                                  {"ShowTime","Int","4"},
                                  {"Scale","Int","4"}, 
                                  {"Ptime","DateTime","8"},
                                  {"Content","NVarChar","450"},
                                  {"Files","NVarChar","400"}, 
                                  {"State","Int","4"},
                                  {"Price","Money","100"},
                                  {"Audit","Bit","1"}
                                  
                             };

            return Tablelist;

        }
        public override SqlParameter[] GetParameters()
        {
            M_Adbuy model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ADID;
            sp[2].Value = model.UID;
            sp[3].Value = model.Time;
            sp[4].Value = model.ShowTime;
            sp[5].Value = model.Scale;
            sp[6].Value = model.Ptime;
            sp[7].Value = model.Content;
            sp[8].Value = model.Files;
            sp[9].Value = model.State;
            sp[10].Value = model.Price;
            sp[11].Value = model.Audit;
           
            return sp;
        }
        public M_Adbuy GetModelFromReader(SqlDataReader rdr)
        {
            M_Adbuy model = new M_Adbuy();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ADID =ConvertToInt( rdr["ADID"]);
            model.UID = ConvertToInt(rdr["UID"]);
            model.Time = ConvertToDate(rdr["Time"]);
            model.ShowTime = ConvertToInt(rdr["ShowTime"]);
            model.Scale = ConvertToInt(rdr["Scale"]);
            model.Ptime = ConvertToDate(rdr["Ptime"]);
            model.Content =rdr["Content"].ToString();
            model.Files = rdr["Files"].ToString();
            model.State = ConvertToInt( rdr["State"]);
            model.Price =Convert.ToDecimal( rdr["Price"]);
            model.Audit = ConverToBool(rdr["Audit"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
        
    }
}

