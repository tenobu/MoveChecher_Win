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
CREATE TABLE File (
	guid            TEXT NOT NULL,
	FolderP_guid    TEXT NOT NULL,
	File_guid       TEXT NOT NULL,
    File_status     TEXT NOT NULL,
    File_FT_flag    TEXT NOT NULL,
    FileFull        TEXT NOT NULL,
    FileName        TEXT NOT NULL,
    FileSize        INT  NOT NULL,
    PRIMARY KEY(File_guid)
)
*/
/*
delete from `社員名簿`
*/
namespace MoveChecker.Lite
{
	[Table(Name = "File")]
	public class Lite_File : Lite_Default
	{
		public static string str_TableName = "File";

		[Column(Name = "guid", CanBeNull = false, DbType = "NVARCHAR")]
		public string guid { get; set; }
		[Column(Name = "FolderP_guid", CanBeNull = false, DbType = "NVARCHAR")]
		public string FolderP_guid { get; set; }
		[Column(Name = "File_guid", IsPrimaryKey = true, CanBeNull = false, DbType = "NVARCHAR")]
		public string File_guid { get; set; }
		[Column(Name = "File_status", CanBeNull = false, DbType = "NVARCHAR")]
		public string File_status { get; set; }
		[Column(Name = "File_FT_flag", CanBeNull = false, DbType = "NVARCHAR")]
		public string File_FT_flag { get; set; }
		[Column(Name = "FileFull", CanBeNull = false, DbType = "NVARCHAR")]
		public string FileFull { get; set; }
		[Column(Name = "FileName", CanBeNull = false, DbType = "NVARCHAR")]
		public string FileName { get; set; }
		[Column(Name = "FileSize", CanBeNull = false, DbType = "INT")]
		public int FileSize { get; set; }


		public Lite_File()
		{
			guid = FolderP_guid = File_guid = File_status = File_FT_flag =
				FileFull = FileName = "";
			FileSize = -1;
		}

		public Lite_File(string _guid, string _folderP_guid, string str_ft_flag, string str_full, string str_name)
		{
			Guid guidValue = Guid.NewGuid();
			var fi = new FileInfo(str_full);

			guid         = _guid;
			FolderP_guid = _folderP_guid;
			File_guid    = guidValue   .ToString();
			File_status  = _Status.None.ToString();
			File_FT_flag = str_ft_flag;
			FileFull     = str_full;
			FileName     = str_name;
			FileSize     = (int)fi.Length;
		}

		#region Kiso
		public static string CreateTableString()
		{
			return
				"CREATE TABLE "                  +
				str_TableName                    +
				" ("                             +
				"guid            TEXT NOT NULL," +
				"FolderP_guid    TEXT NOT NULL," +
				"File_guid       TEXT NOT NULL," +
				"File_status     TEXT NOT NULL," +
				"File_FT_flag    TEXT NOT NULL," +
				"FileFull        TEXT NOT NULL," +
				"FileName        TEXT NOT NULL," +
				"FileSize        INT  NOT NULL," +
				"PRIMARY KEY(File_guid)        " +
				")";
		}

		public override string InsertString()
		{
			return
				"INSERT INTO "   +
				str_TableName    +
				" ("             +
				" guid, "        +
				" FolderP_guid," +
				" File_guid,"    +
				" File_status,"  +
				" File_FT_flag," +
				" FileFull,"     +
				" FileName,"     +
				" FileSize"      +
				" ) VALUES ("    +
				" '"             + this.guid         + "'," +
				" '"             + this.FolderP_guid + "'," +
				" '"             + this.File_guid    + "'," +
				" '"             + this.File_status  + "'," +
				" '"             + this.File_FT_flag + "'," +
				" '"             + this.FileFull     + "'," +
				" '"             + this.FileName     + "'," +
				" "              + this.FileSize     +
				" )";
		}

		public override string UpdateString()
		{
			return
				"UPDATE "           +
				str_TableName       +
				" SET"              +
				" guid = '"         + this.guid         + "', " +
				" FolderP_guid = '" + this.FolderP_guid + "', " +
				" File_status = '"  + this.File_status  + "', " +
				" File_FT_flag = '" + this.File_FT_flag + "', " +
				" FileFull = '"     + this.FileFull     + "', " +
				" FileName = '"     + this.FileName     + "', " +
				" FileSize = "      + this.FileSize     +
				" WHERE"            +
				" File_guid = '"    + this.File_guid    + "'";
		}

		private static Lite_File GetReaderToLite(SQLiteDataReader reader)
		{
			var l = new Lite_File();

			l.guid         =      reader["guid"        ].ToString();
			l.FolderP_guid =      reader["FolderP_guid"].ToString();
			l.File_guid    =      reader["File_guid"   ].ToString();
			l.File_status  =      reader["File_status" ].ToString();
			l.File_FT_flag =      reader["File_FT_flag"].ToString();
			l.FileFull     =      reader["FileFull"    ].ToString();
			l.FileName     =      reader["FileName"    ].ToString();
			l.FileSize     = (int)reader["FileSize"    ];

			return l;
		}
		#endregion

		#region Insert
		public static bool Insert(Lite_File lf)
		{
			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のInsert
				var command = conn.CreateCommand();

				command.CommandText = lf.InsertString();

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
			}
		}

		public static bool InsertAll(List<Lite_File> listf)
		{
			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のInsert
				var sqlt = conn.BeginTransaction();
				var command = conn.CreateCommand();

				foreach (var lf in listf)
				{
					command.CommandText = lf.InsertString();

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
			}
		}
		#endregion

		#region Select
		/*public static Dictionary<string, Lite_File> Select()
		{
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
						var dic = new Dictionary<string, Lite_File>();

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

				return new Dictionary<string, Lite_File>();
			}
			finally
			{
			}
		}*/

		public static List<Lite_File> SelectFromGiud(string _guid)
		{
			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT * FROM "  + str_TableName +
						" WHERE guid = '" + _guid         + "'";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						var list = new List<Lite_File>();

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

				return new List<Lite_File>();
			}
			finally
			{
			}
		}

		public static Dictionary<string, Lite_File> SelectFromFile(string _guid, string _Folder_FT_flag, string _FolderP_guid)
		{
			try
			{
				var db = new SQLiteDB();
				var conn = db.conn;

				conn.Open();

				// SQLのCommand形式のSelect
				using (SQLiteCommand command = conn.CreateCommand())
				{
					command.CommandText =
						"SELECT * FROM "        + str_TableName +
						" WHERE guid = '"       + _guid         + "'" +
						" AND FolderP_guid = '" + _FolderP_guid + "'";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						var dic = new Dictionary<string, Lite_File>();

						while (reader.Read())
						{
							var l = GetReaderToLite(reader);

							dic.Add(l.FileFull, l);
						}

						return dic;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return new Dictionary<string, Lite_File>();
			}
			finally
			{
			}
		}

		public static int SelectFromCount()
		{
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
			}
		}

		/*public static List<Lite_File> SelectFrom社員名簿(string str_社員番号)
		{
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
						var list = new List<Lite_File>();

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

				return new List<Lite_File>();
			}
			finally
			{
			}
		}*/

		/*public static List<Lite_File> SelectFrom社員名簿(List<string> list職番)
		{
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
						var list = new List<Lite_File>();

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

				return new List<Lite_File>();
			}
			finally
			{
			}
		}*/
		#endregion

		#region Update
		public static bool Update(Lite_File lf)
		{
			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のUpdate
				var command = conn.CreateCommand();

				command.CommandText = lf.UpdateString();

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
			}
		}

		public static bool UpdateAll(List<Lite_File> listf)
		{
			if (listf.Count == 0) return true;

			var db = new SQLiteDB();
			var conn = db.conn;

			conn.Open();

			try
			{
				// SQLのCommand形式のUpdate
				var sqlt = conn.BeginTransaction();
				var command = conn.CreateCommand();

				foreach (var lf in listf)
				{
					command.CommandText = lf.UpdateString();

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
			}
		}
		#endregion

		#region Delete
		public static bool DeleteAll()
		{
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
			}
		}
		#endregion
	}
}
