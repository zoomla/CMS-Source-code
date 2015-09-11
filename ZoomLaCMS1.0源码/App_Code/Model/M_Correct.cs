namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// M_Correct 的摘要说明
    /// </summary>
    public class M_Correct
    {
        private int m_CorrectID;
        private string m_CorrectTitle;
        private string m_CorrectUrl;
        private int m_CorrectType;
        private string m_CorrectDetail;
        private string m_CorrectPer;
        private string m_CorrectEmail;
        private bool m_IsNull;

        public M_Correct()
        {            
        }
        public M_Correct(bool flag)
        {
            this.m_IsNull = flag;
        }
        public int CorrectID
        {
            get { return this.m_CorrectID; }
            set { this.m_CorrectID=value;}
        }
        public string CorrectTitle
        {
            get { return this.m_CorrectTitle; }
            set { this.m_CorrectTitle = value; }
        }
        public string CorrectUrl
        {
            get { return this.m_CorrectUrl; }
            set { this.m_CorrectUrl = value; }
        }
        public int CorrectType
        {
            get { return this.m_CorrectType; }
            set { this.m_CorrectType = value; }
        }
        public string CorrectDetail
        {
            get { return this.m_CorrectDetail; }
            set { this.m_CorrectDetail = value; }
        }
        public string CorrectPer
        {
            get { return this.m_CorrectPer; }
            set { this.m_CorrectPer = value; }
        }
        public string CorrectEmail
        {
            get { return this.m_CorrectEmail; }
            set { this.m_CorrectEmail = value; }
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}