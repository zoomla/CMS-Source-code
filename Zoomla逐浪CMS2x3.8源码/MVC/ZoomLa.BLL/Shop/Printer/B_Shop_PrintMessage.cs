using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_PrintMessage : ZL_Bll_InterFace<M_Shop_PrintMessage>
    {
        private M_Shop_PrintMessage initMod = new M_Shop_PrintMessage();
        private string TbName, PK;
        private B_Shop_APIPrinter printBll = new B_Shop_APIPrinter();
        public B_Shop_PrintMessage()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Shop_PrintMessage model)
        {
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 添加打印记录，同时发送打印指令
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="tlpID">模板ID</param>
        /// <param name="devMod">打印设备Mod</param>
        /// <param name="num">打印数量，默认为1</param>
        public int Insert(string content, int tlpID, M_Shop_PrintDevice devMod, int num = 1)
        {
            M_Shop_PrintMessage msgMod = new M_Shop_PrintMessage();
            msgMod.ReqTime = DateTime.Now;
            msgMod.Detail = content;
            msgMod.DevID = devMod.ID;
            msgMod.Mode = 2;
            msgMod.TlpID = tlpID;
            for (int i = 0; i < num; i++)
            {
                msgMod.ReqStatus = printBll.SendFreeMessage(msgMod, devMod.DeviceNo);
            }
            msgMod.TaskStatus = "0";
            return Insert(msgMod);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public M_Shop_PrintMessage SelReturnModel(int ID)
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
        public bool UpdateByID(M_Shop_PrintMessage model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public PageSetting SelPage(int cpage, int psize, int devID = -100, string detail = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (devID != -100) { where += " AND A.DevID=" + devID; }
            if (!string.IsNullOrEmpty(detail)) { where += " AND A.Detail LIKE @detail"; sp.Add(new SqlParameter("detail", detail)); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, "A." + PK, where, "", sp);
            setting.fields = "A.*,B.Alias,B.ShopName";
            setting.t2 = "ZL_Shop_PrintDevice";
            setting.on = "A.DevID=B.ID";
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 后期加入模板,并可按打印机,模板,状态筛选
        /// </summary>
        public DataTable Search(int devID, string skey)
        {
            string fields = "A.*,B.Alias,B.ShopName";
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (devID > 0)
            {
                where += " AND A.DevID=" + devID;
            }
            if (!string.IsNullOrEmpty(skey)) { where += " AND A.Detail LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Shop_PrintDevice", "A.DevID=B.ID", where, "A.ID DESC", sp.ToArray());
        }
        //--------------------------------Tools
        public string DealReqStatus(string reqStatus)
        {
            if (reqStatus.Equals("0")) { return "<span style='color:green;'>发送成功</span>"; }
            else { return "<span style='color:red;'>发送失败(" + reqStatus + ")</span>"; }
        }
    }
}
