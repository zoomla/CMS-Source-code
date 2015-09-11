namespace ZoomLa.DALFactory
{
    using System;
    using System.Reflection;
    using System.Web.Configuration;
    using ZoomLa.IDAL;
    using ZoomLa.SQLDAL;

    /// <summary>
    /// 数据访问层的接口
    /// 根据数据库连接类型创建不同数据库访问的实例
    /// </summary>
    public class IDal
    {
        private static readonly string path = WebConfigurationManager.AppSettings["WebDAL"].ToLower();
        public IDal()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static ID_Admin CreateAdmin()
        {
            return new SD_Admin();
        }

        public static ID_Log CreateLog()
        {
            return new SD_Log();
        }
        public static ID_Model CreateModel()
        {
            return new SD_Model();
        }
        public static ID_ModelField CreateModelField()
        {
            return new SD_ModelField();
        }
        public static ID_AdminProfile CreateAdminProfile()
        {
            return new SD_AdminProfile();
        }

        public static ID_User CreateUser()
        {
            return new SD_User();
        }
        public static ID_Role CreateRole()
        {
            return new SD_Role();
        }
        public static ID_Message CreatMessage()
        {
            return new SD_Message();
        }
        
        public static ID_Node CreateNode()
        {
            return new SD_Node();
        }
        public static ID_Comment CreatComment()
        {
            return new SD_Comment();
        }
        public static ID_Label CreateLabel()
        {
            return new SD_Lable();
        }
        public static ID_CommonModel CreateContent()
        {
            return new SD_CommonModel();
        }
        public static ID_CateDetail CreateCateDetail()
        {
            return new SD_CateDetail();
        }
        public static ID_Cate CreateCate()
        {
            return new SD_Cate();
        }
        public static ID_SpecCate CreateSpecCate()
        {
            return new SD_SpecCate();
        }
        public static ID_Spec CreateSpec()
        {
            return new SD_Spec();
        }
        public static ID_SpecInfo CreateSpecInfo()
        {
            return new SD_SpecInfo();
        }
        public static ID_Adzone CreateADZone()
        {
            return new SD_ADZone();
        }
        public static ID_Advertisement CreateAdvertisement()
        {
            return new SD_Advertisement();
        }
        public static ID_Favorite CreateFavorite()
        {
            return new SD_Favorite();
        }
        public static ID_UserModel CreateUserModel()
        {
            return new SD_UserModel();//?
        }
        public static ID_Author CreateAuthor()
        {
            return new SD_Author();
        }
        public static ID_Source CreateSource()
        {
            return new SD_Source();
        }
        public static ID_KeyWord CreateKeyWord()
        {
            return new SD_KeyWord();
        }
        public static ID_DownServer CreateDownServer()
        {
            return new SD_DownServer();
        }
        public static ID_Group CreateGroup()
        {
            return new SD_Group();
        }
        public static ID_ClientRequire CreateClientRequire()
        {
            return new SD_ClientRequire();
        }
        public static ID_Project CreateProject()
        {
            return new SD_Project();
        }
        public static ID_ProjectWork CreateProjectWork()
        {
            return new SD_ProjectWork();
        }
        public static ID_ProjectDiscuss CreateProjectDiscuss()
        {
            return new SD_ProjectDiscuss();
        }
        public static ID_WorkRole CreateWorkRole()
        {
            return new SD_WorkRole();
        }
        public static ID_PayPlat CreatePayPlat()
        {
            return new SD_PayPlat();
        }
        public static ID_Survey CreateSurvey()
        {
            return new SD_Survey();
        }
        public static ID_Question CreateQuestion()
        {
            return new SD_Question();
        }
        public static ID_Answer CreateAnswer()
        {
            return new SD_Answer();
        }
        public static ID_Correct CreateCorrect()
        {
            return new SD_Correct();
        }
    }
}