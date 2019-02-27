using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using BDUModel;
using BDUBLL;
using ZoomLa.BLL.Helper;

public partial class Plugins_Camera : CustomerPageAction
{
    protected B_Node bnode = new B_Node();
    protected B_Model bmode = new B_Model();
    protected B_ShowField bshow = new B_ShowField();
    protected B_ModelField bfield = new B_ModelField();
    protected B_Content bll = new B_Content();
    protected int NodeID;
    protected int ModelID;
    private B_Role RLL = new B_Role();
    protected B_NodeRole bNr = new B_NodeRole();
    private B_AuditingState ba = new B_AuditingState();
    private B_Admin badmin = new B_Admin();
    protected B_Sensitivity sll = new B_Sensitivity();
    protected bool createnew = true;
    protected DataTable table = new DataTable();//从表
    DataTable nodeInfo = new DataTable();//node的各种信息如名字等，下方赋值。
    List<PicTure> picList = new List<PicTure>();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Request.Form["type"] == "imageS")
        {
            //图片存入临时文件夹，将保存路径返回
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(PictureManage(HttpContext.Current));
            Response.End();
            return;
        }
        else if (Request.Form["type"] == "clearSession")
        {
            Session.Remove("picList");
           
        }
        
        

    }
    protected string PictureManage(HttpContext context)
    {
        ImgHelper imgHelp = new ImgHelper();
        context.Response.ContentType = "text/plain";
        string base64String = context.Request["data"];
        M_UserInfo mu = buser.GetLogin();
        string filePath2 = "/UploadFiles/Camera/"+mu.UserID+"/";//用于存入数据库
        //判断路径是否存在,若不存在则创建路径
        DirectoryInfo upDir = new DirectoryInfo(function.VToP(filePath2));
        if (!upDir.Exists){ upDir.Create();}
        string fileName = DateTime.Now.ToString("yyyyMMddHHmm") + function.GetRandomString(4) + ".jpg";
        imgHelp.Base64ToImg(filePath2 + fileName, base64String);
        return filePath2 + fileName;
    }

    /// <summary>
    /// 将图片数据插入到表中。
    /// </summary>
    protected void InsertToDB(List<PicTure> list)
    {
        foreach (PicTure p in list)
        {
            PicTure_BLL turebll = new PicTure_BLL();
            turebll.Add(p);
        }
    }

}