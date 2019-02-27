using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_UserRegisterIP:M_Base
    {

        #region 定义字段

        //主键，自动增长
        public int Id { get; set; }
        //限制间隔时间(单位为小时)
        public int LimitTime { get; set; }
        //是否限制
        public int IsLimit { get; set; }
        //是否设置受限IP段
        public int IsIPpart { get; set; }
        //受限起始IP
        public string BeginIP { get; set; }
        //受限结束IP
        public string EndIP { get; set; }
        #endregion
        public override string TbName { get { return "ZL_UserRegisterIP"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Id","Int","4"},
                                  {"LimitTime","Int","4"},
                                  {"IsLimit","Int","4"},
                                  {"IsIPpart","Int","4"},
                                  {"BeginIP","NVarChar","1000"}, 
                                  {"EndIP","NVarChar","1000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserRegisterIP model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.LimitTime;
            sp[2].Value = model.IsLimit;
            sp[3].Value = model.IsIPpart;
            sp[4].Value = model.BeginIP;
            sp[5].Value = model.EndIP; 
            return sp;
        }
        public M_UserRegisterIP GetModelFromReader(SqlDataReader rdr)
        {
            M_UserRegisterIP model = new M_UserRegisterIP();
            model.Id = Convert.ToInt32(rdr["ID"]);
            model.LimitTime = ConvertToInt(rdr["LimitTime"]);
            model.IsLimit = ConvertToInt(rdr["IsLimit"]);
            model.IsIPpart = ConvertToInt(rdr["IsIPpart"]);
            model.BeginIP = ConverToStr(rdr["BeginIP"]);
            model.EndIP = ConverToStr(rdr["EndIP"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}