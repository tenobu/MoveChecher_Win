using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using Microsoft.Win32;
using win = System.Windows.Forms;

namespace MoveChecker
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<FileInfo> filesList = new List<FileInfo>();

		private bool bool_Hantei = false;


		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// 設定から読み取る。
			this.Left = Properties.Settings.Default.WindowLeft;
			this.Top = Properties.Settings.Default.WindowTop;
			this.Width = Properties.Settings.Default.WindowWidth;
			this.Height = Properties.Settings.Default.WindowHeight;

			this.label_From_Folder.Content = Properties.Settings.Default.FromFolder;
			this.label_To_Folder.Content = Properties.Settings.Default.ToFolder;

			// FromFolderが何も無かったら、
			if (label_From_Folder.Content.Equals(""))
			{
				// FromFolderに初期値を送る
				label_From_Folder.Content = "無し";
			}

			// ToFolderが何も無かったら、
			if (label_To_Folder.Content.Equals(""))
			{
				// ToFolderに初期値を送る
				label_To_Folder.Content = "無し";
			}

			if (label_From_Folder.Content.Equals("無し") == false && label_To_Folder.Content.Equals("無し") == false)
			{
				SetFromToHantei();
			}
			else if (
				label_From_Folder.Content.Equals("無し") == false && label_To_Folder.Content.Equals("無し"))
			{
				SetFromHantei();
			}
			else if (
				label_From_Folder.Content.Equals("無し") && label_To_Folder.Content.Equals("無し") == false)
			{
				SetToHantei();
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.WindowState == WindowState.Normal)
			{
				// 設定に書き込む。
				Properties.Settings.Default.WindowLeft = this.Left;
				Properties.Settings.Default.WindowTop = this.Top;
				Properties.Settings.Default.WindowWidth = this.Width;
				Properties.Settings.Default.WindowHeight = this.Height;
			}

			// 設定にフォルダ名を書き込む。
			Properties.Settings.Default.FromFolder = (string)this.label_From_Folder.Content;
			Properties.Settings.Default.ToFolder = (string)this.label_To_Folder.Content;

			// 設定に送ったものをセーブ。
			Properties.Settings.Default.Save();
		}

		private void button_From_Folder_Click(object sender, RoutedEventArgs e)
		{
			{
				// フォルダブラウザーダイアログ
				win.FolderBrowserDialog fbd = new win.FolderBrowserDialog();

				fbd.Description = "検索するフォルダを指定してください。";

				fbd.RootFolder = Environment.SpecialFolder.Desktop;

				fbd.SelectedPath = (string)label_From_Folder.Content;

				fbd.ShowNewFolderButton = true;

				win.DialogResult result = fbd.ShowDialog();

				if (result == win.DialogResult.OK)
				{
					label_From_Folder.Content = fbd.SelectedPath;
				}
			}
		}

		private void image_From_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// Note that you can have more than one file.
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				label_From_Folder.Content = files[0];

				SetFromHantei();
			}
		}

		private void image_To_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// Note that you can have more than one file.
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				label_To_Folder.Content = files[0];

				SetToHantei();
			}
		}

		private void SetFromToHantei()
		{
			var size = 0L;

			if (IsDirectory((string)label_From_Folder.Content))
			{
				size = SetFilesSize(new DirectoryInfo((string)label_From_Folder.Content));
			}
			else
			{
				size = SetFileSize(new FileInfo((string)label_From_Folder.Content));
			}

			label_From_FilesSize.Content = size.ToString("#,#0 Byte");

			
			size = 0L;

			if (IsDirectory((string)label_To_Folder.Content))
			{
				size = SetFilesSize(new DirectoryInfo((string)label_To_Folder.Content));
			}
			else
			{
				size = SetFileSize(new FileInfo((string)label_To_Folder.Content));
			}

			label_To_FilesSize.Content = size.ToString("#,#0 Byte");

			
			SetHantei();
		}

		private void SetFromHantei()
		{
			var size = 0L;

			if (IsDirectory((string)label_From_Folder.Content))
			{
				size = SetFilesSize(new DirectoryInfo((string)label_From_Folder.Content));
			}
			else
			{
				size = SetFileSize(new FileInfo((string)label_From_Folder.Content));
			}

			label_From_FilesSize.Content = size.ToString("#,#0 Byte");

			SetHantei();
		}

		private void SetToHantei()
		{
			var size = 0L;

			if (IsDirectory((string)label_To_Folder.Content))
			{
				size = SetFilesSize(new DirectoryInfo((string)label_To_Folder.Content));
			}
			else
			{
				size = SetFileSize(new FileInfo((string)label_To_Folder.Content));
			}

			label_To_FilesSize.Content = size.ToString("#,#0 Byte");

			SetHantei();
		}

		private void SetHantei()
		{
			if (label_From_Folder.Content.Equals("無し") == false && label_To_Folder.Content.Equals("無し") == false)
			{
				if (CheckDirFiles())
				{
					image_Equals.Visibility = Visibility.Visible;
					image_NotEquals.Visibility = Visibility.Hidden;

					if (IsDirectory((string)label_From_Folder.Content))
					{
						label_Hantei.Content = "同じ内容のフォルダ！！";
					}
					else
					{
						label_Hantei.Content = "同じ内容のファイル！！";
					}
				}
				else
				{
					image_Equals.Visibility = Visibility.Hidden;
					image_NotEquals.Visibility = Visibility.Visible;

					if (IsDirectory((string)label_From_Folder.Content))
					{
						label_Hantei.Content = "違う内容のフォルダ！！";
					}
					else
					{
						label_Hantei.Content = "違う内容のファイル！！";
					}
				}
			}
			else if (
				label_From_Folder.Content.Equals("無し") && label_To_Folder.Content.Equals("無し") == false)
			{
				{
					image_Equals.Visibility = Visibility.Hidden;
					image_NotEquals.Visibility = Visibility.Visible;

					if (IsDirectory((string)label_To_Folder.Content))
					{
						label_Hantei.Content = "違う内容のフォルダ！！";
					}
					else
					{
						label_Hantei.Content = "違う内容のファイル！！";
					}
				}
			}
			else if (
				label_From_Folder.Content.Equals("無し") == false && label_To_Folder.Content.Equals("無し"))
			{
				{
					image_Equals.Visibility = Visibility.Hidden;
					image_NotEquals.Visibility = Visibility.Visible;

					if (IsDirectory((string)label_From_Folder.Content))
					{
						label_Hantei.Content = "違う内容のフォルダ！！";
					}
					else
					{
						label_Hantei.Content = "違う内容のファイル！！";
					}
				}
			}
			else if (
				label_From_Folder.Content.Equals("無し") && label_To_Folder.Content.Equals("無し"))
			{
				{
					image_Equals.Visibility = Visibility.Hidden;
					image_NotEquals.Visibility = Visibility.Visible;

					label_Hantei.Content = "設定が無い！！";
				}
			}
		}

		private bool CheckDirFiles()
		{
			return false;
		}

		private bool IsDirectory(string dir_path)
		{
			if ((File.GetAttributes(dir_path) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				return true;
			}

			return false;
		}

		private long SetFilesSize(DirectoryInfo di)
		{
			var size = 0L;

			foreach (var file_c in di.GetFiles())
			{
				size += file_c.Length;
			}

			foreach (var dic_c in di.GetDirectories())
			{
				size += SetFilesSize(dic_c);
			}

			return size;
		}

		private long SetFileSize(FileInfo fi)
		{
			var size = fi.Length;

			return size;
		}

		private void image_From_Folder_Drop(object sender, DragEventArgs e)
		{

		}

		private void label_To_Folder_Drop(object sender, DragEventArgs e)
		{

		}

		private void button_To_Folder_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
