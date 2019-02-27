using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

namespace ZoomLaCMS.API.mod
{
    //查看快递信息
    public class shop_express :API_Base,IHttpHandler
    {
       B_Order_Exp expBll=new B_Order_Exp();
        //nu   ：要查询的快递单号，请勿带特殊符号，不支持中文（大小写不敏感）
        //com  ：要查询的快递公司代码，不支持中文，对应的公司代码见文档
        //muti ：返回信息数量： 1:返回多行完整的信息， 0:只返回一行信息。 不填默认返回多行。 
        //order：排序： desc：按时间由新到旧排列， asc：按时间由旧到新排列。 
        //
        public void ProcessRequest(HttpContext context)
        {
            string key = SiteConfig.SiteOption.KDKey;
            retMod.retcode = M_APIResult.Failed;
            //retMod.callback = CallBack;//暂不开放JsonP
            int expid = DataConverter.CLng(Req("expid"));
            M_Order_Exp expMod = expBll.SelReturnModel(expid);
            if (string.IsNullOrEmpty(key)) { retMod.retmsg = "未配置快递查询key"; RepToClient(retMod); }
            else if (expMod == null || string.IsNullOrEmpty(expMod.ExpNo)) { retMod.retmsg = "快递信息不存在"; RepToClient(retMod); }
            else if (string.IsNullOrEmpty(GetComByExpComp(expMod.ExpComp))) { retMod.retmsg = "[" + expMod.ExpComp + "]未在可查询列表"; RepToClient(retMod); }
            retMod.retmsg = "{\"expcomp\":\"" + expMod.ExpComp + "\",\"expno\":\"" + expMod.ExpNo + "\"}";
            try
            {
                switch (Action)
                {
                    case "get":
                        {
                            string com = GetComByExpComp(expMod.ExpComp);
                            string apiurl = "http://api.kuaidi100.com/api?id=" + key + "&com=" + com + "&nu=" + expMod.ExpNo + "&show=0&muti=1&order=asc";
                            retMod.result = APIHelper.GetWebResult(apiurl, "POST");
                            //retMod.result=SafeSC.ReadFileStr("/Tools/exp.txt");
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "apilink"://免费版,主流的快递不支持以json返回,必须以iframe嵌入他们的链接
                        {
                            if (!string.IsNullOrEmpty(expMod.APILink)) { retMod.result = expMod.APILink; retMod.retcode = M_APIResult.Success; }
                            else
                            {
                                string com = GetComByExpComp(expMod.ExpComp);
                                string apiurl = "http://www.kuaidi100.com/applyurl?key=" + key + "&com=" + com + "&nu=" + expMod.ExpNo;
                                retMod.result = APIHelper.GetWebResult(apiurl, "POST");//参数错误的情况下也会返回网址,且每次不同
                                expMod.APILink = retMod.result;
                                expBll.UpdateByID(expMod);
                               
                                retMod.retcode = M_APIResult.Success;
                            }
                        }
                        break;
                    default:
                        retMod.retmsg = "[" + Action + "]接口不存在";
                        break;
                }
            }
            catch (Exception ex) { retMod.retmsg = ex.Message; }
            RepToClient(retMod);
        }
        public bool IsReusable { get { return false; } }
        private string GetComByExpComp(string compName)
        {
            if (string.IsNullOrEmpty(compName)) { return ""; }
            switch (compName.Trim())
            {
                case "顺丰快递":
                case "顺丰速递": return "shunfeng";
                case "中通速递": return "zhongtong";
                case "申通快递": return "shentong";
                case "圆通速递": return "yuantong";
                case "韵达快递":
                case "韵达快运": return "yunda";
                case "百世汇通": return "huitongkuaidi";
                case "天天快递": return "tiantian";
                case "宅急送": return "zhaijisong";
                case "全峰快递": return "quanfengkuaidi";
                case "E邮宝":
                case "EMS": return "ems";
                case "UPS": return "ups";
                case "德邦物流": return "debangwuliu";
                case "华宇物流": return "tiandihuayu";
                case "如风达快递": return "rufengda";
                case "安能物流": return "annengwuliu";
                default: return "";
            }
        }
    }
}
//{
//"message": "ok", "status": "1", "state": "3", "data": [
//  { "time": "2012-07-07 13:35:14", "context": "客户已签收" },
//  { "time": "2012-07-07 09:10:10", "context": "离开[北京石景山营业厅]派送中，递送员[温]，电话[]" },
//  { "time": "2012-07-06 19:46:38", "context": "到达[北京石景山营业厅]" },
//  { "time": "2012-07-06 15:22:32", "context": "离开[北京石景山营业厅]派送中，递送员[温]，电话[]" },
//  { "time": "2012-07-06 15:05:00", "context": "到达[北京石景山营业厅]" },
//  { "time": "2012-07-06 13:37:52", "context": "离开[北京_同城中转站]发往[北京石景山营业厅]" },
//  { "time": "2012-07-06 12:54:41", "context": "到达[北京_同城中转站]" },
//  { "time": "2012-07-06 11:11:03", "context": "离开[北京运转中心驻站班组] 发往[北京_同城中转站]" },
//  { "time": "2012-07-06 10:43:21", "context": "到达[北京运转中心驻站班组]" },
//  { "time": "2012-07-05 21:18:53", "context": "离开[福建_厦门支公司] 发往 [北京运转中心_航空]" },
//  { "time": "2012-07-05 20:07:27", "context": "已取件，到达 [福建_厦门支公司]" }
//]
//}