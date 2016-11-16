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

using TeraLibrary.MoveChecker.SQLite;

/*
CREATE TABLE FolderGroup (
	guid            TEXT NOT NULL,
	status          TEXT NOT NULL,
    FromFolder      TEXT NOT NULL,
    ToBaseFolder    TEXT NOT NULL,
    ToFolder        TEXT NOT NULL,
    PRIMARY KEY(guid)
)
*/
/*
delete from `社員名簿`
*/
namespace MoveChecker.Lite
{
	[Table(Name = "FolderGroup")]
	public class Lite_FolderGroup : Lite_Default
	{
		public static string str_TableName = "FolderGroup";

		[Column(Name = "guid", IsPrimaryKey = true, CanBeNull = false, DbType = "NVARCHAR")]
		public string guid { get; set; }
		[Column(Name = "status", IsPrimaryKey = true, CanBeNull = false, DbType = "NVARCHAR")]
		public string status { get; set; }
		[Column(Name = "FromFolder", CanBeNull = false, DbType = "NVARCHAR")]
		public string FromFolder { get; set; }
		[Column(Name = "ToBaseFolder", CanBeNull = false, DbType = "NVARCHAR")]
		public string ToBaseFolder { get; set; }
		[Column(Name = "ToFolder", CanBeNull = false, DbType = "NVARCHAR")]
		public string ToFolder { get; set; }

		public Lite_Folder _FromFolder = null;
		public Lite_Folder _ToFolder   = null;

		public static long long_DirsSize = 0L;


		public Lite_FolderGroup()
		{
			guid = status = FromFolder = ToBaseFolder = ToFolder = "";
		}

		public Lite_FolderGroup(string str_from, string str_to_base)
		{
			Guid guidValue = Guid.NewGuid();

			guid           = guidValue.ToString();
			status         = _Status.None.ToString();
			FromFolder     = str_from;
			ToBaseFolder   = str_to_base;
			ToFolder       = "";
		}

		#region Kiso
		public static string CreateTableString()
		{
			return
				"CREATE TABLE "                  +
				str_TableName                    +
				" ("                             +
				"guid            TEXT NOT NULL," +
				"status          TEXT NOT NULL," +
				"FromFolder      TEXT NOT NULL," +
				"ToBaseFolder    TEXT NOT NULL," +
				"ToFolder        TEXT NOT NULL," +
				"PRIMARY KEY(guid)"              +
				")";
		}

		public override string InsertString()
		{
			return
				"INSERT INTO "   +
				str_TableName    +
				" ("             +
				" guid,"         +
				" status,"       +
				" FromFolder,"   +
				" ToBaseFolder," +
				" ToFolder"      +
				" ) VALUES ( "   +
				" '"             + this.guid         + "'," +
				" '"             + this.status       + "'," +
				" '"             + this.FromFolder   + "'," +
				" '"             + this.ToBaseFolder + "'," +
				" '"             + this.ToFolder     + "'"  +
				" )";
		}

		public override string UpdateString()
		{
			return
				"UPDATE "           +
				str_TableName       +
				" SET"              +
				" status = '"       + this.status       + "'," +
				" FromFolder = '"   + this.FromFolder   + "'," +
				" ToBaseFolder = '" + this.ToBaseFolder + "'," +
				" ToFolder = '"     + this.ToFolder     + "'"  +
				" WHERE"            +
				" guid = '"         + this.guid         + "'";
		}

		private static Lite_FolderGroup GetReaderToLite(SQLiteDataReader reader)
		{
			var l = new Lite_FolderGroup();

			l.guid         = reader["guid"        ].ToString();
			l.status       = reader["status"      ].ToString();
			l.FromFolder   = reader["FromFolder"  ].ToString();
			l.ToBaseFolder = reader["ToBaseFolder"].ToString();
			l.ToFolder     = reader["ToFolder"    ].ToString();

			return l;
		}
		#endregion

		#region Insert
		public static bool Insert(Lite_FolderGroup lfg)
		{
			//var func_title = "Lite_社員名簿.Insert()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のInsert
				var command = conn.CreateCommand();

				command.CommandText = lfg.InsertString();

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

		public static bool InsertAll(List<Lite_FolderGroup> listfg)
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

				foreach (var lfg in listfg)
				{
					command.CommandText = lfg.InsertString();

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
		#endregion

		#region Select
		/*public static Dictionary<string, Lite_FolderGroup> Select()
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
		}*/

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
						"SELECT COUNT(*) AS COUNT FROM " + str_TableName;

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

		public static List<Lite_FolderGroup> SelectFromFromTo(string str_from, string str_to_base)
		{
			//var func_title = "Lite_社員名簿.SelectFrom社員名簿()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			var list = new List<Lite_FolderGroup>();

			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT * FROM FolderGroup" +
						" WHERE"                    +
						" FromFolder = '"           + str_from    + "'" +
						" AND"                      +
						" ToBaseFolder = '"         + str_to_base + "'";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
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

				list.Clear();

				return list;
			}
			finally
			{
				//Message.Information(func_title + " End");
				//Message.Information("");
				//Console.WriteLine(func_title + " End");
				//Console.WriteLine("");
			}
		}

		/*public static List<Lite_FolderGroup> SelectFrom社員名簿(string str_社員番号)
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
		}*/

		/*public static List<Lite_FolderGroup> SelectFrom社員名簿(List<string> list職番)
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
		}*/
		#endregion

		#region Update
		public static bool Update(Lite_FolderGroup lfg)
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

				command.CommandText = lfg.UpdateString();

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

		public static bool UpdateAll(List<Lite_FolderGroup> listfg)
		{
			//var func_title = "Lite_社員名簿.UpdateAll()";
			//Console.WriteLine(func_title + " Start");
			//Message.Information(func_title + " Start");

			if (listfg.Count == 0)
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

				foreach (var lfg in listfg)
				{
					command.CommandText = lfg.UpdateString();

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
		#endregion

		#region Delete
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
						"DELETE FROM " + str_TableName;

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
		#endregion

		#region その他
		public void CheckFolder()
		{
			Lite_FolderGroup.long_DirsSize = 0L;

			var list_fo = Lite_Folder.SelectFromGiud(guid, "From", guid);
			if (list_fo.Count == 0)
			{
				var list = FromFolder.Split('\\');
				var l = new Lite_Folder(guid, guid, "From", FromFolder, list[list.Count() - 1]);

				Lite_Folder.Insert(l);

				_FromFolder = l;

				_FromFolder.InsertFolder(FromFolder);
			}
			else if (
				list_fo.Count == 1)
			{
				_FromFolder = list_fo[0];

				_FromFolder.CheckFolder(FromFolder);
			}
			else
			{
				Console.WriteLine("");
			}
		}
		#endregion
	}
}
