using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
// 参照設定のDllも忘れずに！！
using System.IO;

using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

using SQLite;

/*
CREATE TABLE FolderGroup (
	id                 TEXT NOT NULL,
	FolderGroupName    TEXT NOT NULL,
    PRIMARY KEY(id)
)
*/
/*
delete from `社員名簿`
*/
namespace MoveChecker
{
	[System.Data.Linq.Mapping.Table(Name = "FolderGroup")]
	public class Lite_FolderGroup : Lite_Default
	{
		[System.Data.Linq.Mapping.Column(Name = "id", IsPrimaryKey = true, CanBeNull = false, DbType = "INTEGER")]
		public int id { get; set; }
		[System.Data.Linq.Mapping.Column(Name = "FolderGroupName", CanBeNull = false, DbType = "NVARCHAR")]
		public string FolderGroupName { get; set; }

		public Lite_FolderGroup()
		{
			FolderGroupName = "";
		}

		public static string CreateTableString()
		{
			return
				"CREATE TABLE FolderGroup ("          +
				"id                 INTGER NOT NULL," +
				"FolderGroupName    TEXT   NOT NULL," +
				"PRIMARY KEY(id)"                     +
				")";
		}

		public override string InsertString()
		{
			return
				"INSERT INTO"       +
				" FolderGroup"      +
				" ("                +
				" id,"              +
				" ,FolderGroupName" +
				" ) VALUES ( "      +
				" '"                + this.id              +
				"', '"              + this.FolderGroupName +
				"' )";
		}

		public override string UpdateString()
		{
			return
				"UPDATE "              +
				" FolderGroup"         +
				" SET"                 +
				" FolderGroupName = '" + this.FolderGroupName + "' " +
				" WHERE id = "         + this.id;
		}

		private static Lite_FolderGroup GetReaderToLite(SQLiteDataReader reader)
		{
			var l = new Lite_FolderGroup();

			l.id              = (int)reader["id"      ];
			l.FolderGroupName =      reader["社員番号"].ToString();

			return l;
		}

		public static bool Insert(Lite_FolderGroup l社員)
		{
			//var func_title = "Lite_社員名簿.Insert()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var db = new SQLiteDB();
			var conn = db.conn;

			//conn.Open();

			try
			{
				// SQLのCommand形式のInsert
				var command = conn.CreateCommand();

				command.CommandText = l社員.InsertString();

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

		public static bool InsertAll(List<Lite_FolderGroup> list社員)
		{
			//var func_title = "Lite_社員名簿.InsertAll()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のInsert
				var sqlt = conn.BeginTransaction();
				var command = conn.CreateCommand();

				foreach (var l社員 in list社員)
				{
					command.CommandText = l社員.InsertString();

					command.ExecuteNonQuery();
				}

				sqlt.Commit();

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

		public static Dictionary<string, Lite_FolderGroup> Select()
		{
			//var func_title = "Lite_社員名簿.Select()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT * FROM \"社員名簿\" ORDER BY `社員番号`";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						var dic = new Dictionary<string, Lite_FolderGroup>();

						while (reader.Read())
						{
							var l = GetReaderToLite(reader);

							dic.Add(l.社員番号, l);
						}

						return dic;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return new Dictionary<string, Lite_FolderGroup>();
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		public static int SelectFromCount()
		{
			//var func_title = "Lite_社員名簿.SelectFromCount()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT COUNT(*) AS COUNT FROM \"社員名簿\"";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return int.Parse(reader["COUNT"].ToString());
						}

						return 0;
					}
				}
			}
			catch (Exception e)
			{
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

		/*public static int SelectFrom社員名簿(string str_社員番号)
		{
			//var func_title = "Lite_社員名簿.SelectFrom社員名簿()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT COUNT(*) AS COUNT FROM \"社員名簿\"" +
						" WHERE `社員番号` = '"                      + str_社員番号 + "'";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return int.Parse(reader["COUNT"].ToString());
						}

						return 0;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return 0;
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}*/

		public static List<Lite_FolderGroup> SelectFrom社員名簿(string str_社員番号)
		{
			//var func_title = "Lite_社員名簿.SelectFrom社員名簿()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT * FROM \"社員名簿\"" +
						" WHERE `社員番号` = '"      + str_社員番号 + "'";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						var list = new List<Lite_FolderGroup>();

						while (reader.Read())
						{
							list.Add(GetReaderToLite(reader));
						}

						return list;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return new List<Lite_FolderGroup>();
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		public static List<Lite_FolderGroup> SelectFrom社員名簿(List<string> list職番)
		{
			//var func_title = "Lite_社員名簿.SelectFrom社員名簿()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					var str = "";

					foreach (var str_syain_no in list職番)
					{
						if (str.Length > 0)
						{
							str += "', '";
						}

						str += str_syain_no;
					}

					command.CommandText =
						"SELECT * FROM \"社員名簿\"" +
						" WHERE `社員番号` IN ( '"   + str +
						"' ) ORDER BY `社員番号`";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						var list = new List<Lite_FolderGroup>();

						while (reader.Read())
						{
							list.Add(GetReaderToLite(reader));
						}

						return list;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return new List<Lite_FolderGroup>();
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		public static bool Update(Lite_FolderGroup l社員)
		{
			//var func_title = "Lite_社員名簿.Update()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のUpdate
				var command = conn.CreateCommand();

				command.CommandText = l社員.UpdateString();

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

		public static bool UpdateAll(List<Lite_FolderGroup> list社員)
		{
			//var func_title = "Lite_社員名簿.UpdateAll()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (list社員.Count == 0)
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");

				return true;
			}

			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のUpdate
				var sqlt = conn.BeginTransaction();
				var command = conn.CreateCommand();

				foreach (var l社員 in list社員)
				{
					command.CommandText = l社員.UpdateString();

					command.ExecuteNonQuery();
				}

				sqlt.Commit();

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

		public static bool DeleteAll()
		{
			//var func_title = "Lite_社員名簿.DeleteAll()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のDelete
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"DELETE FROM \"社員名簿\"";

					command.ExecuteNonQuery();
				}

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
	}
}
