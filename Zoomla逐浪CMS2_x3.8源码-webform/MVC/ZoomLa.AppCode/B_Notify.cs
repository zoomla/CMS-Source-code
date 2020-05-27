using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 临时存放后期转入BLL
/// </summary>
public class B_Notify
{
    private static List<M_Notify> _notifylist = new List<M_Notify>();
    public static List<M_Notify> NotifyList
    {
        get { if (_notifylist == null)_notifylist = new List<M_Notify>(); return _notifylist; }
    }
    public int expireTime = 5;
    public List<M_Notify> GetNotfiyByUid(int uid)
    {
        string suid = "," + uid + ",";
        return NotifyList.Where(p => p.ReceUsers.Contains(suid) && !p.ReadUsers.Contains(suid)).ToList();
    }
    /// <summary>
    /// 移除过期的提示
    /// </summary>
    public void RemoveExpire()
    {
        NotifyList.RemoveAll(p => (DateTime.Now - p.CDate).Minutes > expireTime);
    }
    public void AddReader(M_Notify model, int uid)
    {
        model.ReadUsers = model.ReadUsers.Trim(',') + "," + uid + ",";
    }
    public static void AddNotify(string uname, string title, string content, string receuser)
    {
        M_Notify model = new M_Notify();
        model.CUName = uname;
        model.Title = title;
        model.Content = content;
        model.ReceUsers = receuser;
        NotifyList.Add(model);
    }
}
public class M_Notify
{
    public M_Notify()
    {
        NotifyType = 1;//AT
        CDate = DateTime.Now;
        ReadUsers = "";
    }
    public string Title { get; set; }
    public string Content { get; set; }
    /// <summary>
    /// 接收人IDS
    /// </summary>
    public string ReceUsers { get; set; }
    /// <summary>
    /// 已接收者的IDS(所有人都接收或超时则自动移除)
    /// </summary>
    public string ReadUsers { get; set; }
    /// <summary>
    /// 发送者名字
    /// </summary>
    public string CUName { get; set; }
    /// <summary>
    /// 提示类型,用于显示不同的图标
    /// </summary>
    public int NotifyType { get; set; }
    public DateTime CDate { get; set; }
}