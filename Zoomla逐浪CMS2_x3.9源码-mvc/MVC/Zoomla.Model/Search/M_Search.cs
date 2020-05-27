using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Search : M_Base
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 链接类型：1为后台链接，2为会员链接
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 块大小：1大，2中，3小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 支持移动：1为支持，0为不支持
        /// </summary>
        public int Mobile { get; set; }

        /// <summary>
        /// 链接打开方式 0为原窗口，1为新窗口，2为父窗口，3为全局
        /// </summary>
        public int OpenType { get; set; }
        /// <summary>
        /// 状态：1为启用，2为关闭
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 文件与路径
        /// </summary>
        public string FlieUrl { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Ico { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 0:后台,3:会员
        /// </summary>
        public int LinkState { get; set; }
        /// <summary>
        /// 用户组ids
        /// </summary>
        public string UserGroup { get; set; }
        public int AdminID { get; set; }
        /// <summary>
        /// 是否推荐：0为不推荐   1为推荐
        /// </summary>
        public int EliteLevel { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }
        public override string TbName { get { return "ZL_Search"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"name","NVarChar","255"},
                                  {"type","Int","4"},
                                  {"OpenType","Int","4"},
                                  {"state","Int","4"},
                                  {"linkState","Int","4"},
                                  {"AdminID","Int","4"},
                                  {"time","DateTime","8"},
                                  {"fileUrl","NVarChar","200"},
                                  {"ico","NVarChar","200"},
                                  {"Size","Int","4"},
                                  {"Mobile","Int","4"},
                                  {"UserGroup","VarChar","8000"},
                                  {"EliteLevel","Int","4"},
                                  {"OrderID","Int","4"}
                              };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Search model = this;
            if (model.Time <= DateTime.MinValue) { model.Time = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.Name;
            sp[2].Value = model.Type;
            sp[3].Value = model.OpenType;
            sp[4].Value = model.State;
            sp[5].Value = model.LinkState;
            sp[6].Value = model.AdminID;
            sp[7].Value = model.Time;
            sp[8].Value = model.FlieUrl;
            sp[9].Value = model.Ico;
            sp[10].Value = model.Size;
            sp[11].Value = model.Mobile;
            sp[12].Value = model.UserGroup;
            sp[13].Value = model.EliteLevel;
            sp[14].Value = model.OrderID;

            return sp;
        }
        public M_Search GetModelFromReader(SqlDataReader rdr)
        {
            M_Search model = new M_Search();
            model.Id = Convert.ToInt32(rdr["id"]);
            model.Name = ConverToStr(rdr["name"]);
            model.Type = ConvertToInt(rdr["type"]);
            model.OpenType = ConvertToInt(rdr["OpenType"]);
            model.State = ConvertToInt(rdr["state"]);
            model.LinkState = ConvertToInt(rdr["linkState"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.Time = ConvertToDate(rdr["time"]);
            model.FlieUrl = ConverToStr(rdr["fileUrl"]);
            model.Ico = ConverToStr(rdr["ico"]);
            model.Size = ConvertToInt(rdr["Size"]);
            model.Mobile = ConvertToInt(rdr["Mobile"]);
            model.UserGroup = ConverToStr(rdr["UserGroup"]);
            model.EliteLevel = ConvertToInt(rdr["EliteLevel"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            rdr.Close();
            return model;
        }
    }
}