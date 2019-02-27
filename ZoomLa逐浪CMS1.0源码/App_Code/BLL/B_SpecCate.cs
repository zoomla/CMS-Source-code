namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using ZoomLa.DALFactory;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web.UI.WebControls;
    /// <summary>
    /// B_SpecCate 的摘要说明
    /// </summary>
    public class B_SpecCate
    {
        private ID_SpecCate dal = IDal.CreateSpecCate();
        private ID_Spec dspec = IDal.CreateSpec();
        public B_SpecCate()
        {            
        }
        public void AddSpecCate(M_SpecCate SpecCate)
        {
            dal.AddCate(SpecCate);
        }
        public void UpdateCate(M_SpecCate SpecCate)
        {
            dal.Update(SpecCate);
        }
        public void DelCate(int SpecCateID)
        {
            dal.DelCate(SpecCateID);
        }
        public M_SpecCate GetCate(int SpecCateID)
        {
            return dal.GetSpecCate(SpecCateID);
        }
        public DataTable GetCateLidt()
        {
            return dal.GetCateList();
        }
        /// <summary>
        /// 专题内容管理的专题树
        /// </summary>
        /// <param name="Nds"></param>
        public void TreeNode(TreeNodeCollection Nds)
        {
            TreeNode tmpNd;
            DataTable dtcate = dal.GetCateList();
            foreach (DataRow dr in dtcate.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["SpecCateID"].ToString();
                tmpNd.Text = dr["SpecCateName"].ToString();
                tmpNd.NavigateUrl = "";
                tmpNd.Target = "";
                Nds.Add(tmpNd);
                SpecChildTree(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value));
            }
        }
        public void SpecChildTree(TreeNodeCollection Nds, int SpecCateID)
        {
            TreeNode tmpNd;
            DataTable ds = this.dspec.GetSpecList(SpecCateID);            
            foreach (DataRow dr in ds.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["SpecID"].ToString();
                tmpNd.Text = dr["SpecName"].ToString();
                tmpNd.NavigateUrl = "SpecContent.aspx?SpecID=" + tmpNd.Value;
                tmpNd.Target = "main_right";
                Nds.Add(tmpNd);
            }
        }
        /// <summary>
        /// 专题选择的专题树
        /// </summary>
        /// <param name="Nds"></param>
        public void InitTreeNode(TreeNodeCollection Nds)
        {
            TreeNode tmpNd;
            DataTable dtcate = dal.GetCateList();
            foreach (DataRow dr in dtcate.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["SpecCateID"].ToString();
                tmpNd.Text = dr["SpecCateName"].ToString();
                tmpNd.NavigateUrl = "javascript:category();";
                tmpNd.Target = "";
                Nds.Add(tmpNd);
                InitSpecTree(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value));
            }
        }

        private void InitSpecTree(TreeNodeCollection Nds, int ParentID)
        {
            TreeNode tmpNd;
            DataTable ds = this.dspec.GetSpecList(ParentID);
            string CateName = this.dal.GetSpecCate(ParentID).SpecCateName;
            foreach (DataRow dr in ds.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["SpecID"].ToString();
                tmpNd.Text = dr["SpecName"].ToString();
                tmpNd.NavigateUrl = "javascript:SetSpec('" + CateName + ">>" + dr["SpecName"].ToString() + "','" + dr["SpecID"].ToString() + "');";
                tmpNd.Target = "";
                Nds.Add(tmpNd);
            }
        }        
    }
}