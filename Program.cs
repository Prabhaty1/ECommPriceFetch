using HtmlAgilityPack;
using Spire.Xls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Text;

namespace ECommPrice
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Enter Excel File path.");
				var filePath = Console.ReadLine();
				while (!Path.IsPathRooted(filePath) && !File.Exists(filePath))
				{
					Console.WriteLine("Enter full path");
					filePath = Console.ReadLine();
				}

				//Create a new workbook
				Workbook workbook = new Workbook();
				//Load a file and imports its data
				workbook.LoadFromFile(filePath);
				Console.WriteLine("Excel File Loaded");

				//Initialize worksheet for Amazon Sheet
				Worksheet workSheet = workbook.Worksheets["Amazon"];
				var amazonURL = workSheet.Columns[0].ToList();
				var details = new Details();
				Console.WriteLine("Working on Amazon Sheet");

				workSheet.Columns[2].NumberFormat = "₹#,##0.00";
				workSheet.Columns[3].NumberFormat = "₹#,##0.00";
				for (int i = 1; i < amazonURL.Count; i++)
				{
					if (!string.IsNullOrEmpty(amazonURL[i].Text))
					{
						GetDetailsFromAmazon(amazonURL[i].Text.Trim(), details);
						if (details != null)
						{
							workSheet["B" + (i + 1)].Value = details.Title;
							workSheet["C" + (i + 1)].Value = details.MRP;
							workSheet["D" + (i + 1)].Value = details.Price;
							Console.WriteLine(i + " Row Updated.");
						}
					}
				}
				workbook.SaveToFile(filePath);

				//Initialize worksheet for Flipkart Sheet
				workSheet = workbook.Worksheets["Flipkart"];
				var flipkartURL = workSheet.Columns[0].ToList();
				details = new Details();
				Console.WriteLine("Working on Flipkart Sheet");

				workSheet.Columns[2].NumberFormat = "₹#,##0.00";
				workSheet.Columns[3].NumberFormat = "₹#,##0.00";
				for (int i = 1; i < flipkartURL.Count; i++)
				{
					if (!string.IsNullOrEmpty(flipkartURL[i].Text))
					{
						GetDetailsFromFlipkart(flipkartURL[i].Text.Trim(), details);
						if (details != null)
						{
							workSheet["B" + (i + 1)].Value = details.Title;
							workSheet["C" + (i + 1)].Value = details.MRP;
							workSheet["D" + (i + 1)].Value = details.Price;
							Console.WriteLine(i + " Row Updated.");
						}
					}
				}
				workbook.SaveToFile(filePath);

				// HTML not working.
				//Initialize worksheet for Myntra Sheet
				workSheet = workbook.Worksheets["Myntra"];
				var myntraURL = workSheet.Columns[0].ToList();
				details = new Details();
				Console.WriteLine("Working on Myntra Sheet");

				workSheet.Columns[2].NumberFormat = "₹#,##0.00";
				workSheet.Columns[3].NumberFormat = "₹#,##0.00";
				for (int i = 1; i < myntraURL.Count; i++)
				{
					if (!string.IsNullOrEmpty(myntraURL[i].Text))
					{
						GetDetailsFromMyntra(myntraURL[i].Text.Trim(), details);
						if (details != null)
						{
							workSheet["B" + (i + 1)].Value = details.Title;
							workSheet["C" + (i + 1)].Value = details.MRP;
							workSheet["D" + (i + 1)].Value = details.Price;
							Console.WriteLine(i + " Row Updated.");
						}
					}
				}
				workbook.SaveToFile(filePath);

				Console.WriteLine("Excel Updated");

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		private static void GetDetailsFromAmazon(string amazonURL, Details details)
		{
			using (var client = new WebClient())
			{
				// Download the HTML
				//string html = client.DownloadString(amazonURL);

				string html = GetHtmlString(amazonURL, "amazon");

				// Now feed it to HTML Agility Pack:
				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(html);

				var titleList = doc.DocumentNode.SelectNodes("//span[@id='productTitle']").ToList();
				if (titleList != null)
				{
					details.Title = titleList.LastOrDefault().InnerText.Trim();
				}

				var mrpList = doc.DocumentNode.SelectNodes("//span[@class='a-price a-text-price']").ToList();
				if (mrpList != null)
				{
					details.MRP = mrpList.FirstOrDefault().ChildNodes[0].InnerText.Trim();
				}

				var priceList = doc.DocumentNode.SelectNodes("//span[@class='a-price-whole']").ToList();
				if (priceList != null)
				{
					details.Price = priceList.FirstOrDefault().InnerText.Trim();
				}
			}
		}

		private static void GetDetailsFromFlipkart(string flipkartURL, Details details)
		{
			using (var client = new WebClient())
			{
				// Download the HTML
				string html = client.DownloadString(flipkartURL);

				// Now feed it to HTML Agility Pack:
				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(html);

				var titleList = doc.DocumentNode.SelectNodes("//span[@class='B_NuCI']").ToList();
				if (titleList != null)
				{
					details.Title = titleList.LastOrDefault().InnerText.Trim();
				}

				var mrpList = doc.DocumentNode.SelectNodes("//div[@class='_3I9_wc _2p6lqe']").ToList();
				if (mrpList != null)
				{
					details.MRP = mrpList.LastOrDefault().InnerText.Trim();
				}

				var priceList = doc.DocumentNode.SelectNodes("//div[@class='_30jeq3 _16Jk6d']").ToList();
				if (priceList != null)
				{
					details.Price = priceList.LastOrDefault().InnerText.Trim();
				}
			}
		}

		private static void GetDetailsFromMyntra(string MyntraURL, Details details)
		{
			using (var client = new WebClient())
			{
				//// Download the HTML
				//html = client.DownloadString(MyntraURL);

				//// Now feed it to HTML Agility Pack:
				string html = GetHtmlString(MyntraURL, "myntra");

				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(html);

				var titleList = doc.DocumentNode.SelectNodes("//body[@oncontextmenu='return!1']").ToList();

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
			}
		}

		public static string GetHtmlString(string url, string website)
		{
			HttpWebResponse response = null;

			if (website == "myntra")
			{
				response = GetHttpWebResponseForMyntra(url);
			}
			if (website == "amazon")
			{
				response = GetHttpWebResponseForAmazon(url);
			}

			Encoding encoding = Encoding.UTF8;
			using (var reader = new StreamReader(response.GetResponseStream(), encoding))
			{
				string responseText = reader.ReadToEnd();
				return responseText;
			}
		}

		private static HttpWebResponse? GetHttpWebResponseForAmazon(string url)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.ContentType = "text/html";
			webRequest.AutomaticDecompression = DecompressionMethods.GZip;
			webRequest.Headers.Add("authority", "www.amazon.in");
			webRequest.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
			webRequest.Headers.Add("accept-language", "en-GB,en;q=0.9");
			//webRequest.Headers.Add("app", "web");
			webRequest.Headers.Add("cache-control", "no-cache");
			//webRequest.Headers.Add("content-type", "text/xml");
			webRequest.Headers.Add("pragma", "no-cache");
			//webRequest.Headers.Add("referer", "https://www.myntra.com/mfb-clearance");
			webRequest.Headers.Add("sec-ch-ua", "\"Microsoft Edge\";v=\"113\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"");
			webRequest.Headers.Add("sec-ch-ua-mobile", "?0");
			webRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
			webRequest.Headers.Add("sec-fetch-dest", "document");
			webRequest.Headers.Add("sec-fetch-mode", "navigate");
			webRequest.Headers.Add("sec-fetch-site", "same-origin");
			webRequest.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36 Edg/113.0.1774.57");
			webRequest.Headers.Add("x-location-context", "pincode=600005;source=IP");
			webRequest.Headers.Add("x-meta-app", "channel=web");
			webRequest.Headers.Add("x-requested-with", "browser");

			webRequest.Headers.Add("Cookie", "session-id=262-4566690-6305502; i18n-prefs=INR; ubid-acbin=258-7132501-3694662; lc-acbin=en_IN; _rails-root_session=UHVIMEpaQjI1K1NUQTQrUlNSa09QQ1BHcTltZy9QWk5YV05LWjNlL2ZJNnZPZ2tBRklJcThodTJqcnFUTnZaV3VXM3pXdXFIT2h2bllSWUYrRmJQZEkreU1vK2tOWlZLbyszYmwrZWcvTzFGcEtKMDNON3k5akxGdXp5QnVSb0p5THdna1MwbW8rSFdSQUFpNnJVdmNtdTMyOE5PV1RoekVKb01hQ2tIUkpVNFZ6OUZodTdVcjJPank3R1lHcEdnLS1KZ0RkWGJuQm0vTzR5K1dhRTBoTkp3PT0%3D--e7934edf4c8e20c5924daaa55f8ed22ff43716c0; s_fid=13DCAFAF72FBD8A0-1C4C2CA41E5F7226; s_cc=true; session-id-time=2082787201l; session-token=\"xZkE7IfCq60CSvxrnEUVJTlHXBbrnOmTQYiTV9r1Tag3/9OYhQEyEY+t6SPSYfmgambRdf58034L+NCLnLwRGs8c0uCbvdF3JZYF+CwuJMRVN62C9ByAdesbNUQEPai+cPfZdOwBsTyWQ05LWyun23KAzEIIeN5B77k8lX8uPivMqwswkQzjuy1Li1BgbU3tWrfAbp4ykBIKrE3pGE+t2f5an2G9aAM1I7xROUFH7pg=\"; csm-hit=tb:s-YRRY0Z542TCAPEBQ1B42|1685466514904&t:1685466514905&adb:adblk_no");

			try
			{
				return (HttpWebResponse)webRequest.GetResponse();
			}
			catch (WebException e)
			{
				if (e.Response == null)
					throw new Exception("Cannot get response");
				return (HttpWebResponse)e.Response;
			}
		}

		public static HttpWebResponse GetHttpWebResponseForMyntra(string url)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.ContentType = "application/json";
			webRequest.AutomaticDecompression = DecompressionMethods.GZip;
			webRequest.Headers.Add("authority", "www.myntra.com");
			webRequest.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
			webRequest.Headers.Add("accept-language", "en-GB,en;q=0.9");
			webRequest.Headers.Add("app", "web");
			webRequest.Headers.Add("cache-control", "no-cache");
			//webRequest.Headers.Add("content-type", "application/json");
			webRequest.Headers.Add("pragma", "no-cache");
			webRequest.Headers.Add("referer", "https://www.myntra.com/mfb-clearance");
			webRequest.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"111\", \"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"111\"");
			webRequest.Headers.Add("sec-ch-ua-mobile", "?0");
			webRequest.Headers.Add("sec-ch-ua-platform", "\"macOS\"");
			webRequest.Headers.Add("sec-fetch-dest", "empty");
			webRequest.Headers.Add("sec-fetch-mode", "cors");
			webRequest.Headers.Add("sec-fetch-site", "same-origin");
			webRequest.Headers.Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
			webRequest.Headers.Add("x-location-context", "pincode=600005;source=IP");
			webRequest.Headers.Add("x-meta-app", "channel=web");
			webRequest.Headers.Add("x-myntra-app", "deviceID=6e83fb7f-959f-4c1f-b61d-2221347380d9;customerID=;reqChannel=web;");
			webRequest.Headers.Add("x-myntraweb", "Yes");
			webRequest.Headers.Add("x-requested-with", "browser");

			webRequest.Headers.Add("Cookie", "bm_sz=4DE44E7353CD39F169289A2D70A4631B~YAAQXLopF5VqDFqHAQAAUKHoXhN1c94YAGwiv9baO1ZgCjQWe5VVzUGdmhe48t43JS93bXWoB2GvtgH9CGGIZOn7fAWj4bC3MemMvSlbjVRpcGAU2HVnvrQNG6+kxNpLntz4bZ3wQ2lJoJe7PAI+35APttsZ30idfylM3nvVz1YPsuH73Z6oN+FnyvVvMSlZIUXo+VcMMWzqpNjXo47GNOCrSjYp73PmhNFd3yF7JWWN7GH571+FOQstEFzMeAdrTJubhc8eXSNOKz/irbAQzf/dlpsHYvPhJJ96001UgrHOAfc=~4473921~3753524; _d_id=6e83fb7f-959f-4c1f-b61d-2221347380d9; mynt-eupv=1; at=ZXlKaGJHY2lPaUpJVXpJMU5pSXNJbXRwWkNJNklqRWlMQ0owZVhBaU9pSktWMVFpZlEuZXlKdWFXUjRJam9pTnpKbE5qbGhZemt0WkRWaVpDMHhNV1ZrTFdGa09EZ3RPVFkyT0RnNE5EWXpPVGxtSWl3aVkybGtlQ0k2SW0xNWJuUnlZUzB3TW1RM1pHVmpOUzA0WVRBd0xUUmpOelF0T1dObU55MDVaRFl5WkdKbFlUVmxOakVpTENKaGNIQk9ZVzFsSWpvaWJYbHVkSEpoSWl3aWMzUnZjbVZKWkNJNklqSXlPVGNpTENKbGVIQWlPakUyT1RZME56WTFNVGNzSW1semN5STZJa2xFUlVFaWZRLjRLWWtkYy1keTJ2YjJJa1BjYmxMeUhTQ1JvVkRGV3g1Y1lGMS1aODJXZTA=; mynt-ulc-api=pincode%3A600005; mynt-loc-src=expiry%3A1680929171598%7Csource%3AIP; _abck=AA126E0FA961DCDDA0B75EA4412BBFE3~0~YAAQXLopF6MwDVqHAQAATlIlXwlXPqxtoge4V/V1sQZJ3ip9Q2Bbd3q+OzD3cB2qkA2UnhpJvhcVDGivBObypQKx8v5V9xIqWSRpK1m6CfukNT9ldrxa5sfOewex+kkDJkIZlWowkowqMdyjbGBxhpYMIk6eaA+lOVTYlJbqbY5Hdh+5wM7kzbdKWzUQ4ESfb4HMGiFh+gtj60zbjTC1ZBpDTYwggSNRZZCkJa0s9dm1MWBFogiQv1gSfnfIng2Q3hCn58pNvDppzLWF5gp6sN08ugIOuoVdjxn+yR3lyi6Wuk5aOcAPuPVRU6EETj5YkRMylmgD626MM8z4hc4RToDkLHvdY91fDvm1wt1rA6uv/3H+ymJQ6BTKxYo5DPWwe+6Nd2LZC8tGjpE5ZgIGI0VcCN5hSa+n~-1~-1~1680932019; AKA_A2=A; _xsrf=tw6uie0sXgw2OFWkSpYKSA6MNsLGInSn; utrid=ABB4Rk4FZ0ZLWxRVZEFAMCM3Mjk2NDY4NjgkMg%3D%3D.c3cfeb1edf05268a56eecb97c863a14b");

			try
			{
				return (HttpWebResponse)webRequest.GetResponse();
			}
			catch (WebException e)
			{
				if (e.Response == null)
					throw new Exception("Cannot get response");
				return (HttpWebResponse)e.Response;
			}
		}
	}
}