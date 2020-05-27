using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.BLL.Third;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.Model.Third;

namespace ZoomLa.PdoApi.CopyRight
{
    /// <summary>
    /// 版权印
    /// 一个网站只需要一个账户
    /// </summary>
    public class C_CopyRight
    {
        private readonly string tokenUrl = "http://oauth2.banquanyin.com/";
        private readonly string apiUrl = "http://api.banquanyin.com/";
        public string Client_ID { get; set; }
        public string Client_Secret { get; set; }
        private string _codeUrl = SiteConfig.SiteInfo.SiteUrl + "/Common/API/CRCallBack.aspx";
        private string configMsg = "";
        public string GetCodeUri { get { return tokenUrl + "authorize?client_id=" + Client_ID + "&response_type=code&redirect_uri=" + _codeUrl; } }
        private static string _accessToken = "";
        public static string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set { _accessToken = value; }
        }
        //-----
        //private static Dictionary<int, string> TokenDir = new Dictionary<int, string>();
        //private string GetTokenByUid()
        //{
        //    M_AdminInfo adminMod = B_Admin.GetLogin();
        //    if (TokenDir.ContainsKey(adminMod.AdminId)) { return TokenDir[adminMod.AdminId]; }
        //    else { return ""; }
        //}
        //private void AddToken(string token)
        //{
        //    try
        //    {
        //        M_AdminInfo adminMod = B_Admin.GetLogin();
        //        if (TokenDir.ContainsKey(adminMod.AdminId))
        //        {
        //            TokenDir[adminMod.AdminId] = token;
        //        }
        //        else { TokenDir.Add(adminMod.AdminId, token); }
        //    }
        //    catch (Exception ex) { throw new Exception("AddToken,"+ex.Message); }
        //}
        //---------------------------------------------
        public static void CheckLogin()
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                C_CopyRight crBll = new C_CopyRight();
                M_Third_PlatInfo infoMod = B_Third_PlatInfo.SelByFlag("版权印");
                InfoCheck(infoMod);
                crBll.UserToToken(infoMod.UserName, infoMod.UserPwd);
                if (!string.IsNullOrEmpty(AccessToken)) { HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl); }
            }
        }
        public C_CopyRight()
        {
            //Client_ID = "24165684208467970";
            //Client_Secret = "24165684208467971";
            if (string.IsNullOrEmpty(Client_ID))
            {
                M_Third_PlatInfo infoMod = B_Third_PlatInfo.SelByFlag("版权印");
                InfoCheck(infoMod);
                Client_ID = infoMod.APPKey;
                Client_Secret = infoMod.APPSecret;
                if (string.IsNullOrEmpty(AccessToken))
                {
                    UserToToken(infoMod.UserName, infoMod.UserPwd);
                }
            }
        }

        private static void InfoCheck(M_Third_PlatInfo infoMod)
        {
            if (infoMod == null) { function.WriteErrMsg("未设置版权印信息,请先<a href='/" + SiteConfig.SiteOption.ManageDir + "/Config/PlatInfoList.aspx'>完成配置</a>"); }
            else if (string.IsNullOrEmpty(infoMod.APPKey)) { function.WriteErrMsg("未设置版权印Key,请先<a href='/" + SiteConfig.SiteOption.ManageDir + "/Config/PlatInfoList.aspx'>完成配置</a>"); }
            else if (string.IsNullOrEmpty(infoMod.APPSecret)) { function.WriteErrMsg("未设置版权印Secret,请先<a href='/" + SiteConfig.SiteOption.ManageDir + "/Config/PlatInfoList.aspx'>完成配置</a>"); }
            else { }
        }
        public List<M_Content_CROrder> GetOrders()
        {
            List<M_Content_CROrder> orders = new List<M_Content_CROrder>();
            JObject result = JsonConvert.DeserializeObject<JObject>(GetOrderList(0));
            if (result["value"].ToString().Equals("1"))
            {
                for (int i = 0; i < DataConverter.CLng(result["rows"]); i++)
                {
                    orders.Add(ConvertOrder(result["data"][i].ToString()));
                }
                return orders;
            }
            else
            {
                return null;
            }
        }
        public M_Content_CROrder GetOrder(string orderId)
        {
            string result = GetOrderInfo(orderId);
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            if (obj["value"].ToString().Equals("1"))
            {
                return ConvertOrder(obj["data"].ToString());
            }
            else
            {
                return null;
            }
        }
        public M_Content_CROrder ConvertOrder(string order)
        {
            M_Content_CROrder orderMod = new M_Content_CROrder();
            JObject obj = JsonConvert.DeserializeObject<JObject>(order);
            orderMod.Amount = DataConverter.CDouble(obj["amount"]);
            orderMod.LicenseeContact = obj["licensee"]["contact"].ToString();
            orderMod.LicenseeTel = obj["licensee"]["tel"].ToString();
            orderMod.LicenseeTitle = obj["licensee"]["title"].ToString();
            orderMod.LicenseeEmail = obj["licensee"]["email"].ToString();
            orderMod.LicensorContact = obj["licensor"]["contact"].ToString();
            orderMod.LicensorTel = obj["licensor"]["tel"].ToString();
            orderMod.LicensorTitle = obj["licensor"]["title"].ToString();
            orderMod.LicensorEmail = obj["licensor"]["email"].ToString();
            orderMod.OrderID = obj["id"].ToString();
            orderMod.AuthPrice = DataConverter.CDouble(obj["detail"][0]["worksLicense"]["price"]);
            orderMod.AuthComment = obj["detail"][0]["worksLicense"]["comment"].ToString();
            orderMod.AuthTitle = obj["detail"][0]["worksLicense"]["title"].ToString();
            orderMod.WorksID = obj["detail"][0]["worksId"].ToString();
            orderMod.WorksTitle = obj["detail"][0]["title"].ToString();
            orderMod.PayStatus = DataConverter.CLng(obj["payStatus"]);
            orderMod.Status = DataConverter.CLng(obj["status"]);
            orderMod.CreateDate = DataConverter.CDate(obj["createDate"]);
            return orderMod;
        }
        /// <summary>
        /// 提交实名认证信息
        /// </summary>
        public string SubmitVerify(string jsondata)
        {
            //个人账户:idCardType,idCardNo,idCardFront,idCardBack,handOfCardFront,trueName
            //企业账户:orgName,shortName,organizationCode,licenseImage,legal,orgType,registerAddress,registerCapital,businessScope,tel,fax
            //idCardFront,idCardBack,handOfCardFront,licenseImage为通过文件上传后返回的文件路径
            return APIHelper.GetWebResult(apiUrl + "privacy/submit_verify?client_id=" + Client_ID + "&access_Token=" + AccessToken, "POST", jsondata);
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file">文件数据（数据流）</param>
        /// <returns></returns>
        public string Upload(Byte[] imageFile)
        {
            int nonce = GetNonce();
            //return APIHelper.GetWebResult("http://file.banquanyin.com/upload?client_id=" + Client_ID + "&signature=" + GetSign(nonce), "POST", file);
            return "文件地址";
        }
        public void Download(string filepath)
        {
            int nonce = GetNonce();
            APIHelper.GetWebResult("http://file.banquanyin.com/download?client_id=" + Client_ID + "&signature=" + GetSign(nonce) + "&id=" + filepath);
        }
        /// <summary>
        /// 获取作品列表
        /// </summary>
        /// <returns></returns>
        public string GetList()
        {
            return APIHelper.GetWebResult(apiUrl + "works/get_list?client_id=" + Client_ID + "&access_token=" + AccessToken);
        }
        /// <summary>
        /// 提交一条新的信息,
        /// </summary>
        /// <param name="copyMod"></param>
        /// <returns></returns>
        public string Create(M_Content_CR crMod)
        {
            string jsondata = "{\"title\": \"" + crMod.Title + "\",\"type\": \"" + crMod.Type + "\",\"rightOwner\": \"" + SiteConfig.SiteInfo.CompanyName + "\",\"content\": \"" + crMod.Content + "\"," +
                           "\"cfgType\": \"" + crMod.FromType + "\",\"keywords\": \"" + crMod.KeyWords + "\",\"author\": \"" + crMod.Author + "\",\"fromUrl\": \"" + crMod.FromUrl + "\",\"priceSettings\": [{\"template\": \"1\",\"price\": \"" + crMod.RepPrice + "\"},{\"template\": \"2\",\"price\": \"" + crMod.MatPrice + "\"}]}";
            return APIHelper.GetWebResult(apiUrl + "works/create?client_id=" + Client_ID + "&access_token=" + AccessToken + "&developId=24258979991388174", "POST", jsondata);
        }
        /// <summary>
        /// 作品详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(string id)
        {
            return APIHelper.GetWebResult(apiUrl + "works/get?client_id=" + Client_ID + "&access_token=" + AccessToken + "&id=" + id);
        }
        /// <summary>
        /// 修改作品
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public string Modify(string jsondata)
        {
            jsondata = "{\"id\": \"C1016042718000020237\",\"title\": \"app 测试20150929103611\",\"type\": \"1\",\"rightOwner\": \"测试人员\",\"content\": \"003cdiv003enothing is left;文章真不好写，尤其是app 标志测试的文章003c/div003e\"," +
                          "\"cfgType\": \"test\",\"keywords\": \"app 文章\",\"author\": \"张三\",\"fromUrl\": \"http://win19:86/test/TestBQY.aspx\",\"priceSettings\": [{\"template\": \"1\",\"price\": \"8\"},{\"template\": \"2\",\"price\": \"9\"}]}";
            return APIHelper.GetWebResult(apiUrl + "works/modify?client_id=" + Client_ID + "&access_token=" + AccessToken, "POST", jsondata);
        }
        /// <summary>
        /// 删除作品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Remove(string id)
        {
            return APIHelper.GetWebResult(apiUrl + "works/remove?client_id=" + Client_ID + "&access_token=" + AccessToken + "&id=" + id);
        }
        /// <summary>
        /// 获取域名信息/会员
        /// </summary>
        /// <returns></returns>
        public string GetDomain()
        {
            return APIHelper.GetWebResult(apiUrl + "member/domain/get?client_id=" + Client_ID + "&access_token=" + AccessToken);
        }
        /// <summary>
        /// 设置域名
        /// </summary>
        /// <returns></returns>
        public string SetDomain(string licenseUrl)
        {
            return APIHelper.GetWebResult(apiUrl + "member/domain/set?client_id=" + Client_ID + "&access_token=" + AccessToken + "&licenseUrl=" + licenseUrl);
        }
        /// <summary>
        /// 获取授权方信息
        /// </summary>
        /// <returns></returns>
        public string GetLicensor()
        {
            return APIHelper.GetWebResult(apiUrl + "member/licensor/get?client_id=" + Client_ID + "&access_token=" + AccessToken);
        }
        /// <summary>
        /// 设置授权方信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public string SetLicensor(string nick, string address, string contact, string mobile, string tel, string title, string cardNo, string email)
        {
            string jsondata = "{\"nick\": \"" + nick + "\",\"isDefault\": \"1\",\"address\": \"" + address + "\",\"contact\": \"" + contact + "\",\"mobile\": \"" + mobile + "\",\"tel\": \"" + tel + "\",\"title\": \"" + title + "\",\"cardno\": \"" + cardNo + "\",\"email\": \"" + email + "\"}";
            return APIHelper.GetWebResult(apiUrl + "member/licensor/set?client_id=" + Client_ID + "&access_token=" + AccessToken, "POST", jsondata);
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetOrderInfo(string id)
        {
            return APIHelper.GetWebResult(apiUrl + "order/get?client_id=" + Client_ID + "&access_token=" + AccessToken + "&id=" + id);
        }
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="type">0-作为买家购买的订单;1-作为卖家卖出的订单</param>
        /// <returns></returns>
        public string GetOrderList(int type)
        {
            return APIHelper.GetWebResult(apiUrl + "order/get_list?client_id=" + Client_ID + "&access_token=" + AccessToken + "&type=" + type);
        }
        /// <summary>
        /// 授权书下载
        /// </summary>
        /// <returns></returns>
        public string GetOrderDoc()
        {
            return APIHelper.GetWebResult(apiUrl + "order/get_doc?client_id=" + Client_ID + "&access_token=" + AccessToken);
        }
        /// <summary>
        /// 获取授权规则
        /// </summary>
        /// <returns></returns>
        public string GetRule(string type)
        {
            return APIHelper.GetWebResult(apiUrl + "rule/get?client_id=" + Client_ID + "&access_token=" + AccessToken + "&type=" + type);
        }
        /// <summary>
        /// 获取授权规则模板
        /// </summary>
        /// <returns></returns>
        public string GetTemplates()
        {
            return APIHelper.GetWebResult(apiUrl + "templates/get_list?access_token=" + AccessToken + "&client_id=" + Client_ID);
        }
        ///// <summary>
        ///// 设置授权规则
        ///// </summary>
        ///// <param name="jsondata"></param>
        ///// <returns></returns>
        //public string SetRule(string jsondata)
        //{
        //    jsondata = "{type:'test',rules:[{ templateId:'1',price: '10'},{ templateId:'2',price: '5'}]}";
        //    return APIHelper.GetWebResult(apiUrl + "rule/set?access_token=" + AccessToken + "&client_id=" + Client_ID, "POST", jsondata);
        //}

        /// <summary>
        /// 添加价格模板
        /// </summary>
        /// <param name="name">模板名称</param>
        public string SetPriceRule(string name, double repPrice, double matPrice)
        {
            string data = "{type:'" + name + "',rules:[{ templateId:'1',price: '" + repPrice + "'},{ templateId:'2',price: '" + matPrice + "'}]}";
            return APIHelper.GetWebResult(apiUrl + "rule/set?access_token=" + AccessToken + "&client_id=" + Client_ID, "POST", data);
        }
        /// <summary>
        /// 设置默认价格模板
        /// </summary>
        public string SetDefPriceRule()
        {
            return SetPriceRule(SiteConfig.SiteInfo.SiteName, 0, 0);
        }
        //public void CodeToToken(string code)
        //{
        //    string result = "";
        //    string token = "";
        //    try
        //    {
        //        int nonce = GetNonce();
        //        string url = tokenUrl + "access_token?client_id=" + Client_ID + "&nonce=" + nonce + "&signature=" + GetSign(nonce) + "&grant_type=authorization_code&code=" + code + "&redirect_uri=" + _codeUrl;
        //        result = APIHelper.GetWebResult(url);
        //        JObject obj = JsonConvert.DeserializeObject<JObject>(result);
        //        token = obj["access_token"].ToString();
        //        AccessToken = token;
        //        //{"access_token":"100ihtoasp271cbae001pnec","refresh_token":"100oi7ifjs043tafpqtin8ak","expires_in":604800}
        //        //adminID
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message + "," + result + "," + token); }
        //}
        public void UserToToken(string uname, string upwd)
        {
            string result = "", token = "";
            try
            {
                int nonce = GetNonce();
                string url = tokenUrl + "access_token?client_id=" + Client_ID + "&nonce=" + nonce + "&signature=" + GetSign(nonce) + "&grant_type=password";
                result = APIHelper.GetWebResult(url, "POST", "username=" + uname + "&password=" + upwd);
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                token = obj["access_token"].ToString();
                AccessToken = token;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "," + result + "," + token); }
        }
        /// <summary>
        /// 用client_id+随机数+client_secret进行MD5加密生成签名，用来获取token
        /// </summary>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public string GetSign(int nonce)
        {
            return StringHelper.MD5("client_id=" + Client_ID + "&nonce=" + nonce + "&client_secret" + Client_Secret);
        }
        public int GetNonce()
        {
            Random rd = new Random();
            return rd.Next(100000, 1000000);
        }
    }
}
