using System;
using System.Collections.Generic;
using System.DirectoryServices;

using System.Text;

namespace IIS
{
    public class VirtualDirectory
    {
        public string Name { get; set; }
        public string Path { get; set; }

        #region 主目录属性 start
        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicsPath { get; set; }
        /// <summary>
        /// 脚本资源访问[默认为false]
        /// </summary>
        public bool AccessSource { get; set; }
        /// <summary>
        /// 写入[默认为false]
        /// </summary>
        public bool AccessWrite { get; set; }
        /// <summary>
        /// 目录浏览[默认为false]
        /// </summary>
        public bool EnableDirBrowsing { get; set; }
        /// <summary>
        /// 读取[默认为true]
        /// </summary>
        public bool AccessRead { get; set; }
        /// <summary>
        /// 记录访问[默认为true]
        /// </summary>
        public bool AccessLog { get; set; }
        /// <summary>
        /// 索引资源[默认为true]
        /// </summary>
        public bool ContentIndexed { get; set; }        

        #region 应用程序配置 start
        /// <summary>
        /// 执行权限[默认为中(共用)]0:无1:纯脚本2:脚本和可执行文件
        /// </summary>
        public int ExecutePermissions { get; set; }
        /// <summary>
        /// 是否配置应用程序
        /// </summary>
        public bool IsApplicaton { get; set; }
        /// <summary>
        /// 应用程序名字
        /// </summary>
        public string ApplicatonName { get; set; }
        /// <summary>
        /// 应用程序保护[默认为中(共用)]0:低(IIS进程)1:高(独立)2:中(共用)
        /// </summary>
        public int AppIsolated { get; set; }
        /// <summary>
        /// 脚本映射
        /// </summary>
        public string ScriptMaps { get; set; }
        /// <summary>
        /// 缓存ISAPI应用程序[默认true]
        /// </summary>
        public bool CacheISAPI { get; set; }
        /// <summary>
        /// 启用会话状态[默认true]
        /// </summary>
        public bool AspAllowSessionState { get; set; }
        /// <summary>
        /// 会话超时[默认20分钟]
        /// </summary>
        public int AspSessionTimeout { get; set; }
        /// <summary>
        /// 启用父路径[默认true]
        /// </summary>
        public bool AspEnableParentPaths { get; set; }
        /// <summary>
        /// 默认ASP语言[默认VBScript]
        /// </summary>
        public string AspScriptLanguage { get; set; }
        /// <summary>
        /// ASP脚本超时[默认90秒]
        /// </summary>
        public int AspScriptTimeout { get; set; }
        /// <summary>
        /// 启用ASP服务器脚本调试[默认false]
        /// </summary>
        public bool AppAllowDebugging { get; set; }
        /// <summary>
        /// 启用ASP客户端脚本调试[默认false]
        /// </summary>
        public bool AppAllowClientDebug { get; set; }
        /// <summary>
        /// 向客户端输入详细脚本错误[默认true]
        /// </summary>
        public bool AspScriptErrorSentToBrowser { get; set; }
        /// <summary>
        /// 友好脚本提示错误信息[默认AspScriptErrorMessage]
        /// </summary>
        public string AspScriptErrorMessage { get; set; }

        #endregion 应用程序配置 end

        #endregion 主目录属性 end

        #region IIS 文档属性 start
        /// <summary>
        /// 启用默认文档[默认为true]
        /// </summary>
        public bool EnableDefaultDoc { get; set; }
        /// <summary>
        /// 默认文档页[default.aspx,default.htm,default.asp,index.htm]
        /// </summary>
        public string DefaultDoc { get; set; }
        #endregion IIS 文档属性 end

        /// <summary>
        /// 构造函数(创建应用程序)
        /// </summary>
        /// <param name="name">虚拟目录名字</param>
        /// <param name="physicsPath">物理地址</param>
        public VirtualDirectory(string name, string physicsPath)
            : this(name, physicsPath, false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">虚拟目录名字</param>
        /// <param name="physicsPath">物理地址</param>
        /// <param name="NonApplication">是否(不创建应用程序)</param>
        public VirtualDirectory(string name, string physicsPath, bool NonApplication)
        {
            this.Name = name;
            this.PhysicsPath = physicsPath;
            this.AccessSource = false;
            this.AccessWrite = false;
            this.EnableDirBrowsing = false;
            this.AccessRead = true;
            this.ContentIndexed = true;
            this.AccessLog = true;
            this.ExecutePermissions = 1;
            this.IsApplicaton = !NonApplication;
            this.ApplicatonName = name;
            this.AppIsolated = 2;
            this.CacheISAPI = true;
            this.AspAllowSessionState = true;
            this.AspSessionTimeout = 20;
            this.AspEnableParentPaths = true;
            this.AspScriptLanguage = "VBScript";
            this.AspScriptTimeout = 90;
            this.AppAllowDebugging = false;
            this.AppAllowClientDebug = false;
            this.AspScriptErrorSentToBrowser = true;
            this.AspScriptErrorMessage = "处理 URL 时服务器出错。请与系统管理员联系";
            
            this.EnableDefaultDoc = true;
            this.DefaultDoc = "default.aspx,default.asp,default.html,default.htm,index.aspx,index.asp,index.html,index.htm";
        }
    }
}
