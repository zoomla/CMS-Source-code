using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_CollectionItem:M_Base
    {
        public int CItem_ID
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModeID
        {
            get;
            set;
        }
        /// <summary>
        /// 节点ID(多节点使用，隔开）
        /// </summary>
        public string NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 专题ID（多专题用，隔开）
        /// </summary>
        public string SpecialID
        {
            get;
            set;
        }
        /// <summary>
        /// 采集网站
        /// </summary>
        public string SiteName
        {
            get;
            set;
        }
        /// <summary>
        /// 采集URL
        /// </summary>
        public string CollUrl
        {
            get;
            set;
        }
        public string PureUrl
        {
            get 
            {
                int start = CollUrl.IndexOf("://")+3;
                return CollUrl.Substring(start);
            }
        }
        /// <summary>
        /// 编码选择
        /// </summary>
        public int CodinChoice
        {
            get;
            set;
        }
        /// <summary>
        /// 指定采集数量
        /// </summary>
        public int CollNum
        {
            get;
            set;
        }
        /// <summary>
        /// 采集排序
        /// </summary>
        public int CollOrder
        {
            get;
            set;
        }
        /// <summary>
        /// 采集简介
        /// </summary>
        public string CollContext
        {
            get;
            set;
        }
        /// <summary>
        /// 列表设置
        /// </summary>
        public string ListSettings
        {
            get;
            set;
        }
        /// <summary>
        /// 分页设置
        /// </summary>
        public string PageSettings
        {
            get;
            set;
        }
        /// <summary>
        /// 内容页采集设置
        /// </summary>
        public string InfoPageSettings
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最后采集时间
        /// </summary>
        public string LastTime
        {
            get;
            set;
        }
        /// <summary>
        /// 采集开关，1开始，2停止
        /// </summary>
        public int Switch
        {
            get;
            set;
        }
        /// <summary>
        /// 采集状态
        /// </summary>
        public int State
        {
            get;
            set;
        }

        public string LinkList { get; set; }
        public override string PK { get { return "CItem_ID"; } }
        public override string TbName { get { return "ZL_CollectionItem"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"CItem_ID","Int","4"},
                                  {"ItemName","NVarChar","500"},
                                  {"ModeID","Int","4"}, 
                                  {"NodeID","NVarChar","200"},
                                  {"SpecialID","NVarChar","200"},
                                  {"SiteName","NVarChar","100"}, 
                                  {"CollUrl","NVarChar","200"},
                                  {"CodinChoice","Int","4"},
                                  {"CollNum","Int","4"}, 
                                  {"CollOrder","Int","4"},
                                  {"CollContext","NVarChar","4000"},
                                  {"ListSettings","NVarChar","1000"}, 
                                  {"PageSettings","NVarChar","1000"}, 
                                  {"InfoPageSettings","NVarChar","4000"}, 
                                  {"AddTime","DateTime","8"}, 
                                  {"LastTime","NVarChar","50"}, 
                                  {"Switch","Int","4"}, 
                                  {"State","Int","4"},
                                  {"LinkList","NText","20000"}
                              };
            return Tablelist;
        }
        public string[,] GetField() { return new M_CollectionItem().FieldList(); }
        public override SqlParameter[] GetParameters()
        {
            M_CollectionItem model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.CItem_ID;
            sp[1].Value = model.ItemName;
            sp[2].Value = model.ModeID;
            sp[3].Value = model.NodeID;
            sp[4].Value = model.SpecialID;
            sp[5].Value = model.SiteName;
            sp[6].Value = model.CollUrl;
            sp[7].Value = model.CodinChoice;
            sp[8].Value = model.CollNum;
            sp[9].Value = model.CollOrder;
            sp[10].Value = model.CollContext;
            sp[11].Value = model.ListSettings;
            sp[12].Value = model.PageSettings;
            sp[13].Value = model.InfoPageSettings;
            sp[14].Value = model.AddTime;
            sp[15].Value = model.LastTime;
            sp[16].Value = model.Switch;
            sp[17].Value = model.State;
            sp[18].Value = model.LinkList;
            return sp;
        }
        public M_CollectionItem GetModelFromReader(SqlDataReader rdr)
        {
            M_CollectionItem model = new M_CollectionItem();
            model.CItem_ID = Convert.ToInt32(rdr["CItem_ID"]);
            model.ItemName = ConverToStr(rdr["ItemName"]);
            model.ModeID = ConvertToInt(rdr["ModeID"]);
            model.NodeID = ConverToStr(rdr["NodeID"]);
            model.SpecialID = ConverToStr(rdr["SpecialID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.CollUrl = ConverToStr(rdr["CollUrl"]);
            model.CodinChoice = ConvertToInt(rdr["CodinChoice"]);
            model.CollNum = ConvertToInt(rdr["CollNum"]);
            model.CollOrder = ConvertToInt(rdr["CollOrder"]);
            model.CollContext = ConverToStr(rdr["CollContext"]);
            model.ListSettings = ConverToStr(rdr["ListSettings"]);
            model.PageSettings = ConverToStr(rdr["PageSettings"]);
            model.InfoPageSettings = ConverToStr(rdr["InfoPageSettings"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.LastTime = ConverToStr(rdr["LastTime"]);
            model.Switch = ConvertToInt(rdr["Switch"]);
            model.State = ConvertToInt(rdr["State"]);
            model.LinkList = ConverToStr(rdr["LinkList"]);
            rdr.Close();
            return model;
        }
    }
}