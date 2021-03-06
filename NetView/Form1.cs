﻿using NetView.Config;
using NetView.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubBusContrainer;
using TreeviewContrainer;
using NetView.Class;
using System.IO;
using System.Windows.Forms.Integration;
using NetView.View;
using DevExpress.XtraBars.Docking;
using ControllerLib;
using ControllerLib.Ethercat;
using DevExpress.XtraBars;
using NetView.Definations;
using ControllerLib.BusConfigModle;
using ControlTest;
using ControllerLib.Ethercat.ModuleConfigModle;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using NetView.Model.ModuleInfo;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using ControllerLib.Model;

namespace NetView
{
	public partial class Form1 : DevExpress.XtraEditors.XtraForm
	{
		DeviceConfigEntry DeviceCfg = null;
		string FileOpenPath = @"C:\";

		ProductContrainer MiddleControl = null;
		treeviewContrainer LeftControl = null;
		DataTable DTVarMonitor = new DataTable();

		ControllerBase ControllerCard = new EC_Controller();
		ProjectController ProjController = new ProjectController();
		

		const string FILE_DEMO_XML_FILE = @"Template\Demo.xml";
		ComportSettingModel ComSettingCfgModel = new ComportSettingModel();


		CancellationTokenSource ctsMonitorController = new CancellationTokenSource();
		CancellationTokenSource ctsHeartBeat = new CancellationTokenSource();
		CancellationTokenSource ctsPickMsg = new CancellationTokenSource();
		ManualResetEvent EventMonitorController = new ManualResetEvent(false);
		ManualResetEvent EventHeartBeat = new ManualResetEvent(false);

		ObservableCollection<MonitorVarModel> m_VarCollect = null;
		//
		List<DataRecieveModel> m_OutputValueRecv_List = new List<DataRecieveModel>();
		List<DataRecieveModel> m_InputValueRecv_List = new List<DataRecieveModel>();
		List<ModuleConfigModleBase> m_ModuleList = new List<ModuleConfigModleBase>();
		List<ModuleInfoBase> m_MonitorList = new List<ModuleInfoBase>();
		ListBox m_DiagramOutputWindow = null;
		Model.DisplayFormat.DisplayFormatBase m_DisplayFormat = new Model.DisplayFormat.DisplayFormatHex(0);
		int m_OldBase = 16;
		bool m_OnFirstCircle = true;
		object m_SetGetLock = new object();
		object m_pickMsgLock = new object();
		UC_VarMonitor m_ucMonitor = null;
		List<UInt32>  m_ModifyValueList = new List<UInt32>();
		uint m_TransmitDelay = 500;
		Queue<ControllerLib.Model.DataFromComport> m_msgQueue = new Queue<ControllerLib.Model.DataFromComport>();
		Dictionary<string, int> ImageDic = new Dictionary<string, int>();

		public Form1()
		{
			InitializeComponent();
			LoadCfg();
			InitCtrl();

		}

		private void BusController_OnConnectStateChanged(object sender, bool e)
		{

			barButtonItemConnect.Enabled = !e;
			barButtonItem_Disconnect.Enabled = e;
			MenuConnect.Enabled = barButtonItemConnect.Enabled;
			MenuDisconnect.Enabled = barButtonItem_Disconnect.Enabled;
			if (!e)
			{
				m_OnFirstCircle = true;
				EventMonitorController.Reset();
				UcMonitor_OnStopMonitorEventHandler(null, null);
				m_ucMonitor.IsMonitor = false;
				lock (m_pickMsgLock)
				{
					m_msgQueue.Clear();
				}
				ShowMessage(EnumMsgType.Error, "Controller Disconnected");
				
			}
			else
			{
				ShowMessage(EnumMsgType.Info, "Controller Connected");
			}

			
		}

		private void LoadCfg()
		{
			try
			{
				Config.ConfigMgr.Instance.LoadConfig();
				DeviceCfg = ConfigMgr.Instance.DeviceCfgEntry;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Close();
			}
		}

		/// <summary>
		/// 利用递归生成TreeNode
		/// </summary>
		/// <param name="DeviceCfg"></param>
		/// <returns></returns>
		private TreeNode GenNode(DeviceConfigEntry DeviceCfg, Dictionary<string,int> ImageDic)
		{
			TreeNode node = new TreeNode(DeviceCfg.Name, ImageDic[DeviceCfg.Icon], ImageDic[DeviceCfg.Icon]);
			foreach (var it in DeviceCfg.Child)
			{
				node.Nodes.Add(GenNode(it, ImageDic));
			}
			return node;
		}

		private void InitCtrl()
		{
			ControllerCard.OnConnectStateChanged += BusController_OnConnectStateChanged;
			ControllerCard.OnDataComeHandler += BusController_OnDataComeHandler;
			barButtonItemConnect.Enabled = true;
			barButtonItem_Disconnect.Enabled = false;
			MenuConnect.Enabled = barButtonItemConnect.Enabled;
			MenuDisconnect.Enabled = barButtonItem_Disconnect.Enabled;


			ImageList imgList = new ImageList();
			string[] ImageList = new string[] { @"images/Category.png" , @"images/Bus.png" , @"images/SlaveMaster.png" , @"images/Device.png" };
			for (int i = 0; i < ImageList.Length; i++)
			{
				imgList.Images.Add(new Bitmap(ImageList[i]));
				ImageDic.Add(ImageList[i].Replace(@"images/",""), i);
			}
			treeViewDevice.ImageList = imgList;

			
			
			treeViewDevice.Nodes.Add(GenNode(ConfigMgr.Instance.DeviceCfgEntry, ImageDic));
			treeViewDevice.ExpandAll();
			treeViewDevice.ItemDrag += TreeViewDevice_ItemDrag;
			treeViewDevice.NodeMouseDoubleClick += TreeViewDevice_NodeMouseDoubleClick;
			this.barSubIteExportFile.Popup += BarSubIteExportFile_Popup;
			//this.treeViewDevice


			//添加中间控件
			MiddleControl = new ProductContrainer();
			this.dockPanelMiddle.Controls.Add(MiddleControl);
			MiddleControl.Dock = DockStyle.Fill;
			


			//添加侧面控件
			LeftControl = new treeviewContrainer();
			LeftControl.OnBusModuleChanged += LeftControl_OnBusModuleChanged;
			LeftControl.OnBusMenuAddClicked += LeftControl_OnBusMenuAddClicked;
			LeftControl.OnBusMenuDeleteClicked += LeftControl_OnBusMenuDeleteClicked;
			this.dockPanelLeft.Controls.Add(LeftControl);
			LeftControl.Dock = DockStyle.Fill;
			LeftControl.ProductContrainer = MiddleControl;
		

			//VarMonitor
			m_ucMonitor = new UC_VarMonitor();
			elementHost2.Child = m_ucMonitor;
			m_ucMonitor.OnStartMonitorEventHandler += UcMonitor_OnStartMonitorEventHandler;
			m_ucMonitor.OnStopMonitorEventHandler += UcMonitor_OnStopMonitorEventHandler;
			m_ucMonitor.OnModifyValueEventHandler += UcMonitor_OnModifyValueEventHandler;
			m_ucMonitor.OnChangeDisplayFormatHandler += UcMonitor_OnChangeDisplayFormatHandler;


			//Add diagram output window
			m_DiagramOutputWindow = new ListBox();
			m_DiagramOutputWindow.Visible = true;
			m_DiagramOutputWindow.HorizontalScrollbar = true;
			m_DiagramOutputWindow.ScrollAlwaysVisible = true;
			m_DiagramOutputWindow.MouseDoubleClick += M_DiagramOutputWindow_MouseDoubleClick;

			//添加菜单一;
			var ctx_copymenu = new MenuItem("copy");
			ctx_copymenu.Click += Ctx_copymenu_Click;
	
			var ctx_menu = new ContextMenu(new MenuItem[]{ ctx_copymenu});
			

			m_DiagramOutputWindow.ContextMenu = ctx_menu;
			m_DiagramOutputWindow.Dock = DockStyle.Fill;
			m_DiagramOutputWindow.Font = new Font("lisu",12);
			this.dockPanelDiagram.SizeChanged += DockPanelDiagram_SizeChanged;
			this.dockPanelDiagram.Controls.Add(m_DiagramOutputWindow);
			this.dockPanelDiagram.Visibility = DockVisibility.Hidden;


			m_VarCollect = m_ucMonitor.VarCollect;

			this.elementHost1.BackColorTransparent = true;
			this.elementHost2.BackColorTransparent = true;
			dockManager1.ActivePanel = dockPanelMiddle;
			dockPanelVarMonitor.VisibilityChanged += DockPanelVarMonitor_VisibilityChanged;

			//dockManager1.RemovePanel(dockPanelVarMonitor);
			ProjController.BusFileMgr = new EthercatFileMgr();


			//Start Task to monitor Controller
			m_OldBase = m_DisplayFormat.Base;
			Task.Run(() =>
			{
				//var ModuleInfoList = new List<ModuleInfoBase>();
				while (!ctsMonitorController.IsCancellationRequested)
				{
					EventMonitorController.WaitOne();
					Thread.Sleep((int)m_TransmitDelay);
					if (ControllerCard.IsConnected)
					{
						if (m_OnFirstCircle)
						{
							m_OnFirstCircle = false;
							//首次不修改输出模块的值，只是在需要修改的时候才修改
							m_ModifyValueList.Clear();
							for (int i = 0; i < m_MonitorList.Count; i++)
							{
								var L = m_MonitorList[i].ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
								if (L != null)
								{
									foreach (var it in L)
										m_ModifyValueList.Add(0x55);
								}
							}
						}
						lock (m_SetGetLock)
						{
							try
							{
								ControllerCard.GetModuleValue(m_ModifyValueList, out m_InputValueRecv_List, out List<DataRecieveModel> outputValueList);
								m_OutputValueRecv_List = outputValueList;

								if (m_OutputValueRecv_List.Count == m_ModifyValueList.Count)
								{
									for(int i =0; i< m_OutputValueRecv_List.Count; i++)
										m_ModifyValueList[i] = m_OutputValueRecv_List[i].RawValue;
								}
							}
							catch (Exception ex)
							{
								ShowMessage(EnumMsgType.Error, $"Controller Disconnected :{ex.Message}");
								ControllerCard.IsConnected = false;
								MessageBox.Show("Controller Disconnected");
							}
						}
						Console.WriteLine($"{m_InputValueRecv_List.Count},{m_OutputValueRecv_List.Count}");
						UpdateChangeDisplayFormat();
					}
				}
			}, ctsMonitorController.Token);

			//HeartBeat
			Task.Run(() =>
			{
				int i = 0;
				while (!ctsHeartBeat.IsCancellationRequested)
				{
					EventHeartBeat.WaitOne();
					Thread.Sleep(500);
					Console.WriteLine($"---------------Heart beat {i++}------------------");
					if (ControllerCard.IsConnected)
					{
						try
						{
							if (!ControllerCard.Hearbeat())
							{
								EventHeartBeat.Reset();
								MessageBox.Show("Connection Timeout");
							}
						}
						catch (Exception ex)
						{
							EventHeartBeat.Reset();
							MessageBox.Show($"Connection Timeout:{ex.Message}");
							Console.WriteLine($"Connection Timeout:{ex.StackTrace}");

						}
					}
				}
			}, ctsHeartBeat.Token);

			//TaskPick Message
			Task.Run(()=> {
				while (!ctsPickMsg.IsCancellationRequested)
				{
					lock (m_pickMsgLock)
					{
						if (m_msgQueue.Count == 0)
						{
							Thread.Sleep(100);
							continue;
						}
					}
					if (m_msgQueue.Count != 0)
					{
						var data = m_msgQueue.Dequeue();
						UpdateDiagram(data);
						AutoScrollListBox();
					}
				}
			});

		}

		private void LeftControl_OnBusMenuDeleteClicked(object sender, string e)
		{
			
		}

		private void LeftControl_OnBusMenuAddClicked(object sender, string e)	//drop
		{
			NewProject(e);
		}

		private void M_DiagramOutputWindow_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			CopyToClipboard();
		}

		/// <summary>
		/// listbox  context menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void Ctx_copymenu_Click(object sender, EventArgs e)
		{
			CopyToClipboard();
		}

		private void CopyToClipboard()
		{
			if (this.m_DiagramOutputWindow.SelectedIndex != -1)
			{
				var text = m_DiagramOutputWindow.SelectedItem.ToString();
				Clipboard.SetDataObject(text);
			}
		}


		private void DockPanelDiagram_SizeChanged(object sender, EventArgs e)
		{
			m_DiagramOutputWindow.Height = this.dockPanelDiagram.Height-10;
			m_DiagramOutputWindow.Width = this.dockPanelDiagram.Width;
		}

		private void BusController_OnDataComeHandler(object sender, ControllerLib.Model.DataFromComport e)
		{
			lock (m_pickMsgLock)
			{
				m_msgQueue.Enqueue(e);
			}
		}

		/// <summary>
		/// 添加列表项
		/// </summary>
		/// <param name="data"></param>
		private void UpdateDiagram(ControllerLib.Model.DataFromComport data)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() =>
				{
					m_DiagramOutputWindow.Items.Add(data.TotalMsg);
					if (m_DiagramOutputWindow.Items.Count > 5000)
						m_DiagramOutputWindow.Items.RemoveAt(0);
				}));
			}
			else
			{
				m_DiagramOutputWindow.Items.Add(data.TotalMsg);
				if (m_DiagramOutputWindow.Items.Count > 5000)
					m_DiagramOutputWindow.Items.RemoveAt(0);
			}

		}

		/// <summary>
		/// 自动滚动
		/// </summary>
		private void AutoScrollListBox()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() =>
				{
					this.m_DiagramOutputWindow.TopIndex = this.m_DiagramOutputWindow.Items.Count - (int)(this.m_DiagramOutputWindow.Height / this.m_DiagramOutputWindow.ItemHeight);
				}));
			}
			else
			{
				this.m_DiagramOutputWindow.TopIndex = this.m_DiagramOutputWindow.Items.Count - (int)(this.m_DiagramOutputWindow.Height / this.m_DiagramOutputWindow.ItemHeight);
			}
		}

		//Display when change the format
		private void UcMonitor_OnChangeDisplayFormatHandler(object sender, Model.DisplayFormat.DisplayFormatBase e)
		{
			m_DisplayFormat = e;
			//可以刷新以下显示的数据
			UpdateChangeDisplayFormat();
		}

		private void UpdateChangeDisplayFormat()
		{
			var OutputMonitorModule = m_VarCollect.Where(c => c.IoType == EnumModuleIOType.OUT);
			var InputMonitorModule = m_VarCollect.Where(c => c.IoType == EnumModuleIOType.IN);
			if (OutputMonitorModule != null && OutputMonitorModule.Count() == m_OutputValueRecv_List.Count)
			{
				for (int i = 0; i < m_OutputValueRecv_List.Count; i++)
				{
					m_DisplayFormat.SetRawDataFromInt(m_OutputValueRecv_List[i].ByteCount,m_OutputValueRecv_List[i].RawValue);
					OutputMonitorModule.ElementAt(i).CurValue = m_DisplayFormat.GetString();
				}
			}
			if (InputMonitorModule != null && InputMonitorModule.Count() == m_InputValueRecv_List.Count)
			{
				for (int i = 0; i < m_InputValueRecv_List.Count; i++)
				{
					m_DisplayFormat.SetRawDataFromInt(m_InputValueRecv_List[i].ByteCount,m_InputValueRecv_List[i].RawValue);
					InputMonitorModule.ElementAt(i).CurValue = m_DisplayFormat.GetString();
				}
			}
			m_OldBase = m_DisplayFormat.Base;
		}




		/// <summary>
		/// 当总线改变的时候
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LeftControl_OnBusModuleChanged(object sender, SubBusContrainer.Model.ModuleAddedArgs e)
		{
			if (e.IsAdd)
			{
				var T = typeof(BusConfigBase);
				var ClassName = $"ControllerLib.BusConfigModle.BusConfig_{e.Module.Name}";
				var obj = T.Assembly.CreateInstance(ClassName) as BusConfigBase;
				ProjController.BusCfg = obj;
			}
			else
			{
				ProjController.BusCfg = null;
			}

			// e.Name
			//ProjController.BusCfg=new 
			//throw new NotImplementedException();
		}

		private void BarSubIteExportFile_Popup(object sender, EventArgs e)
		{
			if (ProjController.BusCfg != null)
			{
				var C = (sender as BarSubItem).LinksPersistInfo;
				foreach (LinkPersistInfo it in C)
					it.Item.Enabled = it.Item.Caption.Contains(ProjController.BusCfg.ShortName);
			}

		}


		private void TreeViewDevice_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			var node = treeViewDevice.SelectedNode;
			if (node != null)
			{
				string ProductName = node.Text;
				var list = ProductName.Split(' ');
				var nLen = list.Length;
				MiddleControl.OnProductItemDoubleClick(this, list[0].Replace("-", "_"));
			}
			
		}

		private void TreeViewDevice_ItemDrag(object sender, ItemDragEventArgs e)
		{
			string ProductName = (e.Item as TreeNode).Text;
			var list = ProductName.Split(' ');
			var nLen = list.Length;
			treeViewDevice.DoDragDrop(list[0].Replace("-", "_"), DragDropEffects.Copy);
		}

		private void barButtonItemOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (ProjController.OpenProject())
				LeftControl.ReplaceNewList(ProjController.BusCfg.BusType.ToString(), ProjController.SubBusNameWithIndexList());
		}

		private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			UpdateProjController();
			ProjController.SaveProject();
		}

		/// <summary>
		/// 可以考虑使用GUID
		/// </summary>
		private void UpdateProjController()
		{
			ProjController.ModuleConfigList.Clear();
			foreach (var it in MiddleControl.Controls)
			{
				if (it is BusModel)
				{
					//DoNothing
				}
				else if (it is SubBusModel)
				{
					SubBusModel SB = it as SubBusModel;
					var SubBusClassName = $"ControllerLib.Ethercat.ModuleConfigModle.ModuleConfig_{SB.ModuleType.ToString()}";
					Type T = typeof(ModuleConfigModleBase);
					dynamic obj = T.Assembly.CreateInstance(SubBusClassName);
					ModuleConfigModleBase CfgBase = obj as ModuleConfigModleBase;
					var list = SB.Mcb.ToStringList().ToArray();
					CfgBase.FromString(list);
					ProjController.ModuleConfigList.Add(CfgBase);
				}
			}
			ProjController.ModuleConfigList.Sort((a, b) => { return a.GlobalIndex - b.GlobalIndex; });
		}

		private void barButtonItemCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			
		}

		/// <summary>
		/// copy
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			CopyToClipboard();
		}

		private void barButtonItemPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		private void barButtonItemSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ConfigMgr.Instance.LoadConfig();
			var para = ConfigMgr.Instance.ParaCfgEntry;

			Window_ComSetting window = new Window_ComSetting();

			ComSettingCfgModel.Baudrate = (int)para.Bandarate;
			ComSettingCfgModel.Data = (byte)para.Data;
			if (Enum.TryParse(para.Parity, out System.IO.Ports.Parity parity))
			{
				ComSettingCfgModel.Parity = parity;
			}
			ComSettingCfgModel.ReceiveTimeout = para.ReceiveTimeout;

			if (Enum.TryParse(para.Stop, out System.IO.Ports.StopBits stopbits))
			{
				ComSettingCfgModel.Stop = stopbits;
			}
			ComSettingCfgModel.TransmitDelay = para.TransmitDelay;

			if (ComSettingCfgModel != null)
				window.ComSetting = ComSettingCfgModel;
			var Res = window.ShowDialog();

			
			if (window.IsOkClicked)
			{
				ComSettingCfgModel = window.ComSetting;
				ControllerCard.SetTimeout(ComSettingCfgModel.ReceiveTimeout);
				m_TransmitDelay = ComSettingCfgModel.TransmitDelay;

				para.Bandarate = (uint)ComSettingCfgModel.Baudrate;
				para.Data = ComSettingCfgModel.Data;
				para.Parity = ComSettingCfgModel.Parity.ToString();
				para.ReceiveTimeout = ComSettingCfgModel.ReceiveTimeout;
				para.Stop = ComSettingCfgModel.Stop.ToString();
				para.TransmitDelay = ComSettingCfgModel.TransmitDelay;

				ConfigMgr.Instance.SaveConfig();
			}

		}

		private void barButtonItemConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			try
			{
				if (ComSettingCfgModel == null)
				{
					MessageBox.Show("Please select comport", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string RegStr = @"COM\d{1,2}";
				if (Regex.IsMatch(ComSettingCfgModel.ComportName, RegStr))
				{
					ControllerCard.Open(ComSettingCfgModel.ComportName);
					if (true || !ControllerCard.IsConnected)
					{
						if (ControllerCard.Connect())
						{
							//UpdateMonitorVarCollect(out List<ModuleInfoBase> list);
							//打开心跳
							//EventHeartBeat.Set();
							MessageBox.Show("Connect sucessfully");
						}
						else
							MessageBox.Show("Can't connect to the controller! Please check!");
					}
					else
					{
						//BusController.CLose();
						MessageBox.Show("Controller is already connected");
					}
				}
				else
				{
					MessageBox.Show("Please select a comport to connect controller");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error when connect to controller:{ex.StackTrace}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
			}
		}
		private void barButtonItem_Disconnect_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (ComSettingCfgModel == null)
				{
					MessageBox.Show("Please select comport", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string RegStr = @"COM\d{1,2}";
				if (Regex.IsMatch(ComSettingCfgModel.ComportName, RegStr))
				{
					ControllerCard.Open(ComSettingCfgModel.ComportName);
					//BusController.CLose();
					EventHeartBeat.Reset();
					ControllerCard.DisConnect();
					MessageBox.Show("Disconnect controller");

				}
				else
				{
					MessageBox.Show("Please select a comport to connect controller");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error when Disconnect controller:{ex.Message}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
			}
		}
		private void barButtonItemContacUs_ItemClick(object sender, ItemClickEventArgs e)
		{
			System.Diagnostics.Process.Start(@".\help\contactus.html");
		}
		private void barButtonItemManual_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(@".\help\manual.pdf");
			}
			catch
			{
				MessageBox.Show("Open manual.pdf error, please check");
			}
		}
		private void barButtonItemCalCRC_ItemClick(object sender, ItemClickEventArgs e)
		{
			var dlg = new Windows_CRC();
			dlg.ShowDialog();
		}

		private void barButtonItemDiagram_ItemClick(object sender, ItemClickEventArgs e)
		{
			dockPanelDiagram.Visibility = dockPanelDiagram.Visibility == DockVisibility.Visible ? DockVisibility.Hidden : DockVisibility.Visible;
			if (dockPanelDiagram.Visibility == DockVisibility.Visible)
				dockManager1.ActivePanel = dockPanelDiagram;
		}
		private void barButtonItemUpload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			try
			{
				m_ModuleList = ControllerCard.GetModuleList();
			
				UpdateUI(m_ModuleList);
				UpdateMonitorVarCollect(out m_MonitorList);

			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error when upload to controller:{ex.Message}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 将读取到的模块信息显示在界面上
		/// Controller自动判断属于哪种控制器
		/// </summary>
		private void UpdateUI(List<ModuleConfigModleBase> ModuleList)
		{
			ProjController.ModuleConfigList = ModuleList;	
			LeftControl.ReplaceNewList(ProjController.BusName, ProjController.SubBusNameWithIndexList(false));
		}

		private void barButtonItemDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			try
			{
				UpdateProjController();
				m_ModuleList = ProjController.ModuleConfigList;
				UpdateMonitorVarCollect(out m_MonitorList);
				if (ControllerCard.SendModuleList(m_ModuleList))
					MessageBox.Show("Download success", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				else
					MessageBox.Show("Download failed", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error when download to controller:{ex.Message}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
			}
		}

		private void barButtonItemMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			dockPanelVarMonitor.Visibility = dockPanelVarMonitor.Visibility == DockVisibility.Visible ? DockVisibility.Hidden : DockVisibility.Visible;
			if (dockPanelVarMonitor.Visibility == DockVisibility.Visible)
				dockManager1.ActivePanel = dockPanelVarMonitor;

		}

		private void MenuSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		private void barButtonItemArrangWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.dockPanelLeft.Show();
			this.dockPanelMiddle.Show();
			this.dockPanelRight.Show();
			this.dockPanelDown.Show();
			this.dockPanelDiagram.Show();
		}

		#region VarMonitor

		private void UcMonitor_OnModifyValueEventHandler(object sender, EventArgs e)
		{
			try
			{
				
					var OutputValueList = m_VarCollect.Where(c => c.IoType == EnumModuleIOType.OUT);
					if (OutputValueList != null)
					{
						List<UInt32> list = new List<UInt32>();
						if (OutputValueList.Count() == m_OutputValueRecv_List.Count)
						{
							for (int i = 0; i < OutputValueList.Count(); i++)
							{
								var Bytelen = m_OutputValueRecv_List[i].ByteCount;
								var ModifyValue= OutputValueList.ElementAt(i).CurValue;
								if (!string.IsNullOrEmpty(OutputValueList.ElementAt(i).ModifyValue))
								{
									ModifyValue = OutputValueList.ElementAt(i).ModifyValue;
									m_DisplayFormat.SetRawDataFromString(Bytelen, ModifyValue, m_DisplayFormat.Base);
								}
								m_DisplayFormat.SetRawDataFromString(Bytelen, ModifyValue, m_DisplayFormat.Base);
								list.Add(m_DisplayFormat.GetRawData());
							}

						}
					lock (m_SetGetLock)
					{
						//只看修改的值，如果没有修改就直接将原来读取的值拿过来
						ControllerCard.SetModuleValue(list);
						m_ModifyValueList = list;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void UcMonitor_OnStopMonitorEventHandler(object sender, EventArgs e)
		{
			this.EventMonitorController.Reset();

			//this.EventHeartBeat.Set();
		}

		private void UcMonitor_OnStartMonitorEventHandler(object sender, EventArgs e)
		{
			this.EventMonitorController.Set();
			this.EventHeartBeat.Reset();

		}

		private void DockPanelVarMonitor_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
		{
			if (e.Visibility == DockVisibility.Hidden && ControllerCard.IsConnected)
			{
				this.EventMonitorController.Reset();
				//this.EventHeartBeat.Set();
			}
		}

		#endregion

		private void barButtonItemNewProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

			NewProject(e.Item.Caption);
		}

		private void NewProject(string BusName)
		{
			var name = BusName.Replace("-", "_");
			var BusClassName = $"BusConfig_{name}";
			LeftControl.ReplaceNewList(BusClassName, new List<Tuple<string, int, int, ControlTest.ModuleConfigModle.ModuleGUIBase>>());	
			MiddleControl.ChangeBus(name);
		}
	
		/// <summary>
		/// 导出文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItemExportFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			var strFilter = $"{ProjController.BusFileMgr.ExtString} File(*.{ProjController.BusFileMgr.ExtString})|*.{ProjController.BusFileMgr.ExtString}";
			sfd.Filter = strFilter;
			sfd.FilterIndex = 2;
			sfd.RestoreDirectory = true;
			sfd.InitialDirectory = FileOpenPath;
			sfd.FileName = $"{ProjController.BusCfg.Name}.{ProjController.BusFileMgr.ExtString}";
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				FileOpenPath = sfd.FileName;
				var NameModelList = new List<ModuleNameModel>();
				foreach (var it in LeftControl.PureNameList)
					NameModelList.Add(new ModuleNameModel()
					{
						PureName = it,
					});
				ProjController.BusFileMgr.SaveFile(NameModelList, FileOpenPath);
			}
		}

		private void ShowMessage(EnumMsgType MsgType, string Msg)
		{
			if (!InvokeRequired)
				this.uC_Output1.MsgCollect.Add(new MessageModel(MsgType, Msg));
			else
				Invoke(new Action(()=> {
					this.uC_Output1.MsgCollect.Add(new MessageModel(MsgType, Msg));
				}));
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//dockPanelVarMonitor.Visibility = DockVisibility.Hidden;
		}

		private void UpdateMonitorVarCollect(out List<ModuleInfoBase> listMonitorModule)
		{
			listMonitorModule = new List<ModuleInfoBase>();
			if (m_ModuleList != null)
			{
				var t = typeof(ModuleInfoBase);
				foreach (var it in m_ModuleList)
				{
					var obj = t.Assembly.CreateInstance($"NetView.Model.ModuleInfo.ModuleInfo_{it.DeviceName}") as ModuleInfoBase;
					obj.LocalIndex = it.GlobalIndex;
					obj.LocalIndex = it.LocalIndex;
					listMonitorModule.Add(obj);
				
				}
				if (InvokeRequired)
				{
					BeginInvoke(new Action(() => { m_VarCollect.Clear(); }));
				}
				else
				{
					m_VarCollect.Clear();
				}
				foreach (var it in listMonitorModule)
				{
					foreach (var c in it.ModuleList)
					{
						if (InvokeRequired)
						{
							BeginInvoke(new Action(() =>
							{
								m_VarCollect.Add(new MonitorVarModel()
								{
									IoType = c.IOType,
									SubModelName = $"{c.Name}_{it.LocalIndex}",
								});

							}));
						}
						else
						{
							m_VarCollect.Add(new MonitorVarModel()
							{
								IoType = c.IOType,
								SubModelName = $"{c.Name}_{it.LocalIndex}",
							});
						}
					}
				}
			}
		}


	}
}
