using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Advertisement:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 广告标识
        /// </summary>
        public int ADID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public int UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 广告类型
        /// </summary>
        public int ADType
        {
            get;
            set;
        }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string ADName
        {
            get;
            set;
        }
        /// <summary>
        /// 过期图片地址
        /// </summary>
        public string ImgUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 图片广告的宽度
        /// </summary>
        public int ImgWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 图片广告的高度
        /// </summary>
        public int ImgHeight
        {
            get;
            set;
        }
        /// <summary>
        /// 广告是flash类型/(透明/或不透明)
        /// </summary>
        public int FlashWmode
        {
            get;
            set;
        }
        /// <summary>
        /// 广告介绍/内容
        /// </summary>
        public string ADIntro
        {
            get;
            set;
        }
        /// <summary>
        /// 目标地址
        /// </summary>
        public string LinkUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 新窗口,原窗口
        /// </summary>
        public int LinkTarget
        {
            get;
            set;
        }
        /// <summary>
        /// 链接提示
        /// </summary>
        public string LinkAlt
        {
            get;
            set;
        }
        /// <summary>
        /// 权重
        /// </summary>
        public int Priority
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Setting
        {
            get;
            set;
        }
        /// <summary>
        /// 是否统计浏览次数
        /// </summary>
        public bool CountView
        {
            get;
            set;
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Views
        {
            get;
            set;
        }
        /// <summary>
        /// 是否统计点击数
        /// </summary>
        public bool CountClick
        {
            get;
            set;
        }
        /// <summary>
        /// 点击数
        /// </summary>
        public int Clicks
        {
            get;
            set;
        }
        /// <summary>
        /// 是否设定过期
        /// </summary>
        public bool Passed
        {
            get;
            set;
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime OverdueDate
        {
            get;
            set;
        }
        /// <summary>
        /// 过期图片地址
        /// </summary>
        public string ImgUrl1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期图片广告的高度
        /// </summary>
        public int ImgHeight1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期图片广告的宽度
        /// </summary>
        public int ImgWidth1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期目标地址
        /// </summary>
        public string LinkUrl1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期新窗口,原窗口
        /// </summary>
        public int LinkTarget1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期链接提示
        /// </summary>
        public string LinkAlt1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期广告介绍/内容
        /// </summary>
        public string ADIntro1
        {
            get;
            set;
        }
        /// <summary>
        /// 过期广告是flash类型/(透明/或不透明)
        /// </summary>
        public int FlashWmode1
        {
            get;
            set;
        }
        public bool IsNull
        {
            get;
            set;
        }
        public int ZoneID
        {
            get;
            set;
        }
        /// <summary>
        /// 出售价格（按天计）
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开放竞价（0：否，1：是）
        /// </summary>
        public int ADBuy
        {
            get;
            set;
        }
        public int Days
        {
            get
            {
                TimeSpan span = (TimeSpan)(this.OverdueDate.Date - DateTime.Today.Date);
                return span.Days;
            }
        }
        #endregion
        public override string PK { get { return "ADID"; } }
        public override string TbName { get { return "ZL_Advertisement"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ADID","Int","4"},            
                        {"UserID","Int","4"},            
                        {"ADType","Int","4"},            
                        {"ADName","NVarChar","100"},            
                        {"ImgUrl","NVarChar","255"},            
                        {"ImgWidth","Int","4"},            
                        {"ImgHeight","Int","4"},            
                        {"FlashWmode","Int","4"},            
                        {"ADIntro","NText","8000"},            
                        {"LinkUrl","NVarChar","255"},            
                        {"LinkTarget","Int","4"},            
                        {"LinkAlt","NVarChar","255"},            
                        {"Priority","Int","4"},            
                        {"Setting","NText","8000"},            
                        {"CountView","Bit","1"},            
                        {"Views","Int","4"},            
                        {"CountClick","Bit","1"},            
                        {"Clicks","Int","4"},            
                        {"Passed","Bit","1"},            
                        {"OverdueDate","DateTime","8"},            
                        {"ImgUrl1","NVarChar","255"},            
                        {"ImgHeight1","Int","4"},            
                        {"ImgWidth1","Int","4"},            
                        {"LinkUrl1","NVarChar","255"},            
                        {"LinkTarget1","Int","4"},            
                        {"LinkAlt1","NVarChar","255"},            
                        {"ADIntro1","NText","8000"},            
                        {"FlashWmode1","Int","4"}            
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Advertisement model = this;
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ADID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.ADType;
            sp[3].Value = model.ADName;
            sp[4].Value = model.ImgUrl;
            sp[5].Value = model.ImgWidth;
            sp[6].Value = model.ImgHeight;
            sp[7].Value = model.FlashWmode;
            sp[8].Value = model.ADIntro;
            sp[9].Value = model.LinkUrl;
            sp[10].Value = model.LinkTarget;
            sp[11].Value = model.LinkAlt;
            sp[12].Value = model.Priority;
            sp[13].Value = model.Setting;
            sp[14].Value = model.CountView;
            sp[15].Value = model.Views;
            sp[16].Value = model.CountClick;
            sp[17].Value = model.Clicks;
            sp[18].Value = model.Passed;
            sp[19].Value = model.OverdueDate;
            sp[20].Value = model.ImgUrl1;
            sp[21].Value = model.ImgHeight1;
            sp[22].Value = model.ImgWidth1;
            sp[23].Value = model.LinkUrl1;
            sp[24].Value = model.LinkTarget1;
            sp[25].Value = model.LinkAlt1;
            sp[26].Value = model.ADIntro1;
            sp[27].Value = model.FlashWmode1;
            return sp;
        }
        public M_Advertisement GetModelFromReader(DbDataReader rdr)
        {
            M_Advertisement model = new M_Advertisement();
            model.ADID = Convert.ToInt32(rdr["ADID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.ADType = ConvertToInt(rdr["ADType"]);
            model.ADName = ConverToStr(rdr["ADName"]);
            model.ImgUrl = ConverToStr(rdr["ImgUrl"]);
            model.ImgWidth = ConvertToInt(rdr["ImgWidth"]);
            model.ImgHeight = ConvertToInt(rdr["ImgHeight"]);
            model.FlashWmode = ConvertToInt(rdr["FlashWmode"]);
            model.ADIntro = ConverToStr(rdr["ADIntro"]);
            model.LinkUrl = ConverToStr(rdr["LinkUrl"]);
            model.LinkTarget = ConvertToInt(rdr["LinkTarget"]);
            model.LinkAlt = ConverToStr(rdr["LinkAlt"]);
            model.Priority = ConvertToInt(rdr["Priority"]);
            model.Setting = ConverToStr(rdr["Setting"]);
            model.CountView = Convert.ToBoolean(rdr["CountView"]);
            model.Views = ConvertToInt(rdr["Views"]);
            model.CountClick = ConverToBool(rdr["CountClick"]);
            model.Clicks = ConvertToInt(rdr["Clicks"]);
            model.Passed = ConverToBool(rdr["Passed"]);
            model.OverdueDate = ConvertToDate(rdr["OverdueDate"]);
            model.ImgUrl1 = ConverToStr(rdr["ImgUrl1"]);
            model.ImgHeight1 = ConvertToInt(rdr["ImgHeight1"]);
            model.ImgWidth1 = ConvertToInt(rdr["ImgWidth1"]);
            model.LinkUrl1 = ConverToStr(rdr["LinkUrl1"]);
            model.LinkTarget1 = ConvertToInt(rdr["LinkTarget1"]);
            model.LinkAlt1 = ConverToStr(rdr["LinkAlt1"]);
            model.ADIntro1 = ConverToStr(rdr["ADIntro1"]);
            model.FlashWmode1 = ConvertToInt(rdr["FlashWmode1"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}