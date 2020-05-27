using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
   public class InboxBLL
    {
       #region 添加邮件
       /// <summary>
       /// 添加邮件
       /// </summary>
       /// <param name="it"></param>
       /// <returns></returns>
       public Guid InsertInbox(InboxTable it)
       {
           return InboxLogic.InsertInbox(it);
       }
       #endregion

       #region 读取某用户收件箱
       /// <summary>
       /// 读取某用户收件箱
       /// </summary>
       /// <param name="InceptID"></param>
       /// <returns></returns>
       public List<InboxTable> Getinbox(Guid InceptID)
       {
           return InboxLogic.Getinbox(InceptID);
       }
       #endregion

       #region 修改邮件的阅读状态
       /// <summary>
       /// 修改邮件的阅读状态
       /// </summary>
       /// <param name="ID"></param>
       public void UpdateState(Guid ID)
       {
           InboxLogic.UpdateState(ID);
       }
       #endregion

       #region 根据ID读取数据
       /// <summary>
       /// 根据ID读取数据
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public InboxTable GetInboxbyID(Guid ID)
       {
           return InboxLogic.GetInboxbyID(ID);
       }
       #endregion

        #region 读取新邮件
       /// <summary>
       /// 读取新邮件
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public int GetInboxCount(Guid UserID, int state)
       {
           return InboxLogic.GetInboxCount(UserID, state);
       }
        #endregion

       #region 根据ID删除记录
       /// <summary>
       /// 根据ID删除记录
       /// </summary>
       /// <param name="ID"></param>
       public void DelinboxByid(Guid ID)
       {
           InboxLogic.DelinboxByid(ID);
       }
       #endregion

       #region 读取某用户发件箱
       /// <summary>
       /// 读取某用户发件箱
       /// </summary>
       /// <param name="InceptID"></param>
       /// <returns></returns>
       public List<InboxTable> Getinboxsend(Guid InceptID)
       {
           return InboxLogic.Getinboxsend(InceptID);
       }
       #endregion
   }
}
