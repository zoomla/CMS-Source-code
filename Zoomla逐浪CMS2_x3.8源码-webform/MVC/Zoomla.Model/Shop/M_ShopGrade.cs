using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    public class M_ShopGrade:M_Base
    {
        #region 公开属性
        /// <summary>
        /// 等级ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// 等级图片
        /// </summary>
        public string Gradeimg { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int CommentNum { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string OtherName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Istrue { get; set; }
        /// <summary>
        /// 等级类型 0-购物等级,1-卖家等级,2-商户等级
        /// </summary>
        public int GradeType { get; set; }
        /// <summary>
        /// 图片数量
        /// </summary>
        public int Imgnum { get; set; }
        #endregion
        public M_ShopGrade()
        {
            this.GradeName = string.Empty;
            this.Gradeimg = string.Empty;
            this.OtherName = string.Empty;
        }
        public override string TbName { get { return "ZL_ShopGrade"; } }
        public override string PK { get { return ""; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"GradeName","NVarChar","255"},
                                  {"Gradeimg","NVarChar","255"},
                                  {"CommentNum","Int","4"}, 
                                  {"OtherName","NVarChar","255"},
                                  {"Istrue","Bit","4"},
                                  {"GradeType","Int","4"}, 
                                  {"Imgnum","Int","4"}
                                 };
            return Tablelist;
        }
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
            M_ShopGrade model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.GradeName;
            sp[2].Value = model.Gradeimg;
            sp[3].Value = model.CommentNum;
            sp[4].Value = model.OtherName;
            sp[5].Value = model.Istrue;
            sp[6].Value = model.GradeType;
            sp[7].Value = model.Imgnum;
            return sp;
        }
        public M_ShopGrade GetModelFromReader(SqlDataReader rdr)
        {
            M_ShopGrade model = new M_ShopGrade();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.GradeName = ConverToStr(rdr["GradeName"]);
            model.Gradeimg = ConverToStr(rdr["Gradeimg"]);
            model.CommentNum = ConvertToInt(rdr["CommentNum"]);
            model.OtherName = ConverToStr(rdr["OtherName"]);
            model.Istrue = ConverToBool(rdr["Istrue"]);
            model.GradeType = ConvertToInt(rdr["GradeType"]);
            model.Imgnum = ConvertToInt(rdr["Imgnum"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}