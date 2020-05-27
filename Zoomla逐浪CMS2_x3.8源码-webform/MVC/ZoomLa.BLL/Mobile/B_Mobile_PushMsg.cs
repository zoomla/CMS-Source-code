using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.Mobile;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Mobile
{
    public class B_Mobile_PushMsg : ZL_Bll_InterFace<M_Mobile_PushMsg>
    {
        private M_Mobile_PushMsg initMod = new M_Mobile_PushMsg();
        private string PK, TbName;
        public B_Mobile_PushMsg() 
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Mobile_PushMsg model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Mobile_PushMsg model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Mobile_PushMsg SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
