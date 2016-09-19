using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MvcTestTask.Models.Api
{
	public class ApiList
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FriendlyName { get; set; }
		public string Language { get; set; }
		public string OptInMode { get; set; }
	}
}