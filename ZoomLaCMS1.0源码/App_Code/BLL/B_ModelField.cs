namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Text;
    using ZoomLa.Common;

    /// <summary>
    /// B_ModelField 的摘要说明
    /// </summary>
    public class B_ModelField
    {
        private ID_ModelField dal = DALFactory.IDal.CreateModelField();
        public B_ModelField()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysteFieldList()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "GeneralID|内容ID|数字|1,Title|标题|标题|1,NodeID|所属节点|节点|1,SpecialID|专题|专题|0,Hits|点击数|数字|0,EliteLevel|推荐级别|数字|1,CreateTime|更新时间|日期时间|1,Status|状态|数字|0".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                row[0] = strArray2[0];
                row[1] = strArray2[1];
                row[2] = strArray2[2];
                row[3] = strArray2[3];
                table.Rows.Add(row);
            }
            return table;
        }
        public DataTable GetSysUserField()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "UserID|会员ID|数字|1,UserName|会员名|会员名|1".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                row[0] = strArray2[0];
                row[1] = strArray2[1];
                row[2] = strArray2[2];
                row[3] = strArray2[3];
                table.Rows.Add(row);
            }
            return table;
        }
        public void Add(M_ModelField info)
        {
            this.dal.Add(info);            
        }
        public void AddField(string tablename, string fieldname, string fieldtype, string defaultvalue)
        {
            this.dal.AddFieldToTable(tablename, fieldname, fieldtype, defaultvalue);
        }
        public void Update(M_ModelField info)
        {
            this.dal.Update(info);
        }
        public void Del(int FieldID,string tablename)
        {
            M_ModelField info=this.dal.GetModelByID(FieldID);
            this.dal.Del(FieldID);
            this.dal.DelField(tablename, info.FieldName);
        }
        public void DelSubField(string tablename, string fieldname)
        {
            this.dal.DelField(tablename, fieldname);
        }
        public void UpdateOrder(M_ModelField info)
        {
            this.dal.UpdateOrder(info);
        }
        public DataSet GetModelFieldList(int ModelID)
        {
            return this.dal.GetModelFieldList(ModelID);
        }
        public M_ModelField GetModelByID(int FieldID)
        {
            return this.dal.GetModelByID(FieldID);
        }
        public M_ModelField GetModelByOrder(int ModelID, int Order)
        {
            return this.dal.GetModelByOrder(ModelID, Order);
        }
        public M_ModelField GetModelByFieldName(int ModelID, string FieldName)
        {
            return this.dal.GetModelByFieldName(ModelID, FieldName);
        }
        public int GetMaxOrder(int ModelID)
        {
            return this.dal.GetMaxOrder(ModelID);
        }
        public int GetMinOrder(int ModelID)
        {
            return this.dal.GetMinOrder(ModelID);
        }
        public M_ModelField GetPreField(int ModelID, int CurrentID)
        {
            int PID = this.dal.GetPreID(ModelID, CurrentID);
            return this.dal.GetModelByID(PID);
        }
        public M_ModelField GetNextField(int ModelID, int CurrentID)
        {
            int NextID = this.dal.GetNextID(ModelID, CurrentID);
            return this.dal.GetModelByID(NextID);
        }
        public string GetFieldType(string FieldType)
        {
            switch (FieldType)
            {
                case "TextType":
                    return "单行文本";                    
                case "OptionType":
                    return "选项";                    
                case "DateType":
                    return "日期时间";                    
                case "MultipleHtmlType":
                    return "多行文本(支持Html)";                    
                case "MultipleTextType":
                    return "多行文本(不支持Html)";                
                case "FileType":
                    return "文件";
                case "PicType":
                    return "图片";
                case "MoneyType":
                    return "货币";
                case "BoolType":
                    return "是/否";
                case "NumType":
                    return "数字";
            }
            return "";
        }
        public bool IsExists(int ModelID, string fieldname)
        {
            return this.dal.IsExists(ModelID, fieldname);
        }
        public string GetFieldContent(string Content, int PlaceId, int TypeId)
        {
            return Content.Split(new char[] { ',' })[PlaceId].Split(new char[] { '=' })[TypeId].ToString();
        }
        /// <summary>
        /// 为模型的自定义字段生成Html输入界面代码
        /// </summary>
        /// <param name="ModelID"></param>        
        public string GetInputHtml(int ModelID,int NodeID)
        {
            DataTable dt = this.dal.GetModelFieldList(ModelID).Tables[0];
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(GetShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(),ModelID,NodeID));                    
                }
            }
            return builder.ToString();
        }
        public string GetUpdateHtml(int ModelID,int NodeID, DataTable dt1)
        {
            DataTable dt = this.dal.GetModelFieldList(ModelID).Tables[0];
            StringBuilder builder = new StringBuilder();
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        builder.Append(GetShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID, dr));                        
                    }
                }
            }
            return builder.ToString();
        }
        public string GetShowStyle(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            B_ShowField bshow = new B_ShowField();
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description,ModelID, NodeID, dr);
        }
        public string GetShowStyle(string Alias,string Name, bool IsNotNull, string Type, string Content, string Description,int ModelID,int NodeID)
        {
            B_ShowField bshow = new B_ShowField();
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, null);
        }


        /// <summary>
        /// 为模型的自定义字段生成Html输入界面代码
        /// </summary>
        /// <param name="ModelID"></param>        
        public string GetInputHtmlUser(int ModelID, int NodeID)
        {
            DataTable dt = this.dal.GetModelFieldList(ModelID).Tables[0];
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(GetShowStyleUser(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID));
                }
            }
            return builder.ToString();
        }
        public string GetUpdateHtmlUser(int ModelID, int NodeID, DataTable dt1)
        {
            DataTable dt = this.dal.GetModelFieldList(ModelID).Tables[0];
            StringBuilder builder = new StringBuilder();
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        builder.Append(GetShowStyleUser(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID, dr));
                    }
                }
            }
            return builder.ToString();
        }
        public string GetShowStyleUser(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            B_ContentField bshow = new B_ContentField();
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, dr);
        }
        public string GetShowStyleUser(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID)
        {
            B_ContentField bshow = new B_ContentField();
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, null);
        }
    }
}