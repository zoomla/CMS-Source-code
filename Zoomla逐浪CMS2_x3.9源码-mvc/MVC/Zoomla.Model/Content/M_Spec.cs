using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    /// 专题Model
    /// </summary>
    public class M_Spec:M_Base
    {
        #region 字段定义
        /// <summary>
        /// 专题ID
        /// </summary>
        public int SpecID { get; set; }
        /// <summary>
        /// 专题名称
        /// </summary>
        public string SpecName { get; set; }
        /// <summary>
        /// 专题标识
        /// </summary>
        public string SpecDir { get; set; }
        /// <summary>
        /// 专题说明
        /// </summary>
        public string SpecDesc { get; set; }
        /// <summary>
        /// 是否在新窗口打开
        /// </summary>
        public bool OpenNew { get; set; }
        /// <summary>
        /// 所属专题类别ID
        /// </summary>
        public int SpecCate { get; set; }
        /// <summary>
        /// 扩展名index
        /// </summary>
        public int ListFileExt { get; set; }
        /// <summary>
        /// 列表页模板
        /// </summary>
        public string ListTemplate { get; set; }
        /// <summary>
        /// 列表页文件名规则
        /// </summary>
        public int ListFileRule { get; set; }
        /// <summary>
        /// 是否是空对象
        /// </summary>
        public bool IsNull { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string SpecDescriptive { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string SpecKeyword { get; set; }
        /// <summary>
        /// 专题图片
        /// </summary>
        public string SpecPicUrl { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public string SpecTips { get; set; }
        /// <summary>
        /// 父级节点
        /// </summary>
        public int Pid { get; set; }
        public DateTime CDate { get; set; }
        public string CUser { get; set; }
        public DateTime EditDate { get; set; }
        public int OrderID { get; set; }
        #endregion
        #region 构造函数
        public M_Spec()
        {

        }
        public M_Spec(bool value)
            : this()
        {
            this.IsNull = value;
        }
        #endregion
        public override string TbName { get { return "ZL_Special"; } }
        public override string PK { get { return "SpecID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"SpecID","Int","4"},
                                  {"SpecName","NVarChar","50"},
                                  {"SpecDir","NVarChar","50"},
                                  {"SpecDesc","NVarChar","255"}, 
                                  {"OpenType","Bit","4"},
                                  {"SpecCate","Int","4"},
                                  {"ListFileExt","Int","4"},
                                  {"ListTemplate","NVarChar","255"},
                                  {"ListFileRule","Int","4"}, 
                                  {"SpecDescriptive","NVarChar","255"},
                                  {"SpecKeyword","NVarChar","255"},
                                  {"SpecPicUrl","NVarChar","255"},
                                  {"SpecTips","NVarChar","255"},
                                  {"Pid","Int","4"},
                                  {"CDate","DateTime","8" },
                                  {"CUser","NVarChar","255"},
                                  {"EditDate","DateTime","8" },
                                  {"OrderID","Int","4" }
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Spec model = this;
            SqlParameter[] sp = GetSP();
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            model.EditDate = DateTime.Now;
            sp[0].Value = model.SpecID;
            sp[1].Value = model.SpecName;
            sp[2].Value = model.SpecDir;
            sp[3].Value = model.SpecDesc;
            sp[4].Value = model.OpenNew;
            sp[5].Value = model.SpecCate;
            sp[6].Value = model.ListFileExt;
            sp[7].Value = model.ListTemplate;
            sp[8].Value = model.ListFileRule;
            sp[9].Value = model.SpecDescriptive;
            sp[10].Value = model.SpecKeyword;
            sp[11].Value = model.SpecPicUrl;
            sp[12].Value = model.SpecTips;
            sp[13].Value = model.Pid;
            sp[14].Value = model.CDate;
            sp[15].Value = model.CUser;
            sp[16].Value = model.EditDate;
            sp[17].Value = model.OrderID;
            return sp;
        }
        public  M_Spec GetModelFromReader(SqlDataReader rdr)
        {
            M_Spec model = new M_Spec();
            model.SpecID = Convert.ToInt32(rdr["SpecID"]);
            model.SpecName = rdr["SpecName"].ToString();
            model.SpecDir = rdr["SpecDir"].ToString();
            model.SpecDesc = rdr["SpecDesc"].ToString();
            model.OpenNew = ConverToBool(rdr["OpenType"]);
            model.SpecCate = ConvertToInt(rdr["SpecCate"]);
            model.ListFileExt = ConvertToInt(rdr["ListFileExt"]);
            model.ListTemplate = rdr["ListTemplate"].ToString();
            model.ListFileRule = ConvertToInt(rdr["ListFileRule"]);
            model.SpecDescriptive = rdr["SpecDescriptive"].ToString();
            model.SpecKeyword = rdr["SpecKeyword"].ToString();
            model.SpecPicUrl = rdr["SpecPicUrl"].ToString();
            model.SpecTips = rdr["SpecTips"].ToString();
            model.Pid =ConvertToInt(rdr["Pid"]);
            try
            {
                model.CDate = ConvertToDate(rdr["CDate"]);
                model.EditDate = ConvertToDate(rdr["EditDate"]);
                model.CUser = ConverToStr(rdr["CUser"]);
                model.OrderID = ConvertToInt(rdr["OrderID"]);
            }
            catch { model.CDate = DateTime.Now; model.EditDate = DateTime.Now; }
            rdr.Close();
            return model;
        }
    }
}