using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.Mobile;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Mobile
{
    /// <summary>
    /// 同微信逻辑
    /// </summary>
    public class B_Mobile_PushAPI : ZL_Bll_InterFace<M_Mobile_PushAPI>
    {
        private M_Mobile_PushAPI initMod = new M_Mobile_PushAPI();
        private string PK, TbName;
        public B_Mobile_PushAPI()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Mobile_PushAPI model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Mobile_PushAPI model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Mobile_PushAPI SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
    }
}
