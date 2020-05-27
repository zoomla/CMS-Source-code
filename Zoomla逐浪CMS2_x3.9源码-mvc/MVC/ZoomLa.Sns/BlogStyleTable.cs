/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ZL_Sns_BlogStyleTable.cs
// 文件功能描述：定义数据表ZL_Sns_BlogStyleTable的业务实体
//
// 创建标识：Owner(2008-12-9) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_BlogStyleTable业务实体
    /// </summary>
    [Serializable]
    public class BlogStyleTable
    {
		#region 字段定义
		
		///<summary>
		///编号
		///</summary>
		private int iD;
		
		///<summary>
		///模板名
		///</summary>
		private string styleName = String.Empty;
		
		///<summary>
		///作者
		///</summary>
		private string author = String.Empty;
		
		///<summary>
		///缩略图
		///</summary>
		private string stylePic = String.Empty;
		
		///<summary>
		///添加时间
		///</summary>
		private DateTime addtime;
		
		///<summary>
		///模板状态(0为停用 1为启用)
		///</summary>
		private int styleState;
		
		///<summary>
		///用户空间首页模板
		///</summary>
		private string userIndexStyle = String.Empty;
		
		///<summary>
		///日志列表模板
		///</summary>
		private string logStyle = String.Empty;
		
		///<summary>
		///日志内容页模板
		///</summary>
		private string logShowStyle = String.Empty;
		
		///<summary>
		///相册列表模板
		///</summary>
		private string photoStyle = String.Empty;
		
		///<summary>
		///相册内容页模板
		///</summary>
		private string photoShowStyle = String.Empty;
		
		///<summary>
		///相片内容页模板
		///</summary>
		private string picShowStyle = String.Empty;
		
		///<summary>
		///群族列表模板
		///</summary>
		private string groupStyle = String.Empty;
		
		///<summary>
		///群族内容页模板
		///</summary>
		private string groupShowStyle = String.Empty;
		
		///<summary>
		///话题内容页模板
		///</summary>
		private string groupTopicShow = String.Empty;
		
		
		#endregion
		
		#region 构造函数
		
		///<summary>
		///
		///</summary>
		public BlogStyleTable()
		{
		}
		
		///<summary>
		///
		///</summary>
		public BlogStyleTable
		(
			int iD,
			string styleName,
			string author,
			string stylePic,
			DateTime addtime,
			int styleState,
			string userIndexStyle,
			string logStyle,
			string logShowStyle,
			string photoStyle,
			string photoShowStyle,
			string picShowStyle,
			string groupStyle,
			string groupShowStyle,
			string groupTopicShow
		)
		{
			this.iD             = iD;
			this.styleName      = styleName;
			this.author         = author;
			this.stylePic       = stylePic;
			this.addtime        = addtime;
			this.styleState     = styleState;
			this.userIndexStyle = userIndexStyle;
			this.logStyle       = logStyle;
			this.logShowStyle   = logShowStyle;
			this.photoStyle     = photoStyle;
			this.photoShowStyle = photoShowStyle;
			this.picShowStyle   = picShowStyle;
			this.groupStyle     = groupStyle;
			this.groupShowStyle = groupShowStyle;
			this.groupTopicShow = groupTopicShow;
			
		}
		
		#endregion
		
		#region 属性定义
		
		///<summary>
		///编号
		///</summary>
		public int ID
		{
			get {return iD;}
			set {iD = value;}
		}

		///<summary>
		///模板名
		///</summary>
		public string StyleName
		{
			get {return styleName;}
			set {styleName = value;}
		}

		///<summary>
		///作者
		///</summary>
		public string Author
		{
			get {return author;}
			set {author = value;}
		}

		///<summary>
		///缩略图
		///</summary>
		public string StylePic
		{
			get {return stylePic;}
			set {stylePic = value;}
		}

		///<summary>
		///添加时间
		///</summary>
		public DateTime Addtime
		{
			get {return addtime;}
			set {addtime = value;}
		}

		///<summary>
		///模板状态(0为停用 1为启用)
		///</summary>
		public int StyleState
		{
			get {return styleState;}
			set {styleState = value;}
		}

		///<summary>
		///用户空间首页模板
		///</summary>
		public string UserIndexStyle
		{
			get {return userIndexStyle;}
			set {userIndexStyle = value;}
		}

		///<summary>
		///日志列表模板
		///</summary>
		public string LogStyle
		{
			get {return logStyle;}
			set {logStyle = value;}
		}

		///<summary>
		///日志内容页模板
		///</summary>
		public string LogShowStyle
		{
			get {return logShowStyle;}
			set {logShowStyle = value;}
		}

		///<summary>
		///相册列表模板
		///</summary>
		public string PhotoStyle
		{
			get {return photoStyle;}
			set {photoStyle = value;}
		}

		///<summary>
		///相册内容页模板
		///</summary>
		public string PhotoShowStyle
		{
			get {return photoShowStyle;}
			set {photoShowStyle = value;}
		}

		///<summary>
		///相片内容页模板
		///</summary>
		public string PicShowStyle
		{
			get {return picShowStyle;}
			set {picShowStyle = value;}
		}

		///<summary>
		///群族列表模板
		///</summary>
		public string GroupStyle
		{
			get {return groupStyle;}
			set {groupStyle = value;}
		}

		///<summary>
		///群族内容页模板
		///</summary>
		public string GroupShowStyle
		{
			get {return groupShowStyle;}
			set {groupShowStyle = value;}
		}

		///<summary>
		///话题内容页模板
		///</summary>
		public string GroupTopicShow
		{
			get {return groupTopicShow;}
			set {groupTopicShow = value;}
		}
	
		#endregion

    }
}
