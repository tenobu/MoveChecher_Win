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

		private FromTo_Controller ft_Cntl = null;


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

			//2016/08/17 17:00:00
			SetTo();

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

					//2016/08/17 17:00:00
					SetTo();

					SetNowHantei();
					SetHantei();
				}
			}
		}

		private void button_To_BaseFolder_Click(object sender, RoutedEventArgs e)
		{
			{
				// フォルダブラウザーダイアログ
				win.FolderBrowserDialog fbd = new win.FolderBrowserDialog();

				fbd.Description = "検索するToフォルダを指定してください。";

				fbd.RootFolder = Environment.SpecialFolder.Desktop;

				fbd.SelectedPath = (string)label_To_Folder.Content;

				fbd.ShowNewFolderButton = true;

				win.DialogResult result = fbd.ShowDialog();

				if (result == win.DialogResult.OK)
				{
					if (fbd.SelectedPath.EndsWith(@"\"))
					{
						var text = fbd.SelectedPath;

						text = text.Remove(text.Count() - 1);

						fbd.SelectedPath = text;
					}

					label_To_Base_Folder.Content = fbd.SelectedPath;

					str_To_BaseFolder = (string)label_To_Base_Folder.Content;

					//2016/08/17 17:00:00
					SetTo();

					SetNowHantei();
					SetHantei();
				}
			}
		}

		private void image_From_Over(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				if (Directory.Exists(files[0]))
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

				if (Directory.Exists(files[0]) == false) return;

				label_From_Folder.Content = files[0];
				str_From_Folder = (string)label_From_Folder.Content;

				if (str_To_BaseFolder.Equals(""))
				{
					return;
				}

				str_To_BaseFolder = (string)label_To_Base_Folder.Content;

				//2016/08/17 17:00:00
				/*var str = new DirectoryInfo(str_From_Folder).Name;

				label_To_Folder.Content = str_To_BaseFolder + @"\" + str;*/

				SetTo();

				SetNowHantei();

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

				if (Directory.Exists(files[0]) == false) return;

				label_To_Base_Folder.Content = files[0];

				if (str_From_Folder.Equals(""))
				{
					return;
				}

				str_To_BaseFolder = (string)label_To_Base_Folder.Content;

				//2016/08/17 17:00:00
				/*var str = new DirectoryInfo(str_From_Folder).Name;

				label_To_Folder.Content = (str_To_BaseFolder + @"\" + str).Replace(@"\\", @"\");*/

				SetTo();

				SetNowHantei();

				SetHantei();
			}
		}

		//2016/08/17 17:00:00
		private void SetTo()
		{
			var from = new DirectoryInfo(str_From_Folder);

			var text = str_To_BaseFolder + @"\" + from.Name;

			label_To_Folder.Content = text;
		}

		private void SetNowHantei()
		{
			ft_Cntl = new FromTo_Controller(str_From_Folder, str_To_BaseFolder);

			var task = Task.Factory.StartNew(() =>
			{
				this.label_Hantei.Dispatcher.BeginInvoke(
					new Action(() =>
					{

						if (ft_Cntl.bool_WaitFlag == true)
						{
							if (ft_Cntl.long_F_SumiSize == 0L && ft_Cntl.long_T_SumiSize == 0L)
							{
								label_From_FilesSize.Content = "？ Byte";
								label_To_FilesSize  .Content = "？ Byte";
								textBlock_File      .Text    = "?";
							}
							else
							{
								label_From_FilesSize.Content = ft_Cntl.long_F_SumiSize.ToString("#,#0 Byte");
								label_To_FilesSize  .Content = ft_Cntl.long_T_SumiSize.ToString("#,#0 Byte");
								textBlock_File      .Text    = ft_Cntl.str_T_FileName;
							}

							image_Hatena.Visibility    = Visibility.Visible;
							image_Equals.Visibility    = Visibility.Hidden;
							image_NotEquals.Visibility = Visibility.Hidden;

							label_Hantei.Content = "フォルダ？をチェック中！！";

							button_Check .IsEnabled = false;
							button_Copy  .IsEnabled = false;
							button_Delete.IsEnabled = false;
							button_Cancel.IsEnabled = true;

							if (ft_Cntl.long_F_SumiSize == 0L && ft_Cntl.long_T_SumiSize == 0L)
							{
								progressBar_All.Value   = 0f;
								progressBar_All.Maximum = 100f;
								textBlock_件数.Text     = "？ Byte";
								textBlock_Parcent.Text  = "0%";
							}
							else
							{
								var size = (float)ft_Cntl.long_T_SumiSize / (float)ft_Cntl.long_F_SumiSize * 100.0f;

								progressBar_All.Value   = ft_Cntl.long_T_SumiSize;
								progressBar_All.Maximum = ft_Cntl.long_F_SumiSize;
								textBlock_件数.Text     = ft_Cntl.long_T_SumiSize.ToString("#,#0 Byte");
								textBlock_Parcent.Text  = ((int)size) + "%";
							}
						}

					}));
			});
		}

		private void button_Check_Click(object sender, RoutedEventArgs e)
		{
			SetNowHantei();

			SetHantei();
		}

		private void SetHantei()
		{
			var task = Task.Factory.StartNew(() =>
			{
				ft_Cntl.Check();

				do
				{

					this.label_Hantei.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							SetProgress();
						}));

					System.Threading.Thread.Sleep(100);

				} while (ft_Cntl.bool_WaitFlag);


				this.label_Hantei.Dispatcher.BeginInvoke(
					new Action(() =>
					{
						SetProgress();

						if (ft_Cntl.str_Status.Equals("Abend"))
						{
							image_Hatena.Visibility    = Visibility.Hidden;
							image_Equals.Visibility    = Visibility.Hidden;
							image_NotEquals.Visibility = Visibility.Visible;

							label_Hantei.Content = ft_Cntl.str_Error;

							button_Check .IsEnabled = true;
							button_Copy  .IsEnabled = false;
							button_Delete.IsEnabled = false;
							button_Cancel.IsEnabled = false;
						}
						else if (
							ft_Cntl.str_Status.Equals("Normal"))
						{
							if (ft_Cntl.bool_EndFlag)
							{
								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Visible;
								image_NotEquals.Visibility = Visibility.Hidden;

								label_Hantei.Content = "フォルダは ＝(イコール) です。";

								button_Check .IsEnabled = true;
								button_Copy  .IsEnabled = false;
								button_Delete.IsEnabled = true;
								button_Cancel.IsEnabled = false;
							}
							else
							{
								var f_size = ft_Cntl.long_F_SumiSize;
								var t_size = ft_Cntl.long_T_SumiSize;

								var str = "";

								if (t_size == 0L)
								{
									str = "Start の準備が出来ました。";
								}
								else
								{
									var s = 100 - (int)((float)t_size / (float)f_size * 100.0);

									str = "Start の準備が出来ました (" + s + "% が残っています)。";
								}

								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								label_Hantei.Content = str;

								button_Check .IsEnabled = true;
								button_Copy  .IsEnabled = true;
								button_Delete.IsEnabled = false;
								button_Cancel.IsEnabled = false;
							}
						}
					}));
			});
		}

		private void button_Copy_Click(object sender, RoutedEventArgs e)
		{
			var task = Task.Factory.StartNew(() =>
			{
				try
				{
					this.button_Copy.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							button_Check .IsEnabled = false;
							button_Copy  .IsEnabled = false;
							button_Delete.IsEnabled = false;
							button_Cancel.IsEnabled = true;
						}));

					ft_Cntl.Copy();

					do
					{
		
						this.button_Copy.Dispatcher.BeginInvoke(
							new Action(() =>
							{
								SetProgress();
							}));

						System.Threading.Thread.Sleep(100);

					} while (ft_Cntl.bool_WaitFlag);


					this.label_Hantei.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							SetProgress();

							if (ft_Cntl.str_Status.Equals("Abend"))
							{
								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								label_Hantei.Content = ft_Cntl.str_Error;

								button_Check .IsEnabled = true;
								button_Copy  .IsEnabled = true;
								button_Delete.IsEnabled = false;
								button_Cancel.IsEnabled = false;
							}
							else if (
								ft_Cntl.str_Status.Equals("Normal"))
							{
								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Visible;
								image_NotEquals.Visibility = Visibility.Hidden;

								label_Hantei.Content = "正常に終了。";

								button_Check.IsEnabled  = true;
								button_Copy.IsEnabled   = false;
								button_Delete.IsEnabled = true;
								button_Cancel.IsEnabled = false;
							}
						}));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
				}
			});
		}

		private void button_Delete_Click(object sender, RoutedEventArgs e)
		{
			var task = Task.Factory.StartNew(() =>
			{
				try
				{
					this.button_Delete.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							button_Check .IsEnabled = false;
							button_Copy  .IsEnabled = false;
							button_Delete.IsEnabled = false;
							button_Cancel.IsEnabled = true;
						}));
					ft_Cntl.Delete();

				loop:

					if (ft_Cntl.bool_WaitFlag)
					{
						System.Threading.Thread.Sleep(100);
						goto loop;
					}

					this.label_Hantei.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							label_From_FilesSize.Content = ft_Cntl.long_F_SumiSize.ToString("#,#0 Byte");
							label_To_FilesSize.Content   = ft_Cntl.long_T_SumiSize.ToString("#,#0 Byte");

							if (ft_Cntl.str_Status.Equals("Abend"))
							{
								image_Hatena.Visibility    = Visibility.Hidden;
								image_Equals.Visibility    = Visibility.Hidden;
								image_NotEquals.Visibility = Visibility.Visible;

								label_Hantei.Content = ft_Cntl.str_Error;
							}
							else if (
								ft_Cntl.str_Status.Equals("Normal"))
							{
								if (ft_Cntl.bool_EndFlag)
								{
									image_Hatena.Visibility    = Visibility.Hidden;
									image_Equals.Visibility    = Visibility.Hidden;
									image_NotEquals.Visibility = Visibility.Visible;

									label_Hantei.Content = "正常に終了。";

									button_Copy.IsEnabled = false;
								}
								else
								{
									var a_size = ft_Cntl.long_F_SumiSize;
									var b_size = ft_Cntl.long_T_SumiSize;
									var str = "";

									if (a_size == b_size)
									{
										image_Hatena.Visibility    = Visibility.Hidden;
										image_Equals.Visibility    = Visibility.Visible;
										image_NotEquals.Visibility = Visibility.Hidden;

										str = "Delete の準備が出来ました。";
									}
									else
									{
										image_Hatena.Visibility    = Visibility.Hidden;
										image_Equals.Visibility    = Visibility.Hidden;
										image_NotEquals.Visibility = Visibility.Visible;

										var s = 100 - (int)((float)b_size / (float)a_size * 100.0);

										str = "Delete の準備が出来ました (" + s + "% が残っています)。";
									}


									label_Hantei.Content = str;

									button_Delete.IsEnabled = true;
								}
							}
						}));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
					button_Delete.Dispatcher.BeginInvoke(
						new Action(() =>
						{
							button_Delete.IsEnabled = true;
						}));
				}
			});
		}

		private void button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			if (ft_Cntl != null)
			{
				ft_Cntl.Cancel();
			}
		}

		private void SetProgress()
		{
			label_Hantei        .Content = ft_Cntl.str_Message;

			label_From_FilesSize.Content = ft_Cntl.long_F_SumiSize.ToString("#,#0 Byte");
			label_To_FilesSize  .Content = ft_Cntl.long_T_SumiSize.ToString("#,#0 Byte");

			textBlock_File      .Text    = ft_Cntl.str_T_FileName;

			var f_size = ft_Cntl.long_F_SumiSize;
			var t_size = ft_Cntl.long_T_SumiSize;

			var size = 0f;
			if (f_size == 0L && t_size == 0L)
			{
				progressBar_All.Value = 0f;
				progressBar_All.Maximum = 100f;
			}
			else
			{
				size = (float)t_size / (float)f_size * 100.0f;

				progressBar_All.Maximum = (double)f_size;
				progressBar_All.Value   = (double)t_size;
			}

			textBlock_件数   .Text = ft_Cntl.long_T_SumiSize.ToString("#,#0 Byte");
			textBlock_Parcent.Text = ((int)size) + "%";
		}
	}
}
