﻿using HtmlAgilityPack;
using Spire.Xls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
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
				Console.WriteLine("Enter Excel File path and close the file before proceeding.");
				var filePath = Console.ReadLine();
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
				() => SheetEditor.FillAmazonSheet(workbook, filePath),
				() => SheetEditor.FillFlipkartSheet(workbook, filePath),
				() => SheetEditor.FillMyntraSheet(workbook, filePath)
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