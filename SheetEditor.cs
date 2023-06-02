using Spire.Xls;
using Spire.Xls.Core;
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
		public static void FillAmazonSheet(Workbook workbook)
		{
			//Initialize worksheet for Amazon Sheet
			Worksheet workSheet = workbook.Worksheets["Amazon"];
			var amazonURL = workSheet.Columns[0].ToList();
			var details = new Details();
			Console.WriteLine("Working on Amazon Sheet");

			workSheet.Columns[2].NumberFormat = "₹#,##0.00";
			workSheet.Columns[3].NumberFormat = "₹#,##0.00";

			for (int i = 1; i < amazonURL.Count; i = i + 5)
			{
				Parallel.Invoke(
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, amazonURL, i, "Amazon"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, amazonURL, i + 1, "Amazon"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, amazonURL, i + 2, "Amazon"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, amazonURL, i + 3, "Amazon"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, amazonURL, i + 4, "Amazon")
				);
			}
		}

		public static void FillFlipkartSheet(Workbook workbook)
		{
			//Initialize worksheet for Flipkart Sheet
			Worksheet workSheet = workbook.Worksheets["Flipkart"];
			var flipkartURL = workSheet.Columns[0].ToList();
			Details details = new Details();
			Console.WriteLine("Working on Flipkart Sheet");

			workSheet.Columns[2].NumberFormat = "₹#,##0.00";
			workSheet.Columns[3].NumberFormat = "₹#,##0.00";

			for (int i = 1; i < flipkartURL.Count; i = i + 5)
			{
				Parallel.Invoke(
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, flipkartURL, i, "Flipkart"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, flipkartURL, i + 1, "Flipkart"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, flipkartURL, i + 2, "Flipkart"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, flipkartURL, i + 3, "Flipkart"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, flipkartURL, i + 4, "Flipkart")
				);
			}
		}

		public static void FillMyntraSheet(Workbook workbook)
		{
			// HTML not working.
			//Initialize worksheet for Myntra Sheet
			Worksheet workSheet = workbook.Worksheets["Myntra"];
			var myntraURL = workSheet.Columns[0].ToList();
			Details details = new Details();
			Console.WriteLine("Working on Myntra Sheet");

			workSheet.Columns[2].NumberFormat = "₹#,##0.00";
			workSheet.Columns[3].NumberFormat = "₹#,##0.00";

			for (int i = 1; i < myntraURL.Count; i = i + 5)
			{
				Parallel.Invoke(
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, myntraURL, i, "Myntra"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, myntraURL, i + 1, "Myntra"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, myntraURL, i + 2, "Myntra"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, myntraURL, i + 3, "Myntra"),
					() => new GetAndSaveData().GetAndSaveDetailsToExcelSheet(workSheet, myntraURL, i + 4, "Myntra")
				);
			}
		}

	}
}
