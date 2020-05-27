using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Model;

namespace ZoomLa.BLL.Helper.Addon
{
    public class CMDHelper
    {
        /// <summary>
        /// 执行命令行
        /// 1,如果调用的是NodeJS,且目标是异步,则该方法必须放在异步方法中执行(delegate即可)
        /// 2,exitTime必须设定,否则命令一旦出错即会卡住,或也可在DataReceived中处理(依据返回字符串)
        /// 3,部分命令需要以Exe或管理员方式执行,否则权限不够
        /// 示例:CMDHelper.ExecBatCommand("", p =>{p("cd d:\\");}, null, 10);
        /// </summary>
        /// <param name="workdir">工作目录,很多场景必须指定</param>
        /// <param name="inputAction">cmd操作</param>
        /// <param name="DataReceived">回调方法</param>
        /// <param name="exitTime">以秒为单位</param>
        public static void ExecBatCommand(string workdir, Action<Action<string>> inputAction, DataReceivedEventHandler DataReceived, int exitTime)
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

                if (DataReceived != null)
                {
                    pro.OutputDataReceived += DataReceived; //(sender, e) => Console.WriteLine(e.Data);
                }
                pro.ErrorDataReceived += (sender, e) => ZLLog.L(ZLEnum.Log.content, "命令行消息返回:" + e.Data);
                pro.Start();
                sIn = pro.StandardInput;
                sIn.AutoFlush = true;
                //pro.BeginOutputReadLine();该句和下方的pro.StandardOutput.ReadToEnd(),有可能导致卡死,如出现卡顿可移除
                inputAction(value => sIn.WriteLine(value));
                string output = pro.StandardOutput.ReadToEnd();//这句用于获取输出的异常信息,该信息不会被DataReceived_Func等接收到
                if (!string.IsNullOrEmpty(output)) { ZLLog.L(output); }
                //如到期未停止,则强行终止
                if (exitTime > 0) { pro.WaitForExit(exitTime * 1000); }
                else { pro.WaitForExit(); }
            }
            finally
            {
                if (pro != null && !pro.HasExited) { pro.Kill(); }
                if (sIn != null) { sIn.Close(); }
                if (sOut != null) { sOut.Close(); }
                if (pro != null) { pro.Close(); }
            }
        }
        //public void DataReceived_Func(object sender, DataReceivedEventArgs e)
        //{
        //    if (e == null || string.IsNullOrEmpty(e.Data)) { return; }
        //    if (e.Data.Contains("BUILD SUCCESSFUL"))
        //    {
        //        ((Process)sender).Kill();
        //        Console.WriteLine("生成完成,退出");
        //    }
        //    else
        //    {
        //        Console.WriteLine(e.Data);
        //    }
        //}
    }
}
