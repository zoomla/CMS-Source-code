using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

namespace ZoomLa.BLL.Plat
{
    /*
     * 暂时存IIS,后期换为专门的文件服务器,否则无法保证文件安全
     */ 
    public class B_Plat_Common
    {
        public enum SaveType { Company, Company_P, Person, Person_P,Blog,Plat_Task,Plat_Project }
        /// <summary>
        /// 传入需要存储的文件类型,返回路径
        /// </summary>
        public static string GetDirPath(SaveType stype)
        {
            B_Plat_Comp compBll=new B_Plat_Comp();
            M_User_Plat upMod = B_User_Plat.GetLogin();
            M_Plat_Comp compMod = compBll.SelReturnModel(upMod.CompID);
            if (string.IsNullOrEmpty(compMod.UPPath)) { compMod.UPPath = compBll.CreateUPPath(compMod); compBll.UpdateByID(compMod); }
            string bPath = SiteConfig.SiteOption.UploadDir;
            string persondir = EncryptHelper.AESEncrypt(upMod.UserID + ":" + (upMod.UserName.Length > 4 ? upMod.UserName.Substring(0, 3) : upMod.UserName)) + "/";
            string vpath = bPath + "Plat/" + compMod.UPPath.Trim('/') + "/";
            switch (stype)
            {
                case SaveType.Company://公司文件,网盘等
                    vpath += "DocCenter/Common/";
                    break;
                case SaveType.Company_P://公司私有文件,如Logo等
                    vpath += "Private/";
                    break;
                case SaveType.Person://个人文件,用于网盘(可共享信息)
                    vpath += "DocCenter/" + persondir;
                    break;
                case SaveType.Person_P://私人文件,如头像等信息(非共享信息)
                    vpath += "Person/" + persondir;
                    break;
                case SaveType.Blog://用户博客上传的附件
                    vpath += "Blog/" + persondir;
                    break;
                case SaveType.Plat_Task://需要再加上TaskName
                    vpath += "Task/";
                    break;
                case SaveType.Plat_Project:
                    vpath += "Object/";
                    break;
            }
            return vpath;
        }
        //生成时间戳,参数使用来源,示例:Plat
        public static string GetTimeStamp(string salt)
        {
            string time = DateTime.Now.ToString("yyyyMMddhhmm");
            return EncryptHelper.AESEncrypt(salt + ":" + time);
        }
        //前往目标邮件站点,返回http://mail.z01.com
        public static string GetMailSite(string mail)
        {
            return "http://mail." + mail.Substring(mail.LastIndexOf('@') + 1);
        }
    }
}
