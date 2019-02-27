using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Sns;

namespace ZoomLa.Sns
{
   public class ChatLogBLL
    {
       #region 添加聊天记录
       /// <summary>
       /// 添加聊天记录
       /// </summary>
       /// <param name="cl"></param>
       public void InsertLog(ChatLog cl)
       {
           ChatLogLogic.InsertLog(cl);
       }
       #endregion

       #region 读取聊天记录
       /// <summary>
       /// 读取聊天记录
       /// </summary>
       /// <param name="Byid"></param>
       /// <returns></returns>
       public List<ChatLog> GetCharLog(int Byid)
       {
          return ChatLogLogic.GetCharLog(Byid);
       }
       #endregion

       #region 清空聊天记录
       /// <summary>
       /// 清空聊天记录
       /// </summary>
       public void DelChat()
       {
           ChatLogLogic.DelChat();
       }
       #endregion

   }
}
