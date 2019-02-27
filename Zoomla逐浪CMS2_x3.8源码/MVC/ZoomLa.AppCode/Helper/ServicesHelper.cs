using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WebServices辅助类
/// </summary>
public class ServicesHelper
{
    public static string siteGroupService = "/API/SiteGroupFunc.asmx";
    public static object InvokeWebSer(string url, string @namespace, string classname, string methodname, object[] args)
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
}