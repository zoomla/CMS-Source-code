using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///I4GiveUTypeҵ��ʵ��
    /// </summary>
    [Serializable]
    public class I4GiveUType
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///����������
        ///</summary>
        private string tyepName = String.Empty;


        #endregion

        #region ���캯��

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

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///����������
        ///</summary>
        public string TyepName
        {
            get { return tyepName; }
            set { tyepName = value; }
        }

        #endregion

    }
}
