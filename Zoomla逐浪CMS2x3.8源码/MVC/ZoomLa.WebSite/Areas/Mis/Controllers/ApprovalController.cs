using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Areas.Mis.Controllers
{
    public class ApprovalController : ZLCtrl
    {
        B_MisType mtBll = new B_MisType();
        B_MisProcedure mpBll = new B_MisProcedure();
        B_MisApproval maBll = new B_MisApproval();
        B_Mis_AppProg mappBll = new B_Mis_AppProg();
        B_MisProLevel proBll = new B_MisProLevel();
        public ActionResult Default()
        {
            if (!B_User.CheckIsLogged()) { return null; }
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
              
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewBag.rptdt = dt;
            }
            DataTable dt1 = maBll.Sel(Inputer: buser.GetLogin().UserName);
            DataTable dt2 = maBll.Sel(Inputer: buser.GetLogin().UserName, Results: 1);
            DataTable dt3 = maBll.Sel(Inputer: buser.GetLogin().UserName, Results: 3);
            DataTable dt5 = maBll.Sel(Approver: buser.GetLogin().UserName);
            DataTable dt6 = maBll.Sel(Approver: buser.GetLogin().UserName, Results: 1);
            DataTable dt7 = maBll.Sel(Approver: buser.GetLogin().UserName, Results: 2);
            DataTable dt8 = maBll.Sel(Send: buser.GetLogin().UserName);
            DataTable dt9 = maBll.Sel(Send: buser.GetLogin().UserName, Results: 3);
            //if (dt1 != null)
            //{
            //    this.lblAllOf.Text = dt1.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblAllOf.Text = "0";
            //}
            //if (dt2 != null)
            //{
            //    this.lblInApps.Text = dt2.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblInApps.Text = "0";
            //}
            //if (dt3 != null)
            //{
            //    this.lblonApps.Text = dt3.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblonApps.Text = "0";
            //}
            //if (dt5 != null)
            //{
            //    this.lblAlls.Text = dt5.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblAlls.Text = "0";
            //}
            //if (dt6 != null)
            //{
            //    this.lblNosave.Text = dt6.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblNosave.Text = "0";
            //}
            //if (dt7 != null)
            //{
            //    this.lalAllss.Text = dt7.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lalAllss.Text = "0";
            //}
            //if (dt8 != null)
            //{
            //    this.lblAllMe.Text = dt8.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblAllMe.Text = "0";
            //}
            //if (dt9 != null)
            //{
            //    this.lblNoPass.Text = dt9.Rows.Count.ToString();
            //}
            //else
            //{
            //    this.lblNoPass.Text = "0";
            //}
            DataTable types = mtBll.Sels();
            List<SelectListItem> nodeList = new List<SelectListItem>();
            nodeList.Add(new SelectListItem { Text = "全部", Value = "0" });
            foreach (DataRow row in types.Rows)
            {
                nodeList.Add(new SelectListItem { Text = row.Field<string>("TypeName"), Value = row.Field<int>("ID").ToString() });
            }
            ViewBag.types = nodeList;
            ViewBag.typesDt = types;
            return View();
        }
        public ActionResult AddApproval()
        {
            if (!B_User.CheckIsLogged()) { return null; }
            ViewBag.inputer = mu.UserName;
            int ID = DataConverter.CLng(Request["ID"]);
            DataTable types = mtBll.Sels();
            List<SelectListItem> nodeList = new List<SelectListItem>();
            nodeList.Add(new SelectListItem { Text = "全部", Value = "0" });
            foreach (DataRow row in types.Rows)
            {
                nodeList.Add(new SelectListItem { Text = row.Field<string>("TypeName"), Value = row.Field<int>("ID").ToString() });
            }
            ViewBag.types = nodeList;
            ViewBag.pros = mpBll.Sel();
            DataTable dt = maBll.Sel(ID);
            if (dt.Rows.Count > 0)
            {
                ViewBag.content = dt.Rows[0]["Content"].ToString();
                ViewBag.approver = dt.Rows[0]["Approver"].ToString();
                ViewBag.results = dt.Rows[0]["Results"].ToString();
            }
            return View();
        }
        public void AddApproval_Add()
        {
            string pro = Request.Form["HidPro"];
            if (!string.IsNullOrEmpty(pro)) { function.WriteErrMsg("未选定流程"); return; }
            int ID = DataConverter.CLng(Request["ID"]);
            M_MisApproval maMod = maBll.SelReturnModel(ID) ?? new M_MisApproval();
            maMod.content = Request.Form["TxtContent"];
            maMod.Approver = Request.Form["TxtApprover"];
            maMod.Inputer = mu.UserName;
            maMod.ProcedureID = DataConverter.CLng(pro);
            maMod.CreateTime = DateTime.Now;
            maMod.Results = DataConverter.CLng(Request.Form["TxtResults"]);
            if (maMod.ID > 0)
            {
                maBll.UpdateByID(maMod);
                function.WriteSuccessMsg("修改成功", "Default"); return;
            }
            else
            {
                if (maMod.Approver.Equals(maMod.Inputer)) { function.WriteErrMsg("审批人不能为申请人!"); return; }
                if (string.IsNullOrEmpty(maMod.content) && string.IsNullOrEmpty(maMod.Approver)) { function.WriteErrMsg("请填写完整的信息"); return; }
                if (maBll.insert(maMod) > 0) { function.WriteSuccessMsg("添加成功!", "Default"); return; }
                else { function.WriteErrMsg("添加失败!", "Default"); return; }
            }
        }
        public ActionResult Approval()
        {
            return View();
        }
        public ActionResult ApproverView()
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) {return null; }
            int ID = DataConverter.CLng(Request["ID"]);
            return View(new DataTable());
        }
        public void InsertRecord()
        {
            int status = DataConverter.CLng(Request["status"]);
            int id = DataConverter.CLng(Request["ID"]);
            M_MisApproval maMod = maBll.SelReturnModel(id);
            #region M_MisProLevel
            M_MisProLevel CurrentLevel = new M_MisProLevel();
            DataTable appProgDT = mappBll.SelByAppID(id.ToString());//已进行到的流程
            DataTable proLevelDT = proBll.SelByProID(maMod.ProcedureID);//全部流程
            if (appProgDT.Rows.Count < 1)//尚未开始
            {
                CurrentLevel = CurrentLevel.GetModelFromDR(proLevelDT.Rows[0]);//用第一个填充,其值是经过Level排序的
            }
            else if (appProgDT.Rows.Count < proLevelDT.Rows.Count)//已经开始但未完成 
            {
                string proLevel = appProgDT.Rows[appProgDT.Rows.Count - 1]["ProLevel"].ToString();//现在进行到的最后
                CurrentLevel = proBll.SelByProIDAndStepNum(maMod.ProcedureID, DataConverter.CLng(proLevel));
            }
            else //已完成，或无流程的
            {
                CurrentLevel.Status = 99;
            }
            #endregion
            M_Mis_AppProg model = new M_Mis_AppProg();
            model.AppID = maMod.ID;
            model.ProID = maMod.ProcedureID;
            model.ProLevel = CurrentLevel.stepNum;
            model.ProLevelName = CurrentLevel.stepName;
            model.ApproveID = mu.UserID;
            model.Result = status;
            model.CreateTime = DateTime.Now;
            mappBll.Insert(model);
            if (status == -1)
            {
                maMod.Results = -1;
            }
            maBll.UpdateByID(maMod);
            Response.Redirect("ApproverView");return;
        }
        public ActionResult EditProcedure()
        {
            int id = Convert.ToInt32(Request.QueryString["ProID"]);
            M_MisProcedure proMod=mpBll.SelReturnModel(id);
            if (proMod!=null)
            {
                ViewBag.TxtName = proMod.ProcedureName;
                DataTable dt = proBll.SelByProID(id);
                ViewBag.repdt = dt;
                if (dt.Rows.Count > 0)
                {
                    ViewBag.txtTRLastIndex = (dt.Rows.Count + 1).ToString();
                }
                else
                {
                    ViewBag.txtTRLastIndex = "1";
                }
                SqlParameter[] sp2 = new SqlParameter[] { new SqlParameter("ID", proMod.MyProType) };
                string Name = mtBll.Sel("ID=@ID", "ID", sp2).Rows[0]["TypeName"].ToString();
                ViewBag.DrpTypeSel = Name;
            }
            else
            {
                ViewBag.TxtName.Text = "";
            }

            DataTable types = mtBll.Sels();
            List<SelectListItem> nodeList = new List<SelectListItem>();
            nodeList.Add(new SelectListItem { Text = "全部", Value = "0" });
            foreach (DataRow row in types.Rows)
            {
                nodeList.Add(new SelectListItem { Text = row.Field<string>("TypeName"), Value = row.Field<int>("ID").ToString() });
            }
            ViewBag.types = nodeList;
            return View();
        }
    }
}
