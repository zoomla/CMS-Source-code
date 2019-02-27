using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ZoomLa.BLL.Helper
{
    public class ReflectionHelper
    {
        //获取所有方法 
        //System.Reflection.MethodInfo[] methods = t.GetMethods(); 
        //获取所有成员 
        //System.Reflection.MemberInfo[] members = t.GetMembers(); 
        //获取所有属性 
        //System.Reflection.PropertyInfo[] properties = t.GetProperties();


        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true);//根据类型创建实例
            return (T)obj;//类型转换并返回
        }
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
        /// <summary>
        /// 读取源码,动态编译后生成类
        /// SLabel labelMod = ReflectionHelper.CreateInstanceByCode<SLabel>(code, "MyLabel", "System.Web.dll", Server.MapPath("/Bin/Zoomla.Bll.dll"));
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="code">C#源码,包含 using引用</param>
        /// <param name="className">类名:MyLabel</param>
        /// <param name="dlls">需要引用的dll,如果是本地dll,需要全路径</param>
        /// <returns></returns>
        public static T CreateInstanceByCode<T>(string code, string className, params string[] dlls)
        {
            object instance = new object();
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            //cp.ReferencedAssemblies.Add("System.Web.dll");
            //cp.ReferencedAssemblies.Add(function.VToP("/Bin/ZoomLa.BLL.dll"));
            foreach (string dll in dlls)
            {
                cp.ReferencedAssemblies.Add(dll);
            }
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            //CompilerResults cr = provider.CompileAssemblyFromFile(cp, Server.MapPath("/test/MyLabel.cs"));
            CompilerResults cr = provider.CompileAssemblyFromSource(cp, code);
            if (cr.Errors.Count > 0)
            {
                //foreach (CompilerError error in cr.Errors) { throw new Exception(error.ErrorText); }
                instance = null;
            }
            else
            {
                Assembly assembly = cr.CompiledAssembly;
                instance = assembly.CreateInstance(className);
            }
            return (T)instance;
        }
        /// <summary>
        /// 反射调用方法,建议用统一基类
        /// </summary>
        private void InvokeMethod(Assembly objAssembly)
        {
            //object objClass = objAssembly.CreateInstance("Dynamicly.HelloWorld");
            //if (objClass == null)
            //{
            //    this.txtResult.Text = "Error: " + "Couldn't load class.";
            //    return;
            //}
            //object[] objCodeParms = new object[1];
            //objCodeParms[0] = "Allan.";
            //string strResult = (string)objClass.GetType().InvokeMember(
            //"GetTime", BindingFlags.InvokeMethod, null, objClass, objCodeParms);
        }
    }
}
