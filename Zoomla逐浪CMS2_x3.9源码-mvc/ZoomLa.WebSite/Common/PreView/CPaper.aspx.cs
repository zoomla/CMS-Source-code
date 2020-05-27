namespace ZoomLaCMS.Common.PreView
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    public partial class CPaper : System.Web.UI.Page
    {
        /*
     * 统一为使用int传值,为0表示忽略该筛选条件==全部
     */
        //   /Common/PreView/CPaper.aspx?Grade=年级&Subject=学科&KeyWord=知识点&Degree=难度
        //年级:p_Views
        private int Grade { get { return DataConverter.CLng(Request.QueryString["Grade"]); } }
        //学科,p_Class
        private int Subject { get { return DataConverter.CLng(Request.QueryString["Subject"]); } }
        //知识点,p_Knowledge
        private int KeyWord { get { return DataConverter.CLng(Request.QueryString["KeyWord"]); } }
        //难度,p_Difficulty
        private int Degree { get { return DataConverter.CLng(Request.QueryString["Degree"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            //http://win27:94/Class_3/NodePage.aspx?Grade=%u4E09%u5E74%u7EA7&Subject=%u6570%u5B66&Keyword=%u6613%u9519%u6DF7%u6DC6%u9898&Degree=%u6781%u96BE
            //根据条件,抽出试题,并生成一张临时试卷,用于给用户测试
            string sql = "SELECT TOP 10 p_id FROM ZL_Exam_Sys_Questions WHERE 1=1 ";
            if (Grade > 0)
            {
                sql += " AND p_views=" + Grade;//年级
            }
            if (Subject > 0)
            {
                sql += " AND p_class=" + Subject;//科目
            }
            if (KeyWord > 0)
            {
                sql += " AND p_Knowledge=" + KeyWord;//知识点
            }
            if (Degree > 0)
            {
                sql += " AND p_Difficulty=" + Degree;//难度
            }

            DataTable questdt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string qids = "";
            foreach (DataRow dr in questdt.Rows)
            {
                qids += dr["p_id"] + ",";
            }
            qids = qids.TrimEnd(',');
            if (string.IsNullOrEmpty(qids)) { function.WriteErrMsg("没有找到相关的题目类型"); }
            Response.Redirect("/User/Questions/ExamDetail.aspx?Qids=" + qids);
        }
    }
}