using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Sns.Model;
using ZoomLa.Sns.Logic;


namespace ZoomLa.Sns.BLL
{
    public class CollectTableBLL
    {
        #region 读取用户收藏状态
        /// <summary>
        /// 读取用户收藏状态
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ID"></param>
        /// <param name="stype"></param>
        /// <returns></returns>

        public CollectTable GetCollectByID(int UserID, Guid ID)
        {
            return CollectTableLogic.GetCollectByID(UserID, ID);
        }
        #endregion

        #region 修改收藏
       /// <summary>
       /// 修改收藏
       /// </summary>
       /// <param name="state"></param>
       /// <param name="LabelName"></param>
       /// <param name="ID"></param>
        public void UpdateCollect(int state, string LabelName, Guid ID)
        {
            CollectTableLogic.UpdateCollect(state, LabelName, ID);
        }
        #endregion

        #region 根据ID查询数据
        /// <summary>
       /// 根据ID查询数据
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public  CollectTable GetCollectTableByID(Guid ID)
       {
           return CollectTableLogic.GetCollectTableByID(ID);
       }
        #endregion

        #region 根据收藏ID读取标签
       /// <summary>
       /// 根据收藏ID读取标签
       /// </summary>
       /// <param name="Byid"></param>
       /// <returns></returns>
       public string GetLabelName(Guid Byid)
       {
           return CollectTableLogic.GetLabelName(Byid);
       }
        #endregion

        #region 读取某类型的所有标签
       /// <summary>
       /// 读取某类型的所有标签
       /// </summary>
       /// <param name="stype"></param>
       /// <returns></returns>
       public string GetAllLabelName(int stype)
       {
           return CollectTableLogic.GetAllLabelName(stype);
       }
        #endregion

        #region 查询某类型某标签下的记录
       /// <summary>
       /// 查询某类型某标签下的记录
       /// </summary>
       /// <param name="label"></param>
       /// <param name="stype"></param>
       /// <returns></returns>
       public List<Guid> GetBookBystate(string label, int stype)
       {
           return CollectTableLogic.GetBookBystate(label,stype);
       }
        #endregion

        #region 添加收藏
       /// <summary>
       /// 添加收藏
       /// </summary>
       /// <param name="ct"></param>
       /// <returns></returns>

       public Guid InsertCollect(CollectTable ct)
       {
           return CollectTableLogic.InsertCollect(ct);
       }
        #endregion

        #region 删除收藏
       /// <summary>
       /// 删除收藏
       /// </summary>
       /// <param name="ID"></param>
       public void DelCollect(Guid ID)
       {
           CollectTableLogic.DelCollect(ID);
       }
        #endregion

       #region 用户收藏信息
       /// <summary>
       /// 用户收藏信息
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="page"></param>
       /// <param name="stype"></param>
       /// <param name="state"></param>
       /// <returns></returns>
       public List<CollectTable> GetcollectByUserID(int UserID, int stype, int state)
       {
           return CollectTableLogic.GetcollectByUserID(UserID, stype, state);
       }
        #endregion

        #region 查询用户标签
       /// <summary>
       /// 查询用户标签
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="stype"></param>
       /// <returns></returns>
       public string GetUserLabel(int UserID, int stype)
       {
           return CollectTableLogic.GetUserLabel(UserID, stype);
       }
        #endregion
    }
}
