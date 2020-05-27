using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ZoomLa.Common;

namespace ZoomLa.BLL.Helper
{
    public class IPCity
    {
        /// <summary>
        /// 与PCC中的值保持一致,便于赋值
        /// </summary>
        /// <param name="location"></param>
        public IPCity(string[] location)
        {
            if (location.Length < 1 || location[0].Contains("本地地址")) { return; }
            Nation = location[0];
            Province = location.Length > 1 ? location[1] : "";
            City = location.Length > 1 ? location[2] : "";
            County = location.Length > 2 ? location[3] : "";
            if (!string.IsNullOrEmpty(Province))
            {
                string[] arr = "北京,上海,天津,重庆,宁夏,西藏,新疆,局域网".Split(',');
                if (!arr.Contains(Province)) { Province += "省"; }
            }
        }
        public string Nation = "", Province = "", City = "", County = "";
        /// <summary>
        /// 如为空,本地地址,局域网,则为无,True:有效地址效
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (Province.Contains("本地地址") || Province.Contains("局域网")) return false;
                if (string.IsNullOrEmpty(Province + City + County)) { return false; }
                return true;
            }
        }
    }
    public class IPScaner
    {
        static IPScaner()
        {
            IPScaner.EnableFileWatch = true;
            IPScaner.Load(function.VToP("/App_Data/IPdata.dat"));
        }
        public static bool EnableFileWatch = false;
        private static int offset;
        private static uint[] index = new uint[256];
        private static byte[] dataBuffer;
        private static byte[] indexBuffer;
        private static long lastModifyTime = 0L;
        private static string ipFile;
        private static readonly object @lock = new object();

        /// <summary>
        /// @nation:国家,@province:省,@city市,@county:县，默认@province|@city
        /// </summary>
        /// 
        public static string IPLocation(string ip, string tlp, bool getisp)
        {
            if (string.IsNullOrEmpty(ip)) { return "无IP信息"; }
            if (ip.Equals("::1")) { return "本地主机"; }
            IPAddress ipadr = null;
            if (!IPAddress.TryParse(ip, out ipadr)) { return "无效IP"; }
            IPCity cityMod = FindCity(ip);
            if (!string.IsNullOrEmpty(cityMod.City)) { cityMod.City += "市 "; }
            string result = "";
            if (cityMod.Province.Equals("局域网")) { result = cityMod.Province; }
            else {
                result = tlp.Replace("@nation", cityMod.Nation)
                            .Replace("@province", cityMod.Province)
                            .Replace("@city", cityMod.City)
                            .Replace("@county", cityMod.County);
            }
            if (getisp)
            {
                string isp = ISPScaner.GetIpISP(ip);
                if (!string.IsNullOrEmpty(isp))
                {
                    result += "(" + isp + ")";
                }
            }
            return result;
        }
        public static string IPLocation(string ip, string tlp = "@province|@city")
        {
            return IPLocation(ip, tlp, false);
        }
        /// <summary>
        /// 已弃用，兼容旧写法，默认返回省市,2:省,3:市,4:全部
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string IPLocation(string ip, int type)
        {
            string tlp = "";
            switch (type)
            {
                case 1:
                    tlp = "@province@city";
                    break;
                case 2:
                    tlp = "@province";
                    break;
                case 3:
                    tlp = "@city";
                    break;
                default:
                    tlp = "@nation@province@city@county";
                    break;
            }
            return IPLocation(ip, tlp);
        }
        public static string GetUserIP()
        {
            HttpContext context = HttpContext.Current;
            try { if (context == null || context.Request == null) return ""; }
            catch { return ""; }
            string clientip = "";
            //可以透过代理服务器取得客户端真实的IP,但是并不是每个代理服务器都能用这个方法取得真实IP
            //如果客户端没有使用代理服务器,这个值就为null,所以必须判断
            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                clientip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Trim();
                if (clientip.IndexOf(",") > -1)
                {
                    string[] tempIp = clientip.Split(',');
                    foreach (string s in tempIp)
                    {
                        //192.168.1.1属于IP地址的C类地址,属于保留IP,专门用于路由器设置.
                        if (!s.StartsWith("192.168"))
                        {
                            clientip = s.Trim();
                            break;
                        }
                    }
                }
            }
            else
            {
                //取得客户端的IP地址，如果客户端使用代理服务器，那么取得的就是代理服务器的IP,而不是真实的IP地址
                clientip = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            try
            {
                IPAddress.Parse(clientip);
            }
            catch
            {
                clientip = "127.127.0.1";
            }
            return clientip;
        }
        public static void Load(string filename)
        {
            ipFile = new FileInfo(filename).FullName;
            Load();
            if (EnableFileWatch)
            {
                Watch();
            }
        }
        /// <summary>
        /// 国家,省,市,县
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string[] Find(string ip)
        {
            string[] def = "本地地址,本地地址,本地地址,本地地址".Split(',');
            if (string.IsNullOrEmpty(ip)) { return def; }
            //fe80::b439:2a5:56a8:dd13,::1
            if (ip.Contains(":")) { return def; }
            lock (@lock)
            {
                var ips = ip.Split('.');
                var ip_prefix_value = int.Parse(ips[0]);
                long ip2long_value = BytesToLong(byte.Parse(ips[0]), byte.Parse(ips[1]), byte.Parse(ips[2]),
                    byte.Parse(ips[3]));
                var start = index[ip_prefix_value];
                var max_comp_len = offset - 1028;
                long index_offset = -1;
                var index_length = -1;
                byte b = 0;
                for (start = start * 8 + 1024; start < max_comp_len; start += 8)
                {
                    if (
                        BytesToLong(indexBuffer[start + 0], indexBuffer[start + 1], indexBuffer[start + 2],
                            indexBuffer[start + 3]) >= ip2long_value)
                    {
                        index_offset = BytesToLong(b, indexBuffer[start + 6], indexBuffer[start + 5],
                            indexBuffer[start + 4]);
                        index_length = 0xFF & indexBuffer[start + 7];
                        break;
                    }
                }
                var areaBytes = new byte[index_length];
                Array.Copy(dataBuffer, offset + (int)index_offset - 1024, areaBytes, 0, index_length);
                return Encoding.UTF8.GetString(areaBytes).Split('\t');
            }
        }
        public static IPCity FindCity(string ip)
        {
            return new IPCity(Find(ip));
        }
        private static void Watch()
        {
            var file = new FileInfo(ipFile);
            if (file.DirectoryName == null) return;
            var watcher = new FileSystemWatcher(file.DirectoryName, file.Name) { NotifyFilter = NotifyFilters.LastWrite };
            watcher.Changed += (s, e) =>
            {
                var time = File.GetLastWriteTime(ipFile).Ticks;
                if (time > lastModifyTime)
                {
                    Load();
                }
            };
            watcher.EnableRaisingEvents = true;
        }
        private static void Load()
        {
            lock (@lock)
            {
                var file = new FileInfo(ipFile);
                lastModifyTime = file.LastWriteTime.Ticks;
                try
                {
                    dataBuffer = new byte[file.Length];
                    using (var fin = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    {
                        fin.Read(dataBuffer, 0, dataBuffer.Length);
                    }

                    var indexLength = BytesToLong(dataBuffer[0], dataBuffer[1], dataBuffer[2], dataBuffer[3]);
                    indexBuffer = new byte[indexLength];
                    Array.Copy(dataBuffer, 4, indexBuffer, 0, indexLength);
                    offset = (int)indexLength;

                    for (var loop = 0; loop < 256; loop++)
                    {
                        index[loop] = BytesToLong(indexBuffer[loop * 4 + 3], indexBuffer[loop * 4 + 2],
                            indexBuffer[loop * 4 + 1],
                            indexBuffer[loop * 4]);
                    }
                }
                catch { }
            }
        }
        private static uint BytesToLong(byte a, byte b, byte c, byte d)
        {
            return ((uint)a << 24) | ((uint)b << 16) | ((uint)c << 8) | d;
        }
    }
}
