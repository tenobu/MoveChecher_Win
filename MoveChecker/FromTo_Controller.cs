using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Threading;
//using System.Threading.Tasks;

using TeraLibrary.MoveChecker.SQLite;

namespace MoveChecker
{
	public class FromTo_Controller
	{
		// 簡素化３
		public string str_Name = "", str_Message = ""/*, str__Status = ""*/, str_Error = "";
		public bool bool_EndFlag = false;

		public long long_F_DirsSize = 0L;
		public long long_F_SumiSize = 0L, long_T_SumiSize = 0L;
		public long long_F_FileSize = 0L, long_T_FileSize = 0L;

		public string str_T_FileName = "";

		public FromTo_Data ft_Data = null;

		public Dictionary<string, FromTo_Data> dic_FT = null;

		public bool bool_WaitFlag = false;

		private CancellationTokenSource cts_Cancel = new CancellationTokenSource();

		public _Level  _level  = _Level .None;
		public _Status _status = _Status.None;

		private Lite_FolderGroup _FolderGroup = null;

	
		// FromTo_Cntl(string f_path, string t_path)
		// 
		//
		public FromTo_Controller(string f_path, string t_base_path)
		{
			FromTo_Data.ft_Cntl = this;

			_level = _Level.Check;

			_status = _Status.Normal;

			if (Directory.Exists(f_path) == false)
			{
				str_Error = "From がディレクトリで無い！！";
				_status = _Status.Abend;
				bool_WaitFlag = false;
				return;
			}

			var di_a = new DirectoryInfo(f_path);

			if (Directory.Exists(t_base_path) == false)
			{
				str_Error = "ToBase がディレクトリで無い！！";
				_status = _Status.Abend;
				bool_WaitFlag = false;
				return;
			}

			var di_b = new DirectoryInfo((t_base_path + @"\" + di_a.Name).Replace(@"\\", @"\"));

			if (di_a.FullName.Equals(t_base_path) ||
				di_a.FullName.Equals(di_b.FullName))
			{
				str_Error = "同じディレクトリ！！";
				_status = _Status.Abend;
				bool_WaitFlag = false;
				return;
			}

			var cnt = Lite_FolderGroup.SelectFromCount();
			if (cnt == -1)
			{
				Lite_FolderGroup.CommandNonQuery(Lite_FolderGroup.CreateTableString());
			}

			cnt = Lite_Folder.SelectFromCount();
			if (cnt == -1)
			{
				Lite_Folder.CommandNonQuery(Lite_Folder.CreateTableString());
			}

			cnt = Lite_File.SelectFromCount();
			if (cnt == -1)
			{
				Lite_File.CommandNonQuery(Lite_File.CreateTableString());
			}

			var list = Lite_FolderGroup.SelectFromFromTo(f_path, t_base_path);
			if (list.Count == 0)
			{
				var l = new Lite_FolderGroup(f_path, t_base_path);

				if (Lite_FolderGroup.Insert(l))
				{
					_FolderGroup = l;
				}
				else
				{
					new Exception();
				}
			}
			else if (
				list.Count == 1)
			{
				_FolderGroup = list[0];
			}
			else
			{
				Console.WriteLine("list count >= 2");
				new Exception("list count >= 2");
			}

			_FolderGroup.CheckFolder();

			dic_FT   = new Dictionary<string , FromTo_Data>();

			str_Name = di_a.Name;

			ft_Data = new FromTo_Data(true, di_a.Name, di_a.FullName, di_b.FullName, cts_Cancel);
		}

		private void CheckDirectory()
		{
			//var di = new DirectoryInfo(_FolderGroup.FromFolder);

			//DirectorySize(di);
		}

		/*private void DirectorySize(DirectoryInfo directoryInfo)
		{
			if (cts_Cancel.Token.IsCancellationRequested)
			{
				return;
			}

			foreach (var fi in directoryInfo.EnumerateFiles())
			{
				var l = new Lite_File(_FolderGroup.guid, fi.FullName);
			}

			long totalSize = directoryInfo.EnumerateFiles().Sum(file => file.Length);

			foreach (var dir in directoryInfo.EnumerateDirectories())
			{
				DirectorySize(dir);
			}

			//ft_Cntl.long_F_DirsSize += totalSize;
		}*/

		public void Check()
		{
			bool_WaitFlag = true;

			str_Message = "フォルダ？をチェック中！！";
			_status = _Status.Normal;

			//var end_flag = true;
			long_F_DirsSize = long_F_SumiSize = long_T_SumiSize = 0L;

			var token = cts_Cancel.Token;

            Task.Factory.StartNew(() => {

				try
				{
					ft_Data.CheckDirectory_0();

					if (token.IsCancellationRequested)
					{
						str_Error = "Error Cancelされました！！";
						_status = _Status.Abend;

						return;
					}
					
					bool_EndFlag &= ft_Data.CheckDirectory();

					if (token.IsCancellationRequested)
					{
						str_Error  = "Error Cancelされました！！";
						_status = _Status.Abend;

						return;
					}

					Console.WriteLine("End = " + bool_EndFlag);
				}
				catch(Exception e)
				{
					str_Error  = "Error From " + e.Message;
					_status = _Status.Abend;
				}
				finally
				{
					bool_WaitFlag = false;
				}

			});
		}


		public void Copy()
		{
			bool_WaitFlag = true;

			str_Message = "コピー中！！";
			_status = _Status.Normal;

			var token = cts_Cancel.Token;

			Task.Factory.StartNew(() =>
			{

				try
				{
					var end_flag = true;

					ft_Data.Copy();

					if (token.IsCancellationRequested)
					{
						str_Error  = "Error Cancelされました！！";
						_status = _Status.Abend;

						return;
					}

					if (end_flag) bool_EndFlag = true;

					bool_EndFlag = end_flag;

					Console.WriteLine("End = " + bool_EndFlag);
				}
				catch (Exception e)
				{
					str_Error = "Error To " + e.Message;
					_status = _Status.Abend;
				}
				finally
				{
					bool_WaitFlag = false;
				}

			});
		}

		public void Delete()
		{
			bool_WaitFlag = true;

			str_Message = "削除中！！";
			_status = _Status.Normal;

			var token = cts_Cancel.Token;

			Task.Factory.StartNew(() =>
			{

				try
				{
					var end_flag = true;
					long_F_SumiSize = 0L;

					ft_Data.Delete();

					if (token.IsCancellationRequested)
					{
						return;
					}

					foreach (var ab in dic_FT.Values)
					{
						if (end_flag && ab.bool_DeleteEnd == false) end_flag = false;

						if (ab.str_Type.Equals("Dir"))
						{
							if (Directory.Exists(ab.str_F_FullName) == true)
							{
								if (end_flag && ab.bool_DeleteEnd == false) end_flag = false;
							}
						}
						else if (
							ab.str_Type.Equals("File"))
						{
							if (File.Exists(ab.str_F_FullName) == true)
							{
								if (end_flag && ab.bool_DeleteEnd == false) end_flag = false;

								long_F_SumiSize += ab.long_F_Length;
							}
						}

						if (token.IsCancellationRequested)
						{
							return;
						}
					}

					bool_EndFlag = end_flag;

					if (end_flag) bool_EndFlag = true;

					Console.WriteLine("End = " + bool_EndFlag);
				}
				catch (Exception e)
				{
					str_Error = "Error From " + e.Message;
					_status = _Status.Abend;
				}
				finally
				{
					bool_WaitFlag = false;
				}

			});
		}

		public void Cancel()
		{
			cts_Cancel.Cancel();
		}
	}

	public class FromTo_Data
	{
		static public FromTo_Controller ft_Cntl = null;

		public string str_Type = "";
		public string str_Name = "", str_F_FullName = "", str_T_FullName = "";
		public long long_F_Length = 0, long_T_Length = 0;
		public bool bool_To_Flag = false, bool_CopyEnd = false, bool_DeleteEnd = false;

		public List<FromTo_Data> ft_Datas = null;

		private CancellationTokenSource cts_Cancel = null;


		public FromTo_Data(
			bool is_dir, string str_name, string str_f_full, string str_t_full,
			CancellationTokenSource _cts)
		{
			str_Name = str_name;

			str_F_FullName = str_f_full;
			str_T_FullName = str_t_full;

			if (is_dir)
			{
				str_Type = "Dir";

				long_F_Length = long_T_Length = 0;
			}
			else
			{
				str_Type = "File";

				long_F_Length = new FileInfo(str_F_FullName).Length;
				if (File.Exists(str_T_FullName))
				{
					long_T_Length = new FileInfo(str_T_FullName).Length;
				}
			}

			cts_Cancel = _cts;
		}

		public void CheckDirectory_0()
		{
			var di = new DirectoryInfo(str_F_FullName);

			//ft_Cntl.long_F_DirsSize = DirectorySize_1(di);
			DirectorySize_2(di);
		}

		private long DirectorySize_1(DirectoryInfo directoryInfo)
		{
			/*long totalSize = directoryInfo.EnumerateFiles().Sum(file => file.Length);

			totalSize += directoryInfo.EnumerateDirectories().Sum(dir => DirectorySize(dir));

			return totalSize;*/

			if (cts_Cancel.Token.IsCancellationRequested)
			{
				return 0;
			}

			long totalSize = directoryInfo.EnumerateFiles().Sum(file => file.Length);

			directoryInfo.EnumerateDirectories().Sum(dir => DirectorySize_1(dir));

			ft_Cntl.long_F_DirsSize += totalSize;

			return totalSize;
		}

		private void DirectorySize_2(DirectoryInfo directoryInfo)
		{
			if (cts_Cancel.Token.IsCancellationRequested)
			{
				return;
			}

			long totalSize = directoryInfo.EnumerateFiles().Sum(file => file.Length);

			/*directoryInfo.EnumerateDirectories(
				dir => DirectorySize_2(dir)
			);*/

			foreach (var dir in directoryInfo.EnumerateDirectories())
			{
				DirectorySize_2(dir);
			}
			
			ft_Cntl.long_F_DirsSize += totalSize;
		}

		public bool CheckDirectory()
		{
			var token = cts_Cancel.Token;

			ft_Datas = new List<FromTo_Data>();

			var end_flag_f = true;
			var end_flag_d = true;

			foreach (var c_a_dir in Directory.GetFiles(str_F_FullName))
			{
				var name = GetFileName(c_a_dir);

				var ft_data = new FromTo_Data(
					false, name, c_a_dir, str_T_FullName + @"\" + name, cts_Cancel);

				if (token.IsCancellationRequested)
				{
					return false;
				}

				end_flag_f &= ft_data.CheckFile(/*dic_FT*/);

				ft_Datas.Add(ft_data);

				if (token.IsCancellationRequested)
				{
					return false;
				}
			}


			foreach (var c_a_dir in Directory.GetDirectories(str_F_FullName))
			{
				var name = GetFileName(c_a_dir);

				var ft_data = new FromTo_Data(
					true, name, c_a_dir, str_T_FullName + @"\" + name, cts_Cancel);

				if (token.IsCancellationRequested)
				{
					return false;
				}

				end_flag_d &= ft_data.CheckDirectory(/*dic_FT*/);

				ft_Datas.Add(ft_data);

				if (token.IsCancellationRequested)
				{
					return false;
				}
			}

			if (Directory.Exists(str_T_FullName))
			{
				bool_CopyEnd = true;
			}

			//dic_FT.Add(str_F_FullName, this);

			return end_flag_f & end_flag_d;
		}

		public bool CheckFile(/*Dictionary<string, FromTo_Data> dic_AB*/)
		{
			var end_flag_f = true;

			ft_Cntl.long_F_SumiSize += long_F_Length;
			ft_Cntl.long_F_FileSize++;

			if (File.Exists(str_T_FullName))
			{
				if (FileCompare(str_F_FullName, str_T_FullName))
				{
					bool_CopyEnd = true;

					ft_Cntl.long_T_SumiSize += long_T_Length;
					ft_Cntl.long_T_FileSize++;

					ft_Cntl.str_T_FileName = str_Name;//GetFileName(str_T_FullName);
				}
				else
				{
					long_T_Length = 0L;
				}
			}

			//dic_AB.Add(str_F_FullName, this);

			return end_flag_f;
		}

		public void Copy()
		{
			var token = cts_Cancel.Token;

		loop:

			if (token.IsCancellationRequested)
			{
				return;
			}

			switch (str_Type)
			{
				case "Dir":

					if (Directory.Exists(str_T_FullName) == false)
					{
						ft_Cntl.str_T_FileName = str_T_FullName;

						Directory.CreateDirectory(str_T_FullName);

						goto loop;
					}

					if (token.IsCancellationRequested)
					{
						return;
					}

					foreach (var ft_data in ft_Datas)
					{
						ft_data.Copy();

						if (token.IsCancellationRequested)
						{
							return;
						}
					}

					break;

				case "File":

					if (bool_CopyEnd == false)
					{
						if (File.Exists(str_T_FullName) == false)
						{
							FileCopy();

							goto loop;
						}
						else
						{
							if (long_F_Length == long_T_Length)
							{
								if (FileCompare(str_F_FullName, str_T_FullName) == false)
								{
									FileCopy();

									goto loop;
								}
								else
								{
									bool_CopyEnd = true;

									ft_Cntl.long_T_SumiSize += long_T_Length;
									ft_Cntl.long_T_FileSize++;
								}
							}
							else
							{
								FileCopy();

								goto loop;
							}
						}
					}

					break;
			}
		}

		private void FileCopy()
		{
			File.Copy(str_F_FullName, str_T_FullName, true);

			if (File.Exists(str_T_FullName))
			{
				long_T_Length = new FileInfo(str_T_FullName).Length;

				ft_Cntl.str_T_FileName = str_Name;//GetFileName(str_T_FullName);
			}
		}

		public void Delete()
		{
			var token = cts_Cancel.Token;

		loop:

			if (token.IsCancellationRequested)
			{
				return;
			}

			switch (str_Type)
			{
				case "File":

					if (bool_DeleteEnd == false)
					{
						if (File.Exists(str_F_FullName))
						{
							ft_Cntl.str_T_FileName = str_T_FullName;
						
							File.Delete(str_F_FullName);

							if (File.Exists(str_F_FullName)) goto loop;
						}

						bool_DeleteEnd = true;
					}

					break;

				case "Dir":

					foreach (var ft_data in ft_Datas)
					{
						ft_data.Delete();
					}

					if (Directory.Exists(str_F_FullName))
					{
						ft_Cntl.str_T_FileName = str_T_FullName;

						Directory.Delete(str_F_FullName);

						if (Directory.Exists(str_F_FullName)) goto loop;
					}

					bool_DeleteEnd = true;

					break;
			}
		}

		private bool FileCompare(string file_name1, string file_name2)
		{
			FileStream reader1 = new FileStream(file_name1,
												FileMode.Open,
												FileAccess.Read);

			FileStream reader2 = new FileStream(file_name2,
												FileMode.Open,
												FileAccess.Read);

			int X1, X2;
			bool ret_val = false;
			while (true)
			{
				X1 = reader1.ReadByte();
				X2 = reader2.ReadByte();
				if (X1 != X2) break;    // 比較
				if (X1 == -1) { ret_val = true; break; }
			}
			reader1.Close();
			reader2.Close();

			return ret_val;
		}

		private long SetFilesSize(DirectoryInfo di)
		{
			var size = 0L;

			try
			{
				foreach (var file_c in di.GetFiles())
				{
					size += file_c.Length;
				}
			}
			catch (System.UnauthorizedAccessException e)
			{
				Console.WriteLine(e.Message);
				return size;
			}

			foreach (var dic_c in di.GetDirectories())
			{
				size += SetFilesSize(dic_c);
			}

			return size;
		}

		private string GetFileName(string str_fullname)
		{
			var list = str_fullname.Split('\\');

			if (list.Count() == 0)
			{
				return "";
			}

			return list[list.Count() - 1];
		}
	}
}
