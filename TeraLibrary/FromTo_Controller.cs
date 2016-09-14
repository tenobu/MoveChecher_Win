using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Threading;
//using System.Threading.Tasks;

namespace TeraLibrary
{
	public class FromTo_Controller
	{
		// 簡素化３
		public string str_Name = "", str_Message = "", str_Status = "", str_Error = "";
		//public DateTime dt_Start = DateTime.Now, dt_Now = DateTime.Now, dt_End = DateTime.Now;
		public bool bool_EndFlag = false;

		public long long_F_SumiSize = 0L, long_T_SumiSize = 0L;
		public long long_F_FileSize = 0L, long_T_FileSize = 0L;

		public string str_T_FileName = "";

		public FromTo_Data ft_Data = null;

		public Dictionary<string, FromTo_Data> dic_FT = null;

		public bool bool_WaitFlag = false;

		private CancellationTokenSource cts_Cancel = null;


		public FromTo_Controller(string a_path, string b_base_path)
		{
			bool_WaitFlag = true;

			str_Status = "Normal";

			if (Directory.Exists(a_path) == false)
			{
				str_Error = "From がディレクトリで無い！！";
				str_Status = "Abend";
				bool_WaitFlag = false;
				return;
			}

			var di_a = new DirectoryInfo(a_path);

			if (Directory.Exists(b_base_path) == false)
			{
				str_Error = "ToBase がディレクトリで無い！！";
				str_Status = "Abend";
				bool_WaitFlag = false;
				return;
			}

			var di_b = new DirectoryInfo((b_base_path + @"\" + di_a.Name).Replace(@"\\", @"\"));

			if (di_a.FullName.Equals(b_base_path) ||
				di_a.FullName.Equals(di_b.FullName))
			{
				str_Error = "同じディレクトリ！！";
				str_Status = "Abend";
				bool_WaitFlag = false;
				return;
			}

			dic_FT   = new Dictionary<string , FromTo_Data>();

			str_Name = di_a.Name;

			cts_Cancel = new CancellationTokenSource();

			ft_Data = new FromTo_Data(this, true, di_a.Name, di_a.FullName, di_b.FullName, cts_Cancel);
		}

		public void Check()
		{
			bool_WaitFlag = true;

			str_Message = "フォルダ？をチェック中！！";
			str_Status  = "Normal";

			var end_flag = true;
			long_F_SumiSize = long_T_SumiSize = 0L;

			var token = cts_Cancel.Token;

            Task.Factory.StartNew(() => {

				try
				{
					ft_Data.CheckDirectory(dic_FT);

					if (token.IsCancellationRequested)
					{
						str_Error  = "Error Cancelされました！！";
						str_Status = "Abend";

						return;
					}

					foreach (var ab in dic_FT.Values)
					{
						if (end_flag && ab.bool_CopyEnd == false) end_flag = false;

						if (token.IsCancellationRequested)
						{
							str_Error  = "Error Cancelされました！！";
							str_Status = "Abend";

							return;
						}
					}

					bool_EndFlag = end_flag;

					Console.WriteLine("End = " + bool_EndFlag);
				}
				catch(Exception e)
				{
					str_Error  = "Error From " + e.Message;
					str_Status = "Abend";
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
			str_Status  = "Normal";

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
						str_Status = "Abend";

						return;
					}

					if (end_flag) bool_EndFlag = true;

					bool_EndFlag = end_flag;

					Console.WriteLine("End = " + bool_EndFlag);
				}
				catch (Exception e)
				{
					str_Error = "Error To " + e.Message;
					str_Status = "Abend";
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
			str_Status  = "Normal";

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
					str_Status = "Abend";
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
		public FromTo_Controller ft_Cntl = null;

		public string str_Type = "";
		public string str_Name = "", str_F_FullName = "", str_T_FullName = "";
		public long long_F_Length = 0, long_T_Length = 0;
		public bool bool_To_Flag = false, bool_CopyEnd = false, bool_DeleteEnd = false;

		public List<FromTo_Data> ft_Datas = null;

		private CancellationTokenSource cts_Cancel = null;


		public FromTo_Data(
			FromTo_Controller ft, bool is_dir, string str_name, string str_f_full, string str_t_full,
			CancellationTokenSource _cts)
		{
			ft_Cntl = ft;

			ft.
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

		public void CheckDirectory(Dictionary<string, FromTo_Data> dic_FT)
		{
			var token = cts_Cancel.Token;

			ft_Datas = new List<FromTo_Data>();

			foreach (var c_a_dir in Directory.GetFiles(str_F_FullName))
			{
				var name = GetFileName(c_a_dir);

				var ft_data = new FromTo_Data(
					ft_Cntl, false, name, c_a_dir, str_T_FullName + @"\" + name, cts_Cancel);

				if (token.IsCancellationRequested)
				{
					return;
				}

				ft_data.CheckFile(dic_FT);

				ft_Datas.Add(ft_data);

				if (token.IsCancellationRequested)
				{
					return;
				}
			}


			foreach (var c_a_dir in Directory.GetDirectories(str_F_FullName))
			{
				var name = GetFileName(c_a_dir);

				var ft_data = new FromTo_Data(
					ft_Cntl, true, name, c_a_dir, str_T_FullName + @"\" + name, cts_Cancel);

				if (token.IsCancellationRequested)
				{
					return;
				}

				ft_data.CheckDirectory(dic_FT);

				ft_Datas.Add(ft_data);

				if (token.IsCancellationRequested)
				{
					return;
				}
			}

			if (Directory.Exists(str_T_FullName))
			{
				bool_CopyEnd = true;
			}

			dic_FT.Add(str_F_FullName, this);
		}

		public void CheckFile(Dictionary<string, FromTo_Data> dic_AB)
		{
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

			dic_AB.Add(str_F_FullName, this);
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
