/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Question.cs
// 文件功能描述：定义数据表Question的业务实体
//
// 创建标识：Owner(2008-10-22) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
	/// <summary>
	///Question业务实体
	/// </summary>
	[Serializable]
	public class Question
	{
		#region 字段定义
		
		///<summary>
		///
		///</summary>
		private Guid iD;
		
		///<summary>
		///
		///</summary>
		private Guid  userID;
		
		///<summary>
		///问题
		///</summary>
		private string questionContent = String.Empty;
		
		///<summary>
		///时间
		///</summary>
		private DateTime creatTime;
		
		
		#endregion
		
		#region 构造函数
		
		///<summary>
		///
		///</summary>
		public Question()
		{
		}
		
		
		
		#endregion
		
		#region 属性定义
		
		///<summary>
		///
		///</summary>
		public Guid ID
		{
			get {return iD;}
			set {iD = value;}
		}

		///<summary>
		///
		///</summary>
		public Guid  UserID
		{
			get {return userID;}
			set {userID = value;}
		}

		///<summary>
		///问题
		///</summary>
		public string QuestionContent
		{
			get {return questionContent;}
			set {questionContent = value;}
		}

		///<summary>
		///时间
		///</summary>
		public DateTime CreatTime
		{
			get {return creatTime;}
			set {creatTime = value;}
		}
	
		#endregion
		
	}
}
