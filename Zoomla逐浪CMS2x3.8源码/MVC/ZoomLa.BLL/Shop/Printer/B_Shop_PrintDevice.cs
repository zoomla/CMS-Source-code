using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_PrintDevice : ZL_Bll_InterFace<M_Shop_PrintDevice>
    {
        private M_Shop_PrintDevice initMod = new M_Shop_PrintDevice();
        private string TbName, PK;
        public B_Shop_PrintDevice()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Shop_PrintDevice model)
        {
            return DBCenter.Insert(model);
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
        public M_Shop_PrintDevice SelReturnModel(int ID)
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
        public bool UpdateByID(M_Shop_PrintDevice model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        /// <summary>
        /// 设为默认设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public bool SetDefault(int id)
        {
            DBCenter.UpdateSQL(TbName, "IsDefault=0", "");
            return DBCenter.UpdateSQL(TbName, "IsDefault=1", "ID=" + id);
        }
        public M_Shop_PrintDevice SelModeByDevice(string deviceNo)
        {
            if (string.IsNullOrEmpty(deviceNo)) { return null; }
            string where = " DeviceNo=@device";
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("device", deviceNo.Trim()));
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, sp.ToArray()))
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
