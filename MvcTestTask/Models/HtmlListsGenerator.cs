using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using RestSharp;
using System.IO;
using MvcTestTask.Models.Api;

namespace MvcTestTask.Models
{
	public class HtmlListsGenerator
	{
		private static readonly string[] _tableHeaders = { Constants.TH_ID, Constants.TH_NAME, Constants.TH_FRIENDLY_NAME, Constants.TH_LANGUAGE, Constants.TH_OPT_MODE };

		/// <summary>
		/// Generate table header in HtmlTextWriter object.
		/// </summary>
		/// <param name="writer">Writer.</param>
		public void GenerateTableHeader(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);

			for (int i = 0; i < _tableHeaders.Length; i++)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Th);
				writer.Write(_tableHeaders[i]);
				writer.RenderEndTag();
			}

			writer.RenderEndTag();
			writer.WriteLine();
		}

		/// <summary>
		/// Generate table body in HtmlTextWriter object.
		/// Put list to a table.
		/// </summary>
		/// <param name="writer">Writer.</param>
		/// <param name="lists">List of ApiList.</param>
		public void GenerateTableBody(HtmlTextWriter writer, List<ApiList> lists)
		{
			if (lists != null)
			{
				foreach (var list in lists)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);

					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.Write(list.Id);
					writer.RenderEndTag();

					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.Write(list.Name);
					writer.RenderEndTag();

					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.Write(list.FriendlyName);
					writer.RenderEndTag();

					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.Write(list.Language);
					writer.RenderEndTag();

					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.Write(list.OptInMode);
					writer.RenderEndTag();

					writer.RenderEndTag();
					writer.WriteLine();
				}
			}
		}

		/// <summary>
		/// Generate HTML document in string format which contains lists.
		/// </summary>
		/// <param name="lists">Lists to add on page.</param>
		/// <returns>String of HTML doc.</returns>
		public string Generate(ApiLists lists)
		{
			try
			{
				var stringWriter = new StringWriter();

				var writer = new HtmlTextWriter(stringWriter);
	
				//<html>
				writer.RenderBeginTag(HtmlTextWriterTag.Html);
				//<body>
				writer.RenderBeginTag(HtmlTextWriterTag.Body);
				//<head>
				writer.RenderBeginTag(HtmlTextWriterTag.Head);
				writer.AddAttribute("http-equiv", "Content-Type");
				writer.AddAttribute(HtmlTextWriterAttribute.Content, "text/html; charset=UTF-8");
				//<meta>
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.WriteLine();

				writer.RenderBeginTag(HtmlTextWriterTag.Title);
				writer.Write("Lists");
				writer.RenderEndTag();

				//</head>
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.WriteLine();

				writer.AddAttribute(HtmlTextWriterAttribute.Style, "overflow: hidden; _zoom: 1");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

				writer.AddAttribute(HtmlTextWriterAttribute.Style, "float: left; width: 50%");
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				this.GenerateTableHeader(writer);
				var singleOptList = lists.GetList(Constants.SINGLE_OPT_IN);
				this.GenerateTableBody(writer, singleOptList);

				//</table>
				writer.RenderEndTag();

				writer.AddAttribute(HtmlTextWriterAttribute.Style, "float: right; width: 50%");
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				this.GenerateTableHeader(writer);
				var doubleOptList = lists.GetList(Constants.DOUBLE_OPT_IN);
				this.GenerateTableBody(writer, doubleOptList);
				//</table>
				writer.RenderEndTag();
				//</div>
				writer.RenderEndTag();
				//</body>
				writer.RenderEndTag();
				//</html>
				writer.RenderEndTag();

				string result = stringWriter.ToString();

				stringWriter.Close();

				return result;
			}
			catch (IOException e)
			{
				return e.Message;
			}
			catch (UnauthorizedAccessException e)
			{
				return e.Message;
			}
		}
	}
}