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
	public class Dic_出退時刻 : Dic_PlusPlus // Dic_ListList// 
	{
		public string
			社員番号, 漢字氏名, カナ氏名, 所属コード, 所属名, 勤怠年月日, 曜日,
			IC出勤時刻, IC出勤時刻訂正, Bulas出勤時刻, Bulas退勤時刻, IC退勤時刻, IC退勤時刻訂正, Bulas拘束時間, IC拘束時間,
			出勤時刻の差異時間, 退勤時刻の差異時間, 勤務時間差異, コメント;


		public Dic_出退時刻()
		{
			//var func_title = "Dic_出退時刻.Dic_出退時刻()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "S";
			topCount = 1;
			columnCount = AlphabetTo.ToInt(alphabet);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		/*public override void Init()
		{
			this.InitDicDatas();
		}*/

		// InitDicDatas
		//
		// 初期社員番号処理
		//
		// Excelから一度にデータを取ったobjDatasから、Dicにデータを移す処理
		//
		public override void InitDicDatas()
		{
			string str;

			syainList = new List<string>();
			dicDatas = new Dictionary<string, List<string>>();

			for (int i = 1; i <= rowsCount; i++)
			{
				// 社員番号の文字列を取る
				var str_1 = objDatas[i, 0].ToString().Replace(" ", "");
				var str_2 = objDatas[i, 6].ToString();

				str = str_1 + " " + str_2;

				// 社員番号+勤怠年月日がKeyとしての役目を持っているか
				syainList.Add(str);

				List<string> list = new List<string>();

				for (int j = 1; j <= columnCount; j++)
				{
					list.Add(ToString(objDatas[i, j]));
				}

				// 役目を持っていない時は、Keyに足す
				// Valueはiのインデックス
				dicDatas.Add(str, list);
			}

			// 最後にobjDatasにnullを入れ、GCに削除依頼（みたいな？）をする
			// objDatasを直接削除をする命令があればいいけど、無い（GCに頼りきり）
			objDatas = null;
		}

		// AddListDatas
		//
		protected override void AddListDatas(List<string> list)
		{
			//var func_title = "Dic_出退時刻.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(勤怠年月日);
			list.Add(曜日);
			list.Add(IC出勤時刻);
			list.Add(IC出勤時刻訂正);
			list.Add(Bulas出勤時刻);
			list.Add(Bulas退勤時刻);
			list.Add(IC退勤時刻);
			list.Add(IC退勤時刻訂正);
			list.Add(Bulas拘束時間);
			list.Add(IC拘束時間);
			list.Add(出勤時刻の差異時間);
			list.Add(退勤時刻の差異時間);
			list.Add(勤務時間差異);
			list.Add(コメント);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_出退時刻.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号           = ToString(list[ 0]);
			漢字氏名           = ToString(list[ 1]);
			カナ氏名           = ToString(list[ 2]);
			所属コード         = ToString(list[ 3]);
			所属名             = ToString(list[ 4]);
			勤怠年月日         = ToString(list[ 5]);
			曜日               = ToString(list[ 6]);
			IC出勤時刻         = ToString(list[ 7]);
			IC出勤時刻訂正     = ToString(list[ 8]);
			Bulas出勤時刻      = ToString(list[ 9]);
			Bulas退勤時刻      = ToString(list[10]);
			IC退勤時刻         = ToString(list[11]);
			IC退勤時刻訂正     = ToString(list[12]);
			Bulas拘束時間      = ToString(list[13]);
			IC拘束時間         = ToString(list[14]);
			出勤時刻の差異時間 = ToString(list[15]);
			退勤時刻の差異時間 = ToString(list[16]);
			勤務時間差異       = ToString(list[17]);
			コメント           = ToString(list[18]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_出退時刻.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(勤怠年月日);
			list.Add(曜日);
			list.Add(IC出勤時刻);
			list.Add(IC出勤時刻訂正);
			list.Add(Bulas出勤時刻);
			list.Add(Bulas退勤時刻);
			list.Add(IC退勤時刻);
			list.Add(IC退勤時刻訂正);
			list.Add(Bulas拘束時間);
			list.Add(IC拘束時間);
			list.Add(出勤時刻の差異時間);
			list.Add(退勤時刻の差異時間);
			list.Add(勤務時間差異);
			list.Add(コメント);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// SetListDatas
		//
		protected override void SetListDatas(List<string> list)
		{
			objDatas[rowIndex,  0] = list[ 0];
			objDatas[rowIndex,  1] = list[ 1];
			objDatas[rowIndex,  2] = list[ 2];
			objDatas[rowIndex,  3] = list[ 3];
			objDatas[rowIndex,  4] = list[ 4];
			objDatas[rowIndex,  5] = list[ 5];
			objDatas[rowIndex,  6] = list[ 6];
			objDatas[rowIndex,  7] = list[ 7];
			objDatas[rowIndex,  8] = list[ 8];
			objDatas[rowIndex,  9] = list[ 9];
			objDatas[rowIndex, 10] = list[10];
			objDatas[rowIndex, 11] = list[11];
			objDatas[rowIndex, 12] = list[12];
			objDatas[rowIndex, 13] = list[13];
			objDatas[rowIndex, 14] = list[14];
			objDatas[rowIndex, 15] = list[15];
			objDatas[rowIndex, 16] = list[16];
			objDatas[rowIndex, 17] = list[17];
			objDatas[rowIndex, 18] = list[18];
		}

		// SetFormatLocal
		// 
		// Excelの個別の項目をセットする
		//
		public override void SetFormatLocal(Excel.Worksheet sk)
		{
			var str = "A" + (TopCount + 1) + ":" + alphabet + (TopCount + 1 + RowIndex - 1);
			Excel.Range range = sk.Range[str];

			range.NumberFormatLocal = "@";


			/*str = str.Replace(str.Substring(0, 4), "G2:G");
			range = sk.Range[str];

			range.NumberFormatLocal = "yyyy/mm/dd";*/

			range.HorizontalAlignment = Excel.Constants.xlRight;
			range.NumberFormatLocal = "@";
		}
	}
}
