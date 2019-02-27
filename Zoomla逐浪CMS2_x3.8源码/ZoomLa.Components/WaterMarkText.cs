namespace ZoomLa.Components
{
    using System;

    [Serializable]
    public class WaterMarkText
    {
        private int m_FoneBorder;
        private string m_FoneBorderColor;
        private string m_FoneColor;
        private int m_FoneSize;
        private string m_FoneStyle;
        private string m_FoneType;
        private bool m_IsNull;
        private string m_Text;
        private string m_WaterMarkPosition;
        private int m_WaterMarkPositionX;
        private int m_WaterMarkPositionY;

        public WaterMarkText()
        {
        }

        public WaterMarkText(bool value)
        {
            this.m_IsNull = value;
        }

        public int FoneBorder
        {
            get
            {
                return this.m_FoneBorder;
            }
            set
            {
                this.m_FoneBorder = value;
            }
        }

        public string FoneBorderColor
        {
            get
            {
                return this.m_FoneBorderColor;
            }
            set
            {
                this.m_FoneBorderColor = value;
            }
        }

        public string FoneColor
        {
            get
            {
                return this.m_FoneColor;
            }
            set
            {
                this.m_FoneColor = value;
            }
        }

        public int FoneSize
        {
            get
            {
                return this.m_FoneSize;
            }
            set
            {
                this.m_FoneSize = value;
            }
        }

        public string FoneStyle
        {
            get
            {
                return this.m_FoneStyle;
            }
            set
            {
                this.m_FoneStyle = value;
            }
        }

        public string FoneType
        {
            get
            {
                return this.m_FoneType;
            }
            set
            {
                this.m_FoneType = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
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