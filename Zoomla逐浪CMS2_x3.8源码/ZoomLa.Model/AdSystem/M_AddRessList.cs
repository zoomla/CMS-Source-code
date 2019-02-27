using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_AddRessList
    public class M_AddRessList:M_Base
    {
        #region 字段 属性
        public int id;
        /// <summary>
        /// ID
        /// </summary>	
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int userID;
        /// <summary>
        /// UserID
        /// </summary>	
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string addRessContext;
        /// <summary>
        /// AddRessContext
        /// </summary>	
        public string AddRessContext
        {
            get { return addRessContext; }
            set { addRessContext = value; }
        }
        #endregion
        public M_AddRessList()
        {

        }

        public override string TbName { get { return "ZL_AddRessList"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"UserID","Int","4"},            
                        {"AddRessContext","NVarChar","4000"}            
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_AddRessList model = this;
            string[,] strArr = FieldList();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.AddRessContext;
            return sp;
        }
        public M_AddRessList GetModelFromReader(SqlDataReader rdr)
        {
            M_AddRessList model = new M_AddRessList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.AddRessContext = ConverToStr(rdr["AddRessContext"]);
            rdr.Close();
            return model;
        }
    }
}