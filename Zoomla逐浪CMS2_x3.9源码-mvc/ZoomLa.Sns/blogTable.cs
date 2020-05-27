/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ZL_Sns_blogTable.cs
// 文件功能描述：定义数据表ZL_Sns_blogTable的业务实体
//
// 创建标识：Owner(2008-12-6) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_blogTable业务实体
    /// </summary>
    [Serializable]
    public class blogTable
    {
		#region 字段定义
		
		///<summary>
		///编号
		///</summary>
		private int iD;
		
		///<summary>
		///用户ID
		///</summary>
		private int userID;
		
		///<summary>
		///空间名称
		///</summary>
		private string blogName = String.Empty;
		
		///<summary>
		///空间简介
		///</summary>
		private string blogContent = String.Empty;
		
		///<summary>
		///申请状态(0为待审核 1审核通过 2关闭)
		///</summary>
		private int blogState;
		
		///<summary>
		///申请时间
		///</summary>
		private DateTime addtime;
		
		///<summary>
		///推荐状态(0为正常1为推荐)
		///</summary>
		private int commendState;
		
		///<summary>
		///模板ID
		///</summary>
		private int styleID;
		
		///<summary>
		///点击数
		///</summary>
		private int blogHits;
		
		
		#endregion
		
		#region 构造函数
		
		///<summary>
		///
		///</summary>
        public blogTable()
		{
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
		///用户ID
		///</summary>
		public int UserID
		{
			get {return userID;}
			set {userID = value;}
		}

		///<summary>
		///空间名称
		///</summary>
		public string BlogName
		{
			get {return blogName;}
			set {blogName = value;}
		}

		///<summary>
		///空间简介
		///</summary>
		public string BlogContent
		{
			get {return blogContent;}
			set {blogContent = value;}
		}

		///<summary>
		///申请状态(0为待审核 1审核通过 2关闭)
		///</summary>
		public int BlogState
		{
			get {return blogState;}
			set {blogState = value;}
		}

		///<summary>
		///申请时间
		///</summary>
		public DateTime Addtime
		{
			get {return addtime;}
			set {addtime = value;}
		}

		///<summary>
		///推荐状态(0为正常1为推荐)
		///</summary>
		public int CommendState
		{
			get {return commendState;}
			set {commendState = value;}
		}

		///<summary>
		///模板ID
		///</summary>
		public int StyleID
		{
			get {return styleID;}
			set {styleID = value;}
		}

		///<summary>
		///点击数
		///</summary>
		public int BlogHits
		{
			get {return blogHits;}
			set {blogHits = value;}
		}
	
		#endregion

    }
}
