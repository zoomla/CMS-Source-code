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
/// ID_ProjectDiscuss 的摘要说明
/// </summary>

namespace ZoomLa.IDAL
{
    public interface ID_ProjectDiscuss
    {
        bool AddProjectDiscuss(M_ProjectDiscuss ProjectDiscuss);
        bool DelProjectDiscuss(int DiscussID);
        bool UpdateProjectDiscuss(M_ProjectDiscuss ProjectDiscuss);
        DataTable GetDiscussByWid(int WId);
        int CountDisByWid(int Wid,int Pid); 
        DataTable GetDiscussAll();
    }
}
