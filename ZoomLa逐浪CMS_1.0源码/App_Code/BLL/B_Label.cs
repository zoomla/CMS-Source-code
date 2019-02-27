using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.DALFactory;
using ZoomLa.IDAL;
using ZoomLa.Model;
namespace ZoomLa.BLL
{
    /// <summary>
    /// B_Label 的摘要说明
    /// </summary>
    public class B_Label
    {
        private ID_Label dal = IDal.CreateLabel();
        public B_Label()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void AddLabel(M_Label label)
        {
            dal.AddLabel(label);
        }
        public void DelLabel(int LabelID)
        {
            dal.DelLabel(LabelID);
        }
        public void UpdateLabel(M_Label label)
        {
            dal.UpdateLabel(label);
        }
        public M_Label GetLabel(int LabelID)
        {
            return dal.GetLabel(LabelID);
        }
        public M_Label GetLabel(string LabelName)
        {
            return dal.GetLabel(LabelName);
        }
        public DataSet GetAllLabel()
        {
            return dal.GetLabelAll();
        }
        public bool IsExist(string LabelName)
        {
            return dal.IsExist(LabelName);
        }
        public DataTable LabelList(string LabelCate, int PageCount, int CPage)
        {
            return dal.GetLabelList(LabelCate, PageCount, CPage).Tables[1];
        }
        public DataTable GetLabelListByCate(string LabelCate)
        {
            return dal.GetLabelListByCate(LabelCate);
        }
        public string LabelCateList()
        {
            return dal.GetLabelCateList();
        }
        public DataTable GetCateList()
        {
            return dal.GetCateList();
        }
        public DataTable GetSourceLabel()
        {
            return dal.GetSourceList();
        }
        public DataTable GetTableName()
        {
            return dal.GetSchemaTable();
        }
        public DataTable GetTableField(string TableName)
        {
            return dal.GetTableField(TableName);
        }
        public int GetLabelListCount(string LabelCate)
        {
            return dal.GetLabelListCount(LabelCate);
        }
        /// <summary>
        /// 将标签转换成页面Html代码
        /// </summary>
        /// <param name="sLabel">标签</param>
        /// <returns></returns>
        public string GetLabelHtml(string sLabel)
        {
            return "";
        }
        /// <summary>
        /// 将模板转换成Html
        /// </summary>
        /// <param name="Template"></param>
        public string ReplaceLabel(string Template)
        {
            return "";
        }
    }
}