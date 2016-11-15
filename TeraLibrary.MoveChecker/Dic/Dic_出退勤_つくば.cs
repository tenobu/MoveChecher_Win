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
	public class Dic_出退勤_つくば : Dic_ListList// Dic_PlusPlus
	{
		public string
			通行時刻, 通行内容, 個人番号, 漢字氏名, カナ氏名, 性別, フロア, 区画番号, 区画名称,	通行要因,
			認証端末種別名称, 所属番号, 所属名称1, 属性, 分類番号;


		public string 所属建家コード;


		public Dic_出退勤_つくば()
		{
			//var func_title = "Dic_出退勤_つくば.Dic_出退勤_つくば()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "O";
			topCount = 6;
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
			//var func_title = "Dic_出退勤_つくば.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(通行時刻);
			list.Add(通行内容);
			list.Add(個人番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(性別);
			list.Add(フロア);
			list.Add(区画番号);
			list.Add(区画名称);
			list.Add(通行要因);
			list.Add(認証端末種別名称);
			list.Add(所属番号);
			list.Add(所属名称1);
			list.Add(属性);
			list.Add(分類番号);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_出退勤_つくば.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			通行時刻         = ToString(list[ 0]);
			通行内容		 = ToString(list[ 1]);
			個人番号		 = ToString(list[ 2]);
			漢字氏名		 = ToString(list[ 3]);
			カナ氏名		 = ToString(list[ 4]);
			性別			 = ToString(list[ 5]);
			フロア			 = ToString(list[ 6]);
			区画番号		 = ToString(list[ 7]);
			区画名称		 = ToString(list[ 8]);
			通行要因		 = ToString(list[ 9]);
			認証端末種別名称 = ToString(list[10]);
			所属番号		 = ToString(list[11]);
			所属名称1		 = ToString(list[12]);
			属性			 = ToString(list[13]);
			分類番号		 = ToString(list[14]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退勤_つくば.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(通行時刻);
			list.Add(通行内容);
			list.Add(個人番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(性別);
			list.Add(フロア);
			list.Add(区画番号);
			list.Add(区画名称);
			list.Add(通行要因);
			list.Add(認証端末種別名称);
			list.Add(所属番号);
			list.Add(所属名称1);
			list.Add(属性);
			list.Add(分類番号);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
