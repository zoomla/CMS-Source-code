namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 水印设置
    /// </summary>
    [Serializable]
    public class WaterMarkConfig
    {
        private bool m_IsNull;
        private int m_WaterMarkType;
        private WaterMarkImage waterMarkImageInfo;
        private WaterMarkText waterMarkTextInfo;

        public WaterMarkConfig()
        {
            if (this.waterMarkTextInfo == null)
            {
                this.waterMarkTextInfo = new WaterMarkText();
            }
            if (this.waterMarkImageInfo == null)
            {
                this.waterMarkImageInfo = new WaterMarkImage();
            }
        }

        public WaterMarkConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public WaterMarkImage WaterMarkImageInfo
        {
            get
            {
                return this.waterMarkImageInfo;
            }
            set
            {
                this.waterMarkImageInfo = value;
            }
        }

        public WaterMarkText WaterMarkTextInfo
        {
            get
            {
                return this.waterMarkTextInfo;
            }
            set
            {
                this.waterMarkTextInfo = value;
            }
        }

        public int WaterMarkType
        {
            get
            {
                return this.m_WaterMarkType;
            }
            set
            {
                this.m_WaterMarkType = value;
            }
        }
    }
}