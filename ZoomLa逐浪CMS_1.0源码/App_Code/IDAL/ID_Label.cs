using ZoomLa.Model;
using System;
using System.Data;
namespace ZoomLa.IDAL
{
    /// <summary>
    /// 标签 的摘要说明
    /// </summary>
    public interface ID_Label
    {
        void AddLabel(M_Label label);
        void UpdateLabel(M_Label label);
        void DelLabel(int LabelID);
        M_Label GetLabel(string LabelName);
        M_Label GetLabel(int LabelID);
        DataSet GetLabelList(string LabelCate, int PageSize, int CPage);
        int GetLabelListCount(string LabelCate);
        string GetLabelCateList();
        DataSet GetLabelAll();
        bool IsExist(string LabelName);
        DataTable GetCateList();
        DataTable GetSourceList();
        DataTable GetSchemaTable();
        DataTable GetTableField(string TableName);
        DataTable GetLabelListByCate(string LabelCate);
    }
}