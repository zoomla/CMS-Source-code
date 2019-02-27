using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Product:M_Base
    {
        public int ExpRemind { get; set; }
        /// <summary>
        /// 余额,银币,点数售价,前端标签取出后,转JSON判断输出
        /// </summary>
        public string LinPrice_Json { get; set; }
        /// <summary>
        /// 所属于哪个商品
        /// </summary>
        public int ParentID { get; set; }
        #region 属性定义
        public int ID
        {
            get;
            set;
        }
        public int OrderID
        {
            get;
            set;
        }
        /// <summary>
        /// [未用][备用字段]
        /// </summary>
        public int Class
        {
            get;
            set;
        }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int Nodeid
        {
            get;
            set;
        }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModelID
        {
            get;
            set;
        }
        /// <summary>
        /// 所属专题ID
        /// </summary>
        public int Categoryid
        {
            get;
            set;
        }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProCode
        {
            get;
            set;
        }
        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode
        {
            get;
            set;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Proname
        {
            get;
            set;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Kayword
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string ProUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 重量
        /// </summary>
        public int Weight
        {
            get;
            set;
        }
        /// <summary>
        /// 服务期限 [disuse]
        /// </summary>
        public int ServerPeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 期限日期格式[3-年2-月1-日0-无限期],页面显示用GetServerPeriod [disuse]
        /// </summary>
        public int ServerType
        {
            get;
            set;
        }
        /// <summary>
        ///商品类型[1:正常|店铺,2:特价,3:积分,4:团购,5:云购,6:IDC,7:旅游,8:酒店,9:机票]
        /// </summary>
        public int ProClass
        {
            get;
            set;
        }
        /// <summary>
        /// ZC:正常,TJ:特价,JF:积分,TG:团购,YG:云购,FW:服务
        /// </summary>
        public enum ClassType
        {
            ZC = 1, TJ = 2, JF = 3, TG = 4, YG = 5, IDC = 6, LY = 7, JD = 8, JP = 9
        }
        /// <summary>
        /// 商品属性
        /// </summary>
        public int Properties
        {
            get;
            set;
        }
        /// <summary>
        /// 销售操作[1-允许销售,2-不允许]
        /// </summary>
        public int Sales
        {
            get;
            set;
        }
        /// <summary>
        /// 商品简介
        /// </summary>
        public string Proinfo
        {
            get;
            set;
        }
        /// <summary>
        /// 商品详细介绍
        /// </summary>
        public string Procontent
        {
            get;
            set;
        }
        /// <summary>
        /// 商品清晰图
        /// </summary>
        public string Clearimg
        {
            get;
            set;
        }
        /// <summary>
        /// 商品缩略图
        /// </summary>
        public string Thumbnails
        {
            get;
            set;
        }
        private string m_thumbnails = "";
        /// <summary>
        /// 缩略图路径,对m_thumbnails封装
        /// </summary>
        public string ThumbPath
        {
            get
            {
                if (!string.IsNullOrEmpty(m_thumbnails))
                    m_thumbnails = "/" + m_thumbnails;
                else { m_thumbnails = "/UploadFiles/nopic.gif"; }
                return m_thumbnails;
            }
        }
        /// <summary>
        /// 厂商
        /// </summary>
        public string Producer
        {
            get;
            set;
        }
        /// <summary>
        /// 商标
        /// </summary>
        public string Brand
        {
            get;
            set;
        }
        /// <summary>
        /// 1-缺货允许购买，0-不允许购买
        /// </summary>
        public int Allowed
        {
            get;
            set;
        }
        /// <summary>
        /// 限制购买数量[购买上限]-1不限制
        /// </summary>
        public int Quota
        {
            get;
            set;
        }
        /// <summary>
        /// 最低购买数量[购买下限]-1不限制
        /// </summary>
        public int DownQuota
        {
            get;
            set;
        }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Stock
        {
            get;
            set;
        }
        /// <summary>
        /// 最低库存数量[报警]
        /// </summary>
        public int StockDown
        {
            get;
            set;
        }
        /// <summary>
        /// 0-实际库存，1-虚拟库存(含订购)
        /// </summary>
        public int JisuanFs
        {
            get;
            set;
        }
        /// <summary>
        /// 税率设置,百分数%
        /// </summary>
        public double Rate
        {
            get;
            set;
        }
        /// <summary>
        /// 税率设置
        /// </summary>
        public int Rateset
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐等级
        /// </summary>
        public int Dengji
        {
            get;
            set;
        }
        /// <summary>
        /// 市场价
        /// </summary>
        public double ShiPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 零售价
        /// </summary>
        public double LinPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 会员价
        /// </summary>
        public string MemberPrice
        {
            get;
            set;
        }
        /// <summary>
        /// [disuse]
        /// </summary>
        public string ActPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许批发 1:是
        /// </summary>
        public int Wholesales
        {
            get;
            set;
        }
        /// <summary>
        /// 零售
        /// </summary>
        public int Wholesaleone
        {
            get;
            set;
        }
        /// <summary>
        /// 多价格信息
        /// </summary>
        public string Wholesalesinfo
        {
            get;
            set;
        }
        /// <summary>
        /// 促销方案
        /// </summary>
        public string Preset
        {
            get;
            set;
        }
        /// <summary>
        /// 购物积分
        /// </summary>
        public int Integral
        {
            get;
            set;
        }
        /// <summary>
        /// 自选购买数量
        /// </summary>
        public int Propeid
        {
            get;
            set;
        }
        /// <summary>
        /// 优惠率%数,不允许负数,不允许超过100,优惠5%,则减价5%
        /// </summary>
        public int Recommend
        {
            get;
            set;
        }
        /// <summary>
        /// 绑定销售(1=绑定 0=不绑定)
        /// </summary>
        public int Priority
        {
            get;
            set;
        }
        /// <summary>
        /// [未用][备用字段]
        /// </summary>
        public int MonthClickNum
        {
            get;
            set;
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 更新时间类型
        /// </summary>
        public int UpdateType
        {
            get;
            set;
        }
        /// <summary>
        /// 模型模板
        /// </summary>
        public string ModeTemplate
        {
            get;
            set;
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string AddUser
        {
            get;
            set;
        }
        /// <summary>
        /// [未用][备用字段](是否下架)
        /// </summary>
        public int DownCar
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get;
            set;
        }
        /// <summary>
        /// 模型表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
        /// <summary>
        /// 1:已审核,0:未审核
        /// </summary>
        public int Istrue
        {
            get;
            set;
        }
        /// <summary>
        /// [未用][备用字段]
        /// </summary>
        public int Isgood
        {
            get;
            set;
        }
        /// <summary>
        /// [未用][备用字段]
        /// </summary>
        public int MakeHtml
        {
            get;
            set;
        }
        /// <summary>
        ///购买商品人数
        /// </summary>
        public int Sold
        {
            get;
            set;
        }
        /// <summary>
        /// 新品
        /// </summary>
        public int Isnew
        {
            get;
            set;
        }
        /// <summary>
        /// 热销
        /// </summary>
        public int Ishot
        {
            get;
            set;
        }
        /// <summary>
        /// 精品
        /// </summary>
        public int Isbest
        {
            get;
            set;
        }
        /// <summary>
        /// 从表ID
        /// </summary>
        public int ItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int ComModelID
        {
            get; set;
        }
        /// <summary>
        /// 点击数
        /// </summary>
        public int AllClickNum
        {
            get;
            set;
        }
        /// <summary>
        /// 设为赠品(1-为赠品 0-非赠品)
        /// </summary>
        public int Largess
        {
            get;
            set;
        }
        /// <summary>
        /// 赠品价格
        /// </summary>
        public double Largesspirx
        {
            get;
            set;
        }
        /// <summary>
        /// 促销方案
        /// </summary>
        public int ProjectType
        {
            get;
            set;
        }
        /// <summary>
        /// 促销方案涉及到数字的字段
        /// </summary>
        public int ProjectPronum
        {
            get;
            set;
        }
        /// <summary>
        /// 方案涉及金钱的字段
        /// </summary>
        public double ProjectMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 购物数量送积分
        /// </summary>
        public int IntegralNum
        {
            get;
            set;
        }
        /// <summary>
        /// 赠品名称
        /// </summary>
        public string PesentNames
        {
            get;
            set;
        }
        /// <summary>
        /// 赠品ID
        /// </summary>
        public int PesentNameid
        {
            get;
            set;
        }
        /// <summary>
        /// 备用字段
        /// </summary>
        public int GuessType
        {
            get;
            set;
        }
        /// <summary>
        /// 商品属性,存储是否返修,竞猜,秒杀等字段等,repair,return
        /// </summary>
        public string GuessXML
        {
            get;
            set;
        }
        /// <summary>
        /// 第一级节点
        /// </summary>
        public int FirstNodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 赠送积分
        /// </summary>
        public int PointVal
        {
            get;
            set;
        }
        /// <summary>
        /// 是否设为标准:0为否,1为是
        /// </summary>
        public int isStand
        {
            get;
            set;
        }
        /// <summary>
        /// 店铺ID,StoreID
        /// </summary>
        public int UserShopID
        {
            get;
            set;
        }
        /// <summary>
        /// 预订价
        /// </summary>
        public double BookPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 比节日时间提前多少天为预订价
        /// </summary>
        public int bookDay
        {
            get;
            set;
        }
        /// <summary>
        /// 计价类型：0:不使用,1:为会员价,2:按会员组定价
        /// 后期扩展支持批发价等
        /// </summary>
        public int UserType
        {
            get;
            set;
        }
        /// <summary>
        /// 如果按会员组打折,格式为:会员组ID|价格
        /// </summary>
        public string UserPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 节日价格
        /// </summary>
        public double FestlPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 节日期间:某段时间内为节日价，格式开始时间|结束时间
        /// </summary>
        public string FestPeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 团购时间：开始时间|结束时间
        /// </summary>
        public string ColonelTime
        {
            get;
            set;
        }
        /// <summary>
        /// 团购订金
        /// </summary>
        public double ColoneDeposit
        {
            get;
            set;
        }
        /// <summary>
        /// 是否进入回收站
        /// </summary>
        public bool Recycler
        {
            get;
            set;
        }
        /// <summary>
        /// Disuse
        /// </summary>
        public string ProSeller
        {
            get;
            set;
        }
        public int UserID { get; set; }
        ///<summary>
        ///运费模板ID
        /// </summary>
        public string FarePrice
        { get; set; }
        //捆绑商品的IDS
        public string BindIDS { get; set; }
        /// <summary>
        /// IDC商品价格
        /// </summary>
        public string IDCPrice { get; set; }
        #endregion
        public M_Product()
        {
            AddTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            Recycler = false;
            ProUnit = "件";
            Quota = -1;
            DownQuota = -1;
            Dengji = 3;//默认三星推荐
            Sales = 1;
        }
        public override string TbName { get { return "ZL_Commodities"; } }
        public override string[,] FieldList() 
        {
            return M_Product.GetFieldList();
        }
        public static string[,] GetFieldList()
        {
        string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"Class","Int","4"},
                                {"Nodeid","Int","4"},
                                {"ModelID","Int","4"},
                                {"Categoryid","Int","4"},
                                {"ProCode","NVarChar","50"},
                                {"BarCode","NVarChar","50"},
                                {"Proname","NVarChar","500"},
                                {"Kayword","NVarChar","255"},
                                {"ProUnit","NVarChar","50"},
                                {"Weight","Int","4"},
                                {"ServerPeriod","Int","4"},
                                {"ServerType","Int","4"},
                                {"ProClass","Int","4"},
                                {"Properties","Int","4"},
                                {"Sales","Int","4"},
                                {"Proinfo","NVarChar","4000"},
                                {"Procontent","Text","10000"},
                                {"Clearimg","NVarChar","4000"},
                                {"Thumbnails","NVarChar","4000"},
                                {"Producer","NVarChar","4000"},
                                {"Brand","NVarChar","4000"},
                                {"Allowed","Int","4"},
                                {"Quota","Int","4"},
                                {"DownQuota","Int","4"},
                                {"Stock","Int","4"},
                                {"StockDown","Int","4"},
                                {"JisuanFs","Int","4"},
                                {"Rate","Money","32"},
                                {"Rateset","Int","4"},
                                {"Dengji","Int","4"},
                                {"ShiPrice","Money","32"},
                                {"LinPrice","Money","32"}, 
                                {"Wholesales","Int","4"},
                                {"Wholesaleone","Int","4"},
                                {"Preset","NVarChar","4000"},
                                {"Integral","Int","4"},
                                {"Propeid","Int","4"},
                                {"Recommend","Int","4"},
                                {"Priority","Int","4"}, 
                                {"AllClickNum","Int","4"},
                                {"UpdateTime","DateTime","8"}, 
                                {"ModeTemplate","NVarChar","255"},
                                {"AddUser","NVarChar","255"},
                                {"DownCar","Int","4"},
                                {"AddTime","DateTime","8"}, 
                                {"TableName","NVarChar","50"},
                                {"Istrue","Int","4"},
                                {"Isgood","Int","4"},
                                {"MakeHtml","Int","4"},
                                {"Sold","Int","4"},
                                {"isnew","Int","4"},
                                {"ishot","Int","4"},
                                {"isbest","Int","4"}, 
                                {"Wholesalesinfo","NVarChar","4000"},
                                {"ItemID","Int","4"},
                                {"ComModelID","Int","4"},
                                {"Largess","Int","4"},
                                {"Largesspirx","Money","32"},
                                {"ProjectType","Int","4"},
                                {"ProjectPronum","Int","4"},
                                {"ProjectMoney","Money","32"},
                                {"IntegralNum","Int","4"},
                                {"PesentNames","NVarChar","4000"},
                                {"PesentNameid","Int","4"}, 
                                {"GuessType","Int","4"},
                                {"GuessXML","NVarChar","1000"},
                                {"FirstNodeID","Int","4"},
                                {"PointVal","Int","4"},  
                                {"BookPrice","Money","32"}, 
                                {"bookDay","Int","4"},
                                {"UserType","Int","4"},
                                {"UserPrice","NVarChar","1000"},
                                {"FarePrice","NVarChar","400"},
                                {"ExpRemind","Int","4"},
                                {"LinPrice_Json","NVarChar","500"},
                                {"ParentID","Int","4"},
                                {"UserID","Int","4"},
                                {"UserShopID","Int","4"},
                                {"BindIDS","VarChar","4000"},
                                {"OrderID","Int","4"},
                                {"IDCPrice","NText","20000"},
                                {"Recycler","Int","4"},

                                };
            return Tablelist; 
             
        }
        public M_Product GetModelFromReader(SqlDataReader rdr)
        {
            M_Product model = new M_Product();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Class = ConvertToInt(rdr["Class"]);
            model.Nodeid = ConvertToInt(rdr["Nodeid"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.Categoryid = ConvertToInt(rdr["Categoryid"]);
            model.AllClickNum = ConvertToInt(rdr["AllClickNum"]);
            model.ProCode = ConverToStr(rdr["ProCode"]);
            model.BarCode = ConverToStr(rdr["BarCode"]);
            model.Proname = ConverToStr(rdr["Proname"]);
            model.Kayword = ConverToStr(rdr["Kayword"]);
            model.ProUnit = ConverToStr(rdr["ProUnit"]);
            model.Weight = ConvertToInt(rdr["Weight"]);
            model.ServerPeriod = ConvertToInt(rdr["ServerPeriod"]);
            model.ServerType = ConvertToInt(rdr["ServerType"]);
            model.ProClass = ConvertToInt(rdr["ProClass"]);
            model.Properties = ConvertToInt(rdr["Properties"]);
            model.Sales = ConvertToInt(rdr["Sales"]);
            model.Proinfo = ConverToStr(rdr["Proinfo"]);
            model.Procontent = ConverToStr(rdr["Procontent"]);
            model.Clearimg = ConverToStr(rdr["Clearimg"]);
            model.Thumbnails = ConverToStr(rdr["Thumbnails"]);
            model.Producer = ConverToStr(rdr["Producer"]);
            model.Brand = ConverToStr(rdr["Brand"]);
            model.Allowed = ConvertToInt(rdr["Allowed"]);
            model.Quota = ConvertToInt(rdr["Quota"]);
            model.DownQuota = ConvertToInt(rdr["DownQuota"]);
            model.Stock = ConvertToInt(rdr["Stock"]);
            model.StockDown = ConvertToInt(rdr["StockDown"]);
            model.JisuanFs = ConvertToInt(rdr["JisuanFs"]);
            model.Rate = ConverToDouble(rdr["Rate"]);
            model.Rateset = ConvertToInt(rdr["Rateset"]);
            model.Dengji = ConvertToInt(rdr["Dengji"]);
            model.ShiPrice = ConverToDouble(rdr["ShiPrice"]);
            model.LinPrice = ConverToDouble(rdr["LinPrice"]);
            model.Wholesales = ConvertToInt(rdr["Wholesales"]);
            model.Wholesaleone = ConvertToInt(rdr["Wholesaleone"]);
            model.Preset = ConverToStr(rdr["Preset"]);
            model.Integral = ConvertToInt(rdr["Integral"]);
            model.Propeid = ConvertToInt(rdr["Propeid"]);
            model.Recommend = ConvertToInt(rdr["Recommend"]);
            model.Priority = ConvertToInt(rdr["Priority"]);
            model.AllClickNum = ConvertToInt(rdr["AllClickNum"]);
            model.UpdateTime = ConvertToDate(rdr["UpdateTime"]);
            model.ModeTemplate = ConverToStr(rdr["ModeTemplate"]);
            model.AddUser = ConverToStr(rdr["AddUser"]);
            model.DownCar = ConvertToInt(rdr["DownCar"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.TableName = ConverToStr(rdr["TableName"]);
            model.Istrue = ConvertToInt(rdr["Istrue"]);
            model.Isgood = ConvertToInt(rdr["Isgood"]);
            model.MakeHtml = ConvertToInt(rdr["MakeHtml"]);
            model.Sold = ConvertToInt(rdr["Sold"]);
            model.Isnew = ConvertToInt(rdr["isnew"]);
            model.Ishot = ConvertToInt(rdr["ishot"]);
            model.Isbest = ConvertToInt(rdr["isbest"]);
            model.Wholesalesinfo = ConverToStr(rdr["Wholesalesinfo"]);
            model.ItemID = ConvertToInt(rdr["ItemID"]);
            model.ComModelID = ConvertToInt(rdr["ComModelID"]);
            model.Largess = ConvertToInt(rdr["Largess"]);
            model.Largesspirx = ConverToDouble(rdr["Largesspirx"]);
            model.ProjectType = ConvertToInt(rdr["ProjectType"]);
            model.ProjectPronum = ConvertToInt(rdr["ProjectPronum"]);
            model.ProjectMoney = ConverToDouble(rdr["ProjectMoney"]);
            model.IntegralNum = ConvertToInt(rdr["IntegralNum"]);
            model.PesentNames = ConverToStr(rdr["PesentNames"]);
            model.PesentNameid = ConvertToInt(rdr["PesentNameid"]);
            model.GuessType = ConvertToInt(rdr["GuessType"]);
            model.GuessXML = ConverToStr(rdr["GuessXML"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.FirstNodeID = ConvertToInt(rdr["FirstNodeID"]);
            model.PointVal = ConvertToInt(rdr["PointVal"]);
            model.isStand = ConvertToInt(rdr["isStand"]);
            model.UserShopID = ConvertToInt(rdr["UserShopID"]);
            model.BookPrice = ConverToDouble(rdr["BookPrice"]);
            model.bookDay = ConvertToInt(rdr["bookDay"]);
            model.UserType = ConvertToInt(rdr["UserType"]);
            model.UserPrice = ConverToStr(rdr["UserPrice"]);
            model.Recycler = ConverToBool(rdr["Recycler"]);
            model.FarePrice = ConverToStr(rdr["FarePrice"]);
            model.ExpRemind = ConvertToInt(rdr["ExpRemind"]);
            model.LinPrice_Json = ConverToStr(rdr["LinPrice_Json"]);
            model.ParentID = ConvertToInt(rdr["ParentID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.BindIDS = ConverToStr(rdr["BindIDS"]);
            model.IDCPrice = ConverToStr(rdr["IDCPrice"]);
            rdr.Close();
            return model;
        }
        public M_Product GetModelFromReader(DataRow rdr)
        {
            M_Product model = new M_Product();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Class = ConvertToInt(rdr["Class"]);
            model.Nodeid = ConvertToInt(rdr["Nodeid"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.Categoryid = ConvertToInt(rdr["Categoryid"]);
            model.AllClickNum = ConvertToInt(rdr["AllClickNum"]);
            model.ProCode = ConverToStr(rdr["ProCode"]);
            model.BarCode = ConverToStr(rdr["BarCode"]);
            model.Proname = ConverToStr(rdr["Proname"]);
            model.Kayword = ConverToStr(rdr["Kayword"]);
            model.ProUnit = ConverToStr(rdr["ProUnit"]);
            model.Weight = ConvertToInt(rdr["Weight"]);
            model.ServerPeriod = ConvertToInt(rdr["ServerPeriod"]);
            model.ServerType = ConvertToInt(rdr["ServerType"]);
            model.ProClass = ConvertToInt(rdr["ProClass"]);
            model.Properties = ConvertToInt(rdr["Properties"]);
            model.Sales = ConvertToInt(rdr["Sales"]);
            model.Proinfo = ConverToStr(rdr["Proinfo"]);
            model.Procontent = ConverToStr(rdr["Procontent"]);
            model.Clearimg = ConverToStr(rdr["Clearimg"]);
            model.Thumbnails = ConverToStr(rdr["Thumbnails"]);
            model.Producer = ConverToStr(rdr["Producer"]);
            model.Brand = ConverToStr(rdr["Brand"]);
            model.Allowed = ConvertToInt(rdr["Allowed"]);
            model.Quota = ConvertToInt(rdr["Quota"]);
            model.DownQuota = ConvertToInt(rdr["DownQuota"]);
            model.Stock = ConvertToInt(rdr["Stock"]);
            model.StockDown = ConvertToInt(rdr["StockDown"]);
            model.JisuanFs = ConvertToInt(rdr["JisuanFs"]);
            model.Rate = ConverToDouble(rdr["Rate"]);
            model.Rateset = ConvertToInt(rdr["Rateset"]);
            model.Dengji = ConvertToInt(rdr["Dengji"]);
            model.ShiPrice = ConverToDouble(rdr["ShiPrice"]);
            model.LinPrice = ConverToDouble(rdr["LinPrice"]);
            model.Wholesales = ConvertToInt(rdr["Wholesales"]);
            model.Wholesaleone = ConvertToInt(rdr["Wholesaleone"]);
            model.Preset = ConverToStr(rdr["Preset"]);
            model.Integral = ConvertToInt(rdr["Integral"]);
            model.Propeid = ConvertToInt(rdr["Propeid"]);
            model.Recommend = ConvertToInt(rdr["Recommend"]);
            model.Priority = ConvertToInt(rdr["Priority"]);
            model.AllClickNum = ConvertToInt(rdr["AllClickNum"]);
            model.UpdateTime = ConvertToDate(rdr["UpdateTime"]);
            model.ModeTemplate = ConverToStr(rdr["ModeTemplate"]);
            model.AddUser = ConverToStr(rdr["AddUser"]);
            model.DownCar = ConvertToInt(rdr["DownCar"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.TableName = ConverToStr(rdr["TableName"]);
            model.Istrue = ConvertToInt(rdr["Istrue"]);
            model.Isgood = ConvertToInt(rdr["Isgood"]);
            model.MakeHtml = ConvertToInt(rdr["MakeHtml"]);
            model.Sold = ConvertToInt(rdr["Sold"]);
            model.Isnew = ConvertToInt(rdr["isnew"]);
            model.Ishot = ConvertToInt(rdr["ishot"]);
            model.Isbest = ConvertToInt(rdr["isbest"]);
            model.Wholesalesinfo = ConverToStr(rdr["Wholesalesinfo"]);
            model.ItemID = ConvertToInt(rdr["ItemID"]);
            model.ComModelID = ConvertToInt(rdr["ComModelID"]);
            model.Largess = ConvertToInt(rdr["Largess"]);
            model.Largesspirx = ConverToDouble(rdr["Largesspirx"]);
            model.ProjectType = ConvertToInt(rdr["ProjectType"]);
            model.ProjectPronum = ConvertToInt(rdr["ProjectPronum"]);
            model.ProjectMoney = ConverToDouble(rdr["ProjectMoney"]);
            model.IntegralNum = ConvertToInt(rdr["IntegralNum"]);
            model.PesentNames = ConverToStr(rdr["PesentNames"]);
            model.PesentNameid = ConvertToInt(rdr["PesentNameid"]);
            model.GuessType = ConvertToInt(rdr["GuessType"]);
            model.GuessXML = ConverToStr(rdr["GuessXML"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.FirstNodeID = ConvertToInt(rdr["FirstNodeID"]);
            model.PointVal = ConvertToInt(rdr["PointVal"]);
            model.isStand = ConvertToInt(rdr["isStand"]);
            model.UserShopID = ConvertToInt(rdr["UserShopID"]);
            model.BookPrice = ConverToDouble(rdr["BookPrice"]);
            model.bookDay = ConvertToInt(rdr["bookDay"]);
            model.UserType = ConvertToInt(rdr["UserType"]);
            model.UserPrice = ConverToStr(rdr["UserPrice"]);
            model.Recycler = ConverToBool(rdr["Recycler"]);
            model.FarePrice = ConverToStr(rdr["FarePrice"]);
            model.ExpRemind = ConvertToInt(rdr["ExpRemind"]);
            model.LinPrice_Json = ConverToStr(rdr["LinPrice_Json"]);
            model.ParentID = ConvertToInt(rdr["ParentID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.BindIDS = ConverToStr(rdr["BindIDS"]);
            model.IDCPrice = ConverToStr(rdr["IDCPrice"]);
            return model;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Product model = this;
            if (model.AddTime <= DateTime.MinValue) { model.AddTime = DateTime.Now; }
            if (model.UpdateTime <= DateTime.MinValue) { model.UpdateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Class;
            sp[2].Value = model.Nodeid;
            sp[3].Value = model.ModelID;
            sp[4].Value = model.Categoryid;
            sp[5].Value = model.ProCode;
            sp[6].Value = model.BarCode;
            sp[7].Value = model.Proname;
            sp[8].Value = model.Kayword;
            sp[9].Value = model.ProUnit;
            sp[10].Value = model.Weight;
            sp[11].Value = model.ServerPeriod;
            sp[12].Value = model.ServerType;
            sp[13].Value = model.ProClass;
            sp[14].Value = model.Properties;
            sp[15].Value = model.Sales;
            sp[16].Value = model.Proinfo;
            sp[17].Value = model.Procontent;
            sp[18].Value = model.Clearimg;
            sp[19].Value = model.Thumbnails;
            sp[20].Value = model.Producer;
            sp[21].Value = model.Brand;
            sp[22].Value = model.Allowed;
            sp[23].Value = model.Quota;
            sp[24].Value = model.DownQuota;
            sp[25].Value = model.Stock;
            sp[26].Value = model.StockDown;
            sp[27].Value = model.JisuanFs;
            sp[28].Value = model.Rate;
            sp[29].Value = model.Rateset;
            sp[30].Value = model.Dengji;
            sp[31].Value = model.ShiPrice;
            sp[32].Value = model.LinPrice;
            sp[33].Value = model.Wholesales;
            sp[34].Value = model.Wholesaleone;
            sp[35].Value = model.Preset;
            sp[36].Value = model.Integral;
            sp[37].Value = model.Propeid;
            sp[38].Value = model.Recommend;
            sp[39].Value = model.Priority;
            sp[40].Value = model.AllClickNum;
            sp[41].Value = model.UpdateTime;
            sp[42].Value = model.ModeTemplate;
            sp[43].Value = model.AddUser;
            sp[44].Value = model.DownCar;
            sp[45].Value = model.AddTime;
            sp[46].Value = model.TableName;
            sp[47].Value = model.Istrue;
            sp[48].Value = model.Isgood;
            sp[49].Value = model.MakeHtml;
            sp[50].Value = model.Sold;
            sp[51].Value = model.Isnew;
            sp[52].Value = model.Ishot;
            sp[53].Value = model.Isbest;
            sp[54].Value = model.Wholesalesinfo;
            sp[55].Value = model.ItemID;
            sp[56].Value = model.ComModelID;
            sp[57].Value = model.Largess;
            sp[58].Value = model.Largesspirx;
            sp[59].Value = model.ProjectType;
            sp[60].Value = model.ProjectPronum;
            sp[61].Value = model.ProjectMoney;
            sp[62].Value = model.IntegralNum;
            sp[63].Value = model.PesentNames;
            sp[64].Value = model.PesentNameid;
            sp[65].Value = model.GuessType;
            sp[66].Value = model.GuessXML;
            sp[67].Value = model.FirstNodeID;
            sp[68].Value = model.PointVal;
            sp[69].Value = model.BookPrice;
            sp[70].Value = model.bookDay;
            sp[71].Value = model.UserType;
            sp[72].Value = model.UserPrice;
            sp[73].Value = model.FarePrice;
            sp[74].Value = model.ExpRemind;
            sp[75].Value = model.LinPrice_Json;
            sp[76].Value = model.ParentID;
            sp[77].Value = model.UserID;
            sp[78].Value = model.UserShopID;
            sp[79].Value = model.BindIDS;
            sp[80].Value = model.OrderID;
            sp[81].Value = model.IDCPrice;
            sp[82].Value = model.Recycler;
            return sp;
        }
    }
    [Serializable]
    public class M_LinPrice//附加金额售价
    {
        public double Purse { get; set; }
        public double Sicon { get; set; }
        public double Point { get; set; }
    }
}