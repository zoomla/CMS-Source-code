namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Text;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL;
    using ZoomLa.Components;
    using System.Collections.Generic;
    using System.Data.Common;
    using SQLDAL.SQL;
    public class B_UserBaseField
    {
        string TbName = "", PK = "";
        M_UserBaseField initMod = new M_UserBaseField();
        public B_UserBaseField()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int GetInsert(M_UserBaseField model)
        {
            return DBCenter.Insert(model);
        }
        public bool GetDelete(int ID)
        {
            M_UserBaseField fieldMod = GetSelect(ID);
            DBCenter.Del(TbName, PK, ID);
            new B_ModelField().DelField("ZL_UserBase", fieldMod.FieldName);
            return true;
        }
        public M_UserBaseField getUserBaseFieldByFieldName(string fieldName)
        {
            List<SqlParameter> spList = new List<SqlParameter>() { new SqlParameter("fieldName", fieldName) };
            return initMod.GetInfoFromDataTable(DBCenter.Sel("ZL_UserBaseField", "fieldName=@fieldName", "", spList));
        }
        public bool GetUpdate(M_UserBaseField model)
        {
            return DBCenter.UpdateByID(model, model.FieldID); ;
        }
        public bool InsertUpdate(M_UserBaseField model)
        {
            if (model.FieldID > 0)
                return GetUpdate(model);
            else
                return GetInsert(model) > 0;
        }
        public bool GetDeletes(int FieldID)
        {
            return GetDelete(FieldID);
        }
        public string GetHtml()
        {
            DataTable dt = Select_All();
            StringBuilder builder = new StringBuilder();
            //string row = "";
            B_ModelField bmf = new B_ModelField();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //返回tr,需要将两行整为一行
                    string str = bmf.GetShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), 0, 0);
                    builder.Append(str);
                    //str = str.Replace("<tr>", "").Replace("</tr>", "");
                    //row += str;
                    //if (((i + 1) % 2) == 0) { builder += "<tr>" + row + "</tr>"; row = ""; }
                }
            }
            //if (!string.IsNullOrEmpty(row)) { builder += "<tr>" + row + "</tr>"; row = ""; }
            return builder.ToString();
        }
        public string GetUpdateHtml(int userid)
        {
            DataTable dt = new B_User().GetUserBaseByuserid(userid.ToString());
            return GetUpdateHtml(dt);
        }
        //用dt传输报错，所以统上使用上方方法远程验证
        public string GetUpdateHtml(DataTable dt1)
        {
            DataTable dt = Select_All();
            StringBuilder builder = new StringBuilder();
            B_ModelField bmf = new B_ModelField();
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        builder.Append(bmf.GetUserShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), 0, 0, dr));
                    }
                }
            }
            return builder.ToString();
        }
        public M_UserBaseField GetSelect(int ID)
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
        public DataTable SelByFieldName(string fieldName)
        {
            List<SqlParameter> spList = new List<SqlParameter>() { new SqlParameter("fieldName", fieldName) };
            return DBCenter.Sel(TbName, "FieldName=@fieldName", "", spList);
        }
        public DataTable Select_All()
        {
            return DBCenter.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }

        public int GetMaxID()
        {
            return DataConvert.CLng(DBCenter.ExecuteScala(TbName, "Max(FieldID)", ""));
        }
        public M_UserBaseField GetPreField(int CurrentID)
        {
            int FieldID = GetPreID(CurrentID);
            return GetSelect(FieldID);
        }
        public int GetPreID(int CurrentID)
        {
            return DataConverter.CLng(DBCenter.ExecuteScala(TbName, "FieldID", "OrderId<" + CurrentID, "OrderId DESC"));
        }
        public int GetNextID(int CurrentID)
        {
            return DataConverter.CLng(DataConvert.CLng(DBCenter.ExecuteScala("ZL_UserBaseField", "FieldID", "OrderId>" + CurrentID, "OrderId Asc")));
        }
        public M_UserBaseField GetNextField(int CurrentID)
        {
            int FieldID = GetNextID(CurrentID);
            return GetSelect(FieldID);
        }
        /// <summary>
        /// 返回UserID,TrueName,HoneyName,用于工作流等显示用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelAll()
        {
            return DBCenter.SelWithField("ZL_UserBase", "UserID,HoneyName,TrueName");
        }
    }
}