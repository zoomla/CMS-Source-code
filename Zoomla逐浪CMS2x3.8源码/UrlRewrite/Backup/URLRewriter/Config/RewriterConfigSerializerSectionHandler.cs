using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace URLRewriter.Config
{
	/// <summary>
	/// Deserializes the markup in Web.config into an instance of the <see cref="RewriterConfiguration"/> class.
	/// </summary>
	public class RewriterConfigSerializerSectionHandler : IConfigurationSectionHandler 
	{
		/// <summary>
		/// Creates an instance of the <see cref="RewriterConfiguration"/> class.
		/// </summary>
		/// <remarks>Uses XML Serialization to deserialize the XML in the Web.config file into an
		/// <see cref="RewriterConfiguration"/> instance.</remarks>
		/// <returns>An instance of the <see cref="RewriterConfiguration"/> class.</returns>
		public object Create(object parent, object configContext, System.Xml.XmlNode section) 
		{
			// Create an instance of XmlSerializer based on the RewriterConfiguration type...
			XmlSerializer ser = new XmlSerializer(typeof(RewriterConfiguration));

			// Return the Deserialized object from the Web.config XML
			return ser.Deserialize(new XmlNodeReader(section));
		}

	}
}
