﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Model.Shop;
using ZoomLa.BLL.Shop;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.User
{
    public partial class RegionManage : CustomerPageAction
    {

        B_Admin badmin = new B_Admin();
        M_AdminInfo mAdmin = new M_AdminInfo();
        M_Shop_RegionPrice regionMod = new M_Shop_RegionPrice();
        B_Shop_RegionPrice regionBll = new B_Shop_RegionPrice();
        public string GuidStr { get { return Request.QueryString["guid"]; } }
        public string Region { get { return HttpUtility.UrlDecode(Request.QueryString["region"] ?? ""); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ProvinceList_Name = ProvinceList.UniqueID;
            //CityList_Name = CityList.UniqueID;
            //CountyList_Name = CountyList.UniqueID;
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='AdminManage.aspx'>管理员管理</a></li><li><a href='javascript:'>地区设置</a></li>");
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            regionMod = regionBll.SelModelByGuid(GuidStr);
            if (regionMod == null) { regionMod = new M_Shop_RegionPrice(); }
            regionMod.Remind = "";
            regionMod.Info = UpdatePrice(regionMod.Info, Save_Hid.Value);
            regionMod.Guid = GuidStr;
            if (regionMod.ID > 0)
            {
                regionBll.UpdateByID(regionMod);
            }
            else
            {
                regionBll.Insert(regionMod);
            }
            function.WriteSuccessMsg("添加成功");
        }
        //如果存在,则无则添加,有则更新
        private string UpdatePrice(string info, string save)
        {
            //无数据则不需要更新
            if (string.IsNullOrEmpty(info)) { return save; }
            if (string.IsNullOrEmpty(save)) { return info; }
            List<M_RegionPrice_Price> infoList = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(info);//原有数据
            List<M_RegionPrice_Price> saveList = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(save);//需要更新或新加入的数据
            for (int i = 0; i < saveList.Count; i++)
            {
                var saveMod = saveList[i];
                var infoMod = infoList.FirstOrDefault(p => p.region.Equals(saveMod.region));
                if (infoMod == null) { infoList.Add(saveMod); }
                else
                {
                    infoMod.price = saveMod.price;
                }
            }
            return JsonConvert.SerializeObject(infoList);
        }
        protected void LtoR_Click(object sender, EventArgs e)
        {
            //int flag = 0;
            //string[] index;
            //if (string.IsNullOrEmpty(Request.Form[ProvinceList_Name]) && string.IsNullOrEmpty(Request.Form[CityList_Name]) && string.IsNullOrEmpty(Request.Form[CountyList_Name])) //如均未选，则退
            //    return;
            //if (!string.IsNullOrEmpty(Request.Form[ProvinceList_Name]))//要转移的值
            //    index = Request.Form[ProvinceList_Name].Split(',');
            //else if (!string.IsNullOrEmpty(Request.Form[CityList_Name])) { index = Request.Form[CityList_Name].Split(','); flag = 1;  }
            //else { index = Request.Form[CountyList_Name].Split(','); flag = 2;  }

            //GetViewState();
            //switch (flag)//0为省,1为市,2为县
            //{
            //    case 0: 
            //        MigrateList(list,MeCounty, index);//已更
            //        break;
            //    case 1: MigrateList(listc, MeCounty,index); break;
            //    case 2: MigrateList(listco, MeCounty, index); break;
            //    default:
            //        return;
            //}
            BatBind();
            SetViewState();
        }
        protected void RtoL_Click(object sender, EventArgs e)
        {
            //string[] index;
            //if (string.IsNullOrEmpty(Request.Form["MeAllCounty"])) return;
            //index = Request.Form["MeAllCounty"].Split(',');
            //GetViewState();
            //foreach (string s in index)
            //    MeCounty.Remove(s);

            //MeAllCounty.DataSource = MeCounty;
            //MeAllCounty.DataBind();
            //SetViewState();
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            GetViewState();
            int id = DataConverter.CLng(Request["id"]);//这里以后需要删除和添加合为一个事务，提供回滚功能
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('保存成功')", true);
        }
        //--------省,市,县选择事件
        protected void ProvinceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CountyList.ClearSelection();
            //CountyList.DataBind();
            //listc = ct.ReadCity(Server.MapPath(path), ProvinceList.SelectedValue);//加入判断，有的则不显示，没有的则显示
            //ListBoxBind(CityList, listc);
            //ViewState["listc"] = listc;
        }
        protected void CityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //当City被选择时清空其他ListBox选择
            //ProvinceList.ClearSelection();
            //string[] s = CityList.SelectedValue.Split(':');
            //if(s.Length>1)
            // ListBoxBind(CountyList, ct.ReadCountyByCity(Server.MapPath(path), s[0], s[1]));
        }
        protected void CountyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ProvinceList.ClearSelection();
            //ProvinceList.DataBind();
            //CityList.ClearSelection();
            //CityList.DataBind();
        }

        /// <summary>
        /// 初始化显示数据
        /// </summary>
        private void InitBind()
        {
            //将list中的数据给目标 
            //list = ct.readProvince(Server.MapPath(path));
            //int id = DataConverter.CLng(Request["id"] ?? "0");
            //mAdmin = B_Admin.GetAdminByAdminId(id);
            //ListBoxBind(ProvinceList, list);
            //if (ProvinceList.Items.Count > 0)//有未选中的省份,则显示城市列表
            //{
            //    listc = ct.ReadCity(Server.MapPath(path), ProvinceList.Items[0].Value);//市一级
            //    ListBoxBind(CityList, listc);
            //} 
            //else
            //{
            //    ProvinceList.Items.Add(new ListItem("已经没有未选中的省份"));
            //}
            //MeAllCounty.DataSource = MeCounty;
            //MeAllCounty.DataBind();
            SetViewState();

        }
        /// <summary>
        /// 绑定列表框
        /// </summary>
        private void ListBoxBind<T>(ListBox l, List<T> list)
        {
            l.DataSource = list;
            l.DataTextField = "name";
            l.DataValueField = "code";
            l.DataBind();
        }

        /// <summary>
        /// 更改需求后的转移方法
        /// </summary>
        //private void MigrateList(List<province> source,List<string>target,string[] index)
        //{
        //if (index == null)  return; 
        //province p = new province();
        //List<Pcity> l = new List<Pcity>();
        //List<County> lc = new List<County>();

        //path=Server.MapPath(path);
        //for (int m = 0; m < index.Length; m++)
        //{
        //    p = source.Find(delegate(province temp)//省的信息
        //    {
        //        return temp.Code == index[m];
        //    });
        //    //获取省下的市
        //    l = ct.ReadCity(path, p.Code);
        //    foreach (Pcity city in l)
        //    {
        //        lc = ct.ReadCountyByCity(path, city.Code.Split(':')[0], city.Code.Split(':')[1]);
        //        ListAdd(target, ct.CreatePCC(p, city, lc));
        //    }
        //    target.Sort();
        //    MeAllCounty.DataSource = target;
        //    MeAllCounty.DataBind();
        //   // list.Remove(p);
        //}
        //} //省

        private void MigrateList(List<Pcity> source, List<string> target, string[] index)
        {
            if (index == null) { return; }
            Pcity city = new Pcity();
            //List<County> lc = new List<County>();
            //path = Server.MapPath(path);

            //for (int m = 0; m < index.Length; m++)
            //{
            //    city = source.Find(delegate(Pcity temp)
            //    {
            //        return temp.Code == index[m];
            //    });

            //    lc = ct.ReadCountyByCity(path, city.Code.Split(':')[0], city.Code.Split(':')[1]);
            //    province p = ct.GetprovinceByCode(path,city.Fcode);

            //    ListAdd(target, ct.CreatePCC(p, city, lc));
            //    target.Sort();
            //    MeAllCounty.DataSource = target;
            //    MeAllCounty.DataBind();
            //}
        } //市

        //private void MigrateList(List<County> source, List<string> target, string[] index)
        //{
        //    //根据code找到省市，并输出
        //    if (index == null) { return; }
        //    path = Server.MapPath(path);
        //    province p =ct.GetprovinceByCode(path,index[0].Split(':')[0]);
        //    Pcity c = ct.GetCityByCode(path, index[0].Split(':')[0], index[0].Split(':')[1]);
        //    List<County> list=new List<County>();

        //    for (int m = 0; m < index.Length; m++)
        //    {
        //        list.Add(ct.GetCountyByCode(path,index[m]));
        //    }
        //    ListAdd(target,ct.CreatePCC(p,c,list));
        //    target.Sort();
        //    MeAllCounty.DataSource = target;
        //    MeAllCounty.DataBind();
        //}//县
        /// <summary>
        /// 从视图中获取对象
        /// </summary>
        private void GetViewState()
        {
            //list = ViewState["list"] as List<province>;
            //listc = ViewState["listc"] as List<Pcity>;
            //MeCounty = ViewState["MeCounty"] as List<string>;
            int id = DataConverter.CLng(Request["id"] ?? "0");
            mAdmin = B_Admin.GetAdminByAdminId(id);
        }
        /// <summary>
        /// 将对象存入视图
        /// </summary>
        private void SetViewState()
        {
        }
        /// <summary>
        /// 绑定
        /// </summary>
        private void BatBind()
        {
            //ListBoxBind(ProvinceList, list);
            //if (ProvinceList.Items.Count < 1) listc.Clear();
            //ListBoxBind(CityList, listc);
            //if (CityList.Items.Count < 1) listco.Clear();
            //ListBoxBind(CountyList,listco);
        }
        /// <summary>
        /// 判断是否重复，重复则不添加
        /// </summary>
        private void ListAdd(List<string> so, List<string> ta)
        {
            foreach (string s in ta)
            {
                if (!so.Contains(s)) so.Add(s);
            }
        }
    }
}