using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public class B_User_Temp:ZL_Bll_InterFace<M_User_Temp>
    {
        //UseType
        //1,邀请码
        string TbName, PK;
        M_User_Temp initMod = new M_User_Temp();
        public B_User_Temp() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_User_Temp model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_User_Temp model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.UserID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName,ID);
        }
        public bool DelByUid(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Delete From " + TbName + " Where ID in(" + ids + ") And UserID=" + uid;
            return SqlHelper.ExecuteSql(sql);
        }

        public M_User_Temp SelReturnModel(int ID)
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
        public bool UpdateByField(int id, string fname, string fvalue)
        {
            SafeSC.CheckDataEx(fname);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("fvalue", fvalue) };
            string sql = "Update " + TbName + " Set " + fname + " =@fvalue Where ID=" + id;
            return SqlHelper.ExecuteSql(sql,sp);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        //------------------------------邀请码
        public bool Code_IsExist(string code) 
        {
            string sql = "Select ID From "+TbName+" Where Str1=@code";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("code",code) };
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp).Rows.Count>0;
        }
        //获取我的生成的邀请码
        public DataTable Code_Sel(int uid)
        {
            string sql = "Select * From " + TbName + " Where UserID=" + uid + " And UseType=1 And Str3='1'";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool Code_Create(int uid,int count) 
        {
            for (int i = 0; i < count; i++)
            {
                M_User_Temp utMod = new M_User_Temp();
                utMod.UserID = uid;
                utMod.UseType = 1;
                utMod.Str1 = function.GetRandomString(8);//邀请码
                utMod.Str3 = "1";//1:未使用,2:已使用
                utMod.Describe = "邀请码";
                Insert(utMod);
            }
            return true;
        }
        //标明邀请号作废
        public bool Code_Used(int id) 
        {
            return UpdateByField(id,"str3","2");
        }
        public M_User_Temp Code_SelModel(string code) 
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("code", code) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, "Where Str1=@code And Str3='1' And UseType=1", sp))
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
        //------------------------------
    }
}
