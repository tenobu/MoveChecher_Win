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

		public AB_Data ab_Data = null;

		public Dictionary<string, AB_Data> dic_AB = null;

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

			var di_b = new DirectoryInfo(b_base_path + @"\" + di_a.Name);

			dic_AB = new Dictionary<string, AB_Data>();


			str_Name = di_a.Name;

			ab_Data = new AB_Data(dic_AB, di_a, di_b);
		}

		public void Check()
		{
			bool_WaitFlag = true;

			if (str_Status.Equals("Abend") == false)
			{
				try
				{
					ab_Data.CheckDirectory(dic_AB);
				}
				catch(Exception e)
				{
					str_Error  = "Error A " + e.Message;
					str_Status = "Abend";
				}
			}

			bool_WaitFlag = false;
		}

		public void Copy()
		{
			bool_WaitFlag = true;

			if (str_Status.Equals("Abend") == false)
			{
				try
				{
					ab_Data.Copy();
				}
				catch (Exception e)
				{
					str_Error = "Error B " + e.Message;
					str_Status = "Abend";
				}
			}

			bool_WaitFlag = false;
		}

		public long A_Size()
		{
			return 0L;
		}

		public long B_Size()
		{
			return 0L;
		}
	}

	public class AB_Data
	{
		public string type = "";
		public string Name = "", a_FullName = "", b_FullName = "";
		public long a_Length = 0, b_Length = 0;
		public bool b_Flag = false, copyEnd = false;

		public DirectoryInfo di_A = null, di_B = null;
		public FileInfo fi_A = null, fi_B = null;

		public List<AB_Data> ab_Datas = null;

 
		public AB_Data(Dictionary<string, AB_Data> dic_AB, DirectoryInfo di_a, DirectoryInfo di_b)
		{
			type = "Dir";

			Name = di_a.Name;

			a_FullName = di_a.FullName;
			b_FullName = di_b.FullName;

			a_Length = b_Length = 0;

			di_A = di_a;
			di_B = di_b;

			//dic_AB.Add(a_FullName, this);
		}

		public AB_Data(Dictionary<string, AB_Data> dic_AB, FileInfo fi_a, FileInfo fi_b)
		{
			type = "File";

			Name = fi_a.Name;

			a_FullName = fi_a.FullName;
			b_FullName = fi_b.FullName;

			a_Length = fi_a.Length;
			if (fi_b.Exists)
			{
				b_Length = fi_b.Length;
			}

			fi_A = fi_a;
			fi_B = fi_b;

			//dic_AB.Add(a_FullName, this);
		}

		public void CheckDirectory(Dictionary<string, AB_Data> dic_AB)
		{
			ab_Datas = new List<AB_Data>();

			foreach (var fi_c_a in di_A.GetFiles())
			{
				var fi_c_b = new FileInfo(b_FullName + @"\" + fi_c_a.Name);

				var ab_data = new AB_Data(dic_AB, fi_c_a, fi_c_b);

				ab_data.CheckFile(dic_AB);

				ab_Datas.Add(ab_data);
			}

			foreach (var di_c_a in di_A.GetDirectories())
			{
				var di_c_b = new DirectoryInfo(b_FullName + @"\" + di_c_a.Name);

				var ab_data = new AB_Data(dic_AB, di_c_a, di_c_b);

				ab_data.CheckDirectory(dic_AB);

				ab_Datas.Add(ab_data);
			}

			dic_AB.Add(a_FullName, this);
		}

		public void CheckFile(Dictionary<string, AB_Data> dic_AB)
		{
			dic_AB.Add(a_FullName, this);
		}

		public void Copy()
		{
			loop:
		
			switch (type)
			{
				case "Dir":
 
					if (di_B.Exists == false)
					{
						di_B.Create();

						di_B = new DirectoryInfo(b_FullName);

						goto loop;
					}

					foreach (var ab_data in ab_Datas)
					{
						ab_data.Copy();
					}

					break;
				
				case "File":

					if (copyEnd == false)
					{
						if (fi_B.Exists == false)
						{
							fi_A.CopyTo(b_FullName, true);

							fi_B = new FileInfo(b_FullName);

							if (fi_B.Exists)
							{
								b_Length = fi_B.Length;
							}

							goto loop;
						}
						else
						{
							if (a_Length == b_Length)
							{
								if (file_compare(a_FullName, b_FullName) == false)
								{
									fi_A.CopyTo(b_FullName, true);

									fi_B = new FileInfo(b_FullName);

									if (fi_B.Exists)
									{
										b_Length = fi_B.Length;
									}

									goto loop;
								}
								else
								{
									copyEnd = true;
								}
							}
							else
							{
								fi_A.CopyTo(b_FullName, true);

								fi_B = new FileInfo(b_FullName);

								if (fi_B.Exists)
								{
									b_Length = fi_B.Length;
								}

								goto loop;
							}
						}
					}

					break;
			}
		}

		private bool file_compare(string file_name1, string file_name2)
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
}
