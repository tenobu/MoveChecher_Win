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
	public class CSV_出退時刻_つくば : CSVFile
	{
		public string 社員番号 { get; set; }
		public string 漢字氏名 { get; set; }
		public string カナ氏名 { get; set; }
		public string 所属コード { get; set; }
		public string 所属名 { get; set; }
		public string 勤怠年月日 { get; set; }
		public string 曜日 { get; set; }
		public string IC出勤時刻 { get; set; }
		public string Bulas出勤時刻 { get; set; }
		public string Bulas退勤時刻 { get; set; }
		public string IC退勤時刻 { get; set; }
		public string Bulas拘束時間 { get; set; }
		public string IC拘束時間 { get; set; }
		public string 出勤時刻の差異時間 { get; set; }
		public string 退勤時刻の差異時間 { get; set; }
		public string 勤務時間差異 { get; set; }
		
		public CSV_出退時刻_つくば()
		{
			var func_title = "CSV_出退時刻_つくば.CSV_出退時刻_つくば()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			AddColumn("社員番号");
			AddColumn("漢字氏名");
			AddColumn("カナ氏名");
			AddColumn("所属コード");
			AddColumn("所属名");
			AddColumn("勤怠年月日");
			AddColumn("曜日");
			AddColumn("IC出勤時刻");
			AddColumn("Bulas出勤時刻");
			AddColumn("Bulas退勤時刻");
			AddColumn("IC退勤時刻");
			AddColumn("Bulas拘束時間");
			AddColumn("IC拘束時間");
			AddColumn("出勤時刻の差異時間");
			AddColumn("退勤時刻の差異時間");
			AddColumn("勤務時間差異");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}
	}
}
