using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
// 参照設定のDllも忘れずに！！
using System.IO;

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

//using TeraLibrary.徳島_勤怠チェック.Dic;

namespace TeraLibrary.MoveChecker.SQLite
{
	[DataContract]
	public class SQLiteDB
	{
		public SQLiteConnection conn = null;


		public SQLiteDB()
		{
			//var func_title = "SQLiteDB.SQLiteDB()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var exePath = System.Environment.GetCommandLineArgs()[0];
			var exeFullPath = System.IO.Path.GetFullPath(exePath);
			var startupPath = System.IO.Path.GetDirectoryName(exeFullPath);

			var connectionString = new SQLiteConnectionStringBuilder
			{
				// execPath の取得
				DataSource = startupPath + @"\MoveChecker.sqlite3",
			};

			conn = new SQLiteConnection(connectionString.ToString());

			//conn.Open();

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		public SQLiteDB(string startupPath)
		{
			//var func_title = "SQLiteDB.SQLiteDB()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			//var startupPath = startup_path;

			var connectionString = new SQLiteConnectionStringBuilder
			{
				// execPath の取得
				DataSource = startupPath + @"\MoveChecker.sqlite3",
			};

			conn = new SQLiteConnection(connectionString.ToString());

			//conn.Open();

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		public void Open()
		{
			//var func_title = "SQLiteDB.Open()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			conn.Open();

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}

		public void Close()
		{
			//var func_title = "SQLiteDB.Close()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			conn.Close();

			//Message.Information(func_title + " End");
			//Message.Information("");
			//Console.WriteLine(func_title + " End");
			//Console.WriteLine("");
		}
	}
}
