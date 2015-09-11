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
    using ZoomLa.DALFactory;
    using ZoomLa.Model;

    /// <summary>
    /// B_Model 的摘要说明
    /// </summary>
    public class B_Model
    {
        private static readonly ID_Model dal = IDal.CreateModel();
        public B_Model()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public DataTable GetList()
        {
            return dal.ListModel();
        }
        public DataTable GetListUser()
        {
            return dal.ListUserModel();
        }
        public DataTable GetListShop()
        {
            return dal.ListShopModel();
        }
        public bool AddModel(M_ModelInfo info)
        {
            return dal.Add(info);
        }
        public bool AddUserModel(M_ModelInfo info)
        {
            return dal.AddUserModel(info);
        }
        public M_ModelInfo GetModelById(int ModelID)
        {
            return dal.GetModelInfo(ModelID);
        }
        public bool UpdateModel(M_ModelInfo info)
        {
            return dal.Update(info);
        }
        public bool DelModel(int ModelID)
        {
            string TableName = dal.GetModelInfo(ModelID).TableName;
            dal.DeleteTable(TableName);
            return dal.Delete(ModelID);
        }
        public bool UpdateInput(string InputCode, int ModelID)
        {
            return dal.UpdateInput(InputCode,ModelID);
        }
        public bool UpdateTemplate(string Template, int ModelID)
        {
            return dal.UpdateModule(Template, ModelID);
        }
    }
}