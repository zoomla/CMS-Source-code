<%@ WebHandler Language="C#" Class="update" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Design;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//用于处理前端的更新请求
public class update :API_Base,IHttpHandler {
    B_User buser = new B_User();
    B_Content conBll = new B_Content();
    B_Product proBll = new B_Product();
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        int siteID = DataConvert.CLng(Req("SiteID"));
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "mb_list":
                    #region
                    {
                        string fields = "wxico,wxsize,wxbk,flag,wxlink,content,dbtype";
                        DataTable dt = GetListDT();
                        foreach (DataRow dr in dt.Rows)
                        {
                            M_CommonData conMod = conBll.SelReturnModel(Convert.ToInt32(dr["id"]));
                            //if (!conMod.Inputer.Equals(mu.UserName)) { continue; }
                            List<SqlParameter> sp1 = GetSPByDR(dr, "title");
                            List<SqlParameter> sp2 = GetSPByDR(dr, fields);
                            DBCenter.UpdateSQL("ZL_CommonModel", GetSet(sp1), "GeneralID=" + conMod.GeneralID, sp1);
                            DBCenter.UpdateSQL("ZL_C_Article", GetSet(sp2), "ID=" + conMod.ItemID, sp2);
                        }
                        retMod.retcode = M_APIResult.Success;
                    }
                    #endregion
                    break;
                case "mb_nav":
                    {
                        DataTable dt = GetListDT();
                        foreach (DataRow dr in dt.Rows)
                        {
                            M_CommonData conMod = conBll.SelReturnModel(Convert.ToInt32(dr["id"]));
                            //if (!conMod.Inputer.Equals(mu.UserName)) { continue; }
                            List<SqlParameter> sp1 = new List<SqlParameter>() { new SqlParameter("title", dr["title"].ToString()) };
                            List<SqlParameter> sp2 = new List<SqlParameter>() { new SqlParameter("wxico", dr["wxico"].ToString()) };
                            DBCenter.UpdateSQL("ZL_CommonModel", "Title=@title", "GeneralID=" + conMod.GeneralID, sp1);
                            DBCenter.UpdateSQL("ZL_C_Article", "wxico=@wxico", "ID=" + conMod.ItemID, sp2);
                        }
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "mg_footbar":
                    {
                        DataTable dt = GetListDT();
                        foreach (DataRow dr in dt.Rows)
                        {
                            M_CommonData conMod = conBll.SelReturnModel(Convert.ToInt32(dr["id"]));
                            //if (!conMod.Inputer.Equals(mu.UserName)) { continue; }
                            List<SqlParameter> sp1 = new List<SqlParameter>() { new SqlParameter("title", dr["title"].ToString()) };
                            List<SqlParameter> sp2 = new List<SqlParameter>() { new SqlParameter("wxico", dr["wxico"].ToString()) };
                            DBCenter.UpdateSQL("ZL_CommonModel", "Title=@title", "GeneralID=" + conMod.GeneralID, sp1);
                            DBCenter.UpdateSQL("ZL_C_Article", "wxico=@wxico", "ID=" + conMod.ItemID, sp2);
                        }
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "mb_image"://更新单个内容的指定字段,是否需要扩展为,根据字段更新目标表,以减少代码
                    {
                        string flag = Req("flag");//传了该值,则代表单条数据更新
                        DataTable dt = GetListDT();
                        foreach (DataRow dr in dt.Rows)
                        {
                            List<SqlParameter> sp2 = new List<SqlParameter>() {
                                new SqlParameter("wxico", dr["wxico"].ToString()),
                                new SqlParameter("flag",dr["flag"].ToString()) 
                            };
                            DBCenter.UpdateSQL("ZL_C_Article", "wxico=@wxico", "Flag=@flag", sp2);
                        }
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "mb_byid"://主用于内容更新
                    {
                        string fields = "wxico,wxsize,wxbk,flag,wxlink,content,dbtype";//dbtype
                        DataTable dt = GetListDT();
                        foreach (DataRow dr in dt.Rows)
                        {
                            M_CommonData conMod = conBll.SelReturnModel(Convert.ToInt32(dr["id"]));
                            List<SqlParameter> sp1 = GetSPByDR(dr, "title");
                            List<SqlParameter> sp2 = GetSPByDR(dr, fields);
                            DBCenter.UpdateSQL("ZL_CommonModel", GetSet(sp1), "GeneralID=" + conMod.GeneralID, sp1);
                            DBCenter.UpdateSQL("ZL_C_Article", GetSet(sp2), "ID=" + conMod.ItemID, sp2);
                        }
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "mb_new":
                    {
                        B_CodeModel codeBll = new B_CodeModel("ZL_C_Article");
                        string nodeName = Req("type").Equals("nav") ? "微站图片" : "微站内容";
                        DataTable nodedt = DBCenter.Sel("ZL_Node", "NodeBySite=" + siteID + " AND NodeName='" + nodeName + "'");
                        if (nodedt.Rows.Count < 1) { retMod.retmsg = nodeName + "节点不存在"; }
                        else
                        {
                            DataRow dr = GetListDT().Rows[0];
                            M_CommonData conMod = new M_CommonData();
                            DataRow artdr = codeBll.NewModel();
                            conMod.Title = dr["title"].ToString();
                            conMod.Inputer = mu.UserName;
                            conMod.Status = (int)ZLEnum.ConStatus.Audited;
                            conMod.TableName = "ZL_C_Article";
                            conMod.ModelID = 2;//文章ID
                            conMod.NodeID = Convert.ToInt32(nodedt.Rows[0]["NodeID"]);//放入微内容处
                            //-----------------------------
                            artdr["content"] = "ajax添加";
                            artdr["wxico"] = dr["wxico"];
                            artdr["wxsize"] = dr["wxsize"];
                            artdr["wxbk"] = dr["wxbk"];
                            artdr["flag"] = dr["flag"];
                            artdr["dbtype"] = dr["dbtype"];
                            conMod.ItemID = codeBll.Insert(artdr);
                            conMod.GeneralID = conBll.insert(conMod);
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = conMod.GeneralID.ToString();
                        }
                    }
                    break;
                case "mb_del":
                    {
                        int id = Convert.ToInt32(Req("id"));
                        M_CommonData conMod = conBll.SelReturnModel(id);
                        if (conMod == null) { retMod.retmsg = "内容[" + id + "]不存在"; }
                        else if (!conMod.Inputer.Equals(mu.UserName)) { retMod.retmsg = "你无权删除[" + id + "]内容"; }
                        else
                        {
                            conBll.SetDel(conMod.GeneralID);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "mb_pro_update"://新建,或更新
                    {
                        DataRow dr = GetListDT().Rows[0];
                        M_Product proMod = new M_Product();
                        if (DataConvert.CLng(dr["id"]) > 0)
                        {
                            int id = Convert.ToInt32(dr["id"]);
                            proMod = proBll.GetproductByid(id);
                            if (proMod == null) { retMod.retmsg = "商品[" + id + "]不存在"; RepToClient(retMod); }
                            else if (proMod.UserID!=mu.UserID) { retMod.retmsg = "无权修改[" + id + "]商品"; RepToClient(retMod); }
                        }
                        proMod.Proname = dr["proname"].ToString();
                        proMod.ActPrice = dr["price"].ToString();
                        proMod.LinPrice = DataConvert.CDouble(dr["price"]);
                        proMod.UserID = mu.UserID;
                        proMod.AddUser = mu.UserName;
                        proMod.Nodeid = B_Design_MBSite.UserShopNodeID;
                        proMod.ParentID = siteID;
                        proMod.Proinfo = dr["proinfo"].ToString();
                        proMod.Procontent = dr["content"].ToString();
                        proMod.Clearimg = dr["pics"].ToString();
                        proMod.Thumbnails = proMod.Clearimg;
                        if (proMod.ID > 0) { proBll.updateinfo(proMod); }
                        else { proMod.ID = proBll.Insert(proMod); }
                        retMod.retmsg = proMod.ID.ToString();
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "mb_pro_del":
                    {
                        int id = Convert.ToInt32(Req("id"));
                        M_Product proMod = proBll.GetproductByid(id);
                        if (proMod == null) { retMod.retmsg = "商品["+id+"]不存在"; }
                        else if (proMod.UserID != mu.UserID) { retMod.retmsg = "你无权删除[" + id + "]商品"; }
                        else 
                        {
                            proBll.RealDelByIDS(id.ToString());
                            retMod.retcode = M_APIResult.Success;
                        }

                    }
                    break;
                default:
                    retMod.retmsg = "[" + Action + "]接口不存在";
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; retMod.retcode = M_APIResult.Failed; }
        RepToClient(retMod);
    }
    public bool IsReusable { get { return false; } }
    private DataTable GetListDT()
    {
        string list = Req("list");
        if (string.IsNullOrEmpty(list)) { retMod.retmsg = "数据为空"; RepToClient(retMod); }
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(list);
        if (dt.Rows.Count < 1) { retMod.retmsg = "数据为空"; RepToClient(retMod); }
        return dt;
    }
    /// <summary>
    /// 参数化后返回
    /// </summary>
    /// <param name="fields">需要生成的字段</param>
    private List<SqlParameter> GetSPByDR(DataRow dr, string fields)
    {
        List<SqlParameter> sp = new List<SqlParameter>();
        foreach (string field in fields.Split(','))
        {
            if (string.IsNullOrEmpty(field)) continue;
            if (dr.Table.Columns.Contains(field))
            {
                sp.Add(new SqlParameter(field, dr[field].ToString()));
            }
        }
        return sp;
    }
    //生成sp后再根据sp生成set
    private string GetSet(List<SqlParameter> list)
    {
        string set = "";
        foreach (SqlParameter sp in list)
        {
            set += sp.ParameterName + "=@" + sp.ParameterName + ",";
        }
        set = set.TrimEnd(',');
        return set;
    }
}