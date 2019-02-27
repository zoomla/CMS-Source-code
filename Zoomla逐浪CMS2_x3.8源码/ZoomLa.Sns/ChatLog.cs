/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ZL_Sns_ChatLog.cs
// 文件功能描述：定义数据表ZL_Sns_ChatLog的业务实体
//
// 创建标识：Owner(2008-12-17) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_ChatLog业务实体
    /// </summary>
    [Serializable]
    public class ChatLog
    {
        #region 字段定义
		
		///<summary>
		///ID
		///</summary>
		private int iD;
		
		///<summary>
		///姓名
		///</summary>
		private string name = String.Empty;
		
		///<summary>
		///性别
		///</summary>
		private string sex = String.Empty;
		
		///<summary>
		///聊天内容
		///</summary>
		private string chatContent = String.Empty;
		
		///<summary>
		///聊天时间
		///</summary>
		private DateTime addtime;
		
		///<summary>
		///聊天对象
		///</summary>
		private int byID;
		
		///<summary>
		///类型
		///</summary>
		private string chatType = String.Empty;
		
		
		#endregion
		
		#region 构造函数
		
		///<summary>
		///
		///</summary>
        public ChatLog()
		{
		}
		
		
		
		#endregion
		
		#region 属性定义
		
		///<summary>
		///ID
		///</summary>
		public int ID
		{
			get {return iD;}
			set {iD = value;}
		}

		///<summary>
		///姓名
		///</summary>
		public string Name
		{
			get {return name;}
			set {name = value;}
		}

		///<summary>
		///性别
		///</summary>
		public string Sex
		{
			get {return sex;}
			set {sex = value;}
		}

		///<summary>
		///聊天内容
		///</summary>
		public string ChatContent
		{
			get {return chatContent;}
			set {chatContent = value;}
		}

		///<summary>
		///聊天时间
		///</summary>
		public DateTime Addtime
		{
			get {return addtime;}
			set {addtime = value;}
		}

		///<summary>
		///聊天对象
		///</summary>
		public int ByID
		{
			get {return byID;}
			set {byID = value;}
		}

		///<summary>
		///类型
		///</summary>
		public string ChatType
		{
			get {return chatType;}
			set {chatType = value;}
		}
	
		#endregion

    }
}
