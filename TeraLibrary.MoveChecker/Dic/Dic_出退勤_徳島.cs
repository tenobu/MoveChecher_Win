using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	// Dic_社員名簿
	// 
	public class Dic_出退勤_徳島 : Dic_ListList// Dic_PlusPlus
	{
		public string
			社員番号,	漢字氏名, かな氏名, 属性2, 属性3, 時刻, 種別, カードデータ;


		public Dic_出退勤_徳島()
		{
			//var func_title = "Dic_出退勤_徳島.Dic_出退勤_徳島()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "H";
			topCount = 2;
			columnCount = AlphabetTo.ToInt(alphabet);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// AddListDatas
		//
		protected override void AddListDatas(List<string> list)
		{
			//var func_title = "Dic_出退勤_徳島.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(かな氏名);
			list.Add(属性2);
			list.Add(属性3);
			list.Add(時刻);
			list.Add(種別);
			list.Add(カードデータ);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_出退勤_徳島.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号     = ToString(list[ 0]);
			漢字氏名     = ToString(list[ 1]);
			かな氏名     = ToString(list[ 2]);
			属性2        = ToString(list[ 3]);
			属性3        = ToString(list[ 4]);
			時刻         = ToString(list[ 5]);
			種別         = ToString(list[ 6]);
			カードデータ = ToString(list[ 7]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退勤_徳島.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(かな氏名);
			list.Add(属性2);
			list.Add(属性3);
			list.Add(時刻);
			list.Add(種別);
			list.Add(カードデータ);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
