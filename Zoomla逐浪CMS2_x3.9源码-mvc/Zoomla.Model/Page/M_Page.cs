using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Page : M_Base
    {
        #region 构造函数
        public M_Page()
        {
        }

        public M_Page
        (
            int ID,
            string Proname,
            string PageTitle,
            string KeyWords,
            string Description,
            string LOGO,
            string Domain,
            int Style,
            int HeadHeight,
            string HeadColor,
            int CommonModelID,
            string TableName,
            int Hits,
            int InfoID,
            int Best,
            int Status,
            int UserID,
            string UserName,
            int ParentPageID,
            int ParentUserID,
            string PageInfo,
            string TopWords,
            string BottonWords,
            DateTime CreateTime,
            int NodeStyle,
            string HeadBackGround
        )
        {
            this.ID = ID;
            this.Proname = Proname;
            this.PageTitle = PageTitle;
            this.KeyWords = KeyWords;
            this.Description = Description;
            this.LOGO = LOGO;
            this.Domain = Domain;
            this.Style = Style;
            this.HeadHeight = HeadHeight;
            this.HeadColor = HeadColor;
            this.CommonModelID = CommonModelID;
            this.TableName = TableName;
            this.Hits = Hits;
            this.InfoID = InfoID;
            this.Best = Best;
            this.Status = Status;
            this.UserID = UserID;
            this.UserName = UserName;
            this.ParentPageID = ParentPageID;
            this.ParentUserID = ParentUserID;
            this.PageInfo = PageInfo;
            this.TopWords = TopWords;
            this.BottonWords = BottonWords;
            this.CreateTime = CreateTime;
            this.NodeStyle = NodeStyle;
            this.HeadBackGround = HeadBackGround;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] PageList()
        {
            string[] Tablelist = { "ID", "Proname", "PageTitle", "KeyWords", "Description", "LOGO", "Domain", "Style", "HeadHeight", "HeadColor", "CommonModelID", "TableName", "Hits", "InfoID", "Best", "Status", "UserID", "UserName", "ParentPageID", "ParentUserID", "PageInfo", "TopWords", "BottonWords", "CreateTime", "NodeStyle", "HeadBackGround" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string Proname { get; set; }
        /// <summary>
        /// 黄页标题
        /// </summary>
        public string PageTitle { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// LOGO
        /// </summary>
        public string LOGO { get; set; }
        /// <summary>
        /// 访问域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public int Style { get; set; }
        /// <summary>
        /// 导航条高度
        /// </summary>
        public int HeadHeight { get; set; }
        /// <summary>
        /// 导航条颜色
        /// </summary>
        public string HeadColor { get; set; }
        /// <summary>
        /// ZL_CommonModel中的ID
        /// </summary>
        public int CommonModelID { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 模型内容ID
        /// </summary>
        public int InfoID { get; set; }
        /// <summary>
        /// 推荐级别
        /// </summary>
        public int Best { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 上级黄页ID(上级商铺)
        /// </summary>
        public int ParentPageID { get; set; }
        /// <summary>
        /// 上级用户ID
        /// </summary>
        public int ParentUserID { get; set; }
        /// <summary>
        /// 黄页说明
        /// </summary>
        public string PageInfo { get; set; }
        /// <summary>
        /// 头部自定义HTML内容
        /// </summary>
        public string TopWords { get; set; }
        /// <summary>
        /// 底部HTML内容
        /// </summary>
        public string BottonWords { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 节点样式
        /// </summary>
        public int NodeStyle { get; set; }
        /// <summary>
        /// 导航背景图片
        /// </summary>
        public string HeadBackGround { get; set; }
        #endregion

        public override string TbName { get { return "ZL_Page"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Proname","NVarChar","4"},
                                  {"PageTitle","NVarChar","4"},
                                  {"KeyWords","NVarChar","4"}, 
                                  {"Description","NVarChar","4"}, 
                                  {"LOGO","NVarChar","4"}, 
                                  {"Domain","NVarChar","4"}, 
                                  {"Style","Int","4"}, 
                                  {"HeadHeight","Int","4"}, 
                                  {"HeadColor","NVarChar","4"}, 
                                  {"CommonModelID","Int","4"}, 
                                  {"TableName","NVarChar","4"}, 
                                  {"Hits","Int","4"}, 
                                  {"InfoID","Int","4"}, 
                                  {"Best","Int","4"}, 
                                  {"Status","Int","4"}, 
                                  {"UserID","Int","4"}, 
                                  {"UserName","NVarChar","4"}, 
                                  {"ParentPageID","Int","4"}, 
                                  {"ParentUserID","Int","4"}, 
                                  {"PageInfo","NText","4"}, 
                                  {"TopWords","NText","4"}, 
                                  {"BottonWords","NText","4"}, 
                                  {"CreateTime","DateTime","4"}, 
                                  {"NodeStyle","Int","4"}, 
                                  {"HeadBackGround","NVarChar","4"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Page model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Proname;
            sp[2].Value = model.PageTitle;
            sp[3].Value = model.KeyWords;
            sp[4].Value = model.Description;
            sp[5].Value = model.LOGO;
            sp[6].Value = model.Domain;
            sp[7].Value = model.Style;
            sp[8].Value = model.HeadHeight;
            sp[9].Value = model.HeadColor;
            sp[10].Value = model.CommonModelID;
            sp[11].Value = model.TableName;
            sp[12].Value = model.Hits;
            sp[13].Value = model.InfoID;
            sp[14].Value = model.Best;
            sp[15].Value = model.Status;
            sp[16].Value = model.UserID;
            sp[17].Value = model.UserName;
            sp[18].Value = model.ParentPageID;
            sp[19].Value = model.ParentUserID;
            sp[20].Value = model.PageInfo;
            sp[21].Value = model.TopWords;
            sp[22].Value = model.BottonWords;
            sp[23].Value = model.CreateTime;
            sp[24].Value = model.NodeStyle;
            sp[25].Value = model.HeadBackGround;
            return sp;
        }

        public M_Page GetModelFromReader(SqlDataReader rdr)
        {
            M_Page model = new M_Page();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Proname = rdr["Proname"].ToString();
            model.PageTitle = rdr["PageTitle"].ToString();
            model.KeyWords = rdr["KeyWords"].ToString();
            model.Description = rdr["Description"].ToString();
            model.LOGO = rdr["LOGO"].ToString();
            model.Domain = rdr["Domain"].ToString();
            model.Style = Convert.ToInt32(rdr["Style"]);
            model.HeadHeight = Convert.ToInt32(rdr["HeadHeight"]);
            model.HeadColor = rdr["HeadColor"].ToString();
            model.CommonModelID = Convert.ToInt32(rdr["CommonModelID"]);
            model.TableName = rdr["TableName"].ToString();
            model.Hits = Convert.ToInt32(rdr["Hits"]);
            model.InfoID = Convert.ToInt32(rdr["InfoID"]);
            model.Best = Convert.ToInt32(rdr["Best"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.ParentPageID = Convert.ToInt32(rdr["ParentPageID"]);
            model.ParentUserID = Convert.ToInt32(rdr["ParentUserID"]);
            model.PageInfo = rdr["PageInfo"].ToString();
            model.TopWords = rdr["TopWords"].ToString();
            model.BottonWords = rdr["BottonWords"].ToString();
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.NodeStyle = Convert.ToInt32(rdr["NodeStyle"]);
            model.HeadBackGround = rdr["HeadBackGround"].ToString();
            rdr.Close();
            return model;
        }
    }
}


