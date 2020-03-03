using ShopeeManagement.Base;
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
	public class ShopeeShopConfigPageViewModel : ViewModelBase
	{
		public ICommand ShopeeShopConfigPageLoading { get; set; }
		public ICommand wvLoaded { get; set; }
		public ShopeeShopConfigPageViewModel()
		{

			ShopeeShopConfigPageLoading = new DelegateCommand(() =>
			{
			});

			wvLoaded = new DelegateCommand<WebView>((s) =>
			{
				if (s != null)
				{
					Uri config_uri = new Uri(StaticResources.ShopConfigUri);
					HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
					Utility.ClearCookie(filter, config_uri);
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
							s.Navigate(config_uri);
						});
					}).Start();
				}
			});
		}
	}
}
