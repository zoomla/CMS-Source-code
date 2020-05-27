using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_CarConfigҵ��ʵ��
    /// </summary>
    [Serializable]
    public class ZL_Sns_CarConfig
    {
        #region �ֶζ���

        ///<summary>
        ///����
        ///</summary>
        private int iD;

        ///<summary>
        ///�ؼ���
        ///</summary>
        private string cKey = String.Empty;

        ///<summary>
        ///˵��
        ///</summary>
        private string ctext = String.Empty;

        ///<summary>
        ///ֵ
        ///</summary>
        private string cvalue = String.Empty;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_CarConfig()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_CarConfig
        (
            int iD,
            string cKey,
            string ctext,
            string cvalue
        )
        {
            this.iD = iD;
            this.cKey = cKey;
            this.ctext = ctext;
            this.cvalue = cvalue;

        }

        #endregion

        #region ���Զ���

        ///<summary>
        ///����
        ///</summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///�ؼ���
        ///</summary>
        public string CKey
        {
            get { return cKey; }
            set { cKey = value; }
        }

        ///<summary>
        ///˵��
        ///</summary>
        public string Ctext
        {
            get { return ctext; }
            set { ctext = value; }
        }

        ///<summary>
        ///ֵ
        ///</summary>
        public string Cvalue
        {
            get { return cvalue; }
            set { cvalue = value; }
        }

        #endregion

    }
}
