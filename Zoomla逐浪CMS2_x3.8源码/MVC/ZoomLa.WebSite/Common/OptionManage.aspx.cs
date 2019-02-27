using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Collections;
namespace ZoomLaCMS.Common
{
    public partial class OptionManage : System.Web.UI.Page
    {
        private B_User ull = new B_User();
        private B_ModelField fll = new B_ModelField();
        public int thisid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["menu"] != null)
            {
                string menu = Request.QueryString["menu"];
                if (menu == "delete")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    int fid = DataConverter.CLng(Request.QueryString["fid"]);
                    int ModelID = DataConverter.CLng(Request.QueryString["ModelID"]);
                    M_ModelField finfos = fll.GetModelByID(ModelID.ToString(), id);

                    string[] s0 = finfos.Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
                    string s1 = finfos.Content.Split(new char[] { ',' })[1];
                    string s2 = finfos.Content.Split(new char[] { ',' })[2];
                    string d1 = s0[0].ToString();
                    string d2 = s0[1].ToString();

                    string sld = "";

                    if (d2.IndexOf("||") > -1)
                    {
                        string[] sarr = d2.Split(new string[] { "||" }, StringSplitOptions.None);
                        for (int ii = 0; ii < sarr.Length; ii++)
                        {
                            if (ii + 1 != fid)
                            {
                                sld = sld + sarr[ii];
                                if (ii < sarr.Length - 1)
                                {
                                    sld = sld + "||";
                                }
                            }
                        }

                        if (BaseClass.Right(sld, 2) == "||")
                        {
                            sld = BaseClass.Left(sld, 0, sld.Length - 2);
                        }


                        sld = d1 + "=" + sld + "," + s1 + "," + s2;
                    }
                    else
                    {
                        if (fid == 1)
                        {
                            sld = d1 + "=" + "," + s1 + "," + s2;
                        }
                    }
                    finfos.Content = sld;
                    fll.Update(finfos);
                    Response.Redirect("OptionManage.aspx?id=" + id + "&ModelID=" + ModelID.ToString() + "");
                }
            }

            int i = 0;
            IList<M_Classon> listuser = new List<M_Classon>();
            int idd = DataConverter.CLng(Request.QueryString["ID"]);
            int ModelIDs = DataConverter.CLng(Request.QueryString["ModelID"]);


            M_ModelField modelinfo = fll.GetModelByID(ModelIDs.ToString(), idd);
            string contents = modelinfo.Content;
            string[] strArray2 = contents.Split(new char[] { ',' })[0].Split(new char[] { '=' });

            if (strArray2 != null && strArray2.Length > 1)
            {
                string optioncontent = "";

                if (strArray2[1] != null)
                {
                    optioncontent = strArray2[1].ToString();
                }

                if (optioncontent.IndexOf("||") > -1)
                {
                    string[] contentarr = optioncontent.Split(new string[] { "||" }, StringSplitOptions.None);
                    if (contentarr != null)
                    {
                        foreach (string contentitem in contentarr)
                        {
                            i = i + 1;
                            if (contentitem.IndexOf('$') > -1)
                            {
                                string[] items = contentitem.Split(new string[] { "$" }, StringSplitOptions.None);
                                int userid = DataConverter.CLng(items[1].ToString());
                                M_UserInfo infos = ull.GetLogin();
                                if (infos.UserID == userid)
                                {
                                    string itemcontent = items[0].ToString();
                                    if (itemcontent.IndexOf('|') > -1)
                                    {
                                        string[] arr = itemcontent.Split(new string[] { "|" }, StringSplitOptions.None);
                                        M_Classon classlist = new M_Classon();
                                        classlist.id = i;
                                        classlist.classname = arr[0].ToString();
                                        classlist.classvalue = arr[1].ToString();
                                        listuser.Add(classlist);
                                    }
                                }
                            }
                            else
                            {
                                if (contentitem.IndexOf('|') > -1)
                                {
                                    string[] arr = contentitem.Split(new string[] { "|" }, StringSplitOptions.None);
                                    M_Classon conarrlist = new M_Classon();
                                    conarrlist.id = i;
                                    conarrlist.classname = arr[0].ToString();
                                    conarrlist.classvalue = arr[1].ToString();
                                    listuser.Add(conarrlist);
                                }
                                else
                                {
                                    M_Classon conarrlistd = new M_Classon();
                                    conarrlistd.id = i;
                                    conarrlistd.classname = contentitem;
                                    conarrlistd.classvalue = contentitem;
                                    listuser.Add(conarrlistd);
                                }
                            }
                        }
                    }
                }
                else
                {
                    M_Classon conarrlistd = new M_Classon();
                    conarrlistd.classname = optioncontent;
                    conarrlistd.classvalue = optioncontent;
                    listuser.Add(conarrlistd);
                }
            }
            Repeater1.DataSource = listuser;
            Repeater1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            int ModelID = DataConverter.CLng(Request.QueryString["ModelID"]);

            Response.Redirect("AddOption.aspx?id=" + id + "&ModelID=" + ModelID + "");
        }
    }
    public partial class M_Classon
    {
        public M_Classon()
        {
        }


        public int m_id;
        public string m_classname;
        public string m_classvalue;

        /// <summary>
        /// 分类ID
        /// </summary>
        public int id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string classname
        {
            get { return this.m_classname; }
            set { this.m_classname = value; }
        }

        /// <summary>
        /// 分类值
        /// </summary>
        public string classvalue
        {
            get { return this.m_classvalue; }
            set { this.m_classvalue = value; }
        }

    }
}