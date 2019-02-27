using System;
using System.Collections.Generic;

using System.Text;
using System.Web;

namespace SDK
{
    public class KxOAuth2
    {
        /// <summary>
        /// 用户名密码方式获取AccessToken
        /// </summary>
        /// <param name="accessTokenUrl">accessToken的url</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="apiKey">APIKEY</param>
        /// <param name="secret">SECRECT</param>
        /// <param name="scope">scope</param>
        /// <returns>返回的数据</returns>
        public KxReturn UserNamePassWordCredentials(string accessTokenUrl, string userName, string passWord, 
            string apiKey, string secret, string scope)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("grant_type", "password");
            queryParams.Add("username", userName);
            queryParams.Add("password", passWord);
            queryParams.Add("client_id", apiKey);
            queryParams.Add("client_secret", secret);
            queryParams.Add("scope", scope);

            HTTPBase httpBase = new HTTPBase();
            string result = httpBase.Post(accessTokenUrl, queryParams);

            return new AccessToken(result);
        }

        /// <summary>
        /// 更新token的方式请求access_token
        /// </summary>
        /// <param name="accessTokenUrl">accessToken的URL</param>
        /// <param name="apiKey">APIKEY</param>
        /// <param name="secret">Secret</param>
        /// <param name="scope">权限Scope</param>
        /// <param name="refresh_token">refreshToken，更新凭证</param>
        /// <returns>返回的数据</returns>
        public KxReturn RefreshTokenCredentials(string accessTokenUrl, string apiKey, string secret, string scope, string refresh_token)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("grant_type", "refresh_token");
            queryParams.Add("client_id", apiKey);
            queryParams.Add("client_secret", secret);
            queryParams.Add("scope", scope);
            queryParams.Add("refresh_token", refresh_token);

            HTTPBase httpBase = new HTTPBase();
            string result = httpBase.Post(accessTokenUrl, queryParams);

            return new AccessToken(result);
        }

        /// <summary>
        /// AuthorizeCode 方式获取AccessToken
        /// </summary>
        /// <param name="accessTokenUrl">access_token的url</param>
        /// <param name="code">第一次请求request token的时候返回的code</param>
        /// <param name="apiKey">APIKEY</param>
        /// <param name="secret">Secret</param>
        /// <param name="redirect_uri">回调URL，必须和第一次请求request token时候一致</param>
        /// <returns>返回数据</returns>
        public KxReturn AuthorizeCodeAccessToken(string accessTokenUrl, string code, string apiKey,
            string secret, string redirect_uri)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("grant_type", "authorization_code");
            queryParams.Add("code", code);
            queryParams.Add("client_id", apiKey);
            queryParams.Add("client_secret", secret);
            queryParams.Add("redirect_uri", redirect_uri);

            HTTPBase httpBase = new HTTPBase();
            string result = httpBase.Post(accessTokenUrl, queryParams);
            return new AccessToken(result);
        }
    
        /// <summary>
        /// 获取Authorize Code方式的authorize请求的URL
        /// </summary>
        /// <param name="authorizeUrl">authorize Url</param>
        /// <param name="apiKey">APIKEY</param>
        /// <param name="callbackUrl">回调URL</param>
        /// <param name="scope">SCOPE范围</param>
        /// <returns>最终的请求URL</returns>
        public string AuthorizeCodeAuthorizeUrl(string authorizeUrl, string apiKey, string callbackUrl, string scope)
        {
            string retUrl = authorizeUrl + "?";
            retUrl += "response_type=code&";
            retUrl += "client_id=" + apiKey + "&";
            retUrl += "redirect_uri=" + HttpUtility.UrlEncode(callbackUrl) + "&";
            retUrl += "scope=" + scope + "&";
            retUrl += "display=popup";

            return retUrl;
        }

        /// <summary>
        /// 获取Implict Grant方式的authorize请求URL
        /// </summary>
        /// <param name="authorizeUrl">authorize Url</param>
        /// <param name="apiKey">APIKEY</param>
        /// <param name="callbackUrl">回调URL</param>
        /// <param name="scope">SCOPE范围</param>
        /// <returns>最终的请求URL</returns>
        public string ImplictAuthorizeUrl(string authorizeUrl, string apiKey, string callbackUrl, string scope)
        {
            string retUrl = authorizeUrl + "?";
            retUrl += "response_type=token&";
            retUrl += "client_id=" + apiKey + "&";
            retUrl += "redirect_uri=" + HttpUtility.UrlEncode(callbackUrl) + "&";
            retUrl += "scope=" + scope + "&";
            retUrl += "display=popup&";
            retUrl += "state=success";

            return retUrl;
        }

        /// <summary>
        /// 根据回调的return Url解析出AccessToken
        /// </summary>
        /// <param name="callbackReturnUrl">回调的Url，accessToken数据跟在#符号后面</param>
        /// <returns>AccessToen</returns>
        public AccessToken ImplictAuthorizeParseAccessToken(string callbackReturnUrl)
        {
            string[] words = callbackReturnUrl.Split('&');

            string access_token = null;
            string expires_in = null;
            string scope = null;
            string refresh_token = null;

            foreach (string word in words)
            {
                string[] keyval = word.Split('=');
                switch (keyval[0])
                {
                    case "access_token":
                        access_token = keyval[1];
                        break;
                    case "expires_in":
                        expires_in = keyval[1];
                        break;
                    case "scope":
                        scope = keyval[1];
                        break;
                    case "refresh_token":
                        refresh_token = keyval[1];
                        break;
                }
            }
            return new AccessToken(access_token, expires_in, refresh_token, scope);
        }
    }
}
