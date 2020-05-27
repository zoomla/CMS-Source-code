using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model.User
{
    public class M_Usermagazine:M_Base
    {
        public int ID { get; set; }
        //用户ID
        public int UserID { get; set; }
        //杂志名称
        public string MagName { get; set; }
        //封面
        public string MagPic { get; set; }
        //创建时间
        public DateTime CreatTime { get;set; }
        //简介
        public string Synopsis { get; set; }
        //杂志所在路径
        public string Url { get; set; }

        /// <summary>
        /// 音乐地址
        /// </summary>
        public string MusicUrl { get; set; }

        /// <summary>
        /// 是否开放游客浏览
        /// </summary>
        public int IsOpen { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string Backgroud { get; set; }

        /// <summary>
        /// 背景图片是否循环
        /// </summary>
        public int IsRepeat { get; set; }
        /// <summary>
        /// 杂志分类
        /// </summary>
        public string Magclass { get; set; }
          /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] UserMagazine()
        {
            string[] Tablelist = { "ID", "UserID", "MagName", "MagPic", "CreatTime", "Synopsis", "Url", "MusicUrl", "IsOpen", "Backgroud", "IsRepeat","Magclass" };
            return Tablelist;
        }
        public override string TbName { get { return "ZL_Magazine"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"MagName","NVarChar","50"}, 
                                  {"MagPic","NVarChar","1000"},
                                  {"CreatTime","DateTime","8"},
                                  {"Synopsis","NText","1000"},
                                  {"Url","NVarChar","100"} ,
                                  {"MusicUrl","NVarChar","100"},
                                  {"IsOpen","Int","4"},
                                  {"Backgroud","NVarChar","100"},
                                  {"IsRepeat","Int","4"},
                                  {"Magclass","NVarChar","50"}
                                 };
            
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Usermagazine model = this;
            if (model.CreatTime <= DateTime.MinValue) { model.CreatTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.MagName;
            sp[3].Value = model.MagPic;
            sp[4].Value = model.CreatTime;
            sp[5].Value = model.Synopsis;
            sp[6].Value = model.Url;
            sp[7].Value = model.MusicUrl;
            sp[8].Value = model.IsOpen;
            sp[9].Value = model.Backgroud;
            sp[10].Value = model.IsRepeat;
            sp[11].Value = model.Magclass;
            return sp;
        }
        public M_Usermagazine GetModelFromReader(SqlDataReader rdr)
        {
            M_Usermagazine model = new M_Usermagazine();

            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID =Convert.ToInt32(rdr["UserID"]);
            model.MagName = ConverToStr(rdr["MagName"]);
            model.MagPic = ConverToStr(rdr["MagPic"]);
            model.CreatTime = ConvertToDate(rdr["CreatTime"]);
            model.Synopsis = ConverToStr(rdr["Synopsis"]);
            model.Url = ConverToStr(rdr["Url"]);
            model.MusicUrl = ConverToStr(rdr["MusicUrl"]);
            model.IsOpen = ConvertToInt(rdr["IsOpen"]);
            model.Backgroud = ConverToStr(rdr["Backgroud"]);
            model.IsRepeat = ConvertToInt(rdr["IsRepeat"]);
            model.Magclass = ConverToStr(rdr["Magclass"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
