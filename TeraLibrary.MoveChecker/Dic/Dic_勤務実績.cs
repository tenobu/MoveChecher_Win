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
	public class Dic_勤務実績 : Dic_PlusPlus// Dictionary<string, int>
	{
		public string
			社員番号, 氏名, 所属コード, 所属名称, 職掌, 職責, 勤怠年月, 有給休暇, 保存有休, 欠勤日数, 出勤日数,
			労働日数, 労働時間, 法定超残業時間, 法定休出時間, 法定外休出時間, 深夜時間, 時間外合計, 時間外合計_回数,
			移動12ヶ月_時分, 法定休日, 時間外合計_色, 移動12ヶ月_色;


		public Dic_勤務実績()
		{
			//var func_title = "Dic_勤務実績.Dic_勤務実績()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			alphabet = "W";
			topCount = 2;
			columnCount = AlphabetTo.ToInt(alphabet);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// 初期化
		public void SetInit()
		{
			//var func_title = "Dic_勤務実績.SetInit()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号 = 氏名 = 所属コード = 所属名称 = 職責 = 職掌 = 勤怠年月 = 有給休暇 = 保存有休 = 欠勤日数 = 出勤日数 =
			労働日数 = 労働時間 = 法定超残業時間 = 法定休出時間 = 法定外休出時間 = 深夜時間 = 時間外合計 = 時間外合計_回数 =
			移動12ヶ月_時分 = 法定休日 = 時間外合計_色 = 移動12ヶ月_色 = "";

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// AddListDatas
		//
		protected override void AddListDatas(List<string> list)
		{
			//var func_title = "Dic_勤務実績.AddListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(氏名);
			list.Add(所属コード);
			list.Add(所属名称);
			list.Add(職掌);
			list.Add(職責);
			list.Add(勤怠年月);
			list.Add(有給休暇);
			list.Add(保存有休);
			list.Add(欠勤日数);
			list.Add(出勤日数);
			list.Add(労働日数);
			list.Add(労働時間);
			list.Add(法定超残業時間);
			list.Add(法定休出時間);
			list.Add(法定外休出時間);
			list.Add(深夜時間);
			list.Add(時間外合計);
			list.Add(時間外合計_回数);
			list.Add(移動12ヶ月_時分);
			list.Add(法定休日);
			list.Add(時間外合計_色);
			list.Add(移動12ヶ月_色);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// SetListDatas
		//
		protected override void SetListDatas(List<string> list)
		{
			//var func_title = "Dic_勤務実績.SetListDatas()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

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
			objDatas[rowIndex, 19] = list[19];
			objDatas[rowIndex, 20] = list[20];
			objDatas[rowIndex, 21] = list[21];
			objDatas[rowIndex, 22] = list[22];

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			//var func_title = "Dic_勤務実績.ColumnMoveRead2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			社員番号 = ToString(list[0]);
			氏名			 = ToString(list[ 1]);
			所属コード		 = ToString(list[ 2]);
			所属名称		 = ToString(list[ 3]);
			職掌			 = ToString(list[ 4]);
			職責			 = ToString(list[ 5]);
			勤怠年月		 = ToString(list[ 6]);
			有給休暇		 = ToString(list[ 7]);
			保存有休		 = ToString(list[ 8]);
			欠勤日数		 = ToString(list[ 9]);
			出勤日数		 = ToString(list[10]);
			労働日数		 = ToString(list[11]);
			労働時間		 = ToString(list[12]);
			法定超残業時間	 = ToString(list[13]);
			法定休出時間	 = ToString(list[14]);
			法定外休出時間	 = ToString(list[15]);
			深夜時間		 = ToString(list[16]);
			時間外合計		 = ToString(list[17]);
			時間外合計_回数	 = ToString(list[18]);
			移動12ヶ月_時分  = ToString(list[19]);
			法定休日		 = ToString(list[20]);
			時間外合計_色	 = ToString(list[21]);
			移動12ヶ月_色	 = ToString(list[22]);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			//var func_title = "Dic_勤務実績.ColumnMoveWrite2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			list.Add(社員番号);
			list.Add(氏名);
			list.Add(所属コード);
			list.Add(所属名称);
			list.Add(職掌);
			list.Add(職責);
			list.Add(勤怠年月);
			list.Add(有給休暇);
			list.Add(保存有休);
			list.Add(欠勤日数);
			list.Add(出勤日数);
			list.Add(労働日数);
			list.Add(労働時間);
			list.Add(法定超残業時間);
			list.Add(法定休出時間);
			list.Add(法定外休出時間);
			list.Add(深夜時間);
			list.Add(時間外合計);
			list.Add(時間外合計_回数);
			list.Add(移動12ヶ月_時分);
			list.Add(法定休日);
			list.Add(時間外合計_色);
			list.Add(移動12ヶ月_色);

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		public void InitSyainNo2()
		{
			//var func_title = "Dic_勤務実績.InitSyainNo2()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			rowIndex = 0;

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// RowsCount
		//
		// Setは書出に必要なObjDatasを定義している
		//
		public override int RowsCount
		{
			get
			{
				//var func_title = "Dic_勤務実績.RowsCount(get)";
				//Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");

				return rowsCount;
			}
			set
			{
				//var func_title = "Dic_勤務実績.RowsCount(set)";
				//Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				rowsCount = value;

				objDatas = new object[rowsCount, columnCount + 1];

				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		// ColumnMoveWrite
		// 
		public override void ColumnMoveWrite()
		{
			//var func_title = "Dic_勤務実績.ColumnMoveWrite()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			objDatas[rowIndex,  0] = 社員番号;
			objDatas[rowIndex,  1] = 氏名;
			objDatas[rowIndex,  2] = 所属コード;
			objDatas[rowIndex,  3] = 所属名称;
			objDatas[rowIndex,  4] = 職掌;
			objDatas[rowIndex,  5] = 職責;
			objDatas[rowIndex,  6] = 勤怠年月;
			objDatas[rowIndex,  7] = 有給休暇;
			objDatas[rowIndex,  8] = 保存有休;
			objDatas[rowIndex,  9] = 欠勤日数;
			objDatas[rowIndex, 10] = 出勤日数;
			objDatas[rowIndex, 11] = 労働日数;
			objDatas[rowIndex, 12] = 労働時間;
			objDatas[rowIndex, 13] = 法定超残業時間;
			objDatas[rowIndex, 14] = 法定休出時間;
			objDatas[rowIndex, 15] = 法定外休出時間;
			objDatas[rowIndex, 16] = 深夜時間;
			objDatas[rowIndex, 17] = 時間外合計;
			objDatas[rowIndex, 18] = 時間外合計_回数;
			objDatas[rowIndex, 19] = 移動12ヶ月_時分;
			objDatas[rowIndex, 20] = 法定休日;
			objDatas[rowIndex, 21] = 時間外合計_色;
			objDatas[rowIndex, 22] = 移動12ヶ月_色;

			rowIndex++;

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		public static string[] GetColumns()
		{
			//var func_title = "Dic_勤務実績.GetColumns()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var columns = new string[] {
				"社員番号", "氏名", "所属コード", "所属名称", "職掌", "職責", "勤怠年月", "有給休暇", "保存有休",
				"欠勤日数", "出勤日数", "労働日数", "労働時間", "法定超残業時間", "法定休出時間", "法定外休出時間",
				"深夜時間", "時間外合計", "時間外合計_回数", "移動12ヶ月_時分", "法定休日", "時間外合計_色",
				"移動12ヶ月_色" };

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");

			return columns;
		}

		// SetFormatLocal
		// 
		// Excelの個別の項目をセットする
		//
		public override void SetFormatLocal(Excel.Worksheet sk)
		{
			//var func_title = "Dic_勤務実績.SetFormatLocal()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var str = "A" + (TopCount + 1) + ":" + alphabet + (TopCount + 1 + RowIndex - 1);
			Excel.Range range = sk.Range[str];

			range.NumberFormatLocal = "@";


			str = str.Replace(str.Substring(0, 4), "G3:G");
			range = sk.Range[str];

			range.NumberFormatLocal = "yyyy/mm";


			str = str.Replace("G3:G", "H3:L");
			range = sk.Range[str];

			range.NumberFormatLocal = "?0.0";

	
			str = str.Replace("H3:L", "M3:T");
			range = sk.Range[str];

			range.HorizontalAlignment = Excel.Constants.xlRight;
			range.NumberFormatLocal = "@";

			//
			//
			//
			//

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		// SetRangeTo
		//
		// ObjDatasをExcelのRengeのValueに送り込む
		//
		public override void SetRangeTo(Excel.Worksheet sk)
		{
			//var func_title = "Dic_勤務実績.SetRangeTo()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			base.SetRangeTo(sk);

			Excel.Range range = sk.Range["W3:W3"];

			for (int row = 0; row < RowIndex; row++)
			{
				var range1 = range.get_Offset(0, -1);
				var range2 = range.get_Offset(0, -5);

				// 時間外勤務の文字列
				#region
				{
					String text = range1.Text;

					if (text.Equals("赤"))
					{
						// レンジの背景色を３(赤)
						range2.Interior.ColorIndex = 3;
					}
					else if (
						text.Equals("黄"))
					{
						// レンジの背景色を６(黄)
						range2.Interior.ColorIndex = 6;
					}
					else if (
						text.Equals("緑"))
					{
						// レンジの背景色を４(緑)
						range2.Interior.ColorIndex = 4;
					}
				}
				#endregion

				// 移動12ヶ月の文字列
				#region
				{
					string text	 = range.Text;

					range2 = range.get_Offset(0, -3);

					if (text.Equals("赤"))
					{
						// レンジの背景色を３(赤)
						range2.Interior.ColorIndex = 3;
					}
					else if (
						text.Equals("黄"))
					{
						// レンジの背景色を６(黄)
						range2.Interior.ColorIndex = 6;
					}
					else if (
						text.Equals("緑"))
					{
						// レンジの背景色を４(緑)
						range2.Interior.ColorIndex = 4;
					}

					range2 = range.get_Offset(0, -4);

					if (((string)range2.Text).Equals("") == false)
					{
						var suuji = int.Parse(range2.Text);

						if (suuji > 0 && suuji < 7)
						{
							// レンジの背景色を６(黄)
							range2.Interior.ColorIndex = 6;
						}
						else if (suuji >= 7)
						{
							// レンジの背景色を３(赤)
							range2.Interior.ColorIndex = 3;
						}
					}
				}
				#endregion
				

				range2 = range.get_Offset(0, -2);

				// V3の時間外勤務の一つ隣の法定休日
				#region
				{
					// 法定休日の文字列
					string text = range2.Text;

					if (text.Equals("×"))
					{
						// レンジの背景色を３(赤)
						range2.Interior.ColorIndex = 3;
					}
				}
				#endregion

				// レンジを次の行の同じ所にする
				range = range.get_Offset(1, 0);
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}