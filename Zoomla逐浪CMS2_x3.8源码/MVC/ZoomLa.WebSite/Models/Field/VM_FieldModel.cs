using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Models.Field
{
    public class VM_FieldModel : VM_Base
    {
        private B_ModelField fieldBll = new B_ModelField();
        private B_Content conBll = new B_Content();
        //------------------------------------
        //解析配置,未配置则默认用后端方式
        public ModelConfig config = null;
        //public ModelConfig modcfg = null;
        //指定模型的列表字段
        public DataTable fieldDT = null;
        //-----------------------------------用于解析
        //字段模型,foreach中使用
        public M_ModelField fieldMod = null;
        //如果是修改,该项不能为空
        public DataRow valueDR = null;
        //-------------
        public string UploadDir = (SiteConfig.SiteOption.UploadDir).TrimEnd('/') + "/";
        public string ManageDir = SiteConfig.SiteOption.ManageDir;
        public int ModelID, NodeID, GeneralID;
        public VM_FieldModel(int modelid, int nodeid, ModelConfig modcfg, int gid , DataRow valueDr = null)
        {
            this.config = modcfg;
            this.ModelID = modelid;
            this.NodeID = nodeid;
            //暂只用于内容管理处
            fieldDT = fieldBll.SelByModelID(ModelID, false, false);
            if (gid > 0)//如果指定了gid
            {
                this.GeneralID = gid;
                DataTable valueDT = conBll.GetContent(gid);
                if (valueDT != null && valueDT.Rows.Count > 0)
                {
                    valueDR = valueDT.Rows[0];
                }
            }
            if (valueDr != null) { this.valueDR = valueDr; }
        }
        /// <summary>
        /// 仅用于注册页面
        /// </summary>
        public VM_FieldModel(DataTable fielddt, ModelConfig modcfg)
        {
            fieldDT = fielddt;
            this.config = modcfg;
        }
        //------------------------------Tools
        public string GetValue(string fname = "")
        {
            if (string.IsNullOrEmpty(fname)) { fname = fieldMod.FieldName; }
            if (valueDR == null || !valueDR.Table.Columns.Contains(fname)) { return ""; }
            return DataConvert.CStr(valueDR[fname]);
        }
    }
}