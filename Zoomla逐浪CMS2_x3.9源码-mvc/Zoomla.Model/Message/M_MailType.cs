namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;


    public class M_MailType:M_Base
    {
        public int ID{ get; set;}
        public string TypeName{ get; set;}
        public override string TbName { get { return "ZL_MailType"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TypeName","NVarChar","50"},
                                 };
            return Tablelist;
        }
        /// <summary>
        /// 获取字段窜
        /// </summary>
        public  string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        /// <summary>
        /// 获取参数串
        /// </summary>
        public  string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        /// <summary>
        /// 获取字段=参数
        /// </summary>
        public  string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public override SqlParameter[] GetParameters()
        {
            M_MailType model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TypeName; 

            return sp;
        }
        public  M_MailType GetModelFromReader(SqlDataReader rdr)
        {
            M_MailType model = new M_MailType();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TypeName =  ConverToStr(rdr["TypeName"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
