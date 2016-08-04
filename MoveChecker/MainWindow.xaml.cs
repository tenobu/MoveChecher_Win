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

using TeraLibrary;

namespace MoveChecker
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private string str_From_Folder = "", str_To_BaseFolder = "";

		private Work_Data wk_Data = null;


		private List<FileInfo> filesList = new List<FileInfo>();

		private Dictionary<string, Work_Data> dic_Name = null;


		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// 設定から読み取る。
			this.Left   = Properties.Settings.Default.WindowLeft;
			this.Top    = Properties.Settings.Default.WindowTop;
			this.Width  = Properties.Settings.Default.WindowWidth;
			this.Height = Properties.Settings.Default.WindowHeight;

			this.label_From_Folder.Content    = Properties.Settings.Default.FromFolder;
			this.label_To_Base_Folder.Content = Properties.Settings.Default.ToBaseFolder;
			this.label_To_Folder.Content      = Properties.Settings.Default.ToFolder;

			// FromFolderが何も無かったら、
			if (label_From_Folder.Content.Equals(""))
			{
				// FromFolderに初期値を送る
				label_From_Folder.Content = "無し";
			}

			// ToBaseFolderが何も無かったら、
			if (label_To_Base_Folder.Content.Equals(""))
			{
				// ToBaseFolderに初期値を送る
				label_To_Base_Folder.Content = "無し";
			}

			str_From_Folder   = (string)label_From_Folder.Content;
			str_To_BaseFolder = (string)label_To_Base_Folder.Content;

			wk_Data = new Work_Data(str_From_Folder, str_To_BaseFolder);

			SetNowHantei();

			//SetFromToHantei();
			SetHantei();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.WindowState == WindowState.Normal)
			{
				// 設定に書き込む。
				Properties.Settings.Default.WindowLeft   = this.Left;
				Properties.Settings.Default.WindowTop    = this.Top;
				Properties.Settings.Default.WindowWidth  = this.Width;
				Properties.Settings.Default.WindowHeight = this.Height;
			}

			// 設定にフォルダ名を書き込む。
			Properties.Settings.Default.FromFolder   = str_From_Folder;
			Properties.Settings.Default.ToBaseFolder = str_To_BaseFolder;
			Properties.Settings.Default.ToFolder     = (string)label_To_Folder.Content;

			// 設定に送ったものをセーブ。
			Properties.Settings.Default.Save();
		}

		private void button_From_Folder_Click(object sender, RoutedEventArgs e)
		{
			{
				SetNowHantei();

				// フォルダブラウザーダイアログ
				win.FolderBrowserDialog fbd = new win.FolderBrowserDialog();

				fbd.Description = "検索するFromフォルダを指定してください。";

				fbd.RootFolder = Environment.SpecialFolder.Desktop;

				fbd.SelectedPath = (string)label_From_Folder.Content;

				fbd.ShowNewFolderButton = true;

				win.DialogResult result = fbd.ShowDialog();

				if (result == win.DialogResult.OK)
				{
					label_From_Folder.Content = fbd.SelectedPath;
					str_From_Folder = (string)label_From_Folder.Content;

					//SetFromHantei();
					SetHantei();
				}
			}
		}

		private void button_To_BaseFolder_Click(object sender, RoutedEventArgs e)
		{
			{
				SetNowHantei();

				// フォルダブラウザーダイアログ
				win.FolderBrowserDialog fbd = new win.FolderBrowserDialog();

				fbd.Description = "検索するToフォルダを指定してください。";

				fbd.RootFolder = Environment.SpecialFolder.Desktop;

				fbd.SelectedPath = (string)label_To_Folder.Content;

				fbd.ShowNewFolderButton = true;

				win.DialogResult result = fbd.ShowDialog();

				if (result == win.DialogResult.OK)
				{
					label_To_Base_Folder.Content = fbd.SelectedPath;
					str_To_BaseFolder = (string)label_To_Base_Folder.Content;

					//SetToHantei();
					SetHantei();
				}
			}
		}

		private void image_From_Over(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				if (IsDirectory(files[0]))
				{
					e.Handled = true;
				}
				else
				{
					e.Handled = false;
				}
			}
			else
			{
				e.Handled = false;
			}
		}

		private void image_From_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				SetNowHantei();

				// Note that you can have more than one file.
				var files = (string[])e.Data.GetData(DataFormats.FileDrop);

				if (IsDirectory(files[0]) == false) return;

				label_From_Folder.Content = files[0];
				str_From_Folder = (string)label_From_Folder.Content;

				//SetFromHantei();
				SetHantei();
			}
		}

		private void image_To_Base_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				SetNowHantei();

				// Note that you can have more than one file.
				var files = (string[])e.Data.GetData(DataFormats.FileDrop);

				if (IsDirectory(files[0]) == false) return;

				label_To_Base_Folder.Content = files[0];

				if (str_From_Folder.Equals(""))
				{
					return;
				}

				str_To_BaseFolder = (string)label_To_Base_Folder.Content;

				var str = new DirectoryInfo(str_From_Folder).Name;

				label_To_Folder.Content = str_To_BaseFolder + @"\" + str;

				//SetToHantei();
				SetHantei();
			}
		}

		private void image_To_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				SetNowHantei();

				// Note that you can have more than one file.
				var files = (string[])e.Data.GetData(DataFormats.FileDrop);

				if (IsDirectory(files[0]) == false) return;

				label_To_Folder.Content = files[0];

				//SetToHantei();
				SetHantei();
			}
		}

		private void button_Start_Click(object sender, RoutedEventArgs e)
		{
			var task = Task.Factory.StartNew(() =>
			{
				try
				{
					this.button_Start.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							button_Start.IsEnabled = false;

						}));
					wk_Data.Copy();

				loop:

					if (wk_Data.bool_WaitFlag)
					{
						System.Threading.Thread.Sleep(100);
						goto loop;
					}

					this.label_Hantei.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							if (wk_Data.str_Status.Equals("Abend"))
							{
								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								label_Hantei.Content = wk_Data.str_Error;
							}
							else if (
								wk_Data.str_Status.Equals("Normal"))
							{
								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Visible;
								image_NotEquals.Visibility = Visibility.Hidden;

								label_Hantei.Content = "正常に終了。";
							}
						}));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
					button_Start.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							button_Start.IsEnabled = true;
						}));
				}
			});
		}

		/*private void SetFromToHantei()
		{
			SetNowHantei();

			var size = 0L;

			if (str_From_Folder.Equals("無し") == false)
			{
				if (IsDirectory(str_From_Folder))
				{
					size = SetFilesSize(new DirectoryInfo(str_From_Folder));
				}
			}

			label_From_FilesSize.Content = size.ToString("#,#0 Byte");

			
			size = 0L;

			if (str_To_BaseFolder.Equals("無し") == false)
			{
				if (IsDirectory(str_To_BaseFolder))
				{
					size = SetFilesSize(new DirectoryInfo(str_To_BaseFolder));
				}
			}

			label_To_FilesSize.Content = size.ToString("#,#0 Byte");

			
			SetHantei();
		}*/

		/*private void SetFromHantei()
		{
			SetNowHantei();

			var size = 0L;

			if (str_From_Folder.Equals("無し") == false)
			{
				if (IsDirectory(str_From_Folder))
				{
					size = SetFilesSize(new DirectoryInfo(str_From_Folder));
				}
			}

			label_From_FilesSize.Content = size.ToString("#,#0 Byte");

			SetHantei();
		}*/

		/*private void SetToHantei()
		{
			SetNowHantei();

			var size = 0L;

			if (str_To_BaseFolder.Equals("無し") == false)
			{
				if (IsDirectory(str_To_BaseFolder))
				{
					//size = SetFilesSize(new DirectoryInfo(str_To_BaseFolder));

					size = wk_Data.B_Size();
				}
			}

			label_To_FilesSize.Content =  size.ToString("#,#0 Byte");

			SetHantei();
		}*/

		private void SetNowHantei()
		{
			var task = Task.Factory.StartNew(() =>
			{
				this.label_Hantei.Dispatcher.BeginInvoke(
					new Action(() =>
					{
						if (wk_Data.bool_WaitFlag == true)
						{
							label_From_Folder.Content = "？ Byte";
							label_To_Folder.Content   = "？ Byte";

							image_Hatena.Visibility    = Visibility.Visible;
							image_Equals.Visibility    = Visibility.Hidden;
							image_NotEquals.Visibility = Visibility.Hidden;

							label_Hantei.Content = "フォルダ？をチェック中！！";
						}
					}));
			});
		}

		private void SetHantei()
		{
			var task = Task.Factory.StartNew(() =>
			{
				wk_Data.Check();

				loop:

				if (wk_Data.bool_WaitFlag)
				{
					System.Threading.Thread.Sleep(100);
					goto loop;
				}


				this.label_Hantei.Dispatcher.BeginInvoke(
					new Action(() =>
					{
						label_From_Folder.Content = wk_Data.A_Size().ToString("0 Byte");
						label_To_Folder.Content   = wk_Data.B_Size().ToString("0 Byte");

						if (wk_Data.str_Status.Equals("Abend"))
						{
							image_Hatena.Visibility    = Visibility.Hidden;
							image_Equals.Visibility    = Visibility.Hidden;
							image_NotEquals.Visibility = Visibility.Visible;

							label_Hantei.Content = wk_Data.str_Error;

							button_Start.IsEnabled = false;
						}
						else if (
							wk_Data.str_Status.Equals("Normal"))
						{
							image_Hatena.Visibility    = Visibility.Hidden;
							image_Equals.Visibility    = Visibility.Hidden;
							image_NotEquals.Visibility = Visibility.Visible;

							label_Hantei.Content = "Start の準備が出来ました。";

							button_Start.IsEnabled = true;
						}
					}));

						/*if (str_From_Folder.Equals("無し") == false && str_To_BaseFolder.Equals("無し") == false)
						{
							if (CheckDirFiles())
							{
								image_Hatena.Visibility = Visibility.Hidden;
								image_Equals.Visibility = Visibility.Visible;
								image_NotEquals.Visibility = Visibility.Hidden;

								if (IsDirectory(str_From_Folder))
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
								image_Hatena.Visibility = Visibility.Hidden;
								image_Equals.Visibility = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								if (IsDirectory((string)str_From_Folder))
								{
									label_Hantei.Content = "違う内容のフォルダ！！";
								}
								else
								{
									//label_Hantei.Content = "違う内容のファイル！！";
									label_Hantei.Content = "To の設定が無い！！";
								}
							}
						}
						else if (
							str_From_Folder.Equals("無し") && str_To_BaseFolder.Equals("無し") == false)
						{
							{
								image_Hatena.Visibility = Visibility.Hidden;
								image_Equals.Visibility = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								if (IsDirectory(str_To_BaseFolder))
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
							str_From_Folder.Equals("無し") == false && str_To_BaseFolder.Equals("無し"))
						{
							{
								image_Hatena.Visibility = Visibility.Hidden;
								image_Equals.Visibility = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								if (IsDirectory((string)str_From_Folder))
								{
									label_Hantei.Content = "違う内容のフォルダ！！";
								}
								else
								{
									//label_Hantei.Content = "違う内容のファイル！！";
									label_Hantei.Content = "To の設定が無い！！";
								}
							}
						}
						else if (
							str_From_Folder.Equals("無し") && str_To_BaseFolder.Equals("無し"))
						{
							{
								image_Hatena.Visibility = Visibility.Hidden;
								image_Equals.Visibility = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								label_Hantei.Content = "設定が無い！！";
							}
						}
					}));*/
			});
		}

		/*private bool CheckDirFiles()
		{
			dic_Name = new Dictionary<string, Work_Data>();

			if (IsDirectory(str_From_Folder) && IsDirectory(str_To_BaseFolder))
			{
				var from_dir = new DirectoryInfo(str_From_Folder);
				var to_dir = new DirectoryInfo(str_To_BaseFolder);

				var wk = new Work_Data(from_dir, to_dir);

				dic_Name.Add(wk.Name, wk);

				return true;
			}

			return false;
		}*/

		private bool IsDirectory(string dir_path)
		{
			if (Directory.Exists(dir_path))
			{
				return true;
			}

			/*if ((File.GetAttributes(dir_path) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				return true;
			}*/

			return false;
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
			catch(System.UnauthorizedAccessException e)
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
