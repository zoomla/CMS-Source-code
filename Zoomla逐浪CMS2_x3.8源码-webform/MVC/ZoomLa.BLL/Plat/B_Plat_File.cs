using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_File : ZL_Bll_InterFace<M_Plat_File>
    {     
        public string TbName, PK;
        public M_Plat_File initMod = new M_Plat_File();
        public B_Plat_File() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_File model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Plat_File model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Guid.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool Del(string guid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("guid", guid) };
            return Sql.Del(TbName, " Guid=@guid",sp);
        }
        public M_Plat_File SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName,"ID",ID))
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
        public M_Plat_File SelReturnModel(string guid)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("guid",guid) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName," Where Guid=@guid",sp))
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
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 根据路径返回文件,因为路径中已限定了公司,所以不必传入公司ID
        /// </summary>
        public DataTable SelByVPath(string vpath)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("vpath", vpath) };
            string where = " VPath=@vpath ";
            return DBCenter.JoinQuery("A.*,B.HoneyName,B.TrueName", TbName, "ZL_User_PlatView", "A.UserID=B.UserID", where, "FileType DESC", sp);
        }
        //重命名文件
        public void ReName(int id, string newname)
        {
            M_Plat_File model = SelReturnModel(id);
            //string oldppath = function.VToP(model.VPath + model.FileName);
            //string newppath = function.VToP(model.VPath + newname);
            //if (Directory.Exists(oldppath))
            //{
            //    Directory.Move(oldppath, newppath);
            //}
            //else if (File.Exists(oldppath))
            //{
            //    File.Move(oldppath, newppath);
            //}
            model.FileName = newname;
            UpdateByID(model);
        }
    }
}
