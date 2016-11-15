using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Threading;
//using System.Threading.Tasks;

namespace MoveChecker
{
	public class Folder_Data
	{
		public string str_FullName = "", str_Name = "";
		
		public bool bool_EndFlag = false;

		public long long_Size = 0L;

		public DateTime date_DT;

		public List<Folder_Data> ChildsData = new List<Folder_Data>();


		static public bool Read(Folder_Data fd)
		{
			return false;
		}
	}
}
