using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTestTask.Models;
using MvcTestTask.Models.Api;

namespace MvcTestTask.Controllers
{
	public class ListsController : Controller
	{

		ESRestClient _client = new ESRestClient();

		public ActionResult Index()
		{
			ApiLists lists = _client.GetAllLists();

			Session.Add("Lists", lists);

			return View(lists);
		}

		public ActionResult Compose()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Send(Newsletter newsletter)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Compose");
			}

			var lists = Session["Lists"] as ApiLists;

			var htmlGenerator = new HtmlListsGenerator();

			newsletter.Content.Html = htmlGenerator.Generate(lists);

			var apiRequest = new ApiRequest(newsletter);

			ApiResponse response = _client.SendLists(apiRequest);

			return View(response);
		}

	}
}
