using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model.User.Develop
{
    public class M_Zone_Node:M_Base
    {
        /// <summary>
        /// 表ZL_Zone_Node的标识字段
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID { get; set; }
        /// <summary>
        /// 标签所属页面的页面ID
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// 标签的ID属性值
        /// </summary>
        public string LabelID { get; set; }
        /// <summary>
        /// 标记div的style值
        /// </summary>
        public string style { get; set; }
        /// <summary>
        /// div层中内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// div的定位类型
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 设置元素的堆叠顺序
        /// </summary>
        public string Z_index { get; set; }
        /// <summary>
        /// 背景属性
        /// </summary>
        public string Background { get; set; }
        /// <summary>
        /// 边框设置
        /// </summary>
        public string Border { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public string Font_size { get; set; }
        /// <summary>
        /// 规定元素应该生成的框的类型
        /// </summary>
        public string Display { get; set; }
        /// <summary>
        /// 设置当对象的内容超过其指定高度及宽度时如何管理内容的属性
        /// </summary>
        public string Overflow { get; set; }
        public int mtop { get; set; }
        public int mleft { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public override string TbName { get { return "ZL_Zone_Node"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"NodeID","Int","4"},
                                  {"PageID","Int","4"},
                                  {"LabelID","NVarChar","1000"},
                                  {"style","NVarChar","1000"},
                                  {"Content","NVarChar","1000"},
                                  {"Position","NVarChar","1000"},
                                  {"Z_index","NVarChar","1000"},
                                  {"Background","NVarChar","1000"},
                                  {"Border","NVarChar","1000"},
                                  {"Font_size","NVarChar","1000"},
                                  {"Display","NVarChar","1000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Zone_Node model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.NodeID;
            sp[3].Value = model.PageID;
            sp[4].Value = model.LabelID;
            sp[5].Value = model.style;
            sp[6].Value = model.Content;
            sp[7].Value = model.Position;
            sp[8].Value = model.Z_index;
            sp[9].Value = model.Background;
            sp[10].Value = model.Border;
            sp[11].Value = model.Font_size;
            sp[12].Value = model.Display; 
            return sp;
        }
        public M_Zone_Node GetModelFromReader(SqlDataReader rdr)
        {
            M_Zone_Node model = new M_Zone_Node();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.NodeID = Convert.ToInt32(rdr["NodeID"]);
            model.PageID = ConvertToInt(rdr["PageID"]);
            model.LabelID = ConverToStr(rdr["LabelID"]);
            model.style = ConverToStr(rdr["style"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.Position = ConverToStr(rdr["Position"]);
            model.Z_index = ConverToStr(rdr["Z_index"]);
            model.Background = ConverToStr(rdr["Background"]);
            model.Border = ConverToStr(rdr["Border"]);
            model.Font_size = ConverToStr(rdr["Font_size"]);
            model.Display = ConverToStr(rdr["Display"]); 
            rdr.Close();
            return model;
        }
    }
}