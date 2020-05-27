using System;


namespace BDUModel
{
    /// <summary>
    ///GroupPicCateg业务实体
    /// </summary>
    [Serializable]
    public class GroupPicCateg
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid groupID = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private Guid categID = Guid.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GroupPicCateg()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public GroupPicCateg
        (
            Guid groupID,
            Guid categID
        )
        {
            this.groupID = groupID;
            this.categID = categID;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public Guid CategID
        {
            get { return categID; }
            set { categID = value; }
        }

        #endregion

    }
}
