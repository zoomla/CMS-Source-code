using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///SystemBooDouChance业务实体
    /// </summary>
    [Serializable]
    public class SystemBooDouChance
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///奖惩类型
        ///</summary>
        private int booDouType;

        ///<summary>
        ///奖惩数值
        ///</summary>
        private int booDouNum;

        ///<summary>
        ///奖惩内容
        ///</summary>
        private string booDouText = String.Empty;

        ///<summary>
        ///奖励类型：如兜币，现金，精神
        ///</summary>
        private int encouragementType;


        #endregion

        #region 构造函数

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
        ///奖惩类型
        ///</summary>
        public int BooDouType
        {
            get { return booDouType; }
            set { booDouType = value; }
        }

        ///<summary>
        ///奖惩数值
        ///</summary>
        public int BooDouNum
        {
            get { return booDouNum; }
            set { booDouNum = value; }
        }

        ///<summary>
        ///奖惩内容
        ///</summary>
        public string BooDouText
        {
            get { return booDouText; }
            set { booDouText = value; }
        }

        ///<summary>
        ///奖励类型：如兜币，现金，精神
        ///</summary>
        public int EncouragementType
        {
            get { return encouragementType; }
            set { encouragementType = value; }
        }

        #endregion

    }
}
