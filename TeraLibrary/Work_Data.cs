using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;

namespace TeraLibrary
{
	public class Work_Data
	{
		public string str_Name = "", str_Status = "", str_Error = "";
		public DateTime dt_Start = DateTime.Now, dt_Now = DateTime.Now, dt_End = DateTime.Now;
		public bool bool_EndFlag = false;

		public long long_A_Size = 0L, long_B_Size = 0L;

		public AB_Data ab_Data = null;

		public Dictionary<string, AB_Data> dic_AB = null;
		public Dictionary<AB_Data, AB_Flag> dic_Flag = null;

		public bool bool_WaitFlag = false;


		public Work_Data(string a_path, string b_base_path)
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

			dic_AB   = new Dictionary<string , AB_Data>();
			dic_Flag = new Dictionary<AB_Data, AB_Flag>();


			str_Name = di_a.Name;

			//2016/08/18 11:22:00
			//ab_Data = new AB_Data(this, di_a, di_b);
			ab_Data = new AB_Data(this, true, di_a.Name, di_a.FullName, di_b.FullName);
		}

		public void Check()
		{
			bool_WaitFlag = true;

			if (str_Status.Equals("Abend") == false)
			{
				var end_flag = true;
				long_A_Size = long_B_Size = 0L;

				ab_Data.CheckDirectory(dic_AB, dic_Flag);

				foreach (var ab in dic_AB.Values)
				{
					if (end_flag && ab.bool_CopyEnd == false) end_flag = false;

					//long_A_Size += ab.a_Length;
					//long_B_Size += ab.b_Length;
				}

				//long_AB_Size = long_A_Size;

				bool_EndFlag = end_flag;

				Console.WriteLine("End = " + bool_EndFlag);
			}

			bool_WaitFlag = false;
		}

		public void Copy()
		{
			bool_WaitFlag = true;

			if (str_Status.Equals("Abend") == false)
			{
				//try
				//{
				ab_Data.Copy();

				var end_flag = true;
				long_B_Size = 0L;

				foreach (var ab in dic_AB.Values)
				{
					if (end_flag && ab.bool_CopyEnd == false) end_flag = false;

					long_B_Size += ab.long_B_Length;
				}

				if (end_flag) bool_EndFlag = true;

				bool_EndFlag = end_flag;
				/*}
				catch (Exception e)
				{
					str_Error = "Error B " + e.Message;
					str_Status = "Abend";
				}*/
			}

			bool_WaitFlag = false;
		}

		public void Delete()
		{
			bool_WaitFlag = true;

			if (str_Status.Equals("Abend") == false)
			{
				//try
				//{
				ab_Data.Delete();

				var end_flag = true;
				long_A_Size = 0L;

				foreach (var ab in dic_AB.Values)
				{
					if (end_flag && ab.bool_DeleteEnd == false) end_flag = false;

					if (ab.str_Type.Equals("Dir"))
					{
						//2016/08/18 13:40:00
						//if (ab.di_A.Exists == true)
						if (Directory.Exists(ab.str_A_FullName) == true)
						{
							if (end_flag && ab.bool_DeleteEnd == false) end_flag = false;
						}
					}
					else if (
						ab.str_Type.Equals("File"))
					{
						//2016/08/18 13:40:00
						//if (ab.di_A.Exists == true)
						if (File.Exists(ab.str_A_FullName) == true)
						{
							if (end_flag && ab.bool_DeleteEnd == false) end_flag = false;

							long_A_Size += ab.long_A_Length;
						}
					}
				}

				bool_EndFlag = end_flag;
				/*}
				catch (Exception e)
				{
					str_Error = "Error B " + e.Message;
					str_Status = "Abend";
				}*/
			}

			bool_WaitFlag = false;
		}
	}

	public class AB_Data
	{
		public Work_Data wk_Data = null;

		public string str_Type = "";
		public string str_Name = "", str_A_FullName = "", str_B_FullName = "";
		public long long_A_Length = 0, long_B_Length = 0;
		public bool bool_B_Flag = false, bool_CopyEnd = false, bool_DeleteEnd = false;

		//public DirectoryInfo di_A = null, di_B = null;
		//public FileInfo fi_A = null, fi_B = null;

		public List<AB_Data> ab_Datas = null;


		//2016/08/18 11:22:00
		/*public AB_Data(Work_Data wk, DirectoryInfo di_a, DirectoryInfo di_b)
		{
			wk_Data = wk;

			type = "Dir";

			Name = di_a.Name;

			a_FullName = di_a.FullName;
			b_FullName = di_b.FullName;

			a_Length = b_Length = 0;

			di_A = di_a;
			di_B = di_b;
		}*/

		//2016/08/18 11:22:00
		/*public AB_Data(Work_Data wk, FileInfo fi_a, FileInfo fi_b)
		{
			wk_Data = wk;

			type = "File";

			Name = fi_a.Name;

			a_FullName = fi_a.FullName;
			b_FullName = fi_b.FullName;

			a_Length = fi_a.Length;
			if (fi_b.Exists)
			{
				b_Length = fi_b.Length;
			}

			wk_Data.long_A_Size += a_Length;
			wk_Data.long_B_Size += b_Length;

			fi_A = fi_a;
			fi_B = fi_b;
		}*/

		//2016/08/18 11:22:00
		public AB_Data(Work_Data wk, bool is_dir, string str_name, string str_a_full, string str_b_full)
		{
			wk_Data = wk;

			str_Name = str_name;

			str_A_FullName = str_a_full;
			str_B_FullName = str_b_full;

			if (is_dir)
			{
				str_Type = "Dir";

				long_A_Length = long_B_Length = 0;
			}
			else
			{
				str_Type = "File";

				long_A_Length = new FileInfo(str_A_FullName).Length;
				if (File.Exists(str_B_FullName))
				{
					long_B_Length = new FileInfo(str_B_FullName).Length;
				}

				wk_Data.long_A_Size += long_A_Length;
				wk_Data.long_B_Size += long_B_Length;
			}

			//di_A = di_a;
			//di_B = di_b;
		}

		public void CheckDirectory(
			Dictionary<string, AB_Data> dic_AB, Dictionary<AB_Data, AB_Flag> dic_Flag)
		{
			ab_Datas = new List<AB_Data>();

			//2016/08/18 12:45:00
			//foreach (var fi_c_a in di_A.GetFiles())
			foreach (var c_a_dir in Directory.GetFiles(str_A_FullName))
			{
				//2016/08/18 11:55:00
				//var fi_c_b = new FileInfo(b_FullName + @"\" + fi_c_a.Name);
				var list = c_a_dir.Split('\\');
				var name = list[list.Count() - 1];

				//2016/08/18 12:45:00
				//var ab_data = new AB_Data(wk_Data, fi_c_a, fi_c_b);
				var ab_data = new AB_Data(wk_Data, false, name, c_a_dir, str_B_FullName + @"\" + name);

				ab_data.CheckFile(dic_AB, dic_Flag);

				ab_Datas.Add(ab_data);
			}


			//2016/08/18 13:10:00
			//foreach (var di_c_a in di_A.GetDirectories())
			foreach (var c_a_dir in Directory.GetDirectories(str_A_FullName))
			{
				//2016/08/18 12:35:00
				//var di_c_b = new DirectoryInfo(b_FullName + @"\" + di_c_a.Name);
				var list = c_a_dir.Split('\\');
				var name = list[list.Count() - 1];

				//var ab_data = new AB_Data(wk_Data, di_c_a, di_c_b);
				var ab_data = new AB_Data(wk_Data, true, name, c_a_dir, str_B_FullName + @"\" + name);

				ab_data.CheckDirectory(dic_AB, dic_Flag);

				ab_Datas.Add(ab_data);
			}

			//2016/08/18 13:10:00
			//if (di_B.Exists)
			if (Directory.Exists(str_B_FullName))
			{
				bool_CopyEnd = true;
			}

			dic_AB.Add(str_A_FullName, this);
			//dic_Flag.Add(this, new AB_Flag());
		}

		public void CheckFile(
			Dictionary<string, AB_Data> dic_AB, Dictionary<AB_Data, AB_Flag> dic_Flag)
		{
			//2016/08/18 13:10:00
			//if (fi_B.Exists)
			if (File.Exists(str_B_FullName))
			{
				//2016/08/18 13:10:00
				//if (FileCompare(fi_A.FullName, fi_B.FullName))
				if (FileCompare(str_A_FullName, str_B_FullName))
				{
					bool_CopyEnd = true;
				}
				else
				{
					long_B_Length = 0L;
				}
			}

			dic_AB.Add(str_A_FullName, this);
			//dic_Flag.Add(this, new AB_Flag());
		}

		public void Copy()
		{
		loop:

			switch (str_Type)
			{
				case "Dir":

					//if (di_B.Exists == false)
					if (Directory.Exists(str_B_FullName) == false)
					{
						/*di_B.Create();

						di_B = new DirectoryInfo(str_B_FullName);*/
						Directory.CreateDirectory(str_B_FullName);

						goto loop;
					}

					foreach (var ab_data in ab_Datas)
					{
						ab_data.Copy();
					}

					break;

				case "File":

					if (bool_CopyEnd == false)
					{
						//if (fi_B.Exists == false)
						if (File.Exists(str_B_FullName) == false)
						{
							//2016/08/18 13:30:00
							FileCopy();

							goto loop;
						}
						else
						{
							if (long_A_Length == long_B_Length)
							{
								if (FileCompare(str_A_FullName, str_B_FullName) == false)
								{
									//2016/08/18 13:30:00
									FileCopy();

									goto loop;
								}
								else
								{
									bool_CopyEnd = true;
								}
							}
							else
							{
								//2016/08/18 13:30:00
								FileCopy();

								goto loop;
							}
						}
					}

					break;
			}
		}

		//2016/08/18 13:30:00
		private void FileCopy()
		{
			/*fi_A.CopyTo(str_B_FullName, true);

			fi_B = new FileInfo(str_B_FullName);*/
			File.Copy(str_A_FullName, str_B_FullName, true);

			//fi_B = new FileInfo(str_B_FullName);

			//if (fi_B.Exists)
			if (File.Exists(str_B_FullName))
			{
				//long_B_Length = fi_B.Length;
				long_B_Length = new FileInfo(str_B_FullName).Length;
			}
		}

		public void Delete()
		{
		loop:

			switch (str_Type)
			{
				case "File":

					if (bool_DeleteEnd == false)
					{
						//2016/08/18 13:30:00
						//if (fi_A.Exists)
						if (File.Exists(str_A_FullName))
						{
							//2016/08/18 13:30:00
							/*fi_A.Delete();

							fi_A = new FileInfo(str_A_FullName);*/
							File.Delete(str_A_FullName);

							//2016/08/18 13:30:00
							//if (fi_A.Exists) goto loop;
							if (File.Exists(str_A_FullName)) goto loop;
						}

						bool_DeleteEnd = true;
					}

					break;

				case "Dir":

					foreach (var ab_data in ab_Datas)
					{
						ab_data.Delete();
					}

					//2016/08/18 13:30:00
					//if (di_A.Exists)
					if (Directory.Exists(str_A_FullName))
					{
						//2016/08/18 13:30:00
						/*di_A.Delete();

						di_A = new DirectoryInfo(str_A_FullName);*/
						Directory.Delete(str_A_FullName);

						//2016/08/18 13:30:00
						//if (di_A.Exists) goto loop;
						if (Directory.Exists(str_A_FullName)) goto loop;
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

			/*if (di.Name.StartsWith("$"))
			{
				return size;
			}*/

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
	}

	public class AB_Flag
	{
		public bool bool_EndFlag = false;


		public AB_Flag()
		{

		}
	}
}
