using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZoomLa.Common;

namespace ZoomLa.AppCode.Controls
{
    //模板页面列表
    public class C_TlpDP
    {
        //控件id与名称
        public string name = "";
        //模板预览URL,无则不显示预览图标
        public string preurl = "";
        //选中值
        public string seled = "";
        public DataTable dt = new DataTable();
        public C_TlpDP(string name, string seled, string preulr = "", DataTable dt = null)
        {
            this.name = name;
            this.preurl = preulr;
            this.seled = seled;
            if (dt == null)
            {
                dt = FileSystemObject.GetDTForTemplate();
            }
        }
    }
}
