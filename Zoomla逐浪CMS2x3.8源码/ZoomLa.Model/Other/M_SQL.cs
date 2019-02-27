using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
   public class M_SQL:M_Base
    { 
        public M_SQL()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ID { get; set; }

       /// <summary>
       /// 插件名称
       /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        
       /// <summary>
       /// 项目单位
       /// </summary>
       /// 
        public string Unit { get; set; }

        /// <summary>
       /// 项目图标
       /// </summary>
       /// 
        public string Icon { get; set; }
       
        /// <summary>
       /// 项目描述
       /// </summary>
       /// 
        public string Explain { get; set; }
       
       /// <summary>
        /// 可执行次数:0次执行|1次|无限次
       /// </summary>
       /// 
        public int RunNum { get; set; }

       /// <summary>
        /// 关联负责人,会员
       /// </summary>
       /// 
        public string UserID { get; set; }

       /// <summary>
        /// 按钮名称默认[提交报表]
       /// </summary>
       /// 
        public string BtnName { get; set; }

       /// <summary>
       /// 附加文件
       /// </summary>
       /// 
        public string SqlUrl { get; set; }

       /// <summary>
        /// 自动计划,多少时间执行一次脚本
       /// </summary>
       /// 
        public string RunTime { get; set; } 

        public override string TbName { get { return "ZL_SQL"; } }

        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TagName","NVarChar","50"},
                                  {"TableName","NVarChar","50"},
                                  {"Unit","NVarChar","50"},
                                  {"Icon","NVarChar","50"},
                                  {"UserID","NVarChar","50"},
                                  {"BtnName","NVarChar","50"},
                                  {"SqlUrl","NVarChar","50"},
                                  {"RunTime","NVarChar","50"},
                                  {"RunNum","Int","4"},
                                  {"Explain","NVarChar","255"}
              };
            return Tablelist;
        }
        public  SqlParameter[] GetParameters(M_SQL model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TagName; 
            sp[2].Value = model.TableName;
            sp[3].Value = model.Unit;
            sp[4].Value = model.Icon;
            sp[5].Value = model.UserID;
            sp[6].Value = model.BtnName;
            sp[7].Value = model.SqlUrl;
            sp[8].Value = model.RunTime;
            sp[9].Value = model.RunNum;
            sp[10].Value = model.Explain;
            return sp;
        }
        public  M_SQL GetModelFromReader(SqlDataReader rdr)
        {

            M_SQL model = new M_SQL();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TagName = rdr["TagName"].ToString();
            model.TableName = rdr["TableName"].ToString();
            model.Unit = rdr["Unit"].ToString();
            model.Icon = rdr["Icon"].ToString();
            model.UserID = rdr["UserID"].ToString();
            model.BtnName = rdr["BtnName"].ToString();
            model.SqlUrl = rdr["SqlUrl"].ToString();
            model.RunNum = Convert.ToInt32(rdr["RunNum"]);
            model.RunTime = rdr["RunTime"].ToString();
            model.Explain = rdr["Explain"].ToString();
            rdr.Close();
            return model;
        }
    }
}