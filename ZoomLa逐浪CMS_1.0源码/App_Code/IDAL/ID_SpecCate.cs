using System;
using ZoomLa.Model;
using System.Data;
namespace ZoomLa.IDAL
{
    /// <summary>
    /// ID_SpecCate 的摘要说明
    /// </summary>
    public interface ID_SpecCate
    {
        void AddCate(M_SpecCate SpecCate);
        void Update(M_SpecCate SpecCate);
        DataTable GetCateList();
        void DelCate(int SpecCateID);
        M_SpecCate GetSpecCate(int SpecCateID);

    }
}