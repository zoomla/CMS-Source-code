namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 缩略图配置
    /// </summary>
    [Serializable]
    public class ThumbsConfig
    {
        private string m_AddBackColor;
        private bool m_IsNull;
        private int m_ThumbsHeight;
        private int m_ThumbsMode;
        private int m_ThumbsWidth;

        public ThumbsConfig()
        {
        }

        public ThumbsConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public string AddBackColor
        {
            get
            {
                return this.m_AddBackColor;
            }
            set
            {
                this.m_AddBackColor = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public int ThumbsHeight
        {
            get
            {
                return this.m_ThumbsHeight;
            }
            set
            {
                this.m_ThumbsHeight = value;
            }
        }

        public int ThumbsMode
        {
            get
            {
                return this.m_ThumbsMode;
            }
            set
            {
                this.m_ThumbsMode = value;
            }
        }

        public int ThumbsWidth
        {
            get
            {
                return this.m_ThumbsWidth;
            }
            set
            {
                this.m_ThumbsWidth = value;
            }
        }
    }
}