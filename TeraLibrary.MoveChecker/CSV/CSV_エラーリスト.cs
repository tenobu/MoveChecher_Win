using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

using TeraLibrary.徳島_勤怠チェック.Dic;

namespace TeraLibrary.徳島_勤怠チェック.CSV
{
	public class CSV_エラーリスト : CSVFile
	{
		public CSV_エラーリスト()
		{
			var func_title = "CSV_エラーリスト.CSV_エラーリスト()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			AddColumn("エラー区分");
			AddColumn("社員番号");
			AddColumn("フォルダ");
			AddColumn("ファイル");
			AddColumn("エラー");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}
	}
}
