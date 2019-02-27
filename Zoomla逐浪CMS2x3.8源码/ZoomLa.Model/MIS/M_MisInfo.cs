using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    /// 日程类型
    /// </summary>
   public class M_MisInfo:M_Base
    {
        #region 定义字段
            /// <summary>
            /// 
            /// </summary>
           public int ID { set; get; }
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { set; get; }
            /// <summary>
            /// 创建人
            /// </summary>
            public string Inputer { set; get; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { set; get; }
        
            /// <summary>
            /// 项目ID
            /// </summary>
            public int ProID { set; get; }
            /// <summary>
            /// 目标ID
            /// </summary>
            public int MID { set; get; }
            /// <summary>
            /// 类型：0记录、1小结、2今日计划、3备忘、4待办、5协商、6求助、 7汇报、8日志、 9消息 、10其他 
            /// </summary>
            public int Type{set;get;}
            /// <summary>
            /// 状态
            /// </summary>
            public int Status { set; get; }
            /// <summary>
            /// 描述
            /// </summary>
            public string Content { set; get; }
            /// <summary>
            /// 分享
            /// </summary>
            public string IsShare { set; get; }
            /// <summary>
            /// 提醒
            /// </summary>
            public string IsWarn { set; get; }
           /// <summary>
            /// 紧要程度:0不重要不紧急,1 重要,2 紧急 ,3重要且紧急
           /// </summary>
            public int Level { set; get; }
           /// <summary>
           /// 推荐
           /// </summary>
            public int IsElit { set; get; }
            #endregion
        #region 构造函数
        public M_MisInfo()
        {
            IsShare = string.Empty;
            IsWarn = string.Empty;
            Title = string.Empty;
            Content = string.Empty;
        } 
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MisInfo()
        {
            string[] Tablelist = { "ID", "Title", "Inputer", "CreateTime", "ProID", "MID", "Type","Status","IsShare","IsWarn", "Content","Level","IsElit"};
            return Tablelist;
        }
        #endregion 
        public override string TbName { get { return "ZL_MisInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Title","NVarChar","255"},
                                  {"Inputer","NVarChar","50"},
                                  {"CreateTime","DateTime","8"},
                                  {"ProID","Int","4"},
                                  {"MID","Int","4"},
                                  {"Type","Int","4"},
                                  {"Status","Int","4"},
                                  {"IsShare","NText","4000"},
                                  {"IsWarn","NText","4000"},
                                  {"Content","NText","4000"},
                                  {"Level","Int","4"},
                                  {"IsElit","Int","4"}
                              };
            return Tablelist; 
        }
        public override SqlParameter[] GetParameters()
        {
            M_MisInfo model = this;
            if (model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Title;
            sp[2].Value = model.Inputer;
            sp[3].Value = model.CreateTime;
            sp[4].Value = model.ProID;
            sp[5].Value = model.MID;
            sp[6].Value = model.Type;
            sp[7].Value = model.Status;
            sp[8].Value = model.IsShare;
            sp[9].Value = model.IsWarn;
            sp[10].Value = model.Content;
            sp[11].Value = model.Level;
            sp[12].Value = model.IsElit;
            return sp;
        }
        public M_MisInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_MisInfo model = new M_MisInfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Inputer =  ConverToStr(rdr["Inputer"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.MID = ConvertToInt(rdr["MID"]);
            model.Type = ConvertToInt(rdr["Type"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.IsShare = ConverToStr(rdr["IsShare"]);
            model.IsWarn = ConverToStr(rdr["IsWarn"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.Level = ConvertToInt(rdr["Level"]);
            model.IsElit = ConvertToInt(rdr["IsElit"]);
            if (!rdr.HasRows)
            {
                rdr.Close();
                rdr.Dispose(); 
            }
            return model;
        }
    }
}