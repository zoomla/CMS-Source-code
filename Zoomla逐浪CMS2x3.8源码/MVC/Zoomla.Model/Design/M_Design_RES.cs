using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_RES : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 资源类型 music|img
        /// </summary>
        public string ZType { get; set; }
        /// <summary>
        /// 用途 bk_pc|bk_h5
        /// </summary>
        public string Useage { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string PreviewImg { get; set; }
        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VPath { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        public int ZStatus { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 用途(前端筛选)
        /// </summary>
        public string use { get; set; }
        /// <summary>
        /// 功能
        /// </summary>
        public string fun { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public string style { get; set; }
        //--------------------------------
        public string UseArr = "招聘,简历,表格,照片,节日,商品,介绍,图文,名片";
        public string FunArr = "背景,图集,表单,互动,特效,事件";
        public string StyleArr = "纯色,黑白,绚丽,怀旧,现代,小清新,扁平化,中国风,欧美风";
        public string TextStyle = "电商,婚庆,节日,商务,生日,字符";
        public string ShapeStyle = "常用,边框,便签,标签,地图,封面,国风,花框,花纹,剪纸,交通,手绘,数据,图腾,相框,折纸";
        public string IconStyle = "LOGO,IT企业,办公,互联网,节日,旅游,农业,其他行业,人物,商务,生日,体育,物品,餐饮,动物,其他企业";
        public override string TbName { get { return "ZL_Design_RES"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                              { "ID","Int","4" },
                              { "ztype","VarChar","50" },
                              { "useage","NVarChar","100" },
                              { "name","NVarChar","500" },
                              { "previewimg","NVarChar","1000" },
                              { "vpath","NVarChar","1000" },
                              { "cdate","DateTime","8" },
                              { "zstatus","Int","4" },
                              { "userid","Int","4" },
                              { "use","NVarChar","4000" },
                              { "fun","NVarChar","4000" },
                              { "style","NVarChar","4000" } 
            };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Design_RES model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ZType;
            sp[2].Value = model.Useage;
            sp[3].Value = model.Name;
            sp[4].Value = model.PreviewImg;
            sp[5].Value = model.VPath;
            sp[6].Value = model.CDate;
            sp[7].Value = model.ZStatus;
            sp[8].Value = model.UserID;
            sp[9].Value = model.use;
            sp[10].Value = model.fun;
            sp[11].Value = model.style;
            return sp;
        }
        public M_Design_RES GetModelFromReader(DbDataReader rdr)
        {
            M_Design_RES model = new M_Design_RES();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ZType = ConverToStr(rdr["ztype"]);
            model.Useage = ConverToStr(rdr["useage"]);
            model.Name = ConverToStr(rdr["name"]);
            model.PreviewImg = ConverToStr(rdr["previewimg"]);
            model.VPath = ConverToStr(rdr["vpath"]);
            model.CDate = ConvertToDate(rdr["cdate"]);
            model.ZStatus = ConvertToInt(rdr["zstatus"]);
            model.UserID = ConvertToInt(rdr["userid"]);
            model.use = ConverToStr(rdr["use"]);
            model.fun = ConverToStr(rdr["fun"]);
            model.style = ConverToStr(rdr["style"]);
            rdr.Close();
            return model;
        }
    }
}
