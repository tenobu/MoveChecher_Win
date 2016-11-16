using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace TeraLibrary.徳島_勤怠チェック.CSV
{
	public class CSVFile
	{
		protected DataTable dataTable = new DataTable();
		protected DataRow dataRow = null;


		public CSVFile()
		{
			var func_title = "CSVFile.CSVFile()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public DataTable DataTable
		{
			get
			{
				var func_title = "CSVFile.DataTable(get)";
				Console.WriteLine(func_title + " Start");
				//Message.Information(func_title + " Start");

				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return dataTable;
			}
		}

		public void RowsClear()
		{
			var func_title = "CSVFile.RowsClear()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			dataTable.Rows.Clear();

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public void AddColumn(string column_name)
		{
			var func_title = "CSVFile.AddColumn()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			dataTable.Columns.Add(column_name, Type.GetType("System.String"));

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public void AddColumns(string[] columns)
		{
			var func_title = "CSVFile.AddColumns()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			for (int i = 0; i < columns.Length; i++)
			{
				dataTable.Columns.Add(columns[i], Type.GetType("System.String"));
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public DataRow NewRow()
		{
			var func_title = "CSVFile.NewRow()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			dataRow = dataTable.NewRow();

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return dataRow;
		}

		public void AddRow(DataRow dr)
		{
			var func_title = "CSVFile.AddRow()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			dataTable.Rows.Add(dr);

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public void AddRow()
		{
			var func_title = "CSVFile.AddRow()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			dataTable.Rows.Add(dataRow);

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public void Read1(DataTable dt)
		{
			var func_title = "CSVFile.Read1()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var dr = dt.Rows[0];

			foreach (var column in dt.Columns)
			{
				dataRow[column.ToString()] = dr[column.ToString()];
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public void Read2(DataTable dt, DataRow dr)
		{
			var func_title = "CSVFile.Read2()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			foreach (var column in dt.Columns)
			{
				var col = dr[column.ToString()];
				dataRow[column.ToString()] = col;
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		public virtual void Write(string csvPath, bool writeHeader)
		{
			var func_title = "CSVFile.Write()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//CSVファイルに書き込むときに使うEncoding
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding("UTF-8");

			//書き込むファイルを開く
			System.IO.StreamWriter sr = new System.IO.StreamWriter(csvPath, false, enc);

			int colCount = dataTable.Columns.Count;
			int lastColIndex = colCount - 1;

			//ヘッダを書き込む
			if (writeHeader)
			{
				for (int i = 0; i < colCount; i++)
				{
					//ヘッダの取得
					string field = dataTable.Columns[i].Caption;
					//"で囲む
					field = EncloseDoubleQuotesIfNeed(field);
					//フィールドを書き込む
					sr.Write(field);
					//カンマを書き込む
					if (lastColIndex > i)
					{
						sr.Write(',');
					}
				}
				//改行する
				sr.Write("\r\n");
			}

			//レコードを書き込む
			foreach (DataRow row in dataTable.Rows)
			{
				for (int i = 0; i < colCount; i++)
				{
					//フィールドの取得
					string field = row[i].ToString();
					//"で囲む
					field = EncloseDoubleQuotesIfNeed(field);
					//フィールドを書き込む
					sr.Write(field);
					//カンマを書き込む
					if (lastColIndex > i)
					{
						sr.Write(',');
					}
				}
				//改行する
				sr.Write("\r\n");
			}

			//閉じる
			sr.Close();

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");
		}

		/// <summary>
		/// 必要ならば、文字列をダブルクォートで囲む
		/// </summary>
		protected string EncloseDoubleQuotesIfNeed(string field)
		{
			var func_title = "CSVFile.EncloseDoubleQuotesIfNeed()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (NeedEncloseDoubleQuotes(field))
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				Console.WriteLine(func_title + " End");
				Console.WriteLine("");

				return EncloseDoubleQuotes(field);
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return field;
		}

		/// <summary>
		/// 文字列をダブルクォートで囲む
		/// </summary>
		protected string EncloseDoubleQuotes(string field)
		{
			var func_title = "CSVFile.EncloseDoubleQuotes()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (field.IndexOf('"') > -1)
			{
				//"を""とする
				field = field.Replace("\"", "\"\"");
			}

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return "\"" + field + "\"";
		}

		/// <summary>
		/// 文字列をダブルクォートで囲む必要があるか調べる
		/// </summary>
		protected bool NeedEncloseDoubleQuotes(string field)
		{
			var func_title = "CSVFile.NeedEncloseDoubleQuotes()";
			Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//Message.Information(func_title + " End");
			//Message.Information("");
			Console.WriteLine(func_title + " End");
			Console.WriteLine("");

			return field.IndexOf('"') > -1 ||
				field.IndexOf(',') > -1 ||
				field.IndexOf('\r') > -1 ||
				field.IndexOf('\n') > -1 ||
				field.StartsWith(" ") ||
				field.StartsWith("\t") ||
				field.EndsWith(" ") ||
				field.EndsWith("\t");
		}
	}
}
