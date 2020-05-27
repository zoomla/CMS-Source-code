using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// WebServices帮助类
/// </summary>
public class WSHelper
{
	public WSHelper()
	{
		
	}
    private string DeAPI = "/Api/SiteGroupFunc.asmx";
    private string DeNSpace = "SiteGroup";
    private string DeCName = "SiteGroupFunc";
    /// <summary>
    /// 通过反射完成WS调用
    /// </summary>
    /// <param name="url">WSUrl</param>
    /// <param name="namespace">WS命名空间</param>
    /// <param name="classname">WS类名</class>
    /// <param name="methodname">WS方法名</class>
    /// <param name="args">传递给WS方法的参数</param>
    /// 示例：url="http://win01:86";serviceUrl = "/Api/SiteGroupFunc.asmx";
    /// object s = InvokeWebSer(url + serviceUrl, "SiteGroup", "SiteGroupFunc", "GetSiteName", new object[] { });
    /// <returns>执行结果</returns>
    public object InvokeWS(string url, string @namespace, string classname, string methodname, object[] args)
    {
        System.Net.WebClient wc = new System.Net.WebClient();
        string URL = string.Empty;
        if ((url.Substring(url.Length - 5, 5) != null) && ((url.Substring(url.Length - 5, 5).ToLower() != "?wsdl")))
            URL = url + "?WSDL";
        else
            URL = url;
        System.IO.Stream stream = wc.OpenRead(URL);
        System.Web.Services.Description.ServiceDescription sd = System.Web.Services.Description.ServiceDescription.Read(stream);
        System.Web.Services.Description.ServiceDescriptionImporter sdi = new System.Web.Services.Description.ServiceDescriptionImporter();
        sdi.AddServiceDescription(sd, "", "");
        System.CodeDom.CodeNamespace cn = new System.CodeDom.CodeNamespace(@namespace);
        System.CodeDom.CodeCompileUnit ccu = new System.CodeDom.CodeCompileUnit();
        ccu.Namespaces.Add(cn);
        sdi.Import(cn, ccu);

        Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();

        System.CodeDom.Compiler.CompilerParameters cplist = new System.CodeDom.Compiler.CompilerParameters();
        cplist.GenerateExecutable = false;
        cplist.GenerateInMemory = true;
        cplist.ReferencedAssemblies.Add("System.dll");
        cplist.ReferencedAssemblies.Add("System.XML.dll");
        cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
        cplist.ReferencedAssemblies.Add("System.Data.dll");

        System.CodeDom.Compiler.CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);

        if (true == cr.Errors.HasErrors)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
            {
                sb.Append(ce.ToString());
                sb.Append(System.Environment.NewLine);
            }
            throw new Exception(sb.ToString());
        }
        System.Reflection.Assembly assembly = cr.CompiledAssembly;
        Type t = assembly.GetType(@namespace + "." + classname, true, true);
        object obj = Activator.CreateInstance(t);
        System.Reflection.MethodInfo mi = t.GetMethod(methodname);
        return mi.Invoke(obj, args);
    }
    //--------封装
    //获取远程逐浪子站节点列表
    public DataTable GetNodeList(string url)
    {
        try
        {
            object o = InvokeWS(url + DeAPI, DeNSpace, DeCName, "GetNodeList", new object[] { });
            return o as DataTable;
        }
        catch { return null; }
        
    }
}