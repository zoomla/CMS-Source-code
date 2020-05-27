using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Config
{
    public partial class IPWeb : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            
        }
        protected void EgvIPLock_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }
        private DataTable IPTable
        {
            get
            {
                if (this.ViewState["IPTable"] == null)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("IPTo", typeof(string));
                    table.Columns.Add("IPFrom", typeof(string));
                    this.ViewState["IPTable"] = table;
                }
                return ((DataTable)this.ViewState["IPTable"]);
            }
            set
            {
                this.ViewState["IPTable"] = value;
            }
        }
        public string Value
        {
            get;set;
        }
    }
}