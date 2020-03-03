using ShopeeManagement.Base;
using ShopeeManagement.Models;
using ShopeeManagement.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace ShopeeManagement.ViewModels
{
    public class ShopeeOrderPageViewModel : ViewModelBase
    {
		public ICommand wvLoaded { get; set; }
		public ICommand ShopeeOrderPageLoading { get; set; }
		public ICommand wvScriptNotify { get; set; }
		public ICommand BtCancelGrPrinterTapped { get; set; }
		public ICommand BtOpenPrinterTapped { get; set; }

		private string _tgGrPrinter;

		private int _tbxBillingCount = 0;
		public ShopeeOrderPageViewModel()
		{
			ShopeeOrderPageLoading = new DelegateCommand(() =>
			{
				StaticResources.SelectedShopLogin.NewOrderCount = 0;
				StaticResources.OrderFeature.SelfCount = 0;
				var bc = Utility.GetToProcessingBillCount(StaticResources.SelectedShopLogin);
			});

			BtCancelGrPrinterTapped = new DelegateCommand(() =>
			{
				this.TgGrPrinter = "Collapsed";
			});

			BtOpenPrinterTapped = new DelegateCommand(() =>
			{
				this.TgGrPrinter = "Visible";
			});

			wvLoaded = new DelegateCommand<WebView>((s) =>
			{
				if (s != null)
				{
					Uri sale_uri = new Uri(StaticResources.SaleUri);
					HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
					Utility.ClearCookie(filter, sale_uri);
					HttpCookieManager cookieManager = filter.CookieManager;
					try
					{
						foreach (var ck in StaticResources.SelectedShopLogin.lstLoginCookie)
						{
							HttpCookie cookie = new HttpCookie(ck.name, ck.domain[0].Equals('.') ? ck.domain : "." + ck.domain, ck.path);
							cookie.Value = ck.value;
							cookieManager.SetCookie(cookie, false);
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
					new Thread(async () =>
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							s.Navigate(sale_uri);
						});
					}).Start();
				}
			});

			wvScriptNotify = new DelegateCommand<NotifyEventArgs>((s) =>
			{

			});
		}
		public int TbxBillingCount
		{
			get => this._tbxBillingCount;
			set => SetProperty(ref this._tbxBillingCount, value);
		}

		public string TgGrPrinter
		{
			get => this._tgGrPrinter;
			set => SetProperty(ref this._tgGrPrinter, value);
		}
	}
}
