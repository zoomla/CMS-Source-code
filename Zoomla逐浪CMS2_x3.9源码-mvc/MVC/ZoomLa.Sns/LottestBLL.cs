using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
  public  class LottestBLL
    {
          #region 添加缘分配对记录
            /// <summary>
            /// 
            /// </summary>
            /// <param name="ln"></param>
            /// <returns></returns>
          public Guid InsertLot(LotNote ln)
          {
              return LotTestLogic.InsertLot(ln);
          }
          #endregion


          #region 查询记录
            /// <summary>
            /// 查询记录
            /// </summary>
            /// <param name="sID"></param>
            /// <param name="eID"></param>
            /// <param name="i"></param>
            /// <returns></returns>
      public LotNote CheckLot(Guid sID, Guid eID, int i)
      {
          return LotTestLogic.CheckLot(sID, eID, i);
      }
          #endregion

      #region 读取信息表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guid GetMessageID()
        {
            return LotTestLogic.GetMessageID();
        }
      #endregion

      #region 读取信息表内容
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetMessage(Guid ID)
        {
            return LotTestLogic.GetMessage(ID);
        }
      #endregion

  }
}
