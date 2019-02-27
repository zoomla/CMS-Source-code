using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Web;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
   public class B_DocModel
    { 
        public string strTableName ,PK;
        private M_DocModel initMod = new M_DocModel();
        public B_DocModel()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
       /// <summary>
       /// ~/UploadFiles/DocTemp/
       /// </summary>
        public static string uploadPath = "~/UploadFiles/DocTemp/";

       /// <summary>
       /// 插入,用于增加模板组与模板
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public int insert(M_DocModel model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
       /// <summary>
       /// 获取指定Status的信息1:模板组,2:模板
       /// </summary>
       /// <returns></returns>
        public DataTable selGroup(int status) 
        {
            return Sql.Sel(strTableName, "status ="+status,"");
        }
       /// <summary>
       /// 获取模板组下模板
       /// </summary>
       /// <returns></returns>
        public DataTable selDocTemp(string status,string parentID) 
        {
           SqlParameter[] sp = new SqlParameter[] { new SqlParameter("status", status), new SqlParameter("parentID", parentID) };
           return Sql.Sel(strTableName, "status =@status and parentID=@parentID", "", sp);
        }

       /// <summary>
       /// 返回模板名字符串，格式1,2,3;
       /// </summary>
       /// <param name="status"></param>
       /// <param name="parentID"></param>
       /// <param name="flag"></param>
       /// <returns></returns>
        public string selDocTemp(string status, string parentID,int flag) 
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("status", status), new SqlParameter("parentID", parentID) };
            string s = "";
            DataTable dt = Sql.Sel(strTableName, "status =@status and parentID=@parentID", "", sp);
            if (dt.Rows.Count < 1)
            {
                s = "";
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    s += dr["DocName"] + ",";
                }
                s = s.Remove(s.LastIndexOf(","), 1); s += ";";
            }
            return s;
        }
        /// <summary>
        /// 返回模板ID字符串，格式1,2,3;
        /// </summary>
        /// <param name="status"></param>
        /// <param name="parentID"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string selDocID(string status, string parentID, int flag)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("status", status), new SqlParameter("parentID", parentID) };
            string s = "";
            DataTable dt = Sql.Sel(strTableName, "status =@status and parentID=@parentID", "", sp);
            if (dt.Rows.Count < 1) 
            { 
                s = ""; 
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    s += dr["ID"] + ",";
                }
                s = s.Remove(s.LastIndexOf(","), 1); s += ";";
            }
            return s;
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_DocModel SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_DocModel model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
       //-------------静态方法区,XML操作
        /// <summary>
       /// 返回模板组列表，checkBox选择，后台form["txt_mb"]获取
       /// </summary>
       /// <returns></returns>
        public static string getTemplateArrayList()    
        {
            B_DocModel bll = new B_DocModel();
            string html = @"<tr class='tdbg'> <td align='right' class='tdbgleft'><span>模板组列表</span></td><td>";
            //<td> <input id="txt_mb_0" type="checkbox" name="txt_mb" value="律师事务所函" />律师事务所函
            DataTable dt = bll.selGroup(1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<input id='txt_mb_" + i + "' type='checkbox' name='txt_mb' value='" + dt.Rows[i]["ID"] + "' style='margin-left:2px;'/>" + dt.Rows[i]["DocName"] + "";
            }
            html += "<input type='button' id='taBtn' onclick=' allCheck(\"txt_mb\")' class='C_input' value='全选' style='margin-left:5px;'/></td></tr>";
            return html;
        }
        /// <summary>
       /// 用于show页面，先看看xml中有无选取模板，再输出checkBox
       /// </summary>
       /// <returns></returns>
        public static string getTemplateArrayList(XmlDocument xmlDoc) 
        {
            B_DocModel bll = new B_DocModel();
            string html = @"<tr class='tdbg'> <td align='right' class='tdbgleft'><span>模板组列表</span></td><td>";
            DataTable dt = bll.selGroup(1);//获取模板组
            bool flag =true;
            try
            {
                if (xmlDoc != null)
                {
                    string[] tempList = xmlDoc.SelectSingleNode("DocInfo/tempArr").InnerText.Split(',');//格式ID,ID2,ID3
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        flag = true;
                        for (int j = 0; j < tempList.Length; j++)
                        {
                            if (dt.Rows[i]["ID"].ToString().Equals(tempList[j]))
                            {
                                html += "<input id='txt_mb_" + i + "' type='checkbox' checked='checked' name='txt_mb' value='" + dt.Rows[i]["ID"] + "' style='margin-left:2px;'/>" + dt.Rows[i]["DocName"] + ""; flag = false; break;
                            }
                        }
                        if (flag)
                        {
                            html += "<input id='txt_mb_" + i + "' type='checkbox' name='txt_mb' value='" + dt.Rows[i]["ID"] + "' style='margin-left:2px;'/>" + dt.Rows[i]["DocName"] + "";
                        }

                    }
                    html += "<input type='button' id='taBtn' onclick=' allCheck(\"txt_mb\")' class='i_bottom' value='全选' style='margin-left:5px;'/></td></tr>";
                    return html;//返回有选择的
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html += "<input id='txt_mb_" + i + "' type='checkbox' name='txt_mb' value='" + dt.Rows[i]["ID"] + "' style='margin-left:2px;'/>" + dt.Rows[i]["DocName"] + "";
                    }
                    html += "<input type='button' id='taBtn' onclick=' allCheck(\"txt_mb\")' class='C_input' value='全选' style='margin-left:5px;'/></td></tr>";
                    return html;
                }
            }
            catch { return getTemplateArrayList(); }
        }

       /// <summary>
       /// 只返回字符串格式模板组|模板组2
       /// </summary>
       /// <param name="xmlDoc"></param>
       /// <returns></returns>
        public static string getTemplateOnlyRead(XmlDocument xmlDoc) 
        {
            B_DocModel bll = new B_DocModel();
            string html = @"<tr class='tdbg'> <td style='font-weight:bold;text-align:center;' class='tdbgleft'><span>模板组列表</span></td><td>";
            DataTable dt = bll.selGroup(1);//获取模板组
            try
            {
                if (xmlDoc != null)
                {
                    string[] tempList = xmlDoc.SelectSingleNode("DocInfo/tempArr").InnerText.Split(',');//格式ID,ID2,ID3
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < tempList.Length; j++)
                        {
                            if (dt.Rows[i]["ID"].ToString().Equals(tempList[j]))
                            {
                                html += "[" + dt.Rows[i]["DocName"] + "]"; break;
                            }
                        }
                    }
                    html += "</td></tr>";
                    return html;//返回有选择的
                }
                else
                {
                    html += "[你当前没有选择任何模板组]</span></td></tr>";
                    return html;
                }
            }
            catch { return "你没有选择模板组"; }
        }
        
       /// <summary>
       /// 获取模板的ID字符串1,2,3
       /// </summary>
       /// <returns></returns>
        public static string getTemplateIDArrary(XmlDocument xmlDoc) 
        {
            try
            {
                return xmlDoc.SelectSingleNode("DocInfo/tempArr").InnerText;
            }
            catch {return ""; }
        }
        /// <summary>
       /// 返回文件夹下的XML文档,如果有的话,title输入标题即可
       /// </summary>
       /// <returns></returns>
        public static XmlDocument getTempXML(string title)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(HttpContext.Current.Server.MapPath(uploadPath + title + "/" + title + ".xml"));
                return xmlDoc;
            }
            catch
            {
                return null; 
            }
        }
        /// <summary>
        /// 更新TempArr，模板组选择信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        public static bool updateTempArr(string title,string value)
        {
                XmlDocument xmlDoc = B_DocModel.getTempXML(title);
                if (xmlDoc != null)
                {
                    if (xmlDoc.SelectSingleNode("DocInfo/tempArr") == null) //如果无当前节点，则
                    {
                        XmlElement temp = xmlDoc.CreateElement("tempArr");
                        temp.InnerText = "";
                        XmlElement root = xmlDoc.DocumentElement;
                        root.AppendChild(temp);   
                    }
                        XmlNode tempArr = xmlDoc.SelectSingleNode("DocInfo/tempArr");
                        tempArr.InnerText = value;
                        xmlDoc.Save(HttpContext.Current.Server.MapPath(uploadPath + title + "/" + title + ".xml"));
                        return true;
                }
                else
                {
                    return false;
                }
        }
    }
}