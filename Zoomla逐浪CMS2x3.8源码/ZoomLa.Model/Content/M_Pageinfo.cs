namespace ZoomLa.Model
{
    using System;

    public class M_Pageinfo
    {
        public M_Pageinfo()
        {
        }
        #region public - Property

        public Int32 UserID { get; set; }
        public String Area { get; set; }
        public String Corporation { get; set; }
        public String ServersType { get; set; }
        public String CorLogo { get; set; }
        public String Dealicence { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion

    }
}
