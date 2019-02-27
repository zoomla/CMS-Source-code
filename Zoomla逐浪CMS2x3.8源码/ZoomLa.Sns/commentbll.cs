using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Sns.Model;
using ZoomLa.Sns.Logic;

namespace ZoomLa.Sns.BLL
{
    public class commentbll
    {
        #region 查询某类型的评论
        /// <summary>
        /// 查询某类型的评论
        /// </summary>
        /// <param name="cID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<CommentAll> GetCommentBycbyID(Guid cID)
        {
            return commentlogic.GetCommentBycbyID(cID);
        }
        #endregion

        #region 查询最新几条评论
        /// <summary>
        /// 查询最新几条评论
        /// </summary>
        /// <param name="topx"></param>
        /// <returns></returns>
        public List<CommentAll> GetTopcomment(int topx)
        {
            return commentlogic.GetTopcomment(topx);
        }
        #endregion

        #region 读取用户的评论
        /// <summary>
        /// 读取用户的评论
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<CommentAll> GetcommandByUserID(Guid UserID)
        {
            return commentlogic.GetcommandByUserID(UserID);
        }
        #endregion

        #region 删除评论
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="ID"></param>
        public void Delcommant(Guid ID)
        {
            commentlogic.Delcommant(ID);
        }
        #endregion

        #region 添加评论
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        public Guid Insertcomment(CommentAll ca)
        {
            return commentlogic.Insertcomment(ca);
        }
        #endregion
    }
}
