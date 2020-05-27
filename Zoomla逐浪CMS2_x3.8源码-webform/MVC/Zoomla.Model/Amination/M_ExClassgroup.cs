using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ExClassgroup:M_Base
    {
        #region 构造函数
        public M_ExClassgroup()
        {
        }

        public M_ExClassgroup
        (
            int GroupID,
            string Regulationame,
            string Regulation,
            int Ratednumber,
            int Actualnumber,
            DateTime Setuptime,
            DateTime Endtime,
            int CourseID,
            double ShiPrice,
            double LinPrice,
            int CourseHour,
            int Presented,
            int isCou,
            int ClassID
        )
        {
            this.GroupID = GroupID;
            this.Regulationame = Regulationame;
            this.Regulation = Regulation;
            this.Ratednumber = Ratednumber;
            this.Actualnumber = Actualnumber;
            this.Setuptime = Setuptime;
            this.Endtime = Endtime;
            this.CourseID = CourseID;
            this.ShiPrice = ShiPrice;
            this.LinPrice = LinPrice;
            this.CourseHour = CourseHour;
            this.Presented = Presented;
            this.isCou = isCou;
            this.ClassID = ClassID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExClassgroupList()
        {
            string[] Tablelist = { "GroupID", "Regulationame", "Regulation", "Ratednumber", "Actualnumber", "Setuptime", "Endtime", "CourseID", "ShiPrice", "LinPrice", "CourseHour", "Presented", "isCou", "ClassID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 班级ID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string Regulationame { get; set; }
        /// <summary>
        /// 班级负责人
        /// </summary>
        public string Regulation { get; set; }
        /// <summary>
        /// 额定人数
        /// </summary>
        public int Ratednumber { get; set; }
        /// <summary>
        /// 实际人数
        /// </summary>
        public int Actualnumber { get; set; }
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime Setuptime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime Endtime { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public double ShiPrice { get; set; }
        /// <summary>
        /// 优惠价
        /// </summary>
        public double LinPrice { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public int CourseHour { get; set; }
        /// <summary>
        /// 订购此班级是否赠送此课程(免费)
        /// </summary>
        public int Presented { get; set; }
        /// <summary>
        /// 是否打折
        /// </summary>
        public int isCou { get; set; }
        /// <summary>
        /// 课程分类ID
        /// </summary>
        public int ClassID { get; set; }
        public string Speaker { get; set; }

        #endregion
        public override string PK { get { return "GroupID"; } }
        public override string TbName { get { return "ZL_ExClassgroup"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"GroupID","Int","4"},
                                  {"Regulationame","NVarChar","50"},
                                  {"Regulation","NVarChar","255"}, 
                                  {"Ratednumber","Int","4"},
                                  {"Actualnumber","Int","4"},
                                  {"Setuptime","DateTime","8"}, 
                                  {"Endtime","DateTime","8"},
                                  {"CourseID","Int","4"},
                                  {"ShiPrice","Money","100"},
                                  {"LinPrice","Money","100"},
                                  {"CourseHour","Int","4"}, 
                                  {"Presented","Int","4"}, 
                                  {"isCou","Int","4"}, 
                                  {"Speaker","NVarChar","50"}, 
                                  {"ClassID","Int","4"}
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
        public M_ExClassgroup GetModelFromReader(SqlDataReader rdr)
        {
            M_ExClassgroup model = new M_ExClassgroup();
            model.GroupID =Convert.ToInt32(rdr["GroupID"]);
            model.Regulationame = ConverToStr(rdr["Regulationame"]);
            model.Regulation = ConverToStr(rdr["Regulation"]);
            model.Ratednumber = ConvertToInt(rdr["Ratednumber"]);
            model.Actualnumber = ConvertToInt(rdr["Actualnumber"]);
            model.Setuptime = ConvertToDate(rdr["Setuptime"]);
            model.Endtime = ConvertToDate(rdr["Endtime"]);
            model.CourseID = ConvertToInt(rdr["CourseID"]);
            model.ShiPrice = ConverToDouble(rdr["ShiPrice"]);
            model.LinPrice = ConverToDouble(rdr["LinPrice"]);
            model.CourseHour = ConvertToInt(rdr["CourseHour"]);
            model.Presented = ConvertToInt(rdr["Presented"]);
            model.isCou = ConvertToInt(rdr["isCou"]);
            model.Speaker = ConverToStr(rdr["Speaker"]);
            model.ClassID = ConvertToInt(rdr["ClassID"]);
            rdr.Close();
            
            rdr.Dispose();
            
            return model;
        }
    }
}