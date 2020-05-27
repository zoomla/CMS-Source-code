<%@ WebHandler Language="C#" Class="content" %>

using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
//用于内容转载,删除
public class content : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    B_Content conBll = new B_Content();
    private int Gid { get { return DataConvert.CLng(Req("Gid")); } }
    public void ProcessRequest(HttpContext context) 
    {
        throw new Exception("接口关闭,请联系管理员开启");
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        if (mu.IsNull) { retMod.retmsg = "请登录后再操作"; RepToClient(retMod); }
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "add":
                    break;
                case "edit":
                    break;
                case "del"://删除自己的转载或内容
                    {
                        M_CommonData conMod = conBll.SelReturnModel(Gid);
                        if (conMod == null) { retMod.retmsg = "内容不存在"; }
                        else if (!conMod.Inputer.ToLower().Equals(mu.UserName.ToLower())) { retMod.retmsg = "你无权删除该内容"; }
                        else 
                        {
                            conBll.SetDel(conMod.GeneralID);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "forward":
                    {
                        M_CommonData forMod = conBll.SelReturnModel(Gid);
                        List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("uname", mu.UserName) };
                        if (forMod == null) { retMod.retmsg = "内容不存在"; }
                        else if (forMod.ModelID != 46) { retMod.retmsg = "非游记内容无法转载"; }
                        else if (DBCenter.IsExist("ZL_CommonModel", "Inputer=@uname AND InfoID='" + forMod.GeneralID + "'", sp))
                        {
                            retMod.retmsg = "你已转发过该内容,不能重复转载"; 
                        }
                        else
                        {
                            M_CommonData conMod = new M_CommonData();
                            conMod.NodeID = forMod.NodeID;
                            conMod.FirstNodeID = forMod.FirstNodeID;
                            conMod.ModelID = forMod.ModelID;
                            conMod.ItemID = forMod.ItemID;
                            conMod.TableName = forMod.TableName;
                            conMod.TagKey = forMod.TagKey;
                            conMod.Status = (int)ZLEnum.ConStatus.Audited;
                            conMod.ParentTree = forMod.ParentTree;
                            //---------------------------
                            conMod.Title = Req("Title");
                            conMod.Inputer = mu.UserName;
                            conMod.InfoID = forMod.GeneralID.ToString();
                            //修改infoID,修改title   
                            conMod.GeneralID = conBll.insert(conMod);
                            //---------------------------
                            retMod.result = conMod.GeneralID.ToString();
                            retMod.retcode = M_APIResult.Success;
                        }
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
}