using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///BooDouUser业务实体
    /// </summary>
    [Serializable]
    public class BooDouUser
    {
        #region 字段定义

        ///<summary>
        ///用户ID
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///参与布兜的时间
        ///</summary>
        private DateTime booDouTime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public BooDouUser()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public BooDouUser
        (
            Guid userID,
            DateTime booDouTime
        )
        {
            this.userID = userID;
            this.booDouTime = booDouTime;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///用户ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///参与布兜的时间
        ///</summary>
        public DateTime BooDouTime
        {
            get { return booDouTime; }
            set { booDouTime = value; }
        }

        #endregion

    }
}
