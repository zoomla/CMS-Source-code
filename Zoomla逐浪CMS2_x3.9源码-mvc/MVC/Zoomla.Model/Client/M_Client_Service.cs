using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Client_Service : M_Base
    {
        #region 构造函数
        public M_Client_Service()
        {
        }

        public M_Client_Service
        (
            int Flow,
            string ServiceTitle,
            string SelectClient,
            string ContacterList,
            string Processor,
            string ServiceType,
            string ServiceMode,
            DateTime ServiceTime,
            string TakeTime,
            string Point,
            string ServiceContent,
            string ServiceResult,
            string Feedback,
            string Remark,
            DateTime Add_date
        )
        {
            this.Flow = Flow;
            this.ServiceTitle = ServiceTitle;
            this.SelectClient = SelectClient;
            this.ContacterList = ContacterList;
            this.Processor = Processor;
            this.ServiceType = ServiceType;
            this.ServiceMode = ServiceMode;
            this.ServiceTime = ServiceTime;
            this.TakeTime = TakeTime;
            this.Point = Point;
            this.ServiceContent = ServiceContent;
            this.ServiceResult = ServiceResult;
            this.Feedback = Feedback;
            this.Remark = Remark;
            this.Add_date = Add_date;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Client_ServiceList()
        {
            string[] Tablelist = { "Flow", "ServiceTitle", "SelectClient", "ContacterList", "Processor", "ServiceType", "ServiceMode", "ServiceTime", "TakeTime", "Point", "ServiceContent", "ServiceResult", "Feedback", "Remark", "Add_date" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int Flow { get; set; }
        /// <summary>
        /// 服务主题
        /// </summary>
        public string ServiceTitle { get; set; }
        /// <summary>
        /// 对应客户
        /// </summary>
        public string SelectClient { get; set; }
        /// <summary>
        /// 客户方联系人
        /// </summary>
        public string ContacterList { get; set; }
        /// <summary>
        /// 服务人员
        /// </summary>
        public string Processor { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 服务方式
        /// </summary>
        public string ServiceMode { get; set; }
        /// <summary>
        /// 服务时间
        /// </summary>
        public DateTime ServiceTime { get; set; }
        /// <summary>
        /// 花费时间
        /// </summary>
        public string TakeTime { get; set; }
        /// <summary>
        /// 服务积分
        /// </summary>
        public string Point { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string ServiceContent { get; set; }
        /// <summary>
        /// 服务结果
        /// </summary>
        public string ServiceResult { get; set; }
        /// <summary>
        /// 客户反馈
        /// </summary>
        public string Feedback { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Add_date { get; set; }
        #endregion

        public override string PK { get { return "Flow"; } }
        public override string TbName { get { return "ZL_Client_Service"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Flow","Int","4"},
                                  {"ServiceTitle","NChar","50"},
                                  {"SelectClient","NChar","50"},
                                  {"ContacterList","NChar","50"},
                                  {"Processor","NChar","50"},
                                  {"ServiceType","NChar","50"},
                                  {"ServiceMode","NChar","50"},
                                  {"ServiceTime","DateTime","8"},
                                  {"TakeTime","NChar","50"},
                                  {"Point","VarChar","5"},
                                  {"ServiceContent","NChar","50"},
                                  {"ServiceResult","NChar","50"},
                                  {"Feedback","NChar","50"},
                                  {"Remark","NChar","100"},
                                  {"Add_date","DateTime","8"}
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

        public SqlParameter[] GetParameters(M_Client_Service model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.Flow;
            sp[1].Value = model.ServiceTitle;
            sp[2].Value = model.SelectClient;
            sp[3].Value = model.ContacterList;
            sp[4].Value = model.Processor;
            sp[5].Value = model.ServiceType;
            sp[6].Value = model.ServiceMode;
            sp[7].Value = model.ServiceTime;
            sp[8].Value = model.TakeTime;
            sp[9].Value = model.Point;
            sp[10].Value = model.ServiceContent;
            sp[11].Value = model.ServiceResult;
            sp[12].Value = model.Feedback;
            sp[13].Value = model.Remark;
            sp[14].Value = model.Add_date;
            return sp;
        }
        public M_Client_Service GetModelFromReader(SqlDataReader rdr)
        {
            M_Client_Service model = new M_Client_Service();
            model.Flow = Convert.ToInt32(rdr["Flow"]);
            model.ServiceTitle = ConverToStr(rdr["ServiceTitle"]);
            model.SelectClient = ConverToStr(rdr["SelectClient"]);
            model.ContacterList = ConverToStr(rdr["ContacterList"]);
            model.Processor = ConverToStr(rdr["Processor"]);
            model.ServiceType = ConverToStr(rdr["ServiceType"]);
            model.ServiceMode = ConverToStr(rdr["ServiceMode"]);
            model.ServiceTime = ConvertToDate(rdr["ServiceTime"]);
            model.TakeTime = ConverToStr(rdr["TakeTime"]);
            model.Point = ConverToStr(rdr["Point"]);
            model.ServiceContent = ConverToStr(rdr["ServiceContent"]);
            model.ServiceResult = ConverToStr(rdr["ServiceResult"]);
            model.Feedback = ConverToStr(rdr["Feedback"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.Add_date = ConvertToDate(rdr["Add_date"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}