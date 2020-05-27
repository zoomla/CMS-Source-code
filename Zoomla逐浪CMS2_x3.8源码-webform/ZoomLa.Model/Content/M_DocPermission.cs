using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    //ZL_DocPermission
    public class M_DocPermission : M_Base
    {

        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>		
        public string UserID { get; set; }
        /// <summary>
        /// UserName
        /// </summary>		
        public string UserName { get; set; }
        /// <summary>
        /// GroupName
        /// </summary>		
        public string GroupName { get; set; }
        /// <summary>
        /// 1,权限组2,用户与权限组的关联,不设的话为默认组
        /// </summary>		
        public string Status { get; set; }
        /// <summary>
        /// OnlyMe
        /// </summary>		
        public string OnlyMe { get; set; }
        /// <summary>
        /// AllowNodes
        /// </summary>		
        public string AllowNodes { get; set; }
        /// <summary>
        /// CreateAble
        /// </summary>		
        public string CreateAble { get; set; }
        /// <summary>
        /// RetrieveAble
        /// </summary>		
        public string RetrieveAble { get; set; }
        /// <summary>
        /// UpdateAble
        /// </summary>		
        public string UpdateAble { get; set; }
        /// <summary>
        /// DeleteAble
        /// </summary>		
        public string DeleteAble { get; set; }
        /// <summary>
        /// 备注:预留
        /// </summary>		
        public string Remind { get; set; }
        /// <summary>
        /// 所属权限组ID
        /// </summary>		
        public string OwnGroupID { get; set; }

        public override string TbName { get { return "ZL_DocPermission"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","VarChar","10"},
                                  {"UserName","NVarChar","50"},
                                  {"GroupName","NVarChar","50"},
                                  {"Status","VarChar","10"},
                                  {"OnlyMe","VarChar","10"},
                                  {"AllowNodes","VarChar","100"},
                                  {"CreateAble","VarChar","10"},
                                  {"RetrieveAble","VarChar","10"},
                                  {"UpdateAble","VarChar","10"},
                                  {"DeleteAble","VarChar","10"},
                                  {"Remind","NVarChar","100"},
                                  {"OwnGroupID","VarChar","10"}
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)//要用GetLength(0)，否则长度是二维数组*子键的长度
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
        public string GetParas()
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
        public string GetFieldAndPara()
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

        public override SqlParameter[] GetParameters()//用于插入数据,将模型作为参数传入
        {
            M_DocPermission model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserName;
            sp[3].Value = model.GroupName;
            sp[4].Value = model.Status;
            sp[5].Value = model.OnlyMe;
            sp[6].Value = model.AllowNodes;
            sp[7].Value = model.CreateAble;
            sp[8].Value = model.RetrieveAble;
            sp[9].Value = model.UpdateAble;
            sp[10].Value = model.DeleteAble;
            sp[11].Value = model.Remind;
            sp[12].Value = model.OwnGroupID;
            return sp;
        }

        public M_DocPermission GetModelFromReader(SqlDataReader rdr)
        {
            M_DocPermission model = new M_DocPermission();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.GroupName = ConverToStr(rdr["GroupName"]);
            model.Status = ConverToStr(rdr["Status"]);
            model.OnlyMe = ConverToStr(rdr["OnlyMe"]);
            model.AllowNodes = ConverToStr(rdr["AllowNodes"]);
            model.CreateAble = ConverToStr(rdr["CreateAble"]);
            model.RetrieveAble = ConverToStr(rdr["RetrieveAble"]);
            model.UpdateAble = ConverToStr(rdr["UpdateAble"]);
            model.DeleteAble = ConverToStr(rdr["DeleteAble"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.OwnGroupID = ConverToStr(rdr["OwnGroupID"]);

            rdr.Close();
            rdr.Dispose();
            return model;
        }

        /// <summary>
        /// 判断用户能否看到其他人的文章
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool isOnlyMe()
        {
            return OnlyMe.Equals("1");
        }
        /// <summary>
        /// 能否新建
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool isCreateAble()
        {
            return CreateAble.Equals("1");
        }
        /// <summary>
        /// 能否执行修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool isUpdateAble()
        {
            return UpdateAble.Equals("1");
        }
        /// <summary>
        /// 能否执行删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool isDeleteAble()
        {
            return DeleteAble.Equals("1");
        }

    }
}

