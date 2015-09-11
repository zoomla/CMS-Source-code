using ZoomLa.IDAL;
using ZoomLa.DALFactory;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
namespace ZoomLa.BLL
{

    /// <summary>
    /// B_SpecInfo 的摘要说明
    /// </summary>
    public class B_SpecInfo
    {
        private static readonly ID_SpecInfo dal = IDal.CreateSpecInfo();
        protected B_Spec bll = new B_Spec();
        protected B_SpecCate bcate = new B_SpecCate();
        public B_SpecInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 将专题信息添加到数据库中
        /// </summary>
        /// <param name="info">专题信息实例对象</param>
        public void Add(M_SpecInfo info)
        {
            dal.Add(info);
        }
        /// <summary>
        /// 将专题信息从数据库中删除
        /// </summary>
        /// <param name="SpecInfoID"></param>
        public void Del(int SpecInfoID)
        {
            dal.Del(SpecInfoID);
        }
        /// <summary>
        /// 获取某内容所属的专题ID字符串，可用','拆分成数组
        /// </summary>
        /// <param name="ItemID">内容ID</param>
        /// <returns></returns>
        public string GetSpec(int ItemID)
        {
            return dal.GetSpecial(ItemID);
        }
        /// <summary>
        /// 获取某专题信息所属专题类别和专题组合成的专题名
        /// </summary>
        /// <param name="SpecialID"></param>
        /// <returns></returns>
        public string GetSpecName(int SpecialID)
        {
            M_Spec info = this.bll.GetSpec(SpecialID);
            string SpecCate = this.bcate.GetCate(info.SpecCate).SpecCateName;
            return SpecCate + ">>" + info.SpecName;
        }
        /// <summary>
        /// 读取属于指定专题ID的内容列表
        /// </summary>
        /// <param name="SpecID">专题ID</param>
        /// <returns></returns>
        public DataTable GetSpecContent(int SpecID)
        {
            return dal.GetSpecContent(SpecID);
        }
        /// <summary>
        /// 检测指定专题ID和内容ID的专题信息是否存在
        /// </summary>
        /// <param name="SpecID">专题ID</param>
        /// <param name="ItemID">内容ID</param>
        /// <returns></returns>
        public bool IsExist(int SpecID, int InfoID)
        {
            return dal.IsExist(SpecID, InfoID);
        }
        /// <summary>
        /// 将指定专题信息ID的专题信息改变成另一个专题所属，即移动到另一专题
        /// </summary>
        /// <param name="SpecInfoID">专题信息ID</param>
        /// <param name="SpecID">另一专题ID</param>
        public void UpdateSpecID(int SpecInfoID, int SpecID)
        {
            dal.ChgSpecID(SpecInfoID, SpecID);
        }
        /// <summary>
        /// 获取指定专题信息ID的专题信息实例对象
        /// </summary>
        /// <param name="SpecInfoID">专题信息ID</param>
        /// <returns></returns>
        public M_SpecInfo GetSpecInfo(int SpecInfoID)
        {
            return dal.GetSpecInfo(SpecInfoID);
        }
        /// <summary>
        /// 将指定内容ID的内容所属专题更换成指定的专题组
        /// </summary>
        /// <param name="SpecID">专题ID组</param>
        /// <param name="ItemID">内容ID</param>
        public void UpdateSpec(string SpecID, int ItemID)
        {
            dal.DelItemSpec(SpecID, ItemID);
            string[] SpecArr = SpecID.Split(new char[] { ',' });
            M_SpecInfo info=new M_SpecInfo();
            info.InfoID=ItemID;
            for (int i = 0; i < SpecArr.Length; i++)
            {
                if (!IsExist(DataConverter.CLng(SpecArr[i]), ItemID))
                {
                    info.SpecialID = DataConverter.CLng(SpecArr[i]);
                    dal.Add(info);
                }
            }
        }
    }
}