using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZoomLa.BLL.MIS
{
    public class B_OA_ShowField
    {
        public FieldConfig config = new FieldConfig();
        B_ShowField showBll = new B_ShowField();
        B_ModelField fieldBll = new B_ModelField();
        /// <summary>
        /// 解析模型字段表
        /// </summary>
        /// <param name="dt">模型字段列表</param>
        public string ShowStyleField(DataTable fielddt,int nodeid)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow dr in fielddt.Rows)
            {
                FieldModel model = FieldModel.LoadFromDR(dr, nodeid);
                model.config = config;
                builder.Append(showBll.ShowStyleField(model));
            }
            return builder.ToString();
        }
        public string GetUpdateAllHtml(int ModelID, int NodeID, DataTable dt1)
        {
            StringBuilder builder = new StringBuilder();
            DataTable dt = fieldBll.GetModelFieldListall(ModelID);
            if (dt1.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FieldModel model = FieldModel.LoadFromDR(dt.Rows[i], NodeID);
                    model.dr = dr;
                    model.config = config;
                    builder.Append(showBll.ShowStyleField(model));
                }
            }
            return builder.ToString();
        }
        ////解析字段
        //public string ShowStyleField(FieldModel model)
        //{
        //    //string Alias = model.Alias,Name = model.Name; bool IsNotNull = model.IsNotNull;
        //    //string Content = model.Content, Description = model.Description;
        //    //int ModelID = model.ModelID, NodeID = model.NodeID; bool ischain = false;
        //    //DataRow dr = model.dr;
        //    string html = showBll.ShowStyleField(model);
        //    return html;
        //}   
    }
}
