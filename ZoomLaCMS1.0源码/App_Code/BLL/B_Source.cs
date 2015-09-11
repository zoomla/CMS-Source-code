
namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.DALFactory;
    /// <summary>
    /// B_Source 的摘要说明
    /// </summary>
    public class B_Source
    {
        private static readonly ID_Source SourceMethod = IDal.CreateSource();
        public B_Source()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_Source sourceInfo)
        {
            return SourceMethod.Add(sourceInfo);
        }
        public bool DeleteByID(string sourceId)
        {
            return SourceMethod.DeleteByID(sourceId);
        }
        public DataTable GetSourceAll()
        {
            return SourceMethod.GetSourceAll();
        }
        public bool Update(M_Source SourceInfo)
        {
            return SourceMethod.Update(SourceInfo);
        }
        public M_Source GetSourceByid(int sId)
        {
            return SourceMethod.GetSourceByid(sId);
        }
        public DataTable SearchSource(string sourcekey)
        {
            return SourceMethod.SearchSource(sourcekey);
        }
    }
}
