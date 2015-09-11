namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 节点信息
    /// </summary>
    public class M_Node
    {
        //节点ID
        private int m_NodeID;
        //节点名称
        private string m_NodeName;
        //节点类型。0为根节点，1为栏目，2为单个页面，3为外部链接
        private int m_NodeType;
        //节点提示，不支持HTML
        private string m_Tips;
        //节点标识，只能是英文字母和数字，并且要以字母开头。
        private string m_NodeDir;
        //栏目图片地址
        private string m_NodePicUrl;
        //外部链接
        private string m_NodeUrl;
        //栏目说明
        private string m_Description;
        //栏目META关键词
        private string m_Meta_Keywords;
        //针对搜索引擎的说明
        private string m_Meta_Description;
        //打开方式false原窗口,true新窗口
        private bool m_OpenType;
        //栏目权限。0--开放栏目  1--认证栏目
        private bool m_PurviewType;
        //评论权限0不允许 1允许
        private bool m_CommentType;
        //热点的点击数最小值
        private int m_HitsOfHot;
        //列表页模板
        private string m_ListTemplateFile;
        //栏目首页模板
        private string m_IndexTemplate;
        //选择的内容模型数组
        private string m_ContentModel;        
        //节点下项目的打开方式
        private bool m_ItemOpenType;
        //内容页的文件名规则
        private int m_ContentHtmlRule;
        //列表页的扩展名
        private int m_ListPageHtmlEx;
        //动态的内容页文件名
        private int m_ContentFileEx;
        //父节点ID
        private int m_ParentID;
        //排序ID
        private int m_OrderID;
        //子节点数
        private int m_Child;
        //节点深度
        private int m_Depth;
        //是否是空节点
        private bool m_IsNull;

        public M_Node()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public M_Node(bool flag)
        {
            this.m_IsNull = flag;
        }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.m_NodeID;
            }
            set
            {
                this.m_NodeID = value;
            }
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.m_NodeName;
            }
            set
            {
                this.m_NodeName = value;
            }
        }
        /// <summary>
        /// 节点类型。0为容器栏目，1为专题栏目，2为单个页面，3为外部链接
        /// </summary>
        public int NodeType
        {
            get
            {
                return this.m_NodeType;
            }
            set
            {
                this.m_NodeType = value;
            }
        }
        /// <summary>
        /// 节点提示，不支持HTML
        /// </summary>
        public string Tips
        {
            get
            {
                return this.m_Tips;
            }
            set
            {
                this.m_Tips = value;
            }
        }
        /// <summary>
        /// 节点目录，只能是英文字母和数字，并且要以字母开头
        /// </summary>
        public string NodeDir
        {
            get
            {
                return this.m_NodeDir;
            }
            set
            {
                this.m_NodeDir = value;
            }
        }
        public string NodeUrl
        {
            get { return this.m_NodeUrl; }
            set { this.m_NodeUrl = value; }
        }
        /// <summary>
        /// 栏目图片地址
        /// </summary>
        public string  NodePic
        {
            get
            {
                return this.m_NodePicUrl;
            }
            set
            {
                this.m_NodePicUrl = value;
            }
        }
        /// <summary>
        /// 栏目说明
        /// </summary>
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }
        /// <summary>
        /// 栏目META关键词
        /// </summary>
        public string Meta_Keywords
        {
            get
            {
                return this.m_Meta_Keywords;
            }
            set
            {
                this.m_Meta_Keywords = value;
            }
        }
        /// <summary>
        /// 针对搜索引擎的说明
        /// </summary>
        public string Meta_Description
        {
            get
            {
                return this.m_Meta_Description;
            }
            set
            {
                this.m_Meta_Description = value;
            }
        }
        /// <summary>
        /// 打开方式0原窗口
        /// </summary>
        public bool OpenNew
        {
            get
            {
                return this.m_OpenType;
            }
            set
            {
                this.m_OpenType = value;
            }
        }
        /// <summary>
        /// 栏目权限。0--开放栏目  1--认证栏目
        /// </summary>
        public bool PurviewType
        {
            get
            {
                return this.m_PurviewType;
            }
            set
            {
                this.m_PurviewType = value;
            }
        }
        /// <summary>
        /// 评论权限0不允许，1允许
        /// </summary>
        public bool CommentType
        {
            get
            {
                return this.m_CommentType;
            }
            set
            {
                this.m_CommentType = value;
            }
        }
        /// <summary>
        /// 热点的点击数最小值
        /// </summary>
        public int HitsOfHot
        {
            get
            {
                return this.m_HitsOfHot;
            }
            set
            {
                this.m_HitsOfHot = value;
            }
        }
        /// <summary>
        /// 列表页模板
        /// </summary>
        public string ListTemplateFile
        {
            get
            {
                return this.m_ListTemplateFile;
            }
            set
            {
                this.m_ListTemplateFile = value;
            }
        }
        /// <summary>
        /// 栏目首页模板
        /// </summary>
        public string IndexTemplate
        {
            get
            {
                return this.m_IndexTemplate;
            }
            set
            {
                this.m_IndexTemplate = value;
            }
        }
        /// <summary>
        /// 选择的内容模型数组
        /// </summary>
        public string ContentModel
        {
            get
            {
                return this.m_ContentModel;
            }
            set
            {
                this.m_ContentModel = value;
            }
        }
        /// <summary>
        /// 节点下项目的打开方式
        /// </summary>
        public bool ItemOpenType
        {
            get
            {
                return this.m_ItemOpenType;
            }
            set
            {
                this.m_ItemOpenType = value;
            }
        }
        /// <summary>
        /// 内容页的文件名规则
        /// </summary>
        public int ContentPageHtmlRule
        {
            get
            {
                return this.m_ContentHtmlRule;
            }
            set
            {
                this.m_ContentHtmlRule = value;
            }
        }
        /// <summary>
        /// 列表首页的文件扩展名
        /// </summary>
        public int ListPageHtmlEx
        {
            get
            {
                return this.m_ListPageHtmlEx;
            }
            set
            {
                this.m_ListPageHtmlEx = value;
            }
        }
        /// <summary>
        /// 内容页文件扩展名
        /// </summary>
        public int ContentFileEx
        {
            get
            {
                return this.m_ContentFileEx;
            }
            set
            {
                this.m_ContentFileEx = value;
            }
        }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentID
        {
            get 
            {
                return this.m_ParentID;
            }
            set
            {
                this.m_ParentID = value;
            }
        }
        /// <summary>
        /// 子节点数
        /// </summary>
        public int Child
        {
            get 
            {
                return this.m_Child;
            }
            set
            {
                this.m_Child = value;
            }
        }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int OrderID
        {
            get { return this.m_OrderID; }
            set { this.m_OrderID = value; }
        }
        /// <summary>
        /// 节点深度
        /// </summary>
        public int Depth
        {
            get { return this.m_Depth; }
            set { this.m_Depth = value; }
        }
        /// <summary>
        /// 是否空节点
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}