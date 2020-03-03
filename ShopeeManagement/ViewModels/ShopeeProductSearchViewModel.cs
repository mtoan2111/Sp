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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace ShopeeManagement.ViewModels
{
    public class ShopeeProductSearchViewModel : ViewModelBase
    {
		public ICommand BtCheckShopNameValidate { get; set; }
		public ICommand BtSearchByShopNameTapped { get; set; }
		public ICommand BtDownloadToLocalDbTapped { get; set; }
		public ICommand BtSearchByProductNameTapped { get; set; }
		public ICommand ShopeeProductSearchLoading { get; set; }
		public ICommand BtSearchByCategoryTapped { get; set; }
		public ICommand BtDownloadPageTapped { get; set; }
		public ICommand BtPageForward { get; set; }
		public ICommand BtPagePrevious { get; set; }
		public ICommand CbxSubSubCategoryWheelChanged { get; set; }

		private ObservableCollection<ShopUser> _listShopSearch;
		private ObservableCollection<Item> _listShopItems;
		private ObservableCollection<Categories> _listCategories;
		private ObservableCollection<SubCategory> _listSubCategories;
		private ObservableCollection<SubSubCategory> _listSubSubCategories;
		private ObservableCollection<SortProduct> _listSortType;

		private int _tbxShopNameCount;
		//private int _tbxChoosingProductCount;
		private int _tbxTotalProduct;
		private int _tbxCurrentPage;
		private int _tbxTotalPage;
		private UInt64? _curCatId;

		private Visibility _isTbxShopeNameCountResultShown;
		private Visibility _isSubCategoriesShown;
		private Visibility _isWarningTbxShopNameShown;
		private Visibility _isWarningTbxProductNameShown;
		private Visibility _isCbxShopPickerShown;
		private Visibility _isSubSubCategoriesShown;
		private Visibility _isTbxProductNameInShopShown;
		private Visibility _isTbxProductNameInCategoryShown;

		private bool _isBtPagePreviousEnable;
		private bool _isBtPageForwardEnable;
		private bool _isBtSearchByShopNameEnable;
		private bool _isBtSearchByCategoryEnable;

		private ShopUser _selectedShopSearch;
		private Categories _selectedMainCategory;
		private SubCategory _selectedSubCategory;
		private SubSubCategory _selectedSubSubCategory;
		private SortProduct _selectedSortType;

		private int _searchByShopNameOffset = 0;
		private int _searchByRelevantNameOffset = 0;
		private int _searchByCategoryOffset = 0;
		private int _currentSearch;

		private object _lock = new object();

		private string _tbxThumbnail;
		private string _tbxName;
		private string _tbxPrice;
		private string _tbxOriginPrice;
		private string _tbxInStock;
		private string _tbxView;
		private string _tbxLike;
		private string _tbxDownloadPage;
		private string _tbxDownload;
		private string _tbxProductPerPage;
		private string _tbxTotalProductS;
		private string _tbxInStockS;
		private string _tbxPageS;
		private string _tbxShopName;
		private string _tbxRequire;
		private string _tbxCheck;
		private string _tbxSold;
		private string _tbxCreatedDate;
		private string _tbxResult;
		private string _cbxPlaceHolderShop;
		private string _tbxSearch;
		private string _tbxSearchByShopName;
		private string _tbxProductName;
		private string _tbxSearchByProductName;
		private string _tbxMainCategory;
		private string _cbxPlaceholderMainCategory;
		private string _tbxSubCategory;
		private string _cbxPlaceholderSubCateogry;
		private string _tbxSearchByCategory;
		private string _tbxSortProduct;
		private string _tbProductName;
		private string _tbxSortType;
		private string _cbxPlaceHolderSortDefault;
		private string _tbxProductNameInShop;
		private string _tbProductNameInShop;
		private string _tbProductNameInCategory;
		private string _tbxProductNameInCategory;
		private string _sortBy ="pop";
		private string _sortOrder = "desc";

		public ShopeeProductSearchViewModel()
		{
			this.TbxThumbnail = StaticResources.choosingLanguage.SearchPageThumbnail;
			this.TbxName = StaticResources.choosingLanguage.SearchPageName;
			this.TbxPrice = StaticResources.choosingLanguage.SearchPagePrice;
			this.TbxOriginPrice = StaticResources.choosingLanguage.SearchPageOriginalPrice;
			this.TbxInStock = StaticResources.choosingLanguage.SearchPageInStock;
			this.TbxView = StaticResources.choosingLanguage.SearchPageView;
			this.TbxLike = StaticResources.choosingLanguage.SearchPageLike;
			this.TbxSold = StaticResources.choosingLanguage.SearchPageSold;
			this.TbxDownloadPage = StaticResources.choosingLanguage.SearchPageDownloadPage;
			this.TbxDownload = StaticResources.choosingLanguage.SearchPageDownload;
			this.TbxProductPerPage = StaticResources.choosingLanguage.SearchPageProductPerPage;
			this.TbxTotalProductS = StaticResources.choosingLanguage.SearchPageTotalProduct;
			this.TbxInStockS = StaticResources.choosingLanguage.SearchPageInStockS;
			this.TbxPageS = StaticResources.choosingLanguage.SearchPagePage;
			this.TbxShopName = StaticResources.choosingLanguage.SearchPageShopName;
			this.TbxRequire = StaticResources.choosingLanguage.SearchPageRequired;
			this.TbxCheck = StaticResources.choosingLanguage.SearchPageCheck;
			this.TbxResult = StaticResources.choosingLanguage.SearchPageResult;
			this.CbxPlaceHolderShop = StaticResources.choosingLanguage.SearchPageCbxChooseShop;
			this.TbxSearch = StaticResources.choosingLanguage.SearchPageSearch;
			this.TbxSearchByShopName = StaticResources.choosingLanguage.SearchPageSearchByShopName;
			this.TbxProductName = StaticResources.choosingLanguage.SearchPageProductName;
			this.TbxSearchByProductName = StaticResources.choosingLanguage.SearchPageSearchByProductName;
			this.TbxMainCategory = StaticResources.choosingLanguage.SearchPageMainCategory;
			this.CbxPlaceholderMainCategory = StaticResources.choosingLanguage.SearchPageCbxChooseMainCateogry;
			this.TbxSubCategory = StaticResources.choosingLanguage.SearchPageSubCategory;
			this.CbxPlaceholderSubCateogry = StaticResources.choosingLanguage.SearchPageCbxChooseSubCategory;
			this.TbxSearchByCategory = StaticResources.choosingLanguage.SearchPageSearchByCategory;
			this.TbxCreatedDate = StaticResources.choosingLanguage.SearchPageCreatedDate;
			this.TbxSortProduct = StaticResources.choosingLanguage.SearchPageSortProduct;
			this.TbxSortType = StaticResources.choosingLanguage.SearchPageSortType;
			this.CbxPlaceHolderSortDefault = StaticResources.choosingLanguage.SearchPageSortDefault;
			this.TbxProductNameInShop = StaticResources.choosingLanguage.SearchPageProductInShop;
			this.TbxProductNameInCategory = StaticResources.choosingLanguage.SearchPageProductInCategory;


			this.isTbxShopeNameCountResultShown = Visibility.Collapsed;
			//this.TbxChoosingProductCount = StaticResources.TotalChoosingProduct;
			this.TbxCurrentPage = 1;
			this.isSubCategoriesShown = Visibility.Collapsed;
			this.isSubSubCategoriesShown = Visibility.Collapsed;
			this.isCbxShopPickerShown = Visibility.Collapsed;
			this.isWarningTbxProductNameShown = Visibility.Collapsed;
			this.isWarningTbxShopNameShown = Visibility.Collapsed;
			this.isTbxProductNameInShopShown = Visibility.Collapsed;
			this.isTbxProductNameInCategoryShown = Visibility.Collapsed;
			this.ListSortType = PickupStaticEntity.getSortType();
			if (StaticResources.ListCategories.Count() == 0)
			{
				if (StaticResources.SelectedShopLogin != null)
				{
					Utility.getCategories(StaticResources.SelectedShopLogin, new AsyncCallback(_getCategoriesCallback));
				}
			}
			else
			{
				this.ListCategories = StaticResources.ListCategories;
			}
			ShopeeProductSearchLoading = new DelegateCommand(async () =>
			{
				//if (StaticResources.SelectedShopLogin == null)
				//{
				//	if (StaticResources.lstShopInfoLogin.Count() == 0)
				//	{
				//		await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningLogin).ShowAsync();
				//		StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
				//		StaticResources.isLoginPage = true;
				//	}
				//	else
				//	{
				//		await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningPick).ShowAsync();
				//	}
				//	return;
				//}
				//this.TbxChoosingProductCount = StaticResources.TotalChoosingProduct;
				foreach (var item in this.ListShopItems)
				{
					if (StaticResources.dictCanDownload.ContainsKey(item.itemid))
					{
						item.isCanDownload = StaticResources.dictCanDownload[item.itemid];
					}
					else
					{
						item.isCanDownload = true;
						StaticResources.dictCanDownload[item.itemid] = true;
					}
				}
			});

			BtCheckShopNameValidate = new DelegateCommand<TextBox>((s) =>
			{
				if (!string.IsNullOrEmpty(s.Text))
				{
					Utility.getShopID(s.Text, new AsyncCallback(_getShopIDCallback));
					this.isWarningTbxShopNameShown = Visibility.Collapsed;
				}
				else
				{
					this.isWarningTbxShopNameShown = Visibility.Visible;
				}
			});

			BtPageForward = new DelegateCommand<TextBox>((s) =>
			{
				this.TbxCurrentPage++;
				switch (this._currentSearch)
				{
					case 1:
						this._searchByShopNameOffset += 30;
						if (string.IsNullOrEmpty(this.TbProductNameInShop))
						{
							Utility.getShopItems(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", new AsyncCallback(_getShopItemsCallback), this._sortBy, this._sortOrder);
						}
						else
						{
							Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy, this._sortOrder);
						}
						break;
					case 2:
						this._searchByRelevantNameOffset += 30;
						Utility.getRelevantItems(StaticResources.SelectedShopLogin, s.Text, this.TbxCurrentPage, this._searchByRelevantNameOffset, new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
						break;
					case 3:
						this._searchByCategoryOffset += 30;
						if (string.IsNullOrEmpty(this.TbProductNameInCategory))
						{
							Utility.getShopItems(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByCategoryOffset, "search", new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
						}
						else
						{
							Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByShopNameOffset, "search", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
						}
						break;
					default:
						break;
				};
			});

			BtPagePrevious = new DelegateCommand<TextBox>((s) =>
			{
				this.TbxCurrentPage--;
				switch (this._currentSearch)
				{
					case 1:
						this._searchByShopNameOffset -= 30;
						if (string.IsNullOrEmpty(this.TbProductNameInShop))
						{
							Utility.getShopItems(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", new AsyncCallback(_getShopItemsCallback), this._sortBy, this._sortOrder);
						}
						else
						{
							Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy, this._sortOrder);
						}
						break;
					case 2:
						this._searchByRelevantNameOffset -= 30;
						Utility.getRelevantItems(StaticResources.SelectedShopLogin, s.Text, this.TbxCurrentPage, this._searchByRelevantNameOffset, new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
						break;
					case 3:
						this._searchByCategoryOffset -= 30;
						if (string.IsNullOrEmpty(this.TbProductNameInCategory))
						{
							Utility.getShopItems(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByCategoryOffset, "search", new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
						}
						else
						{
							Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByShopNameOffset, "search", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
						}
						break;
					default:
						break;
				};
			});

			BtDownloadPageTapped = new DelegateCommand(() =>
			{
				foreach(var item in ListShopItems)
				{
					if (item.isCanDownload == true)
					{
						Utility.getItemSearchDetails(StaticResources.SelectedShopLogin, item.shopid, item.name, item.itemid, new AsyncCallback(_getProductDetailCallback));
						item.isCanDownload = false;
					}
				}
			});

			BtSearchByShopNameTapped = new DelegateCommand(() =>
			{
				if (this.SelectedShopSearch != null)
				{
					this._searchByShopNameOffset = 0;
					this.TbxCurrentPage = 1;
					if (string.IsNullOrEmpty(this.TbProductNameInShop))
					{
						Utility.getShopItems(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", new AsyncCallback(_getShopItemsCallback), this._sortBy, this._sortOrder);
					}
					else
					{
						Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy, this._sortOrder);
					}
					this._currentSearch = 1;
				}
			});

			BtDownloadToLocalDbTapped = new DelegateCommand<Item>((s) =>
			{
				Utility.getItemSearchDetails(StaticResources.SelectedShopLogin, s.shopid, s.name, s.itemid , new AsyncCallback(_getProductDetailCallback));
				s.isCanDownload = false;
			});

			BtSearchByCategoryTapped = new DelegateCommand(() =>
			{
				this._searchByCategoryOffset = 0;
				this.TbxCurrentPage = 1;
				if (string.IsNullOrEmpty(this.TbProductNameInCategory))
				{
					Utility.getShopItems(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByCategoryOffset, "search", new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
				}
				else
				{
					Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByShopNameOffset, "search", this.TbProductNameInCategory, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
				}
				this._currentSearch = 3;
			});

			BtSearchByProductNameTapped = new DelegateCommand<TextBox>((s) =>
			{
				if (!string.IsNullOrEmpty(s.Text))
				{
					this.TbxCurrentPage = 1;
					this._searchByRelevantNameOffset = 0;
					Utility.getRelevantItems(StaticResources.SelectedShopLogin, s.Text, 0, this._searchByRelevantNameOffset, new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
					this._currentSearch = 2;
					this.isWarningTbxProductNameShown = Visibility.Collapsed;
				}
				else
				{
					this.isWarningTbxProductNameShown = Visibility.Visible;
				}
			});
		}

		private async void _getRelevantItemInShopCallBack(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
				string resultContent = string.Empty;
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				};
				if (!string.IsNullOrEmpty(resultContent))
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{

						var totalItemDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopItems>(resultContent);
						this.TbxTotalProduct = totalItemDetails.total_count;
						this.TbxTotalPage = (this.TbxTotalProduct % 30 == 0) ? (this.TbxTotalProduct / 30) : ((this.TbxTotalProduct / 30) + 1);
						checkValidPageIteraction();
						ListShopItems = totalItemDetails.items;
						foreach (var shop in ListShopItems)
						{
							if (StaticResources.dictCanDownload.ContainsKey(shop.itemid))
							{
								shop.isCanDownload = StaticResources.dictCanDownload[shop.itemid];
							}
							else
							{
								StaticResources.dictCanDownload[shop.itemid] = true;
							}
						}
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async void _getShopIDCallback(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
				string resultContent = string.Empty;
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				};
				if (!string.IsNullOrEmpty(resultContent))
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						ListShopSearch = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopSearchInfo>(resultContent).data.users;
						foreach(var shop in ListShopSearch)
						{
							shop.portrait = "https://cf.shopee.vn/file/" + shop.portrait;
						}
						if (ListShopSearch != null && ListShopSearch.Count > 0)
						{
							this.TbxShopNameCount = ListShopSearch.Count();
							this.isTbxShopeNameCountResultShown = Visibility.Visible;
							this.isCbxShopPickerShown = Visibility.Visible;
						}
						else
						{
							this.TbxShopNameCount = 0;
							this.isTbxShopeNameCountResultShown = Visibility.Visible;
							this.isCbxShopPickerShown = Visibility.Collapsed;
							this.isBtSearchByShopNameEnable = false;
						}
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async void _getShopItemsCallback(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
				string resultContent = string.Empty;
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				};
				if (!string.IsNullOrEmpty(resultContent))
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{

						var totalItemDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopItems>(resultContent);
						this.TbxTotalProduct = totalItemDetails.total_count;
						this.TbxTotalPage = (this.TbxTotalProduct % 30 == 0) ? (this.TbxTotalProduct / 30) : ((this.TbxTotalProduct / 30) + 1);
						checkValidPageIteraction();
						ListShopItems = totalItemDetails.items;
						foreach (var shop in ListShopItems)
						{
							if (StaticResources.dictCanDownload.ContainsKey(shop.itemid))
							{
								shop.isCanDownload = StaticResources.dictCanDownload[shop.itemid];
							}
							else
							{
								StaticResources.dictCanDownload[shop.itemid] = true;
							}
						}
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async void _getCategoriesCallback(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
				string resultContent = string.Empty;
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				};
				if (!string.IsNullOrEmpty(resultContent))
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						StaticResources.ListCategories = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Categories>>(resultContent);
						this.ListCategories = StaticResources.ListCategories;
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async void _getProductDetailCallback(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

			try
			{
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
				string resultContent = string.Empty;
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				};
				if (!string.IsNullOrEmpty(resultContent))
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						var item = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemSearchDetails>(resultContent).item;
						if (item != null)
						{
							new Thread(() =>
							{
								ImageDownloadService.Download(item);
							}).Start();
							StaticResources.TotalChoosingProduct++;
							item.index = StaticResources.TotalChoosingProduct;
							StaticResources.ListProductDetails.Add(item);
							StaticResources.ChoosingProductFeature.SelfCount++;
							this.ListShopItems.Where(p => p.itemid == item.itemid).FirstOrDefault().isCanDownload = false;
							StaticResources.dictCanDownload[item.itemid] = false;
						}
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private void checkValidPageIteraction()
		{
			if (this.TbxCurrentPage <= this.TbxTotalPage && this.TbxCurrentPage > 1)
			{
				this.isBtPagePreviousEnable = true;
				this.isBtPageForwardEnable = true;
			}
			else if (this.TbxCurrentPage == 1 && this.TbxCurrentPage < this.TbxTotalPage)
			{
				this.isBtPagePreviousEnable = false;
				this.isBtPageForwardEnable = true;
			}
			else if (this.TbxCurrentPage >= this.TbxTotalPage)
			{
				this.isBtPagePreviousEnable = true;
				this.isBtPageForwardEnable = false;
			}
		}

		public Visibility isTbxShopeNameCountResultShown
		{
			get
			{
				return this._isTbxShopeNameCountResultShown;
			}
			set
			{
				SetProperty(ref this._isTbxShopeNameCountResultShown, value);
			}
		}

		public int TbxShopNameCount
		{
			get
			{
				return this._tbxShopNameCount;
			}
			set
			{
				SetProperty(ref this._tbxShopNameCount, value);
			}
		}

		public ObservableCollection<ShopUser> ListShopSearch
		{
			get
			{
				if (this._listShopSearch == null)
				{
					this._listShopSearch = new ObservableCollection<ShopUser>();
				}
				return this._listShopSearch;
			}
			set
			{
				SetProperty(ref this._listShopSearch, value);
			}
		}

		public ObservableCollection<Item> ListShopItems
		{
			get
			{
				if (this._listShopItems == null)
				{
					this._listShopItems = new ObservableCollection<Item>();
				}
				return this._listShopItems;
			}
			set
			{
				SetProperty(ref this._listShopItems, value);
			}
		}

		public int TbxTotalProduct
		{
			get
			{
				return this._tbxTotalProduct;
			}
			set
			{
				SetProperty(ref this._tbxTotalProduct, value);
			}
		}

		public ShopUser SelectedShopSearch
		{
			get
			{
				return this._selectedShopSearch;
			}
			set
			{
				SetProperty(ref this._selectedShopSearch, value);
				if (this._selectedShopSearch != null)
				{
					this.isTbxProductNameInShopShown = Visibility.Visible;
					this.isBtSearchByShopNameEnable = true;
				}
				else
				{
					this.isTbxProductNameInShopShown = Visibility.Collapsed;
				}
			}
		}

		//public int TbxChoosingProductCount
		//{
		//	get
		//	{
		//		return this._tbxChoosingProductCount;
		//	}
		//	set
		//	{
		//		SetProperty(ref this._tbxChoosingProductCount, value);
		//	}
		//}

		public int TbxCurrentPage
		{
			get
			{
				return this._tbxCurrentPage;
			}
			set
			{
				SetProperty(ref this._tbxCurrentPage, value);
			}
		}

		public int TbxTotalPage
		{
			get
			{
				return this._tbxTotalPage;
			}
			set
			{
				SetProperty(ref this._tbxTotalPage, value);
			}
		}

		public bool isBtPagePreviousEnable
		{
			get
			{
				return this._isBtPagePreviousEnable;
			}
			set
			{
				SetProperty(ref this._isBtPagePreviousEnable, value);
			}
		}
		public bool isBtPageForwardEnable
		{
			get
			{
				return this._isBtPageForwardEnable;
			}
			set
			{
				SetProperty(ref this._isBtPageForwardEnable, value);
			}
		}

		public ObservableCollection<Categories> ListCategories
		{
			get
			{
				if (this._listCategories == null)
				{
					this._listCategories = new ObservableCollection<Categories>();
				}
				return this._listCategories;
			}
			set
			{
				SetProperty(ref this._listCategories, value);
			}
		}
		public ObservableCollection<SubCategory> ListSubCategories
		{
			get
			{
				if (this._listSubCategories == null)
				{
					this._listSubCategories = new ObservableCollection<SubCategory>();
				}
				return this._listSubCategories;
			}
			set
			{
				SetProperty(ref this._listSubCategories, value);
			}
		}

		public ObservableCollection<SubSubCategory> ListSubSubCategories
		{
			get
			{
				if (this._listSubSubCategories == null)
				{
					this._listSubSubCategories = new ObservableCollection<SubSubCategory>();
				}
				return this._listSubSubCategories;
			}
			set
			{
				SetProperty(ref this._listSubSubCategories, value);
			}
		}

		public Categories SelectedMainCategory
		{
			get
			{
				return this._selectedMainCategory;
			}
			set
			{
				if (this._selectedMainCategory != value)
				{
					SetProperty(ref this._selectedMainCategory, value);
					if (this._selectedMainCategory != null)
					{
						this.ListSubCategories = this._selectedMainCategory.sub;
						this.isSubCategoriesShown = Visibility.Visible;
						this.isTbxProductNameInCategoryShown = Visibility.Visible;
						this.isSubSubCategoriesShown = Visibility.Collapsed;
						this.isBtSearchByCategoryEnable = true;
						this._curCatId = this._selectedMainCategory.main.catid;
						this.ListSubSubCategories = null;
					}
					else
					{
						this.isSubCategoriesShown = Visibility.Collapsed;
						this.isSubSubCategoriesShown = Visibility.Collapsed;
						this.isTbxProductNameInCategoryShown = Visibility.Collapsed;
						this.isBtSearchByCategoryEnable = false;
					}
				}
			}
		}

		public SubCategory SelectedSubCategory
		{
			get
			{
				return this._selectedSubCategory;
			}
			set
			{
				if (this._selectedSubCategory != value)
				{
					SetProperty(ref this._selectedSubCategory, value);
					if (this._selectedSubCategory != null)
					{
						this.ListSubSubCategories = this.SelectedSubCategory.sub_sub;
						this.isSubSubCategoriesShown = Visibility.Visible;
						this._curCatId = this._selectedSubCategory.catid;
					}
				}
			}
		}

		public SubSubCategory SelectedSubSubCategory
		{
			get
			{
				return this._selectedSubSubCategory;
			}
			set
			{
				if (this._selectedSubSubCategory != value)
				{
					SetProperty(ref this._selectedSubSubCategory, value);
					this._curCatId = SelectedSubSubCategory.catid;
				}
			}
		}

		public Visibility isSubCategoriesShown
		{
			get
			{
				return this._isSubCategoriesShown;
			}
			set
			{
				SetProperty(ref this._isSubCategoriesShown, value);
			}
		}

		public bool isBtSearchByShopNameEnable
		{
			get
			{
				return this._isBtSearchByShopNameEnable;
			}
			set
			{
				SetProperty(ref this._isBtSearchByShopNameEnable, value);
			}
		}

		public Visibility isWarningTbxShopNameShown
		{
			get
			{
				return this._isWarningTbxShopNameShown;
			}
			set
			{
				SetProperty(ref this._isWarningTbxShopNameShown, value);
			}
		}

		public Visibility isWarningTbxProductNameShown
		{
			get
			{
				return this._isWarningTbxProductNameShown;
			}
			set
			{
				SetProperty(ref this._isWarningTbxProductNameShown, value);
			}
		}

		public Visibility isCbxShopPickerShown
		{
			get
			{
				return this._isCbxShopPickerShown;
			}
			set
			{
				SetProperty(ref this._isCbxShopPickerShown, value);
			}
		}

		public bool isBtSearchByCategoryEnable
		{
			get
			{
				return this._isBtSearchByCategoryEnable;
			}
			set
			{
				SetProperty(ref this._isBtSearchByCategoryEnable, value);
			}
		}

		public string TbxThumbnail
		{
			get => this._tbxThumbnail;
			set => SetProperty(ref this._tbxThumbnail, value);
		}
		public string TbxName
		{
			get => this._tbxName;
			set => SetProperty(ref this._tbxName, value);
		}
		public string TbxPrice
		{
			get => this._tbxPrice;
			set => SetProperty(ref this._tbxPrice, value);
		}
		public string TbxOriginPrice
		{
			get => this._tbxOriginPrice;
			set => SetProperty(ref this._tbxOriginPrice, value);
		}
		public string TbxInStock
		{
			get => this._tbxInStock;
			set => SetProperty(ref this._tbxInStock, value);
		}
		public string TbxView
		{
			get => this._tbxView;
			set => SetProperty(ref this._tbxView, value);
		}
		public string TbxLike
		{
			get => this._tbxLike;
			set => SetProperty(ref this._tbxLike, value);
		}
		public string TbxDownloadPage
		{
			get => this._tbxDownloadPage;
			set => SetProperty(ref this._tbxDownloadPage, value);
		}
		public string TbxDownload
		{
			get => this._tbxDownload;
			set => SetProperty(ref this._tbxDownload, value);
		}
		public string TbxProductPerPage
		{
			get => this._tbxProductPerPage;
			set => SetProperty(ref this._tbxProductPerPage, value);
		}
		public string TbxTotalProductS
		{
			get => this._tbxTotalProductS;
			set => SetProperty(ref this._tbxTotalProductS, value);
		}
		public string TbxInStockS
		{
			get => this._tbxInStockS;
			set => SetProperty(ref this._tbxInStockS, value);
		}
		public string TbxPageS
		{
			get => this._tbxPageS;
			set => SetProperty(ref this._tbxPageS, value);
		}
		public string TbxShopName
		{
			get => this._tbxShopName;
			set => SetProperty(ref this._tbxShopName, value);
		}
		public string TbxRequire
		{
			get => this._tbxRequire;
			set => SetProperty(ref this._tbxRequire, value);
		}
		public string TbxCheck
		{
			get => this._tbxCheck;
			set => SetProperty(ref this._tbxCheck, value);
		}
		public string TbxResult
		{
			get => this._tbxResult;
			set => SetProperty(ref this._tbxResult, value);
		}
		public string CbxPlaceHolderShop
		{
			get => this._cbxPlaceHolderShop;
			set => SetProperty(ref this._cbxPlaceHolderShop, value);
		}
		public string TbxSearch
		{
			get => this._tbxSearch;
			set => SetProperty(ref this._tbxSearch, value);
		}
		public string TbxSearchByShopName
		{
			get => this._tbxSearchByShopName;
			set => SetProperty(ref this._tbxSearchByShopName, value);
		}
		public string TbxProductName
		{
			get => this._tbxProductName;
			set => SetProperty(ref this._tbxProductName, value);
		}
		public string TbxSearchByProductName
		{
			get => this._tbxSearchByProductName;
			set => SetProperty(ref this._tbxSearchByProductName, value);
		}
		public string TbxMainCategory
		{
			get => this._tbxMainCategory;
			set => SetProperty(ref this._tbxMainCategory, value);
		}
		public string CbxPlaceholderMainCategory
		{
			get => this._cbxPlaceholderMainCategory;
			set => SetProperty(ref this._cbxPlaceholderMainCategory, value);
		}
		public string TbxSubCategory
		{
			get => this._tbxSubCategory;
			set => SetProperty(ref this._tbxSubCategory, value);
		}
		public string CbxPlaceholderSubCateogry
		{
			get => this._cbxPlaceholderSubCateogry;
			set => SetProperty(ref this._cbxPlaceholderSubCateogry, value);
		}
		public string TbxSearchByCategory
		{
			get => this._tbxSearchByCategory;
			set => SetProperty(ref this._tbxSearchByCategory, value);
		}

		public string TbxSold
		{
			get => this._tbxSold;
			set => SetProperty(ref this._tbxSold, value);
		}

		public string TbxCreatedDate
		{
			get => this._tbxCreatedDate;
			set => SetProperty(ref this._tbxCreatedDate, value);
		}

		public string TbxSortProduct
		{
			get => this._tbxSortProduct;
			set => SetProperty(ref this._tbxSortProduct, value);
		}

		public ObservableCollection<SortProduct> ListSortType
		{
			get
			{
				if (this._listSortType == null)
				{
					this._listSortType = new ObservableCollection<SortProduct>();
				}
				return this._listSortType;
			}
			set
			{
				SetProperty(ref this._listSortType, value);
			}
		}

		public SortProduct SelectedSortType
		{
			get => this._selectedSortType;
			set
			{
				if (this._selectedSortType != value)
				{
					SetProperty(ref this._selectedSortType, value);
					this._sortBy = this._selectedSortType.sort;
					this._sortOrder = this._selectedSortType.order;
					switch (this._currentSearch)
					{
						case 1:
							if (string.IsNullOrEmpty(TbProductNameInShop))
							{
								Utility.getShopItems(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", new AsyncCallback(_getShopItemsCallback), this._sortBy, this._sortOrder);
							}
							else
							{
								Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, this.SelectedShopSearch.username, this.SelectedShopSearch.shopid, this._searchByShopNameOffset, "shop", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy, this._sortOrder);
							}
							break;
						case 2:
							Utility.getRelevantItems(StaticResources.SelectedShopLogin, TbProductName, this.TbxCurrentPage, this._searchByRelevantNameOffset, new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
							break;
						case 3:
							if (string.IsNullOrEmpty(TbProductNameInCategory))
							{
								Utility.getShopItems(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByCategoryOffset, "search", new AsyncCallback(_getShopItemsCallback), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
							}
							else
							{
								Utility.getRelevantItemsInShop(StaticResources.SelectedShopLogin, "", this._curCatId, this._searchByShopNameOffset, "search", this.TbProductNameInShop, new AsyncCallback(_getRelevantItemInShopCallBack), this._sortBy == "pop" ? "relevancy" : this._sortBy, this._sortOrder);
							}
							break;
						default:
							break;
					};
				}
			}
		}

		public string TbProductName
		{
			get => this._tbProductName;
			set => SetProperty(ref this._tbProductName, value);
		}

		public string TbxSortType
		{
			get => this._tbxSortType;
			set => SetProperty(ref this._tbxSortType, value);
		}

		public string CbxPlaceHolderSortDefault
		{
			get => this._cbxPlaceHolderSortDefault;
			set => SetProperty(ref this._cbxPlaceHolderSortDefault, value);
		}

		public Visibility isSubSubCategoriesShown
		{
			get => this._isSubSubCategoriesShown;
			set => SetProperty(ref this._isSubSubCategoriesShown, value);
		}

		public string TbxProductNameInShop
		{
			get => this._tbxProductNameInShop;
			set
			{
				SetProperty(ref this._tbxProductNameInShop, value);
			}
		}
		public string TbProductNameInShop
		{
			get => this._tbProductNameInShop;
			set
			{
				if (this._tbProductNameInShop != value)
				{
					SetProperty(ref this._tbProductNameInShop, value);
					this._searchByShopNameOffset = 0;
				}
			}
		}

		public Visibility isTbxProductNameInShopShown
		{
			get => this._isTbxProductNameInShopShown;
			set => SetProperty(ref this._isTbxProductNameInShopShown, value);
		}

		public string TbProductNameInCategory
		{
			get => this._tbProductNameInCategory;
			set
			{
				if (this._tbProductNameInCategory != value)
				{
					SetProperty(ref this._tbProductNameInCategory, value);
					this._searchByCategoryOffset = 0;
				}
			}
		}

		public string TbxProductNameInCategory
		{
			get => this._tbxProductNameInCategory;
			set => SetProperty(ref this._tbxProductNameInCategory, value);
		}

		public Visibility isTbxProductNameInCategoryShown
		{
			get => this._isTbxProductNameInCategoryShown;
			set => SetProperty(ref this._isTbxProductNameInCategoryShown, value);
		}
	}
}
