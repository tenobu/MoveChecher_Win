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

using SQLite;

namespace MoveChecker
{
	// Lite_Default
	// 
	// SQLite のデフォルトのクラス
	[Serializable]
	public class Lite_Default
	{
		// CommandScalar
		//
		// 何らかの値が返って来る
		// 値がobjectの為、何が帰って来るかは判らない
		// これはINT専用のため、返り値をINTにしている
		public static int CommandScalar(string comm)
		{
			//var func_title = "Lite_Default.CommandScalar()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();

				db.Open();

				var command = new SQLiteCommand(db.conn);

				command.CommandText = comm;
				var count = command.ExecuteScalar();

				int cnt = 0;
				// ここで、objectをINTに変換している
				// 返り値がINTじゃなかったら、-1にして全てエラーにしている
				if (int.TryParse(count.ToString(), out cnt) == false)
				{
					return -1;
				}

				return cnt;
			}
			catch (Exception e)
			{
				//MessageBox.Show(e.Message);
				Console.WriteLine(e.Message);

				return -1;
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		// CommandNonQuery
		//
		// 何にも値が返って来ないメソッド
		public static bool CommandNonQuery(string comm)
		{
			//var func_title = "Lite_Default.CommandNonQuery()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();

				db.Open();

				var command = new SQLiteCommand(db.conn);

				command.CommandText = comm;
				command.ExecuteNonQuery();

				return true;
			}
			catch (Exception e)
			{
				//MessageBox.Show(e.Message);
				Console.WriteLine(e.Message);

				return false;
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		public virtual string InsertString()
		{
			return "";
		}

		public virtual string UpdateString()
		{
			return "";
		}
	}
}
