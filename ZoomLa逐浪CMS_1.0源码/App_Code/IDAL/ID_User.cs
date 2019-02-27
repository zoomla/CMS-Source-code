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
using System.Collections.Generic;

namespace ZoomLa.IDAL
{
    /// <summary>
    /// ID_User 的摘要说明
    /// </summary>
    public interface ID_User
    {
        //添加会员到数据库
        bool Add(M_UserInfo UserInfo);
        bool DeleteUserById(int userId);
        bool UpDate(M_UserInfo userInfo);
        bool IsExit(int userID);
        bool IsExit(string userName);
        M_UserInfo SeachByID(int userID);
        M_UserInfo SeachByName(string userName);
        DataTable GetUserAll();
        bool UpUserLock(int userID, DateTime lockoutTime);//锁定会员并记录时间
        bool UpUserUnLock(int userID);//解锁（正常）
        bool UpUserTrueFroM(int userID); //未通过管理员认证
        bool UpUserTrueFroE(int userID); //未通过邮件认证
        DataTable GetQuestion(string question);//获取问题的答案
        M_UserInfo GetLoginUser(string loginname, string loginpass);
        M_UserInfo LoginUser(string loginname, string pass);
        IList<string[]> GetUserNameAndEmailList(int num,string text);
        IList<string> GetUserNameList(int startRowIndexId, int maxiNumRows, string keyword);
        int GetUserNameListTotal(string keyword);
    }
}