using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.User
{
    public class B_User_BlogStyle : B_Base<M_User_BlogStyle>
    {
        private string TbName, PK;
        private M_User_BlogStyle initMod = new M_User_BlogStyle();
        public B_User_BlogStyle() { TbName = initMod.TbName; PK = initMod.PK; }
        public int Insert(M_User_BlogStyle model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_User_BlogStyle model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID) { return DBCenter.Del(TbName, PK, ID); }
        public void DelByIDS(string ids) { DBCenter.DelByIDS(TbName, PK, ids); }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public M_User_BlogStyle SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
