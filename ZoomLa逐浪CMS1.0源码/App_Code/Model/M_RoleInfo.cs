namespace ZoomLa.Model
{
    using System;
    /// <summary>
    /// M_RoleInfo 的摘要说明
    /// 角色信息
    /// </summary>
    public class M_RoleInfo
    {
        /// <summary>
        /// 角色ＩＤ
        /// </summary>
        private int m_RoleID;
        public int RoleID
        {
            get
            {
                return m_RoleID;
            }
            set
            {
                m_RoleID = value;
            }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        private string m_RoleName;
        public string RoleName
        {
            get
            {
                return m_RoleName;
            }
            set
            {
                m_RoleName = value;
            }
        }

        /// <summary>
        /// 角色描述
        /// </summary>
        private string m_Description;
        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        /// <summary>
        /// 操作代码
        /// （具有权限）
        /// </summary>
        private string m_OperateCode;
        public string OperateCode
        {
            get { return m_OperateCode; }
            set { m_OperateCode = value; }
        }
        //是否是空对象
        private bool m_IsNull = false;
        /// <summary>
        ///对象是否空对象
        /// </summary>
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }
        public M_RoleInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
    }
}