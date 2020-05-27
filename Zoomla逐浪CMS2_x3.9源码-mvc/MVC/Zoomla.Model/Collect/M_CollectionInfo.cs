using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_CollectionInfo:M_Base
    {
        /// <summary>
        /// 
        /// </summary>
        public int C_IID
        {
            get;
            set;
        }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModeID
        {
            get;
            set;
        }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 采集ID
        /// </summary>
        public int CollID
        {
            get;
            set;
        }
        /// <summary>
        /// 原网页地址
        /// </summary>
        public string OldUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 新地址
        /// </summary>
        public string NewUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        public override string PK { get { return "C_IID"; } }
        public override string TbName { get { return "ZL_CollectionInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"C_IID","Int","4"},
                                  {"ModeID","Int","4"},
                                  {"NodeID","Int","4"}, 
                                  {"ItemID","Int","4"},
                                  {"CollID","Int","4"},
                                  {"OldUrl","NChar","500"}, 
                                  {"NewUrl","NChar","500"},
                                  {"State","Int","4"},
                                  {"AddTime","NChar","50"}, 
                                  {"Remark","NChar","500"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_CollectionInfo model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.C_IID;
            sp[1].Value = model.ModeID;
            sp[2].Value = model.NodeID;
            sp[3].Value = model.ItemID;
            sp[4].Value = model.CollID;
            sp[5].Value = model.OldUrl;
            sp[6].Value = model.NewUrl;
            sp[7].Value = model.State;
            sp[8].Value = model.AddTime;
            sp[9].Value = model.Remark;
           

            return sp;
        }
        public M_CollectionInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_CollectionInfo model = new M_CollectionInfo();
            model.C_IID = Convert.ToInt32(rdr["C_IID"]);
            model.ModeID = Convert.ToInt32(rdr["ModeID"]);
            model.NodeID = Convert.ToInt32(rdr["NodeID"]);
            model.ItemID = Convert.ToInt32(rdr["ItemID"]);
            model.CollID = Convert.ToInt32(rdr["CollID"]);
            model.OldUrl = ConverToStr(rdr["OldUrl"]);
            model.NewUrl = ConverToStr(rdr["NewUrl"]);
            model.State = ConvertToInt(rdr["State"]);
            model.AddTime = ConverToStr(rdr["AddTime"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            rdr.Close();
            return model;
        }
    }
}


