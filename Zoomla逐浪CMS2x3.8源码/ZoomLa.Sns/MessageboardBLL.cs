using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class MessageboardBLL
    {
        #region 添加留言
        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public Guid InsertMessage(Messageboard me)
        {
            return MessageboardLogic.InsertMessage(me);
        }
        #endregion

        #region 读取用户留言信息
        /// <summary>
        /// 读取用户留言信息
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public List<Messageboard> GetMessageByInceptID(int InceptID)
        {
            return MessageboardLogic.GetMessageByInceptID(InceptID);
        }
        #endregion

        #region 读取留言回复信息
        /// <summary>
        /// 读取留言回复信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<Messageboard> GetRestoreMessageByID(Guid ID)
        {
            return MessageboardLogic.GetRestoreMessageByID(ID);
        }
        #endregion

        #region 删除留言
        public void DelMessage(Guid ID)
        {
            MessageboardLogic.DelMessage(ID);
        }
        #endregion

        #region 留言
        /// <summary>
        /// 读取用户自己留言或回复信息
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public List<Messageboard> GetMessageBySendID(int SendID, Guid RestoreID)
        {
            return MessageboardLogic.GetMessageBySendID(SendID, RestoreID);
        }
        #endregion
    }
}
