using System;
using System.Data.OleDb;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RewriterTester
{
	/// <summary>
	/// Summary description for ListCategories.
	/// </summary>
	public class ListCategories : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid dgCategories;
	
		private void Page_Load(object sender, System.EventArgs e)
		{			
			if (!Page.IsPostBack)
				BindData();
		}

		private void BindData()
		{
			OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("Northwind.mdb"));

			const string SQL = "SELECT FriendlyURL, CategoryName FROM Categories ORDER BY CategoryName";

			OleDbCommand myCommand = new OleDbCommand(SQL, myConnection);

			myConnection.Open();
			OleDbDataReader reader = myCommand.ExecuteReader();
			dgCategories.DataSource = reader;
			dgCategories.DataBind();
			myConnection.Close();			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
