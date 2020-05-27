using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///SystemBooDouChanceҵ��ʵ��
    /// </summary>
    [Serializable]
    public class SystemBooDouChance
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///��������
        ///</summary>
        private int booDouType;

        ///<summary>
        ///������ֵ
        ///</summary>
        private int booDouNum;

        ///<summary>
        ///��������
        ///</summary>
        private string booDouText = String.Empty;

        ///<summary>
        ///�������ͣ��綵�ң��ֽ𣬾���
        ///</summary>
        private int encouragementType;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public SystemBooDouChance()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemBooDouChance
        (
            Guid iD,
            int booDouType,
            int booDouNum,
            string booDouText,
            int encouragementType
        )
        {
            this.iD = iD;
            this.booDouType = booDouType;
            this.booDouNum = booDouNum;
            this.booDouText = booDouText;
            this.encouragementType = encouragementType;

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
        ///��������
        ///</summary>
        public int BooDouType
        {
            get { return booDouType; }
            set { booDouType = value; }
        }

        ///<summary>
        ///������ֵ
        ///</summary>
        public int BooDouNum
        {
            get { return booDouNum; }
            set { booDouNum = value; }
        }

        ///<summary>
        ///��������
        ///</summary>
        public string BooDouText
        {
            get { return booDouText; }
            set { booDouText = value; }
        }

        ///<summary>
        ///�������ͣ��綵�ң��ֽ𣬾���
        ///</summary>
        public int EncouragementType
        {
            get { return encouragementType; }
            set { encouragementType = value; }
        }

        #endregion

    }
}
