using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_Plat_CompDoc : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 文档名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 容量,1.24G,35M等格式(预留,后期将值写入字段,避免每次重复获取)
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 文档的虚拟路径,不存UploadFiles
        /// </summary>
        public string VPath { get; set; }
        /// <summary>
        /// 标识文档类型 1:公司文档,2:个人文档,3:部门文档,4:项目文档,代表可见范围
        /// </summary>
        public int DocType { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public override string TbName { get { return "ZL_Plat_CompDoc"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Name","NVarChar","200"},
                                {"FileSize","VarChar","50"},
        		        		{"VPath","NVarChar","200"},
        		        		{"DocType","Int","4"},
        		        		{"UserID","Int","4"},
                                {"CreateTime","DateTime","8"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_CompDoc model=this;
            EmptyDeal(); 
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.FileSize;
            sp[3].Value = model.VPath;
            sp[4].Value = model.DocType;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CreateTime;
            return sp;
        }
        public M_Plat_CompDoc GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_CompDoc model = new M_Plat_CompDoc();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = rdr["Name"].ToString();
            model.FileSize = rdr["FileSize"].ToString();
            model.VPath = rdr["VPath"].ToString();
            model.DocType = Convert.ToInt32(rdr["DocType"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal() 
        {
            if (CreateTime.Year < 1901) { CreateTime = DateTime.Now; }
        }
    }
}
