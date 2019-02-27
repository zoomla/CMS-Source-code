using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLa.BLL.Content
{
    public class ContentHelper
    {
        B_Model modelBll = new B_Model();
        public string GetElite(string Elite)
        {
            if (DataConverter.CLng(Elite) > 0)
                return "推荐";
            else
                return "未推荐";
        }
        //根据模型ID获取图标
        public string GetPic(object modeid)
        {
            M_ModelInfo model = modelBll.GetModelById(DataConverter.CLng(modeid));
            if (model == null) { return ""; }
            if (!string.IsNullOrEmpty(model.ItemIcon))
            {
                return "<span class=\"" + model.ItemIcon + "\" />";
            }
            else
            {
                return "";
            }
        }
    }
}
