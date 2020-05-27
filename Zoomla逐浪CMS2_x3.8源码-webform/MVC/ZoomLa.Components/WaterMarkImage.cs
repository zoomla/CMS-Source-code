namespace ZoomLa.Components
{
    using System;

    [Serializable]
    public class WaterMarkImage
    {
        private string m_ImagePath;
        private bool m_IsNull;
        private string m_WaterMarkPosition;
        private int m_WaterMarkPositionX;
        private int m_WaterMarkPositionY;

        public WaterMarkImage()
        {
        }

        public WaterMarkImage(bool value)
        {
            this.m_IsNull = value;
        }

        public string ImagePath
        {
            get
            {
                return this.m_ImagePath;
            }
            set
            {
                this.m_ImagePath = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public string WaterMarkPosition
        {
            get
            {
                return this.m_WaterMarkPosition;
            }
            set
            {
                this.m_WaterMarkPosition = value;
            }
        }

        public int WaterMarkPositionX
        {
            get
            {
                return this.m_WaterMarkPositionX;
            }
            set
            {
                this.m_WaterMarkPositionX = value;
            }
        }

        public int WaterMarkPositionY
        {
            get
            {
                return this.m_WaterMarkPositionY;
            }
            set
            {
                this.m_WaterMarkPositionY = value;
            }
        }
    }
}