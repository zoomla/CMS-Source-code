using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
/*
 * 试题API,后期改为ASHX
 */ 
public partial class User_Exam_QuestAPI : System.Web.UI.Page
{
    B_Temp tempBll = new B_Temp();
    B_User buser = new B_User();
    M_Temp tempMod = new M_Temp();
    B_TempUser userBll = new B_TempUser();
    private string Action { get { return Request["Action"]; } }
    private int QType { get { return DataConvert.CLng(Request["QType"]); } }
    private int Qid { get { return DataConvert.CLng(Request["Qid"]); } }
    private string Qids
    {
        get
        {
            string qids = (Request["Qids"] ?? "").Trim(',');
            while(qids.Contains(",,"))
            {
                qids=qids.Replace(",,",",");
            }
            return qids;
        }
    }
    private int FAILED = -1, SUCCESS = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!buser.CheckLogin()) { return; }
        M_UserInfo mu = userBll.GetLogin();
        int result = FAILED;
       // if (mu == null || mu.UserID < 1) { result = NOLOGIN; ReturnTo(result); }
        switch (Action)
        {
            case "cart_add"://试题篮
                tempMod = GetCartByUid(mu.UserID);
                tempMod.Str1=StrHelper.AddToIDS(tempMod.Str1, Qid.ToString());
                AddORUpdate(tempMod);
                result = SUCCESS;
                break;
            case"cart_adds":
                if (string.IsNullOrEmpty(Qids)) { break; }
                tempMod = GetCartByUid(mu.UserID);
                foreach (string qid in Qids.Split(','))
                {
                    if (string.IsNullOrEmpty(qid)) continue;
                    tempMod.Str1 = StrHelper.AddToIDS(tempMod.Str1, qid);
                }
                AddORUpdate(tempMod);
                result = SUCCESS;
                break;
            case "cart_remove":
                tempMod = GetCartByUid(mu.UserID);
                tempMod.Str1 = StrHelper.RemoveToIDS(tempMod.Str1, Qid.ToString());
                AddORUpdate(tempMod);
                result = SUCCESS;
                break;
            case "cart_clear":
                tempMod = GetCartByUid(mu.UserID);
                tempMod.Str1 = "";
                AddORUpdate(tempMod);
                result = SUCCESS;
                break;
            case "collect_add"://试题收藏与移除
                break;
            case "collect_remove":
                break;
        }
        ReturnTo(result);
    }
    private void ReturnTo(int result) 
    {
        Response.Write(result); Response.Flush(); Response.End();
    }
    private M_Temp GetCartByUid(int uid,int usetype=10)
    {
        tempMod = tempBll.SelModelByUid(uid, usetype);
        if (tempMod == null) { tempMod = new M_Temp(); tempMod.UserID = uid; tempMod.UseType = usetype; }
        return tempMod;
    }
    private void AddORUpdate(M_Temp model)
    {
        if (model.ID > 0) { tempBll.UpdateByID(model); }
        else { tempBll.Insert(model); }
    }
}