using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public abstract class B_Base<T> where T : new()
    {
        //protected string TbName, PK;
        //protected T initMod = new T();
        //public int Insert(M_Base model)
        //{
        //    return Sql.insertID(TbName, initMod.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        //}
        //public bool UpdateByID(M_ARoleAuth model)
        //{
        //    return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        //}
        //public bool Del(int ID)
        //{
        //    return Sql.Del(TbName, ID);
        //}
        //public M_ARoleAuth SelReturnModel(int ID)
        //{
        //    using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
        //    {
        //        if (reader.Read())
        //        {
        //            return initMod.GetModelFromReader(reader);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
