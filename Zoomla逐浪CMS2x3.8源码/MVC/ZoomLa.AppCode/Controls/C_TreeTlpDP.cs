using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZoomLa.AppCode.Controls
{
    public class C_TreeTlpDP
    {
        public string name = "TreeTlp";
        public string seled = "";
        public DataTable dt = new DataTable();
        //未选中时显示的文字
        public string emptyText = "请选择";
        //dt中主键字段名(值名称),parentid字段名,显示名称
        public string F_ID = "NodeID";
        public string F_Pid = "ParentID";
        public string F_Name = "NodeName";
    }
}
