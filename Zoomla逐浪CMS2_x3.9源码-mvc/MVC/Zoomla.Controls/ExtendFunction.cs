using System;
using System.Collections.Generic;
using System.Text;

namespace Controls.ExGridViewFunction
{
    /// <summary>
    /// 扩展功能类，抽象类
    /// </summary>
    public abstract class ExtendFunction
    {
        /// <summary>
        /// ExGridView对象变量
        /// </summary>
        protected ExGridView _sgv;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExtendFunction()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sgv">ExGridView对象</param>
        public ExtendFunction(ExGridView sgv)
        {
            this._sgv = sgv;
        }

        /// <summary>
        /// ExGridView对象
        /// </summary>
        public ExGridView ExGridView
        {
            get { return this._sgv; }
            set { this._sgv = value; }
        }

        /// <summary>
        /// 实现扩展功能
        /// </summary>
        public void Complete()
        {
            if (this._sgv == null)
            {
                throw new ArgumentNullException("ExGridView", "扩展功能时未设置ExGridView对象");
            }
            else
            {
                Execute();
            }
        }

        /// <summary>
        /// 扩展功能的具体实现
        /// </summary>
        protected abstract void Execute();
    }
}
