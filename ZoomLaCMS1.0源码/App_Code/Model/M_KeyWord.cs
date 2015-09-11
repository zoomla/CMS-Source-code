namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// M_KeyWord 的摘要说明
    /// </summary>
    public class M_KeyWord
    {
        private int m_KeyWordID;//关键字ID
        private string m_KeywordText;//关键字名称
        private int m_KeywordType;//关键字类别
        private int m_Priority;//关键字优先级
        private int m_Hits;//关键字使用量
        private DateTime m_LastUseTime;//关键字最后使用时间
        private string m_ArrGeneralID;//关键字对应的内容ID
        private int m_QuoteTimes; //关键字被引用的次数

        public M_KeyWord()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int KeyWordID
        {
            get { return this.m_KeyWordID; }
            set { this.m_KeyWordID = value; }
        }
        public string KeywordText
        {
            get { return this.m_KeywordText; }
            set { this.m_KeywordText = value; }
        }
        public int KeywordType
        {
            get { return this.m_KeywordType; }
            set { this.m_KeywordType = value; }
        }
        public int Priority
        {
            get { return this.m_Priority; }
            set { this.m_Priority = value; }
        }
        public int Hits
        {
            get { return this.m_Hits; }
            set { this.m_Hits = value; }
        }
        public DateTime LastUseTime
        {
            get { return this.m_LastUseTime; }
            set { this.m_LastUseTime = value; }
        }
        public string ArrGeneralID
        {
            get { return this.m_ArrGeneralID; }
            set { this.m_ArrGeneralID = value; }
        }
        public int QuoteTimes
        {
            get { return this.m_QuoteTimes; }
            set { this.m_QuoteTimes = value; }
        }
    }
}
