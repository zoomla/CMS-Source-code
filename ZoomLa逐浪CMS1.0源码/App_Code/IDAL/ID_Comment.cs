namespace ZoomLa.IDAL
{
    using ZoomLa.Model;
    using System;
    using System.Data;

    /// <summary>
    /// 评论信息
    /// </summary>
    public interface ID_Comment
    {
        /// <summary>
        /// 发表(增加)评论
        /// </summary>
        /// <param name="commentInfo"></param>
        /// <returns></returns>
        bool Add(M_Comment commentInfo);
        /// <summary>
        /// 根据ID更新审核
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        bool AuditedByID(int commentID,bool Audited);
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        bool Delete(int commentID);
        /// <summary>
        /// 查询某节点所有评论
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        DataTable SeachCommentAll(int nodeID);
        DataTable SeachComment();
        DataTable SeachCommentAudit(bool Audited);
        /// <summary>
        /// 查询某内容的 所有评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <returns></returns>
        DataTable SeachComment_ByItemID(int generalID);        
        /// <summary>
        /// 查询某内容指定标题的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        DataTable SeachComment_ByIDTitle(int generalID,string Title);
        /// <summary>
        /// 获取某内容指定标题的评论的支持方总数
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        int GetComment_CountIDTitlePK(int generalID, string Title,bool PK);
        /// <summary>
        /// 根据CommentID查询评论并实例化
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        M_Comment GetCommen_ByCommentID(int commentID);
        /// <summary>
        /// 查询某节点的是否审核通过的评论
        /// </summary>
        /// <param name="Audited"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        DataTable SeachComment_ByAudited(bool Audited, int nodeID);
        /// <summary>
        /// 查询用户在某节点下发表的评论
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        DataTable SearchByUser(int UserID, int NodeID);

    }
}
