namespace ZoomLa.IDAL
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
    using ZoomLa.Model;

    /// <summary>
    /// ID_Message 的摘要说明
    /// </summary>
    public interface ID_Message
    {
        bool Add(M_Message message);//增加信息
        bool Update(M_Message message);//更新信息
        bool DeleteById(int msgID);//根据信息ID删除信息
        bool IsExit(int msgID);//根据信息ID判断信息是否存在
        bool IsExit(string title);//根据信息标题判断信息是否已存在
        M_Message SeachById(int msgID);//根据ID查询信息
        DataTable SeachByUser(string UserName);//查询某个用户的短消息
        DataTable SeachMessageAll();//查询所有信息
        void DeleteByDate(int datediff);
        void DeleteByUser(string UserName);
        int UserMessCount(string UserName);
    }
}