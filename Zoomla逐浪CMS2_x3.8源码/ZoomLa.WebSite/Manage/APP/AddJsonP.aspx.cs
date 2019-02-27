using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.CreateJS;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

public partial class Manage_APP_AddJsonP : CustomerPageAction
{
    //该页面有注入风险,仅给超管开放
    B_API_JsonP apiBll = new B_API_JsonP();
    B_Label labelBll = new B_Label();
    B_Admin badmin = new B_Admin();
    PageSetting pageConfig = new PageSetting();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    private int LabelID { get { return DataConvert.CLng(Request.QueryString["LabelID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.IsSuperManage();
        if (!IsPostBack)
        {
            //标签列表调用跳转
            if (LabelID > 0)
            {
                LabelInvoke();
            }
            else if (Mid > 0)//修改
            {
                M_API_JsonP apiMod = apiBll.SelReturnModel(Mid);
                Alias_T.Text = apiMod.Alias;
                T1_T.Text = apiMod.T1;
                T2_T.Text = apiMod.T2;
                MyPK_T.Text = apiMod.MyPK;
                Fields_T.Text = apiMod.Fields;
                ONStr_T.Text = apiMod.ONStr;
                WhereStr_T.Text = apiMod.WhereStr;
                OrderStr_T.Text = apiMod.OrderStr;
                Params_Hid.Value = apiMod.Params;
                MyState_Chk.Checked = apiMod.MyState == 1;
                ReMark_T.Text = apiMod.Remark;
            }
            if (!string.IsNullOrEmpty(T1_T.Text))
            {
                pageConfig.t1 = T1_T.Text;
                pageConfig.t2 = T2_T.Text;
                pageConfig.pk = MyPK_T.Text;
                pageConfig.fields = Fields_T.Text;
                pageConfig.on = ONStr_T.Text;
                pageConfig.where = WhereStr_T.Text;
                pageConfig.order = OrderStr_T.Text;
                try
                {
                    DataTable dt = DBCenter.SelPage(pageConfig);
                    //EGV.DataSource = dt;
                    //EGV.DataBind();
                }
                catch (Exception ex) { Exception_L.Text = ex.Message; }
                SQL_T.Text = pageConfig.sql;
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='JsonPManage.aspx'>移动接口</a></li><li>调用管理</li>");
        }
    }
    private void LabelInvoke() 
    {
        //1,获取主表与次表的表名,然后替换为 A. B.
        //{table1}.dbo.ZL_CommonModel left join {table1}.dbo.ZL_C_Article on {table1}.dbo.ZL_CommonModel.ArticleID={table1}.dbo.ZL_C_Article.aabbb
        M_Label labelMod = labelBll.GetLabelXML(LabelID);
        if (string.IsNullOrEmpty(labelMod.ProceParam)) { return; }
        M_SubLabel subMod = JsonConvert.DeserializeObject<M_SubLabel>(labelMod.ProceParam);
        Alias_T.Text = labelMod.LableName;
        T1_T.Text = subMod.PureT1;
        T2_T.Text = subMod.PureT2;
        labelMod.LabelField = PureStr(labelMod.LabelField, subMod);
        labelMod.LabelWhere = PureStr(labelMod.LabelWhere, subMod);
        labelMod.LabelOrder = PureStr(labelMod.LabelOrder, subMod);
        if (!string.IsNullOrEmpty(subMod.OnField2))
        {
            //标签添加时,左边必定是主表,右边必定是次表
            ONStr_T.Text = "A." + subMod.OnField1 + "=B." + subMod.OnField2;
        }
        Fields_T.Text = labelMod.LabelField;
        WhereStr_T.Text = labelMod.LabelWhere;
        OrderStr_T.Text = labelMod.LabelOrder;
        ReMark_T.Text = "调用标签" + labelMod.LableName;
        MyPK_T.Text = "A." + SqlHelper.ExecuteTable("SELECT TOP 1 * FROM " + subMod.PureT1 + " WHERE 1=2").Columns[0].ColumnName;
        //---------------
        if (labelMod.ParamList.Count > 0)
        {
            Params_Hid.Value = JsonConvert.SerializeObject(labelMod.ParamList);
        }
    }
    private string PureStr(string label, M_SubLabel subMod)
    {
        label = label.Replace("{table1}.dbo.", "").Replace("{table2}.dbo.", "");
        return label.Replace(subMod.PureT1, "A").Replace(subMod.PureT2, "B");
    }
    private void MyBind()
    {
        //int itemcount = 0;
        //DataTable dt = PageHelper.SelPage(100, 1, out itemcount, MyPK_T.Text, Fields_T.Text, T1_T.Text, T2_T.Text, WhereStr_T.Text, ONStr_T.Text, OrderStr_T.Text, null);
        //EGV.DataSource = dt;
        //EGV.DataBind();
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_API_JsonP apiMod = new M_API_JsonP();
        if (Mid > 0)
        {
            apiMod = apiBll.SelReturnModel(Mid);
        }
        apiMod.Alias = Alias_T.Text;
        apiMod.T1 = T1_T.Text;
        apiMod.T2 = T2_T.Text;
        apiMod.MyPK = MyPK_T.Text;
        apiMod.Fields = Fields_T.Text;
        apiMod.ONStr = ONStr_T.Text;
        apiMod.WhereStr = WhereStr_T.Text;
        apiMod.OrderStr = OrderStr_T.Text;
        apiMod.Remark = ReMark_T.Text;
        if(!string.IsNullOrEmpty(Request.Form["param"]))
        {
            string[] paramarr=Request.Form["param"].Split(new char[]{','});//开放参数
            string[] typearr = Request.Form["paramtype"].Split(new char[]{','});//参数类型
            string[] defarr = Request.Form["defvalue"].Split(new char[] { ',' });//默认参数值
            string paramvalue="";
            for (int i = 0; i < paramarr.Length; i++)
            {
                paramvalue += "{\"name\":\"" + paramarr[i] + "\",\"type\":\"" + typearr[i] + "\",\"defval\":\"" + defarr[i] + "\"}" + ",";
            }
            apiMod.Params ="["+paramvalue.Trim(',')+"]";
        }
        apiMod.MyState = MyState_Chk.Checked ? 1 : 0;
        if (Mid > 0)
        {
            apiBll.UpdateByID(apiMod);
        }
        else
        {
            apiMod.AdminID = badmin.GetAdminLogin().AdminId;
            apiBll.Insert(apiMod);
        }
        function.WriteSuccessMsg("操作成功","JsonPManage.aspx");
    }
    protected void Test_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //EGV.PageIndex = e.NewPageIndex;
        //MyBind();
    }
}