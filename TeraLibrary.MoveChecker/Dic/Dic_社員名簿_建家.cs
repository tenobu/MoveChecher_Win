using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	// Dic_社員名簿_建家
	// 
	public class Dic_社員名簿_建家 : Dic_ListList // Dic_PlusPlus
	{
		public string
			社員番号 = "", 漢字氏名, カナ氏名, 異動日, 建家コード, 建家名称, 建家略称, システム使用アドレス;


		public Dic_社員名簿_建家()
		{
			//var func_title = "Dic_出退勤_建家.Dic_出退勤_建家()";
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
			//var func_title = "Dic_出退勤_建家.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(異動日);
			list.Add(建家コード);
			list.Add(建家名称);
			list.Add(建家略称);
			list.Add(システム使用アドレス);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_出退勤_建家.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号             = ToString(list[0]);
			漢字氏名             = ToString(list[1]);
			カナ氏名             = ToString(list[2]);
			異動日               = ToString(list[3]);
			建家コード           = ToString(list[4]);
			建家名称             = ToString(list[5]);
			建家略称             = ToString(list[6]);
			システム使用アドレス = ToString(list[7]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退勤_建家.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(異動日);
			list.Add(建家コード);
			list.Add(建家名称);
			list.Add(建家略称);
			list.Add(システム使用アドレス);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
