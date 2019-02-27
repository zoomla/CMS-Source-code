namespace ZoomLa.Components
{
    using System;

    [Serializable]
    public class IPLockConfig
    {
        private string m_AdminLockIPBlack;
        private string m_AdminLockIPType;
        private string m_AdminLockIPWhite;
        private bool m_IsNull;
        private string m_LockIPBlack;
        private string m_LockIPType;
        private string m_LockIPWhite;

        public IPLockConfig()
        {
        }

        public IPLockConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public string AdminLockIPBlack
        {
            get
            {
                return this.m_AdminLockIPBlack;
            }
            set
            {
                this.m_AdminLockIPBlack = value;
            }
        }

        public string AdminLockIPType
        {
            get
            {
                return this.m_AdminLockIPType;
            }
            set
            {
                this.m_AdminLockIPType = value;
            }
        }

        public string AdminLockIPWhite
        {
            get
            {
                return this.m_AdminLockIPWhite;
            }
            set
            {
                this.m_AdminLockIPWhite = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public string LockIPBlack
        {
            get
            {
                return this.m_LockIPBlack;
            }
            set
            {
                this.m_LockIPBlack = value;
            }
        }

        public string LockIPType
        {
            get
            {
                return this.m_LockIPType;
            }
            set
            {
                this.m_LockIPType = value;
            }
        }

        public string LockIPWhite
        {
            get
            {
                return this.m_LockIPWhite;
            }
            set
            {
                this.m_LockIPWhite = value;
            }
        }
    }
}

