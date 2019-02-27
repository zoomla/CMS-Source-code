using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

public partial class User_Exam_QuestRPT : System.Web.UI.Page
{
    B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    B_Exam_Class bqc = new B_Exam_Class();
    B_ExamPoint bep = new B_ExamPoint();
    B_Temp tempBll = new B_Temp();
    B_User buser = new B_User();
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
    //学科IDS
    private string NodeID { get { return Request["NodeID"] ?? ""; } }
    //教材版本
    private int Version { get { return DataConverter.CLng(Request["Version"]); } }
    private string Diffcult { get { return (Request["Diffcult"] ?? ""); } }
    private int p_Type { get { return DataConverter.CLng(Request["type"]); } }
    //年级
    private int Grade { get { return DataConverter.CLng(Request["grade"]); } }
    //知识点|关键词
    private string KeyWord { get { return (Request["KeyWord"] ?? "").Trim(','); } }
    private int psize = 10;
    public string CartQids { get { return ViewState["CartQids"] as string; } set { ViewState["CartQids"] = value; } }
    //True未登录
    public bool IsNotLogin { get { return Convert.ToBoolean(ViewState["IsNotLogin"]); } set { ViewState["IsNotLogin"] = value; } }
    public DataTable KnowsDT = new DataTable();
    B_TempUser UserBll = new B_TempUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = UserBll.GetLogin();
        IsNotLogin = mu.UserID < 1;
        login_div.Visible = IsNotLogin;
        M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);
        CartQids = tempMod == null ? "" : tempMod.Str1;
        int count = 0;
        DataTable dt = SelPage(out count);
        if (dt.Rows.Count < 1)
        {
            empty_div.Visible = true;
        }
        else
        {
            string hrefTlp = "<a href='javascript:;' onclick='LoadByAjax(\"@query\",@page);' title=''>@text</a>";
            RPT.DataSource = dt;
            RPT.DataBind();
            BindKonwsDT(dt);
            Page_Lit.Text = PageCommon.CreatePageHtml(PageHelper.GetPageCount(count, psize), PageCommon.GetCPage(), 10, hrefTlp);
            count_sp.InnerText = count.ToString();
        }
       
    }
    public void BindKonwsDT(DataTable dt)
    {
        string knowsids = "";
        foreach (DataRow dr in dt.Rows)
        {
            knowsids += dr["TagKey"].ToString() + ",";
        }
        KnowsDT = knowBll.SelByIDS(StrHelper.PureIDSForDB(knowsids));
        RPT.DataBind();
    }
    public string GetTagKey()
    {
        if (KnowsDT != null && KnowsDT.Rows.Count > 0 && !string.IsNullOrEmpty(Eval("Tagkey").ToString()))
        {
            string knownames = "";
            DataRow[] drs = KnowsDT.Select("k_id IN (" + Eval("Tagkey").ToString().Trim(',') + ")");
            foreach (DataRow item in drs)
            {
                knownames += item["k_name"].ToString() + ",";
            }
            string names = knownames.Trim(',');
            return names;
        }
        return "";
    }
    private DataTable SelPage(out int count)
    {
        string where = "1=1 ";
        if (!string.IsNullOrEmpty(NodeID) && !NodeID.Equals("0"))
        {
            SafeSC.CheckIDSEx(NodeID);
            where += " AND p_class IN(" + NodeID + ")";
        }
        if (Diffcult.Contains("-"))
        {
            //基础(0.91-1.00)容易(0.81-0.90)中等(0.51-0.80)偏难(0.31-0.50)极难(0.01-0.30)
            double sdiff = DataConverter.CDouble(Diffcult.Split('-')[0]);
            double ediff = DataConverter.CDouble(Diffcult.Split('-')[1]);
            //if (sdiff < 0) { sdiff = 0; }
            //if (ediff <= 0) { ediff = 1; }
            where += " AND (p_Difficulty>=" + sdiff + " AND p_Difficulty<=" + ediff + ")";
        }
        if (Grade > 0)
        {
            where += " AND p_Views=" + Grade;
        }
        if (p_Type > 0)//除非选择了大题,才允许输出大题
        {
            where += " AND p_Type=" + p_Type;
        }
        else { where += " AND p_type!=10"; }
        List<SqlParameter> splist = new List<SqlParameter>();
        if (!string.IsNullOrEmpty(KeyWord))
        {
            string[] keys = KeyWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string keyWhere = "";
            for (int i = 0; i < keys.Length; i++)
            {
                keyWhere += " OR ','+TagKey+',' LIKE @key" + i;
                splist.Add(new SqlParameter("key" + i, "%," + keys[i] + ",%"));
            }
            if (!string.IsNullOrEmpty(keyWhere))
            {
                where += " AND (" + keyWhere.Substring(3, keyWhere.Length - 3) + ") ";
            }
        }
        if (Version > 0)
        {
            where += " AND Version=" + Version;
        }
        //return PageHelper.SelPage(psize, PageCommon.GetCPage(), out count, "p_id", "*", "ZL_Exam_Sys_Questions", where, "ORDER BY p_id DESC", sp);
        PageSetting config = new PageSetting()
        {
            psize = psize,
            cpage = PageCommon.GetCPage(),
            pk = "p_id",
            fields = "A.*,B.C_ClassName",
            t1 = "ZL_Exam_Sys_Questions",
            t2 = "ZL_Exam_Class",
            on = "A.p_class=B.c_id",
            where = where,
            order = "p_id DESC",
            sp = splist.ToArray()
        };
        DataTable dt = DBCenter.SelPage(config);
        count = config.itemCount;
        return dt;
    }
    public bool GetIsContain()
    {
        return CartQids.Contains("," + Eval("p_id") + ",");
    }
    //批量删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            questBll.DelByIDS(ids);
        }
    }
    public string GetContent()
    {
        if (Convert.ToInt32(Eval("p_type")) == 10)
        {
            string result = Eval("LargeContent").ToString();
            string json = Eval("QInfo", "");
            if (string.IsNullOrEmpty(json)) { return "未选择小题"; }
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["p_Content"].ToString() + "<br />";
                string tempstr = "";
                result+= questBll.GetSubmit(DataConverter.CLng(dr["p_id"]), DataConverter.CLng(dr["p_type"]),ref tempstr);
            }
            return result;
        }
        else { return Eval("p_content", ""); }
    }
}