using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using ZoomLa.SQLDAL;

namespace ConsoleApplication1
{
    class Program
    {
        private static string TbName = "ZL_Font_Apply";
        static void Main(string[] args)
        {
            SqlHelper.ConnectionString = "Data Source=sz03,28973;Initial Catalog=ziti163_com;User ID=ziti163_com_f;Password=U_passwor@-43-^-9Ulfwld2006;";
            Console.WriteLine("程序启动,开始检测字体任务");
            //SqlHelper.ExecuteSql("TRUNCATE TABLE " + TbName);
            //Directory.Delete(@"C:\inetpub\wwwroot\Hosting\ziti163.com\WebFont\User\", true);
            //Directory.CreateDirectory(@"C:\inetpub\wwwroot\Hosting\ziti163.com\WebFont\User\");
            try
            {
                while (true)
                {
                    DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM " + TbName + " WHERE ZStatus=0");
                    if (dt.Rows.Count > 0)
                    {
                        Console.WriteLine("发现任务,开始进行处理");
                        ExBat(dt);
                        Console.WriteLine("任务处理完成");
                    }
                    //Console.WriteLine("无任务,正在等待["+DateTime.Now+"]");
                    System.Threading.Thread.Sleep(2000);//必须2秒以上?
                }
            }
            catch (Exception ex) { Console.WriteLine("循环退出:"+ex.Message); }
            Console.ReadLine();
        }
        public static void ExBat(DataTable dt)
        {
            //字体与需要生成的文字信息均在其中
            foreach (DataRow dr in dt.Rows)
            {
                string workdir = dr["FontDir"].ToString();
                if (!Directory.Exists(workdir))
                {
                    Console.WriteLine("目录[" + workdir + "]不存在,直接略过");
                }
                else
                {
                    ExecBatCommand(workdir, p => { p(@"font-spider tlp.html"); }, DataReceived_Func, 10);
                }
                SqlHelper.ExecuteSql("UPDATE " + TbName + " SET ZStatus=1 WHERE ID=" + dr["ID"].ToString());
            }
        }
        public static void ExecBatCommand(string workdir, Action<Action<string>> inputAction, DataReceivedEventHandler DataReceived, int exitTime = 5)
        {
            Process pro = null;
            StreamWriter sIn = null;
            StreamReader sOut = null;
            try
            {
                pro = new Process();
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.CreateNoWindow = true;
                pro.StartInfo.WorkingDirectory = workdir.TrimEnd('\\');// @"C:\APPTlp";//等于其开始的默认位置,必须指定,cd跳转无效
                pro.StartInfo.RedirectStandardInput = true;
                //如果要将 RedirectStandardOutput 设置为 true，必须先将 UseShellExecute 设置为 false。否则，读取 StandardOutput 流时将引发异常
                //该值指示是否将应用程序的输出写入 Process.StandardOutput 流中,若要将输出写入 Process.StandardOutput 中，则为 true；否则为 false。
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;

                pro.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                pro.ErrorDataReceived += (sender, e) => Console.WriteLine("命令行消息返回:" + e.Data);
                pro.Start();
                sIn = pro.StandardInput;
                sIn.AutoFlush = true;
                //在应用程序的重定向 StandardOutput 流上开始异步读取操作。
                //pro.BeginOutputReadLine();该段导致其退出,无法执行
                inputAction(value => sIn.WriteLine(value));
                //string output = pro.StandardOutput.ReadToEnd();//这句用于将异常信息输出,否则你看不到正常信息的报错
                //if (!string.IsNullOrEmpty(output)) { Console.WriteLine(output); }
                //如到期未停止,则强行终止pro.WaitForExit();
                pro.WaitForExit(exitTime * 1000);
                
            }
            catch (Exception ex) { Console.WriteLine("出错:" + ex.Message); }
            finally
            {
                if (pro != null && !pro.HasExited) { pro.Kill(); }
                if (sIn != null) { sIn.Close(); }
                if (sOut != null) { sOut.Close(); }
                if (pro != null) { pro.Close(); }
            }
        }
        public static void DataReceived_Func(object sender, DataReceivedEventArgs e)
        {
            if (e == null || string.IsNullOrEmpty(e.Data)) { return; }
            Console.WriteLine(e.Data);
            //if (e.Data.Contains("BUILD SUCCESSFUL"))
            //{
            //    ((Process)sender).Kill();
            //    Console.WriteLine("生成完成,退出");
            //}
            //else
            //{
            //    Console.WriteLine(e.Data);
            //}
        }
    }
}
