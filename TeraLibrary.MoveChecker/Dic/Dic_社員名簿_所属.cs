using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	// Dic_社員名簿_所属
	// 
	public class Dic_社員名簿_所属 : Dic_ListList // Dic_PlusPlus
	{
		public string
			社員番号 = "", 漢字氏名, カナ氏名, 異動日, 所属コード, 所属名,
			レベル０２所属コード, レベル０２名称, レベル０４所属コード, レベル０４名称,
			レベル０５所属コード, レベル０５名称, システム使用アドレス;


		public Dic_社員名簿_所属()
		{
			//var func_title = "Dic_出退勤_所属.Dic_出退勤_所属()";
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

		// AddListDatas
		//
		protected override void AddListDatas(List<string> list)
		{
			//var func_title = "Dic_出退勤_所属.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(異動日);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(レベル０２所属コード);
			list.Add(レベル０２名称);
			list.Add(レベル０４所属コード);
			list.Add(レベル０４名称);
			list.Add(レベル０５所属コード);
			list.Add(レベル０５名称);
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
			//var func_title = "Dic_出退勤_所属.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号             = ToString(list[ 0]);
			漢字氏名             = ToString(list[ 1]);
			カナ氏名             = ToString(list[ 2]);
			異動日               = ToString(list[ 3]);
			所属コード           = ToString(list[ 4]);
			所属名               = ToString(list[ 5]);
			レベル０２所属コード = ToString(list[ 6]);
			レベル０２名称       = ToString(list[ 7]);
			レベル０４所属コード = ToString(list[ 8]);
			レベル０４名称       = ToString(list[ 9]);
			レベル０５所属コード = ToString(list[10]);
			レベル０５名称       = ToString(list[11]);
			システム使用アドレス = ToString(list[12]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退勤_所属.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(異動日);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(レベル０２所属コード);
			list.Add(レベル０２名称);
			list.Add(レベル０４所属コード);
			list.Add(レベル０４名称);
			list.Add(レベル０５所属コード);
			list.Add(レベル０５名称);
			list.Add(システム使用アドレス);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
