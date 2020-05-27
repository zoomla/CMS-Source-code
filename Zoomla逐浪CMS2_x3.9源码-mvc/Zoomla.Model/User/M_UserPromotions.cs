using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_UserPromotions:M_Base
    {
        #region 属性定义
        /// <summary>
        /// 权限ID
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 用户组ID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID { get; set; }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModelID { get; set; }
        /// <summary>
        /// 查看
        /// </summary>
        public int look { get; set; }
        /// <summary>
        /// 下载
        /// </summary>
        public int Down { get; set; }
        /// <summary>
        /// 引用
        /// </summary>
        public int quote { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        public int addTo { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        public int Modify { get; set; }
        /// <summary>
        /// 仅当前节点
        /// </summary>
        public int Columns { get; set; }
        /// <summary>
        /// 允许评论
        /// </summary>
        public int Comments { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public int Deleted { get; set; }
        #endregion
        public override string TbName { get { return "ZL_UserPromotions"; } }
        public override string PK { get { return "pid"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"pid","Int","4"},
                                  {"GroupID","Int","4"},
                                  {"NodeID","Int","4"},
                                  {"ModelID","Int","4"},
                                  {"look","Int","4"},
                                  {"addTo","Int","4"},
                                  {"Modify","Int","4"},
                                  {"Columns","Int","4"},
                                  {"Comments","Int","4"},
                                  {"Deleted","Int","4"},
                                  {"quote","Int","4"},
                                  {"Down","Int","4"}
                                  
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserPromotions model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.pid;
            sp[1].Value = model.GroupID;
            sp[2].Value = model.NodeID;
            sp[3].Value = model.ModelID;
            sp[4].Value = model.look;
            sp[5].Value = model.addTo;
            sp[6].Value = model.Modify;
            sp[7].Value = model.Columns;
            sp[8].Value = model.Comments;
            sp[9].Value = model.Deleted;
            sp[10].Value = model.quote;
            sp[11].Value = model.Down;
            return sp;
        }
        public M_UserPromotions GetModelFromReader(SqlDataReader rdr)
        {
            M_UserPromotions model = new M_UserPromotions();
            model.pid = Convert.ToInt32(rdr["pid"]);
            model.GroupID = ConvertToInt(rdr["GroupID"]);
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.look = ConvertToInt(rdr["look"]);
            model.addTo = ConvertToInt(rdr["addTo"]);
            model.Modify = ConvertToInt(rdr["Modify"]);
            model.Columns = ConvertToInt(rdr["Columns"]);
            model.Comments = ConvertToInt(rdr["Comments"]);
            model.Deleted = ConvertToInt(rdr["Deleted"]);
            model.quote = ConvertToInt(rdr["quote"]);
            model.Down = ConvertToInt(rdr["down"]);
            rdr.Close();
            return model;
        }
    }
}