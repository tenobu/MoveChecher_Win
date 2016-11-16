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
CREATE TABLE Folder (
	guid              TEXT NOT NULL,
	FolderP_guid      TEXT NOT NULL,
	Folder_guid       TEXT NOT NULL,
    Folder_status     TEXT NOT NULL,
    Folder_FT_flag    TEXT NOT NULL,
    FolderFull        TEXT NOT NULL,
    FolderName        TEXT NOT NULL,
    FolderSize        INT  NOT NULL,
    FolderCS_guid     TEXT NOT NULL,
    PRIMARY KEY(Folder_guid)
)
*/
/*
delete from `社員名簿`
*/
namespace MoveChecker.Lite
{
	[Table(Name = "Folder")]
	public class Lite_Folder : Lite_Default
	{
		public static string str_TableName = "Folder";

		[Column(Name = "guid", CanBeNull = false, DbType = "NVARCHAR")]
		public string guid { get; set; }
		[Column(Name = "FolderP_guid", IsPrimaryKey = true, CanBeNull = false, DbType = "NVARCHAR")]
		public string FolderP_guid { get; set; }
		[Column(Name = "Folder_guid", IsPrimaryKey = true, CanBeNull = false, DbType = "NVARCHAR")]
		public string Folder_guid { get; set; }
		[Column(Name = "Folder_status", CanBeNull = false, DbType = "NVARCHAR")]
		public string Folder_status { get; set; }
		[Column(Name = "Folder_FT_flag", CanBeNull = false, DbType = "NVARCHAR")]
		public string Folder_FT_flag { get; set; }
		[Column(Name = "FolderFull", CanBeNull = false, DbType = "NVARCHAR")]
		public string FolderFull { get; set; }
		[Column(Name = "FolderName", CanBeNull = false, DbType = "NVARCHAR")]
		public string FolderName { get; set; }
		[Column(Name = "FolderSize", CanBeNull = false, DbType = "INT")]
		public int FolderSize { get; set; }

		public List<Lite_Folder> _Folders = new List<Lite_Folder>();
		public List<Lite_File  > _Files   = new List<Lite_File  >();


		public Lite_Folder()
		{
			guid = FolderP_guid = Folder_guid = Folder_status = Folder_FT_flag =
				FolderFull = FolderName = "";
			FolderSize = -1;
		}

		public Lite_Folder(string _guid, string _folderP_guid, string str_ft_flag, string str_full, string str_name)
		{
			Guid guidValue = Guid.NewGuid();

			guid           = _guid;
			FolderP_guid   = _folderP_guid;
			Folder_guid    = guidValue.ToString();
			Folder_status  = _Status.None.ToString();
			Folder_FT_flag = str_ft_flag;
			FolderFull     = str_full;
			FolderName     = str_name;
			FolderSize     = -1;
		}

		#region Kiso
		public static string CreateTableString()
		{
			return
				"CREATE TABLE "                    +
				str_TableName                      +
				" ("                               +
				"guid              TEXT NOT NULL," +
				"FolderP_guid      TEXT NOT NULL," +
				"Folder_guid       TEXT NOT NULL," +
				"Folder_status     TEXT NOT NULL," +
				"Folder_FT_flag    TEXT NOT NULL," +
				"FolderFull        TEXT NOT NULL," +
				"FolderName        TEXT NOT NULL," +
				"FolderSize        INT  NOT NULL," +
				"PRIMARY KEY(Folder_guid)        " +
				")";
		}

		public override string InsertString()
		{
			return
				"INSERT INTO "     +
				str_TableName      +
				" ("               +
				" guid, "          +
				" FolderP_guid,"   +
				" Folder_guid,"    +
				" Folder_status,"  +
				" Folder_FT_flag," +
				" FolderFull,"     +
				" FolderName,"     +
				" FolderSize"      +
				" ) VALUES ("      +
				" '"               + this.guid           + "'," +
				" '"               + this.FolderP_guid   + "'," +
				" '"               + this.Folder_guid    + "'," +
				" '"               + this.Folder_status  + "'," +
				" '"               + this.Folder_FT_flag + "'," +
				" '"               + this.FolderFull     + "'," +
				" '"               + this.FolderName     + "'," +
				" '"               + this.FolderSize     + "'"  +
				" )";
		}

		public override string UpdateString()
		{
			return
				"UPDATE "             +
				str_TableName         +
				" SET"                +
				" guid = '"           + this.guid           + "'," +
				" FolderP_guid = '"   + this.FolderP_guid   + "'," +
				" Folder_status = '"  + this.Folder_status  + "'," +
				" Folder_FT_flag = '" + this.Folder_FT_flag + "'," +
				" FolderFull = '"     + this.FolderFull     + "'," +
				" FolderName = '"     + this.FolderName     + "'," +
				" FolderSize = "      + this.FolderSize     +
				" WHERE" +
				" Folder_guid = '"    + this.Folder_guid    + "'";
		}

		private static Lite_Folder GetReaderToLite(SQLiteDataReader reader)
		{
			var l = new Lite_Folder();

			l.guid           =      reader["guid"          ].ToString();
			l.FolderP_guid   =      reader["FolderP_guid"  ].ToString();
			l.Folder_guid    =      reader["Folder_guid"   ].ToString();
			l.Folder_status  =      reader["Folder_status" ].ToString();
			l.Folder_FT_flag =      reader["Folder_FT_flag"].ToString();
			l.FolderFull     =      reader["FolderFull"    ].ToString();
			l.FolderName     =      reader["FolderName"    ].ToString();
			l.FolderSize     = (int)reader["FolderSize"    ];

			return l;
		}
		#endregion

		#region Insert
		public static bool Insert(Lite_Folder lf)
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

		public static bool InsertAll(List<Lite_Folder> listf)
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
		/*public static Dictionary<string, Lite_Folder> Select()
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
						var dic = new Dictionary<string, Lite_Folder>();

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

				return new Dictionary<string, Lite_Folder>();
			}
			finally
			{
			}
		}*/

		public static List<Lite_Folder> SelectFromGiud(string _guid, string _Folder_FT_flag, string _FolderP_guid)
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
						var list = new List<Lite_Folder>();

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

				return new List<Lite_Folder>();
			}
			finally
			{
			}
		}

		public static Dictionary<string, Lite_Folder> SelectFromFolder(string _guid, string _Folder_FT_flag, string _FolderP_guid)
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
						"SELECT * FROM " + str_TableName +
						" WHERE guid = '" + _guid + "'" +
						" AND FolderP_guid = '" + _FolderP_guid + "'";

					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						var dic = new Dictionary<string, Lite_Folder>();

						while (reader.Read())
						{
							var l = GetReaderToLite(reader);

							dic.Add(l.FolderFull, l);
						}

						return dic;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return new Dictionary<string, Lite_Folder>();
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

		/*public static List<Lite_Folder> SelectFrom社員名簿(string str_社員番号)
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
						var list = new List<Lite_Folder>();

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

				return new List<Lite_Folder>();
			}
			finally
			{
			}
		}*/

		/*public static List<Lite_Folder> SelectFrom社員名簿(List<string> list職番)
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
						var list = new List<Lite_Folder>();

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

				return new List<Lite_Folder>();
			}
			finally
			{
			}
		}*/
		#endregion

		#region Update
		public static bool Update(Lite_Folder lf)
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

		public static bool UpdateAll(List<Lite_Folder> listf)
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

		#region その他
		public void InsertFolder(string _folder)
		{
			var dis = Directory.GetDirectories(_folder);

			foreach (var dic in dis)
			{
				var list = dic.Split('\\');

				var l = new Lite_Folder(guid, Folder_guid, Folder_FT_flag, dic, list[list.Count() - 1]);

				Lite_Folder.Insert(l);

				_Folders.Add(l);

				l.InsertFolder(dic);
			}

			var fis = Directory.GetFiles(_folder);

			foreach (var fic in fis)
			{
				var list = fic.Split('\\');

				var l = new Lite_File(guid, Folder_guid, Folder_FT_flag, fic, list[list.Count() - 1]);

				Lite_File.Insert(l);

				_Files.Add(l);
			}
		}

		public void CheckFolder(string _folder)
		{
			if (Directory.Exists(FolderFull) == false ||
				FolderFull.Equals(_folder) == false)
			{
				new Exception(Folder_FT_flag + "ディレクトリが記録と違う！！");
			}

			var dic_fo = Lite_Folder.SelectFromFolder(guid, Folder_FT_flag, Folder_guid);

			var dis = Directory.GetDirectories(_folder);

			if (dic_fo.Count != dis.Count())
			{
				new Exception(Folder_FT_flag + "の" + _folder + "の数が記録と違う！！");
			}

			foreach (var dic in dis)
			{
				if (dic_fo.ContainsKey(dic) == false)
				{
					var list = dic.Split('\\');

					var l = new Lite_Folder(guid, Folder_guid, Folder_FT_flag, dic, list[list.Count() - 1]);

					Lite_Folder.Insert(l);

					_Folders.Add(l);

					l.InsertFolder(dic);
				}
				else
				{
					var l = dic_fo[dic];

					_Folders.Add(l);

					l.CheckFolder(dic);
				}
			}

			var dic_fi = Lite_File.SelectFromFile(guid, Folder_FT_flag, Folder_guid);

			var fis = Directory.GetFiles(_folder);

			foreach (var fic in fis)
			{
				if (dic_fi.ContainsKey(fic) == false)
				{
					var list = fic.Split('\\');

					var l = new Lite_File(guid, Folder_guid, Folder_FT_flag, fic, list[list.Count() - 1]);

					Lite_File.Insert(l);

					_Files.Add(l);

					Lite_FolderGroup.long_DirsSize += l.FileSize;
				}
				else
				{
					var l = dic_fi[fic];

					_Files.Add(l);

					Lite_FolderGroup.long_DirsSize += l.FileSize;
				}
			}
		}
		#endregion
	}
}
