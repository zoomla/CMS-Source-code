using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///FriendGroup业务实体
    /// </summary>
    [Serializable]
    public class FriendGroup
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///主人编号
        ///</summary>
        private Guid hostID = Guid.Empty;

        ///<summary>
        ///分组名称
        ///</summary>
        private string groupName = String.Empty;

        ///<summary>
        ///分组状态
        ///</summary>
        private int taxis;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public FriendGroup()
        {
        }

        ///<summary>
        ///
        ///</summary>


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
        ///主人编号
        ///</summary>
        public Guid HostID
        {
            get { return hostID; }
            set { hostID = value; }
        }

        ///<summary>
        ///分组名称
        ///</summary>
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        ///<summary>
        ///分组排序
        ///</summary>
        public int Taxis
        {
            get { return taxis; }
            set { taxis = value; }
        }

        #endregion
    }
}
