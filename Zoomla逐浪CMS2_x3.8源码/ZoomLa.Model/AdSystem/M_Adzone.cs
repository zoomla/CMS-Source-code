namespace ZoomLa.Model
{

    using System;
    using System.Data;
    using System.Data.SqlClient;
    using ZoomLa.Model.AdSystem;
    using System.Data.Common;

    /// <summary>
    /// Adzone 的摘要说明
    /// </summary>
    [Serializable]
    public class M_Adzone:M_Base
    {
        public int ZoneID { get; set; }
        /// <summary>
        /// 广告版位名称
        /// </summary>
        public string ZoneName { get; set; }
        /// <summary>
        /// 生成JS名
        /// </summary>
        public string ZoneJSName { get; set; }
        /// <summary>
        /// 版位描述
        /// </summary>
        public string ZoneIntro { get; set; }
        /// <summary>
        /// 版位类型
        /// </summary>
        public int ZoneType { get; set; }
        /// <summary>
        /// 是否默认设置
        /// </summary>
        public bool DefaultSetting { get; set; }
        /// <summary>
        /// 具体自定义设置信息
        /// </summary>
        public string ZoneSetting { get; set; }
        /// <summary>
        /// 版位宽度
        /// </summary>
        public int ZoneWidth { get; set; }
        /// <summary>
        /// 版位高度
        /// </summary>
        public int ZoneHeight { get; set; }
        /// <summary>
        /// 是否活动
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// 显示方式
        /// </summary>
        public int ShowType{get;set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        public bool IsNull { get; set; }
        private bool m_IsNull;
       
        /// <summary>
        /// 是否开放用户申请
        /// </summary>
        public int Sales { get; set; }
        public M_Adzone()
        {

        }
        public M_Adzone(bool flag)
        {
            this.m_IsNull = flag;
        }
        public override string PK { get { return "ZoneID"; } }
        public override string TbName { get { return "ZL_AdZone"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ZoneID","Int","4"},
                                  {"ZoneName","NChar","100"},
                                  {"ZoneJSName","NChar","100"}, 
                                  {"ZoneIntro","NChar","255"},
                                  {"ZoneType","Int","4"},
                                  {"DefaultSetting","Bit","4"}, 
                                  {"ZoneSetting","NChar","255"},
                                  {"ZoneWidth","Int","4"},
                                  {"ZoneHeight","Int","4"}, 
                                  {"Active","Bit","4"},
                                  {"ShowType","Int","4"},
                                  {"UpdateTime","DateTime","8"},
                                  {"Sales","Int","4"}
                             };

            return Tablelist;

        }
        
        public override SqlParameter[] GetParameters()
        {
            M_Adzone model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ZoneID;
            sp[1].Value = model.ZoneName;
            sp[2].Value = model.ZoneJSName;
            sp[3].Value = model.ZoneIntro;
            sp[4].Value = model.ZoneType;
            sp[5].Value = model.DefaultSetting;
            sp[6].Value = model.ZoneSetting;
            sp[7].Value = model.ZoneWidth;
            sp[8].Value = model.ZoneHeight;
            sp[9].Value = model.Active;
            sp[10].Value = model.ShowType;
            sp[11].Value = model.UpdateTime;
            sp[12].Value = model.Sales;

            return sp;
        }
        public M_Adzone GetModelFromReader(DbDataReader rdr)
        {
            M_Adzone model = new M_Adzone();
            model.ZoneID = Convert.ToInt32(rdr["ZoneID"]);
            model.ZoneName = ConverToStr(rdr["ZoneName"]);
            model.ZoneJSName = ConverToStr(rdr["ZoneJSName"]);
            model.ZoneIntro = ConverToStr(rdr["ZoneIntro"]);
            model.ZoneType = ConvertToInt(rdr["ZoneType"]);
            model.DefaultSetting = ConverToBool(rdr["DefaultSetting"]);
            model.ZoneSetting = ConverToStr(rdr["ZoneSetting"]);
            model.ZoneWidth = ConvertToInt( rdr["ZoneWidth"]);
            model.ZoneHeight = ConvertToInt(rdr["ZoneHeight"]);
            model.Active = ConverToBool(rdr["Active"]);
            model.ShowType = ConvertToInt(rdr["ShowType"]);
            model.UpdateTime = ConvertToDate(rdr["UpdateTime"]);
            model.Sales = ConvertToInt(rdr["Sales"]);


            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}

