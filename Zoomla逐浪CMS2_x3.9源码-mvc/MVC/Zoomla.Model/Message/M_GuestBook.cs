using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_GuestBook:M_Base
    {
        public M_GuestBook()
        {
            this.GID = 0;
            this.ParentID = 0;
            this.CateID = 0;
            this.UserID = 0;
            this.Title = "";
            this.TContent = "";
            this.IsNull = false;
            this.IP = "";
        }
        public M_GuestBook(bool value)
        {
            this.IsNull = value;
        }
        /// <summary>
        /// 帖子ID
        /// </summary>
        public int GID { get; set; }
        /// <summary>
        /// 所属帖子ID 标题贴ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 0:未审核,1:已审核,-1回收站
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 审核ID
        /// </summary>
        public int CateID { get; set; }
        /// <summary>
        /// 发表人ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string TContent { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime GDate { get; set; }
        /// <summary>
        /// 是否空实例
        /// </summary>
        public bool IsNull { get; private set; }
        public string IP { get; set; }
        public override string PK { get { return "GID"; } }
        public override string TbName { get { return "ZL_Guestbook"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"GID","Int","4"},
                                  {"ParentID","Int","4"},
                                  {"CateID","Int","4"},
                                  {"UserID","Int","4"}, 
                                  {"Title","NVarChar","200"},
                                  {"TContent","Text","400"}, 
                                  {"GDate","DateTime","8"},
                                  {"IP","NVarChar","50"},
                                  {"Status","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public  SqlParameter[] GetParameters(M_GuestBook model)
        {
            if (model.GDate <= DateTime.MinValue) { model.GDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.GID;
            sp[1].Value = model.ParentID;
            sp[2].Value = model.CateID;
            sp[3].Value = model.UserID;
            sp[4].Value = model.Title;
            sp[5].Value = model.TContent;
            sp[6].Value = model.GDate;
            sp[7].Value = model.IP;
            sp[8].Value = model.Status;
            return sp;
        }

        public  M_GuestBook GetModelFromReader(SqlDataReader rdr)
        {
            M_GuestBook model = new M_GuestBook();
            model.GID = Convert.ToInt32(rdr["GID"]);
            model.ParentID = ConvertToInt(rdr["ParentID"]);
            model.CateID = ConvertToInt(rdr["CateID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.TContent = ConverToStr(rdr["TContent"]);
            model.GDate = ConvertToDate(rdr["GDate"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.Status =ConvertToInt(rdr["Status"]);
            rdr.Close();
            return model;
        }
    }
    
}
