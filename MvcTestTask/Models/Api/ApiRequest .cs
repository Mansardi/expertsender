using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp.Serializers;
using System.Xml.Serialization;

namespace MvcTestTask.Models.Api
{
	[XmlInclude(typeof(Newsletter))]
	public class ApiRequest
	{
		public string ApiKey { get; set; }
		public object Data { get; set; }

		public ApiRequest() { }

		public ApiRequest(object data)
		{
			Data = data;
		}
	}
}