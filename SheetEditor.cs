using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommPrice
{
	public static class SheetEditor
	{
		public static void FillMyntraSheet(Workbook workbook, string filePath)
		{
			// HTML not working.
			//Initialize worksheet for Myntra Sheet
			Worksheet workSheet = workbook.Worksheets["Myntra"];
			var myntraURL = workSheet.Columns[0].ToList();
			Details details = new Details();
			Console.WriteLine("Working on Myntra Sheet");

			workSheet.Columns[2].NumberFormat = "₹#,##0.00";
			workSheet.Columns[3].NumberFormat = "₹#,##0.00";
			for (int i = 1; i < myntraURL.Count; i++)
			{
				try
				{
					if (!string.IsNullOrEmpty(myntraURL[i].Text))
					{
						SiteCrawler.GetDetailsFromMyntra(myntraURL[i].Text.Trim(), details);
						if (details != null)
						{
							workSheet["B" + (i + 1)].Style.Color = Color.White;
							workSheet["B" + (i + 1)].Value = details.Title;

							workSheet["C" + (i + 1)].Value = details.MRP;
							workSheet["D" + (i + 1)].Value = details.Price;
							Console.WriteLine(i + " Row Updated (Myntra)");
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Format("Failed for Sheet Myntra, URL at [A:{0}], Error : {1}", (i + 1), ex.Message));
					workSheet["B" + (i + 1)].Value = "Error while fetching details";
					workSheet["B" + (i + 1)].Style.Color = Color.FromArgb(204, 0, 0);
				}
			}
		}

		public static void FillFlipkartSheet(Workbook workbook, string filePath)
		{
			//Initialize worksheet for Flipkart Sheet
			Worksheet workSheet = workbook.Worksheets["Flipkart"];
			var flipkartURL = workSheet.Columns[0].ToList();
			Details details = new Details();
			Console.WriteLine("Working on Flipkart Sheet");

			workSheet.Columns[2].NumberFormat = "₹#,##0.00";
			workSheet.Columns[3].NumberFormat = "₹#,##0.00";
			for (int i = 1; i < flipkartURL.Count; i++)
			{
				try
				{
					if (!string.IsNullOrEmpty(flipkartURL[i].Text))
					{
						SiteCrawler.GetDetailsFromFlipkart(flipkartURL[i].Text.Trim(), details);
						if (details != null)
						{
							workSheet["B" + (i + 1)].Style.Color = Color.White;
							workSheet["B" + (i + 1)].Value = details.Title;

							workSheet["C" + (i + 1)].Value = details.MRP;
							workSheet["D" + (i + 1)].Value = details.Price;
							Console.WriteLine(i + " Row Updated (Flipkart)");
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Format("Failed for Sheet Flipkart, URL at [A:{0}], Error : {1}", (i + 1), ex.Message));
					workSheet["B" + (i + 1)].Value = "Error while fetching details";
					workSheet["B" + (i + 1)].Style.Color = Color.FromArgb(204, 0, 0);
				}
			}
		}

		public static void FillAmazonSheet(Workbook workbook, string filePath)
		{
			//Initialize worksheet for Amazon Sheet
			Worksheet workSheet = workbook.Worksheets["Amazon"];
			var amazonURL = workSheet.Columns[0].ToList();
			var details = new Details();
			Console.WriteLine("Working on Amazon Sheet");

			workSheet.Columns[2].NumberFormat = "₹#,##0.00";
			workSheet.Columns[3].NumberFormat = "₹#,##0.00";
			for (int i = 1; i < amazonURL.Count; i++)
			{
				try
				{
					if (!string.IsNullOrEmpty(amazonURL[i].Text))
					{
						SiteCrawler.GetDetailsFromAmazon(amazonURL[i].Text.Trim(), details);
						if (details != null)
						{
							workSheet["B" + (i + 1)].Style.Color = Color.White;
							workSheet["B" + (i + 1)].Value = details.Title;

							workSheet["C" + (i + 1)].Value = details.MRP;
							workSheet["D" + (i + 1)].Value = details.Price;
							Console.WriteLine(i + " Row Updated (Amazon)");
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Format("Failed for Sheet Amazon, URL at [A:{0}], Error : {1}", (i + 1), ex.Message));
					workSheet["B" + (i + 1)].Value = "Error while fetching details";
					workSheet["B" + (i + 1)].Style.Color = Color.FromArgb(204, 0, 0);
				}
			}
		}

	}
}
