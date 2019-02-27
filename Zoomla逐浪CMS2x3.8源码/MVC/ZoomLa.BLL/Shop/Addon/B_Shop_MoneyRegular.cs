using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_MoneyRegular
    {
        private M_Shop_MoneyRegular initMod = new M_Shop_MoneyRegular();
        private string TbName, PK;
        public B_Shop_MoneyRegular()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Shop_MoneyRegular model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Shop_MoneyRegular SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 根据金额,获取最接近的一条数据(即值最大的)
        /// </summary>
        public M_Shop_MoneyRegular SelModelByMin(double money) 
        {
            string where = "Min<='" + money + "'";
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, "Min DESC"))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public bool UpdateByID(M_Shop_MoneyRegular model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        /// <summary>
        /// 用于订单支持后的回调页,根据充值金额对其赠送
        /// </summary>
        public void AddMoneyByMin(M_UserInfo mu, double money, string addon) 
        {
            B_User buser = new B_User();
            M_Shop_MoneyRegular regularMod = SelModelByMin(money);
            if (regularMod != null)
            {
                string remind = regularMod.UserRemind + "," + addon;
                if (regularMod.Purse > 0)
                {
                    buser.AddMoney(mu.UserID, regularMod.Purse, M_UserExpHis.SType.Purse, remind);
                }
                if (regularMod.Sicon > 0)
                {
                    buser.AddMoney(mu.UserID, regularMod.Sicon, M_UserExpHis.SType.SIcon, remind);
                }
                if (regularMod.Point > 0)
                {
                    buser.AddMoney(mu.UserID, regularMod.Point, M_UserExpHis.SType.Point, remind);
                }
            }
        }
        //--------------------
        public bool IsExist(double money)
        {
            //同种金额的不能重复添加
            return DBCenter.IsExist(TbName, "Min='" + money + "'");
        }
    }
}
