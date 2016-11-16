using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace TeraLibrary.ExcelIO
{
	// Dic_PlusPlus
	//
	// Excel から Dictionary<string, string> への移項機能の基本クラス
	//
	// データの落とし方
	//     xxxxx,データ種別,項目名,データ,,,,
	//
	// データの落とし方
	//     xxxxx      : 社員番号
	//     データ種別 : "業績1","コース変更","その他理由"など
	//     項目名     : Excel の上に並んでいる項目名を、"で刳るんで
	//     データ     : Excel のデータを、"で刳るんで
	//     項目名とデータはペアにする
	//
	// Dic_Plusは現行バージョン
	//
	public class Dic_PlusPlus : Dictionary<string, int>
	{
		// Excelの横方向にあるアルファベットのインデックス
		// Excelの横の列のアルファベット
		// それを定義している(使っている)最大値の変数
		protected string alphabet = "";
		protected int columnCount = 0, rowsCount = 0, rowIndex = 0, rowsLength = 0, rowsOld = 0;
		protected int topCount = 1;

		protected object[,] objColumn;

		// データの本体
		// Excelのテーブルから直接ムーブされる
		protected object[,] objDatas;

		// 次にデータが有るか無いか判別している
		protected bool isNext = false;

		// 現在の社員番号
		protected string syainNO;

		// シートの名前
		protected string syuruiName;

		// このデータがあるかないかをフラグでセット。
		protected Boolean swichFlag = false;

		protected List<string> syainList;
		// 内部処理用
		// Excelから離れて、間接的にデータを保存
		protected Dictionary<string, List<string>> dicDatas;

		protected List<string> listエラー = null;


		public Dic_PlusPlus()
		{
			var func_title = "Dic_PlusPlus.Dic_PlusPlus()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public Dictionary<string, List<string>> DicDatas
		{
			get
			{
				var func_title = "Dic_PlusPlus.DicDatas(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return dicDatas;
			}
		}

		// Excelの横方向にあるアルファベットのインデックス
		// Excelの横の列のアルファベット
		// それを定義している(使っている)最大値のプロパティ
		//
		public virtual string Alphabet
		{
			get
			{
				var func_title = "Dic_PlusPlus.Alphabet(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return alphabet;
			}
		}

		public virtual int TopCount
		{
			get
			{
				var func_title = "Dic_PlusPlus.TopCount(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return topCount;
			}
		}

		// 横のカウント数
		public virtual int ColumnCount
		{
			get
			{
				var func_title = "Dic_PlusPlus.ColumnCount(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return columnCount;
			}
		}

		// 縦のカウント数
		public virtual int RowsCount
		{
			get
			{
				var func_title = "Dic_PlusPlus.RowsCount(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return rowsCount;
			}
			set
			{
				var func_title = "Dic_PlusPlus.RowsCount(set)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				rowsCount = value;

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			}
		}

		// 縦のカウント数Old
		public virtual int RowsOld
		{
			get
			{
				var func_title = "Dic_PlusPlus.RowsOld(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return rowsOld;
			}
			set
			{
				var func_title = "Dic_PlusPlus.RowsOld(set)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				rowsOld = value;

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			}
		}

		public virtual int RowIndex
		{
			get
			{
				var func_title = "Dic_PlusPlus.RowIndex(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return rowIndex;
			}
			set
			{
				var func_title = "Dic_PlusPlus.RowIndex(set)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				rowIndex = value;

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			}
		}

		public virtual object[,] ObjDatas
		{
			get
			{
				var func_title = "Dic_PlusPlus.ObjDatas(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return objDatas;
			}
			set
			{
				var func_title = "Dic_PlusPlus.ObjDatas(set)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				objDatas = value;

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			}
		}

		public virtual string SyainNO
		{
			get
			{
				var func_title = "Dic_PlusPlus.SyainNO(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return syainNO;
			}
			set
			{
				var func_title = "Dic_PlusPlus.SyainNO(set)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				syainNO = value;

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			}
		}

		public virtual Boolean SwichFlag
		{
			get
			{
				var func_title = "Dic_PlusPlus.SwichFlag(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return swichFlag;
			}
			set
			{
				var func_title = "Dic_PlusPlus.SwichFlag(set)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				swichFlag = value;

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");
			}
		}

		// InitDicDatas
		//
		// 初期社員番号処理
		//
		// Excelから一度にデータを取ったobjDatasから、Dicにデータを移す処理
		//
		public virtual void InitDicDatas()
		{
			var func_title = "Dic_PlusPlus.InitDicDatas()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			string str;
			int int_row;

			syainList = new List<string>();
			dicDatas = new Dictionary<string, List<string>>();

			for (int i = 1; i <= rowsCount; i++)
			{
				// 社員番号の文字列を取る
				str = objDatas[i, 1].ToString().Replace(" ", "");

				// 社員番号がKeyとしての役目を持っているか
				if (TryGetValue(str, out int_row) == false)
				{
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
			}

			// 最後にobjDatasにnullを入れ、GCに削除依頼（みたいな？）をする
			// objDatasを直接削除をする命令があればいいけど、無い（GCに頼りきり）
			objDatas = null;

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// EndDicDatas
		//
		// 初期社員番号処理
		//
		// dicをobjDatasに入れる処理
		//
		public virtual void EndDicDatas()
		{
			var func_title = "Dic_PlusPlus.EndDicDatas()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// dicDatasのカウンタをrowsCountに入れる
			rowsCount = dicDatas.Count;

			// dicDatasを見て、objDatasを再定義
			objDatas = new object[rowsCount + 1, columnCount + 1];

			rowIndex = 0;

			foreach (var key in dicDatas.Keys)
			{
				var list = dicDatas[key];

				SetListDatas(list);

				rowIndex++;
			}

			rowsCount = rowIndex;

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// AddListDatas
		//
		protected virtual void AddListDatas(List<string> list)
		{
			var func_title = "Dic_PlusPlus.AddListDatas()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// SetListDatas
		//
		protected virtual void SetListDatas(List<string> list)
		{
			var func_title = "Dic_PlusPlus.SetListDatas()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// ColumnMoveRead2
		// 
		public virtual void ColumnMoveRead2(List<string> list)
		{
			var func_title = "Dic_PlusPlus.ColumnMoveRead2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// ColumnMoveWrite2
		// 
		public virtual void ColumnMoveWrite2(List<string> list)
		{
			var func_title = "Dic_PlusPlus.ColumnMoveWrite2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// ColumnMoveRewrite2
		// 
		public virtual void ColumnMoveRewrite2(List<string> list)
		{
			var func_title = "Dic_PlusPlus.ColumnMoveRewrite2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// ReadNext
		// 
		// 次の社員番号が取れるかどうか
		//
		public virtual bool ReadNext()
		{
			var func_title = "Dic_PlusPlus.ReadNext()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

		Top:

			if (isNext)
			{
				rowIndex++;
			}
			else
			{
				isNext = true;
			}

			Console.WriteLine("rowIndex : " + rowIndex + " rowsCount : " + rowsCount);

			if (rowIndex > rowsCount - 1)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return false;
			}

			var syain_no = syainList[rowIndex];

			if (syain_no == null)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return false;
			}

			try
			{
				int int_syai = 0;
				if (int.TryParse(syain_no, out int_syai) == false) goto Top;

				// 数字が5桁か判定
				if (syain_no.Length < 5 || syain_no.Length > 5) goto Top;

				syainNO = syain_no;
			}
			catch (FormatException)
			{
				//msg.WriteLine("社員番号外1 = " + obj_syai);
				goto Top;
			}
			catch (OverflowException)
			{
				//msg.WriteLine("社員番号外2 = " + obj_syai);
				goto Top;
			}
			catch (InvalidCastException)
			{
				//msg.WriteLine("社員番号外3 = " + obj_syai);
				goto Top;
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return true;
		}

		// Find
		//
		// 与えた社員番号がKeysにあるか探す
		//
		public virtual void Find(string syainNo)
		{
			var func_title = "Dic_PlusPlus.Find()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//rowIndex = 0;

			// 社員番号がKeyで、Valueを登録されているか
			swichFlag = TryGetValue(syainNo, out rowIndex);
			// 登録されていたら
			if (swichFlag)
			{
				// 登録されている物を出す
				ColumnMoveRead();
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// Find2
		//
		// 与えた社員番号がKeysにあるか探す
		//
		public virtual void Find2(string syainNo)
		{
			var func_title = "Dic_PlusPlus.Find2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			List<string> list = null;

			swichFlag = dicDatas.TryGetValue(syainNo, out list);
			if (swichFlag)
			{
				// 登録されている物を出す
				ColumnMoveRead2(list);
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// SetDicDatas
		//
		// 与えられたsyainNoをキーにして、ColumnMoveWrite2にてパーツを得て、dicDatasに書き込む
		//
		public virtual void SetDicDatas(string syainNo)
		{
			var func_title = "Dic_PlusPlus.SetDicDatas()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			List<string> list = new List<string>();

			ColumnMoveWrite2(list);

			dicDatas.Add(syainNo, list);

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// ResetDicDatas
		//
		// 与えられたsyainNoをキーにして、ColumnMoveWrite2にてパーツを得て、dicDatasの同じ場所に書き込む
		//
		public void ResetDicDatas(string syainNo)
		{
			var func_title = "Dic_PlusPlus.ResetDicDatas()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			List<string> list = new List<string>();

			ColumnMoveWrite2(list);

			dicDatas[syainNo] = list;

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public virtual void ColumnMoveRead()
		{
			var func_title = "Dic_PlusPlus.ColumnMoveRead()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public virtual void ColumnMoveWrite()
		{
			var func_title = "Dic_PlusPlus.ColumnMoveWrite()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public virtual void ColumnMoveRewrite()
		{
			var func_title = "Dic_PlusPlus.ColumnMoveRewrite()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public void SetKeyValue(string key, string value)
		{
			var func_title = "Dic_PlusPlus.SetKeyValue()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//dic_Col[key] = value;

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public string GetValue(string key)
		{
			var func_title = "Dic_PlusPlus.GetValue()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return null;
			//return dic_Col[key];
		}

		// ToString
		//
		// objがnullの時、""(スペース)を戻す
		// objがそれ以外の時、objをToString()で文字列に直してから戻す
		//
		protected string ToString(object obj)
		{
			var func_title = "Dic_PlusPlus.ToString()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (obj == null)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return "";
			}

			/*string text = obj.ToString();

			if (text.Length > 0)
			{
				if (text.Contains("\n"))
				{
					text = text.Replace("\n", "");
				}
			}

			return text;*/

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return obj.ToString();
		}

		// CopySyainNO
		//
		//public SortedSet<string> CopySyainNO()
		public List<string> CopySyainNO()
		{
			var func_title = "Dic_PlusPlus.CopySyainNO()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var syain_no = new List<string>();

			int no;

			foreach (var syainNo in Keys)
			{
				if (int.TryParse(syainNo, out no))
				{
					//var syainNo_ = syainNo.Replace(" ", "");

					//syain_no.Add(syainNo_);
					syain_no.Add(syainNo);
				}
			}

			syain_no.Sort();

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return syain_no;
		}

		// CopySyainNO
		//
		//public SortedSet<string> CopySyainNO(SortedSet<string> syain_no)
		public List<string> CopySyainNO(List<string> syain_no)
		{
			var func_title = "Dic_PlusPlus.CopySyainNO()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			int no;

			foreach (var syainNo in Keys)
			{
				if (int.TryParse(syainNo, out no))
				{
					//var syainNo_ = syainNo.Replace(" ", "");

					//if (syain_no.Contains(syainNo_) == false)
					if (syain_no.Contains(syainNo) == false)
					{
						//syain_no.Add(syainNo_);
						syain_no.Add(syainNo);
					}
				}
			}

			syain_no.Sort();

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return syain_no;
		}

		// ErrorCheck
		//
		public virtual void ErrorCheck()
		{
			var func_title = "Dic_PlusPlus.ErrorCheck()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// ToError
		//
		protected void ToError(string mongon)
		{
			var func_title = "Dic_PlusPlus.ToError()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			listエラー.Add(mongon);

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// SetClear
		//
		// 以前のExcelのデータを丸ごと消す
		//
		public virtual void SetClear(Excel.Worksheet sk)
		{
			var func_title = "Dic_PlusPlus.SetClear()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// 以前、データがあったかどうか、調べる
			if (rowsOld >= TopCount + 1)
			{
				var str = "A" + (TopCount + 1) + ":" + alphabet + RowsOld;
				Excel.Range range = sk.Range[str];

				range.Clear();
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// SetFormatLocal
		// 
		// Excelの個別の項目をセットする
		//
		public virtual void SetFormatLocal(Excel.Worksheet sk)
		{
			var func_title = "Dic_PlusPlus.SetFormatLocal()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		// SetRangeTo
		//
		// ObjDatasをExcelのRengeのValueに送り込む
		//
		public virtual void SetRangeTo(Excel.Worksheet sk)
		{
			var func_title = "Dic_PlusPlus.SetRangeTo()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			// TopCountは、このページの頭の不必要な行をのけて、必要な行は何行目かを示す
			// alphabetは、このページの横を示す英字
			var str = "A" + (TopCount + 1) + ":" + alphabet + (TopCount + 1 + RowIndex - 1);
			// 文字列strは、このデータの広さを表している
			Excel.Range range = sk.Range[str];

			// ObjDatasのデータを、Excelのrangeに向けて移動させる
			range.Value = ObjDatas;

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}
	}
}
