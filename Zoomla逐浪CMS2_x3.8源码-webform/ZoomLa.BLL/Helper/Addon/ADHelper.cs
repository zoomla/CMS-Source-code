using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
namespace ZoomLa.BLL.Helper
{
    /// <summary>
    /// 域用户验证
    /// </summary>
    public class ADHelper
    {
        private string _path;
        public ADHelper(string path)
        {
            _path = path;
        }
        /// <summary>
        /// 根据域+用户名+密码返回结果,未匹配为null
        /// </summary>
        public SearchResult IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return result;
                }
                //Update the new path to the user in the directory.
                _path = result.Path;
                //{"cn":["hu"],"adspath":["LDAP://CN=hu,OU=营销部,DC=adz01,DC=com"]} 
                //_filterAttribute = (string)result.Properties["cn"][0];
                return result;
            }
            catch (Exception)
            {
                //throw new Exception(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 根据用户名,获取其所属的组
        /// </summary>
        public string GetGroups(string name)
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + name + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
        #region 辅助Tools
        /// <summary>
        /// 用户不存在则新建,部门不存在则也新建
        /// </summary>
        public M_UserInfo H_SyncUser(SearchResult result, string upwd)
        {
            B_Group gpBll = new B_Group();
            B_User buser = new B_User();
            string uname = (string)result.Properties["cn"][0];
            string gname = H_GetInfoFromResult(result.Path, "OU");
            M_UserInfo mu = buser.GetUserByName(uname);
            if (!mu.IsNull) { return mu; } else { mu = new M_UserInfo(); }
            mu.UserName = uname;
            mu.UserPwd = StringHelper.MD5(upwd);
            mu.GroupID = 1;

            M_Group gpMod = gpBll.SelModelByName(gname);
            if (gpMod.GroupID > 0) { mu.GroupID = gpMod.GroupID; }
            else if (gpMod.IsNull && !string.IsNullOrEmpty(gname))
            {
                gpMod = new M_Group();
                gpMod.GroupName = gname;
                gpMod.GroupID = gpBll.GetInsert(gpMod);
                mu.GroupID = gpMod.GroupID;
            }
            mu.Email = function.GetRandomString(8) + "@random.com";
            mu.UserID = buser.AddModel(mu);
            return mu;
        }
        private string H_GetInfoFromResult(string path, string info)
        {
            //LDAP://CN=hu,OU=营销部,DC=adz01,DC=com
            path = path.Replace("LDAP://", "");
            foreach (string item in path.Split(','))
            {
                string name = item.Split('=')[0].ToLower();
                string value = item.Split('=')[1];
                if (name.Equals(info.ToLower())) { return value; }
            }
            return "";
        }
        #endregion
    }
}
