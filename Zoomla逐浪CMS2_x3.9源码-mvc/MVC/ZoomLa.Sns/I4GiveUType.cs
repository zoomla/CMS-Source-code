using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///I4GiveUType业务实体
    /// </summary>
    [Serializable]
    public class I4GiveUType
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///忏悔类别名称
        ///</summary>
        private string tyepName = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public I4GiveUType()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public I4GiveUType
        (
            Guid iD,
            string tyepName
        )
        {
            this.iD = iD;
            this.tyepName = tyepName;

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
        ///忏悔类别名称
        ///</summary>
        public string TyepName
        {
            get { return tyepName; }
            set { tyepName = value; }
        }

        #endregion

    }
}
