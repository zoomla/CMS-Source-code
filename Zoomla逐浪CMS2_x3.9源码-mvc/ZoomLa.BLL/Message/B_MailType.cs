namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Net.Mail;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;


   public class B_MailType
    {
        public B_MailType()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        private string PK, strTableName;
        private M_MailType initmod = new M_MailType();

       public DataTable Sel(int ID)
       {
           return Sql.Sel(strTableName, PK, ID);
       } 
       /// <summary>
       /// 查询所有记录
       /// </summary>
       public DataTable Sel()
       {
           return Sql.Sel(strTableName);
       }
       public bool UpdateReceive(M_MailType model)
       {
           return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), model.GetFieldAndPara(), model.GetParameters());
       }
       public bool DelType(int ID)
       {
           return Sql.Del(strTableName, ID);
       }
       public int insertType(M_MailType model)
       {
           return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
       }
       
    }
}
