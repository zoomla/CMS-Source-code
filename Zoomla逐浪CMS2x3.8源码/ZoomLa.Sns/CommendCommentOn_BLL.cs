using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class CommendCommentOn_BLL
    {
        #region 添加推荐评论
        /// <summary>
        /// 添加推荐评论
        /// </summary>
        /// <param name="ccon"></param>
        public void Add(CommendCommentOn ccon)
        {
            CommendCommentOn_Logic.Add(ccon);
        }
        #endregion

        #region 删除推荐评论
        /// <summary>
        /// 删除推荐评论
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            CommendCommentOn_Logic.Del(id);
        }
        #endregion

        #region 根据推荐ID查询评论
        /// <summary>
        /// 根据推荐ID查询评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommendCommentOn> GetCommendCommentOn(Guid id,PagePagination page)
        {
            return CommendCommentOn_Logic.GetCommendCommentOn(id,page );
        }
        #endregion
    }
}
