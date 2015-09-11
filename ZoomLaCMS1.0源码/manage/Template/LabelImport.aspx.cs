namespace ZoomLa.WebSite.Manage.Template
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
using ZoomLa.Model;
    using System.Text;

    public partial class LabelImport : System.Web.UI.Page
    {
        private B_Label bll = new B_Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("LabelImport"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string filename = base.Request.PhysicalApplicationPath + @"\" + "App_Data" + @"\" + "LabelExport.xml";

            if (!FileSystemObject.IsExist(filename, FsoMethod.File))
                function.WriteErrMsg("数据文件：../App_Data/LabelExport.xml 不存在");
            try
            {
                DataSet ds = new DataSet();
                M_Label info = new M_Label();
                ds.ReadXml(filename);
                DataTable dt = ds.Tables["Table"];
                int count = dt.Rows.Count;
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    info.LabelID = 0;
                    info.LableName = dr["LabelName"].ToString();
                    info.LabelCate = dr["LabelCate"].ToString();
                    info.Desc = dr["LabelDesc"].ToString();
                    info.LableType = DataConverter.CLng(dr["LabelType"]);
                    info.Param = dr["LabelParam"].ToString();
                    info.LabelTable = dr["LabelTable"].ToString();
                    info.LabelField = dr["LabelField"].ToString();
                    info.LabelWhere = dr["LabelWhere"].ToString();
                    info.LabelOrder = dr["LabelOrder"].ToString();
                    info.LabelCount = dr["LabelCount"].ToString();
                    info.Content = dr["LabelContent"].ToString();
                    string str2 = ((i * 100) / count).ToString("F1");
                    str2 = str2 + "%";
                    if (this.bll.IsExist(info.LableName))
                        info.LableName = info.LableName + DataSecurity.RandomNum(4);
                    this.bll.AddLabel(info);
                    this.tp.Style["Width"] = str2;
                    this.tn.Text = str2;
                    this.tc.Text = i.ToString();
                    
                    i++;
                }
                this.tp.Style["Width"] = "100%";
                this.tn.Text = "100%";
                this.tc.Text = count.ToString();
                this.finallytd.Text = "导入完毕";
            }
            catch
            {
                function.WriteErrMsg("导入标签出现异常！");
            }

        }
    }
}