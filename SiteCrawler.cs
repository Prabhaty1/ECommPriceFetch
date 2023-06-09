﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommPrice
{
	public static class SiteCrawler
	{
		public static void GetDetailsFromAmazon(string amazonURL, Details details)
		{
			using (var client = new WebClient())
			{
				Headers.CreateHeaderForAmazon(client);

				// Download the HTML
				string html = client.DownloadString(amazonURL);
				if (string.IsNullOrEmpty(html))
				{
					details.Title = "Page Not Found";
					return;
				}

				// Now feed it to HTML Agility Pack:
				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(html);

				var titleList = doc.DocumentNode.SelectNodes("//span[@id='productTitle']");
				if (titleList != null)
				{
					details.Title = titleList.LastOrDefault().InnerText.Trim();
				}

				var mrpList = doc.DocumentNode.SelectNodes("//span[@class='a-price a-text-price']");
				if (mrpList != null)
				{
					details.MRP = mrpList.FirstOrDefault().ChildNodes[0].InnerText.Trim();
				}

				var priceList = doc.DocumentNode.SelectNodes("//span[@class='a-price-whole']");
				if (priceList != null)
				{
					details.Price = priceList.FirstOrDefault().InnerText.Trim();
				}

				CheckDetailFields(details);
			}
		}

		public static void GetDetailsFromFlipkart(string flipkartURL, Details details)
		{
			using (var client = new WebClient())
			{
				// Download the HTML
				string html = client.DownloadString(flipkartURL);
				if (string.IsNullOrEmpty(html))
				{
					details.Title = "Page Not Found";
					return;
				}

				// Now feed it to HTML Agility Pack:
				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(html);

				var titleList = doc.DocumentNode.SelectNodes("//span[@class='B_NuCI']");
				if (titleList != null)
				{
					details.Title = titleList.LastOrDefault().InnerText.Trim();
				}

				var mrpList = doc.DocumentNode.SelectNodes("//div[@class='_3I9_wc _2p6lqe']");
				if (mrpList != null)
				{
					details.MRP = mrpList.LastOrDefault().InnerText.Trim();
				}

				var priceList = doc.DocumentNode.SelectNodes("//div[@class='_30jeq3 _16Jk6d']");
				if (priceList != null)
				{
					details.Price = priceList.LastOrDefault().InnerText.Trim();
				}

				CheckDetailFields(details);
			}
		}

		public static void GetDetailsFromMyntra(string MyntraURL, Details details)
		{
			using (var client = new WebClient())
			{
				Headers.CreateHeaderForMyntra(client);

				// Download the HTML
				string html = client.DownloadString(MyntraURL);
				if (string.IsNullOrEmpty(html))
				{
					details.Title = "Page Not Found";
					return;
				}

				//// Now feed it to HTML Agility Pack:
				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(html);

				var a = Newtonsoft.Json.Linq.JObject.Parse(doc.DocumentNode.SelectNodes("//script[@type='application/ld+json']").ToList()[1].InnerText);
				if (a != null)
				{
					details.Title = a["description"].ToString();
					details.Price = a["offers"]["price"].ToString();
				}

				var b = Newtonsoft.Json.Linq.JObject.Parse(doc.DocumentNode.SelectNodes("//body[@oncontextmenu='return!1']").ToList().FirstOrDefault().ChildNodes[5].InnerText.Substring(14));
				if (b != null)
				{
					details.MRP = b["pdpData"]["mrp"].ToString();
				}

				CheckDetailFields(details);
			}
		}

		private static void CheckDetailFields(Details details)
		{
			if (string.IsNullOrEmpty(details.Title))
			{
				details.Title = "Page Not Found";
			}
			if (string.IsNullOrEmpty(details.MRP) && !string.IsNullOrEmpty(details.Price))
			{
				details.MRP = details.Price;
			}
			if (string.IsNullOrEmpty(details.MRP) && string.IsNullOrEmpty(details.Price))
			{
				details.MRP = details.Price = "Not Available";
			}
		}
	}
}
