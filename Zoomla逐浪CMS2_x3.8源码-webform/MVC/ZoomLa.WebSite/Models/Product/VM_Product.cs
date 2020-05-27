using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Shop;
using Newtonsoft.Json;
using ZoomLa.Common;

namespace ZoomLaCMS.Models.Product
{
    //用于添加|修改商品页
    public class VM_Product
    {
        private B_Group gpBll = new B_Group();
        private B_Node nodeBll = new B_Node();
        private B_Product proBll=new B_Product();
        private B_Shop_FareTlp fareBll=new B_Shop_FareTlp();
        private B_ModelField fieldBll=new B_ModelField();
        public M_Product proMod = null;
        public M_Node nodeMod = null;
        public int NodeID = 0, ModelID = 0;
        //新建时为Guid,而后则为商品的ID
        public string ProGuid = "";
        public string bindList = "";
        public string groupList = "";//会员组信息,给予前端JS调用
        public string modelHtml="";
        // 会员价格列表
        public DataTable gpriceDT = null;
        public DataTable fareDT = null;
        public VM_Product(M_Product proMod, HttpRequestBase Request)
        {
            groupList = JsonConvert.SerializeObject(DBCenter.SelWithField("ZL_Group", "GroupID,GroupName"));
            gpriceDT = gpBll.GetGroupList();
            gpriceDT.Columns.Add(new DataColumn("gprice", typeof(string)));
            fareDT = fareBll.Sel();
            this.proMod = proMod;
            if (proMod.ID > 0)
            {
                this.NodeID = proMod.Nodeid;
                this.ModelID = proMod.ModelID;
                this.ProGuid = proMod.ID.ToString();
                //会员组价
                if (proMod.UserType == 2 && proMod.UserPrice.Contains("[") && !proMod.UserPrice.Equals("[]"))
                {
                    DataTable upDT = JsonConvert.DeserializeObject<DataTable>(proMod.UserPrice);
                    foreach (DataRow dr in upDT.Rows)
                    {
                        DataRow[] drs = gpriceDT.Select("GroupID='" + dr["gid"] + "'");
                        if (drs.Length > 0)
                        {
                            drs[0]["gprice"] = DataConvert.CDouble(dr["price"]).ToString("F2");
                        }
                    }
                }
                //捆绑商品
                if (!string.IsNullOrEmpty(proMod.BindIDS))
                {
                    DataTable dt = proBll.SelByIDS(proMod.BindIDS, "id,Thumbnails,Proname,LinPrice");
                    bindList = JsonConvert.SerializeObject(dt);
                }
                if (!string.IsNullOrEmpty(proMod.TableName))
                {
                    DataTable valueDT = proBll.Getmodetable(proMod.TableName.ToString(), proMod.ItemID);
                    if (valueDT != null && valueDT.Rows.Count > 0)
                    {
                        modelHtml = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig() { ValueDT = valueDT });
                    }
                }
            }
            else
            {
                this.NodeID = DataConvert.CLng(Request.QueryString["NodeID"]);
                this.ModelID = DataConvert.CLng(Request.QueryString["ModelID"]);
                this.ProGuid = System.Guid.NewGuid().ToString();
                this.proMod.ProCode = B_Product.GetProCode();
                modelHtml = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig() { Source = ModelConfig.SType.Admin });
            }
            nodeMod = nodeBll.SelReturnModel(NodeID);
        }
    }
}