using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using RestSharp.Serializers;
using System.ComponentModel.DataAnnotations;

namespace MvcTestTask.Models.Api
{
	public class Content
	{
		[Required]
		[Display(Name = "Имя")]
		public string FromName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "E-mail")]
		public string FromEmail { get; set; }

		[Required]
		[Display(Name = "Тема")]
		public string Subject { get; set; }

		[Display(Name = "Текст")]
		public string Plain { get; set; }

		[XmlIgnore]
		public string Html { get; set; }

		[XmlElement("Html")]
		public XmlNode[] CDataContent
		{
			get
			{
				var dummy = new XmlDocument();
				return new XmlNode[] { dummy.CreateCDataSection(Html) };
			}
			set
			{
				if (value == null)
				{
					Html = null;
					return;
				}

				if (value.Length != 1)
				{
					throw new InvalidOperationException(
							String.Format(
									"Invalid array length {0}", value.Length));
				}

				var node0 = value[0];
				var cdata = node0 as XmlCDataSection;
				if (cdata == null)
				{
					throw new InvalidOperationException(
							String.Format(
									"Invalid node type {0}", node0.NodeType));
				}

				Html = cdata.Data;
			}
		}
	}

}
