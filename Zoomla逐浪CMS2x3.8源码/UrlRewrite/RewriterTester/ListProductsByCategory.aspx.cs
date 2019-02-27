using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RewriterTester
{
	/// <summary>
	/// Summary description for ListProductsByCategory.
	/// </summary>
	public class ListProductsByCategory : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblCategoryName;
		protected System.Web.UI.WebControls.DataGrid dgProducts;
		protected ActionlessForm.Form Form1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private int CategoryID = 1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// determine the CategoryID passed in
			if (Request.QueryString["CategoryID"] != null)
				CategoryID = Convert.ToInt32(Request.QueryString["CategoryID"]);

			if (!Page.IsPostBack)
				BindData("UnitPrice DESC");
		}

		private void BindData(string orderBy)
		{
			OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("Northwind.mdb"));

			string productSQL = "SELECT ProductName, UnitPrice FROM Products WHERE CategoryID = @CatID ORDER BY " + orderBy;
			const string catNameSQL = "SELECT CategoryName FROM Categories WHERE CategoryID = @CatID";

			OleDbCommand myCommand = new OleDbCommand(productSQL, myConnection);
			myCommand.Parameters.Add(new OleDbParameter("@CatID", CategoryID));

			myConnection.Open();
			OleDbDataReader reader = myCommand.ExecuteReader();
			dgProducts.DataSource = reader;
			dgProducts.DataBind();
			
			reader.Close();

			myCommand.CommandText = catNameSQL;
			lblCategoryName.Text = myCommand.ExecuteScalar().ToString();
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
			this.dgProducts.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgProducts_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgProducts_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BindData(e.SortExpression);
		}
	}
}
