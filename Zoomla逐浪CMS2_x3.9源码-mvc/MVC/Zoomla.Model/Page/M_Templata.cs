using System;
using System.Data.SqlClient;
using System.Data;


namespace ZoomLa.Model
{
    /// <summary>
    ///M_Templata业务实体
    /// </summary>
    [Serializable]
    public class M_Templata : M_Base
    {
        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public M_Templata()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public M_Templata
        (
            int templateID,
            string templateName,
            string templateUrl,
            int templateType,
            string openType,
            string userGroup,
            DateTime addtime,
            int isTrue,
            int userid,
            int orderID,
            int parentID,
            string modelinfo,
            string identifiers,
            string nodeFileEx,
            string contentFileEx,
            string nodeimgurl,
            string nodeimgtext,
            string pagecontent,
            string pageMetakeyword,
            string pageMetakeyinfo,
            string linkurl,
            string linkimg,
            string linktxt,
            string username,
            int ParentNode
        )
        {
            this.TemplateID = templateID;
            this.TemplateName = templateName;
            this.TemplateUrl = templateUrl;
            this.TemplateType = templateType;
            this.OpenType = openType;
            this.UserGroup = userGroup;
            this.Addtime = addtime;
            this.IsTrue = isTrue;
            this.UserID = userid;
            this.OrderID = orderID;
            this.ParentID = parentID;
            this.Modelinfo = modelinfo;
            this.Identifiers = identifiers;
            this.NodeFileEx = nodeFileEx;
            this.ContentFileEx = contentFileEx;
            this.Nodeimgurl = nodeimgurl;
            this.Nodeimgtext = nodeimgtext;
            this.Pagecontent = pagecontent;
            this.PageMetakeyword = pageMetakeyword;
            this.PageMetakeyinfo = pageMetakeyinfo;
            this.Linkurl = linkurl;
            this.Linkimg = linkimg;
            this.Linktxt = linktxt;
            this.Username = username;
            this.ParentNode = ParentNode;
        }

        #endregion

        #region 属性定义

        ///<summary>
        ///黄页栏目ID
        ///</summary>
        public int TemplateID { get; set; }

        ///<summary>
        ///黄页栏目名称
        ///</summary>
        public string TemplateName { get; set; }

        ///<summary>
        ///黄页栏目地址
        ///</summary>
        public string TemplateUrl { get; set; }

        ///<summary>
        ///栏目类型(1-单页型栏目，2-栏目型，3-Url转发型栏目)
        ///</summary>
        public int TemplateType { get; set; }

        ///<summary>
        ///默认打开方式
        ///</summary>
        public string OpenType { get; set; }

        ///<summary>
        ///栏目样式组ID
        ///</summary>
        public string UserGroup { get; set; }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime Addtime { get; set; }

        ///<summary>
        ///启用状态(0-停用,1-启用)
        ///</summary>
        public int IsTrue { get; set; }

        ///<summary>
        ///所属用户ID
        ///</summary>
        public int UserID { get; set; }

        ///<summary>
        ///排列顺序
        ///</summary>
        public int OrderID { get; set; }

        ///<summary>
        ///上级节点ID [0-根级]
        ///</summary>
        public int ParentID { get; set; }

        ///<summary>
        ///模型信息
        ///</summary>
        public string Modelinfo { get; set; }
        ///<summary>
        ///标识符
        ///</summary>
        public string Identifiers { get; set; }

        ///<summary>
        ///栏目扩展名
        ///</summary>
        public string NodeFileEx { get; set; }

        ///<summary>
        ///内容页扩展名
        ///</summary>
        public string ContentFileEx { get; set; }
        ///<summary>
        ///栏目图片地址
        ///</summary>
        public string Nodeimgurl { get; set; }

        ///<summary>
        ///栏目提示
        ///</summary>
        public string Nodeimgtext { get; set; }

        ///<summary>
        ///说明 用于在单页页详细介绍单页信息，支持HTML
        ///</summary>
        public string Pagecontent { get; set; }

        ///<summary>
        ///META关键词
        ///</summary>
        public string PageMetakeyword { get; set; }

        ///<summary>
        ///META网页描述
        ///</summary>
        public string PageMetakeyinfo { get; set; }
        ///<summary>
        ///外部链接地址
        ///</summary>
        public string Linkurl { get; set; }

        ///<summary>
        ///外部链接图片地址
        ///</summary>
        public string Linkimg { get; set; }

        ///<summary>
        ///外部链接提示
        ///</summary>
        public string Linktxt { get; set; }
        public string Username { get; set; }
        /// <summary>
        /// 绑定系统节点
        /// </summary>
        public int ParentNode { get; set; }
        /// <summary>
        /// 是否需要审核0:不需要,1:需要(默认)
        /// </summary>
        public int NeedAudit { get; set; }
        #endregion

        public override string TbName { get { return "ZL_PageTemplate"; } }
        public override string PK { get { return "TemplateID"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"TemplateID","Int","4"},
                                  {"TemplateName","NVarChar","50"},
                                  {"TemplateUrl","NVarChar","50"},
                                  {"TemplateType","Int","4"}, 
                                  {"OpenType","NVarChar","50"},
                                  {"UserGroup","NVarChar","50"},
                                  {"Addtime","DateTime","8"},
                                  {"IsTrue","Int","4"},
                                  {"Userid","Int","4"}, 
                                  {"UserName","NVarChar","4000"},
                                  {"OrderID","Int","4"},
                                  {"ParentID","Int","4"},
                                  {"Modelinfo","Text","400"},
                                  {"identifiers","NVarChar","50"}, 
                                  {"NodeFileEx","NVarChar","255"},
                                  {"ContentFileEx","NVarChar","255"},
                                  {"Nodeimgurl","NVarChar","50"},
                                  {"Nodeimgtext","NVarChar","50"},
                                  {"Pagecontent","Text","400"}, 
                                  {"PageMetakeyword","NVarChar","255"},
                                  {"PageMetakeyinfo","NVarChar","255"},
                                  {"linkurl","NVarChar","255"},
                                  {"linkimg","NVarChar","255"},
                                  {"linktxt","NVarChar","255"}, 
                                  {"ParentNode","Int","4"},
                                  {"NeedAudit","Int","4"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Templata model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.TemplateID;
            sp[1].Value = model.TemplateName;
            sp[2].Value = model.TemplateUrl;
            sp[3].Value = model.TemplateType;
            sp[4].Value = model.OpenType;
            sp[5].Value = model.UserGroup;
            sp[6].Value = model.Addtime;
            sp[7].Value = model.IsTrue;
            sp[8].Value = model.UserID;
            sp[9].Value = model.Username;
            sp[10].Value = model.OrderID;
            sp[11].Value = model.ParentID;
            sp[12].Value = model.Modelinfo;
            sp[13].Value = model.Identifiers;
            sp[14].Value = model.NodeFileEx;
            sp[15].Value = model.ContentFileEx;
            sp[16].Value = model.Nodeimgurl;
            sp[17].Value = model.Nodeimgtext;
            sp[18].Value = model.Pagecontent;
            sp[19].Value = model.PageMetakeyword;
            sp[20].Value = model.PageMetakeyinfo;
            sp[21].Value = model.Linkurl;
            sp[22].Value = model.Linkimg;
            sp[23].Value = model.Linktxt;
            sp[24].Value = model.ParentNode;
            sp[25].Value = model.NeedAudit;
            return sp;
        }

        public M_Templata GetModelFromReader(SqlDataReader rdr)
        {
            M_Templata model = new M_Templata();
            model.TemplateID = Convert.ToInt32(rdr["TemplateID"]);
            model.TemplateName = rdr["TemplateName"].ToString();
            model.TemplateUrl = rdr["TemplateUrl"].ToString();
            model.TemplateType = Convert.ToInt32(rdr["TemplateType"]);
            model.OpenType = rdr["OpenType"].ToString();
            model.UserGroup = rdr["UserGroup"].ToString();
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.IsTrue = Convert.ToInt32(rdr["IsTrue"]);
            model.UserID = Convert.ToInt32(rdr["Userid"]);
            model.Username = rdr["UserName"].ToString();
            model.OrderID = Convert.ToInt32(rdr["OrderID"]);
            model.ParentID = Convert.ToInt32(rdr["ParentID"]);
            model.Modelinfo = rdr["Modelinfo"].ToString();
            model.Identifiers = rdr["identifiers"].ToString();
            model.NodeFileEx = rdr["NodeFileEx"].ToString();
            model.ContentFileEx = rdr["ContentFileEx"].ToString();
            model.Nodeimgurl = rdr["Nodeimgurl"].ToString();
            model.Nodeimgtext = rdr["Nodeimgtext"].ToString();
            model.Pagecontent = rdr["Pagecontent"].ToString();
            model.PageMetakeyword = rdr["PageMetakeyword"].ToString();
            model.PageMetakeyinfo = rdr["PageMetakeyinfo"].ToString();
            model.Linkurl = rdr["linkurl"].ToString();
            model.Linkimg = rdr["linkimg"].ToString();
            model.Linktxt = rdr["linktxt"].ToString();
            model.ParentNode = Convert.ToInt32(rdr["ParentNode"]);
            model.NeedAudit = Convert.ToInt32(rdr["NeedAudit"] == DBNull.Value ? 1 : rdr["NeedAudit"]);
            rdr.Close();
            return model;
        }
    }
}