using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MvcTestTask.Models.Api
{
	[XmlRoot("Data")]
	public class Newsletter
	{
		public Recipients Recipients { get; set; }
		public Content Content { get; set; }
	}
}