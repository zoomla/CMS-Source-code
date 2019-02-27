namespace ZoomLa.DALFactory
{
    using System;
    using System.Reflection;
    using System.Web.Configuration;
    using ZoomLa.IDAL;
    using ZoomLa.SQLDAL;
    using ZoomLa.IDAL.Shop;
    using ZoomLa.SQLDAL.Shop;
    using ZoomLa.IDAL.User;
    using ZoomLa.SQLDAL.User;
    using ZoomLa.IDAL.Component;
    using ZoomLa.SQLDAL.Component;
    using ZoomLa.IDAL.Content;
    using ZoomLa.SQLDAL.Content;
    using ZoomLa.IDAL.User.Develop;
    using ZoomLa.SQLDAL.User.Develop;
    using ZoomLa.IDAL.AdSystem;
    using ZoomLa.SQLDAL.AdSystem;
    using ZoomLa.IDAL.FTP;
    using ZoomLa.SQLDAL.FTP;
    using ZoomLa.IDAL.Page;
    using ZoomLa.SQLDAL.Page;


    /// <summary>
    /// 数据访问层的接口
    /// 根据数据库连接类型创建不同数据库访问的实例
    /// </summary>
    public class IDal
    {
        private static readonly string path = WebConfigurationManager.AppSettings["WebDAL"];
        public IDal()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 创建审核状态数据访问层
        /// </summary>
        /// <returns></returns> 
        public static ID_AuditingState CreateAuditingState()
        {
            return new SD_AuditingState();
        }
        /// <summary>
        /// 创建流程管理数据访问层
        /// </summary>
        /// <returns></returns>
        public static ID_Flow CreateFlow()
        {
            return new SD_Flow();
        }
        /// <summary>
        /// 创建流程步骤数据访问层
        /// </summary>
        /// <returns></returns>
        public static ID_Process CreateProcess()
        {
            return new SD_Process();
        }
        /// <summary>
        /// 创建节点绑定权限数据访问层
        /// </summary>
        /// <returns></returns>
        public static ID_NodeBindDroit CreateNodeBinDroit()
        {
            return new SD_NodeBindDroit();
        }
        public static ID_Admin CreateAdmin()
        {
            return new SD_Admin();
        }

        public static ID_Log CreateLog()
        {
            return new SD_Log();
        }
        public static ID_History CreateHis()
        {
            return new SD_History();
        }
        public static ID_Model CreateModel()
        {
            return new SD_Model();
        }
        public static ID_ModelField CreateModelField()
        {
            return new SD_ModelField();
        }
        public static ID_MoneyManage CreateMoneyManage()
        {
            return new SD_MoneyManage();
        }
        public static ID_Client_Basic CreateClient_Basic()
        {
            return new SD_Client_Basic();
        }
        public static ID_Client_Enterprise CreateClient_Enterprise()
        {
            return new SD_Client_Enterprise();
        }
        public static ID_Client_Penson CreateClient_Penson()
        {
            return new SD_Client_Penson();
        }
        public static ID_Client_Service CreateClient_Service()
        {
            return new SD_Client_Service();
        }
        public static ID_AdminProfile CreateAdminProfile()
        {
            return new SD_AdminProfile();
        }

        public static ID_User CreateUser()
        {
            return new SD_User();
        }

        public static ID_Cut CreateCut()
        {
            return new SD_Cut();

        }
        public static SD_PageSign CreatePsign()
        {
            return new SD_PageSign();
        }
        public static ID_Plugins CreatePlugins()
        {
            return new SD_Plugins();

        }
        public static ID_Role CreateRole()
        {
            return new SD_Role();
        }
        public static ID_Message CreatMessage()
        {
            return new SD_Message();
        }
        public static ID_Project CreateProject()
        {
            return new SD_Project();
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
        public static ID_GradeOption CreateGradeOption()
        {
            return new SD_GradeOption();
        }
        public static ID_ClientRequire CreateClientRequire()
        {
            return new SD_ClientRequire();
        }
        public static ID_Projects CreateProjects()
        {
            return new SD_Projects();
        }
        public static ID_ProjectType CreateProjectType()
        {
            return new SD_ProjectType();
        }
        public static ID_ProjectBaseField CreateProjectBaseField()
        {
            return new SD_ProjectBaseField();
        }
        public static ID_ProjectsComments CreateProjectsComments()
        {
            return new SD_ProjectsComments();
        }
        public static ID_Product CreateProduct()
        {
            return new SD_Product();
        }
        public static ID_Processes CreateProcesses()
        {
            return new SD_Processes();
        }
        /// <summary>
        /// 代购使用
        /// </summary>
        /// <returns></returns>
        public static ID_Products CreateProducts()
        {
            return new SD_Products();
        }
        public static ID_UserProduct CreateUserProduct()
        {
            return new SD_UserProduct();
        }
        public static ID_UserProduct_T CreateUserProduct_t()
        {
            return new SD_UserProduct_T();
        }
        public static ID_UserAlipayTable CreateUserAlipay()
        {
            return new SD_UserAlipayTable();
        }
        public static ID_Stock CreateStock()
        {
            return new SD_Stock();
        }
        public static ID_UserStock CreateUserStock()
        {
            return new SD_UserStock();
        }
        public static ID_Present CreatePresent()
        {
            return new SD_Present();
        }
        public static ID_Promotions CreatePromotions()
        {
            return new SD_Promotions();
        }
        public static ID_OrderList CreateOrderList()
        {
            return new SD_OrderList();
        }
        public static ID_Cart CreateCart()
        {
            return new SD_Cart();
        }
        public static ID_CartPro CreateCartPro()
        {
            return new SD_CartPro();
        }
        /// <summary>
        /// 代购使用
        /// </summary>
        /// <returns></returns>
        public static ID_CartPros CreateCartPros()
        {
            return new SD_CartPros();
        }
        public static ID_UserOrderList CreateUserOrderList()
        {
            return new SD_UserOrderList();
        }
        public static ID_UserCart CreateUserCart()
        {
            return new SD_UserCart();
        }
        public static ID_UserCartPro CreateUserCartPro()
        {
            return new SD_UserCartPro();
        }
        public static ID_UserStoreTable CreateUserStoreTable()
        {
            return new SD_UserStoreTable();
        }

        public static ID_Manufacturers CreateManufacturers()
        {
            return new SD_Manufacturers();
        }

        public static ID_Trademark CreateTrademark()
        {
            return new SD_Trademark();
        }

        public static ID_Delivier CreateDelivier()
        {
            return new SD_Delivier();
        }

        public static ID_Templata CreateTemplata()
        {
            return new SD_Templata();
        }

        public static ID_PageStyle CreatePageStyle()
        {
            return new SD_PageStyle();
        }

        public static ID_ProjectWork CreateProjectWork()
        {
            return new SD_ProjectWork();
        }

        public static ID_StoreStyleTable CreateStoreStyle()
        {
            return new SD_StoreStyleTable();
        }
        public static ID_ProjectDiscuss CreateProjectDiscuss()
        {
            return new SD_ProjectDiscuss();
        }
        public static ID_UserFriend CreateUserFriend()
        {
            return new SD_UserFriend();
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

        public static ID_Answer CreateAnswer()
        {
            return new SD_Answer();
        }
        public static ID_Correct CreateCorrect()
        {
            return new SD_Correct();
        }
        public static ID_Payment CreatePayment()
        {
            return new SD_Payment();
        }
        public static ID_ProjectField CreateProjectField()
        {
            return new SD_ProjectField();
        }
        public static ID_GroupFieldPermissions CreateGroupFieldPermissions()
        {
            return new SD_GroupFieldPermissions();
        }
        public static ID_ProjectCategory CreateProjectCategory()
        {
            return new SD_ProjectCategory();
        }
        public static ID_ProjectAffairs CreateProjectAffairs()
        {
            return new SD_ProjectAffairs();
        }

        public static ID_DataDicCategory CreateDicCate()
        {
            return new SD_DataDicCategory();
        }
        public static ID_DataDictionary CreateDictionary()
        {
            return new SD_DataDictionary();
        }
        public static ID_Bbs CreateBBS()
        {
            return new SD_Bbs();
        }
        public static ID_GuestBook CreateGuestBook()
        {
            return new SD_GuestBook();
        }

        public static ID_ShopNodeinfo CreateShopNode()
        {
            return new SD_ShopNodeinfo();
        }
        public static ID_ShopLable CreateShopLable()
        {
            return new SD_ShopLable();
        }
        public static ID_Shopconfig CreateShopconfig()
        {
            return new SD_Shopconfig();
        }
        public static ID_Shopsearch CreateShopsearch()
        {
            return new SD_Shopsearch();
        }
        public static ID_ShopGrade CreateShopGrade()
        {
            return new SD_ShopGrade();
        }
        public static ID_ShopCommentary CreateShopCommentary()
        {
            return new SD_ShopCommentary();
        }
        public static ID_ShopBrand CreateShopBrand()
        {
            return new SD_ShopBrand();
        }
        public static ID_CallNote CreateCallNote()
        {
            return new SD_CallNote();
        }
        public static ID_Permission CreatePermission()
        {
            return new SD_Permission();
        }
        public static ID_BindPro CreateBindPro()
        {
            return new SD_BindPro();
        }
        public static ID_School CreateSchool()
        {
            return new SD_School();
        }
        public static ID_Student CreateStudent()
        {
            return new SD_Student();
        }
        public static ID_ClassRoom CreateClassRoom()
        {
            return new SD_ClassRoom();
        }
        public static ID_Interlocution CreateInterlocution()
        {
            return new SD_Interlocution();
        }
        public static ID_Result CreateResult()
        {
            return new SD_Result();
        }
        public static ID_RoomNotify CreateRoomNotify()
        {
            return new SD_RoomNotify();
        }
        public static ID_RoomActive CreateRoomActive()
        {
            return new SD_RoomActive();
        }
        public static ID_RoomActiveJoin CreateRoomActiveJoin()
        {
            return new SD_RoomActiveJoin();
        }
        public static ID_AddRessList CreateAddRessList()
        {
            return new SD_AddRessList();
        }
        public static ID_RoomUpFile CreateRoomUpFile()
        {
            return new SD_RoomUpFile();
        }
        public static ID_MailManage CreateMailManage()
        {
            return new SD_MailManage();
        }
        public static ID_MailInfo CreateMailInfo()
        {
            return new SD_MailInfo();
        }
        public static ID_Subscribe CreateSubscribe()
        {
            return new SD_Subscribe();
        }
        public static ID_MailIdiograph CreateMailIdiograph()
        {
            return new SD_MailIdiograph();
        }
        public static ID_Scheme CreateScheme()
        {
            return new SD_Scheme();
        }
        public static ID_SchemeInfo CreateSchemeInfo()
        {
            return new SD_SchemeInfo();
        }
        public static ID_ShopCompete CreateShopCompete()
        {
            return new SD_ShopCompete();
        }
        public static ID_Pub CreatePub()
        {
            return new SD_Pub();
        }
        public static ID_Card CreateCard()
        {
            return new SD_Card();
        }
        public static ID_CardType CreateCardType()
        {
            return new SD_CardType();
        }
        public static ID_Cash CreateCash()
        {
            return new SD_Cash();
        }
        public static ID_ExChange CreateExChange()
        {
            return new SD_ExChange();
        }
        public static ID_ChangeTalk CreateChangeTalk()
        {
            return new SD_ChangeTalk();
        }
        public static ID_ChangeProduct CreateChangeProduct()
        {
            return new SD_ChangeProduct();
        }
        public static ID_URLRewriter CreateURLRewriter()
        {
            return new SD_URLRewriter();
        }
        public static ID_CollectionItem CreateCollectionItem()
        {
            return new SD_CollectionItem();
        }
        public static ID_DChat CreateDChat()
        {
            return new SD_DChat();
        }
        public static ID_Duser CreateDuser()
        {
            return new SD_Duser();
        }
        public static ID_Daffiche CreateDaffiche()
        {
            return new SD_Daffiche();
        }
        public static ID_Dbooth CreateDbooth()
        {
            return new SD_Dbooth();
        }
        public static ID_Dscene CreateDscene()
        {
            return new SD_Dscene();
        }
        public static ID_Dinformation CreateDinformation()
        {
            return new SD_Dinformation();
        }
        public static ID_CollectionInfo CreateCollectionInfo()
        {
            return new SD_CollectionInfo();
        }
        public static ID_NodeRole CreateNodeRole()
        {
            return new SD_NodeRole();
        }
        public static ID_MultiNode CreateMultiNode()
        {
            return new SD_MultiNode();
        }
        public static ID_DShowUser CreateDShowUser()
        {
            return new SD_DShowUser();
        }
        public static ID_CallNode CreateCallNode()
        {
            return new SD_CallNode();
        }
        public static ID_MiUserInfo CreateMiUserInfo()
        {
            return new SD_MiUserInfo();
        }
        public static ID_MTit CreateMTit()
        {
            return new SD_MTit();
        }
        public static ID_CustomerService CreateCustomerService()
        {
            return new SD_CustomerService();
        }
        public static ID_ServiceSeat CreateServiceSeat()
        {
            return new SD_ServiceSeat();
        }
        public static ID_WapNode CreateWapNode()
        {
            return new SD_WapNode();
        }
        public static ID_WapOrder CreateWapOrder()
        {
            return new SD_WapOrder();
        }
        public static ID_WapArticle CreateWapArticle()
        {
            return new SD_WapArticle();
        }
        public static ID_OnlineUsers CreateOnlineUsers()
        {
            return new SD_OnlineUsers();
        }
        public static ID_WapGolds CreateWapGolds()
        {
            return new SD_WapGolds();
        }
        public static ID_SNum CreateSNum()
        {
            return new SD_SNum();
        }
        public static ID_SUser CreateSUser()
        {
            return new SD_SUser();
        }

        public static ID_File CreateFile()
        {
            return new SD_File();
        }

        public static ID_PrintType CreatePrintType()
        {
            return new SD_PrintType();
        }
        public static ID_Program CreateProgram()
        {
            return new SD_Program();
        }
        public static ID_SiteInfoXMl CreateSiteInfoXMl()
        {
            return new SD_SiteInfoXMl();
        }
        public static ID_Print CreatePrint()
        {
            return new SD_Print();
        }
        public static ID_SiteChild CreateSiteChild()
        {
            return new SD_SiteChild();
        }
        public static ID_SitePas CreateSitePas()
        {
            return new SD_SitePas();
        }
        public static ID_UnionInfo CreateUnionInfo()
        {
            return new SD_UnionInfo();
        }
        public static ID_SendList CreateSendList()
        {
            return new SD_SendList();
        }
        public static ID_SiteMap CreateSiteMap()
        {
            return new SD_SiteMap();
        }
        public static ID_UserIP getUserIP()
        {
            return new SD_UserIP();
        }
        public static ID_ToUrl getUrl()
        {
            return new SD_Url();
        }
        public static ID_UserBaseField CreateUserBaseField()
        {
            return new SD_UserBaseField();
        }
        public static ID_Addlink CreateAddlink()
        {
            return new SD_Addlink();
        }

        public static ID_UserBank CreateUserBank()
        {
            return new SD_UserBank();
        }
        public static ID_BossInfo CreateBossInfo()
        {
            return new SD_BossInfo();
        }

        public static ID_MuClass CreateMuClass()
        {
            return new SD_MuClass();
        }

        public static ID_MuPage CreateMuPage()
        {
            return new SD_MuPage();
        }

        public static ID_MuTemp CreateMuTemp()
        {
            return new SD_MuTemp();
        }

        public static ID_MuProduct CreateMuProduct()
        {
            return new SD_MuProduct();
        }

        public static ID_MuPic CreateMuPic()
        {
            return new SD_MuPic();
        }

        #region crystal do those



        public static ID_MobilePay CreateMobilePay()
        {
            return new SD_MobilePay();
        }

        public static ID_Data CreateData()
        {
            return new SD_Data();
        }

        public static ID_GuessBet CreateGuessBet()
        {
            return new SD_GuessBet();
        }
        /// <summary>
        /// MP值
        /// </summary>
        /// <returns></returns>
        public static ID_GuessMP CreateGuessMP()
        {
            return new SD_GuessMP();
        }
        public static ID_Defray CreateDefray()
        {
            return new SD_Defray();
        }


        #endregion


        public static ID_VideoHouse CreateVideoHouse()
        {
            return new SD_VideoHouse();
        }

        public static ID_VideoHouseApply CreateVideoHouseApply()
        {
            return new SD_VideoHouseApply();
        }

        public static ID_VideoInfo CreateVideoInfo()
        {
            return new SD_VideoInfo();
        }
        public static ID_Vinvite CreateVinvite()
        {
            return new SD_Vinvite();
        }
        public static ID_VResume CreateVResume()
        {
            return new SD_VResume();
        }
        public static ID_VJobInfo CreateVJobInfo()
        {
            return new SD_VJobInfo();
        }
        public static ID_VRoom CreateVRoom()
        {
            return new SD_VRoom();
        }
        public static ID_MySubscription CreateBBS_MySubscription()
        {
            return new SD_MySubscription();
        }
        public static ID_SubscriptionCount Create_SubscriptionCount()
        {
            return new SD_SubscriptionCount();
        }
        public static ID_Bull_Exp CreateBull_Exp()
        {
            return new SD_Bull_Exp();
        }
        public static ID_Bull_Grass CreateBull_Grass()
        {
            return new SD_Bull_Grass();
        }
        public static ID_Bull_GrassQuarterly CreateBull_GrassQuarterly()
        {
            return new SD_Bull_GrassQuarterly();
        }
        public static ID_Bull_InfoList CreateBull_InfoList()
        {
            return new SD_Bull_InfoList();
        }
        public static ID_Bull_Config CreateBull_Config()
        {
            return new SD_Bull_Config();
        }
        public static ID_Bull_Fertilizer CreateBull_Fertilizer()
        {
            return new SD_Bull_Fertilizer();
        }
        public static ID_Bull_Niu CreateBull_Niu()
        {
            return new SD_Bull_Niu();
        }
        public static ID_Bull_Scaninfo CreateBull_Scaninfo()
        {
            return new SD_Bull_Scaninfo();
        }
        public static ID_Bid CreateBid()
        {
            return new SD_Bid();
        }
        public static ID_Bider CreateBider()
        {
            return new SD_Bider();
        }
        public static ID_Article CreateArticle()
        {
            return new SD_Article();
        }
        public static ID_SettlementInfoList CreateSettlementInfoList()
        {
            return new SD_SettlementInfoList();
        }
        public static ID_BigLog CreateBigLog()
        {
            return new SD_BigLog();
        }
        public static ID_Room_Class CreateRoom_Class()
        {
            return new SD_Room_Class();
        }
        public static ID_Room_Decorative CreateRoom_Decorative()
        {
            return new SD_Room_Decorative();
        }
        public static ID_Room_Items CreateRoom_Items()
        {
            return new SD_Room_Items();
        }
        public static ID_Room_Object CreateRoom_Object()
        {
            return new SD_Room_Object();
        }
        public static ID_UserFave CreateUserFave()
        {
            return new SD_UserFave();
        }
        public static ID_PageSkins CreatePageSkins()
        {
            return new SD_PageSkins();
        }
        public static ID_Sns_Kiss CreateSns_Kiss()
        {
            return new SD_Sns_Kiss();
        }
        public static ID_CUser CreateCUser()
        {
            return new SD_CUser();
        }
        public static ID_Exam_Sys_Questions CreateQuestions()
        {
            return new SD_Exam_Sys_Questions();
        }
        public static ID_Questions CreateQuestions1()
        {
            return new SD_Questions();
        }
        public static ID_Exam_Class CreateQuestions_Class()
        {
            return new SD_Exam_Class();
        }
        public static ID_Questions_Class CreateQuestions_Class1()
        {
            return new SD_Questions_Class();
        }
        public static ID_Questions_User CreateQuestions_User()
        {
            return new SD_Questions_User();
        }

        public static ID_Search CreateSearch()
        {
            return new SD_Search();
        }

        public static ID_Exam_Type CreateQuestions_Type()
        {
            return new SD_Exam_Type();
        }

        public static ID_Questions_Type CreateQuestions_Type1()
        {
            return new SD_Questions_Type();
        }
        public static ID_Exam_Sys_Papers CreatePapers_System()
        {
            return new SD_Exam_Sys_Papers();
        }
        public static ID_Papers_System CreatePapers_System1()
        {
            return new SD_Papers_System();
        }
        public static ID_Papers_User CreatePapers_User()
        {
            return new SD_Papers_User();
        }

        public static ID_Questions_Knowledge CreateQuestion_Knowledge()
        {
            return new SD_Questions_Knowledge();
        }

        public static ID_IServer CreateIServer()
        {
            return new SD_IServer();
        }

        public static ID_IServerReply CreateIServerReply()
        {
            return new SD_IServerReply();
        }
        public static ID_CreateJS CreateCreateJS()
        {
            return new SD_CreateJS();
        }
        /// <summary>
        /// 过滤关键字
        /// </summary>
        /// <returns></returns>
        public static ID_Sensitivity CreateSensitivity()
        {
            return new SD_Sensitivity();
        }

        public static ID_UserShopClass CreateUserShopClass()
        {
            return new SD_UserShopClass();
        }

        public static ID_UserRegisterIP CreateUserRegisterIP()
        {
            return new SD_UserRegisterIP();
        }

        public static ID_Passenger CreatePassenger()
        {
            return new SD_Passenger();
        }

        public static ID_Arrive CreateArrive()
        {
            return new SD_Arrive();
        }
        public static ID_UserPromotions CreateUserPromotions()
        {
            return new SD_UserPromotions();
        }

        public static ID_WapForum_Class CreateWapForumClass()
        {
            return new SD_WapForum_Class();
        }

        public static ID_WapForum_Post CreateWapForumPost()
        {
            return new SD_WapForum_Post();
        }

        public static ID_WapForum_Replies CreateWapForumReplies()
        {
            return new SD_WapForum_Replies();
        }

        public static ID_WapNoverNVedio CreateWapNoverNVedio()
        {
            return new SD_WapNoverNVedio();
        }
        public static ID_MbClass CreateMbClass()
        {
            return new SD_MbClass();
        }
        public static ID_Mbtopic CreateMbtopic()
        {
            return new SD_Mbtopic();
        }
        public static ID_Microb CreateMicrob()
        {
            return new SD_Microb();
        }

        public static ID_School_Daren CreateSchoolDaren()
        {
            return new SD_School_Daren();
        }

        public static ID_BookRead CreateBookRead()
        {
            return new SD_BookRead();
        }

        public static ID_Shopsite CreateShopsite()
        {
            return new SD_Shopsite();
        }

        public static ID_RebateOrder CreateRebateOrder()
        {
            return new SD_RebateOrder();
        }

        public static ID_Page CreatePage()
        {
            return new SD_Page();
        }

        public static ID_Honor CreateHonor()
        {
            return new SD_Honor();
        }
        public static ID_Accountinfo CreateAccountinfo()
        {
            return new SD_Accountinfo();
        }

        public static ID_FTP CreateFTP()
        {
            return new SD_FTP();
        }

        public static ID_Redindulgence CreateRedindulgence()
        {
            return new SD_Redindulgence();
        }

        public static ID_GiftCard_shop CreateGiftCard_shop()
        {
            return new SD_GiftCard_shop();
        }

        public static ID_GiftCard_User CreateGiftCard_User()
        {
            return new SD_GiftCard_User();
        }

        public static ID_Keyword CreateKeyword()
        {
            return new SD_Keyword();
        }

        public static ID_RedEnvelope CreateRedEnvelope()
        {
            return new SD_RedEnvelope();
        }

        public static ID_PointRecord CreatePointRecord()
        {
            return new SD_PointRecord();
        }

        public static ID_InviteRecord CreateInviteRecord()
        {
            return new SD_InviteRecord();
        }

        public static ID_ShopsiteClass CreateShopsiteClass()
        {
            return new SD_ShopsiteClass();
        }

        public static ID_Promotion CreatePromotion()
        {
            return new SD_Promotion();
        }

        public static ID_Allianceinfo CreateAllianceinfo()
        {
            return new SD_Allianceinfo();
        }

        public static ID_PromoCount CreatePromoCount()
        {
            return new SD_PromoCount();
        }

        public static ID_OnlineCusServ CreateOnlineCusServ()
        {
            return new SD_OnlineCusServ();
        }
        public static ID_DPanoramic CreateDPanoramic()
        {
            return new SD_DPanoramic();
        }
        public static ID_DMusic CreateDMusic()
        {
            return new SD_DMusic();
        }

        public static ID_SitePicAdv CreateSitePicAdv()
        {
            return new SD_SitePicAdv();
        }

        public static ID_SiteTextAdv CreateSiteTextAdv()
        {
            return new SD_SiteTextAdv();
        }

        public static ID_Regsterapi CreateRegsterapi()
        {
            return new SD_Regsterapi();
        }

        public static ID_MbComment CreateMbComment()
        {
            return new SD_MbComment();
        }

        public static ID_HidTopic CreateHidTopic()
        {
            return new SD_HidTopic();
        }

        public static ID_MbTheme CreateMbTheme()
        {
            return new SD_MbTheme();
        }

        public static ID_PaiSecretary CreatePaiSecretary()
        {
            return new SD_PaiSecretary();
        }

        public static ID_Compete CreateCompete()
        {
            return new SD_Compete();
        }

        public static ID_CompleteHistory CreateCompleteHistory()
        {
            return new SD_CompleteHistory();
        }

        public static ID_CompSecretary CreateCompSecretary()
        {
            return new SD_CompSecretary();
        }

        public static ID_S_Flolar CreateS_Flolar()
        {
            return new SD_S_Flolar();
        }

        public static ID_S_FloClass CreateS_FloClass()
        {
            return new SD_S_FloClass();
        }

        public static ID_UserFloral CreateUserFloral()
        {
            return new SD_UserFloral();
        }

        public static ID_S_FloPack CreateS_FloPack()
        {
            return new SD_S_FloPack();
        }

        public static ID_S_FloGoods CreateS_FloGoods()
        {
            return new SD_S_FloGoods();
        }

        public static ID_Examination CreateExamination()
        {
            return new SD_Examination();
        }

        public static ID_ExAnswer CreateExAnswer()
        {
            return new SD_ExAnswer();
        }

        public static ID_ExAttendance CreateExAttendance()
        {
            return new SD_ExAttendance();
        }

        public static ID_ExClassgroup CreateExClassgroup()
        {
            return new SD_ExClassgroup();
        }

        public static ID_ExStudent CreateExStudent()
        {
            return new SD_ExStudent();
        }

        public static ID_ExStudytime CreateExStudytime()
        {
            return new SD_ExStudytime();
        }

        public static ID_BindFlolar CreateBindFlolar()
        {
            return new SD_BindFlolar();
        }
        public static ID_Exroom CreateExroom()
        {
            return new SD_Exroom();
        }

        public static ID_UserRecei CreateUserRecei()
        {
            return new SD_UserRecei();
        }
        public static ID_Package CreatePackage()
        {
            return new SD_Package();
        }
        public static ID_UserDay CreateUserDay()
        {
            return new SD_UserDay();
        }

        public static ID_OrderDelivery CreateOrderDelivery()
        {
            return new SD_OrderDelivery();
        }
        public static ID_WBUser CreateWBUser()
        {
            return new SD_WBUser();
        }

        public static ID_OrderBaseField CreateOrderBaseField()
        {
            return new SD_OrderBaseField();
        }

        public static ID_UserCaritHis CreateUserCaritHis()
        {
            return new SD_UserCaritHis();
        }

        public static ID_InvtoType CreateInvtoType()
        {
            return new SD_InvtoType();
        }

        public static ID_PointGrounp CreatePointGrounp()
        {
            return new SD_PointGrounp();
        }
        //团购
        public static ID_ZL_GroupBuy CreateZL_GroupBuy()
        {
            return new SD_ZL_GroupBuy();
        }

        public static ID_Course CreateCourse()
        {
            return new SD_Course();
        }


        public static ID_UserCourse CreateUserCourse()
        {
            return new SD_UserCourse();
        }

        public static ID_Courseware CreateCourseware()
        {
            return new SD_Courseware();
        }

        public static ID_ExLecturer CreateExLecturer()
        {
            return new SD_ExLecturer();
        }

        public static ID_ExTeacher CreateExTeacher()
        {
            return new SD_ExTeacher();
        }

        public static ID_ExamPoint CreateExamPoint()
        {
            return new SD_ExamPoint();
        }
        public static ID_Class CreateClass()
        {
            return new SD_Class();
        }

        public static ID_Paper_Questions CreatePaper_Questions()
        {
            return new SD_Paper_Questions();
        }

        public static ID_ScoreStatics CreateScoreStatics()
        {
            return new SD_ScoreStatics();
        }

        public static ID_GroupBuyList CreateGroupBuyList()
        {
            return new SD_GroupBuyList();
        }

        public static ID_Recruitment CreateRecruitment()
        {
            return new SD_Recruitment();
        }

        public static ID_EnrollList CreateEnrollList()
        {
            return new SD_EnrollList();
        }

        public static ID_VideoRoom CreateVideoRoom()
        {
            return new SD_VideoRoom();
        }

        public static ID_RoomCall CreateRoomCall()
        {
            return new SD_RoomCall();
        }
        public static ID_RoomUser CreateRoomUser()
        {
            return new SD_RoomUser();
        }
        public static ID_RoomUserGrade CreateRoomUserGrade()
        {
            return new SD_RoomUserGrade();
        }
        public static ID_VideoMessage CreateVideoMessage()
        {
            return new SD_VideoMessage();
        }
        public static ID_VideoHall CreateVideoHall()
        {
            return new SD_VideoHall();
        }
        public static ID_ArticlePromotion CreateArticlePromotion()
        {
            return new SD_ArticlePromotion();
        }
        public static ID_MtrlsMrktng CreateMtrlsMrktng()
        {
            return new SD_MtrlsMrktng();
        }

        public static ID_Rebates CreateRebates()
        {
            return new SD_Rebates();
        }
        public static ID_RoomInfo CreateRoomInfo()
        {
            return new SD_RoomInfo();
        }
        public static ID_VideoUser CreateVideoUser()
        {
            return new SD_VideoUser();
        }
        public static ID_UserGrade CreateUserGrade()
        {
            return new SD_UserGrade();
        }
        public static ID_Agent CreateAgent()
        {
            return new SD_Agent();
        }
        public static ID_Wiipointscard CreateWiipointscard()
        {
            return new SD_Wiipointscard();
        }
        public static ID_VideoUserFriend CreateVideoUserFriend()
        {
            return new SD_VideoUserFriend();
        }
        public static ID_VideoUserGroup CreateVideoUserGroup()
        {
            return new SD_VideoUserGroup();
        }
        public static ID_MobileSMS CreateMobileSMS()
        {
            return new SD_MobileSMS();
        }
        public static ID_Auction CreateAuction()
        {
            return new SD_Auction();
        }
        public static ID_Frient CreateFrient()
        {
            return new SD_Frient();
        }
        public static ID_LoginType CreateLoginType()
        {
            return new SD_LoginType();
        }
        public static ID_UserGroup CreateUserGroup()
        {
            return new SD_UserGroup();
        }
        public static ID_Chat CreateChat()
        {
            return new SD_Chat();
        }
        public static ID_Counter CreateCounter()
        {
            return new SD_Counter();
        }
        public static ID_UserRoom CreateUserRoom()
        {
            return new SD_UserRoom();
        }

        public static ID_ComClassManage CreateGetComManage()
        {
            return new SD_ComClassManage();
        }

        public static ID_CutModule CreateCutModule()
        {
            return new SD_CutModule();
        }

        public static ID_Bidding CreateBidding()
        {
            return new SD_Bidding();
        }
        public static ID_GetBiddingPrice CreateBiddingPrice()
        {
            return new SD_GetBiddingPrice();
        }
        public static ID_Hits CreateHits()
        {
            return new SD_Hits();
        }
        public static ID_Zone_Node CreateZoneNode()
        {
            return new SD_Zone_Node();
        }
        public static ID_Test test()
        {
            return new SD_Test();
        }
        public static ID_Zone_Site CreateZoneSite()
        {
            return new SD_Zone_Site();
        }
        public static ID_Zone_SheetStyle CreateSheetStyle()
        {
            return new SD_Zone_SheetStyle();
        }
        public static ID_Zone_Question CreateZoneQuestion()
        {
            return new SD_Zone_Question();
        }
        public static ID_SiteManager CreateSiteManager()
        {
            return new SD_SiteManager();
        }
        public static ID_Map CreateMap()
        {
            return new SD_Map();
        }
        public static ID_Zone_Count CreateZoneCount()
        {
            return new SD_Zone_Count();
        }
        public static ID_Adbuy CreateZladbuy()
        {
            return new SD_Adbuy();
        }
        public static ID_ViewHistory CreateViewHistory()
        {
            return new SD_ViewHistory();
        }
        public static ID_EditWord CreateEditWord()
        {
            return new SD_EditWord();
        }
        public static ID_IntoPage CreateZladInto()
        {
            return new SD_IntoPage();
        }
        public static ID_PageReg CreatePageReg()
        {
            return new SD_PageReg();
        }
        public static ID_Plan CreatePlan()
        {
            return new SD_Plan();
        }
    }
}