using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    public class M_User_BlogStyle : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string StyleName { get; set; }
        public string Author { get; set; }
        /// <summary>
        /// 模板缩略图
        /// </summary>
        public string StylePic { get; set; }
        public DateTime Addtime { get; set; }
        public int StyleState { get; set; }
        /// <summary>
        /// 首页虚拟路径
        /// </summary>
        public string UserIndexStyle { get; set; }
        public string LogStyle { get; set; }
        public string LogShowStyle { get; set; }
        public string PhotoStyle { get; set; }
        public string PhotoShowStyle { get; set; }
        public string PicShowStyle { get; set; }
        public string GroupStyle { get; set; }
        public string GroupShowStyle { get; set; }
        public string GroupTopicShow { get; set; }

        public override string TbName { get { return "ZL_Sns_BlogStyleTable"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"StyleName","VarChar","50"},
        		        		{"Author","VarChar","50"},
        		        		{"StylePic","VarChar","255"},
        		        		{"Addtime","DateTime","8"},
        		        		{"StyleState","Int","4"},
        		        		{"UserIndexStyle","VarChar","255"},
        		        		{"LogStyle","VarChar","255"},
        		        		{"LogShowStyle","VarChar","255"},
        		        		{"PhotoStyle","VarChar","255"},
        		        		{"PhotoShowStyle","VarChar","255"},
        		        		{"PicShowStyle","VarChar","255"},
        		        		{"GroupStyle","VarChar","255"},
        		        		{"GroupShowStyle","VarChar","255"},
        		        		{"GroupTopicShow","VarChar","255"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_User_BlogStyle model = this;
            if (model.Addtime <= DateTime.MinValue) { model.Addtime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.StyleName;
            sp[2].Value = model.Author;
            sp[3].Value = model.StylePic;
            sp[4].Value = model.Addtime;
            sp[5].Value = model.StyleState;
            sp[6].Value = model.UserIndexStyle;
            sp[7].Value = model.LogStyle;
            sp[8].Value = model.LogShowStyle;
            sp[9].Value = model.PhotoStyle;
            sp[10].Value = model.PhotoShowStyle;
            sp[11].Value = model.PicShowStyle;
            sp[12].Value = model.GroupStyle;
            sp[13].Value = model.GroupShowStyle;
            sp[14].Value = model.GroupTopicShow;
            return sp;
        }
        public M_User_BlogStyle GetModelFromReader(DbDataReader rdr)
        {
            M_User_BlogStyle model = new M_User_BlogStyle();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.StyleName = ConverToStr(rdr["StyleName"]);
            model.Author = ConverToStr(rdr["Author"]);
            model.StylePic = ConverToStr(rdr["StylePic"]);
            model.Addtime = ConvertToDate(rdr["Addtime"]);
            model.StyleState = ConvertToInt(rdr["StyleState"]);
            model.UserIndexStyle = ConverToStr(rdr["UserIndexStyle"]);
            model.LogStyle = ConverToStr(rdr["LogStyle"]);
            model.LogShowStyle = ConverToStr(rdr["LogShowStyle"]);
            model.PhotoStyle = ConverToStr(rdr["PhotoStyle"]);
            model.PhotoShowStyle = ConverToStr(rdr["PhotoShowStyle"]);
            model.PicShowStyle = ConverToStr(rdr["PicShowStyle"]);
            model.GroupStyle = ConverToStr(rdr["GroupStyle"]);
            model.GroupShowStyle = ConverToStr(rdr["GroupShowStyle"]);
            model.GroupTopicShow = ConverToStr(rdr["GroupTopicShow"]);
            rdr.Close();
            return model;
        }
    }
}
