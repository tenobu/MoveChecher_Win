using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	// Dic_社員名簿_退職
	// 
	public class Dic_社員名簿_退職 : Dic_ListList // Dic_PlusPlus
	{
		public string
			社員番号 = "", 漢字氏名, カナ氏名, 異動日, 退職日付, 退職区分;


		public Dic_社員名簿_退職()
		{
			//var func_title = "Dic_出退勤_退職.Dic_出退勤_退職()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "F";
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
			//var func_title = "Dic_出退勤_退職.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(異動日);
			list.Add(退職日付);
			list.Add(退職区分);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_出退勤_退職.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号 = ToString(list[0]);
			漢字氏名 = ToString(list[1]);
			カナ氏名 = ToString(list[2]);
			異動日   = ToString(list[3]);
			退職日付 = ToString(list[4]);
			退職区分 = ToString(list[5]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退勤_退職.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(異動日);
			list.Add(退職日付);
			list.Add(退職区分);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
