using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Web.Routing;
using MvcTestTask.Models.Api;

namespace MvcTestTask.Models
{
	public class ESRestClient
	{
		/// <summary>
		/// Base URL of API.
		/// </summary>
		private const string BASE_URL = "https://api.esv2.com";

		/// <summary>
		/// Secret key for access to API.
		/// </summary>
		private readonly string _secretKey = "9DfWEl04zzEcnWsEc6mx";

		/// <summary>
		/// Execute a REST request.
		/// </summary>
		/// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
		/// <param name="request">The RestRequest to execute.</param>
		/// <returns></returns>
		public T Execute<T>(RestRequest request) where T : new()
		{
			var client = new RestClient();
			client.BaseUrl = new Uri(BASE_URL);

			request.AddParameter("SecretKey", _secretKey, ParameterType.UrlSegment);

			var response = client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var appException = new ApplicationException(message, response.ErrorException);
				throw appException;
			}
			return response.Data;
		}

		/// <summary>
		/// Makes a GET request to the Lists resource.
		/// </summary>
		/// <returns>Returns a paged list of lists from the account.</returns>
		public ApiLists GetAllLists()
		{
			var request = new RestRequest();
			request.Resource = "Api/Lists?apiKey={SecretKey}";

			return Execute<ApiLists>(request);
		}

		/// <summary>
		/// Makes a POST request to the Newsletter resource.
		/// </summary>
		/// <param name="apiRequest">Request data.</param>
		/// <returns>Returns a key of created newsletter</returns>
		public ApiResponse SendLists(ApiRequest apiRequest)
		{
			apiRequest.ApiKey = _secretKey;

			var request = new RestRequest("Api/Newsletters", Method.POST);
			request.RequestFormat = DataFormat.Xml;
			request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
			request.AddBody(apiRequest);

			return Execute<ApiResponse>(request);
		}

	}

}
