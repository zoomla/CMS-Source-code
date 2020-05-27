using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Data.Common;
namespace ZoomLa.Model
{
    public class M_GuestBookCate : M_Base
    {
        /// <summary>
        /// 普通栏目,贴吧栏目
        /// </summary>
        public enum TypeEnum { Cate, PostBar, }
        public M_GuestBookCate()
        {
            this.CateID = 0;
            this.CateName = "";
            this.PermiBit = "";
            this.IsNull = false;
            this.NeedLog = 0;
            this.ParentID = 0;
        }
        public M_GuestBookCate(bool value)
        {
            this.IsNull = value;
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CateID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 0:版块,1:分类(不显示贴子)
        /// </summary>
        public string PermiBit { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull { get; private set; }
        /// <summary>
        /// 0:匿名可发贴等,1:必须登录
        /// </summary>
        public int NeedLog { get; set; }
        public int GType { get; set; }

        public string BarImage { get; set; }

        public string Desc { get; set; }

        public string BarInfo { get; set; }
        public int OrderID { get; set; }
        public int ParentID { get; set; }
        /// <summary>
        /// 发贴权限,同于NeedLog
        /// </summary>
        public int PostAuth { get; set; }
        /// <summary>
        /// 吧主的IDS
        /// </summary>
        public string BarOwner { get; set; }
        //是否本吧吧主
        public bool IsBarOwner(int uid)
        {
            return (uid > 0 && !string.IsNullOrWhiteSpace(BarOwner) && BarOwner.Split(',').Where(p => p.Equals(uid.ToString())).ToArray().Length > 0);
        }
        /// <summary>
        /// 贴吧状态 1,普通 2,贴子需要审核(不显示)3,贴子需要审核(显示)
        /// </summary>
        public int Status { get; set; }
        //发贴积分
        public int SendScore { get; set; }
        //回贴积分
        public int ReplyScore { get; set; }
        //压缩图片大小
        public int ZipImgSize { get; set; }
        //是分享到能力中心
        public int IsPlat { get; set; }
        public override string PK { get { return "Cateid"; } }
        public override string TbName { get { return "ZL_Guestcate"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Cateid","Int","4"},
                                  {"Catename","NVarChar","255"},
                                  {"Permibit","NVarChar","255"},
                                  {"NeedLog","Int","4"},
                                  {"GType","Int","4"},
                                  {"BarImage","NVarChar","255"},
                                  {"Desc","NVarChar","500"},
                                  {"BarInfo","NVarChar","255"},
                                  {"OrderID","Int","4"},
                                  {"ParentID","Int","4"},
                                  {"BarOwner","VarChar","500"},
                                  {"PostAuth","Int","4"},
                                  {"Status","Int","4"},
                                  {"SendScore","Int","4"},
                                  {"ReplyScore","Int","4"},
                                  {"ZipImgSize","Int","4"},
                                  {"IsPlat","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            var model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.CateID;
            sp[1].Value = model.CateName;
            sp[2].Value = model.PermiBit;
            sp[3].Value = model.NeedLog;
            sp[4].Value = model.GType;
            sp[5].Value = model.BarImage;
            sp[6].Value = model.Desc;
            sp[7].Value = model.BarInfo;
            sp[8].Value = model.OrderID;
            sp[9].Value = model.ParentID;
            sp[10].Value = model.BarOwner;
            sp[11].Value = model.PostAuth;
            sp[12].Value = model.Status;
            sp[13].Value = model.SendScore;
            sp[14].Value = model.ReplyScore;
            sp[15].Value = model.ZipImgSize;
            sp[16].Value = model.IsPlat;
            return sp;
        }
        public M_GuestBookCate GetModelFromReader(DbDataReader rdr)
        {
            M_GuestBookCate model = new M_GuestBookCate();
            model.CateID = Convert.ToInt32(rdr["CateID"]);
            model.CateName = rdr["CateName"].ToString();
            model.PermiBit = rdr["PermiBit"].ToString();
            model.NeedLog = Convert.ToInt32(rdr["NeedLog"]);
            model.GType = Convert.ToInt32(rdr["GType"] == DBNull.Value ? "0" : rdr["GType"]);
            model.BarImage = rdr["BarImage"].ToString();
            model.Desc = rdr["Desc"].ToString();
            model.BarInfo = rdr["BarInfo"].ToString();
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.ParentID = ConvertToInt(rdr["ParentID"]);
            model.BarOwner = rdr["BarOwner"] == DBNull.Value ? "" : rdr["BarOwner"].ToString();
            model.PostAuth = rdr["PostAuth"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["PostAuth"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.SendScore = ConvertToInt(rdr["SendScore"]);
            model.ReplyScore = ConvertToInt(rdr["ReplyScore"]);
            model.ZipImgSize = ConvertToInt(rdr["ZipImgSize"]);
            model.IsPlat = ConvertToInt(rdr["IsPlat"]);
            rdr.Close();
            return model;
        }
    }
}