using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Common
{
    public class ChartCall
    {
        private int m_ChartID;
        private string m_ChartTitle;
        private string m_ChartType;
        private string m_ChartUnit;
        private int m_ChartWidth;
        private int m_ChartHeight;
        public string CharData { get; set; }


        public int ChartID
        {
            get { return this.m_ChartID; }
            set { this.m_ChartID = value; }
        }

        /// <summary>
        /// 图表标题
        /// </summary>
        public string ChartTitle
        {
            get { return this.m_ChartTitle; }
            set { this.m_ChartTitle = value; }
        }

        /// <summary>
        /// 图表类型
        /// </summary>
        public string ChartType
        {
            get { return this.m_ChartType; }
            set { this.m_ChartType = value; }
        }


        /// <summary>
        /// 图表单位
        /// </summary>
        public string ChartUnit
        {
            get { return this.m_ChartUnit; }
            set { this.m_ChartUnit = value; }
        }

        /// <summary>
        /// 图表宽
        /// </summary>
        public int ChartWidth
        {
            get { return this.m_ChartWidth; }
            set { this.m_ChartWidth = value; }
        }

        /// <summary>
        /// 图表高
        /// </summary>
        public int ChartHeight
        {
            get { return this.m_ChartHeight; }
            set { this.m_ChartHeight = value; }
        }

    }
}
