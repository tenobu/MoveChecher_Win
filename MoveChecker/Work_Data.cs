using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;

namespace MoveChecker
{
	public class Work_Data
	{
		public bool endFlag = false;
		public string Name = "";

		public AB_Data a = null;
		public AB_Data b = null;

		public Work_Data(DirectoryInfo dir_a, DirectoryInfo dir_b)
		{
			a = new AB_Data(dir_a);
			b = new AB_Data(dir_b);

			CheckData();
		}

		public Work_Data(FileInfo file_a, FileInfo file_b)
		{
			a = new AB_Data(file_a);
			b = new AB_Data(file_b);

			CheckData();
		}

		private void CheckData()
		{
			if (a.Name.Equals(b.Name) == false)
			{
				Name = "Error";
				return;
			}

			Name = a.Name;




			var from_files = from_dir.GetFiles();
			var dic_from = new Dictionary<string, FileInfo>();

			foreach (var fi in from_files)
			{
				dic_from.Add(fi.Name, fi);
			}

			var to_files = to_dir.GetFiles();
			var dic_to = new Dictionary<string, FileInfo>();

			foreach (var fi in to_files)
			{
				dic_to.Add(fi.Name, fi);
			}

			if (dic_from.Count() != dic_to.Count())
			{
				return false;
			}
		}
	}

	public class AB_Data
	{
		public string type = "";
		public string fullName = "", Name = "";
		public long Length = 0;

		public AB_Data(DirectoryInfo ab)
		{
			type = "Dir";

			fullName = ab.FullName;
			Name = ab.Name;
			Length = 0;
		}

		public AB_Data(FileInfo ab)
		{
			type = "File";
			
			fullName = ab.FullName;
			Name = ab.Name;
			Length = ab.Length;
		}
	}

	[Serializable]
	public class Lite_動向調査
	{
		public string
			社員番号, 漢字氏名, 性別, 生年月日, 年齢_年数値, レベル０２名称, レベル０３名称, レベル０４名称,
			所属略称, 駐在, 建家名称, 建家順位, 役職, 担当１名称, 留・勤区分, 留・勤期間開始日, 留・勤機関名,
			社員区分名, 社員区分変更日, 職責ランク名称, 職責ランク順位, 入社経路, グループ入社日, 入社日付,
			退職区分, 退職日付, 退職事由;
	}

	public class Lite_Flag
	{
		public Boolean Flag = true;
	}

	public class Dic_Hantosi
	{
		public Lite_動向調査 l_前 = null;
		public Lite_動向調査 l_後 = null;

		public string str_Basyo = null;


		public Dic_Hantosi()
		{
		}
	}

	public class Dic_SyainNo : Dictionary<string, Dic_Hantosi>
	{
	}

	public static class Util
	{
		/// <summary>
		/// 指定されたインスタンスの深いコピーを生成し返します
		/// </summary>
		/// <typeparam name="T">コピーするインスタンスの型</typeparam>
		/// <param name="original">コピー元のインスタンス</param>
		/// <returns>指定されたインスタンスの深いコピー</returns>
		public static T DeepCopy<T>(T original)
		{
			// シリアル化した内容を保持するメモリーストリームを生成
			MemoryStream stream = new MemoryStream();
			try
			{
				// バイナリ形式でシリアライズするためのフォーマッターを生成
				var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				// コピー元のインスタンスをシリアライズ
				formatter.Serialize(stream, original);
				// メモリーストリームの現在位置を先頭に設定
				stream.Position = 0L;
				// メモリーストリームの内容を逆シリアル化
				return (T)formatter.Deserialize(stream);
			}
			finally
			{
				stream.Close();
			}
		}
	}

	public class DanJyo
	{
		public string[] str_Sex = { "男性", "女性" };

		public int[] int_Nasi, int_Ari, int_Kei;

		public int int_GoKei;

		public DanJyo()
		{
			int_Nasi = new int[2];
			int_Ari  = new int[2];
			int_Kei  = new int[2];

			int_Nasi[0] = int_Ari[0] = int_Kei[0] = int_Nasi[1] = int_Ari[1] = int_Kei[1] = int_GoKei = 0;
		}

		public void Soukei()
		{
			int_Kei[0] = int_Nasi[0] + int_Ari[0];
			int_Kei[1] = int_Nasi[1] + int_Ari[1];

			int_GoKei = int_Kei[0] + int_Kei[1];
		}
	}
}
