using ShopeeManagement.Base;
using ShopeeManagement.Models;
using ShopeeManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http.Filters;

namespace ShopeeManagement.ViewModels
{
	public class ShopeeLoginPageViewModel : ViewModelBase
	{
		public ICommand wvContentLoadding { get; set; }
		public ICommand wvShopeeLoginLoaded { get; set; }
		public ICommand ShopeeLoginPageLoading { get; set; }
		public ICommand BtTurnOfLoginFormTapped { get; set; }
		public ICommand BtAddNewAccountTapped { get; set; }
		public ICommand BtDeleteAccountTapped { get; set; }
		public ICommand BtDeleteAllAccountTapped { get; set; }
		public ICommand BtOpenChatPageTapped { get; set; }
		public ICommand BtOpenOrderPageTapped { get; set; }
		public ICommand BtRevenueReportTapped { get; set; }

		private ShopInfoLogin _selectedShop;
		private Uri _shopeeLoginUri;
		private Visibility _isGrLoginFormShown;
		public List<ShortCookie> LstShortCookie = new List<ShortCookie>();
		private ObservableCollection<ShopInfoLogin> _listShop;

		private string _tgGrFormLogin;
		private string _tbxHeaderAccount;
		private string _tbxUserName;
		private string _tbxStatus;
		private string _tbxDeleteAll;
		private string _tbxDelete;
		private string _tbxAddNewAccount;
		private string _tbxMessage;
		private string _tbxOrder;
		private string _tbxMessageOpen;
		private string _tbxOrderOpen;
		private string _tbxWeekRevenue;
		private string _tbxMonthRevenue;
		private string _tbxTotalRevenue;
		private string _tbxRevenueReport;

		public ShopeeLoginPageViewModel()
		{
			this.TbxHeaderAccount = StaticResources.choosingLanguage.LoginPageHeaderAccount;
			this.TbxUserName = StaticResources.choosingLanguage.LoginPageUserName;
			this.TbxStatus = StaticResources.choosingLanguage.LoginPageStatus;
			this.TbxDeleteAll = StaticResources.choosingLanguage.LoginPageDeleteAll;
			this.TbxDelete = StaticResources.choosingLanguage.LoginPageDelete;
			this.TbxAddNewAccount = StaticResources.choosingLanguage.LoginPageAddNewAccount;
			this.TbxMessage = StaticResources.choosingLanguage.LoginPageNewMessage;
			this.TbxOrder = StaticResources.choosingLanguage.LoginPageNewOrder;
			this.TbxMessageOpen = StaticResources.choosingLanguage.LoginPageMessageOpen;
			this.TbxOrderOpen = StaticResources.choosingLanguage.LoginPageOrderOpen;
			this.TbxWeekRevenue = StaticResources.choosingLanguage.LoginPageWeekRevenue;
			this.TbxMonthRevenue = StaticResources.choosingLanguage.LoginPageMonthRevenue;
			this.TbxTotalRevenue = StaticResources.choosingLanguage.LoginPageTotalRevenue;
			this.TbxRevenueReport = StaticResources.choosingLanguage.LoginPageRevenueReport;

			this.isGrLoginFormShown = Visibility.Collapsed;
			this.ListShop = StaticResources.lstShopInfoLogin;
			//this.ShopeeLoginUri = new Uri(StaticResources.SellerMainUri + "account/signin");
			BtTurnOfLoginFormTapped = new DelegateCommand(() =>
			{
				this.isGrLoginFormShown = Visibility.Collapsed;
				this.TgGrFormLogin = "Collapsed";
			});

			BtOpenChatPageTapped = new DelegateCommand<ShopInfoLogin>(async (s) =>
			{
				StaticResources.isOpenChatPage = true;
				StaticResources.isOpenOrderPage = false;
				if (StaticResources.SelectedShopLogin == s)
				{
					await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningCurrentShop).ShowAsync();
					return;
				}
				StaticResources.SelectedShopLogin = s;
			});

			BtRevenueReportTapped = new DelegateCommand(() =>
			{
				foreach (var shop in StaticResources.lstShopInfoLogin)
				{
					new Thread(async () =>
					{
						await Utility.GetRevenueCount(shop);
					}).Start();
				}
			});

			BtOpenOrderPageTapped = new DelegateCommand<ShopInfoLogin>(async (s) =>
			{
				StaticResources.isOpenChatPage = false;
				StaticResources.isOpenOrderPage = true;
				if (StaticResources.SelectedShopLogin == s)
				{
					await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningCurrentShop).ShowAsync();
					return;
				}
				StaticResources.SelectedShopLogin = s;
			});

			BtDeleteAccountTapped = new DelegateCommand<ShopInfoLogin>((s) =>
			{
				if (StaticResources.SelectedShopLogin == s)
				{
					StaticResources.SelectedShopLogin = null;
				}
				StaticResources.lstShopInfoLogin.Remove(s);
				StaticResources.TotalSellerAccount.Remove(s);
				XMLService.xmlSerializeShopInfo(StaticResources.TotalSellerAccount);
			});

			BtDeleteAllAccountTapped = new DelegateCommand(() =>
			{
				foreach (var shop in StaticResources.lstShopInfoLogin)
				{
					StaticResources.TotalSellerAccount.Remove(shop);
				}
				StaticResources.lstShopInfoLogin.Clear();
				StaticResources.SelectedShopLogin = null;
				XMLService.xmlSerializeShopInfo(StaticResources.TotalSellerAccount);
			});

			BtAddNewAccountTapped = new DelegateCommand(() =>
			{
				//  Clear cookie 
				// Login
				// Login -> navigate 
				// Lay cookie
				// close

				this.isGrLoginFormShown = Visibility.Visible;
				this.TgGrFormLogin = "Visible";
				Uri loginUri = this.ShopeeLoginUri;
				HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
			});

			wvContentLoadding = new DelegateCommand(() =>
			{
				var httpBaseProtocolFilter = new HttpBaseProtocolFilter();
				var cookieManager = httpBaseProtocolFilter.CookieManager;
				var cc = cookieManager.GetCookies(this.ShopeeLoginUri);
				bool bAuthenticated = false;
				string strCookie = string.Empty;
				if (cc == null)
					return;
				foreach (var cookie in cc)
				{
					LstShortCookie.Add(new ShortCookie
					{
						name = cookie.Name.ToUpper().TrimEnd().TrimStart(),
						value = cookie.Value.TrimEnd().TrimStart(),
						path = cookie.Path.TrimEnd().TrimStart(),
						domain = cookie.Domain.TrimEnd().TrimStart()
					});
					strCookie += cookie.Name + "=" + cookie.Value + ";";
					if (cookie.Name.ToUpper().Contains("SPC_U"))
					{
						if (cookie.Value != "-")
						{
							bAuthenticated = true;
						}
					}
				}
				if (bAuthenticated)
				{
					string LoginAPIUri = "";
					var SPC_CDS = LstShortCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
					if (SPC_CDS != null)
					{
						LoginAPIUri = StaticResources.SellerMainUri + "api/v2/login/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
					}
					HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(LoginAPIUri));
					req.Headers.Add("Cookie", strCookie);
					req.Host = StaticResources.SellerHostUri;
					req.Accept = "application/json, text/plain, */*";
					req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
					req.Referer = StaticResources.SellerReferUri;
					req.Headers.Add("Accept-Encoding", "zip, deflate, br");
					//req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
					try
					{
						var res = (HttpWebResponse)req.GetResponse();
						string resultContent = string.Empty;
						using (var reader = new StreamReader(res.GetResponseStream()))
						{
							resultContent = reader.ReadToEnd();
						};
						if (!string.IsNullOrEmpty(resultContent))
						{
							var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopInfo>(resultContent);
							if (!string.IsNullOrEmpty(result.username))
							{
								if (StaticResources.lstShopInfoLogin == null)
								{
									StaticResources.lstShopInfoLogin = new ObservableCollection<ShopInfoLogin>();
								}
								var tmpShop = StaticResources.TotalSellerAccount.Where(p => p.Shop.shopid == result.shopid && p.ShortRegion == StaticResources.ShortRegion).FirstOrDefault();
								if (tmpShop != null)
								{
									tmpShop.Shop = result;
									tmpShop.lstLoginCookie = LstShortCookie;
									tmpShop.strLoginCookie = strCookie;
									tmpShop.ShopStatus = "Updated";
								}
								else
								{
									tmpShop = new ShopInfoLogin();
									tmpShop.Shop = result;
									tmpShop.lstLoginCookie = LstShortCookie;
									tmpShop.strLoginCookie = strCookie;
									tmpShop.ShortRegion = StaticResources.ShortRegion;
									StaticResources.TotalSellerAccount.Add(tmpShop);
									StaticResources.lstShopInfoLogin.Add(tmpShop);
									tmpShop.ShopStatus = "New";
								}
								StaticResources.DictGuid[tmpShop.Shop.id] = Guid.NewGuid().ToString();
								StaticResources.SelectedShopLogin = tmpShop;
								this.isGrLoginFormShown = Visibility.Collapsed;
								ImageDownloadService.DownloadAvatar(result);
								XMLService.xmlSerializeShopInfo(StaticResources.TotalSellerAccount);
							}
						}
					}
					catch
					{

					}
				}
			});


			ShopeeLoginPageLoading = new DelegateCommand(() =>
			{
				//Call browser
			});
		}

		public Uri ShopeeLoginUri
		{
			get
			{
				return this._shopeeLoginUri;
			}
			set
			{
				SetProperty(ref this._shopeeLoginUri, value);
			}
		}

		public Visibility isGrLoginFormShown
		{
			get
			{
				return this._isGrLoginFormShown;
			}
			set
			{
				SetProperty(ref this._isGrLoginFormShown, value);
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

		public string TgGrFormLogin
		{
			get
			{
				return this._tgGrFormLogin;
			}
			set
			{
				SetProperty(ref this._tgGrFormLogin, value);
			}
		}

		public string TbxHeaderAccount
		{
			get => this._tbxHeaderAccount;
			set => SetProperty(ref this._tbxHeaderAccount, value);
		}
		public string TbxUserName
		{
			get => this._tbxUserName;
			set => SetProperty(ref this._tbxUserName, value);
		}
		public string TbxStatus
		{
			get => this._tbxStatus;
			set => SetProperty(ref this._tbxStatus, value);
		}
		public string TbxDeleteAll
		{
			get => this._tbxDeleteAll;
			set => SetProperty(ref this._tbxDeleteAll, value);
		}
		public string TbxDelete
		{
			get => this._tbxDelete;
			set => SetProperty(ref this._tbxDelete, value);
		}
		public string TbxAddNewAccount
		{
			get => this._tbxAddNewAccount;
			set => SetProperty(ref this._tbxAddNewAccount, value);
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

		public string TbxMessageOpen
		{
			get => this._tbxMessageOpen;
			set => SetProperty(ref this._tbxMessageOpen, value);
		}

		public string TbxOrderOpen
		{
			get => this._tbxOrderOpen;
			set => SetProperty(ref this._tbxOrderOpen, value);
		}

		public ShopInfoLogin SelectedShop
		{
			get => this._selectedShop;
			set
			{
				StaticResources.isOpenChatPage = false;
				StaticResources.isOpenOrderPage = false;
				if (this._selectedShop != value)
				{
					SetProperty(ref this._selectedShop, value);
					StaticResources.SelectedShopLogin = this._selectedShop;
				}
			}
		}

		public string TbxWeekRevenue
		{
			get => this._tbxWeekRevenue;
			set => SetProperty(ref this._tbxWeekRevenue, value);
		}
		public string TbxMonthRevenue
		{
			get => this._tbxMonthRevenue;
			set => SetProperty(ref this._tbxMonthRevenue, value);
		}
		public string TbxTotalRevenue
		{
			get => this._tbxTotalRevenue;
			set => SetProperty(ref this._tbxTotalRevenue, value);
		}

		public string TbxRevenueReport
		{
			get => this._tbxRevenueReport;
			set => SetProperty(ref this._tbxRevenueReport, value);
		}

	}
}
