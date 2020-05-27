using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Auth;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User
{
    public partial class RoleAuthList : System.Web.UI.Page
    {
        B_Role roleBll = new B_Role();
        B_ARoleAuth authBll = new B_ARoleAuth();
        //以其确定显示顺序等
        //edu|教育模块,extend|扩展功能,system|系统配置,oa|企业办公,mobile|移动管理,bar|贴吧问答,site|站群配置,
        string[] ownerlist = "model|模型&节点,content|内容,shop|商城,store|店铺,user|用户,page|黄页,label|模板&标签,crm|CRM,pub|互动,other|其它".Split(',');
        //模型节点oa    
        //string[] auth_model = "ModelManage:内容模型管理,ModelEdit:内容模型编辑,ShopModelManage:商品模型管理,ShopModelEdit:商品模型编辑,PageModelManage:黄页模型管理,AddPageModel:添加黄页模型,NodeManage:节点管理,NodeEdit:节点编辑,SpecManage:专题管理".Split(',');
        //string[] auth_label = "TemplateManage:模板管理,TemplateEdit:模板编辑,CssManage:风格管理,CssEdit:风格编辑,LabelManage:标签管理,LabelEdit:标签编辑,LabelImport:标签导入,LabelExport:标签导出".Split(',');
        //string[] auth_user = "AdminManage:管理员管理,AdminEdit:管理员编辑,RoleMange:角色管理,RoleEdit:角色设置,UserManage:会员管理,UserGroup:会员组管理,UserModel:会员模型管理,UserModelField:会员模型字段管理,UserRoleMange:会员角色管理,UserRoleEdit:会员角色设置,MessManage:短消息发送,EmailManage:邮件列表".Split(',');
        //string[] auth_shop = "ProductManage:商品管理,StockManage:库存管理,AddStock:添加库存记录,CartManage:购物车,OrderList:订单处理,BankRollList:资金明细,SaleList:销售明细,PayList:支付明细,InvoiceList:开发票明细,TotalSale:总体销售统计,ProductSale:商品销售排名,CategotySale:商品类别销售排名,UserOrders:会员订单排名,UserExpenditure:会员购物排名,DeliverType:商城配置".Split(',');
        //string[] auth_page = "PageModelManage:申请样式管理,PageModel:添加申请样式,PageManage:黄页审核与管理,PageAudit:黄页用户节点管理,PageContent:黄页内容管理,PageStyle:黄页样式管理,AddPageStyle:添加黄页样式".Split(',');
        //string[] auth_store = "ApplyStoreStyle:店铺申请设置,ApplyStoreStyleAdd:添加申请模型,StoreManage:商家店铺管理,StoreExamine:商家店铺审核,StoreStyleManage:商家店铺模板管理,StoreStyleAdd:添加店铺模板,StoreProductManage:商家商品管理,StoreinfoManage:店铺配置".Split(',');
        //string[] auth_other = "ADManage:广告管理,StatInfoListReport:访问统计,CorrectManage:纠错管理,LogManage:日志管理,SourceManage:其他管理,SurveyManage:问卷调查和投票管理,GuestCateMana:留言管理,DownServerManage:文件管理,DictionaryManage:数据字典管理,CustomerService:客服通（Beta）,CustomerService:其他功能,GameManage:游戏管理,DevCenter:开发中心".Split(',');
        //private List<M_Auth> authList = new List<M_Auth>();
        private DataTable AuthDT { get { return ViewState["AuthDT"] as DataTable; } set { ViewState["AuthDT"] = value; } }
        private int RoleID { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (RoleID < 1) { function.WriteErrMsg("请输入角色ID"); }
                MyBind();
            }
        }
        private void MyBind()
        {
            //后期改为XML存储或Json存储
            M_RoleInfo roleMod = roleBll.SelReturnModel(RoleID);
            RoleName_L.Text = roleMod.RoleName;
            string json = SafeSC.ReadFileStr("/Config/AuthList.config");
            AuthDT = JsonConvert.DeserializeObject<DataTable>(json);
            //AuthDT = SqlHelper.ExecuteTable(CommandType.Text, "SELECT layer,[name],[text],owner,[desc] FROM ZL_AuthList");
            DataTable tabDT = GetAuthModel();
            TabRPT.DataSource = tabDT;
            TabRPT.DataBind();
            ownerRPT.DataSource = tabDT;
            ownerRPT.DataBind();
            M_ARoleAuth authMod = authBll.SelModelByRid(RoleID);
            if (authMod != null)
            {
                function.Script(this, "SetChkVal('model','" + authMod.model + "');");
                function.Script(this, "SetChkVal('content','" + authMod.content + "');");
                function.Script(this, "SetChkVal('crm','" + authMod.crm + "');");
                function.Script(this, "SetChkVal('label','" + authMod.label + "');");
                function.Script(this, "SetChkVal('shop','" + authMod.shop + "');");
                function.Script(this, "SetChkVal('store','" + authMod.store + "');");
                function.Script(this, "SetChkVal('page','" + authMod.page + "');");
                function.Script(this, "SetChkVal('user','" + authMod.user + "');");
                function.Script(this, "SetChkVal('other','" + authMod.other + "');");
                function.Script(this, "SetChkVal('pub','" + authMod.pub + "');");
            }
            //AuthDT=AuthDT.DefaultView.ToTable(false, "layer,name,text,owner,desc".Split(','));
            //string json = JsonConvert.SerializeObject(AuthDT,Formatting.Indented);
            //SafeSC.WriteFile("/test/AuthList.config", json);
        }
        private DataTable GetAuthModel()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("name", typeof(string)));
            dt.Columns.Add(new DataColumn("text", typeof(string)));
            for (int i = 0; i < ownerlist.Length; i++)
            {
                string name = ownerlist[i].Split('|')[0];
                string text = ownerlist[i].Split('|')[1];
                DataRow dr = dt.NewRow();
                dr["name"] = name; dr["text"] = text;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //绑定显示,如果无则不显示
        protected void ownerRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //显示具体所拥有的权限
                DataRowView drv = e.Item.DataItem as DataRowView;
                Repeater RPT = e.Item.FindControl("AuthRPT") as Repeater;
                AuthDT.DefaultView.RowFilter = "";
                AuthDT.DefaultView.RowFilter = "owner='" + drv["name"] + "'";
                RPT.DataSource = AuthDT.DefaultView.ToTable();
                RPT.DataBind();
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_ARoleAuth model = new M_ARoleAuth();
            model.Rid = RoleID;
            model.model = Request.Form["model"];
            model.content = Request.Form["content"];
            model.crm = Request.Form["crm"];
            model.label = Request.Form["label"];
            model.shop = Request.Form["shop"];
            model.store = Request.Form["store"];
            model.page = Request.Form["page"];
            model.user = Request.Form["user"];
            model.other = Request.Form["other"];
            model.pub = Request.Form["pub"];
            authBll.Insert(model);
            function.WriteSuccessMsg("操作成功");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            AuthDT = null;
        }
    }
}