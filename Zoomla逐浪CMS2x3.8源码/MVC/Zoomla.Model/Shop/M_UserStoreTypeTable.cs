using System;
namespace ZoomLa.Model
{
	/// <summary>
	///ZL_UserStoreTypeTable业务实体
	/// </summary>
	[Serializable]
	public class M_UserStoreTypeTable
	{
		public M_UserStoreTypeTable()
		{
            this.TypeName = string.Empty;
		}
		#region 属性定义		
        public int ID { get; set; }
		///<summary>
		///类型名称
		///</summary>
        public string TypeName { get; set; }
		#endregion		
	}
}