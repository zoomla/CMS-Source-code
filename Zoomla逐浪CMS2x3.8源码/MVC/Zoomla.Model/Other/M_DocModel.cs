using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_DocModel : M_Base
    {

        public int ID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string DocName { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 会员组权限
        /// </summary>
        /// 
        public string AllowGroup { get; set; }

        public DateTime AddTime { get; set; }
        /// <summary>
        /// 状态1为模板组,2为模板组下模组
        /// </summary>
        /// 
        public int Status { get; set; }
        /// <summary>
        /// 表单格式
        /// </summary>
        /// 
        public string Form { get; set; }

        /// <summary>
        /// 模版地址
        /// </summary>
        /// 
        public string TemUrl { get; set; }
        /// <summary>
        /// 所属模板组
        /// </summary>
        public string ParentID { get; set; }

        public override string TbName { get { return "ZL_DocModel"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"DocName","NVarChar","255"},
                                  {"AddUser","NVarChar","50"},
                                  {"AddTime","DateTime","8"},
                                  {"AllowGroup","NVarChar","255"},
                                  {"Status","Int","4"},
                                  {"Form","NVarChar","255"},
                                  {"TemUrl","NVarChar","255"},
                                  {"ParentID","VarChar","50"}
              };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_DocModel model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.DocName;
            sp[2].Value = model.AddUser;
            sp[3].Value = model.AddTime;
            sp[4].Value = model.AllowGroup;
            sp[5].Value = model.Status;
            sp[6].Value = model.Form;
            sp[7].Value = model.TemUrl;
            sp[8].Value = model.ParentID;
            return sp;
        }
        public M_DocModel GetModelFromReader(SqlDataReader rdr)
        {
            M_DocModel model = new M_DocModel();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.DocName = rdr["DocName"].ToString();
            model.AddUser = rdr["AddUser"].ToString();
            model.AddTime = Convert.ToDateTime(rdr["AddTime"].ToString());
            model.AllowGroup = rdr["AllowGroup"].ToString();
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.Form = rdr["Form"].ToString();
            model.TemUrl = rdr["TemUrl"].ToString();
            model.ParentID = rdr["ParentID"].ToString();
            rdr.Close();
            return model;
        }
    }
}