using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZoomLa.Controls
{
    public class C_TreeView
    {
        public DataTable DataSource = new DataTable();
        public string NodeName = "NodeName";
        public string NodeID = "NodeID";
        public string NodePid = "ParentID";
        public string Url = "";//点击访问的Url
        /// <summary>
        /// 是否加载css和js等资源,如一个场景需要用到多个时后面的为false
        /// </summary>
        public bool LoadRes = true;
        public string liAllTlp = "<a href='QuestList'>全部内容</a>";
        public string LiContentTlp = "<a href='QuestList?NodeID=@NodeID'>@NodeName</a>";
        public string SelectedNode = "";//选中节点
    }
}
