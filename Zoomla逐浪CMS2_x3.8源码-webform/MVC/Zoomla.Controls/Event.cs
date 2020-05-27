using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using System.Web.UI.WebControls;
using System.Web.UI;


namespace Controls
{
    /// <summary>
    /// ExGridView类的事件部分
    /// </summary>
    public partial class ExGridView
    {
        private static readonly object rowDataBoundDataRowEventKey = new object();
        /// <summary>
        /// RowDataBound事件中的DataControlRowType.DataRow部分
        /// </summary>
        [Category("扩展")]
        public event RowDataBoundDataRowHandler RowDataBoundDataRow
        {
            add { Events.AddHandler(rowDataBoundDataRowEventKey, value); }
            remove { Events.RemoveHandler(rowDataBoundDataRowEventKey, value); }
        }
        /// <summary>
        /// 触发RowDataBoundDataRow事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRowDataBoundDataRow(GridViewRowEventArgs e)
        {
            RowDataBoundDataRowHandler handler = Events[rowDataBoundDataRowEventKey] as RowDataBoundDataRowHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }


        private static readonly object rowDataBoundCellEventKey = new object();



        private static readonly object renderBeginEventKey = new object();
        /// <summary>
        /// RenderBegin
        /// </summary>
        [Category("扩展")]
        public event RenderBeginHandler RenderBegin
        {
            add { Events.AddHandler(renderBeginEventKey, value); }
            remove { Events.RemoveHandler(renderBeginEventKey, value); }
        }
        /// <summary>
        /// 触发RenderBegin事件
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void OnRenderBegin(HtmlTextWriter writer)
        {
            RenderBeginHandler handler = Events[renderBeginEventKey] as RenderBeginHandler;

            if (handler != null)
            {
                handler(this, writer);
            }
        }


        private static readonly object renderEndEventKey = new object();
        /// <summary>
        /// RenderEnd
        /// </summary>
        [Category("扩展")]
        public event RenderEndHandler RenderEnd
        {
            add { Events.AddHandler(renderEndEventKey, value); }
            remove { Events.RemoveHandler(renderEndEventKey, value); }
        }
        /// <summary>
        /// 触发RenderEnd事件
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void OnRenderEnd(HtmlTextWriter writer)
        {
            RenderEndHandler handler = Events[renderEndEventKey] as RenderEndHandler;

            if (handler != null)
            {
                handler(this, writer);
            }
        }


        private static readonly object initPagerEventKey = new object();
        /// <summary>
        /// InitPager
        /// </summary>
        [Category("扩展")]
        public event InitPagerHandler InitPager
        {
            add { Events.AddHandler(initPagerEventKey, value); }
            remove { Events.RemoveHandler(initPagerEventKey, value); }
        }
        /// <summary>
        /// 触发InitPager事件
        /// </summary>
        /// <param name="row">一个 System.Web.UI.WebControls.GridViewRow，表示要初始化的页导航行</param>
        /// <param name="columnSpan">页导航行应跨越的列数</param>
        /// <param name="pagedDataSource">一个 System.Web.UI.WebControls.PagedDataSource，表示数据源</param>
        protected virtual void OnInitPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            InitPagerHandler handler = Events[initPagerEventKey] as InitPagerHandler;

            if (handler != null)
            {
                handler(this, row, columnSpan, pagedDataSource);
            }
        }
    }
}
