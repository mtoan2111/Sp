using ShopeeManagement.Base;
using ShopeeManagement.Models;
using ShopeeManagement.Services;
using ShopeeManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ShopeeManagement.ViewModels
{
	public class StartPageViewModel : ViewModelBase
	{
		public ICommand BtStartPageTapped { get; set; }
		public ICommand FrStartPageLoaded { get; set; }
		public ICommand WvStartPageNavigated { get; set; }
		public ObservableCollection<ShopeeServer> ListShopeeServer { get; set; } = new ObservableCollection<ShopeeServer>();

		private ShopeeServer _selectedShopeeServer;
        private int _selectedShopeeServerIndex = -1;


        private string _grStartPageTag;
		private string _grCheckingConnectionTag;
		private string _connectionStatus;
		private string _grWelcomeTag;
		private string _dimensionConfigFileName;
		private string _logisticsChannelsFileName;

		private Uri _wvStartPageSource;

		private Frame _frStartPage;
		public StartPageViewModel()
		{
            if (ApplicationData.Current.LocalSettings.Values["SelectedShopeeServerIndex"] != null)
            {
                this.SelectedShopeeServerIndex = (int)ApplicationData.Current.LocalSettings.Values["SelectedShopeeServerIndex"];
            }
            this.GrCheckingConnectionTag = "Visible";
			this.ConnectionStatus = "Checking your connection";
			new Thread(async () => 
			{
				while (!await this._checkingNetworkConnection())
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						this.ConnectionStatus = "Connection error! Please checking your network again";
					});
					Thread.Sleep(10000);
				}
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					this.GrCheckingConnectionTag = "Collapsed";
					this.GrWelcomeTag = "Visible";
					this.WvStartPageSource = new Uri("http://18.221.114.122:8000/selltools/HDSD_ShopeeUp.htm");
				});
			}).Start();
			ListShopeeServer = PickupStaticEntity.getShopeeServer();

			WvStartPageNavigated = new DelegateCommand(() =>
			{
				this.GrWelcomeTag = "Collapsed";
			});

			BtStartPageTapped = new DelegateCommand<ShopeeServer>((s) =>
			{
				if (this.SelectedShopeeServerIndex == 0)
				{
					StaticResources.choosingLanguage = StaticResources.Vietnamese;
				}
				else
				{
					StaticResources.choosingLanguage = StaticResources.English;
				}
				switch (s.ShortRegion)
				{
					case "VN":
						this._dimensionConfigFileName = "/DimensionConfig.json";
						this._logisticsChannelsFileName = "/LogisticsConfig.json";
						break;
					case "SG":
						this._dimensionConfigFileName = "/DimensionConfigSG.json";
						this._logisticsChannelsFileName = "/LogisticsConfigSG.json";
						break;
					case "MA":
						this._dimensionConfigFileName = "/DimensionConfigMA.json";
						this._logisticsChannelsFileName = "/LogisticsConfigMA.json";
						break;
					case "ID":
						this._dimensionConfigFileName = "/DimensionConfigID.json";
						this._logisticsChannelsFileName = "/LogisticsConfigID.json";
						break;
					case "PH":
						this._dimensionConfigFileName = "/DimensionConfigPH.json";
						this._logisticsChannelsFileName = "/LogisticsConfigPH.json";
						break;
					default:
						break;
				}

				StaticResources.MainUri = s.UriSet.MainUri;
				StaticResources.SellerMainUri = s.UriSet.SellerMainUri;
				StaticResources.HostUri = s.UriSet.HostUri;
				StaticResources.SellerHostUri = s.UriSet.SellerHostUri;
				StaticResources.ReferUri = s.UriSet.ReferUri;
				StaticResources.SellerReferUri = s.UriSet.SellerReferUri;
				StaticResources.OriginUri = s.UriSet.OriginUri;
				StaticResources.SellerOriginUri = s.UriSet.SellerOriginUri;
				StaticResources.ImgServer = s.UriSet.ImgServer;
				StaticResources.ShortRegion = s.ShortRegion;
				StaticResources.Currency = s.Currency;
				StaticResources.ChatUri = s.UriSet.ChatUri;
				StaticResources.SaleUri = s.UriSet.SaleUri;
				StaticResources.ShopConfigUri = s.UriSet.ShopConfigUri;
				StaticResources.lstShopInfoLogin = new ObservableCollection<ShopInfoLogin>(StaticResources.TotalSellerAccount.Where(p => p.ShortRegion == s.ShortRegion));
				foreach(var shop in StaticResources.lstShopInfoLogin)
				{
					StaticResources.DictGuid[shop.Shop.id] = Guid.NewGuid().ToString();
				}
				try
				{
					var path = ApplicationData.Current.LocalFolder.Path;
					using (var jsonReader = new StreamReader(path + this._dimensionConfigFileName))
					{
						StaticResources.ShopeePreConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopeePreConfig>(jsonReader.ReadToEnd());
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
				try
				{
					var path = ApplicationData.Current.LocalFolder.Path;
					using (var jsonReader = new StreamReader(path + this._logisticsChannelsFileName))
					{
						StaticResources.LogisticsChannel = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<LogisticsChannelClone>>(jsonReader.ReadToEnd());
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
				if (this._frStartPage != null)
				{
					this._frStartPage.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
					//this._frStartPage.Navigate(typeof(ShopeeNewItemPage), null, new DrillInNavigationTransitionInfo());
				}
				ApplicationData.Current.LocalSettings.Values["SelectedShopeeServerIndex"] = this.SelectedShopeeServerIndex;
            });

			FrStartPageLoaded = new DelegateCommand<Frame>((s) =>
			{
				if (s != null)
				{
					this._frStartPage = s;
				}
			});
		}


		private async Task<bool> _checkingNetworkConnection()
		{
			using (HttpClient client = new HttpClient())
			{
				try
				{
					string responseBody = await client.GetStringAsync(new Uri("https://www.google.com/"));
					return true;
				}
				catch
				{
					return false;
				}
			}
		}
		public ShopeeServer SelectedShopeeServer
		{
			get
			{
				return this._selectedShopeeServer;
			}
			set
			{
				SetProperty(ref this._selectedShopeeServer, value);
				if(this._selectedShopeeServer != null)
				{
					this.GrStartPageTag = "Visible";
				}
			}
		}

        public int SelectedShopeeServerIndex
        {
            get
            {
                return this._selectedShopeeServerIndex;
            }
            set
            {
                SetProperty(ref this._selectedShopeeServerIndex, value);
                if (this._selectedShopeeServerIndex != -1)
                {
                    this.GrStartPageTag = "Visible";
                }
            }
        }

        public string GrStartPageTag
		{
			get
			{
				return this._grStartPageTag;
			}
			set
			{
				SetProperty(ref this._grStartPageTag, value);
			}
		}

		public string GrCheckingConnectionTag
		{
			get
			{
				return this._grCheckingConnectionTag;
			}
			set
			{
				SetProperty(ref this._grCheckingConnectionTag, value);
			}
		}

		public string ConnectionStatus
		{
			get
			{
				return this._connectionStatus;
			}
			set
			{
				SetProperty(ref this._connectionStatus, value);
			}
		}

		public string GrWelcomeTag
		{
			get
			{
				return this._grWelcomeTag;
			}
			set
			{
				SetProperty(ref this._grWelcomeTag, value);
			}
		}

		public Uri WvStartPageSource
		{
			get
			{
				return this._wvStartPageSource;
			}
			set
			{
				SetProperty(ref this._wvStartPageSource, value);
			}
		}
	}
}
