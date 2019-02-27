using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
using System.Data.Common;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Message
{
    public class B_Guest_Medals : ZL_Bll_InterFace<M_Guest_Medals>
    {
        public string TbName = "", PK = "";
        M_Guest_Medals initMod = new M_Guest_Medals();
        public B_Guest_Medals()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName,PK,ID);
        }
        public int AddMedal_U(int id,int sender)
        {
            if (CheckMedalDiff(id, sender)) { return -2; }//不能重复颁发
            B_Guest_Bar barbll = new B_Guest_Bar();
            M_Guest_Bar barmod = barbll.SelReturnModel(id);
            M_GuestBookCate catemod = new B_GuestBookCate().SelReturnModel(barmod.CateID);
            int medalid = 2;//吧主勋章
            //不是吧主扣除积分
            if (!catemod.IsBarOwner(sender))
            {
                if (barmod.CUser == sender) { return -3; }//非吧主不能给自己颁发勋章
                medalid = 1;//网友勋章
                B_User buser = new B_User();
                M_UserInfo mu = buser.SelReturnModel(sender);
                if (mu.UserExp <= 0) { return -1; }//用户积分不足
                buser.ChangeVirtualMoney(sender, new M_UserExpHis()
                {
                    score = -1,
                    ScoreType = (int)M_UserExpHis.SType.Point,
                    detail = string.Format("{0} {1}在版面:{2}发表勋章给:[{3}]的贴子,扣除{4}分", DateTime.Now, mu.UserName, catemod.CateName, barmod.Title, catemod.SendScore)
                });
            }
            Insert(new M_Guest_Medals() { UserID = barmod.CUser, BarID = id, MedalID = medalid, Sender = sender });
            return medalid;
        }
        /// <summary>
        /// 检测勋章颁发者是否颁发过此贴
        /// True:是
        /// </summary>
        public bool CheckMedalDiff(int barid, int sender)
        {
            return DBCenter.Count(TbName, "BarID=" + barid + " AND Sender=" + sender) > 0;
        }
        public int Insert(M_Guest_Medals model)
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
        /// <summary>
        /// 获取贴子所得勋章
        /// </summary>
        public DataTable SelByBid(int barid)
        {
            return DBCenter.Sel(TbName, "BarID=" + barid);
        }
        public string GetMedalIcon(DataTable dt)
        {
            string htmltlp = "<span class='fa-stack fa-lg'><i class='fa fa-sun-o fa-stack-2x'></i><i class='fa fa-stack-1x '>@type</i></span>";
            string iconhtml = "";
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr["medalid"].ToString())
                {
                    case "1":
                        iconhtml += htmltlp.Replace("@type", "网");
                        break;
                    case "2":
                        iconhtml += htmltlp.Replace("@type", "版");
                        break;
                    case "3":
                        iconhtml += htmltlp.Replace("@type", "系");
                        break;
                }
            }
            return iconhtml;
        }
        /// <summary>
        /// 批量查询多个贴子的勋章
        /// </summary>
        public DataTable SelByBarIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.Sel(TbName, "BarID IN (" + ids + ")");
        }
        public DataTable SelByUid(int uid)
        {
            return DBCenter.Sel(TbName, "UserID=" + uid);
        }
        public M_Guest_Medals SelReturnModel(int ID)
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
        public bool UpdateByID(M_Guest_Medals model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
    }
}
