using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_IServer: M_Base
    {
 
        public M_IServer() { }
        #region 属性定义
        /// <summary>
        /// 主键，自动增长
        /// </summary>
        public int QuestionId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 问题优先级
        /// </summary>
        public string Priority
        {
            get;
            set;
        }

        /// <summary>
        /// 问题类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 已读次数
        /// </summary>
        public int ReadCount
        {
            get;
            set;
        }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubTime
        {
            get;
            set;
        }

        /// <summary>
        /// 问题状态
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime SolveTime
        {
            get;
            set;
        }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string Path
        {
            get;
            set;
        }
        /// <summary>
        /// 问题来源
        /// </summary>
        public string Root
        {
            get;
            set;
        }
        /// <summary>
        /// 来源附件信息
        /// </summary>
        public string RootInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否阅读
        /// </summary>
        private bool m_IsNull = false;
        public M_IServer(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
        public DateTime RequestTime
        {
            get;
            set;
        }
        /// <summary>
        /// 订单类型(0-非订单)
        /// </summary>
        public int OrderType
        {
            get;
            set;
        }
        #endregion
        public override string PK { get { return "QuestionId"; } }
        public override string TbName { get { return "ZL_IServer"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"QuestionId","Int","4"},
                                  {"UserId","Int","4"},
                                  {"Title","NVarChar","100"},
                                  {"Content","NText","1000"},
                                  {"Priority","NVarChar","50"},
                                  {"Type","NVarChar","50"},
                                  {"ReadCount","Int","4"},
                                  {"SubTime","DateTime","8"},
                                  {"State","NVarChar","50"},
                                  {"SolveTime","DateTime","8"},
                                  {"path","NVarChar","1000"},
                                  {"Root","NVarChar","50"},
                                  {"RootInfo","NVarChar","50"},
                                  {"RequestTime","DateTime","8"},
                                  {"OrderType","Int","4"}
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
            M_IServer model = this;
            if (model.RequestTime <= DateTime.MinValue) model.RequestTime = DateTime.Now.AddDays(1);
            if (model.SubTime <= DateTime.MinValue) model.SubTime = DateTime.Now;
            if (model.SolveTime <= DateTime.MinValue) model.SolveTime = DateTime.Now;
            string[,] strArr = FieldList();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.QuestionId;
            sp[1].Value = model.UserId;
            sp[2].Value = model.Title;
            sp[3].Value = model.Content;
            sp[4].Value = model.Priority;
            sp[5].Value = model.Type;
            sp[6].Value = model.ReadCount;
            sp[7].Value = model.SubTime;
            sp[8].Value = model.State;
            sp[9].Value = model.SolveTime;
            sp[10].Value = model.Path;
            sp[11].Value = model.Root;
            sp[12].Value = model.RootInfo;
            sp[13].Value = model.RequestTime;
            sp[14].Value = model.OrderType;
            return sp;
        }

        public M_IServer GetModelFromReader(SqlDataReader rdr)
        {
            M_IServer model = new M_IServer();
            model.QuestionId = ConvertToInt(rdr["QuestionId"]);
            model.UserId = ConvertToInt(rdr["UserId"]);
            model.Title = rdr["Title"].ToString();
            model.Content = rdr["Content"].ToString();
            model.Priority = rdr["Priority"].ToString();
            model.Type = rdr["Type"].ToString();
            model.ReadCount = ConvertToInt(rdr["ReadCount"]);
            model.SubTime = ConvertToDate(rdr["SubTime"]);
            model.State = rdr["State"].ToString();
            model.SolveTime = ConvertToDate(rdr["SolveTime"]);
            model.Path = rdr["path"].ToString();
            model.Root = rdr["Root"].ToString();
            model.RootInfo = rdr["RootInfo"].ToString();
            model.RequestTime = ConvertToDate(rdr["RequestTime"]);
            model.OrderType = ConvertToInt(rdr["OrderType"]);
            rdr.Dispose();
            return model;
        }
    }
}