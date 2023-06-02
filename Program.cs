using HtmlAgilityPack;
using Spire.Xls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ECommPrice
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DateTime licenseTill = new DateTime(2023, 06, 30);
			if (DateTime.Now > licenseTill)
			{
				Console.WriteLine("License Expired.");
				return;
			}
			else
			{
				Console.WriteLine(string.Format("License valid till {0}.", licenseTill.Date.ToShortDateString()));
			}

			try
			{
				Console.WriteLine("Enter Excel File path and close the file before proceeding.");
				var filePath = Console.ReadLine();

				// Validates file path
				if (Regex.Matches(filePath, "^\".*\"$").Count == 1)
				{
					filePath =  filePath.Substring(1, filePath.Length - 2);
				}
				while (!Path.IsPathRooted(filePath) && !File.Exists(filePath))
				{
					Console.WriteLine("Enter full path");
					filePath = Console.ReadLine();
				}

				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				//Create a new workbook
				Workbook workbook = new Workbook();
				//Load a file and imports its data
				workbook.LoadFromFile(filePath);
				Console.WriteLine("Excel File Loaded");

				Parallel.Invoke(
					() => SheetEditor.FillAmazonSheet(workbook),
					() => SheetEditor.FillFlipkartSheet(workbook),
					() => SheetEditor.FillMyntraSheet(workbook)
				);

				workbook.SaveToFile(filePath);
				Console.WriteLine("Excel Updated");

				stopwatch.Stop();

				Console.WriteLine(string.Format("Time elapsed : {0}", stopwatch.Elapsed));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}
	}
}