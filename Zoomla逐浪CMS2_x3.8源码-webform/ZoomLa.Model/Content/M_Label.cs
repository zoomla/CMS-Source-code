using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using ZoomLa.Model.CreateJS;
namespace ZoomLa.Model
{
    public class M_Label:M_Base
    {
        public override string PK { get { return "LabelID"; } }
        public override string TbName { get { return ""; } }
        public int LabelID { get; set; }
        /// <summary>
        /// 站群用
        /// </summary>
        public int LabelAddUser { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LableName { get { return _labelName.Replace(" ", ""); } set { _labelName = value; } }
        private string _labelName = "", _labelCate = "";
        /// <summary>
        /// 标签分类
        /// </summary>
        public string LabelCate { get { return _labelCate.Replace(" ", ""); } set { _labelCate = value; } }
        /// <summary>
        /// 标签类型
        /// </summary>
        public int LableType { get; set; }
        /// <summary>
        /// 标签说明
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 标签参数
        /// </summary>
        public string Param { get; set; }
        /// <summary>
        /// 查询的表
        /// </summary>
        public string LabelTable { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string LabelField { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string LabelWhere { get; set; }
        /// <summary>
        /// 查询排序
        /// </summary>
        public string LabelOrder { get; set; }
        /// <summary>
        /// 标签内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 查询数量
        /// </summary>
        public string LabelCount { get; set; }
        /// <summary>
        /// 站群用
        /// </summary>
        public int LabelNodeID { get; set; }
        /// <summary>
        /// 判断模式
        /// </summary>
        public string Modeltypeinfo { get; set; }
        /// <summary>
        /// 计算方式(循环、累加)
        /// </summary>
        public string addroot { get; set; }
        /// <summary>
        /// 判断逻辑
        /// </summary>
        public string setroot { get; set; }
        /// <summary>
        /// 判断值
        /// </summary>
        public string Modelvalue { get; set; }
        /// <summary>
        /// 参数标签
        /// </summary>
        public string Valueroot { get; set; }
        /// <summary>
        /// 是否开启(1-开启 0-关闭)
        /// </summary>
        public int IsOpen { get; set; }
        /// <summary>
        /// 不满足条件的内容
        /// </summary>
        public string FalseContent { get; set; }
        /// <summary>
        /// 数据源信息:主数据源|主表,次数据源|次表
        /// </summary>
        public string DataSourceType { get; set; }
        /// <summary>
        /// (Disuse),暂用于二附院存结构名,以后取消
        /// </summary>
        public string ConnectString { get; set; }
        /// <summary>
        /// 存储过程名
        /// </summary>
        public string ProceName { get; set; }
        /// <summary>
        /// 存储过程参数 参数名:值,参数名:值,改为存储标签详情
        /// </summary>
        public string ProceParam { get; set; }
        /// <summary>
        /// 是否存储过程 True:是,False:不是
        /// </summary>
        public bool IsProce 
        {
            get 
            {
                return !(string.IsNullOrEmpty(ProceName)||string.IsNullOrEmpty(ProceName.Trim()));
            }
        }
        public bool IsNull { get; private set; }
        /// <summary>
        /// 仅用于B_CreateHtml中提示
        /// </summary>
        public string ErrorMsg { get; set; }
        public override string[,] FieldList()
        {
            return null;
        }
        public static M_Label GetInfoFromDataTable(XmlNode node,DataTable dt)
        {
            M_Label info = new M_Label();
            if (dt == null || dt.Rows.Count < 1) { return info; }
            DataRow dr = dt.Rows[0];
            info.LabelID = info.ConvertToInt(node["LabelID"].InnerText);
            info.LableName = node["LabelName"].InnerText;
            info.LableType = info.ConvertToInt(node["LabelType"].InnerText);
            info.LabelCate = node["LabelCate"].InnerText;
            info.Desc = dr["LabelDesc"].ToString();
            info.Param = dr["LabelParam"].ToString();
            info.LabelTable = dr["LabelTable"].ToString();
            info.LabelField = dr["LabelField"].ToString();
            info.LabelWhere = dr["LabelWhere"].ToString();
            info.LabelOrder = dr["LabelOrder"].ToString();
            info.Content = dr["LabelContent"].ToString();
            info.LabelCount = dr["LabelCount"].ToString();
            info.LabelAddUser = info.ConvertToInt(GetFromDR(dt, "LabelAddUser", "0"));
            info.LabelNodeID = info.ConvertToInt(GetFromDR(dt, "LabelNodeID", "0"));
            info.Modeltypeinfo = GetFromDR(dt, "Modeltypeinfo");
            info.addroot = GetFromDR(dt, "addroot");
            info.setroot = GetFromDR(dt, "setroot");
            info.Modelvalue = GetFromDR(dt, "Modelvalue");
            info.Valueroot = GetFromDR(dt, "Valueroot");
            info.IsOpen = info.ConvertToInt(GetFromDR(dt, "IsOpen"));
            info.FalseContent = GetFromDR(dt, "FalseContent");
            info.DataSourceType = GetFromDR(dt, "DataSourceType");
            info.ConnectString = GetFromDR(dt, "ConnectString");
            info.ProceName = GetFromDR(dt, "ProceName");
            info.ProceParam = GetFromDR(dt, "ProceParam");
            return info;
        }
        public static string GetFromDR(DataTable dt, string field, string def="")
        {
            return dt.Columns.IndexOf(field) == -1 ? def : dt.Rows[0][field].ToString();
        }
        //---------------------------------------------对信息再解析
        private List<M_API_Param> _paramList = new List<M_API_Param>();
        /// <summary>
        /// 解析Param并返回模型列表
        /// </summary>
        public List<M_API_Param> ParamList
        {
            get
            {
                if (_paramList.Count > 0) { return _paramList; } 
                if (!string.IsNullOrEmpty(Param))
                {
                    string[] paramArr = Param.Split('|');
                    foreach (string param in paramArr)
                    {
                        string[] valArr = param.Split(',');
                        M_API_Param model = new M_API_Param();
                        model.name = valArr[0];
                        model.defval = valArr[1];
                        model.type = valArr[2];
                        model.desc = valArr[3];
                        _paramList.Add(model);
                    }
                }
                return _paramList;
            }
        }
        public M_Label()
        {
            this.LableName = string.Empty;
            this.LabelCate = string.Empty;
            this.Desc = string.Empty;
            this.Param = string.Empty;
            this.LabelTable = string.Empty;
            this.LabelField = string.Empty;
            this.LabelWhere = string.Empty;
            this.LabelOrder = string.Empty;
            this.Content = string.Empty;
            this.LabelCount = string.Empty;
            this.Modeltypeinfo = string.Empty;
            this.addroot = string.Empty;
            this.setroot = string.Empty;
            this.Modelvalue = string.Empty;
            this.Valueroot = string.Empty;
            this.FalseContent = string.Empty;
            this.DataSourceType = string.Empty;
            this.ConnectString = string.Empty;
        }
        public M_Label(bool flag):this()
        {
            this.IsNull = flag;
        }
    }
    public class M_SubLabel 
    {
        /// <summary>
        /// 只包含表名
        /// </summary>
        public string PureT1 { get { if (string.IsNullOrEmpty(T1)) { return ""; } else return T1.Split('.')[2]; } }
        public string PureT2 { get { if (string.IsNullOrEmpty(T2)) { return ""; } else return T2.Split('.')[2]; } }
        /// <summary>
        /// 主表名,ZoomlaCMS.dbo.ZL_CommonModel
        /// </summary>
        public string T1 { get; set; }
        /// <summary>
        /// 次表名
        /// </summary>
        public string T2 { get; set; }
        public string JoinType { get; set; }
        public string OnField1 { get; set; }
        public string OnField2 { get; set; }
    }
}