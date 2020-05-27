using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

namespace Controls
{
    /// <summary>
    /// ExGridView类的委托部分
    /// </summary>
    public partial class ExGridView
    {
        /// <summary>
        /// RowDataBoundDataRow事件委托
        /// </summary>
        /// <remarks>
        /// RowDataBound事件中的DataControlRowType.DataRow部分
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void RowDataBoundDataRowHandler(object sender, GridViewRowEventArgs e);

 

        /// <summary>
        /// RenderBegin事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="writer"></param>
        public delegate void RenderBeginHandler(object sender, HtmlTextWriter writer);

        /// <summary>
        /// RenderEnd事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="writer"></param>
        public delegate void RenderEndHandler(object sender, HtmlTextWriter writer);

        /// <summary>
        /// InitPagerHandler事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="row">一个 System.Web.UI.WebControls.GridViewRow，表示要初始化的页导航行</param>
        /// <param name="columnSpan">页导航行应跨越的列数</param>
        /// <param name="pagedDataSource">一个 System.Web.UI.WebControls.PagedDataSource，表示数据源</param>
        public delegate void InitPagerHandler(object sender, GridViewRow row, int columnSpan, PagedDataSource pagedDataSource);
    }
}
