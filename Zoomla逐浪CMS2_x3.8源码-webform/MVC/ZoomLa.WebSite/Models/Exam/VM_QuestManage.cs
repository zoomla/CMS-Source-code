using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Models.Exam
{
    //用于组卷页面
    public class VM_QuestManage
    {
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Class nodeBll = new B_Exam_Class();
        B_Exam_Version verBll = new B_Exam_Version();
        B_Temp tempBll = new B_Temp();
        B_TempUser tubll = new B_TempUser();
        /// <summary>
        /// 问题列表
        /// </summary>
        public PageSetting setting = new PageSetting();
        public M_UserInfo mu = new M_UserInfo();
        //------------前台信息
        public DataTable verDT = null;
        public DataTable gradeDT = null;
        public string QListCount = "";//各种试题的统计json
        public string QuestType_Lit = "";
        public VM_QuestManage(HttpRequestBase Request)
        {
            this.mu = tubll.GetLogin();
            M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);
            if (tempMod == null) { tempMod = new M_Temp(); }
            gradeDT = B_GradeOption.GetGradeList(6, 0);
            verDT = verBll.Sel();
            QListCount = questBll.GetCountByIDS(tempMod.Str1);
            //function.Script(this, "RenderQList(" + list + ");");
            QuestType_Lit = GetTreeStr(FillQuest(nodeBll.SelectQuesClasses()), 0, "quest");
        }
        //树形
        string hasChild_tlp = "<li data-pid=@pid data-id=@id><span class='fa fa-plus-circle treeicon'></span><a class='filter_class @type' data-val='@id' href='javascript:;'>@name</a><ul>@childs</ul></li>";
        string childs_tlp = "<li class='lastchild' data-pid=@pid data-id=@id style='@islast'><a class='filter_class @type' data-val='@id' href='javascript:;'>@name</a></li>";
        public string GetTreeStr(DataTable dt, int pid, string type = "")
        {
            string html = "";
            DataRow[] drs = dt.Select("Pid=" + pid);
            for (int i = 0; i < drs.Length; i++)
            {
                DataRow item = drs[i];
                if (dt.Select("Pid=" + DataConvert.CLng(item["ID"])).Length > 0)
                {
                    html += hasChild_tlp.Replace("@id", item["ID"].ToString()).Replace("@name", item["NodeName"].ToString()).Replace("@pid", item["Pid"].ToString()).Replace("@type", type)
                        .Replace("@childs", GetTreeStr(dt, DataConvert.CLng(item["ID"]), type));
                }
                else
                {
                    html += childs_tlp.Replace("@pid", item["Pid"].ToString()).Replace("@id", item["ID"].ToString()).Replace("@name", item["NodeName"].ToString()).Replace("@type", type).Replace("@islast", i == drs.Length - 1 ? "background-position:0 -1766px;" : "");
                }
            }

            return html;
        }
        //知识点
        public DataTable FillKnows(DataTable dt)
        {
            DataTable treedt = InitTreeTable();
            foreach (DataRow item in dt.Rows)
            {
                DataRow dr = treedt.NewRow();
                dr["ID"] = item["k_id"];
                dr["Pid"] = item["Pid"];
                dr["NodeName"] = item["k_name"];
                treedt.Rows.Add(dr);
            }
            return treedt;
        }
        public DataTable InitTreeTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Pid");
            dt.Columns.Add("NodeName");
            return dt;
        }
        //试题科目
        private DataTable FillQuest(List<M_Exam_Class> list)
        {
            DataTable dt = InitTreeTable();
            foreach (M_Exam_Class item in list)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.C_id;
                dr["Pid"] = item.C_Classid;
                dr["NodeName"] = item.C_ClassName;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}