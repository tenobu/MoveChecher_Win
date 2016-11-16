using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using TeraLibrary.ExcelIO;

namespace TeraLibrary.徳島_勤怠チェック.Dic
{
	// Dic_社員名簿
	// 
	public class Dic_社員名簿 : Dic_ListList // Dic_PlusPlus
	{
		public string
			社員番号 = "", 漢字氏名, カナ氏名, 日付, 退職日付, 退職区分, 所属コード, 所属名, 建家コード, 建家名称,
			職掌名称, 職責ランク名称, 職責ランク略称, 役職コード, 役職, 業務の種類,
			レベル０４所属コード, レベル０４名称, レベル０５所属コード, レベル０５名称, メールID,
			所属管理課コード, 所属管理課;


		public string 所属建家コード;


		public Dic_社員名簿()
		{
			alphabet = "W";
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
			list.Add(日付);
			list.Add(退職日付);
			list.Add(退職区分);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(建家コード);
			list.Add(建家名称);
			list.Add(職掌名称);
			list.Add(職責ランク名称);
			list.Add(職責ランク略称);
			list.Add(役職コード);
			list.Add(役職);
			list.Add(業務の種類);
			list.Add(レベル０４所属コード);
			list.Add(レベル０４名称);
			list.Add(レベル０５所属コード);
			list.Add(レベル０５名称);
			list.Add(メールID);
			list.Add(所属管理課コード);
			list.Add(所属管理課);
			list.Add(所属建家コード);
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveRead2(List<string> list)
		{
			社員番号				 = ToString(list[ 0]);
			漢字氏名				 = ToString(list[ 1]);
			カナ氏名				 = ToString(list[ 2]);
			日付					 = ToString(list[ 3]);
			退職日付				 = ToString(list[ 4]);
			退職区分				 = ToString(list[ 5]);
			所属コード				 = ToString(list[ 6]);
			所属名					 = ToString(list[ 7]);
			建家コード				 = ToString(list[ 8]);
			建家名称				 = ToString(list[ 9]);
			職掌名称				 = ToString(list[10]);
			職責ランク名称			 = ToString(list[11]);
			職責ランク略称			 = ToString(list[12]);
			役職コード				 = ToString(list[13]);
			役職					 = ToString(list[14]);
			業務の種類				 = ToString(list[15]);
			レベル０４所属コード	 = ToString(list[16]);
			レベル０４名称			 = ToString(list[17]);
			レベル０５所属コード	 = ToString(list[18]);
			レベル０５名称			 = ToString(list[19]);
			メールID				 = ToString(list[20]);
			所属管理課コード         = ToString(list[21]);
			所属管理課               = ToString(list[22]);

			所属建家コード = 所属コード + "-" + 建家コード;
		}

		// ColumnMoveRead2
		// 
		public override void ColumnMoveWrite2(List<string> list)
		{
			list.Add(社員番号);
			list.Add(漢字氏名);
			list.Add(カナ氏名);
			list.Add(日付);
			list.Add(退職日付);
			list.Add(退職区分);
			list.Add(所属コード);
			list.Add(所属名);
			list.Add(建家コード);
			list.Add(建家名称);
			list.Add(職掌名称);
			list.Add(職責ランク名称);
			list.Add(職責ランク略称);
			list.Add(役職コード);
			list.Add(役職);
			list.Add(業務の種類);
			list.Add(レベル０４所属コード);
			list.Add(レベル０４名称);
			list.Add(レベル０５所属コード);
			list.Add(レベル０５名称);
			list.Add(メールID);
			list.Add(所属管理課コード);
			list.Add(所属管理課);
			list.Add(所属建家コード);
		}
	}
}
