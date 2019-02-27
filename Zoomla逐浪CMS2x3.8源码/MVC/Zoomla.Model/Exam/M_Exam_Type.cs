using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Exam_Type:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 类型ID
        /// </summary>
        public int t_id{get;set;}
        /// <summary>
        /// 题型名称
        /// </summary>
        public string t_name {get;set;}
        /// <summary>
        /// 类型：单选,多选,操作,判断，填空，语音,问答,组合
        /// </summary>
        public int t_type{get;set;}
        /// <summary>
        /// 说明
        /// </summary>
        public string t_remark {get;set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime t_createtime{get;set;}
        /// <summary>
        /// 创建用户
        /// </summary>
        public int t_creatuser{get;set;}
        #endregion

        #region 构造函数
        public M_Exam_Type()
        {
        }

        public M_Exam_Type
        (
            int t_id,
            string t_name,
            int t_type,
            string t_remark,
            DateTime t_createtime,
            int t_creatuser
        )
        {
            this.t_id = t_id;
            this.t_name = t_name;
            this.t_type = t_type;
            this.t_remark = t_remark;
            this.t_createtime = t_createtime;
            this.t_creatuser = t_creatuser;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Questions_TypeList()
        {
            string[] Tablelist = { "t_id", "t_name", "t_type", "t_remark", "t_createtime", "t_creatuser" };
            return Tablelist;
        }
        #endregion

        public override string PK { get { return "t_id"; } }
        public override string TbName { get { return "ZL_Exam_Type"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"t_id","Int","4"},
                                  {"t_name","NVarChar","50"},
                                  {"t_type","Int","4"},
                                  {"t_remark","NVarChar","1000"},
                                  {"t_createtime","DateTime","8"},
                                  {"t_creatuser","Int","4"},

                                 };
            return Tablelist;
        }
        public  override SqlParameter[] GetParameters()
        {
            M_Exam_Type model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.t_id;
            sp[1].Value = model.t_name;
            sp[2].Value = model.t_type;
            sp[3].Value = model.t_remark;
            sp[4].Value = model.t_createtime;
            sp[5].Value = model.t_creatuser;
            return sp;
        }

        public  M_Exam_Type GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_Type model = new M_Exam_Type();
            model.t_id = Convert.ToInt32(rdr["t_id"]);
            model.t_name = rdr["t_name"].ToString();
            model.t_type = Convert.ToInt32(rdr["t_type"]);
            model.t_remark = rdr["t_remark"].ToString();
            model.t_createtime = Convert.ToDateTime(rdr["t_createtime"]);
            model.t_creatuser = Convert.ToInt32(rdr["t_creatuser"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
   
}
