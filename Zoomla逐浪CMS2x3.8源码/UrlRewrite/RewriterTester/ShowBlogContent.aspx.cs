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
	/// Summary description for ShowBlogContent.
	/// </summary>
	public class ShowBlogContent : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgBlogContent;		

		private void Page_Load(object sender, System.EventArgs e)
		{
			// find out the year/month/day to view
			int year = DateTime.Now.Year, month = -1, day = -1;

			if (Request.QueryString["year"] != null)
				year = Convert.ToInt32(Request.QueryString["year"]);
			
			OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("BlogData.mdb"));

			string SQL = "SELECT Title, DateAdded FROM blog_Content WHERE PostType=1 AND DATEPART('yyyy', DateAdded) = @Year ";
			if (Request.QueryString["month"] != null)
			{
				SQL += "AND DATEPART('m', DateAdded) = @Month ";
				month = Convert.ToInt32(Request.QueryString["month"]);
			}
			if (Request.QueryString["day"] != null)
			{
				SQL += "AND DATEPART('d', DateAdded) = @Day ";
				day = Convert.ToInt32(Request.QueryString["day"]);
			}			
			SQL += "ORDER BY DateAdded";			

			OleDbCommand myCommand = new OleDbCommand(SQL, myConnection);

			myCommand.Parameters.Add(new OleDbParameter("@Year", year));
			if (month != -1) myCommand.Parameters.Add(new OleDbParameter("@Month", month));
			if (day != -1) myCommand.Parameters.Add(new OleDbParameter("@Day", day));

			myConnection.Open();
			dgBlogContent.DataSource = myCommand.ExecuteReader();
			dgBlogContent.DataBind();
			myConnection.Close();

			if (dgBlogContent.Items.Count == 0)
				Response.Write("No blog entries for the specified year/month/day...");
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
