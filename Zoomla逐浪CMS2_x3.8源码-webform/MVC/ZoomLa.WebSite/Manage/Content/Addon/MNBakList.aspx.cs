using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Content.Addon
{
    public partial class MNBakList : System.Web.UI.Page
    {
        B_MNBackup mnBll = new B_MNBackup();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!IsPostBack)
            {
                SafeSC.CreateDir(mnBll.vdir);
                MyBind();
                Call.HideBread(Master);
                //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/DatalistProfile.aspx'>扩展功能</a></li> <li><a href='" + CustomerPageAction.customPath2 + "Config/RunSql.aspx'>开发中心</a></li><li><a href='"+Request.RawUrl+"'>备份列表</a></li>");
            }
        }
        public void MyBind()
        {
            EGV.DataSource = mnBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string name = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "download":
                    SafeSC.DownFile(mnBll.vdir + name, name);
                    break;
                case "restore":
                    mnBll.RestoreByName(name);
                    function.WriteSuccessMsg("恢复完成");
                    break;
                case "del2":
                    SafeSC.DelFile(mnBll.vdir + name);
                    function.WriteSuccessMsg("删除完成");
                    break;
            }
        }
        protected void CreateBak_Btn_Click(object sender, EventArgs e)
        {
            mnBll.CreateBak();
            function.WriteSuccessMsg("备份创建成功");
        }
        public string GetFileSize()
        {
            return FileSystemObject.ConvertSizeToShow((long)Eval("Size"));
        }
    }
        public class B_MNBackup
    {
        //需要备份和清空的表
        private string[] tbnames = "ZL_Node,ZL_Node_ModelTemplate,ZL_Model,ZL_ModelField,ZL_UserPromotions,ZL_NodeRole,ZL_Pub".Split(',');
        public string vdir, pdir = "";
        public B_MNBackup()
        {
            //vdir = "/Storage/MNBackup/";
            vdir = SiteConfig.SiteOption.TemplateDir + "/System/";
            pdir = function.VToP(vdir);
        }
        public DataTable Sel()
        {
            DataTable dt = FileSystemObject.GetDirectoryInfos(pdir, FsoMethod.File);
            return dt;
        }
        //互动模型需要扩展
        public void RestoreByName(string name)
        {
            B_Model modelBll = new B_Model();
            B_ModelField fieldBll = new B_ModelField();
            B_Pub pubBll = new B_Pub();
            string ppath = SafeSC.PathDeal(pdir + name);
            if (!File.Exists(ppath)) { function.WriteErrMsg("备份文件[" + name + "]不存在"); }
            //清空数据
            modelBll.DeleteAll();
            foreach (string tbname in tbnames)
            {
                SqlHelper.ExecuteSql("TRUNCATE TABLE " + tbname);
            }
            SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_CommonModel");
            //开始恢复
            DataSet ds = new DataSet();
            ds.ReadXml(ppath);
            //批量导入部分数据
            SqlHelper.Insert_Bat(ds.Tables["ZL_Node"], SqlHelper.ConnectionString);
            if (ds.Tables["ZL_Pub"] != null) { SqlHelper.Insert_Bat(ds.Tables["ZL_Pub"], SqlHelper.ConnectionString); }
            if (ds.Tables["ZL_Node_ModelTemplate"] != null) { SqlHelper.Insert_Bat(ds.Tables["ZL_Node_ModelTemplate"], SqlHelper.ConnectionString); }
            if (ds.Tables["ZL_UserPromotions"] != null) { SqlHelper.Insert_Bat(ds.Tables["ZL_UserPromotions"], SqlHelper.ConnectionString); }
            if (ds.Tables["ZL_NodeRole"] != null) { SqlHelper.Insert_Bat(ds.Tables["ZL_NodeRole"], SqlHelper.ConnectionString); }
            //恢复模型数据与其对应的表
            DataTable modelDT = ds.Tables["ZL_Model"];
            DataTable fieldDT = ds.Tables["ZL_ModelField"];
            DataTable pubDT = ds.Tables["ZL_Pub"];
            foreach (DataRow dr in modelDT.Rows)
            {
                M_ModelInfo modelMod = new M_ModelInfo().GetModelFromReader(dr);
                ZoomLa.SQLDAL.DBHelper.Table_Del(modelMod.TableName);
                switch (modelMod.ModelType.ToString())
                {
                    case "3"://用户模型和黄页申请模型
                        modelBll.AddUserModel(modelMod);
                        break;
                    case "6"://店铺申请模型
                        modelBll.AddStoreModel(modelMod);
                        break;
                    case "7"://互动模型
                        if (pubDT == null) { break; }
                        M_Pub pubmodel = new M_Pub();
                        pubmodel.PubName = modelMod.ModelName;
                        pubmodel.PubTableName = modelMod.TableName;
                        DataRow[] drs = pubDT.Select("PubModelID=" + modelMod.ModelID);
                        pubmodel.PubType = drs.Length > 0 ? DataConvert.CLng(drs[0]["PubType"]) : 0;
                        pubBll.CreateModelInfo(pubmodel);
                        break;
                    case "8"://功能模型
                        modelBll.AddFunModel(modelMod);
                        break;
                    default://内容模型、商城模型、黄页内容模型、店铺商品模型
                        modelBll.AddModel(modelMod);
                        break;
                }
            }
            //恢复字段
            if (fieldDT != null && fieldDT.Rows.Count > 0)
            {
                fieldDT.DefaultView.RowFilter = "sys_type ='false'";
                foreach (DataRow dr in fieldDT.DefaultView.ToTable().Rows)
                {
                    int modelID = DataConvert.CLng(dr["ModelID"]);
                    if (modelID < 1) { continue; }
                    DataRow[] drs = modelDT.Select("ModelID='" + modelID + "'");
                    //OA不处理,未找到不处理
                    if (drs.Length < 1 || drs[0]["ModelType"].ToString().Equals("12")) { continue; }
                    M_ModelInfo modelMod = new M_ModelInfo().GetModelFromReader(drs[0]);
                    M_ModelField fieldMod = new M_ModelField().GetModelFromReader(dr);
                    //Pub表会报重复
                    if (fieldBll.IsExists(modelMod.ModelID, fieldMod.FieldName)) { continue; }
                    if (DBCenter.DB.Table_Exist(modelMod.TableName)) //互动表可能未创建,忽略
                    {
                        fieldBll.AddModelField(modelMod, fieldMod);
                    }
                }
                SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_ModelField");
                SqlHelper.Insert_Bat(fieldDT, SqlHelper.ConnectionString);
            }
        }
        public void CreateBak()
        {
            string vpath = vdir + DateTime.Now.ToString("yyyyMMdd_") + function.GetRandomString(4) + ".config";
            string ppath = function.VToP(vpath);
            DataSet ds = new DataSet();
            foreach (string table in tbnames)
            {
                DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM " + table);
                dt.TableName = table;
                ds.Tables.Add(dt.Copy());
                dt.Dispose();
            }
            ds.WriteXml(ppath);
        }
        //------------------------Tools
    }
}