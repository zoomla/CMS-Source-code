using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_PointGrounp:M_Base
    {
        #region 构造函数
        public M_PointGrounp()
        {
        }

        public M_PointGrounp
        (
            int ID,
            double PointVal,
            string Remark,
            DateTime AddTime,
            string GroupName,
            string ImgUrl
        )
        {
            this.ID = ID;
            this.PointVal = PointVal;
            this.Remark = Remark;
            this.AddTime = AddTime;
            this.GroupName = GroupName;
            this.ImgUrl = ImgUrl;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] PointGrounpList()
        {
            string[] Tablelist = { "ID", "PointVal", "Remark", "AddTime", "GroupName", "ImgUrl" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 积分等级
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 积分数
        /// </summary>
        public double PointVal { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; }
        #endregion

        public override string TbName { get { return "ZL_PointGrounp"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"PointVal","Float","16"},
                                  {"Remark","NVarChar","500"},
                                  {"AddTime","DateTime","8"}, 
                                  {"GroupName","NVarChar","50"}, 
                                  {"ImgUrl","NVarChar","255"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_PointGrounp model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.PointVal;
            sp[2].Value = model.Remark;
            sp[3].Value = model.AddTime;
            sp[4].Value = model.GroupName;
            sp[5].Value = model.ImgUrl;
            return sp;
        }

        public  M_PointGrounp GetModelFromReader(SqlDataReader rdr)
        {
            M_PointGrounp model = new M_PointGrounp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.PointVal = ConvertToInt(rdr["PointVal"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.GroupName = ConverToStr(rdr["GroupName"]);
            model.ImgUrl = ConverToStr(rdr["ImgUrl"]);
            rdr.Close();
            return model;
        }
    }
}


