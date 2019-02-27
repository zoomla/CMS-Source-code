using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_NodeRole : M_Base
    {
        #region 定义字段
        public int pNid { get; set; }
        public int RN_ID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RID { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NID { get; set; }
        /// <summary>
        /// 查看
        /// </summary>
        public int look { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        public int Modify { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        public int addTo { get; set; }
        /// <summary>
        /// 当前栏目
        /// </summary>
        public int Columns { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public int Comments { get; set; }
        #endregion

        #region 构造函数
        public M_NodeRole()
        { }
        public M_NodeRole
        (
            int RN_ID,
            int RID,
            int NID,
            int look,
            int Modify,
            int State,
            int addTo,
            int Columns,
            int Comments,
            int pNid
        )
        {
            this.RN_ID = RN_ID;
            this.RID = RID;
            this.NID = NID;
            this.look = look;
            this.Modify = Modify;
            this.State = State;
            this.addTo = addTo;
            this.Columns = Columns;
            this.Comments = Comments;
            this.pNid = pNid;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] NodeRoleList()
        {
            string[] Tablelist = { "RN_ID", "RID", "NID", "look", "Modify", "State", "addTo", "Columns", "Comments", "pNid" };
            return Tablelist;
        }
        #endregion

        public override string PK { get { return "RN_ID"; } }
        public override string TbName { get { return "ZL_NodeRole"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"RN_ID","Int","4"},
                                  {"RID","Int","4"},
                                  {"NID","Int","4"},
                                  {"look","Int","4"},
                                  {"Modify","Int","4"},
                                  {"State","Int","4"},
                                  {"addTo","Int","4"},
                                  {"Columns","Int","4"},
                                  {"Comments","Int","4"},
                                  {"PNid","Int","4"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_NodeRole model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.RN_ID;
            sp[1].Value = model.RID;
            sp[2].Value = model.NID;
            sp[3].Value = model.look;
            sp[4].Value = model.Modify;
            sp[5].Value = model.State;
            sp[6].Value = model.addTo;
            sp[7].Value = model.Columns;
            sp[8].Value = model.Comments;
            sp[9].Value = model.pNid;
            return sp;
        }

        public M_NodeRole GetModelFromReader(SqlDataReader rdr)
        {
            M_NodeRole model = new M_NodeRole();
            model.RN_ID = Convert.ToInt32(rdr["RN_ID"]);
            model.RID = Convert.ToInt32(rdr["RID"]);
            model.NID = Convert.ToInt32(rdr["NID"]);
            model.look = Convert.ToInt32(rdr["look"]);
            model.Modify = Convert.ToInt32(rdr["Modify"]);
            model.State = Convert.ToInt32(rdr["State"]);
            model.addTo = Convert.ToInt32(rdr["addTo"]);
            model.Columns = Convert.ToInt32(rdr["Columns"]);
            model.Comments = Convert.ToInt32(rdr["Comments"]);
            model.pNid = Convert.ToInt32(rdr["PNid"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}