using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	// Dic_役職兼務
	// 
	public class Dic_役職兼務 : Dic_ListList //Dic_PlusPlus
	{
		public string
			社員番号 = "", 漢字氏名, カナ氏名, 退職日付, 退職区分,
			システム使用アドレス,
			レベル０４所属コード, レベル０４名称,
			レベル０５所属コード, レベル０５名称,
			所属コード, 所属名, 役職コード, 役職,
			兼務_兼務番号_1, 兼務_所属コード_1, 兼務_所属名_1, 兼務_役職コード_1, 兼務_役職_1,
			兼務_兼務番号_2, 兼務_所属コード_2, 兼務_所属名_2, 兼務_役職コード_2, 兼務_役職_2,
			兼務_兼務番号_3, 兼務_所属コード_3, 兼務_所属名_3, 兼務_役職コード_3, 兼務_役職_3,
			兼務_兼務番号_4, 兼務_所属コード_4, 兼務_所属名_4, 兼務_役職コード_4, 兼務_役職_4,
			兼務_兼務番号_5, 兼務_所属コード_5, 兼務_所属名_5, 兼務_役職コード_5, 兼務_役職_5,
			兼務_兼務番号_6, 兼務_所属コード_6, 兼務_所属名_6, 兼務_役職コード_6, 兼務_役職_6,
			兼務_兼務番号_7, 兼務_所属コード_7, 兼務_所属名_7, 兼務_役職コード_7, 兼務_役職_7,
			兼務_兼務番号_8, 兼務_所属コード_8, 兼務_所属名_8, 兼務_役職コード_8, 兼務_役職_8,
			兼務_兼務番号_9, 兼務_所属コード_9, 兼務_所属名_9, 兼務_役職コード_9, 兼務_役職_9,
			建家コード, 建家名, 所属管理課コード, 所属管理課;


		public Dic_役職兼務()
		{
			alphabet = "BK";
			topCount = 1;
			columnCount = AlphabetTo.ToInt(alphabet);
		}

		// AddListDatas
		//
		protected override void AddListDatas(List<string> list)
		{
			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(退職日付);
			list.Add(退職区分);
			list.Add(システム使用アドレス);
			list.Add(レベル０４所属コード);
			list.Add(レベル０４名称);
			list.Add(レベル０５所属コード);
			list.Add(レベル０５名称);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(役職コード);
			list.Add(役職);
			list.Add(兼務_兼務番号_1);
			list.Add(兼務_所属コード_1);
			list.Add(兼務_所属名_1);
			list.Add(兼務_役職コード_1);
			list.Add(兼務_役職_1);
			list.Add(兼務_兼務番号_2);
			list.Add(兼務_所属コード_2);
			list.Add(兼務_所属名_2);
			list.Add(兼務_役職コード_2);
			list.Add(兼務_役職_2);
			list.Add(兼務_兼務番号_3);
			list.Add(兼務_所属コード_3);
			list.Add(兼務_所属名_3);
			list.Add(兼務_役職コード_3);
			list.Add(兼務_役職_3);
			list.Add(兼務_兼務番号_4);
			list.Add(兼務_所属コード_4);
			list.Add(兼務_所属名_4);
			list.Add(兼務_役職コード_4);
			list.Add(兼務_役職_4);
			list.Add(兼務_兼務番号_5);
			list.Add(兼務_所属コード_5);
			list.Add(兼務_所属名_5);
			list.Add(兼務_役職コード_5);
			list.Add(兼務_役職_5);
			list.Add(兼務_兼務番号_6);
			list.Add(兼務_所属コード_6);
			list.Add(兼務_所属名_6);
			list.Add(兼務_役職コード_6);
			list.Add(兼務_役職_6);
			list.Add(兼務_兼務番号_7);
			list.Add(兼務_所属コード_7);
			list.Add(兼務_所属名_7);
			list.Add(兼務_役職コード_7);
			list.Add(兼務_役職_7);
			list.Add(兼務_兼務番号_8);
			list.Add(兼務_所属コード_8);
			list.Add(兼務_所属名_8);
			list.Add(兼務_役職コード_8);
			list.Add(兼務_役職_8);
			list.Add(兼務_兼務番号_9);
			list.Add(兼務_所属コード_9);
			list.Add(兼務_所属名_9);
			list.Add(兼務_役職コード_9);
			list.Add(兼務_役職_9);
			list.Add(建家コード);
			list.Add(建家名);
			list.Add(所属管理課コード);
			list.Add(所属管理課);
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			社員番号			 = ToString(list[ 0]);
			漢字氏名			 = ToString(list[ 1]);
			カナ氏名			 = ToString(list[ 2]);
			退職日付			 = ToString(list[ 3]);
			退職区分			 = ToString(list[ 4]);
			システム使用アドレス = ToString(list[ 5]);
			レベル０４所属コード = ToString(list[ 6]);
			レベル０４名称		 = ToString(list[ 7]);
			レベル０５所属コード = ToString(list[ 8]);
			レベル０５名称		 = ToString(list[ 9]);
			所属コード			 = ToString(list[10]);
			所属名				 = ToString(list[11]);
			役職コード			 = ToString(list[12]);
			役職				 = ToString(list[13]);
			兼務_兼務番号_1		 = ToString(list[14]);
			兼務_所属コード_1	 = ToString(list[15]);
			兼務_所属名_1		 = ToString(list[16]);
			兼務_役職コード_1	 = ToString(list[17]);
			兼務_役職_1			 = ToString(list[18]);
			兼務_兼務番号_2		 = ToString(list[19]);
			兼務_所属コード_2	 = ToString(list[20]);
			兼務_所属名_2		 = ToString(list[21]);
			兼務_役職コード_2	 = ToString(list[22]);
			兼務_役職_2			 = ToString(list[23]);
			兼務_兼務番号_3		 = ToString(list[24]);
			兼務_所属コード_3	 = ToString(list[25]);
			兼務_所属名_3		 = ToString(list[26]);
			兼務_役職コード_3	 = ToString(list[27]);
			兼務_役職_3			 = ToString(list[28]);
			兼務_兼務番号_4		 = ToString(list[29]);
			兼務_所属コード_4	 = ToString(list[30]);
			兼務_所属名_4		 = ToString(list[31]);
			兼務_役職コード_4	 = ToString(list[32]);
			兼務_役職_4			 = ToString(list[33]);
			兼務_兼務番号_5		 = ToString(list[34]);
			兼務_所属コード_5	 = ToString(list[35]);
			兼務_所属名_5		 = ToString(list[36]);
			兼務_役職コード_5	 = ToString(list[37]);
			兼務_役職_5			 = ToString(list[38]);
			兼務_兼務番号_6		 = ToString(list[39]);
			兼務_所属コード_6	 = ToString(list[40]);
			兼務_所属名_6		 = ToString(list[41]);
			兼務_役職コード_6	 = ToString(list[42]);
			兼務_役職_6			 = ToString(list[43]);
			兼務_兼務番号_7		 = ToString(list[44]);
			兼務_所属コード_7	 = ToString(list[45]);
			兼務_所属名_7		 = ToString(list[46]);
			兼務_役職コード_7	 = ToString(list[47]);
			兼務_役職_7			 = ToString(list[48]);
			兼務_兼務番号_8		 = ToString(list[49]);
			兼務_所属コード_8	 = ToString(list[50]);
			兼務_所属名_8		 = ToString(list[51]);
			兼務_役職コード_8	 = ToString(list[52]);
			兼務_役職_8			 = ToString(list[53]);
			兼務_兼務番号_9		 = ToString(list[54]);
			兼務_所属コード_9	 = ToString(list[55]);
			兼務_所属名_9		 = ToString(list[56]);
			兼務_役職コード_9	 = ToString(list[57]);
			兼務_役職_9			 = ToString(list[58]);
			建家コード			 = ToString(list[59]);
			建家名				 = ToString(list[60]);
			所属管理課コード     = ToString(list[61]);
			所属管理課           = ToString(list[62]);
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(退職日付);
			list.Add(退職区分);
			list.Add(システム使用アドレス);
			list.Add(レベル０４所属コード);
			list.Add(レベル０４名称);
			list.Add(レベル０５所属コード);
			list.Add(レベル０５名称);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(役職コード);
			list.Add(役職);
			list.Add(兼務_兼務番号_1);
			list.Add(兼務_所属コード_1);
			list.Add(兼務_所属名_1);
			list.Add(兼務_役職コード_1);
			list.Add(兼務_役職_1);
			list.Add(兼務_兼務番号_2);
			list.Add(兼務_所属コード_2);
			list.Add(兼務_所属名_2);
			list.Add(兼務_役職コード_2);
			list.Add(兼務_役職_2);
			list.Add(兼務_兼務番号_3);
			list.Add(兼務_所属コード_3);
			list.Add(兼務_所属名_3);
			list.Add(兼務_役職コード_3);
			list.Add(兼務_役職_3);
			list.Add(兼務_兼務番号_4);
			list.Add(兼務_所属コード_4);
			list.Add(兼務_所属名_4);
			list.Add(兼務_役職コード_4);
			list.Add(兼務_役職_4);
			list.Add(兼務_兼務番号_5);
			list.Add(兼務_所属コード_5);
			list.Add(兼務_所属名_5);
			list.Add(兼務_役職コード_5);
			list.Add(兼務_役職_5);
			list.Add(兼務_兼務番号_6);
			list.Add(兼務_所属コード_6);
			list.Add(兼務_所属名_6);
			list.Add(兼務_役職コード_6);
			list.Add(兼務_役職_6);
			list.Add(兼務_兼務番号_7);
			list.Add(兼務_所属コード_7);
			list.Add(兼務_所属名_7);
			list.Add(兼務_役職コード_7);
			list.Add(兼務_役職_7);
			list.Add(兼務_兼務番号_8);
			list.Add(兼務_所属コード_8);
			list.Add(兼務_所属名_8);
			list.Add(兼務_役職コード_8);
			list.Add(兼務_役職_8);
			list.Add(兼務_兼務番号_9);
			list.Add(兼務_所属コード_9);
			list.Add(兼務_所属名_9);
			list.Add(兼務_役職コード_9);
			list.Add(兼務_役職_9);
			list.Add(建家コード);
			list.Add(建家名);
			list.Add(所属管理課コード);
			list.Add(所属管理課);
		}
	}
}
