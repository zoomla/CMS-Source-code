
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Design;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZoomLaCMS.Design
{
    /// <summary>
    /// design 的摘要说明
    /// </summary>
    public class design : API_Base, IHttpHandler
    {
        B_User buser = new B_User();
        B_Node nodeBll = new B_Node();
        B_Design_Page pageBll = new B_Design_Page();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        private int CPage { get { int _cpage = DataConvert.CLng(Req("cpage")); if (_cpage < 1) { _cpage = 1; } return _cpage; } }
        private int PSize { get { int _psize = DataConvert.CLng(Req("psize")); if (_psize < 1) { _psize = 10; } return _psize; } }
        public void ProcessRequest(HttpContext context)
        {
            //throw new Exception("该接口默认不开放,请联系管理员");
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            //if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
            //retMod.callback = CallBack;//暂不开放JsonP
            int siteID = DataConvert.CLng(Req("SiteID"));
            try
            {
                switch (Action)
                {
                    case "save":
                        #region 保存或更新
                        {
                            M_Design_Page model = JsonConvert.DeserializeObject<M_Design_Page>(Req("model"));
                            if (string.IsNullOrEmpty(model.guid))
                            {
                                model.guid = System.Guid.NewGuid().ToString();
                                model.UserID = mu.UserID;
                                model.UserName = mu.UserName;
                                pageBll.Insert(model);
                            }
                            else
                            {
                                M_Design_Page pageMod = pageBll.SelModelByGuid(model.guid);
                                pageMod.comp = model.comp;
                                pageMod.page = model.page;
                                pageMod.labelArr = model.labelArr;
                                pageMod.UPDate = DateTime.Now;
                                pageBll.UpdateByID(pageMod);
                            }
                            retMod.result = model.guid;
                            retMod.retcode = M_APIResult.Success;
                        }
                        #endregion
                        break;
                    //--------------节点
                    case "node_sel"://返回站点所拥有的节点,过滤掉用户名的所在节点
                        {
                            DataTable dt = Node_Sel(siteID);
                            dt.DefaultView.RowFilter = "NodeType IN (1)";
                            dt = dt.DefaultView.ToTable();
                            retMod.result = JsonConvert.SerializeObject(dt);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "node_del":
                        {
                            int nid = DataConvert.CLng(Req("nid"));
                            if (nid < 1) { retMod.retmsg = "未指定节点ID"; }
                            else
                            {
                                DBCenter.DelByWhere("ZL_Node", "NodeBySite=" + mu.SiteID + " AND NodeID=" + nid);
                                retMod.retcode = M_APIResult.Success;
                            }
                        }
                        break;
                    //--------------内容
                    case "content_selbynid"://获取用户站点下的文章,附表只返回简介
                        {
                            string nids = Req("nid");
                            retMod.result = JsonConvert.SerializeObject(Content_SelByNid(siteID, nids));
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "content_selbyid"://根据ID获取文章内容与正文
                        {
                            int gid = DataConvert.CLng(Req("Gid"));
                            retMod.result = JsonConvert.SerializeObject(Content_ByID(gid));
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "mb_getdata"://按条件筛选内容并返回
                        {
                            int id = DataConvert.CLng(Req("id"));
                            int psize = DataConvert.CLng(Req("psize"));//取几条数据
                            if (psize < 1) { psize = 100; }
                            string nodename = HttpUtility.UrlDecode(Req("nodename"));
                            string dbtype = Req("dbtype");
                            string flag = Req("flag");

                            List<SqlParameter> sp = new List<SqlParameter>();
                            string fields = "A.GeneralID AS id,A.title,A.hits,A.CreateTime AS cdate,B.wxsize,B.wxico,B.wxbk,B.wxlink,B.content,B.flag,B.dbtype";//固定返回这些字段
                            string where = " A.Status!=" + (int)ZLEnum.ConStatus.Recycle + " ";//去除回收站文件
                            if (!string.IsNullOrEmpty(nodename))
                            {
                                string filter = "SELECT NODEID FROM ZL_Node WHERE NodeBySite=" + siteID + " AND NodeName =@nodename";
                                where += " AND A.NodeID IN (" + filter + ")";
                                sp.Add(new SqlParameter("nodename", nodename));
                            }
                            if (!string.IsNullOrEmpty(flag)) { where += " AND B.flag=@flag"; sp.Add(new SqlParameter("flag", flag)); }
                            if (!string.IsNullOrEmpty(dbtype)) { where += " AND B.dbtype=@dbtype"; sp.Add(new SqlParameter("dbtype", dbtype)); }
                            //通过ID筛取其值会变动,需要通过标识符
                            if (id > 0) { where += " AND A.GeneralID=" + id; }
                            //retMod.retmsg = where;

                            DataTable dt = DBCenter.SelPage(new PageSetting()
                            {
                                cpage = 1,
                                psize = psize,
                                pk = "A.GeneralID",
                                fields = fields,
                                t1 = "ZL_CommonModel",
                                t2 = "ZL_C_Article",
                                on = "A.ItemID=B.ID",
                                where = where,
                                order = "A.GeneralID ASC",
                                sp = sp.ToArray()
                            });
                            //对数据进行处理,如链接等,以便于统一管理,并减轻客户端逻辑,或交由tools处理
                            retMod.result = JsonConvert.SerializeObject(dt);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "mb_pro_get"://获取商品数据
                        {
                            int id = DataConvert.CLng(Req("id"));
                            int psize = DataConvert.CLng(Req("psize"));
                            if (psize < 1) { psize = 100; }

                            List<SqlParameter> sp = new List<SqlParameter>();
                            string fields = "A.id,A.proname,A.Thumbnails AS pics,A.proinfo,A.procontent AS content,A.LinPrice AS price,A.AddTime AS cdate";//固定返回这些字段
                            string where = "A.Recycler=0 AND A.ParentID=" + siteID + " ";//去除回收站文件
                                                                                         //通过ID筛取其值会变动,需要通过标识符
                            if (id > 0) { where += " AND A.ID=" + id; }
                            DataTable dt = DBCenter.SelPage(new PageSetting()
                            {
                                cpage = 1,
                                psize = psize,
                                pk = "A.ID",
                                fields = fields,
                                t1 = "ZL_Commodities",
                                t2 = "ZL_P_Shop",
                                on = "A.ItemID=B.ID",
                                where = where,
                                order = "A.ID ASC",
                                sp = sp.ToArray()
                            });
                            //对数据进行处理,如链接等,以便于统一管理,并减轻客户端逻辑,或交由tools处理
                            retMod.result = JsonConvert.SerializeObject(dt);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    //--------------站点信息
                    case "site_sel":
                        {
                            M_Design_SiteInfo sfMod = sfBll.SelReturnModel(siteID);
                            DataTable dt = DBCenter.SelWithField("ZL_Design_SiteInfo", "ID,SiteName,Logo", "ID=" + siteID);
                            retMod.result = FirstToJson(dt);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    //--------------用户(SiteID)
                    case "user_sel":
                        {
                            //DataTable dt = DBCenter.SelWithField("ZL_User", "UserID,UserName", "SiteID=" + siteID);
                            //retMod.result = JsonConvert.SerializeObject(dt);
                            //retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    default:
                        {
                            retMod.retmsg = "[" + Action + "]接口不存在";
                        }
                        break;
                }
            }
            catch (Exception ex) { retMod.retmsg = ex.Message; }
            RepToClient(retMod);
        }

        public bool IsReusable { get { return false; } }
        //---------------节点
        private DataTable Node_Sel(int siteID)
        {
            return DBCenter.SelWithField("ZL_Node", "NodeID,NodeName,NodeType", "NodeBySite=" + siteID, "OrderID DESC");
        }
        //---------------内容
        private DataTable Content_ByID(int gid)
        {
            return DBCenter.JoinQuery("*", "ZL_CommonModel", "ZL_C_Article", "A.ItemID=B.ID", "A.GeneralID=" + gid);
        }
        private DataTable Content_SelByNid(int siteID, string nids)//需要改为不依靠登录用户获取
        {
            string where = "";
            if (string.IsNullOrEmpty(nids))
            {
                DataTable dt = Node_Sel(siteID);
                foreach (DataRow dr in dt.Rows)
                {
                    nids += dr["NodeID"] + ",";
                }
                nids = nids.TrimEnd(',');
            }
            SafeSC.CheckIDSEx(nids);
            where += " A.NodeID IN(" + nids + ")";
            PageSetting setting = new PageSetting()
            {
                cpage = CPage,
                psize = PSize,
                fields = "A.*,B.synopsis",
                t1 = "ZL_CommonModel",
                t2 = "ZL_C_Article",
                on = "A.ItemID=B.ID",
                where = where,
                order = "A.GeneralID DESC"
            };
            return DBCenter.SelPage(setting);
        }
        //---------------Common
        private string FirstToJson(DataTable dt)
        {
            string json = "";
            DataRow dr = dt.Rows[0];
            foreach (DataColumn col in dt.Columns)
            {
                json += "\"" + col.ColumnName + "\":\"" + dr[col.ColumnName] + "\",";
            }
            json = json.TrimEnd(',');
            json = "{" + json + "}";
            return json;
        }
    }
}