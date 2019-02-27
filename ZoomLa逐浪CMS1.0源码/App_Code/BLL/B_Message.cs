namespace ZoomLa.BLL
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
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;

    /// <summary>
    /// B_Message 的摘要说明
    /// </summary>
    public class B_Message
    {
	    public B_Message()
	    {
		    //
		    // TODO: 在此处添加构造函数逻辑
		    //
	    }
        private static readonly ID_Message message = IDal.CreatMessage();
        //增加信息
        public static bool Add(M_Message messInfo)
        {
            return message.Add(messInfo);
        }
        //更新信息
        public static bool Update(M_Message messInfo)
        {
            return message.Update(messInfo);
        }
        //删除信息
        public static bool DelteById(int MsgID)
        {
            return message.DeleteById(MsgID);
        }
        //查询所有信息
        public static DataView GetMessAll()
        {
            DataTable dt = message.SeachMessageAll();
            return dt.DefaultView;
        }
        
        public static M_Message GetMessByID(int msgID)
        {
            return message.SeachById(msgID);
        }
        public static DataTable SeachByUserName(string UserName)
        {
            return message.SeachByUser(UserName);
        }
        public static void DeleteByDate(int datediff)
        {
            message.DeleteByDate(datediff);
        }
        public static void DeleteByUser(string UserName)
        {
            message.DeleteByUser(UserName);
        }
        public static int UserMessCount(string UserName)
        {
            return message.UserMessCount(UserName);
        }
    }
}