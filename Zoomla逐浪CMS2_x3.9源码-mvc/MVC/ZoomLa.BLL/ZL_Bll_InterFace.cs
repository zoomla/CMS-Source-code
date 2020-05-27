using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZoomLa.BLL
{
    internal interface ZL_Bll_InterFace <T>
    {
         int Insert(T model);
        /// <summary>
        /// 根据ID更新
        /// </summary>
         bool UpdateByID(T model);
         bool Del(int ID);
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
         T SelReturnModel(int ID);
        /// <summary>
        /// 查询所有记录
        /// </summary>
         DataTable Sel();
    }
}
