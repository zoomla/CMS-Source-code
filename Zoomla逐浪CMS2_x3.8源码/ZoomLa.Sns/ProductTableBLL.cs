using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using FHModel;
using BDUModel;

namespace FHBLL
{
   public class ProductTableBLL
    {
       #region 读取商品类型
        /// <summary>
        /// 读取商品类型
        /// </summary>
        /// <returns></returns>
       public new List<ProductTypetable> GetType()
       {
           return ProductTableLogic.GetType();
       }
       #endregion

       #region 添加商品
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Guid InsertProduct(ProductTable pt)
        {
            return ProductTableLogic.InsertProduct(pt);
        }
       #endregion

       #region 读取商品
        /// <summary>
        /// 读取商品
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<ProductTable> GetProductTable(PagePagination page)
        {
            return ProductTableLogic.GetProductTable(page);
        }
       #endregion

       #region 删除商品
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="delid"></param>
       public  void DelProduct(string delid)
       {
           ProductTableLogic.DelProduct(delid);
       }
       #endregion

       #region 根据ID读取数据
        /// <summary>
        /// 根据ID读取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ProductTable GetPtByID(Guid ID)
        {
            return ProductTableLogic.GetPtByID(ID);
        }
       #endregion

       #region 修改商品
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="pt"></param>
        public void UpdateProductByID(ProductTable pt)
        {
            ProductTableLogic.UpdateProductByID(pt);
        }
       #endregion

       #region 根据类型读取商品
        /// <summary>
        /// 读取商品
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<ProductTable> GetProductTableBytID(Guid TypeID,PagePagination page)
        {
            return ProductTableLogic.GetProductTableBytID(TypeID, page);
        }
       #endregion

       #region 购买商品
        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="usp"></param>
        /// <returns></returns>
        public Guid InsertShop(UserShopProduct usp)
        {
            return ProductTableLogic.InsertShop(usp);
        }
       #endregion

       #region 读取用户购买商品
        /// <summary>
        /// 读取用户购买商品
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserShopProduct> GetUserShop(int UserID)
        {
            return ProductTableLogic.GetUserShop(UserID);
        }
       #endregion
   }
}
