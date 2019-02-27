namespace ZoomLa.BLL.Message
{
using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model.Message;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
    using ZoomLa.Model;
using System.Data.Common;
using ZoomLa.SQLDAL.SQL;

    public class B_BaikeEdit
    {
        private string PK, TbName = "";
        private M_BaikeEdit initMod = new M_BaikeEdit();
        public B_BaikeEdit()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_BaikeEdit model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_BaikeEdit model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public void DelByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return;
            SafeSC.CheckIDSEx(ids);
            DBCenter.DelByIDS(TbName, PK, ids);
        }
        public void BatStatus(string ids, int status)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=" + status, PK + " IN (" + ids + ")");
        }
        public void BatReject(string ids, string msg)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("msg", msg));
            DBCenter.UpdateSQL(TbName, "AdminRemind=@msg,Status=" + (int)ZLEnum.ConStatus.Reject, PK + " IN (" + ids + ")", sp);
        }
        public M_BaikeEdit SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public DataTable SelBy(int status, string flow, string skey)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "1=1 ";
            if (status != -100) { where += " AND Status=" + status; }
            if (!string.IsNullOrEmpty(skey)) { where += " AND Tittle LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            if (!string.IsNullOrEmpty(flow)) { where += " AND Flow =@flow"; sp.Add(new SqlParameter("flow", flow)); }
            return DBCenter.Sel(TbName, where, PK + " DESC", sp);
        }
        /// <summary>
        /// 使用该版词条替代原有的词条
        /// </summary>
        public void Apply(int id)
        {
            B_Baike bkBll = new B_Baike();
            M_BaikeEdit source = SelReturnModel(id);
            M_Baike target = bkBll.SelModelByFlow(source.Flow);
            ConverToEdit(target, source, "all");
            target.Status = 1;
            bkBll.UpdateByID(target);
        }
        //-----------------------------------User
        public DataTable U_Sel(int uid, int status)
        {
            string where = "UserID=" + uid;
            if (status != -100)
            {
                where += " AND Status=" + status;
            }
            return DBCenter.Sel(TbName, where, PK + " DESC");
        }
        public PageSetting SelPage(int cpage, int psize, int uid, int status)
        {
            string where = "1=1 ";
            if (uid > 0) { where += " AND UserID=" + uid; }
            if (status != -100)
            {
                where += " AND Status=" + status;
            }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        //-----------------------------------Tools
        public void ConverToEdit(M_Baike target, M_Baike source, string type = "")
        {
            if (type.Equals("all"))//完成拷贝,新建百科时复制一份入Baike_Edit
            {
                target.UserId = source.UserId;
                target.UserName = source.UserName;
                target.AddTime = source.AddTime;
                target.Status = source.Status;
                target.UpdateTime = source.UpdateTime;
                target.VerStr = source.VerStr;
                target.OldID = source.OldID;
            }
            target.EditID = source.EditID;
            target.Tittle = source.Tittle;
            target.Contents = source.Contents;
            target.Reference = source.Reference;
            target.Btype = source.Btype;
            target.Extend = source.Extend;
            target.Elite = source.Elite;
            target.Brief = source.Brief;
            target.BriefImg = source.BriefImg;
            target.Classification = source.Classification;
            target.Editnumb = source.Editnumb;
            target.GradeIDS = source.GradeIDS;
            target.Flow = source.Flow;
        }
        public string GetStatus(int status)
        {
            switch (status)
            {
                case (int)ZLEnum.ConStatus.Reject:
                    return "<span style='color:red;'>未通过</span>";
                case 1:
                    return "<span style='color:green;'>已通过</span>";
                case (int)ZLEnum.ConStatus.UnAudit:
                default:
                    return "未审核";
            }
        }
    }
}
