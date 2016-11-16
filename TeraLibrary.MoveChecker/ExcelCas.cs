using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

using CultureInfo = System.Globalization.CultureInfo;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary;
using TeraLibrary.ExcelIO;
using TeraLibrary.徳島_勤怠チェック.Dic;

namespace TeraLibrary.徳島_勤怠チェック
{
	// Excel Castum
	//
	// Excelの細かい処理
	//
	// １．Excelの再計算をマニュアルにする
	// ２．シート"昇給"のデータを全て消す
	// ３．他のシードを変換して、データ種別を付けて Dic に直す。
	// ４．社員番号でSORTする。
	// ５．社員番号単位で読み、計算してExcelのフォーマットに直す。
	//
	public class ExcelCas : ExcelInOut
	{
		// FileCopy
		//
		public bool FileCopy(string origin_path, string excel_path)
		{
			var func_title = "ExcelCas.FileCopy()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// Excelファイルをコピー
			try
			{
				Message.Information("コピー処理 Start");

				System.IO.File.Copy(
					origin_path, excel_path, true);

				Message.Information("コピー正常終了");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return true;
			}
			catch (Exception e)
			{
				Message.Error(e.Message);

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return false;
			}
			finally
			{
				Message.Information("コピー処理 End");
				Message.Information("");
			}
		}

		// GetSheetsRead
		//
		// Excel ⇒ Dic.ObjDatas に書き込む
		//
		// Excel のシート data を選び、Dic.ObjDatas に溜めて送る。
		//
		public void GetSheetsRead(string data, Dic_PlusPlus dic)
		{
			var func_title = "ExcelCas.GetSheetsRead()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// シートdataを開ける
			var sk = GetSheet(data);

			object[,] o = new object[3,3];
			o = sk.Range["A1:C3"].Value;

			// 指定したシートの縦のライン数を返す
			dic.RowsOld = dic.RowsCount = GetRowsCount(sk);
			if (dic.RowsCount >= dic.TopCount)
			{
				dic.RowsCount -= dic.TopCount;
			}

			if (dic.RowsCount > 0)
			{
				var str = "A" + (dic.TopCount + 1) + ":" + dic.Alphabet + (dic.RowsCount + dic.TopCount);

				//dic.RowsCount -= dic.TopCount;

				// 取るべきデータを含んだ範囲をRangeで表し、
				// ObjDatasに一度に送る
				dic.ObjDatas = sk.Range[str].Value;//object[1,1]～[1,6]
			}

			dic.InitDicDatas();

			Message.Information(
				string.Format("Read {0} = {1} x {2}",
				data, AlphabetTo.ToAlphabet(dic.ColumnCount), dic.RowsCount));

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// GetSheetsRead
		//
		// Excel ⇒ Dic.ObjDatas に書き込む
		//
		// Excel のシート data を選び、Dic.ObjDatas に溜めて送る。
		//
		public void GetSheetsRead2(string data, Dic_PlusPlus dic)
		{
			var func_title = "ExcelCas.GetSheetsRead2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// シートdataを開ける
			var sk = GetSheet(data);

			// 指定したシートの縦のライン数を返す
			dic.RowsOld = dic.RowsCount = GetRowsCount(sk);
			if (dic.RowsCount >= dic.TopCount)
			{
				dic.RowsCount -= dic.TopCount;
			}

			if (dic.RowsCount > 0)
			{
				var str = "A" + (dic.TopCount + 1) + ":" + dic.Alphabet + (dic.RowsCount + dic.TopCount);

				//dic.RowsCount -= dic.TopCount;

				// 取るべきデータを含んだ範囲をRangeで表し、
				// ObjDatasに一度に送る
				dic.ObjDatas = sk.Range[str].Value;//object[1,1]～[1,6]
			}

			dic.InitDicDatas();

			Message.Information(
				string.Format("Read {0} = {1} x {2}",
				data, AlphabetTo.ToAlphabet(dic.ColumnCount), dic.RowsCount));

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// GetSheetsRead
		//
		// Excel ⇒ Dic.ObjDatas に書き込む
		//
		// Excel のシート data を選び、Dic.ObjDatas に溜めて送る。
		//
		public void GetSheetsRead2(string data, Dic_ListList dic)
		{
			var func_title = "ExcelCas.GetSheetsRead2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// シートdataを開ける
			var sk = GetSheet(data);

			// 指定したシートの縦のライン数を返す
			dic.RowsOld = dic.RowsCount = GetRowsCount(sk);
			if (dic.RowsCount >= dic.TopCount)
			{
				dic.RowsCount -= dic.TopCount;
			}

			if (dic.RowsCount > 0)
			{
				var str = "A" + (dic.TopCount + 1) + ":" + dic.Alphabet + (dic.RowsCount + dic.TopCount);

				//dic.RowsCount -= dic.TopCount;

				// 取るべきデータを含んだ範囲をRangeで表し、
				// ObjDatasに一度に送る
				dic.ObjDatas = sk.Range[str].Value;//object[1,1]～[1,6]
			}

			dic.InitDicDatas();

			Message.Information(
				string.Format("Read {0} = {1} x {2}",
				data, AlphabetTo.ToAlphabet(dic.ColumnCount), dic.RowsCount));

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// SetSheetsWrite
		//
		// Dic.ObjDatas ⇒ Excel に書き込む
		//
		// Dic.ObjDatas に溜めたデータを、Excel に実際に書き込む
		//
		public void SetSheetsWrite(string data, Dic_PlusPlus dic)
		{
			var func_title = "ExcelCas.SetSheetsWrite()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (errFlag)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return;
			}

			dic.EndDicDatas();

			var sk = GetSheet(data);

			dic.SetClear(sk);

			dic.SetFormatLocal(sk);

			dic.SetRangeTo(sk);

			Message.Information(
				string.Format("Write {0} = {1} x {2}",
				data, AlphabetTo.ToAlphabet(dic.ColumnCount), dic.RowsCount));

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public bool Delete()
		{
			var func_title = "ExcelCas.Delete()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (errFlag)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			
				return false;
			}

			End();

			System.IO.File.Delete(excelPath);

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return true;
		}

		public bool IsDelete(Dic_PlusPlus dic)
		{
			var func_title = "ExcelCas.IsDelete()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (errFlag)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			
				return false;
			}

			var count = 0;

			foreach (var sheet_name in GetSheetsList())
			{
				var sk = GetSheet(sheet_name);

				// 指定したシートの縦のライン数を返す
				if (GetRowsCount(sk) > dic.TopCount) count++;
			}

			if (count == 0)
			{
				End();

				System.IO.File.Delete(excelPath);
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return true;
		}
	}
}
