using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace MoveChecker
{
	public class DirectoryInfo_
	{
		protected string str_FullName = "";

		protected List<DirectoryInfo_> list_DireInfo = null;
		protected List<FileInfo_     > list_FileInfo = null;


		public DirectoryInfo_()
		{

		}

		//public bool FromExists(string str_path)
	}

	public class FileInfo_
	{
		protected string str_FullName = "";


		public FileInfo_()
		{

		}
	}

	public class Directory_Controller
	{
		protected string str_Name = "", str_Message = "", str_Status = "", str_Error = "";

		protected bool bool_EndFlag = false, bool_WaitFlag = false;

		protected string str_DirPath = "";

		protected long long_SumiSize = 0L, long_FileSize = 0L;

		public Directory_Controller()
		{
			str_Status = "Normal";
		}

		public string Path
		{
			get
			{
				return str_DirPath;
			}
		}
	}

	public class From_Controller : Directory_Controller
	{
		private To_Controller toCntl = null;


		public string Path
		{
			get
			{
				return str_DirPath;
			}
			set
			{
				if (Directory.Exists(value) == false)
				{
					str_Error = "From がディレクトリで無い！！";
					str_Status = "Abend";
					bool_WaitFlag = false;

					return;
				}

				str_DirPath = value;

				var di_a = new DirectoryInfo(str_DirPath);
			}
		}

		public void ToCntl(To_Controller to_cntl)
		{
			toCntl = to_cntl;
		}
	}

	public class To_Controller : Directory_Controller
	{
		private string str_Base_DirPath = "", str_PathName = "";
	}


}
