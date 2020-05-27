using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

/*
 * OA公共类，用于装饰与统计信息
 */ 
public class OACommon
{
    protected B_Group groupBll = new B_Group();
    protected B_MisProLevel stepBll = new B_MisProLevel();
    protected B_Mis_AppProg progBll = new B_Mis_AppProg();
    protected B_OA_FreePro freeBll = new B_OA_FreePro();
    protected B_OA_Document oaBll = new B_OA_Document();
    protected B_User buser = new B_User();
    protected M_Mis_AppProg progMod = new M_Mis_AppProg();
    protected M_OA_Document oaMod = new M_OA_Document();
    protected M_Group groupMod = new M_Group();
    public string OADir = AppDomain.CurrentDomain.BaseDirectory+@"\UploadFiles\OA\";
    public string OAVDir = "/UploadFiles/OA/";//Uname+uid/
    public OACommon() { }
    /// <summary>
    /// 签字信息,发稿人,发稿日期(创建公文模板时使用，审核公文时处理)
    /// </summary>
    public string[] Holder = new string[] { "{$SignInfo}" };//,"{$UserName}","{$PostDate}"
    public string[] TempHolder = new string[] { "{$SignImg}" };
    //{$SignInfo}:签字审批信息
    /// <summary>
    /// OA模板占位符处理
    /// </summary>
    public string ReplaceHolder(M_OA_Document oaMod,string filter="")
    {
        for (int i = 0; i < Holder.Length; i++)
        {
            string r = GetHolder(oaMod, 0, filter);
            oaMod.Content = oaMod.Content.Replace(Holder[i], r);
        }
        return oaMod.Content;
    }
    /// <summary>
    /// 返回占位符字符串
    /// </summary>
    /// <param name="oaMod">OA模型</param>
    /// <param name="holdIndex">占位符</param>
    /// <param name="filter">是否要进行进滤,输入RowFilter语句,用于事务</param>
    public string GetHolder(M_OA_Document oaMod, int holdIndex,string filter="")
    {
        string r = "";
        switch (Holder[holdIndex])
        {
            case "{$SignInfo}":
                //获取所有用户签字信息
                DataTable progDT = progBll.SelByAppID(oaMod.ID.ToString());
                if (!string.IsNullOrEmpty(filter))
                {
                    progDT.DefaultView.RowFilter = filter;
                    progDT = progDT.DefaultView.ToTable();
                }
                if (progDT == null || progDT.Rows.Count < 1)
                {

                }
                else
                {
                    //需要判断是协办还是主办人
                    foreach (DataRow dr in progDT.Rows)
                    {
                        int docid = Convert.ToInt32(dr["AppID"]);
                        int stepnum = Convert.ToInt32(dr["ProLevel"]);
                        M_MisProLevel freeMod = oaMod.IsFreePro ? freeBll.SelByProIDAndStepNum(docid, stepnum) : stepBll.SelByProIDAndStepNum(oaMod.ProID, stepnum);
                        if (freeMod == null) { continue; }
                        if (freeMod.CCUser.Contains("," + DataConvert.CStr(dr["AppRoveID"]) + ","))//协办
                        {
                            string template = HttpUtility.HtmlDecode(OAConfig.ParterSignTemplate);
                            r += template.Replace("@UserName", dr["UserName"].ToString()).Replace("@Remind", dr["Remind"].ToString()).Replace("@SignDate", Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy年MM月dd日"));
                        }
                        else//主办
                        {
                            string template = HttpUtility.HtmlDecode(OAConfig.LeaderSignTemplate);
                            r += template.Replace("@UserName", dr["UserName"].ToString()).Replace("@Remind", dr["Remind"].ToString()).Replace("@SignDate", Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy年MM月dd日"));
                        }
                    }
                }
                break;
            default:
                break;
        }
        return r;
    }

    /// <summary>
    ///  签章(创建公文模板时使用,发送公文时处理)
    /// </summary>
    public string GetHolder(M_Mis_Model oaTemp, string value)
    {
        string r = "";
        for (int i = 0; i < TempHolder.Length; i++)
        {
            string action = TempHolder[i];
            switch (action)
            {
                case "{$SignImg}"://调用部门签章信息
                    //string img="<img src='{0}'></img>";
                    groupMod = groupBll.GetByID(Convert.ToInt32(value));
                    r = oaTemp.ModelContent.Replace(TempHolder[0], groupMod.SignImg);
                    break;
            }
        }
        return r;
    }
    /// <summary>
    /// 清空文档中的占位符
    /// </summary>
    public string ClearHolder(M_OA_Document oaMod) 
    {
        for (int i = 0; i < Holder.Length; i++)
        {
            oaMod.Content = oaMod.Content.Replace(Holder[i],"");
        }
        for (int i = 0; i < Holder.Length; i++)
        {
            oaMod.Content = oaMod.Content.Replace(TempHolder[i], "");
        }
        return oaMod.Content;
    }
    //------保存文件 
    public enum SaveType{Sign,Mail};
    public bool IsWord(string fname)
    {
        string[] word = "doc,docx".Split(',');
        string ext = Path.GetExtension(fname).ToLower().Replace(".", "");
        return word.Contains(ext);
    }
    public string GetMyDir(M_UserInfo mu)//稍后编码加一次密
    {
        return OAVDir += mu.UserName + mu.UserID + "/";
    }
    /// <summary>
    /// 存储文件，返回虚拟路径,注意IE下其是全路径,Chrome下仅文件名
    /// </summary>
    public string SaveFile(HttpPostedFile file, SaveType type, string value = "")
    {
        string result = "";
        if (file.ContentLength < 1) return "";
        string path = "", filename = "";
        switch (type)
        {
            case SaveType.Sign://组名||用户名
                path = OADir + @"\Sign\" + value+@"\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                filename = GetFileName(path, file.FileName);
                SafeSC.SaveFile(path + filename, file);
                result = path + filename;
                break;
            case SaveType.Mail:
                path = OADir + @"\Mail\" + value + @"\";//用户名
                 if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                filename = GetFileName(path, file.FileName);
                SafeSC.SaveFile(path+filename,file);
                result = path + filename;
                break;
        }
        return function.PToV(result);
    }
    /// <summary>
    /// 如有重名，以(1).jpg,(2).jpg重命名文件,
    /// </summary>
    /// <param name="ppath">物理路径</param>
    /// <param name="fileName">带后缀名</param>
    public string GetFileName(string ppath, string fileName)
    {
        if (fileName.IndexOf(@"\") != -1)//IE提交
        {
            int si = fileName.LastIndexOf(@"\");
            int ei=fileName.Length-si;
            fileName = fileName.Substring(si, ei);
        }
        for (int i = 1; File.Exists(ppath + fileName); i++)
        {
            fileName = fileName.Replace("(" + (i-1) + ").", ".");
            int index = fileName.LastIndexOf(".");
            fileName= fileName.Insert((index),"("+i+")");
        }
        return fileName;
    }
    /// <summary>
    /// 服务于NodeMap,flag=1,根据nodeID获取节点名
    /// </summary>
    /// <param name="nodeName">存在XML中的节点名</param>
    /// <returns>节点ID</returns>
    public static string GetNodeID(string node, int flag = 0)
    {
        DataTable nodelist = new DataTable();
        nodelist = JsonHelper.JsonToDT(OAConfig.NodeMap);
        var nodeids = (from nodes in nodelist.AsEnumerable() select new { ID = nodes["ID"].ToString(), NodeName = nodes["NodeName"].ToString(), NodeID = nodes["NodeID"].ToString() }).ToList();
        try
        {
            string result = "";
            switch (flag)
            {
                case 0://根据节点名获取节点ID
                    result = nodeids.Find(n => n.NodeName.Equals(node)).NodeID;
                    break;
                case 1://根据节点ID获取节点名
                    result = nodeids.Find(n => n.NodeID.Equals(node)).NodeName;
                    break;
                default:
                    break;
            }
            return result;
        }
        catch { return ""; }
    }
    //---------------------流程块
    public void CreateStep(M_OA_Document oaMod, M_MisProcedure proceMod, OAStepParam param)
    {
        switch (proceMod.MyProType)
        {
            case M_MisProcedure.ProTypes.Admin:
                ////不需处理
                //break;
            case M_MisProcedure.ProTypes.AdminFree:
                CreateAdminFreeStep(oaMod, proceMod, param);
                break;
            case M_MisProcedure.ProTypes.Free:
                CreateFreeStep(oaMod, proceMod, param);
                break;
        }
    }
    public void CreateFreeStep(M_OA_Document oaMod, M_MisProcedure proceMod,OAStepParam param)
    {
        M_MisProLevel freeMod = null;
        if (param.IsFirst)
        {
            freeMod = freeBll.SelByDocID(oaMod.ID);
        }
        else if (param.StepID > 0)//修改步骤
        {
            freeMod = freeBll.SelReturnModel(param.StepID);
        }
        bool isUpdate = true;
        if (freeMod == null)//非修改则新建
        {
            isUpdate = false;
            freeMod = new M_MisProLevel();
        }
        freeMod.ProID = proceMod.ID ;
        freeMod.stepNum = param.StepNum;
        freeMod.stepName = proceMod.ProcedureName + "第" + param.StepNum+ "步";
        freeMod.SendMan = buser.GetLogin().UserID.ToString();
        freeMod.ReferUser = param.ReferUser.Trim(',');
        freeMod.ReferGroup = "";
        freeMod.CCUser = param.CCUser.Trim(',');
        freeMod.CCGroup = "";
        freeMod.HQoption = 1;
        freeMod.Qzzjoption = 0;
        freeMod.HToption = 2;
        freeMod.EmailAlert = "";
        freeMod.EmailGroup = "";
        freeMod.SmsAlert = "";
        freeMod.SmsGroup = "";
        freeMod.BackOption = oaMod.ID;
        freeMod.PublicAttachOption = 1;
        freeMod.PrivateAttachOption = 1;
        freeMod.DocAuth = "refer";
        freeMod.Status = 1;
        freeMod.CreateTime = DateTime.Now;
        freeMod.Remind = oaMod.Title + "的流程";
        if (isUpdate)
        {
            freeBll.UpdateByID(freeMod);
        }
        else
        {
            freeBll.Insert(freeMod);
            if (!param.IsFirst)//非起始步骤修改,则更新状态
            {
                oaMod.Status = 0;
                oaBll.UpdateByID(oaMod);
            }
        }
    }
    //后期可扩展为单步步骤,自由
    public void CreateAdminFreeStep(M_OA_Document oaMod, M_MisProcedure proceMod,OAStepParam param)
    {
        //从proceMod中拷贝流程信息,但主办人可自指定
        M_MisProLevel freeMod = null;
        M_MisProLevel stepMod = stepBll.SelByProIDAndStepNum(proceMod.ID, param.StepNum);
        if (stepMod == null) { function.WriteErrMsg("错误:"+proceMod.ProcedureName+"下步骤不完整"); }
        if (param.IsFirst)
        {
            freeMod=freeBll.SelByDocID(oaMod.ID);
        }
        else if (param.StepID > 0)
        {
            freeMod = freeBll.SelReturnModel(param.StepID);
        }
        bool isUpdate = true;//需处理
        if (freeMod == null)
        {
            isUpdate = false;
            stepMod.ID = 0;
            freeMod = stepMod;
        }
        freeMod.SendMan = buser.GetLogin().UserID.ToString();
        freeMod.ReferUser = param.ReferUser.Trim(',');
        freeMod.ReferGroup = "";
        freeMod.CCUser = param.CCUser.Trim(',');
        //freeMod.CCGroup = "";
        freeMod.BackOption = oaMod.ID;
        freeMod.CreateTime = DateTime.Now;
        freeMod.Remind = oaMod.Title + "的流程";
        if (isUpdate)
        {
            freeBll.UpdateByID(freeMod);
        }
        else
        {
            freeBll.Insert(freeMod);
            if (!param.IsFirst)
            {
                oaMod.Status = 0;
                oaBll.UpdateByID(oaMod);
            }
        }
    }
}
public class OAStepParam
{
    public string CCUser = "";
    public string ReferUser = "";
    public int StepNum = 1;
    public int StepID = 0;
    //是否起始步骤
    public bool IsFirst = false;
}