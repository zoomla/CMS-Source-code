using System;
using System.Data;
using System.Configuration;
using System.Web;
using ZoomLa.IDAL;
using ZoomLa.DALFactory;
using ZoomLa.Model;
using ZoomLa.Common;
namespace ZoomLa.BLL
{
    /// <summary>
    /// B_Content 的摘要说明
    /// </summary>
    public class B_Content
    {
        private static readonly ID_CommonModel dal = IDal.CreateContent();

        public B_Content()
        {

        }
        /// <summary>
        /// 获取某栏目的内容列表
        /// </summary>
        /// <param name="NodelID"></param>
        /// <returns></returns>
        public DataTable GetContentList(int NodelID, string flag)
        {
            return dal.ContentList(NodelID, flag);
        }
        /// <summary>
        /// 获取指定用户输入的内容
        /// </summary>
        /// <param name="NodeID">栏目节点ID</param>
        /// <param name="flag">状态标志 表示未审核、审核、退稿、删除到回收站等状态</param>
        /// <param name="inputer">输入内容的用户名</param>
        /// <returns>数据表</returns>
        public DataTable ContentListUser(int NodeID, string flag, string inputer)
        {
            return dal.ContentListUser(NodeID, flag, inputer);
        }
        public int GetListCount(int NodeID, string falg)
        {
            return dal.GetListCount(NodeID, falg);
        }
        /// <summary>
        /// 读取回收站内的内容列表
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="CPage"></param>
        /// <returns></returns>
        public DataTable GetContentRecycle(int NodeID)
        {
            return dal.ContentRecycle(NodeID);
        }
        public DataTable ContentSearch(string filter, int PageSize, int CPage)
        {
            return dal.ContentSearch(filter, PageSize, CPage).Tables[1];
        }
        public int CountSearch(string filter)
        {
            return dal.CountSearch(filter);
        }
        /// <summary>
        /// 添加内容到数据库  返回新内容ID
        /// </summary>
        /// <param name="ContentDT">存储内容字段及其值的数据表</param>
        /// <param name="CommonData">存储内容公共数据的表</param>
        /// <returns>新内容ID</returns>
        public int AddContent(DataTable ContentDT, M_CommonData CommonData)
        {
            string FieldList = "";
            string FieldValue = "";
            foreach (DataRow dr in ContentDT.Rows)
            {
                if (string.IsNullOrEmpty(FieldList))
                {
                    FieldList = dr["FieldName"].ToString();
                }
                else
                {
                    FieldList = FieldList + "," + dr["FieldName"].ToString();
                }
                if (string.IsNullOrEmpty(FieldValue))
                    FieldValue = this.GetFieldValue(dr);
                else
                    FieldValue = FieldValue + "," + this.GetFieldValue(dr);
            }
            return dal.AddContent(FieldList, FieldValue, CommonData);
        }
        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="ContentDT"></param>
        /// <param name="CommonData"></param>
        public void UpdateContent(DataTable ContentDT, M_CommonData CommonData)
        {
            string FieldSet = "";
            foreach (DataRow dr in ContentDT.Rows)
            {
                if (string.IsNullOrEmpty(FieldSet))
                    FieldSet = this.GetFieldSet(dr);
                else
                    FieldSet = FieldSet + "," + this.GetFieldSet(dr);
            }
            dal.UpdateContent(FieldSet, CommonData);
        }
        /// <summary>
        /// 获取内容值
        /// </summary>
        /// <param name="GeneralID"></param>
        /// <returns></returns>
        public DataTable GetContent(int GeneralID)
        {
            B_ModelField bmodelfield = new B_ModelField();
            M_CommonData info = GetCommonData(GeneralID);
            //读取内容信息
            return dal.GetContent(info);
        }
        public bool MoveContent(string ContentIDS, int NodeID)
        {
            return dal.MoveContent(ContentIDS, NodeID);
        }
        /// <summary>
        /// 获取内容公共数据
        /// </summary>
        /// <param name="GeneralID"></param>
        /// <returns></returns>
        public M_CommonData GetCommonData(int GeneralID)
        {
            return dal.GetCommonData(GeneralID);
        }
        public string GetFieldValue(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            string result = "";
            switch (FieldType)
            {
                case "TextType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "OptionType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "ListBoxType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "DateType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultipleHtmlType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultipleTextType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "FileType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "PicType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "FileSize":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "ThumbField":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultiPicType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "OperatingType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "SuperLinkType":
                    result = "'" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MoneyType":
                    result = dr["FieldValue"].ToString();
                    break;
                case "BoolType":
                    result = dr["FieldValue"].ToString();
                    break;
                case "NumType":
                    result = dr["FieldValue"].ToString();
                    break;
            }
            return result;
        }
        public void SetDel(int GeneralID)
        {
            dal.SetDel(GeneralID);
        }
        public void SetAudit(int GeneralID)
        {
            dal.SetAudit(GeneralID);
        }
        public void Reset(int GeneralID)
        {
            dal.Reset(GeneralID);
        }
        public void ResetAll()
        {
            dal.ResetAll();
        }
        public void DelRecycle()
        {
            DataTable dt = dal.ContentRecycle(0);
            foreach (DataRow dr in dt.Rows)
            {
                M_CommonData info = dal.GetCommonData(DataConverter.CLng(dr["GeneralID"].ToString()));
                dal.DelContent(info);
            }
        }
        public void DelContent(int GeneralID)
        {
            M_CommonData info = dal.GetCommonData(GeneralID);
            dal.DelContent(info);
        }
        public DataTable GetCreateAllList()
        {
            return dal.GetCreateAllList();
        }
        public DataTable GetCreateIDList(int startID, int endID)
        {
            return dal.GetCreateIDList(startID, endID);
        }
        public DataTable GetCreateDateList(DateTime startTime, DateTime endTime)
        {
            return dal.GetCreateDateList(startTime, endTime);
        }
        public DataTable GetCreateNodeList(string NodeArr)
        {
            return dal.GetCreateNodeList(NodeArr);
        }
        public DataTable GetCreateCountList(int Count)
        {
            return dal.GetCreateCountList(Count);
        }
        public void UpdateCreate(int GeneralID, string Template)
        {
            dal.UpdateCreate(GeneralID, Template);
        }
        public int GetPreID(int InfoID)
        {
            int NodeID = GetCommonData(InfoID).NodeID;
            int prid = dal.GetPreID(InfoID, NodeID);
            if (prid == 0)
                prid = InfoID;
            return prid;
        }
        public int GetNextID(int InfoID)
        {
            int NodeID = GetCommonData(InfoID).NodeID;
            int prid = dal.GetNextID(InfoID, NodeID);
            if (prid == 0)
                prid = InfoID;
            return prid;           
        }
        public void UpHits(int InfoID)
        {
            dal.UpHits(InfoID);
        }
        public string GetFieldSet(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            string result = "";
            switch (FieldType)
            {
                case "TextType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "OptionType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "DateType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultipleHtmlType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultipleTextType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "FileType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "PicType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "ListBoxType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "FileSize":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "ThumbField":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultiPicType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "OperatingType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "SuperLinkType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MoneyType":
                    result = dr["FieldName"].ToString() + "=" + dr["FieldValue"].ToString();
                    break;
                case "BoolType":
                    result = dr["FieldName"].ToString() + "=" + dr["FieldValue"].ToString();
                    break;
                case "NumType":
                    result = dr["FieldName"].ToString() + "=" + dr["FieldValue"].ToString();
                    break;
            }
            return result;
        }
    }
}