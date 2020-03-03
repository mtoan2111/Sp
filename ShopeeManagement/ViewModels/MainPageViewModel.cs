using ShopeeManagement.Base;
using ShopeeManagement.Models;
using ShopeeManagement.Services;
using ShopeeManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ShopeeManagement.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public ICommand BtHamburgerTapped { get; set; }
		public ICommand NavigationFrameLoaded { get; set; }
		public ObservableCollection<MainFeatures> ListMainFeatures { get; set; }
		public ObservableCollection<MainFeatures> ListMainManagement { get; set; }

		private ObservableCollection<ShopInfoLogin> _listShop;

		private MainFeatures _selectedMainFeature;
		private MainFeatures _selectedSettingFeature;
		private ShopInfoLogin _selectedShop;
		private int? _selectedMainFeatureIndex;
		private int? _selectedSettingFeatureIndex;
		private int _totalMessage;
		private int _totalOrder;

		private string _cbxPlaceholder;
		private string _tbxVersion;
		private string _token;
		private string _tbxMessage;
		private string _tbxOrder;

		private event EventHandler HadShopInValid;

		private Frame _navFrame;
		private SplitView _spView;
		private Timer _getNewMessageTimer;
		private Timer _getNewOrderTimer;
		private Timer _getMetaDataTimer;


		public MainPageViewModel()
		{
			this.TbxVersion = "2.1.7.0";
			this.CbxPlaceholder = StaticResources.choosingLanguage.MainPageChooseAShop;
			this.TbxMessage = StaticResources.choosingLanguage.MainPageMessageCount;
			this.TbxOrder = StaticResources.choosingLanguage.MainPageOrderCount;
			foreach(var shop in StaticResources.lstShopInfoLogin)
			{
				if (string.IsNullOrEmpty(shop.token))
				{
					shop.token = Utility.GetTokenFromGuid(shop);
				}
			}

			_getMetaDataTimer = new Timer( async (state) =>
			{
				foreach(var shop in StaticResources.lstShopInfoLogin)
				{
					if (shop.ShopStatus.Contains("Out") == false)
					{
						if (string.IsNullOrEmpty(shop.token))
						{
							shop.token = Utility.GetTokenFromGuid(shop);
						}
						Utility.SyncMessage(shop, shop.token);
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						async () =>
						{
							shop.NewMessageCount = await Utility.GetMessageCount(shop);
							shop.NewOrderCount = await Utility.GetNewOrderCount(shop);
						});
					}
				}
			}, null, (int)TimeSpan.FromSeconds(5).TotalMilliseconds, (int)TimeSpan.FromMinutes(5).TotalMilliseconds);



			this._checkingSellerAccount();
			StaticResources.GlobalPropertyChanged += (s, e) =>
			{
				switch (e.PropertyName)
				{
					case "SelectedShopLogin":
						if (this.SelectedShop != StaticResources.SelectedShopLogin)
						{
							this.SelectedShop = StaticResources.SelectedShopLogin;
						}
						break;
					default:
						break;
				}
			};

			StaticResources.EvJumpToLoginPage += (s, e) =>
			{
				this.SelectedMainFeatureIndex = -1;
				this.SelectedMainFeature = null;
				this.SelectedSettingFeatureIndex = 0;
				this.SelectedSettingFeature = ListMainManagement[0];
				StaticResources.isLoginPage = false;
			};

			this.HadShopInValid += async (s, e) =>
			{
				await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningValid).ShowAsync();
				if (this._navFrame != null)
				{
					this._navFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
				}
			};



			ListMainFeatures = PickupStaticEntity.GetMainFeatures();
			ListMainManagement = PickupStaticEntity.GetMainManagement();
			ListShop = StaticResources.lstShopInfoLogin;
			this.SelectedSettingFeatureIndex = null;
			this.SelectedSettingFeature = null;
			if (StaticResources.lstShopInfoLogin == null)
			{
				StaticResources.lstShopInfoLogin = new ObservableCollection<ShopInfoLogin>();
			}
			BtHamburgerTapped = new DelegateCommand<SplitView>((s) =>
			{
				_spView = s;
				s.IsPaneOpen = !s.IsPaneOpen;
			});

			NavigationFrameLoaded = new DelegateCommand<Frame>((s) =>
			{
				_navFrame = s;
				StaticResources.NavigationFrame = s;
				this.SelectedShop = StaticResources.SelectedShopLogin;
			});
		}

		public MainFeatures SelectedMainFeature
		{
			get
			{
				return this._selectedMainFeature;
			}
			set
			{
				SetProperty(ref this._selectedMainFeature, value);
				if (_navFrame != null && this._selectedMainFeature != null)
				{
					try
					{
						this.SelectedSettingFeature = null;
						this.SelectedSettingFeatureIndex = null;
						_navFrame.Navigate(this._selectedMainFeature.PageType, null, new DrillInNavigationTransitionInfo());
					}
					catch
					{

					}
				}
				//SetProperty(ref this._selectedMainFeature, null);
			}
		}

		private async void _checkingSellerAccount()
		{

			foreach (var shop in StaticResources.lstShopInfoLogin)
			{
				await Utility.checkSellerCookieValidate(shop);
			}
			var shopInvalidateCount = StaticResources.lstShopInfoLogin.Where(p => p.ShopStatus.Contains("Out")).ToList().Count();
			if (shopInvalidateCount > 0)
			{
				HadShopInValid(this, null);
			}
			if (StaticResources.lstShopInfoLogin != null && StaticResources.lstShopInfoLogin.Count() > 0)
			{
				var lstWorkingShop = StaticResources.lstShopInfoLogin.Where(p => !p.ShopStatus.Contains("Out")).ToList();
				if(lstWorkingShop != null && lstWorkingShop.Count() > 0)
				{
					StaticResources.SelectedShopLogin = lstWorkingShop[0];
				}
			}
		}

		public ObservableCollection<ShopInfoLogin> ListShop
		{
			get
			{
				return this._listShop;
			}
			set
			{
				SetProperty(ref this._listShop, value);
			}
		}

		public ShopInfoLogin SelectedShop
		{
			get
			{
				return this._selectedShop;
			}
			set
			{
				if (this._selectedShop != value)
				{
					SetProperty(ref this._selectedShop, value);
					if (this._selectedShop != null)
					{
						if (value.ShopStatus.Contains("Out"))
						{
							this.HadShopInValid(this, null);
						}
						else
						{
							StaticResources.SelectedShopLogin = this._selectedShop;
							if (_navFrame != null)
							{
								this.SelectedMainFeatureIndex = 0;
								if (this._spView != null)
								{
									this._spView.IsPaneOpen = false;
								}
								try
								{
									if (string.IsNullOrEmpty(StaticResources.SelectedShopLogin.token))
									{
										StaticResources.SelectedShopLogin.token = Utility.GetTokenFromGuid(StaticResources.SelectedShopLogin);
									}
									if (this._getNewMessageTimer != null)
									{
										this._getNewMessageTimer.Change(Timeout.Infinite, Timeout.Infinite);
									}
									this._getNewMessageTimer = new Timer(async (state) =>
									{
										if (StaticResources.SelectedShopLogin.ShopStatus.Contains("Out") == false)
										{
											Utility.SyncMessage(StaticResources.SelectedShopLogin, StaticResources.SelectedShopLogin.token);
											this._totalMessage = await Utility.GetMessageCount(StaticResources.SelectedShopLogin);
											await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
											() =>
											{
												StaticResources.SelectedShopLogin.NewMessageCount = this._totalMessage;
												StaticResources.MessageFeature.SelfCount = _totalMessage;
											});
										}
									}, null, (int)(TimeSpan.FromSeconds(2).TotalMilliseconds), (int)(TimeSpan.FromMinutes(1).TotalMilliseconds));
									if (_getNewOrderTimer != null)
									{
										_getNewOrderTimer.Change(Timeout.Infinite, Timeout.Infinite);
									}
									_getNewOrderTimer = new Timer(async (state) =>
									{
										if (StaticResources.SelectedShopLogin.ShopStatus.Contains("Out") == false)
										{
											_totalOrder = await Utility.GetNewOrderCount(StaticResources.SelectedShopLogin);
											await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
											() =>
											{
												StaticResources.SelectedShopLogin.NewOrderCount = this._totalOrder;
												StaticResources.OrderFeature.SelfCount = _totalOrder;
											});
										}
									}, null, (int)(TimeSpan.FromSeconds(2).TotalMilliseconds), (int)(TimeSpan.FromMinutes(10).TotalMilliseconds));
								}
								catch
								{

								}

								StaticResources.MessageFeature.SelfCount = StaticResources.SelectedShopLogin.NewMessageCount;
								StaticResources.OrderFeature.SelfCount = StaticResources.SelectedShopLogin.NewOrderCount;

								if (StaticResources.isOpenChatPage == true)
								{
									this.SelectedMainFeatureIndex = 3;
									StaticResources.isOpenChatPage = false;
									_navFrame.Navigate(typeof(ShopeeChatPage), null, new DrillInNavigationTransitionInfo());
								}
								else if (StaticResources.isOpenOrderPage == true)
								{
									this.SelectedMainFeatureIndex = 4;
									StaticResources.isOpenOrderPage = false;
									_navFrame.Navigate(typeof(ShopeeOrderPage), null, new DrillInNavigationTransitionInfo());
								}
								else
								{
									this.SelectedMainFeatureIndex = 0;
									_navFrame.Navigate(typeof(ShopeeProductPage), null, new DrillInNavigationTransitionInfo());
								}
							}
						}
					}
					else if (this._selectedShop == null)
					{
						this.SelectedMainFeatureIndex = null;
					}
				}
			}
		}

		public int? SelectedMainFeatureIndex
		{
			get
			{
				return this._selectedMainFeatureIndex;
			}
			set
			{
				SetProperty(ref this._selectedMainFeatureIndex, value);
			}
		}

		public MainFeatures SelectedSettingFeature
		{
			get
			{
				return this._selectedSettingFeature;
			}
			set
			{
				SetProperty(ref this._selectedSettingFeature, value);
				if (_navFrame != null && this._selectedSettingFeature != null)
				{
					try
					{
						this.SelectedMainFeature = null;
						this.SelectedMainFeatureIndex = null;
						_navFrame.Navigate(this._selectedSettingFeature.PageType, null, new DrillInNavigationTransitionInfo());
					}
					catch
					{

					}
				}
			}
		}
		public int? SelectedSettingFeatureIndex
		{
			get
			{
				return this._selectedSettingFeatureIndex;
			}
			set
			{
				SetProperty(ref this._selectedSettingFeatureIndex, value);
			}
		}

		public string CbxPlaceholder
		{
			get
			{
				return this._cbxPlaceholder;
			}
			set
			{
				SetProperty(ref this._cbxPlaceholder, value);
			}
		}

		public string TbxVersion
		{
			get => this._tbxVersion;
			set => SetProperty(ref this._tbxVersion, value);
		}

		public string TbxMessage
		{
			get => this._tbxMessage;
			set => SetProperty(ref this._tbxMessage, value);
		}

		public string TbxOrder
		{
			get => this._tbxOrder;
			set => SetProperty(ref this._tbxOrder, value);
		}
	}
}
