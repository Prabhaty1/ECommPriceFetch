using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommPrice
{
	public static class Headers
	{
		public static void CreateHeaderForAmazon(WebClient client)
		{
			client.Headers.Add("authority", "www.amazon.in");
			client.Headers.Add("content-type", "text/html");
			//client.Headers.Add("accept-encoding", "gzip");
			client.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
			client.Headers.Add("accept-language", "en-GB,en;q=0.9");
			client.Headers.Add("cache-control", "no-cache");
			client.Headers.Add("pragma", "no-cache");
			client.Headers.Add("sec-ch-ua", "\"Microsoft Edge\";v=\"113\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"");
			client.Headers.Add("sec-ch-ua-mobile", "?0");
			client.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
			client.Headers.Add("sec-fetch-dest", "document");
			client.Headers.Add("sec-fetch-mode", "navigate");
			client.Headers.Add("sec-fetch-site", "same-origin");
			client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36 Edg/113.0.1774.57");
			client.Headers.Add("x-location-context", "pincode=600005;source=IP");
			client.Headers.Add("x-meta-app", "channel=web");
			client.Headers.Add("x-requested-with", "browser");
			client.Headers.Add("Cookie", "session-id=262-4566690-6305502; i18n-prefs=INR; ubid-acbin=258-7132501-3694662; lc-acbin=en_IN; s_fid=13DCAFAF72FBD8A0-1C4C2CA41E5F7226; session-id-time=2082787201l; session-token=KQZIEVPfCT3cfE+ZKrZQ2ZHW7YUDVYGv8oEMAYJj8SAGwIQ4B9m5zk5MY7lUgM5S/8w/M5lNwpflWB/NbExjvxqW50tVPys1FHBF+ErL8kl3QbUcLTkPtybFRxwN80KTyLy2mtun7urvW2eCQW35OkGNfJh5azb2DChY1raYafnTjCQdglnVzq4nJvni67mHeGpSH1Xfnhd8KYFkvUxXyKeTP86d8FSdjyb1RhNVgPw=; csm-hit=tb:141WYRRPWK1VYT8B7H3G+s-SP6YZPGH6ZA243K5F40G|1685641893203&t:1685641893203&adb:adblk_no");

		}

		public static void CreateHeaderForMyntra(WebClient client)
		{
			client.Headers.Add("authority", "www.myntra.com");
			client.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
			client.Headers.Add("accept-language", "en-GB,en;q=0.9");
			client.Headers.Add("app", "web");
			client.Headers.Add("cache-control", "no-cache");
			client.Headers.Add("content-type", "application/json");
			client.Headers.Add("pragma", "no-cache");
			client.Headers.Add("referer", "https://www.myntra.com/mfb-clearance");
			client.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"111\", \"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"111\"");
			client.Headers.Add("sec-ch-ua-mobile", "?0");
			client.Headers.Add("sec-ch-ua-platform", "\"macOS\"");
			client.Headers.Add("sec-fetch-dest", "empty");
			client.Headers.Add("sec-fetch-mode", "cors");
			client.Headers.Add("sec-fetch-site", "same-origin");
			client.Headers.Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
			client.Headers.Add("x-location-context", "pincode=600005;source=IP");
			client.Headers.Add("x-meta-app", "channel=web");
			client.Headers.Add("x-myntra-app", "deviceID=6e83fb7f-959f-4c1f-b61d-2221347380d9;customerID=;reqChannel=web;");
			client.Headers.Add("x-myntraweb", "Yes");
			client.Headers.Add("x-requested-with", "browser");
			client.Headers.Add("Cookie", "bm_sz=4DE44E7353CD39F169289A2D70A4631B~YAAQXLopF5VqDFqHAQAAUKHoXhN1c94YAGwiv9baO1ZgCjQWe5VVzUGdmhe48t43JS93bXWoB2GvtgH9CGGIZOn7fAWj4bC3MemMvSlbjVRpcGAU2HVnvrQNG6+kxNpLntz4bZ3wQ2lJoJe7PAI+35APttsZ30idfylM3nvVz1YPsuH73Z6oN+FnyvVvMSlZIUXo+VcMMWzqpNjXo47GNOCrSjYp73PmhNFd3yF7JWWN7GH571+FOQstEFzMeAdrTJubhc8eXSNOKz/irbAQzf/dlpsHYvPhJJ96001UgrHOAfc=~4473921~3753524; _d_id=6e83fb7f-959f-4c1f-b61d-2221347380d9; mynt-eupv=1; at=ZXlKaGJHY2lPaUpJVXpJMU5pSXNJbXRwWkNJNklqRWlMQ0owZVhBaU9pSktWMVFpZlEuZXlKdWFXUjRJam9pTnpKbE5qbGhZemt0WkRWaVpDMHhNV1ZrTFdGa09EZ3RPVFkyT0RnNE5EWXpPVGxtSWl3aVkybGtlQ0k2SW0xNWJuUnlZUzB3TW1RM1pHVmpOUzA0WVRBd0xUUmpOelF0T1dObU55MDVaRFl5WkdKbFlUVmxOakVpTENKaGNIQk9ZVzFsSWpvaWJYbHVkSEpoSWl3aWMzUnZjbVZKWkNJNklqSXlPVGNpTENKbGVIQWlPakUyT1RZME56WTFNVGNzSW1semN5STZJa2xFUlVFaWZRLjRLWWtkYy1keTJ2YjJJa1BjYmxMeUhTQ1JvVkRGV3g1Y1lGMS1aODJXZTA=; mynt-ulc-api=pincode%3A600005; mynt-loc-src=expiry%3A1680929171598%7Csource%3AIP; _abck=AA126E0FA961DCDDA0B75EA4412BBFE3~0~YAAQXLopF6MwDVqHAQAATlIlXwlXPqxtoge4V/V1sQZJ3ip9Q2Bbd3q+OzD3cB2qkA2UnhpJvhcVDGivBObypQKx8v5V9xIqWSRpK1m6CfukNT9ldrxa5sfOewex+kkDJkIZlWowkowqMdyjbGBxhpYMIk6eaA+lOVTYlJbqbY5Hdh+5wM7kzbdKWzUQ4ESfb4HMGiFh+gtj60zbjTC1ZBpDTYwggSNRZZCkJa0s9dm1MWBFogiQv1gSfnfIng2Q3hCn58pNvDppzLWF5gp6sN08ugIOuoVdjxn+yR3lyi6Wuk5aOcAPuPVRU6EETj5YkRMylmgD626MM8z4hc4RToDkLHvdY91fDvm1wt1rA6uv/3H+ymJQ6BTKxYo5DPWwe+6Nd2LZC8tGjpE5ZgIGI0VcCN5hSa+n~-1~-1~1680932019; AKA_A2=A; _xsrf=tw6uie0sXgw2OFWkSpYKSA6MNsLGInSn; utrid=ABB4Rk4FZ0ZLWxRVZEFAMCM3Mjk2NDY4NjgkMg%3D%3D.c3cfeb1edf05268a56eecb97c863a14b");

		}
	}
}
