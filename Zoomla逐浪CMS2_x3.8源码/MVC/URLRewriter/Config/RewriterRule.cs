using System;

namespace URLRewriter.Config
{
	/// <summary>
	/// Represents a rewriter rule.  A rewriter rule is composed of a pattern to search for and a string to replace
	/// the pattern with (if matched).
	/// </summary>
	[Serializable()]
	public class RewriterRule
	{
		// private member variables...
		private string lookFor, sendTo;

		#region Public Properties
		/// <summary>
		/// Gets or sets the pattern to look for.
		/// </summary>
		/// <remarks><b>LookFor</b> is a regular expression pattern.  Therefore, you might need to escape
		/// characters in the pattern that are reserved characters in regular expression syntax (., ?, ^, $, etc.).
		/// <p />
		/// The pattern is searched for using the <b>System.Text.RegularExpression.Regex</b> class's <b>IsMatch()</b>
		/// method.  The pattern is case insensitive.</remarks>
		public string LookFor
		{
			get
			{
				return lookFor;
			}
			set
			{
				lookFor = value;
			}
		}

		/// <summary>
		/// The string to replace the pattern with, if found.
		/// </summary>
		/// <remarks>The replacement string may use grouping symbols, like $1, $2, etc.  Specifically, the
		/// <b>System.Text.RegularExpression.Regex</b> class's <b>Replace()</b> method is used to replace
		/// the match in <see cref="LookFor"/> with the value in <b>SendTo</b>.</remarks>
		public string SendTo
		{
			get
			{
				return sendTo;
			}
			set
			{
				sendTo = value;
			}
		}
		#endregion
	}
}
