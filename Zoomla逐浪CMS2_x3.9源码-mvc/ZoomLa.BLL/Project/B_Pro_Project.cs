using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Project
{
    public class B_Pro_Project : ZL_Bll_InterFace<M_Pro_Project>
    {
        private string PK, TbName;
        private M_Pro_Project initMod = new M_Pro_Project();
        public B_Pro_Project()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }

        public int Insert(M_Pro_Project model)
        {
            return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_Pro_Project model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }

        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }

        public M_Pro_Project SelReturnModel(int ID)
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
        public DataTable Search(string name = "", string manageer = "", string tecDirector = "", string sdate = "", string edate = "", int type = 0, int status = -100)
        {
            string where = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                where += " AND ProName LIKE @name";
                sp.Add(new SqlParameter("name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(manageer))
            {
                where += " AND ProManageer LIKE @manageer";
                sp.Add(new SqlParameter("manageer", "%" + manageer + "%"));
            }
            if (!string.IsNullOrEmpty(tecDirector))
            {
                where += " AND TecDirector LIKE @tec";
                sp.Add(new SqlParameter("tec", "%" + tecDirector + "%"));
            }
            if (!string.IsNullOrEmpty(sdate))
            {
                DateTime dt;
                if (DateTime.TryParse(sdate, out dt))
                {
                    where += " AND DATEDIFF(ms,@sdate,[CDate]) > 0";
                    sp.Add(new SqlParameter("sdate",dt));
                }
            }
            if (!string.IsNullOrEmpty(edate))
            {
                DateTime dt;
                if (DateTime.TryParse(sdate, out dt))
                {
                    where += " AND DATEDIFF(ms,[CDate],@edate) > 0";
                    sp.Add(new SqlParameter("edate", dt));
                }
            }
            if (type > 0)
            {
                where += " AND ZType = " + type;
            }
            if (status != -100)
            {
                where += " AND ZStatus = " + status;
            }
            return DBCenter.Sel(TbName, where, "", sp);
        }
    }
}
