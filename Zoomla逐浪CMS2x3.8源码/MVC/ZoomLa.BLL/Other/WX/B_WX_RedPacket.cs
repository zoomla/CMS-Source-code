using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Other
{
    public class B_WX_RedPacket
    {
        private string TbName, PK;
        private M_WX_RedPacket initMod = new M_WX_RedPacket();
        public B_WX_RedPacket()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int appid, string skey = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "1=1 ";
            if (!string.IsNullOrEmpty(skey)) { where += " AND Name LIKE @skey "; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            if (appid != 0)
            {
                where += " AND AppID=" + appid;
            }
            string fields = "*";
            fields += ",(SELECT COUNT(ID) FROM ZL_WX_RedDetail WHERE MainID=A.ID)AS RedCount";
            fields += ",(SELECT SUM(AMOUNT) FROM ZL_WX_RedDetail WHERE MainID=A.ID) AS RedAmount";
            fields += ",(SELECT COUNT(ID) FROM ZL_WX_RedDetail WHERE MainID=A.ID AND ZStatus=1)AS LeftRedCount";
            PageSetting setting = PageSetting.Single(1, int.MaxValue, TbName, PK, where, "", sp, fields);
            return DBCenter.SelPage(setting);
        }
        public int Insert(M_WX_RedPacket model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_WX_RedPacket model)
        {
            return DBCenter.UpdateByID(model,model.ID);
        }
        public bool Del(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        public M_WX_RedPacket SelReturnModel(int ID)
        {
            if (ID < 1) { return null; }
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
