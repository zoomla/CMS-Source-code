using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///BooDouUserҵ��ʵ��
    /// </summary>
    [Serializable]
    public class BooDouUser
    {
        #region �ֶζ���

        ///<summary>
        ///�û�ID
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///���벼����ʱ��
        ///</summary>
        private DateTime booDouTime;


        #endregion

        #region ���캯��

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

        #region ���Զ���

        ///<summary>
        ///�û�ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///���벼����ʱ��
        ///</summary>
        public DateTime BooDouTime
        {
            get { return booDouTime; }
            set { booDouTime = value; }
        }

        #endregion

    }
}
