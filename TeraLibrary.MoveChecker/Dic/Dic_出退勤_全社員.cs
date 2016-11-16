using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	public class Dic_出退勤_全社員 : Dic_ListList// Dic_PlusPlus// Dictionary<string, int>
	{
		public string
			会社コード, 給与支払年月, 勤怠年月, 社員番号, 氏名, 所属コード, 名称, 日付, 曜日区分, 出勤時刻_入力,
			退勤時刻_入力, 法定超残業時間, 分類コード;


		public Dic_出退勤_全社員()
		{
			//var func_title = "Dic_出退勤_全社員.Dic_出退勤_全社員()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "M";
			topCount = 1;
			columnCount = AlphabetTo.ToInt(alphabet);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		public void SetInit()
		{
			//var func_title = "Dic_出退勤_全社員.SetInit()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			会社コード = 給与支払年月 = 勤怠年月 = 社員番号 = 氏名 = 所属コード = 名称 = 日付 = 曜日区分 =
				出勤時刻_入力 = 退勤時刻_入力 = 法定超残業時間 = 分類コード = "";

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// AddListDatas
		//
		protected override void AddListDatas(List<string> list)
		{
			//var func_title = "Dic_出退勤_全社員.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(会社コード);
			list.Add(給与支払年月);
			list.Add(勤怠年月);
			list.Add(社員番号);
			list.Add(氏名);
			list.Add(所属コード);
			list.Add(名称);
			list.Add(日付);
			list.Add(曜日区分);
			list.Add(出勤時刻_入力);
			list.Add(退勤時刻_入力);
			list.Add(法定超残業時間);
			list.Add(分類コード);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_出退勤_全社員.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			会社コード     = ToString(list[ 0]);
			給与支払年月   = ToString(list[ 1]);
			勤怠年月       = ToString(list[ 2]);
			社員番号       = ToString(list[ 3]);
			氏名           = ToString(list[ 4]);
			所属コード     = ToString(list[ 5]);
			名称           = ToString(list[ 6]);
			日付           = ToString(list[ 7]);
			曜日区分       = ToString(list[ 8]);
			出勤時刻_入力  = ToString(list[ 9]);
			退勤時刻_入力  = ToString(list[10]);
			法定超残業時間 = ToString(list[11]);
			分類コード     = ToString(list[12]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退勤_全社員.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(会社コード);
			list.Add(給与支払年月);
			list.Add(勤怠年月);
			list.Add(社員番号);
			list.Add(氏名);
			list.Add(所属コード);
			list.Add(名称);
			list.Add(日付);
			list.Add(曜日区分);
			list.Add(出勤時刻_入力);
			list.Add(退勤時刻_入力);
			list.Add(法定超残業時間);
			list.Add(分類コード);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}