namespace ZoomLaCMS.BU
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Exam;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Exam;
    using ZoomLa.SQLDAL;
    public partial class VersionList : System.Web.UI.Page
    {
        B_Exam_Version verBll = new B_Exam_Version();
        B_User buser = new B_User();
        B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string result = "";
                switch (Request.Form["action"])
                {
                    case "getlist":
                        {
                            int pid = DataConvert.CLng(Request.Form["pid"]);
                            DataTable dt = GetChildVersion(pid);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string ids = dt.Rows[i]["Knows"].ToString();
                                string names = knowBll.GetNamesByIDS(ids).Replace(",", " | ").Trim('|');
                                dt.Rows[i]["Knows"] = names;
                            }
                            result = JsonConvert.SerializeObject(dt);
                        }
                        break;
                    case "del":
                        {
                            int id = DataConvert.CLng(Request.Form["id"]);
                            if (id > 0)
                            {
                                verBll.Del(id);
                                result = "true";
                            }
                        }
                        break;
                    case "move":
                        {
                            string[] oids = Request.Form["oid"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] nids = Request.Form["nid"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            M_Exam_Version overMod = verBll.SelReturnModel(DataConvert.CLng(oids[0]));
                            overMod.OrderID = DataConvert.CLng(nids[1]);
                            M_Exam_Version nverMod = verBll.SelReturnModel(DataConvert.CLng(nids[0]));
                            nverMod.OrderID = DataConvert.CLng(oids[1]);
                            verBll.UpdateByID(overMod);
                            verBll.UpdateByID(nverMod);
                            result = "true";
                        }
                        break;
                }
                Response.Clear();
                Response.Write(result);
                Response.End();
            }
            if (!IsPostBack)
            {
            }
        }
        public DataTable GetChildVersion(int pid, int uid = 0)
        {
            string TbName = "ZL_Exam_Version";
            string fields = "A.*,B.GradeName,(SELECT COUNT(*) FROM " + TbName + " WHERE Pid=A.ID) AS Child,(SELECT C_ClassName FROM ZL_Exam_Class WHERE A.NodeID=C_id) AS NodeName ,(SELECT Pid FROM ZL_Exam_Version WHERE ID = A.Pid) AS PPid";
            string where = "A.Pid = " + pid;
            if (uid > 0) { where = "A.UserID=" + uid; }
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Grade", "A.Grade=B.GradeID", where, "A.OrderID ASC");
        }
    }
}