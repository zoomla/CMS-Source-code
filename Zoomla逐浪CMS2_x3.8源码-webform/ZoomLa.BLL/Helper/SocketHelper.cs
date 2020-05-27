using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ZoomLa.Model;
/*
 * Socket通信辅助类,用于银行接口
 */ 
namespace ZoomLa.BLL.Helper
{
    public class SocketHelper
    {
        //netstat

        /// <summary>
        /// Socket发送
        /// </summary>
        /// <param name="serverip">127.0.0.1</param>
        /// <param name="port">8885</param>
        /// <returns>服务端返回的字符串</returns>
        public string SendToServer(string serverip, int port, string msg)
        {
            byte[] result = new byte[102400];
            IPAddress ipMod = IPAddress.Parse(serverip);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(new IPEndPoint(ipMod, port));
                client.Send(Encoding.ASCII.GetBytes(msg)); //发送byte[] 
                int len = client.Receive(result);
                return Encoding.ASCII.GetString(result, 0, len);
            }
            catch (Exception ex)
            {
                ZLLog.L(ZoomLa.Model.ZLEnum.Log.exception, new M_Log() { Action = "Socket通信", Message = "服务端:" + serverip + ":" + port + ",原因:" + ex.Message });
                throw new Exception("连接服务器失败,原因:" + ex.Message);
            }
            finally
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}
