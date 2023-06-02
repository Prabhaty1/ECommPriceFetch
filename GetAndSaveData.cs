using Spire.Xls.Core;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommPrice
{
	public class GetAndSaveData
	{
		public void GetAndSaveDetailsToExcelSheet(Worksheet workSheet, List<IXLSRange> urlList, int i, string sheetName)
		{
			try
			{
				if (i >= urlList.Count)
				{
					return;
				}

				Details details = new Details();
				if (!string.IsNullOrEmpty(urlList[i].Text))
				{
					switch (sheetName)
					{
						case "Amazon" :
							SiteCrawler.GetDetailsFromAmazon(urlList[i].Text.Trim(), details);
							break;
						case "Flipkart" :
							SiteCrawler.GetDetailsFromFlipkart(urlList[i].Text.Trim(), details);
							break;
						case "Myntra" :
							SiteCrawler.GetDetailsFromMyntra(urlList[i].Text.Trim(), details);
							break;
						default: 
							return;
					}

					if (details != null)
					{
						workSheet["B" + (i + 1)].Style.Font.Color = Color.Black;
						workSheet["B" + (i + 1)].Value = details.Title;

						workSheet["C" + (i + 1)].Value = details.MRP;
						workSheet["D" + (i + 1)].Value = details.Price;
						Console.WriteLine(string.Format(i + " Row Updated ({0})", sheetName));
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Failed for Sheet {0}, URL at [A:{1}], Error : {2}", sheetName, (i + 1), ex.Message));
				workSheet["B" + (i + 1)].Value = ex.Message;
				workSheet["B" + (i + 1)].Style.Font.Color = Color.FromArgb(204, 0, 0);
				workSheet["C" + (i + 1)].Value = "";
				workSheet["D" + (i + 1)].Value = "";
			}
		}
	}
}
