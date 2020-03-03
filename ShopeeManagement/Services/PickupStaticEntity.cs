using ShopeeManagement.Models;
using ShopeeManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ShopeeManagement.Services
{
    public static class PickupStaticEntity
    {
		public static ObservableCollection<MainFeatures> GetMainFeatures()
		{
			var mainFeatures = new ObservableCollection<MainFeatures>();
			mainFeatures.Add(new MainFeatures
			{
				Icon = "\uE71D",
				Title = StaticResources.choosingLanguage.MainPageProductListTitle,
				Tag = "All_Item",
				PageType = typeof(ShopeeProductPage)
			});

			mainFeatures.Add(new MainFeatures
			{
				Icon = "\uE773",
				Title = StaticResources.choosingLanguage.MainPageProductSearchTitle,
				Tag = "All_Item",
				PageType = typeof(ShopeeProductSearch)
			});

			StaticResources.ChoosingProductFeature = new MainFeatures
			{
				Icon = "\uE9D5",
				Title = StaticResources.choosingLanguage.MainPageProductDowloaded,
				Tag = "All_Item",
				PageType = typeof(ShopeeProductDownloadedPage),
			};
			mainFeatures.Add(StaticResources.ChoosingProductFeature);

			StaticResources.MessageFeature = new MainFeatures
			{
				Icon = "\uE8BD",
				Title = StaticResources.choosingLanguage.MainPageMessage,
				Tag = "All_Item",
				PageType = typeof(ShopeeChatPage),
			};
			mainFeatures.Add(StaticResources.MessageFeature);

			StaticResources.OrderFeature = new MainFeatures
			{
				Icon = "\uE7BF",
				Title = StaticResources.choosingLanguage.MainPageOrder,
				Tag = "",
				PageType = typeof(ShopeeOrderPage),
			};
			mainFeatures.Add(StaticResources.OrderFeature);

			mainFeatures.Add(new MainFeatures
			{
				Icon = "\uEF58",
				Title = StaticResources.choosingLanguage.MainPageShopConfig,
				Tag = "",
				PageType = typeof(ShopeeShopConfigPage),
			});

			mainFeatures.Add(new MainFeatures
			{
				Icon = "\uE9D5",
				Title = "New Item",
				Tag = "All_Item",
				PageType = typeof(ShopeeNewItemPage),
			});
			return mainFeatures;
		}

		public static ObservableCollection<MainFeatures> GetMainManagement()
		{
			var mainFeatures = new ObservableCollection<MainFeatures>();
			mainFeatures.Add(new MainFeatures
			{
				Icon = "\uE779",
				Title = StaticResources.choosingLanguage.MainPageAccountManagement,
				Tag = "Acc_Management",
				PageType = typeof(ShopeeLoginPage)
			});

			mainFeatures.Add(new MainFeatures
			{
				Icon = "\uE713",
				Title = StaticResources.choosingLanguage.MainPageShopeeConfig,
				Tag = "All_Item",
				PageType = typeof(ShopeeConfigPage)
			});

			return mainFeatures;
		}

		public static ObservableCollection<LogoPosition> getLogoPosition()
		{
			var logoPosition = new ObservableCollection<LogoPosition>();
			logoPosition.Add(new	LogoPosition
			{
				horizontal = HorizontalAlignment.Left,
				vertical = VerticalAlignment.Top
			});

			logoPosition.Add(new LogoPosition
			{
				horizontal = HorizontalAlignment.Left,
				vertical = VerticalAlignment.Bottom
			});

			logoPosition.Add(new LogoPosition
			{
				horizontal = HorizontalAlignment.Right,
				vertical = VerticalAlignment.Top
			});

			logoPosition.Add(new LogoPosition
			{
				horizontal = HorizontalAlignment.Right,
				vertical = VerticalAlignment.Bottom
			});
			return logoPosition;
		}

		public static ObservableCollection<ShopeeServer> getShopeeServer()
		{
			var server = new ObservableCollection<ShopeeServer>();
			server.Add(new ShopeeServer
			{
				Region = "Việt Nam",
				ShortRegion = "VN",
				ImgUri = "vietnam-flag-xs.png",
				Currency = "VND",
				UriSet = new ApiUri
				{
					MainUri = "https://shopee.vn/",
					SellerMainUri = "https://banhang.shopee.vn/",
					ImgServer = "https://cf.shopee.vn/file/",
					HostUri = "shopee.vn",
					SellerHostUri = "banhang.shopee.vn",
					ReferUri = "https://shopee.vn/",
					SellerReferUri = "https://banhang.shopee.vn/",
					OriginUri = "https://shopee.vn/",
					SellerOriginUri = "https://banhang.shopee.vn/",
					ChatUri = "https://banhang.shopee.vn/webchat/",
					SaleUri = "https://banhang.shopee.vn/portal/sale/",
					ShopConfigUri = "https://banhang.shopee.vn/portal/settings/shop/profile/",
				}
			});

			server.Add(new ShopeeServer
			{
				Region = "Singapore",
				ShortRegion = "SG",
				ImgUri = "singapore-flag-xs.png",
				Currency = "SGD",
				UriSet = new ApiUri
				{
					MainUri = "https://shopee.sg/",
					SellerMainUri = "https://seller.shopee.sg/",
					ImgServer = "https://cf.shopee.sg/file/",
					HostUri = "shopee.sg",
					SellerHostUri = "seller.shopee.sg",
					ReferUri = "https://shopee.sg/",
					SellerReferUri = "https://seller.shopee.sg/",
					OriginUri = "https://shopee.sg/",
					SellerOriginUri = "https://seller.shopee.sg/",
					ChatUri = "https://seller.shopee.sg/webchat/",
					SaleUri = "https://seller.shopee.sg/portal/sale/",
					ShopConfigUri = "https://seller.shopee.sg/portal/settings/shop/profile/",
				}
			});

			//server.Add(new ShopeeServer
			//{
			//	Region = "China",
			//	ShortRegion = "CN",
			//	ImgUri = "china-flag-xs.png",
			//	UriSet = new ApiUri
			//	{
			//		MainUri = "https://shopee.cn/",
			//		SellerMainUri = "https://seller.xiapi.shopee.cn/",
			//		ImgServer = "https://cf.shopee.cn/file/",
			//		HostUri = "shopee.cn",
			//		SellerHostUri = "seller.xiapi.shopee.cn",
			//		ReferUri = "https://shopee.cn/",
			//		SellerReferUri = "https://seller.xiapi.shopee.cn/",
			//		OriginUri = "https://shopee.cn/",
			//		SellerOriginUri = "https://seller.xiapi.shopee.cn/",
			//	}
			//});

			//server.Add(new ShopeeServer
			//{
			//	Region = "Taiwan",
			//	ShortRegion = "TW",
			//	ImgUri = "taiwan-flag-xs.png",
			//	UriSet = new ApiUri
			//	{
			//		MainUri = "https://shopee.tw/",
			//		SellerMainUri = "https://seller.shopee.tw/",
			//		ImgServer = "https://cf.shopee.tw/file/",
			//		HostUri = "shopee.tw",
			//		SellerHostUri = "seller.shopee.tw",
			//		ReferUri = "https://shopee.tw/",
			//		SellerReferUri = "https://seller.shopee.tw/",
			//		OriginUri = "https://shopee.tw/",
			//		SellerOriginUri = "https://seller.shopee.tw/",
			//	}
			//});

			//server.Add(new ShopeeServer
			//{
			//	Region = "Thailand",
			//	ShortRegion = "TH",
			//	ImgUri = "thailand-flag-xs.png",
			//	UriSet = new ApiUri
			//	{
			//		MainUri = "https://shopee.co.th/",
			//		SellerMainUri = "https://seller.shopee.co.th/",
			//		ImgServer = "https://cf.shopee.co.th/file/",
			//		HostUri = "shopee.co.th",
			//		SellerHostUri = "seller.shopee.co.th",
			//		ReferUri = "https://shopee.co.th/",
			//		SellerReferUri = "https://seller.shopee.co.th/",
			//		OriginUri = "https://shopee.co.th/",
			//		SellerOriginUri = "https://seller.shopee.co.th/",
			//	}
			//});

			server.Add(new ShopeeServer
			{
				Region = "Indonesia",
				ShortRegion = "ID",
				ImgUri = "indonesia-flag-xs.png",
				Currency = "IDR",
				UriSet = new ApiUri
				{
					MainUri = "https://shopee.co.id/",
					SellerMainUri = "https://seller.shopee.co.id/",
					ImgServer = "https://cf.shopee.co.id/file/",
					HostUri = "shopee.co.id",
					SellerHostUri = "seller.shopee.co.id",
					ReferUri = "https://shopee.co.id/",
					SellerReferUri = "https://seller.shopee.co.id/",
					OriginUri = "https://shopee.co.id/",
					SellerOriginUri = "https://seller.shopee.co.id/",
					ChatUri = "https://seller.shopee.co.id/webchat/",
					SaleUri = "https://seller.shopee.co.id/portal/sale/",
					ShopConfigUri = "https://seller.shopee.co.id/portal/settings/shop/profile/",
				}
			});

			server.Add(new ShopeeServer
			{
				Region = "Malaysia",
				ShortRegion = "MA",
				ImgUri = "malaysia-flag-xs.png",
				Currency = "MYR",
				UriSet = new ApiUri
				{
					MainUri = "https://shopee.com.my/",
					SellerMainUri = "https://seller.shopee.com.my/",
					ImgServer = "https://cf.shopee.com.my/file/",
					HostUri = "shopee.com.my",
					SellerHostUri = "seller.shopee.com.my",
					ReferUri = "https://shopee.com.my/",
					SellerReferUri = "https://seller.shopee.com.my/",
					OriginUri = "https://shopee.com.my/",
					SellerOriginUri = "https://seller.shopee.com.my/",
					ChatUri = "https://seller.shopee.com.my/webchat",
					SaleUri = "https://seller.shopee.com.my/portal/sale/",
					ShopConfigUri = "https://seller.shopee.com.my/portal/settings/shop/profile/",
				}
			});

			server.Add(new ShopeeServer
			{
				Region = "Philippines",
				ShortRegion = "PH",
				ImgUri = "philippines-flag-xs.png",
				Currency = "PHP",
				UriSet = new ApiUri
				{
					MainUri = "https://shopee.ph/",
					SellerMainUri = "https://seller.shopee.ph/",
					ImgServer = "https://cf.shopee.ph/file/",
					HostUri = "shopee.ph",
					SellerHostUri = "seller.shopee.ph",
					ReferUri = "https://shopee.ph/",
					SellerReferUri = "https://seller.shopee.ph/",
					OriginUri = "https://shopee.ph/",
					SellerOriginUri = "https://seller.shopee.ph/",
					ChatUri = "https://seller.shopee.ph/webchat/",
					SaleUri = "https://seller.shopee.ph/portal/sale/",
					ShopConfigUri = "https://seller.shopee.ph/portal/settings/shop/profile/",
				}
			});

			return server;
		}

		public static ObservableCollection<SortProduct> getSortType()
		{
			var sort = new ObservableCollection<SortProduct>();
			sort.Add(new SortProduct
			{
				sortType = StaticResources.choosingLanguage.SearchPageSortPopular,
				sort = "pop",
				order = "desc",
			});

			sort.Add(new SortProduct
			{
				sortType = StaticResources.choosingLanguage.SearchPageSortCreatedTime,
				sort = "ctime",
				order = "desc",
			});

			sort.Add(new SortProduct
			{
				sortType = StaticResources.choosingLanguage.SearchPageSortHotSale,
				sort = "sales",
				order = "desc",
			});

			sort.Add(new SortProduct
			{
				sortType = StaticResources.choosingLanguage.SearchPageSortPriceDown,
				sort = "price",
				order = "desc",
			});

			sort.Add(new SortProduct
			{
				sortType = StaticResources.choosingLanguage.SearchPageSortPriceUp,
				sort = "price",
				order = "asc",
			});
			return sort;
		}
    }
}
