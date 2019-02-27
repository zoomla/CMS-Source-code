using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLa.Web
{
    /// <summary>
    /// 自定义数据绑定模板列, 为用户模型信息搜索列表使用
    /// </summary>
    public class CusTemplate : ITemplate
    {
        /// <summary>
        /// 绑定字段名
        /// </summary>
        private string proName;

        private string modelID;
        /// <summary>
        /// 构造方法
        /// </summary>     
        public CusTemplate()
        {
            this.proName = "";
        }
        /// <summary>
        /// 绑定字段名
        /// </summary>
        public string ProName
        {
            set { proName = value; }
            get { return proName; }
        }
        public string ModelID
        {
            set { modelID = value; }
            get { return modelID; }
        }
        void Li_DataBinding(object sender, EventArgs e)
        {
            LiteralControl hi = (LiteralControl)sender;
            GridViewRow container = (GridViewRow)hi.NamingContainer;
            //关键位置
            //使用DataBinder.Eval绑定数据
            //ProName,Template的属性.在创建Template实例时,为此属性赋值(数据源字段)
            hi.Text = "<a href=\"/User/Info/ShowModel.aspx?ModelID=" + ModelID + "&ID=" + DataBinder.Eval(container.DataItem, ProName).ToString() + "\"";
        }
        #region ITemplate 成员
        
        void ITemplate.InstantiateIn(Control container)
        {
            LiteralControl li = new LiteralControl();
            li.DataBinding += new EventHandler(Li_DataBinding);//创建数据绑定事件
            container.Controls.Add(li);
        }

        #endregion
    }
}
