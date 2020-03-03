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
using Windows.UI.Xaml.Media.Animation;

namespace ShopeeManagement.ViewModels
{
	public class ShopeeProductPageViewModel : ViewModelBase
	{
		public ICommand ShopeeProductPageLoading { get; set; }
		public ICommand ProductChecked { get; set; }
		public ICommand ProductUnChecked { get; set; }
		public ICommand BtPagePrevious { get; set; }
		public ICommand BtPageForward { get; set; }
		public ICommand BtDownloadToLocalDbTapped { get; set; }
		public ICommand BtDownloadAllProductTapped { get; set; }
		public ICommand BtDeleteProductTapped { get; set; }
		public ICommand BtDeleteAllProductTapped { get; set; }
		public ICommand BtBoostProductTapped { get; set; }

		private ObservableCollection<ListProduct> _productList;
		private ObservableCollection<ListProduct> _productListBoosting;
		private PageProductList _pageProductList;

		private bool _isBtPagePreviousEnable;
		private bool _isBtPageForwardEnable;

		private Visibility _isGrLoadingProductShown;

		//private int _tbxChoosingProductCount;
		private int _tbxTotalProduct;
		private int _tbxTotalProductBoosting;
		private int _tbxTotalPage;
		private int _tbxCurrentPage;
		private int _pagesize;

		private string _grLoadingProductTag;
		private string _hdProductList;
		private string _tbxBoostingProduct;
		private string _tbxThumbnail;
		private string _tbxName;
		private string _tbxPrice;
		private string _tbxStatus;
		private string _tbxInStock;
		private string _tbxView;
		private string _tbxLike;
		private string _tbxSold;
		private string _tbxBoost;
		private string _tbxDeletePage;
		private string _tbxDownloadPage;
		private string _tbxPageS;
		private string _tbxDownload;
		private string _tbxDelete;
		private string _tbxTotalProducts;

		public ShopeeProductPageViewModel()
		{
			this.HdProductList = StaticResources.choosingLanguage.ProductPageProductList;
			this.TbxBoostingProduct = StaticResources.choosingLanguage.ProductPageTotalBoostingProduct;
			this.TbxThumbnail = StaticResources.choosingLanguage.ProductPageThumbnail;
			this.TbxName = StaticResources.choosingLanguage.ProductPageName;
			this.TbxPrice = StaticResources.choosingLanguage.ProductPagePrice;
			this.TbxStatus = StaticResources.choosingLanguage.ProductPageStatus;
			this.TbxInStock = StaticResources.choosingLanguage.ProductPageInStock;
			this.TbxView = StaticResources.choosingLanguage.ProductPageView;
			this.TbxLike = StaticResources.choosingLanguage.ProductPageLike;
			this.TbxSold = StaticResources.choosingLanguage.ProductPageSold;
			this.TbxBoost = StaticResources.choosingLanguage.ProductPageBoost;
			this.TbxDeletePage = StaticResources.choosingLanguage.ProductPageDeletePage;
			this.TbxDownloadPage = StaticResources.choosingLanguage.ProductPageDownloadPage;
			this.TbxPageS = StaticResources.choosingLanguage.ProductPagePage;
			this.TbxDelete = StaticResources.choosingLanguage.ProductPageDelete;
			this.TbxDownload = StaticResources.choosingLanguage.ProductPageDownload;
			this.TbxTotalProductS = StaticResources.choosingLanguage.ProductPageTotalProduct;
			ShopeeProductPageLoading = new DelegateCommand(async () =>
			{
				this.ProductList.Clear();
				if (StaticResources.SelectedShopLogin == null)
				{
					if (StaticResources.lstShopInfoLogin.Count() == 0)
					{
						await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningLogin).ShowAsync();
						StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
						StaticResources.isLoginPage = true;
					}
					else
					{
						await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningPick).ShowAsync();
						StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
						StaticResources.isLoginPage = true;
					}
					return;
				}
				this.isBtPageForwardEnable = false;
				this.isBtPagePreviousEnable = false;
				if (StaticResources.SelectedShopLogin != null)
				{
					this.isGrLoadingProductShown = Visibility.Visible;
					this.GrLoadingProductTag = "Visible";
					Utility.getProductPerPage(StaticResources.SelectedShopLogin, new AsyncCallback(_getProductPageCallback), 1);
				}
				checkValidPageIteraction();
			});

			BtDownloadToLocalDbTapped = new DelegateCommand<ListProduct>((s) =>
			{
				if (s.status != 6)
				{
					Utility.getItemSearchDetails(StaticResources.SelectedShopLogin, StaticResources.SelectedShopLogin.Shop.shopid, s.name, s.id, new AsyncCallback(_getProductDetailCallback));
				}
			});

			BtBoostProductTapped = new DelegateCommand<ListProduct>((s) =>
			{
				if (this.TbxTotalProductBoosting <= 5)
				{
					new Thread(async () =>
					{
						if (Utility.boostProductFromLocal(StaticResources.SelectedShopLogin, s.id) == true)
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								s.boost_cool_down_seconds = 14400;
								this.TbxTotalProductBoosting++;
							});
						}

					}).Start();
				}
			});

			BtDeleteProductTapped = new DelegateCommand<ListProduct>(async (s) =>
			{
				var verificationMessage = new MessageDialog(StaticResources.choosingLanguage.GlobalWarningDelete);
				verificationMessage.Commands.Add(new UICommand(StaticResources.choosingLanguage.GlobalWaringYes, (m) =>
				{
					new Thread(async () =>
					{
						var deletingProduct = new DeletingProduct();
						deletingProduct.product_id_list = new List<ulong?>();
						deletingProduct.product_id_list.Add(s.id);
						string strDeletingProduct = Newtonsoft.Json.JsonConvert.SerializeObject(deletingProduct);
						if (Utility.deleteProduct(StaticResources.SelectedShopLogin, strDeletingProduct) == true)
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								this.ProductList.Remove(s);
							});
							if (StaticResources.SelectedShopLogin != null)
							{
								Utility.getProductPerPage(StaticResources.SelectedShopLogin, new AsyncCallback(_getProductPageCallback), 1);
							}
						}
					}).Start();
				}));

				verificationMessage.Commands.Add(new UICommand(StaticResources.choosingLanguage.GlobalWarningNo, (m) =>
				{

				}));

				verificationMessage.DefaultCommandIndex = 0;
				verificationMessage.CancelCommandIndex = 1;
				await verificationMessage.ShowAsync();
			});

			BtDeleteAllProductTapped = new DelegateCommand(async () =>
			{
				var verificationMessage = new MessageDialog(StaticResources.choosingLanguage.GlobalWarningDeleteAll);
				verificationMessage.Commands.Add(new UICommand(StaticResources.choosingLanguage.GlobalWaringYes, (m) =>
				{
					new Thread(async () =>
					{
						var deletingProduct = new DeletingProduct();
						deletingProduct.product_id_list = new List<ulong?>();
						foreach (var product in ProductList)
						{
							deletingProduct.product_id_list.Add(product.id);
						}
						string strDeletingProduct = Newtonsoft.Json.JsonConvert.SerializeObject(deletingProduct);
						if (Utility.deleteProduct(StaticResources.SelectedShopLogin, strDeletingProduct) == true)
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								if (StaticResources.SelectedShopLogin != null)
								{
									Utility.getProductPerPage(StaticResources.SelectedShopLogin, new AsyncCallback(_getProductPageCallback), 1);
								}
							});
						}
					}).Start();
				}));

				verificationMessage.Commands.Add(new UICommand(StaticResources.choosingLanguage.GlobalWarningNo, (m) =>
				{

				}));

				verificationMessage.DefaultCommandIndex = 0;
				verificationMessage.CancelCommandIndex = 1;
				await verificationMessage.ShowAsync();
			});

			BtDownloadAllProductTapped = new DelegateCommand(() =>
			{
				foreach (var item in ProductList)
				{
					if (item.isCanDownload == true)
					{
						Utility.getItemSearchDetails(StaticResources.SelectedShopLogin, StaticResources.SelectedShopLogin.Shop.shopid, item.name, item.id, new AsyncCallback(_getProductDetailCallback));
					}
				}
			});

			BtPagePrevious = new DelegateCommand(() =>
			{
				this.TbxCurrentPage--;
				this.GrLoadingProductTag = "Visible";
				Utility.getProductPerPage(StaticResources.SelectedShopLogin, new AsyncCallback(_getProductPageCallback), this.TbxCurrentPage);
			});

			BtPageForward = new DelegateCommand(() =>
			{
				this.TbxCurrentPage++;
				this.GrLoadingProductTag = "Visible";
				Utility.getProductPerPage(StaticResources.SelectedShopLogin, new AsyncCallback(_getProductPageCallback), this.TbxCurrentPage);
			});
		}

		private void checkValidPageIteraction()
		{
			if (this.TbxTotalPage == 1)
			{
				this.isBtPagePreviousEnable = false;
				this.isBtPageForwardEnable = false;
			}
			else if (this.TbxCurrentPage < this.TbxTotalPage && this.TbxCurrentPage > 1)
			{
				this.isBtPagePreviousEnable = true;
				this.isBtPageForwardEnable = true;
			}
			else if (this.TbxCurrentPage == 1 && this.TbxCurrentPage <= this.TbxTotalPage)
			{
				this.isBtPagePreviousEnable = false;
				this.isBtPageForwardEnable = true;
			}
			else if (this.TbxCurrentPage == this.TbxTotalPage)
			{
				this.isBtPagePreviousEnable = true;
				this.isBtPageForwardEnable = false;
			}
		}

		private async void _getProductPageCallback(IAsyncResult asynchronousResult)
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
					async () =>
					{
						try
						{
							var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resultContent);
							if (dumpJson.error != null && dumpJson.err_message != null)
							{
								if (!string.IsNullOrEmpty(dumpJson.error))
								{
									await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
									async () =>
									{
										StaticResources.SelectedShopLogin.ShopStatus = "Out of date";
										await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningSession).ShowAsync();
										StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
										StaticResources.isLoginPage = true;
									});
								}
								return;
							}
							var dumpJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage2>(resultContent);
							if (dumpJson2.errcode != null && dumpJson2.message != null)
							{
								if (!string.IsNullOrEmpty(dumpJson2.errcode))
								{
									await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
									async () =>
									{
										StaticResources.SelectedShopLogin.ShopStatus = "Out of date";
										await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningSession).ShowAsync();
										StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
										StaticResources.isLoginPage = true;
									});
								}
								return;
							}
						}
						catch
						{
						}
						pageProductList = Newtonsoft.Json.JsonConvert.DeserializeObject<PageProductList>(resultContent);
						ProductList = pageProductList.data.list;
						this.ProductListBoosting.Clear();
						this.TbxTotalProductBoosting = 0;
						foreach (var product in ProductList)
						{
							product.image = product.images[0];
						}

						Utility.getListProductBoost(StaticResources.SelectedShopLogin, _getProductBoostCallback);

						if (this._pageProductList != null)
						{
							this.TbxCurrentPage = pageProductList.data.page_info.page_number;
							this.TbxTotalProduct = pageProductList.data.page_info.total;
							this._pagesize = pageProductList.data.page_info.page_size;
							this.TbxTotalPage = (this.TbxTotalProduct % this._pagesize == 0) ? (this.TbxTotalProduct / this._pagesize) : ((this.TbxTotalProduct / this._pagesize) + 1);
							checkValidPageIteraction();
						}
						this.GrLoadingProductTag = "Collapsed";
					});
				}
			}
			catch (WebException ex)
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					this.GrLoadingProductTag = "Collapsed";
				});
				try
				{
					using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
					{
						var resp = streamReader.ReadToEnd();
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								async () =>
								{
									StaticResources.SelectedShopLogin.ShopStatus = "Out of date";
									await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningSession).ShowAsync();
									StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
									StaticResources.isLoginPage = true;
								});
							}
							return;
						}
						var dumpJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage2>(resp);
						if (dumpJson2.errcode != null && dumpJson2.message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson2.errcode))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								async () =>
								{
									StaticResources.SelectedShopLogin.ShopStatus = "Out of date";
									await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningSession).ShowAsync();
									StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
									StaticResources.isLoginPage = true;
								});
							}
							return;
						}
					}
				}
				catch (Exception)
				{
				}

			}
		}

		private async void _getProductBoostCallback(IAsyncResult asynchronousResult)
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
						var result = Newtonsoft.Json.JsonConvert.DeserializeObject<BoostProductResult>(resultContent);
						this.TbxTotalProductBoosting = result.data.list.Count();

					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async void _getListProductBoostCallback(IAsyncResult asynchronousResult)
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
						var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ListProductBoosted>(resultContent);
						var ListBoosted = result.list;
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
							StaticResources.ChoosingProductFeature.SelfCount++;
							this.ProductList.Where(p => p.id == item.itemid).FirstOrDefault().isCanDownload = false;
							StaticResources.dictCanDownload[item.itemid] = false;
							item.index = StaticResources.TotalChoosingProduct;
							StaticResources.ListProductDetails.Add(item);
						}
					});
					//isGetProduct = true;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}



		public PageProductList pageProductList
		{
			get
			{
				return this._pageProductList;
			}
			set
			{
				SetProperty(ref this._pageProductList, value);
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

		public ObservableCollection<ListProduct> ProductList
		{
			get
			{
				if (this._productList == null)
				{
					this._productList = new ObservableCollection<ListProduct>();
				}
				return this._productList;
			}
			set
			{
				SetProperty(ref this._productList, value);
				if (value != null)
				{
					foreach (var product in this.ProductList)
					{
						if (StaticResources.dictCanDownload.ContainsKey(product.id))
						{
							product.isCanDownload = StaticResources.dictCanDownload[product.id];
						}
						else
						{
							StaticResources.dictCanDownload[product.id] = true;
						}
					}
				}
			}
		}

		public ObservableCollection<ListProduct> ProductListBoosting
		{
			get
			{
				if (this._productListBoosting == null)
				{
					this._productListBoosting = new ObservableCollection<ListProduct>();
				}
				return this._productListBoosting;
			}
			set
			{
				SetProperty(ref this._productListBoosting, value);
			}
		}

		public string GrLoadingProductTag
		{
			get
			{
				return this._grLoadingProductTag;
			}
			set
			{
				SetProperty(ref this._grLoadingProductTag, value);
			}
		}

		public Visibility isGrLoadingProductShown
		{
			get
			{
				return this._isGrLoadingProductShown;
			}
			set
			{
				SetProperty(ref this._isGrLoadingProductShown, value);
			}
		}

		public int TbxTotalProductBoosting
		{
			get
			{
				return this._tbxTotalProductBoosting;
			}
			set
			{
				SetProperty(ref this._tbxTotalProductBoosting, value);
				if (this._tbxTotalProductBoosting >= 5)
				{
					foreach (var item in ProductList)
					{
						item.isCanBoost = false;
					}
				}
				else
				{
					foreach (var item in ProductList)
					{
						if (item.boost_cool_down_seconds == 0)
						{
							item.isCanBoost = true;
						}
					}
				}
			}
		}

		public string HdProductList
		{
			get => this._hdProductList;
			set => SetProperty(ref this._hdProductList, value);
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
		public string TbxStatus
		{
			get => this._tbxStatus;
			set => SetProperty(ref this._tbxStatus, value);
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
		public string TbxSold
		{
			get => this._tbxSold;
			set => SetProperty(ref this._tbxSold, value);
		}
		public string TbxBoost
		{
			get => this._tbxBoost;
			set => SetProperty(ref this._tbxBoost, value);
		}
		public string TbxDeletePage
		{
			get => this._tbxDeletePage;
			set => SetProperty(ref this._tbxDeletePage, value);
		}
		public string TbxDownloadPage
		{
			get => this._tbxDownloadPage;
			set => SetProperty(ref this._tbxDownloadPage, value);
		}
		public string TbxPageS
		{
			get => this._tbxPageS;
			set => SetProperty(ref this._tbxPageS, value);
		}
		public string TbxDownload
		{
			get => this._tbxDownload;
			set => SetProperty(ref this._tbxDownload, value);
		}
		public string TbxDelete
		{
			get => this._tbxDelete;
			set => SetProperty(ref this._tbxDelete, value);
		}
		public string TbxBoostingProduct
		{
			get => this._tbxBoostingProduct;
			set => SetProperty(ref this._tbxBoostingProduct, value);
		}
		public string TbxTotalProductS
		{
			get => this._tbxTotalProducts;
			set => SetProperty(ref this._tbxTotalProducts, value);
		}
	}
}
