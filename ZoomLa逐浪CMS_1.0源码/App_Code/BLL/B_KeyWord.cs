
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
    /// KeyWord 的摘要说明
    /// </summary>
    public class B_KeyWord
    {
        private static readonly ID_KeyWord KeyWordMethod = IDal.CreateKeyWord();
        public B_KeyWord()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_KeyWord keywordInfo)
        {
            return KeyWordMethod.Add(keywordInfo);
        }
        public bool DeleteByID(string keywordId)
        {
            return KeyWordMethod.DeleteByID(keywordId);
        }
        public DataTable GetKeyWordAll()
        {
            return KeyWordMethod.GetKeyWordAll();
        }
        public bool Update(M_KeyWord KeyWordInfo)
        {
            return KeyWordMethod.Update(KeyWordInfo);
        }
        public M_KeyWord GetKeyWordByid(int kId)
        {
            return KeyWordMethod.GetKeyWordByid(kId);
        }
    }
}
