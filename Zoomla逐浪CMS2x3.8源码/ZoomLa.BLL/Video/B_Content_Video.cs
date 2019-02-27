using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public class B_Content_Video:ZL_Bll_InterFace<M_Content_Video>
    {
        public M_Content_Video initMod = new M_Content_Video();
        public string TbName, PK;
        public B_Content_Video()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Content_Video model)
        {
            return Sql.insert(TbName, initMod.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Content_Video model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName,ID);
        }

        public M_Content_Video SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
            return Sql.Sel(TbName,"","CDate DESC");
        }
        //所有视频数据(以文件形式)
        public DataTable SelForFile()
        {
            string sql = "SELECT VName AS Name,Thumbnail AS Path,CDate AS CreateTime,VPath AS FilePath FROM " + TbName;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
    }
}
