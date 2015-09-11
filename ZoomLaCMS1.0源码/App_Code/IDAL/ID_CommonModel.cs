using ZoomLa.Model;
using System;
using System.Data;
namespace ZoomLa.IDAL
{
    /// <summary>
    /// 内容操作公用接口
    /// </summary>
    public interface ID_CommonModel
    {
        DataTable ContentList(int NodelID, string flag);
        int GetListCount(int NodeID,string flag);
        DataTable ContentRecycle(int NodeID);
        int AddContent(string FieldList, string FieldValue, M_CommonData CommonData);
        void UpdateContent(string FieldSet, M_CommonData CommonData);
        void DelContent(M_CommonData CommonData);
        bool MoveContent(string ContentIDS, int NodeID);
        void SetDel(int GeneralID);
        void SetAudit(int GeneralID);
        void Reset(int GeneralID);
        void ResetAll();        
        void UpdateCreate(int GeneralID, string Template);
        DataTable GetContent(M_CommonData CommonData);
        M_CommonData GetCommonData(int GeneralID);
        DataTable GetCreateAllList();
        DataTable GetCreateIDList(int startID, int endID);
        DataTable GetCreateDateList(DateTime startTime, DateTime endTime);
        DataTable GetCreateNodeList(string NodeArr);
        DataTable GetCreateCountList(int Count);
        DataTable ContentListUser(int NodelID, string flag,string inputer);
        DataSet ContentSearch(string filter, int PageSize, int CPage);
        int CountSearch(string filter);
        int GetPreID(int InfoID,int NodeID);
        int GetNextID(int InfoID,int NodeID);
        void UpHits(int GeneralID);
    }
}