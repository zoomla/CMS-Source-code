using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    public class Parking_BLL
    {
        #region ��ӳ�����Ϣ
        /// <summary>
        /// ��ӳ�����Ϣ
        /// </summary>
        /// <param name="cl"></param>
        public void AddCarList(P_CarList cl)
        {
            Parking_Logic.AddCarList(cl);
        }
        #endregion

        #region ���³����б���Ϣ
        /// <summary>
        /// ���³����б���Ϣ
        /// </summary>
        /// <param name="cl"></param>
        public void UpdateCarList(P_CarList cl)
        {
            Parking_Logic.UpdateCarList(cl);
        }
        #endregion

        #region ���ó����Ƿ����
        /// <summary>
        /// ���ó����Ƿ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="check"></param>
        public void UpdateCarListCheck(int id, int check)
        {
            Parking_Logic.UpdateCarListCheck(id, check);
        }
        #endregion

        #region ��ѯϵͳ�����г���
        /// <summary>
        /// ��ѯϵͳ�����г���
        /// </summary>
        /// <returns></returns>
        public List<P_CarList> GetCarList()
        {
            return Parking_Logic.GetCarList();
        }
        #endregion

        #region ��ѯ����������Ϣ
        /// <summary>
        /// ��ѯ����������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public P_CarList GetCar(int id)
        {
            return Parking_Logic.GetCar(id);
        }
        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="mcar"></param>
        public void AddMyCar(ZL_Sns_MyCar mcar)
        {
            Parking_Logic.AddMyCar(mcar);
        }
        #endregion

        #region �����û�������Ϣ
        /// <summary>
        /// �����û�������Ϣ
        /// </summary>
        /// <param name="mc"></param>
        public void UpdataMyCar(ZL_Sns_MyCar mc)
        {
            Parking_Logic.UpdataMyCar(mc);
        }
        #endregion

        #region �����û�ID��ѯ�û�ӵ�г���
        /// <summary>
        /// �����û�ID��ѯ�û�ӵ�г���
        /// </summary>
        /// <param name="P_uid"></param>
        /// <returns></returns>
        public List<ZL_Sns_MyCar> GetMyCarList(int P_uid)
        {
            return Parking_Logic.GetMyCarList(P_uid);
        }
        #endregion

        #region ���ݱ��ID��ѯ�û�ӵ�г�����Ϣ
        /// <summary>
        /// ���ݱ��ID��ѯ�û�ӵ�г�����Ϣ
        /// </summary>
        /// <param name="pmid"></param>
        /// <returns></returns>
        public ZL_Sns_MyCar GetMyCar(int pmid)
        {
            return Parking_Logic.GetMyCar(pmid);
        }
        #endregion

        #region �����û�ID������ID��ѯ�û�������������Ϣ
        /// <summary>
        /// �����û�ID������ID��ѯ�û�������������Ϣ
       /// </summary>
       /// <param name="P_uid">�û�ID</param>
       /// <param name="Pid">����ID</param>
       /// <returns></returns>
        public ZL_Sns_MyCar GetMyCar(int P_uid, int Pid)
        {
            return Parking_Logic.GetMyCar(P_uid, Pid);
        }
        #endregion

        #region ����û�ӵ�еĳ���
        /// <summary>
        /// ����û�ӵ�еĳ���
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="userid">�û�ID</param>
        /// <returns>����True���û�δӵ�����ֳ�������false���û��Ѿ�ӵ�����ֳ�</returns>
        public bool CheckCar(int id, int userid)
        {
            return Parking_Logic.CheckCar(id, userid);
        }
        #endregion

        #region ����û�������־
        /// <summary>
        /// ����û�������־
        /// </summary>
        /// <param name="zscarlog"></param>
        public void AddCarLog(ZL_Sns_CarLog zscarlog)
        {
            Parking_Logic.AddCarLog(zscarlog);
        }
        #endregion

        #region �����û�ID��ѯ������־
        /// <summary>
        /// �����û�ID��ѯ������־
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ZL_Sns_CarLog> GetUserIDCarLog(int id)
        {
            return Parking_Logic.GetUserIDCarLog(id);
        }
        #endregion

        #region ��ʼ���û���λ��Ϣ
        /// <summary>
        /// ��ʼ���û���λ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        public void AddMyPose(int id)
        {
            //����û���λ
            if (!CheckUserPose(id))
            {
                Parking_Logic.AddMyPose(id);
            }
        }
        #endregion

        #region �����û���λ��Ϣ
        /// <summary>
        /// �����û���λ��Ϣ
        /// </summary>
        /// <param name="mp"></param>
        public void UpdateMyPose(ZL_Sns_MyPose mp)
        {
            Parking_Logic.UpdateMyPose(mp);
        }
        #endregion

        #region ����û���λ
        /// <summary>
        /// ����û���λ
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>����true ���û��Ѿ�ӵ�г�λ������false ���û�û�г�λ</returns>
        public bool CheckUserPose(int userid)
        {
            return Parking_Logic.CheckUserPose(userid);
        }
        #endregion

        #region �����û�ID��ѯ�û���λ
        /// <summary>
        /// �����û�ID��ѯ�û���λ
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ZL_Sns_MyPose GetMyPose(int userid)
        {
            return Parking_Logic.GetMyPose(userid);
        }
        #endregion

        #region ���ͣ����Ϣ
        /// <summary>
        /// ���ͣ����Ϣ
        /// </summary>
        /// <param name="zsr"></param>
        public int AddReport(ZL_Sns_Report zsr)
        {
            return Parking_Logic.AddReport(zsr);
        }
        #endregion

        #region ���³���ͣ����Ϣ
        /// <summary>
        /// ���³���ͣ����Ϣ
        /// </summary>
        /// <param name="zsr"></param>
        public void UpdateReport(int id)
        {
            Parking_Logic.UpdateReport(id);

        }
        #endregion

        #region ����ID��ѯ����ͣ����Ϣ
        /// <summary>
        /// ����ID��ѯ����ͣ����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  ZL_Sns_Report GetCarReport(int id)
        {
            return Parking_Logic.GetCarReport(id);
        }
        #endregion

        #region �޸�����λ����
        /// <summary>
        /// �޸�����λ����
        /// </summary>
        /// <param name="cc"></param>
        public void UpdateCarConfig(string cvalue, int id)
        {
            Parking_Logic.UpdateCarConfig(cvalue, id);
        }
        #endregion

        #region ��ѯ����λ����
        /// <summary>
        /// ��ѯ����λ����
        /// </summary>
        /// <returns></returns>
        public List<ZL_Sns_CarConfig> GetCarConfig()
        {
            return Parking_Logic.GetCarConfig();
        }
        #endregion
    }
}
