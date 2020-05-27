using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using FHModel;
using BDUModel;

namespace FHBLL
{
   public class HomeSetBLL
    {
       #region 添加用户放置商品
       /// <summary>
       /// 添加用户放置商品
       /// </summary>
       /// <param name="hc"></param>
       /// <returns></returns>
       public Guid InsertHomeSet(HomeCollocate hc)
       {
           return HomeSetLogic.InsertHomeSet(hc);
       }
       #endregion

       #region 读取用户放置商品
       /// <summary>
       /// 读取用户放置商品
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public List<HomeCollocate> GetHomeProductByUserID(int UserID)
       {
           return HomeSetLogic.GetHomeProductByUserID(UserID);
       }
       #endregion

       #region 读取用户头像放置
       /// <summary>
       /// 读取用户头像放置
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public HomeHeadCollocate GetHomeSetHeadByUserID(int UserID)
       {
           return HomeSetLogic.GetHomeSetHeadByUserID(UserID);
       }
       #endregion

       #region 保存用户头像位置
       /// <summary>
       /// 保存用户头像位置
       /// </summary>
       /// <param name="hcc"></param>
       public  void UpdateHead(HomeHeadCollocate hcc)
       {
           HomeSetLogic.UpdateHead(hcc);
       }
       #endregion

       #region 保存商品位置
       /// <summary>
       /// 保存商品位置
       /// </summary>
       /// <param name="left"></param>
       /// <param name="top"></param>
       /// <param name="ID"></param>
       public void UpdateProduct(int left,int top,Guid ID)
       {
           HomeSetLogic.UpdateProduct(left, top, ID);
       }
       #endregion

       #region 检测商品是否使用
       /// <summary>
       /// 检测商品是否使用(使用中返回true)
       /// </summary>
       /// <param name="ShopID"></param>
       /// <returns></returns>
       public bool CheckShopid(Guid ShopID)
       {
           return HomeSetLogic.CheckShopid(ShopID);
       }
       #endregion

       #region 删除放置商品
       /// <summary>
       /// 删除放置商品
       /// </summary>
       /// <param name="ShopID"></param>
       public void DelHomeSet(Guid ShopID)
       {
           HomeSetLogic.DelHomeSet(ShopID);
       }
       #endregion

       #region 初始化用户小屋头像
       /// <summary>
       /// 初始化用户小屋头像
       /// </summary>
       /// <param name="hhc"></param>
       public void InsertUserHead(HomeHeadCollocate hhc)
       {
           HomeSetLogic.InsertUserHead(hhc);
       }
       #endregion

       #region 最新布置
       /// <summary>
       /// 最新布置
       /// </summary>
       /// <returns></returns>
       public List<HomeCollocate> GetNewUserSet(int i,PagePagination page)
       {
           return HomeSetLogic.GetNewUserSet(i, page);
       }
       #endregion

       //#region 得到大户人家
       ///// <summary>
       ///// 得到大户人家
       ///// </summary>
       ///// <param name="i"></param>
       ///// <returns></returns>
       //public List<UserTable> GetMostPeopel(int i, PagePagination page)
       //{
       //    return HomeSetLogic.GetMostPeopel(i, page);
       //}
       //#endregion
   }
}
