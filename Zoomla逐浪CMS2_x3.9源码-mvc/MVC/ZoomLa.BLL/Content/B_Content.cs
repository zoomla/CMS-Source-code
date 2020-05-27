using System;
using System.Data;
using System.Configuration;
using System.Web;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Collections.Generic;
using ZoomLa.Components;
using ZoomLa.Model.Page;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Page;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.SQLDAL.SQL;
using System.Data.Common;
using ConStatus = ZoomLa.Model.ZLEnum.ConStatus;

namespace ZoomLa.BLL   
{
    public class B_Content
    {
        public string TbName,PK;
        private M_CommonData initMod = new M_CommonData();
        public B_Content() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return DBCenter.Sel(TbName, PK + "=" + ID);
        }
        public M_CommonData GetCommonData(int GeneralID)
        {
            if (GeneralID < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, GeneralID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return null;
            }
        }
        public M_CommonData SelReturnModel(int id) 
        {
            return GetCommonData(id);
        }
        public M_CommonData SelReturnModel(string strWhere,SqlParameter[] sp=null)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, strWhere, sp))
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
        public bool SelHasTitle(string title)
        {
            title = title.Trim();
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("@title", title) };
            return DBCenter.Count(TbName, "Title=@title", sp) > 0;
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        /// <summary>
        /// 用于动力模块等
        /// </summary>
        public DataTable Search(string skey = "", string ids = "", string nids = "", string status = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND Title LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            if (!string.IsNullOrEmpty(ids)) { SafeSC.CheckIDSEx(ids); where += " AND GeneralID IN(" + ids + ")"; }
            if (!string.IsNullOrEmpty(nids)) { where += " AND NodeID IN (" + nids + ")"; SafeSC.CheckIDSEx(nids); }
            if (string.IsNullOrEmpty(status)) { where += " AND Status!=" + (int)ZLEnum.ConStatus.Recycle; }
            else { where += " AND Status=" + DataConvert.CLng(status); }
            return DBCenter.Sel(TbName, where, PK + " DESC", sp);
        }
        /// <summary>
        /// 根据专题获取文章
        /// </summary>
        public DataTable SelBySpecialID(string specialID)
        {
            List<SqlParameter> sp = new List<SqlParameter> { new SqlParameter("SpecialID", "%," + specialID + ",%") };
            DataTable dt = DBCenter.Sel(TbName, "SpecialID Like @SpecialID", "", sp);
            return dt;
        }
        public DataTable SelByItemID(int item)
        {
            return DBCenter.Sel(TbName, "ItemID=" + item);
        }
        //标题近似名查重
        public DataTable GetByDupTitle(string title)
        {
            DataTable dt = new DataTable();
            if (title.Length > 5)
            {
                string title2 = title;
                for (int i = 4; i < title.Length; i = i + 4)
                {
                    title2 = title2.Substring(0, i) + "_" + title2.Substring((i + 1), (title2.Length - i - 1));
                }
                title2 = title.Substring(1, title.Length - 1);
                //根据字符个数,去掉头尾
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("title", "%" + title + "%") };
                dt = DBCenter.SelWithField(TbName, "GeneralID,Title", "TableName Like 'ZL_C_%' AND Title Like @title");
            }
            return dt;
        }
        public DataTable SelByStatus(ConStatus status)
        {
            return DBCenter.Sel(TbName, "status=" + (int)status, "CreateTime DESC");
        }
        //--------------------------------------------------用于ContentManage
        /// <summary>
        /// 默认为全部搜索,该接口未参数化，请勿对外开放
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="nodeID">节点ID，为0搜全部</param>
        /// <param name="status">状态</param>
        /// <param name="keyType">关键词类型(标题,作者，ID)</param>
        /// <param name="keyWord">关键词</param>
        /// <param name="keyWord">权限筛选,办理有权限的节点 格式:1,2,100,3</param>
        public PageSetting SelPage(int pageIndex, int pageSize, int nodeID = 0, int modelID = 0, string status = "", int keyType = 0, string keyWord = "", string authNodes = "")
        {
            List<SqlParameter> spList = new List<SqlParameter>();
            string where = "", order = "A.OrderID Desc,GeneralID Desc";
            where = GetPageWhere(spList, nodeID, modelID, status, keyType, keyWord, authNodes);
            ////用于计算点击数等
            //SelWhere = filter.Replace("A.", "").Replace("B.", "");
            PageSetting config = new PageSetting()
            {
                fields = "A.*,B.NodeName",pk = PK,t1 = TbName,t2 = "ZL_Node",
                cpage = pageIndex,psize = pageSize,
                on = "A.NodeID=B.NodeID",where = where,order = order,
                sp = spList.ToArray()
            };
            config.dt = DBCenter.SelPage(config);
            config.addon =DBCenter.ExecuteScala(TbName,"SUM(Hits)",where.Replace("A.", "").Replace("B.", ""),"", spList).ToString();
            return config;
        }
        /// <summary>
        /// 配置 where 1=1使用
        /// </summary>
        private string GetPageWhere(List<SqlParameter> spList, int nodeID = 0,int modelID=0, string status = "", int keyType = 0, string keyWord = "", string authNodes = "") 
        {
            //string nodes = stringTreeNodes(nodeID, "");//如为父节点，则遍历出子节点
            keyWord = keyWord.Trim();
            string filter = "A.Status Not In(-2) ";
            if (!string.IsNullOrEmpty(status))//便于支持多状态筛选
            {
                status = StrHelper.PureIDSForDB(status);
                SafeSC.CheckIDSEx(status);
                filter += " And A.Status In(" + status + ") ";
            }
            string keySql = "";
            if (!string.IsNullOrEmpty(keyWord))//以空格作为分隔符的多条件搜索
            {
                string[] keyArr = keyWord.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < keyArr.Length; i++)
                {
                    string key = keyArr[i];
                    string index = "skey" + i;
                    switch (keyType)
                    {
                        default:
                        case 0:
                            #region 按Gid或标题搜索
                            {
                                if (i == 0)
                                {
                                    if (DataConvert.CLng(key) > 0)
                                    {
                                        keySql += " AND (A.GeneralID=" + key + " ";
                                    }
                                    else
                                    {
                                        spList.Add(new SqlParameter(index, "%" + key + "%"));
                                        keySql += " AND (A.Title LIKE @" + index + " ";
                                    }
                                }
                                else
                                {
                                    if (DataConvert.CLng(key) > 0)
                                    {
                                        keySql += " OR GeneralID=" + key + " ";
                                    }
                                    else
                                    {
                                        spList.Add(new SqlParameter(index, "%" + key + "%"));
                                        keySql += " OR Title LIKE @" + index + " ";
                                    }
                                }
                            }
                            #endregion
                            break;
                        case 1:
                            #region 按作者名筛选,一般只有一个所以不包
                            {
                                spList.Add(new SqlParameter(index, key));
                                if (i == 0)
                                {
                                    keySql += " And (A.Inputer=@" + index + " ";
                                }
                                else
                                {
                                    //keySql += " OR A.Inputer=@" + index + " ";
                                }
                            }
                            #endregion
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(keySql)) { keySql += ")"; }
                filter += keySql;
            }
            if (nodeID > 0)//取其和下一级子级(后期是否扩展为递归)
            {
                string childNodes = new B_Node().GetChilds(nodeID);
                filter += " AND A.NodeID IN(" + childNodes + ")";
            }
            if (modelID > 0)
            {
                filter += " AND A.ModelID=" + modelID;
            }
            filter += " AND A.TableName LIKE 'ZL_C_%'";
            //-----权限模块
            if (!string.IsNullOrEmpty(authNodes))
            {
                filter += " And A.NodeID In(" + authNodes.TrimEnd(',') + ")";
            }
            return filter;
        }
        //------------工作流相关
        /// <summary>
        /// 获取自己拥有审核权限的文章,默认流程所有管理员员均拥有权限
        /// </summary>
        /// <param name="roles">角色字符串</param>
        /// <param name="tnid">NodeID,为0取全部</param>
        public DataTable GetDTByAuth(string roles, int tnid = 0)
        {
            //未设定工作流的文章,默认所有管理员都拥有权限
            //1,查出ZL_CommonModel中绑定了工作流,且非已审核,退稿,回收站,草稿的文章
            //2,查出ZL_Process中自己所拥有的权限
            //3,匹配NodeID与NeedCode,即自己现在能操作的文章
            //备注:GetLikeSql(badmin.GetAdminLogin().RoleList)格式: Where a.PRole Like '%,1,%' OR a.PRole Like '%,2,%'
            //备注:tb的NodeID自动重命名为NodeID1
            //string sql2 = "Select * From (Select a.*,b.FID From ZL_CommonModel as a Left Join ZL_NodeBindDroit as b on a.NodeID=b.NodeID Where a.[status] not in (99,-1,-2,-3) and b.Fid is not null And b.Fid > 0) as ta Left Join (Select a.*,b.NodeID From ZL_Process as a Left Join ZL_NodeBindDroit as b on a.PFlowID=b.FID " + GetLikeSql(roles) + ")as tb on ta.NodeID=tb.NodeID And ta.[Status]=tb.[NeedCode] Where tb.[NeedCode] is not null And TableName ='ZL_C_Article'";
            //if (tnid > 0)
            //{
            //    dt.DefaultView.RowFilter = "NodeID = " + tnid;
            //    dt = dt.DefaultView.ToTable();
            //}
            //return dt;
            return null;
        }
        public void UpHits(int InfoID)
        {
            DBCenter.UpdateSQL(TbName, "Hits=Hits+1", "GeneralID=" + InfoID);
        }
        public bool UpdateByID(M_CommonData model)
        {
            return DBCenter.UpdateByID(model, model.GeneralID);
        }
        public bool Update(M_CommonData model) { return UpdateByID(model); }
        public void UpdateOrder(int mid, int oid)
        {
            DBCenter.UpdateSQL(TbName, "OrderID = " + oid, "GeneralID=" + mid);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName,PK, ID);
        }
        /// <summary>
        /// 批量移除内容所属id
        /// </summary>
        public bool DelSpecID(string ids, int specid)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.UpdateSQL(TbName, "SpecialID=REPLACE(SpecialID,'," + specid + ",','')", "GeneralID IN (" + ids + ")");
        }
        /// <summary>
        /// 根据NodeID获取内容,不包含放入回收站的
        /// </summary>
        public DataTable GetContentByNode(int nodeID)
        {
            return DBCenter.Sel(TbName, "NodeID=" + nodeID + "And Status=99", "CreateTime DESC");
        }
        public DataTable GetContenByNodeOA(int nodeID)
        {
            return DBCenter.Sel(TbName, "(NodeID=" + nodeID + " OR FirstNodeID =" + nodeID + " ) And Status=99", "CreateTime DESC");
        }
        /// <summary>
        /// 根据NodeIDS,批量获取内容
        /// </summary>
        /// <param name="nodeIDS">1,2,3</param>
        public DataTable GetContentByNodeS(string ids, string inputer = "", int status = 99)
        {
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "NodeID in (" + ids + ") And Status=" + status;
            if (!string.IsNullOrEmpty(inputer))
            {
                sp.Add(new SqlParameter("Inputer", inputer));
                where += "Inputer=@Inputer";
            }
            return DBCenter.Sel(TbName, where, "CreateTime DESC", sp);
        }
        /// <summary>
        /// 获取OA模型下的节点,不能绑定多个节点
        /// </summary>
        public string GetNodeIDS(int modelID)
        {
            DataTable dt = DBCenter.Sel(TbName, "ContentModel=" + modelID);
            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += dt.Rows[i][0].ToString() + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
        public DataTable GetCommModelsByNodeId(int NodeID)
        {
            string where = NodeID > 0 ? "nodeid=" + NodeID : "";
            where += " And tablename like 'ZL_C_%'";
            return DBCenter.Sel(TbName, where, "CreateTime desc");
        }
        public DataTable PRoContent(string GeneralIDs, int NodeID)
        {
            SafeSC.CheckIDSEx(GeneralIDs);
            string where = "GeneralID in (" + GeneralIDs + ") and NodeID=" + NodeID;
            return DBCenter.Sel(TbName, where);
        }
        public bool Del_itemID(string table, int itemID)
        {
            SafeSC.CheckDataEx(table);
            return DBCenter.Del(table, "ItemID", itemID);
        }
        public DataTable GetContentByItems(int gid)
        {
            M_CommonData model = SelReturnModel(gid);
            return GetContentByItems(model.TableName,model.GeneralID);
        }
        /// <summary>
        /// 获取附加表中的数据
        /// </summary>
        public DataTable GetContentByItems(string tablename, int Gid)
        {
            SafeSC.CheckDataEx(tablename);
            //if (!DBHelper.Table_IsExist(tablename)) { return new DataTable(); }
            return DBCenter.JoinQuery("*", TbName, tablename, "A.ItemID=B.ID", "A.GeneralID=" + Gid);
        }
        /// <summary>
        /// 获取指定用户输入的内容(用户中心使用)
        /// </summary>
        /// <param name="NodeID">栏目节点ID</param>
        /// <param name="flag">状态标志 表示未审核、审核、退稿、删除到回收站等状态</param>
        /// <param name="inputer">输入内容的用户名</param>
        /// <returns>数据表</returns>
        public DataTable ContentListUser(int NodeID, string flag, string inputer, string keyword, int ty, string status)
        {
            List<SqlParameter> splist = new List<SqlParameter>() { new SqlParameter("inputer", inputer) };
            string sqlWhere = "Inputer=@inputer";
            if (NodeID != 0) { sqlWhere += " AND NodeID=" + NodeID; }
            if (keyword.Length > 0)
            {
                if (flag == "UnAudit")
                    sqlWhere += " AND Status=0";
                if (flag == "Audit")
                    sqlWhere += " AND Status=99";
                if (flag == "Reject")
                    sqlWhere += " AND Status=-1";
                if (!string.IsNullOrEmpty(keyword))
                {
                    if (ty == 0) { splist.Add(new SqlParameter("keyword", "%" + keyword + "%")); sqlWhere += " AND Title like @keyword"; }
                    else { sqlWhere += " AND GeneralID=" + DataConverter.CLng(keyword); }
                }
            }
            //默认不抽回收站
            if (!string.IsNullOrEmpty(status))
            {
                sqlWhere += " AND status=" + Convert.ToInt32(status);
            }
            else
            {
                sqlWhere += " AND status!=-2";
            }
            sqlWhere += " And tablename like 'ZL_C_%'";
            return DBCenter.Sel(TbName, sqlWhere, "OrderID Desc, GeneralID Desc", splist);
        }
        public DataTable ContentModelList(int ModelID, string flag, string inputer)
        {
            string where = "";
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "Audit") { where = "Status=99"; }
                else if (flag == "Elite") { where = "EliteLevel>0"; }
                else { where = "Status<>99 and Status<>-2"; }
            }
            else
            {
                where = "Status<>-2";
            }

            where += " AND Inputer=@Inputer AND ModelID = " + ModelID;
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("Inputer", inputer) };
            return DBCenter.Sel(TbName, "", "GeneralID Desc", sp);
        }
        //仅MyContent使用
        public DataTable PageContentList(string TableName, string flag, string Inputer, int Nodeid, string keyword)
        {
            string strSql = "";
            string filter = "";
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "Audit")
                    filter = "Status=99 ";
                else
                {
                    if (flag == "Elite")
                        filter = "EliteLevel>0 ";
                    else
                        filter = "Status<>99 and Status<>-2 ";
                }
            }
            else
            {
                filter = "Status<>-2 ";
            }
            string where = "";
            if (keyword != "")
            {
                where = "and Title Like @keyword ";
            }
            if (Nodeid > 0)
            {
                where = where + "and Nodeid = " + Nodeid + "";
            }
            strSql += "TableName like @TableName and Inputer = @Inputer " + where + " and " + filter;
            List<SqlParameter> sp = new List<SqlParameter>()
            { 
                new SqlParameter("Inputer", Inputer),
                new SqlParameter("keyword", "%"+keyword+"%"),
                new SqlParameter("TableName",TableName+"%")
            };
            return DBCenter.Sel(TbName, strSql, "OrderID Desc", sp);
        }
        /// <summary>
        /// 读取回收站内的内容列表
        /// </summary>
        public DataTable GetContentRecycle(int NodeID)
        {
            string where = "Status=" + (int)ConStatus.Recycle + " And TableName like 'ZL_C_%'";
            if (NodeID > 0) {
                string childNodes = new B_Node().GetChilds(NodeID);
                where += " AND A.NodeID IN(" + childNodes + ")";
            }
            return SqlHelper.JoinQuery("A.*,B.NodeName", TbName, "ZL_Node", "A.NodeID=B.NodeID", where, "GeneralID DESC");
        }
        #region 黄页相关
        ///<summary>
        ///清空回收站
        /// <summary>
        public bool Page_ClearRecycle()
        {
            return DBCenter.DelByWhere(TbName, "Status=-2 And TableName like 'ZL_Page_%'");
        }
        ///<summary>
        ///根据指定条件搜索黄页回收站
        ///<summary>
        public DataTable Page_GetRecycle(string key = "", string uname = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "Status=-2 And tablename like 'ZL_Page_%'";
            if (!string.IsNullOrEmpty(key))
            {
                sp.Add(new SqlParameter("key", "%" + key + "%"));
                where += " AND Title Like @key";
            }
            if (!string.IsNullOrEmpty(uname)) { sp.Add(new SqlParameter("uname", uname)); where += " AND Inputer=@uname"; }
            return DBCenter.Sel(TbName, where, "GeneralID DESC", sp);
        }
        public int Page_Add(DataTable ContentDT, M_PageReg model)
        {
            if (!string.IsNullOrEmpty(model.TableName) && ContentDT != null && ContentDT.Rows.Count > 0)
            {
                model.InfoID = DBCenter.Insert(model.TableName, BLLCommon.GetFields(ContentDT), BLLCommon.GetParas(ContentDT), BLLCommon.GetParameters(ContentDT).ToArray());
            }
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 更新内容
        /// </summary>
        public void Page_Update(DataTable ContentDT, M_PageReg model)
        {
            int ItemID = model.InfoID;
            if (ContentDT != null && ContentDT.Rows.Count > 0)
            {
                if (DBCenter.IsExist(model.TableName, "ID=" + ItemID))
                {
                    DBCenter.UpdateSQL(model.TableName, BLLCommon.GetFieldAndPara(ContentDT), "ID = " + ItemID, BLLCommon.GetParameters(ContentDT));
                }
                else
                {
                    DBCenter.Insert(model.TableName, BLLCommon.GetFields(ContentDT), BLLCommon.GetParas(ContentDT), BLLCommon.GetParameters(ContentDT).ToArray());
                }
            }
            DBCenter.UpdateByID(model, model.ID);
        }
        #endregion
        public DataTable ContentSearch(string filter, List<SqlParameter> sp = null)
        {
            return DBCenter.Sel(TbName, filter, "", sp);
        }
        public int insert(M_CommonData model)
        {
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 添加内容到数据库
        /// </summary>
        public int AddContent(DataTable ContentDT, M_CommonData model)
        {
            int itemid = 0;
            if (!string.IsNullOrEmpty(model.TableName) && ContentDT.Rows.Count > 0)
            {
                itemid = DBCenter.Insert(model.TableName, BLLCommon.GetFields(ContentDT), BLLCommon.GetParas(ContentDT), BLLCommon.GetParameters(ContentDT).ToArray());
            }
            model.ItemID = itemid;
            model.OrderID = DataConvert.CLng(DBCenter.ExecuteScala(TbName, "max(OrderID)", ""));
            return DBCenter.Insert(model);
        }
        //------------
        /// <summary>
        /// 更新内容
        /// </summary>
        public void UpdateContent(DataTable ContentDT, M_CommonData model)
        {
            //需要重处理
            int ItemID = model.ItemID;
            if (ContentDT != null && ContentDT.Rows.Count > 0)
            {
                List<SqlParameter> splist = new List<SqlParameter>();
                splist.AddRange(BLLCommon.GetParameters(ContentDT));
                if (DBCenter.IsExist(model.TableName, "ID=" + ItemID))
                {
                    DBCenter.UpdateSQL(model.TableName, BLLCommon.GetFieldAndPara(ContentDT), "ID=" + ItemID, splist);
                }
                else
                {
                    DBCenter.Insert(model.TableName, BLLCommon.GetFields(ContentDT), BLLCommon.GetParas(ContentDT), splist.ToArray());
                }
            }
            UpdateByID(model);
        }
        public void GetContent(int GeneralID, ref M_CommonData cdata, ref DataTable table)
        {
            cdata = SelReturnModel(GeneralID);
            table = DBCenter.Sel(cdata.TableName, "ID=" + cdata.ItemID);
        }
        /// <summary>
        /// 指内容的附加表中数据
        /// </summary>
        public DataTable GetContent(int GeneralID)
        {
            M_CommonData model = SelReturnModel(GeneralID);
            return DBCenter.Sel(model.TableName, "ID=" + model.ItemID);
        }
        //移动内容
        public bool MoveContent(string ContentIDS, int NodeID)
        {
            SafeSC.CheckIDSEx(ContentIDS);
            string tree = GetParentTree(NodeID);
            return DBCenter.UpdateSQL(TbName, "NodeID=" + NodeID + ",ParentTree='" + tree + "'", "GeneralID IN (" + ContentIDS + ")");
        }
        /// <summary>
        /// 移入回收站
        /// </summary>
        public void SetDel(int GeneralID)
        {
            DBCenter.UpdateSQL(TbName, "Status=" + (int)ConStatus.Recycle, "GeneralID=" + GeneralID);
        }
        public void SetAudit(int GeneralID, int status)
        {
            DBCenter.UpdateSQL(TbName, "Status=" + status+ ",AuditTime='"+DateTime.Now.ToString()+"'", "GeneralID=" + GeneralID);
        }
        public DataTable SelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.Sel(TbName, "GeneralID IN (" + ids + ")");
        }
        /// <summary>
        /// 批量修改内容状态
        /// </summary>
        public void SetAuditByIDS(string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=" + status, "GeneralID IN (" + ids + ")");
        }
        public void ResetAll()
        {
            DBCenter.UpdateSQL(TbName, "Status=0", "Status=" + (int)ConStatus.Recycle);
        }
        public void Reset(int id)
        {
            UpdateStatus(id.ToString(), 0);
        }
        public void Reset(string ids)
        {
            UpdateStatus(ids, 0);
        }
        public void UpdateStatus(int gid, int status)
        {
            UpdateStatus(gid.ToString(), status);
        }
        public void UpdateStatus(string ids, int status, string inputer = "")
        {
            if (string.IsNullOrEmpty(ids)) return;
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = PK + " IN (" + ids + ") ";
            if (!string.IsNullOrEmpty(inputer)) { where += " AND Inputer=@inputer"; sp.Add(new SqlParameter("inputer", inputer)); }
            DBCenter.UpdateSQL(TbName, "Status=" + status, where, sp);
        }
        /// <summary>
        /// 后台清空回收站
        /// </summary>
        public void DelRecycle()
        {
            DataTable dt = DBCenter.Sel(TbName, "Status=" + (int)ZLEnum.ConStatus.Recycle);
            foreach (DataRow dr in dt.Rows)
            {
                DelContent(dr["TableName"].ToString(), DataConvert.CLng(dr["ItemID"]), DataConvert.CLng(dr["GeneralID"]));
            }
        }
        /// <summary>
        /// 直接移除内容与附表数据
        /// </summary>
        public void DelContent(string ids, string inputer)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            foreach (string id in ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                M_CommonData model = GetCommonData(Convert.ToInt32(id));
                if (model == null) { continue; }
                if (!string.IsNullOrEmpty(inputer) && !model.Inputer.Equals(inputer)) { continue; }
                DelContent(model.TableName, model.ItemID, model.GeneralID);
            }
        }
        public void DelContent(int GeneralID) { DelContent(GeneralID.ToString(),""); }
        public void DelContent(string tbname, int itemid, int gid)
        {
            if (DBHelper.Table_IsExist(tbname))
            {
                DBCenter.Del(tbname, "ID", itemid);
            }
            DBCenter.Del(TbName, PK, gid);
        }
        public DataTable GetCreateAllList()
        {
            return DBCenter.Sel(TbName, "IsCreate=0");
        }
        public DataTable GetCreateIDList(int startID, int endID)
        {
            return DBCenter.Sel(TbName, "(GeneralID>" + startID + " AND GeneralID<" + endID + ") AND IsCreate=0", "GeneralID DESC");
        }
        public DataTable GetCreateDateList(DateTime startTime, DateTime endTime)
        {
            string where = DBCenter.GetDateSql("CreateTime",startTime.ToString(), endTime.ToString());
            return DBCenter.Sel(TbName, where, "GeneralID DESC");
        }
        public DataTable GetCreateNodeList(string nodeids)
        {
            SafeSC.CheckIDSEx(nodeids);
            return DBCenter.Sel(TbName, "NodeID IN(" + nodeids + ")", "GeneralID DESC");
        }
        public DataTable GetCreateCountList(int Count)
        {
            return DBCenter.SelTop(Count, PK, "*", TbName, "", "GeneralID Desc");
        }
        public void UpdateCreate(int GeneralID, string Template)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("Template", Template) };
            DBCenter.UpdateSQL(TbName, "IsCreate=1,HtmlLink=@Template", "GeneralID=" + GeneralID, sp);
        }
        //新增方法
        public void UpdateCreate1(int GeneralID)
        {
            DBCenter.UpdateSQL(TbName, "IsCreate=0", "GeneralID=" + GeneralID);
        }
        public bool UpTemplata(int GeneralID, string tempstr)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("temp", tempstr) };
            return DBCenter.UpdateSQL(TbName, "Template=@temp", "GeneralID=" + GeneralID, sp);
        }
        public int GetPreID(int InfoID)
        {
            int NodeID = GetCommonData(InfoID).NodeID;
            int prid = DataConvert.CLng(DBCenter.ExecuteScala(TbName, "GeneralID", "NodeID=" + NodeID + " AND GeneralID<" + InfoID, "GeneralID DESC"));
            if (prid == 0) { prid = InfoID; }
            return prid;
        }
        public int GetNextID(int InfoID)
        {
            int NodeID = GetCommonData(InfoID).NodeID;
            int prid = DataConvert.CLng(DBCenter.ExecuteScala(TbName, "GeneralID", "NodeID=" + NodeID + " AND GeneralID>" + InfoID, "GeneralID ASC"));
            if (prid == 0) { prid = InfoID; }
            return prid;
        }
        public DataTable GetCommonByItem(int ModelID, int NodeID, int ItemID)
        {
            return DBCenter.Sel(TbName, "ModelID=" + ModelID + " and NodeID=" + NodeID + " and ItemID=" + ItemID);
        }
        /// <summary>
        /// 根据用户名删除内容
        /// </summary>
        public bool DeCommonModel(string name)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("name", name) };
            return DBCenter.DelByWhere(TbName, "Inputer=@name", sp);
        }
        /// <summary>
        /// 更新内容审核状态(将所有状态改为已审核)
        /// </summary>
        /// <param name="nodeid"></param>
        public void UpdateState(int nodeid)
        {
            DBCenter.UpdateSQL(TbName, "Status=99", "NodeID=" + nodeid);
        }
        public DataTable GetContentAll()
        {
            return Sel();
        }
        public bool UpdateOrder(M_CommonData Cdata)
        {
            return DBCenter.UpdateSQL(TbName, "OrderId=" + Cdata.OrderID, "GeneralID=" + Cdata.GeneralID);
        }
        /// <summary>
        /// 获取指定inputer的文章
        /// </summary>
        public DataTable ContentListUser(int NodeID, string inputer, int type)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("inputer", inputer) };
            string where = "Inputer=@inputer";
            if (NodeID > 0) { where += " AND NodeID=" + NodeID; }
            if (type != 0) { where += " AND OrederClass=" + type; }
            return DBCenter.Sel(TbName, where, "OrderID Desc, GeneralID Desc", sp);
        }
        /// <summary>
        /// 获取指定inputer的文章
        /// </summary>
        public DataTable ContentListUser(int NodeID, string inputer)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("inputer", inputer) };
            string where = "Inputer=@inputer";
            if (NodeID > 0) { where += " AND NodeID=" + NodeID; }
            return DBCenter.Sel(TbName, where, "GeneralID DESC",sp);
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="field">查询的字段名</param>
        /// <param name="value">查询的字段值</param>
        /// <returns></returns>
        public DataTable CountData(string field, string value)
        {
            DataTable dt = new DataTable();
            switch (field)
            {
                case "subject":
                    dt.Columns.Add("ID");
                    dt.Columns.Add("栏目名称");
                    dt.Columns.Add("合计");
                    break;
                //case "user":
                //    dt.Columns.Add("ID");
                //    dt.Columns.Add("用户");
                //    dt.Columns.Add("合计");
                //    break;
                case "manager":
                    dt.Columns.Add("ID");
                    dt.Columns.Add("管理员");
                    dt.Columns.Add("合计");
                    break;

                case "dateTime":
                    dt.Columns.Add("起始时间");
                    dt.Columns.Add("结束时间");
                    dt.Columns.Add("合计");
                    break;
            }
            if (field == "dateTime")
            {
                dt.Rows.Add(new object[] { value.Split('|')[0], value.Split('|')[1], "共计" + CountDatas("CreateTime", value) + "篇文章" });
            }
            else
            {
                string[] strArr = value.Split(',');
                B_Node nodeBll = new B_Node();
                foreach (string str in strArr)
                {
                    M_Node nodeMod = nodeBll.SelReturnModel(DataConvert.CLng(str));
                    switch (field)
                    {
                        case "subject":
                            dt.Rows.Add(new object[] { str, nodeMod.NodeName, "共计" + CountDatas("NodeID", str) + "篇文章" });
                            break;
                        //case "user":
                        //    B_User b_user = new B_User();
                        //    M_UserInfo m_userinfo = b_user.GetUserByName(str);
                        //    dt.Rows.Add(new object[] { m_userinfo.UserID, str, "共计" + dal.CountData("Inputer", str) + "篇文章" });
                        //    if (dal.CountData("Inputer", str).Rows[0]["countNum"].ToString() != "0")
                        //        dt.Rows.Add(new object[] { "其中(ID)", "", "" });
                        //    break;
                        case "manager":
                            M_AdminInfo m_info = B_Admin.GetAdminByAdminName(str);

                            dt.Rows.Add(new object[] { m_info.AdminId, str, "共计" + CountDatas("Inputer", str) + "篇文章" });
                            if (CountDatas("Inputer", str) != 0) { dt.Rows.Add(new object[] { "其中(ID)", "", "" }); }
                            break;
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="field">查询的字段名</param>
        /// <param name="value">查询的字段值</param>
        /// <returns></returns>
        public int CountDatas(string field, string value)
        {
            string where = "";
            List<SqlParameter> sp = new List<SqlParameter>();
            switch (field)
            {
                case "NodeID":
                    SafeSC.CheckIDSEx(value);
                    where = "NodeID IN (" + value + ")";
                    break;
                case "Inputer":
                    string paramStr = "";//@name
                    var valArr = value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < valArr.Length; i++)
                    {
                        paramStr += "@str" + i + ",";
                        sp.Add(new SqlParameter(paramStr.Trim(','), valArr[i]));
                    }
                    paramStr = paramStr.TrimEnd(',');
                    where = "Inputer IN (" + paramStr + ") AND NodeID >0";
                    break;
                case "CreateTime":
                    string stime = Convert.ToDateTime(value.Split('|')[0]).ToString();//防注
                    string etime = Convert.ToDateTime(value.Split('|')[1]).ToString();
                    where = DBCenter.GetDateSql("CreateTime", stime, etime);
                    break;
            }
            return DBCenter.Count(TbName, where, sp);
        }
        private string stringTreeNodes(int ParentID, string nodess)
        {
            string nodes = nodess;
            DataTable dt = GetNodeListS(ParentID);
            foreach (DataRow dr in dt.Rows)
            {
                if (nodes == "")
                {
                    nodes += dr["NodeID"].ToString();
                }
                else
                {
                    nodes += "," + dr["NodeID"].ToString();
                }

                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    nodes = stringTreeNodes(DataConverter.CLng(dr["NodeID"].ToString()), nodes);
                }
            }
            return nodes;
        }
        private DataTable GetNodeListS(int ParentID)
        {
            return DBCenter.Sel("ZL_Node", "NodeType=1 AND NodeListType<>6 AND ParentID=" + ParentID, "OrderID");
        }
        public string returnnodelist = "";
        /// <summary>
        /// 获得父级树
        /// </summary>
        /// <returns></returns>
        public string GetParentTree(int NodeID)
        {
            M_Node nodelist =new B_Node().SelReturnModel(NodeID);
            returnnodelist = NodeID + "," + returnnodelist;
            if (nodelist.NodeID > 0 && nodelist.ParentID > 0)
            {
                GetParentTree(nodelist.ParentID);
            }
            if (returnnodelist != "")
            {
                if (BaseClass.Left(returnnodelist, 1) != ",")
                {
                    returnnodelist = "," + returnnodelist;
                }
                if (BaseClass.Right(returnnodelist, 1) != ",")
                {
                    returnnodelist = returnnodelist + ",";
                }
            }
            return returnnodelist;
        }
        /// <summary>
        /// 交换序号，按格式，切割后使用
        /// </summary>
        /// <param name="id1">1:358</param>
        /// <param name="id2">2:359</param>
        public void SwitchOrderID(string id1, string id2)
        {
            int mid1 = Convert.ToInt16(id1.Split(':')[0]);
            int oid1 = Convert.ToInt16(id1.Split(':')[1]);
            int mid2 = Convert.ToInt16(id2.Split(':')[0]);
            int oid2 = Convert.ToInt16(id2.Split(':')[1]);
            DBCenter.UpdateSQL(TbName, "OrderID=" + oid1, "GeneralID=" + mid1);
            DBCenter.UpdateSQL(TbName, "OrderID=" + oid2, "GeneralID=" + mid2);
        }
        /// <summary>
        /// 是否有重名文章存在,True存在
        /// </summary>
        public bool IsExist(int nodeID, string title)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("title", title.Trim()) };
            string where = "Title=@title";
            if (nodeID > 0) { where += " AND NodeID=" + nodeID; }
            return DBCenter.IsExist(TbName, where, sp);
        }
        /// <summary>
        /// 工黄页批量删除
        /// </summary>
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        /// <summary>
        /// 黄页批量还原
        /// </summary>
        public bool RecByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.UpdateSQL(TbName, "status = 99", "GeneralID IN (" + ids + ")");
        }
        /// <summary>
        /// 获取我的店铺,如未通过则报错
        /// </summary>
        public M_CommonData SelMyStore_Ex()
        {
            M_CommonData storeMod = SelMyStore(new B_User().GetLogin().UserName);
            if (storeMod == null) { function.WriteErrMsg("你尚未开通店铺"); }
            else if (storeMod.Status != (int)ConStatus.Audited) { function.WriteErrMsg("你的店铺尚未通过审核"); }
            return storeMod;
        }
        public M_CommonData SelMyStore(string uname)
        {
            if (string.IsNullOrEmpty(uname)) { return null; }
            string sql = "Inputer=@uname AND TableName LIKE '%ZL_Store_%'";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uname", uname) };
            return SelReturnModel(sql, sp);
        }
        //用于获取用户店铺ID
        public string GetGeneralID(string username)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("Inputer", username) };
            return DataConvert.CStr(DBCenter.ExecuteScala(TbName, "GeneralID DESC", "TableName like 'ZL_Store_%' And Inputer=@Inputer", "",sp));
        }
        public DataTable SelBySkey(string skey)
        {
            string where = "TableName LIKE '%ZL_Store_%' AND (Title Like @skey {0})";
            if (DataConvert.CLng(skey) > 0)
            {
                where = string.Format(where, "OR GeneralID=" + DataConvert.CLng(skey));
            }
            else
            {
                where = string.Format(where, "");
            }
            return DBCenter.Sel(TbName, where, "", new List<SqlParameter> { new SqlParameter("skey", "%" + skey + "%") });
        }
        //获取第一个多文本Html字段中的内容,用于微信引入等
        public string GetHtmlContent(int gid)
        {
            M_CommonData datamodel = SelReturnModel(gid);
            B_ModelField fieldBll = new B_ModelField();
            DataTable dt = fieldBll.GetModelFieldListall(datamodel.ModelID);
            DataRow[] drs = dt.Select("FieldName='content'");
            if (drs.Length <= 0)
                drs = dt.Select("FieldType='MultipleHtmlType'");
            if (drs.Length > 0)
            {
                DataTable contentdt = GetContent(gid);
                return contentdt.Rows[0][drs[0]["FieldName"].ToString()].ToString();
            }
            else
            {
                return "";
            }
        }
        //根据节点ID获取表名(节点不应有多模型,节点中数据不应推送)
        public string GetTbNameByNid(int nid)
        {
            return DBCenter.ExecuteScala(TbName, "TableName", "NodeID=" + nid).ToString();
        }
        /// <summary>
        /// 用于listHtmlContent
        /// </summary>
        public DataTable SelForList(int stype, string skey, int otype, string okey)
        {
            string strWhere = "A.HtmlLink IS NOT NULL AND A.HtmlLink !='' ";
            string order = "CreateTime DESC";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("skey", "%" + skey + "%") };
            if (!string.IsNullOrEmpty(skey))
            {
                switch (stype)
                {
                    case 1:
                        strWhere += "AND A.Title LIKE @skey";
                        break;
                    case 2:
                        strWhere += "AND A.GeneralID LIKE @skey";
                        break;
                    case 3:
                        strWhere += "AND A.Inputer LIKE @skey";
                        break;
                }
            }
            switch (okey)
            {
                case "desc":
                    okey = " DESC";
                    break;
                default:
                    okey = " ASC";
                    break;
            }
            if (otype > 0)
            {
                switch (otype)
                {
                    case 1://GeneralID
                        order = "GeneralID " + okey;
                        break;
                    case 2://CreateTime
                        order = "CreateTime " + okey;
                        break;
                    case 3://UpDateTime
                        order = "UpDateTime " + okey;
                        break;
                    case 4://Hits
                        order = "Hits " + okey;
                        break;
                    case 5://EliteLevel
                        order = "EliteLevel " + okey;
                        break;
                }
            }
            return DBCenter.JoinQuery("A.*,B.NodeName", "ZL_CommonModel", "ZL_Node", "A.NodeID=B.NodeID", strWhere, order, sp);
        }
        /// <summary>
        /// 查找某个节点，某个状态下的内容(用于生成发布)
        /// </summary>
        /// <param name="Nodeid">节点ID</param>
        /// <param name="status">内容状态</param>
        /// <returns></returns>
        public DataTable GetNodeAri(int Nodeid, int status = 1000)
        {
            string where = "NodeID=" + Nodeid + "  And tablename like 'ZL_C_%' ";
            if (status != 1000)
            {
                where += " AND Status=" + status;
            }
            return DBCenter.Sel(TbName, where, "CreateTime DESC");
        }
        #region Tools
        public static string GetFieldAlias(string field, DataTable dt)
        {
            if (dt == null || dt.Rows.Count < 1 || string.IsNullOrEmpty(field)) { return "未定义"; }
            DataRow[] drs = dt.Select("FieldName='" + field + "'");
            return drs.Length > 0 ? drs[0]["FieldAlias"].ToString() : "未定义";
        }
        // 生成Like语句,仅用于GetDTByAuth
        private string GetLikeSql(string roles)
        {
            string[] roleArr = roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "Where ";
            if (roleArr.Length < 1) return "";
            for (int i = 0; i < roleArr.Length; i++)
            {
                result += " ','+a.PRole Like '%," + roleArr[i] + ",%' OR ";
            }
            result = result.TrimEnd("OR ".ToCharArray());
            return result;
        }
        private string GetNodeParent(int NodeID)
        {
            string returnlist = NodeID.ToString();
            string where = "";
            if (NodeID > 0)
            {
                where += "ParentID=" + NodeID;
            }
            DataTable dt = DBCenter.Sel("ZL_Node", where);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                returnlist = returnlist + "," + dt.Rows[i]["NodeID"];
                returnlist = returnlist + GetNodeParentList(DataConverter.CLng(dt.Rows[i]["NodeID"]));
            }
            return returnlist;
        }
        private string GetNodeParentList(int NodeID)
        {
            string returnlist = "";
            DataTable dt = DBCenter.Sel(TbName, "ParentID=" + NodeID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                returnlist = returnlist + "," + dt.Rows[i]["NodeID"];
                returnlist = returnlist + GetNodeParentList(DataConverter.CLng(dt.Rows[i]["NodeID"]));
            }
            return returnlist;
        }
        private DataTable Selt(string table, int id)
        {
            return DBCenter.Sel(table, "ID=" + id);
        }
        private bool IsDate(string s)
        {
            try
            {
                DateTime dt = DateTime.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetEliteStr(object elite) 
        {
            switch (DataConvert.CLng(elite))
            {
                case 1:
                    return "已推荐";
                default:
                    return "未推荐";
            }
        }
        public static string GetStatusStr(int status)
        {
            switch (status)
            {
                case (int)ConStatus.Draft:
                    return "草稿";
                case (int)ConStatus.Recycle:
                    return "回收站";
                case (int)ConStatus.Reject:
                    return "退稿";
                case (int)ConStatus.UnAudit:
                    return "待审核";
                case (int)ConStatus.NotSure:
                    return "待确认";
                case (int)ConStatus.Audited:
                    return "已审核";
                case (int)ConStatus.Filed:
                    return "已归档";
                default:
                    return "" + status + "";
            }
        }
        #endregion
       
    }
}