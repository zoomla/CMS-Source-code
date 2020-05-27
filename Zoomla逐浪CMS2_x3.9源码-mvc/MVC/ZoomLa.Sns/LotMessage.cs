/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： LotMessage.cs
// 文件功能描述：定义数据表LotMessage的业务实体
//
// 创建标识：Owner(2008-10-24) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///LotMessage业务实体
    /// </summary>
    [Serializable]
    public class LotMessage
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private string messageContent = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public LotMessage()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public LotMessage
        (
            Guid iD,
            string messageContent
        )
        {
            this.iD = iD;
            this.messageContent = messageContent;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }

        #endregion

    }
}
