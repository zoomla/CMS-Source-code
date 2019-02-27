namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;

    /// <summary>
    /// B_Comment 的摘要说明
    /// </summary>
    public class B_Comment
    {
        private static readonly ID_Comment comment = IDal.CreatComment();

        public B_Comment()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 发表(增加)评论
        /// </summary>
        /// <param name="commentInfo"></param>
        /// <returns></returns>
        public bool Add(M_Comment commentInfo)
        {
            return comment.Add(commentInfo);
        }
        
        /// <summary>
        /// 审核指定ID的评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public bool Update_ByAudited_ID(int commentID,bool Audited)
        {            
            return comment.AuditedByID(commentID,Audited);
        }
        /// <summary>
        /// 删除指定评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public bool Delete(int commentID)
        {
            return comment.Delete(commentID);
        }
        /// <summary>
        /// 查询节点的所有评论
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public DataTable SeachCommentAll(int nodeID)
        {
            return comment.SeachCommentAll(nodeID);
        }
        public DataTable SeachComment()
        {
            return comment.SeachComment();
        }
        public DataTable SeachCommentAudit(bool Audited)
        {
            return comment.SeachCommentAudit(Audited);
        }
        /// <summary>
        /// 查询某内容的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByGeneralID(int generalID)
        {
            return comment.SeachComment_ByItemID(generalID);
        }
        /// <summary>
        /// 查询某内容指定标题的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByIDTitle(int generalID, string Title)
        {
            return comment.SeachComment_ByIDTitle(generalID, Title);
        }
        /// <summary>
        /// 获取某内容指定标题的评论的支持方或反对方总数
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public int GetComment_CountIDTitlePK(int generalID, string Title, bool PK)
        {
            return comment.GetComment_CountIDTitlePK(generalID, Title, PK);
        }
        /// <summary>
        /// 查询某评论内容
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public M_Comment GetCommen_ByCommentID(int commentID)
        {
            return comment.GetCommen_ByCommentID(commentID);
        }
        /// <summary>
        /// 查询某节点的是否审核通过的评论
        /// </summary>
        /// <param name="Audited"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByAudited(bool Audited, int nodeID)
        {
            return comment.SeachComment_ByAudited(Audited, nodeID);
        }
        /// <summary>
        /// 查询用户发表的评论
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public DataTable SearchByUser(int UserID, int NodeID)
        {
            return comment.SearchByUser(UserID, NodeID);
        }

    }
}
