using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///UserEmailValidate业务实体
    /// </summary>
    [Serializable]
    public class UserEmailValidate
    {
        #region 字段定义

        ///<summary>
        ///ID
        ///</summary>
        private Guid id = Guid.Empty;

        ///<summary>
        ///邮箱
        ///</summary>
        private string userEmail = String.Empty;

        ///<summary>
        ///用户昵称
        ///</summary>
        private string userName = String.Empty;

        ///<summary>
        ///用户密码
        ///</summary>
        private string userpwd = String.Empty;      

        ///<summary>
        ///注册认证激活码
        ///</summary>
        private string validate = String.Empty;

        ///<summary>
        ///注册时间
        ///</summary>
        private DateTime creatTime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserEmailValidate()
        {
        }

      
        #endregion

        #region 属性定义

        ///<summary>
        ///ID
        ///</summary>
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        ///<summary>
        ///用户名称
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        ///<summary>
        ///用户密码
        ///</summary>
        public string Userpwd
        {
            get { return userpwd; }
            set { userpwd = value; }
        }


        ///<summary>
        ///邮箱
        ///</summary>
        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }

        ///<summary>
        ///注册认证激活码
        ///</summary>
        public string Validate
        {
            get { return validate; }
            set { validate = value; }
        }

        ///<summary>
        ///注册时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        #endregion
    }
}
