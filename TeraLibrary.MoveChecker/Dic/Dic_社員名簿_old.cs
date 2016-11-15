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
	public class Dic_社員名簿_old : Dic_ListList // Dic_PlusPlus
	{
		public string
			社員番号 = "", 漢字氏名, カナ氏名, 退職日付, 退職区分, 変更日, 所属コード, 所属名;


		public Dic_社員名簿_old()
		{
			//var func_title = "Dic_社員名簿.Dic_社員名簿()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "H";
			topCount = 1;
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
			//var func_title = "Dic_社員名簿.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(退職日付);
			list.Add(退職区分);
			list.Add(変更日);
			list.Add(所属コード);
			list.Add(所属名);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_社員名簿.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号   = ToString(list[ 0]);
			漢字氏名   = ToString(list[ 1]);
			カナ氏名   = ToString(list[ 2]);
			退職日付   = ToString(list[ 3]);
			退職区分   = ToString(list[ 4]);
			変更日     = ToString(list[ 5]);
			所属コード = ToString(list[ 6]);
			所属名     = ToString(list[ 7]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_社員名簿.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(退職日付);
			list.Add(退職区分);
			list.Add(変更日);
			list.Add(所属コード);
			list.Add(所属名);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
