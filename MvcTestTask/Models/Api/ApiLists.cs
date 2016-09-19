using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTestTask.Models.Api
{
	public class ApiLists
	{
		public List<ApiList> Lists { get; set; }

		public ApiLists()
		{
			Lists = new List<ApiList>();
		}

		public List<ApiList> GetList(string optInMode)
		{
			var result = new List<ApiList>();
			foreach (var list in Lists)
			{
				if (list.OptInMode == optInMode)
					result.Add(list);
			}

			return result;
		}
	}
}