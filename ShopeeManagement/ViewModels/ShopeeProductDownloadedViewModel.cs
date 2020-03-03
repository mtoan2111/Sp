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
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using System.Drawing;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.AccessCache;

namespace ShopeeManagement.ViewModels
{
	public class ShopeeProductDownloadedViewModel : ViewModelBase
	{
		public ICommand BtEditProductTapped { get; set; }
		public ICommand BtUploadProductTapped { get; set; }
		public ICommand BtDeleteProductTapped { get; set; }
		public ICommand BtUploadAllItemTapped { get; set; }
		public ICommand BtDeleteAllProductTapped { get; set; }
		public ICommand BtApplyTextPreProcessTapped { get; set; }
		public ICommand ShopeeProductDownloadedPageLoading { get; set; }
		public ICommand BtUndoAllItemTapped { get; set; }
		public ICommand BtSaveFileToLocalTapped { get; set; }
		public ICommand BtLoadFileFromLocalTapped { get; set; }
		public ICommand BtDeleteAllProductErrorTapped { get; set; }
		public ICommand BtDeleteProductErrorTapped { get; set; }
		public ICommand BtImageProcessingTapped { get; set; }
		public ICommand BtAddLogoImageTapped { get; set; }
		public ICommand BtDeleteLogoImageTapped { get; set; }
		public ICommand DragImageOverHandler { get; set; }
		public ICommand DropImageHandler { get; set; }
		public ICommand MovableManipulationStarted { get; set; }
		public ICommand MovableManipulationDelta { get; set; }
		public ICommand MovableManipulationCompleted { get; set; }
		public ICommand BrLogoImageLoaded { get; set; }
		public ICommand ScaleLogoImageHandle { get; set; }
		public ICommand BrTransformLoaded { get; set; }
		public ICommand BtAddNewFrameImageTapped { get; set; }
		public ICommand BtDeleteFrameImageTapped { get; set; }

		private ObservableCollection<ShopInfoLogin> _listShop;
		private ObservableCollection<ItemSearch> _listItemClone;
		private ObservableCollection<ItemSearch> _listItem;
		private ObservableCollection<ItemSearch> _listItemError;
		private ShopInfoLogin _selectedShop;
		public ObservableCollection<LogoPosition> ListLogoPosition { get; set; }
		public ObservableCollection<ObservableCollection<ItemSearch>> ListChangeStack { get; set; }

		private LogoPosition _logoPostion;
		private object _lockObj = new object();
		private event EventHandler _evUpdateDone;
		private NewItemImages _frameImg;
		private NewItemImages _logoImg;
		private Border _brLogoImage;
		private CompositeTransform _brTransform;

		private Visibility _isGrUploadingProductShown;

		private bool _insertLogoOn = false;

		private int _tbxCurrentProgress;
		private int _tbxTotalUploadingProduct;
		private int _tbxTotalProductError;
		private int _tbxLimitation;
		private int _tbxCurrentProduct;
		private int _tbxRemainingProduct;
		private int _coreCPU = 4;
		private int _funcScore = 0;
		private int _limit = 1000;
		private int _cProduct = 0;

		private string _tbxHeaderUploading;
		private string _tbxThumbnail;
		private string _tbxName;
		private string _tbxDescription;
		private string _tbxPrice;
		private string _tbxDeleteAll;
		private string _tbxUpload;
		private string _tbxDelete;
		private string _tbxError;
		private string _tbxErrorReason;
		private string _tbxSave;
		private string _tbxLoad;
		private string _tbxUploadTo;
		private string _cbxChooseShop;
		private string _tbxUploadAll;
		private string _tbxExampleImg;
		private string _tbxLogoPosition;
		private string _tbxImgSection;
		private string _tbxInsertBegining;
		private string _tbxInsertEnding;
		private string _tbxReplaceFrom;
		private string _tbxReplaceTo;
		private string _tbxNameSection;
		private string _tbxDescriptionSection;
		private string _tbxPriceIncrease;
		private string _tbxPriceDecrease;
		private string _tbxPriceSection;
		private string _tbxApplyAll;
		private string _tbxUndoAll;
		private string _tbxInsertLogo;
		private string _tbxImgResolution;
		private string _tbxFrameResolution;
		private string _tbxImg;
		private string _tbxFrame;
		private string _tbxImgDrag;
		private string _tbxImgScale;
		private string _grUploadingProductTag;
		private string _grApplyProductTag;
		private string _tbNameInsertFromBegining;
		private string _tbNameInsertToEnding;
		private string _tbNameReplaceFrom;
		private string _tbNameReplaceTo;
		private string _tbDesInsertFromBegining;
		private string _tbDesInsertToEnding;
		private string _tbDesReplaceFrom;
		private string _tbDesReplaceTo;
		private string _tbIncreasePrice;
		private string _tbDecreasePrice;
		private string _tbxCurrentProductT;
		private string _tbxLimitationT;
		private string _tbxRemainingProductT;


		public ShopeeProductDownloadedViewModel()
		{
			this.TbxHeaderUploading = StaticResources.choosingLanguage.DownloadedPageUploading;
			this.TbxThumbnail = StaticResources.choosingLanguage.DownloadedPageThumbnail;
			this.TbxName = StaticResources.choosingLanguage.DownloadedPageName;
			this.TbxDescription = StaticResources.choosingLanguage.DownloadedPageDescription;
			this.TbxPrice = StaticResources.choosingLanguage.DownloadedPagePrice;
			this.TbxDeleteAll = StaticResources.choosingLanguage.DownloadedPageDeletePage;
			this.TbxUpload = StaticResources.choosingLanguage.DownloadedPageUpload;
			this.TbxDelete = StaticResources.choosingLanguage.DownloadedPageDelete;
			this.TbxError = StaticResources.choosingLanguage.DownloadedPageError;
			this.TbxErrorReason = StaticResources.choosingLanguage.DownloadedErrorReason;
			this.TbxSave = StaticResources.choosingLanguage.DownloadedPageSave;
			this.TbxLoad = StaticResources.choosingLanguage.DownloadedPageLoad;
			this.TbxUploadTo = StaticResources.choosingLanguage.DownloadedPageUploadTo;
			this.CbxChooseShop = StaticResources.choosingLanguage.DownloadedPageCbxChooseShop;
			this.TbxUploadAll = StaticResources.choosingLanguage.DownloadedPageUploadAll;
			this.TbxExampleImg = StaticResources.choosingLanguage.DownloadedPageExampleImage;
			this.TbxLogoPosition = StaticResources.choosingLanguage.DownloadedPageLogoPosition;
			this.TbxImgSection = StaticResources.choosingLanguage.DownloadedPageImageSection;
			this.TbxInsertBegining = StaticResources.choosingLanguage.DownloadedPageInsertBegining;
			this.TbxInsertEnding = StaticResources.choosingLanguage.DownloadedPageInsertEnding;
			this.TbxReplaceFrom = StaticResources.choosingLanguage.DownloadedPageReplaceFrom;
			this.TbxReplaceTo = StaticResources.choosingLanguage.DownloadedPageReplaceTo;
			this.TbxNameSection = StaticResources.choosingLanguage.DownloadedPageNameSection;
			this.TbxDescriptionSection = StaticResources.choosingLanguage.DownloadedPageDescriptionSection;
			this.TbxPriceIncrease = StaticResources.choosingLanguage.DownloadedPagePriceIncrease;
			this.TbxPriceDecrease = StaticResources.choosingLanguage.DownloadedPagePriceDecrease;
			this.TbxPriceSection = StaticResources.choosingLanguage.DownloadedPagePriceSection;
			this.TbxApplyAll = StaticResources.choosingLanguage.DownloadedPageApplyAll;
			this.TbxUndoAll = StaticResources.choosingLanguage.DownloadedPageUndoAll;
			this.TbxInsertLogo = StaticResources.choosingLanguage.DownloadedPageInsertLogo;
			this.TbxImgResolution = StaticResources.choosingLanguage.DownloadedPageImgResolution;
			this.TbxFrameResolution = StaticResources.choosingLanguage.DownloadedPageFrameResolution;
			this.TbxFrame = StaticResources.choosingLanguage.DownloadedPageChooseFrame;
			this.TbxImg = StaticResources.choosingLanguage.DownloadedPageChooseImg;
			this.TbxImgDrag = StaticResources.choosingLanguage.DownloadedPageImgDrag;
			this.TbxImgScale = StaticResources.choosingLanguage.DownloadedPageImgScale;
			this.TbxCurrentProductT = StaticResources.choosingLanguage.DownloadedPageCurrentProduct;
			this.TbxLimitationT = StaticResources.choosingLanguage.DownloadedPageLimitation;
			this.TbxRemainingProductT = StaticResources.choosingLanguage.DownloadedPageProductRemaining;

			this._coreCPU = Environment.ProcessorCount;

			this.ListChangeStack = new ObservableCollection<ObservableCollection<ItemSearch>>();
			this._evUpdateDone += (s, e) =>
			{
				this.GrUploadingProductTag = "Collapsed";
			};
			ListShop = StaticResources.lstShopInfoLogin;
			ListLogoPosition = PickupStaticEntity.getLogoPosition();
			this.SelectedLogoPostion = ListLogoPosition[0];
			ListItem = StaticResources.ListProductDetails;
			this.isGrUploadingProductShown = Visibility.Collapsed;

			if (ListShop != null && ListShop.Count > 0)
				SelectedShop = ListShop[0];

			StaticResources.GlobalPropertyChanged += (s, e) =>
			{
				switch (e.PropertyName)
				{
					case "ListProductDetails":
						//ListItem.Clear();
						ListItem = StaticResources.ListProductDetails;
						break;
					default:
						break;
				}
			};

			BtAddNewFrameImageTapped = new DelegateCommand(async () =>
			{
				var picker = new Windows.Storage.Pickers.FileOpenPicker();
				picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
				picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
				picker.FileTypeFilter.Add(".jpg");
				picker.FileTypeFilter.Add(".jpeg");
				picker.FileTypeFilter.Add(".png");
				var file = await picker.PickSingleFileAsync();
				if (file != null)
				{
					using (Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
					{
						BitmapImage bitmapImage = new BitmapImage();
						bitmapImage.SetSource(fileStream);
						this.FrameImage.Source = bitmapImage;
						this.FrameImage.sourceStorageFile = file;
						this.FrameImage.Token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file);
						this.FrameImage.isAddImageShown = Visibility.Collapsed;
						this.FrameImage.isCanDeleteShown = Visibility.Visible;
					}
				}
			});

			BtAddLogoImageTapped = new DelegateCommand(async () =>
			{
				var picker = new Windows.Storage.Pickers.FileOpenPicker();
				picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
				picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
				picker.FileTypeFilter.Add(".jpg");
				picker.FileTypeFilter.Add(".jpeg");
				picker.FileTypeFilter.Add(".png");
				var file = await picker.PickSingleFileAsync();
				if (file != null)
				{
					using (Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
					{
						BitmapImage bitmapImage = new BitmapImage();
						bitmapImage.SetSource(fileStream);
						this.LogoImage.Source = bitmapImage;
						this.LogoImage.sourceStorageFile = file;
						this.LogoImage.Token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file);
						this.LogoImage.isAddImageShown = Visibility.Collapsed;
						this.LogoImage.isCanDeleteShown = Visibility.Visible;
					}
				}
			});

			BtDeleteLogoImageTapped = new DelegateCommand(() =>
			{
				this.LogoImage = new NewItemImages();
			});

			BtDeleteFrameImageTapped = new DelegateCommand(() =>
			{
				this.FrameImage = new NewItemImages();
			});


			MovableManipulationStarted = new DelegateCommand<CompositeTransform>((s) =>
			{
				this._brLogoImage.Opacity = 0.4;
				this._brTransform = s;
			});

			BrTransformLoaded = new DelegateCommand<CompositeTransform>((s) =>
			{
				this._brTransform = s;
			});

			BrLogoImageLoaded = new DelegateCommand<Border>((s) =>
			{
				this._brLogoImage = s;
			});

			BrTransformLoaded = new DelegateCommand<CompositeTransform>((s) =>
			{
				this._brTransform = s;
			});


			MovableManipulationDelta = new DelegateCommand<ManipulationDeltaRoutedEventArgs>((s) =>
			{
				var calibrateSpace = (int)(7 * Math.Abs(this._brTransform.ScaleX - 1) * 10);
				int boundX = 0;
				int boundY = 0;
				if (this._brTransform.ScaleX < 1)
				{
					boundX = 100 + calibrateSpace;
					boundY = 105 + calibrateSpace;
				}
				else
				{
					boundX = 100 - calibrateSpace;
					boundY = 105 - calibrateSpace;
				}
				this._brTransform.TranslateX += s.Delta.Translation.X;
				this._brTransform.TranslateY += s.Delta.Translation.Y;
				if (this._brTransform.TranslateX < -100)
				{
					this._brTransform.TranslateX = -100;
				}
				if (this._brTransform.TranslateX > boundX)
				{
					this._brTransform.TranslateX = boundX;
				}
				if (this._brTransform.TranslateY < -105)
				{
					this._brTransform.TranslateY = -105;
				}
				if (this._brTransform.TranslateY > boundY)
				{
					this._brTransform.TranslateY = boundY;
				}
			});

			MovableManipulationCompleted = new DelegateCommand(() =>
			{
				this._brLogoImage.Opacity = 1;
				this.LogoImage.DeltaX = this._brTransform.TranslateX;
				this.LogoImage.DeltaY = this._brTransform.TranslateY;
				this.LogoImage.Scale = this._brTransform.ScaleX;
			});

			DragImageOverHandler = new DelegateCommand<DragEventArgs>((s) =>
			{
				s.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
			});

			DropImageHandler = new DelegateCommand<DragEventArgs>(async (s) =>
			{
				if (s.DataView.Contains(StandardDataFormats.StorageItems))
				{
					var items = await s.DataView.GetStorageItemsAsync();
					if (items.Count > 0)
					{
						var storageFile = items[0] as StorageFile;
						var bitmapImage = new BitmapImage();
						bitmapImage.SetSource(await storageFile.OpenAsync(FileAccessMode.Read));
						// Set the image on the main page to the dropped image
						LogoImage.Source = bitmapImage;
					}
				}
			});

			ScaleLogoImageHandle = new DelegateCommand<PointerRoutedEventArgs>((s) =>
			{
				double dblDelta_Scroll = s.GetCurrentPoint(this._brLogoImage).Properties.MouseWheelDelta;
				dblDelta_Scroll = (dblDelta_Scroll > 0) ? 1.1 : 0.9;
				if (this._brTransform != null)
				{
					this._brTransform.ScaleX = this._brTransform.ScaleX * dblDelta_Scroll;
					this._brTransform.ScaleY = this._brTransform.ScaleY * dblDelta_Scroll;
					if (this._brTransform.ScaleY > 1.5)
					{
						this._brTransform.ScaleX = 1.5;
						this._brTransform.ScaleY = 1.5;
					}
					if (this._brTransform.ScaleX < 0.7)
					{
						this._brTransform.ScaleX = 0.7;
						this._brTransform.ScaleY = 0.7;
					}
				}
			});


			BtImageProcessingTapped = new DelegateCommand(() =>
			{

			});

			BtEditProductTapped = new DelegateCommand<ItemSearch>((s) =>
			{

			});

			BtDeleteAllProductErrorTapped = new DelegateCommand(() =>
			{
				this.ListItemError.Clear();
				this.TbxTotalProductError = 0;
			});

			BtDeleteProductErrorTapped = new DelegateCommand<ItemSearch>((s) =>
			{
				this.ListItemError.Remove(s);
				this.TbxTotalProductError--;
			});


			BtSaveFileToLocalTapped = new DelegateCommand(() =>
			{
				if (StaticResources.ListProductDetails.Count() > 0)
				{
					try
					{
						var path = ApplicationData.Current.LocalFolder.Path;
						using (var jsonWrite = new StreamWriter(path + "/LastSession.json"))
						{
							jsonWrite.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(StaticResources.ListProductDetails));
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
				}
			});

			BtLoadFileFromLocalTapped = new DelegateCommand(() =>
			{
				try
				{
					var path = ApplicationData.Current.LocalFolder.Path;
					using (var jsonReader = new StreamReader(path + "/LastSession.json"))
					{
						if (StaticResources.ListProductDetails.Count() > 0)
						{
							StaticResources.ListProductDetails = new ObservableCollection<ItemSearch>(StaticResources.ListProductDetails.Concat(Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<ItemSearch>>(jsonReader.ReadToEnd())));
						}
						else
						{
							StaticResources.ListProductDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<ItemSearch>>(jsonReader.ReadToEnd());
						}
						StaticResources.ChoosingProductFeature.SelfCount = StaticResources.ListProductDetails.Count();
						if (StaticResources.ListProductDetails.Count() > 0)
						{
							for (int i = 0; i < StaticResources.ListProductDetails.Count(); i++)
							{
								StaticResources.dictCanDownload[StaticResources.ListProductDetails[i].itemid] = false;
								StaticResources.ListProductDetails[i].index = i + 1;
							}
						}
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}

			});

			BtApplyTextPreProcessTapped = new DelegateCommand(async () =>
			{
				this.GrApplyProductTag = "Visible";
				ListChangeStack.Add(new ObservableCollection<ItemSearch>(StaticResources.ListProductDetails.Select(p => p.clone()).ToList()));

				foreach (var item in StaticResources.ListProductDetails)
				{
					if (!string.IsNullOrEmpty(this.TbNameInsertFromBegining))
					{
						item.name = this.TbNameInsertFromBegining.TrimEnd().TrimStart() + " " + item.name;
					}
					if (!string.IsNullOrEmpty(this.TbNameInsertToEnding))
					{
						item.name = item.name + " " + this.TbNameInsertToEnding.TrimEnd().TrimStart();
					}

					if (!string.IsNullOrEmpty(this.TbNameReplaceFrom) && !string.IsNullOrEmpty(this.TbNameReplaceTo))
					{
						item.name = item.name.Replace(this.TbNameReplaceFrom, this.TbNameReplaceTo).TrimEnd().TrimStart();
					}

					if (!string.IsNullOrEmpty(this.TbDesInsertFromBegining))
					{
						item.description = this.TbDesInsertFromBegining.TrimEnd().TrimStart() + " " + item.description;
					}
					if (!string.IsNullOrEmpty(this.TbDesInsertToEnding))
					{
						item.description = item.description + " " + this.TbDesInsertToEnding.TrimEnd().TrimStart();
					}

					if (!string.IsNullOrEmpty(this.TbDesReplaceFrom) && !string.IsNullOrEmpty(this.TbDesReplaceTo))
					{
						item.description = item.description.Replace(this.TbDesReplaceFrom, this.TbDesReplaceTo).TrimEnd().TrimStart();
					}
					try
					{
						if (!string.IsNullOrEmpty(this.TbIncreasePrice))
						{
							double priceAdd = 0;
							double.TryParse(this.TbIncreasePrice, out priceAdd);
							if (priceAdd > 0 && priceAdd < 100)
							{

								var pricetmp = Convert.ToInt64(item.price.Value + item.price.Value * priceAdd / 100);
								item.price = Convert.ToInt64(Math.Round(Convert.ToInt64(pricetmp) / 100000000.0)) * 100000000;
							}
						}
						if (!string.IsNullOrEmpty(this.TbDecreasePrice))
						{
							double priceSub = 0;
							double.TryParse(this.TbDecreasePrice, out priceSub);
							if (priceSub > 0 && priceSub < 100)
							{
								item.price = Convert.ToInt64(item.price.Value - item.price.Value * priceSub / 100);
								item.price = Convert.ToInt64(Math.Round(Convert.ToInt64(item.price) / 100000000.0)) * 100000000;
							}
						}
					}
					catch (Exception)
					{

					}

					await ImageProcessing(item);

				}
				this.GrApplyProductTag = "Collapsed";
				//await new MessageDialog("Success!").ShowAsync();
			});

			BtUndoAllItemTapped = new DelegateCommand(() =>
			{
				if (ListChangeStack != null && ListChangeStack.Count() > 0)
				{
					StaticResources.ListProductDetails.Clear();
					StaticResources.ListProductDetails = new ObservableCollection<ItemSearch>(ListChangeStack[ListChangeStack.Count() - 1].Select(p => p.clone()).ToList());
					ListChangeStack.RemoveAt(ListChangeStack.Count() - 1);
				}
			});

			ShopeeProductDownloadedPageLoading = new DelegateCommand(async () =>
			{
				//if (StaticResources.SelectedShopLogin == null)
				//{
				//	if (StaticResources.lstShopInfoLogin.Count() == 0)
				//	{
				//		await new MessageDialog("Please login a Shopee seller account first!!").ShowAsync();
				//		StaticResources.NavigationFrame.Navigate(typeof(ShopeeLoginPage), null, new DrillInNavigationTransitionInfo());
				//		StaticResources.isLoginPage = true;
				//	}
				//	else
				//	{
				//		await new MessageDialog("Choose an account first before working!").ShowAsync();
				//	}
				//	return;
				//}
			});

			BtDeleteAllProductTapped = new DelegateCommand(() =>
			{
				StaticResources.ListProductDetails.Clear();
				StaticResources.dictCanDownload.Clear();
				StaticResources.TotalChoosingProduct = 0;
				StaticResources.ChoosingProductFeature.SelfCount = 0;
			});

			BtUploadAllItemTapped = new DelegateCommand(async () =>
			{
				try
				{

					if (StaticResources.ListProductDetails.Count() > 0)
					{
						if (this.SelectedShop == null)
						{
							await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningUpload).ShowAsync();
							return;
						}
						if (StaticResources.ShortRegion != "PH")
						{
							if (StaticResources.LogisticsChannel == null || StaticResources.LogisticsChannel.Count() <= 0)
							{
								await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningConfig).ShowAsync();
								StaticResources.NavigationFrame.Navigate(typeof(ShopeeConfigPage), null, new DrillInNavigationTransitionInfo());
								return;
							}
						}
						this.GrUploadingProductTag = "Visible";
						Utility.GetShopLimitation(this.SelectedShop, _getShopLimitationCallBack);
					}
				}
				catch
				{

				}
			});

			BtUploadProductTapped = new DelegateCommand<ItemSearch>(async (s) =>
			{
				//if (this._funcScore > 0)
				//{
				//	await new MessageDialog(StaticResources.choosingLanguage.DownloadedApplyBeforeUploading).ShowAsync();
				//	return;
				//}

				if (this.SelectedShop == null)
				{
					await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningUpload).ShowAsync();
					return;
				}
				if (StaticResources.LogisticsChannel == null || StaticResources.LogisticsChannel.Count() <= 0)
				{
					await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningConfig).ShowAsync();
					StaticResources.NavigationFrame.Navigate(typeof(ShopeeConfigPage), null, new DrillInNavigationTransitionInfo());
					return;
				}
				this.GrUploadingProductTag = "Visible";
				this.TbxTotalUploadingProduct = 1;
				this.TbxCurrentProgress = 0;
				new Thread(async () =>
				{
					try
					{
						if (await Utility.UploadItem_V3(this.SelectedShop, s, this.InsertLogoOn) == false)
						//if (await Utility.UploadItemfromLocal(this.SelectedShop, s, this.InsertLogoOn) == false)
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								this.ListItemError.Add(s);
								this.TbxTotalProductError++;
							});
						}
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							lock (this._lockObj)
							{
								this.TbxCurrentProgress++;
								if (this.TbxCurrentProgress == this.TbxTotalUploadingProduct)
								{
									this._evUpdateDone(this, null);
								}
							}
						});
					}
					catch
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							this.ListItemError.Add(s);
							this.TbxTotalProductError++;
						});
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							lock (this._lockObj)
							{
								this.TbxCurrentProgress++;
								if (this.TbxCurrentProgress == this.TbxTotalUploadingProduct)
								{
									this._evUpdateDone(this, null);
								}
							}
						});
					}
				}).Start();
			});

			BtDeleteProductTapped = new DelegateCommand<ItemSearch>((s) =>
			{
				StaticResources.ListProductDetails.Remove(s);
				StaticResources.dictCanDownload[s.itemid] = true;
				StaticResources.TotalChoosingProduct--;
				StaticResources.ChoosingProductFeature.SelfCount = StaticResources.TotalChoosingProduct;
			});
		}

		private async void _getShopLimitationCallBack(IAsyncResult asynchronousResult)
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
										this.SelectedShop.ShopStatus = "Out of date";
										this.GrUploadingProductTag = "Collapsed";
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
										this.SelectedShop.ShopStatus = "Out of date";
										this.GrUploadingProductTag = "Collapsed";
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
						var jsonContent = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopLimitation>(resultContent);
						try
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								this.TbxLimitation = jsonContent.list_limit;
							});
							Utility.getProductPerPage(this.SelectedShop, _getProductPerPageCallBack, 1);
						}
						catch (Exception ex)
						{
							Debug.WriteLine("getShippingFee_V3 {0}", ex);
						}
					});
				}
			}
			catch (WebException ex)
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					this.GrUploadingProductTag = "Collapsed";
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
									this.SelectedShop.ShopStatus = "Out of date";
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
									this.SelectedShop.ShopStatus = "Out of date";
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

		private async void _getProductPerPageCallBack(IAsyncResult asynchronousResult)
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
										this.SelectedShop.ShopStatus = "Out of date";
										this.GrUploadingProductTag = "Collapsed";
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
										this.SelectedShop.ShopStatus = "Out of date";
										this.GrUploadingProductTag = "Collapsed";
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
						var pageProductList = Newtonsoft.Json.JsonConvert.DeserializeObject<PageProductList>(resultContent);
						if (pageProductList != null)
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								this.TbxCurrentProduct = pageProductList.data.page_info.total;
								this.TbxRemainingProduct = this.TbxLimitation - this.TbxCurrentProduct;
								this.TbxTotalUploadingProduct = StaticResources.ListProductDetails.Count();
								this.TbxCurrentProgress = 0;
								if (this.TbxRemainingProduct < StaticResources.ListProductDetails.Count())
								{
									this.TbxTotalUploadingProduct = this.TbxRemainingProduct;
								}
								ThreadPool.SetMaxThreads(this._coreCPU * 2, this._coreCPU * 2);
								for (int i = 0; i < this.TbxTotalUploadingProduct; i++)
								{
									ThreadPool.QueueUserWorkItem(new WaitCallback(async (state) =>
									{
										ItemSearch item = state as ItemSearch;
										try
										{
											if (await Utility.UploadItem_V3(this.SelectedShop, item, this.InsertLogoOn) == false)
											//if (await Utility.UploadItemfromLocal(this.SelectedShop, item, this.InsertLogoOn) == false)
											{
												await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
												() =>
												{
													this.ListItemError.Add(item);
													this.TbxTotalProductError++;
												});
											}
											await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
											() =>
											{
												lock (this._lockObj)
												{
													this.TbxCurrentProgress++;
													if (this.TbxCurrentProgress == this.TbxTotalUploadingProduct)
													{
														this._evUpdateDone(this, null);
													}
												}
											});
										}
										catch
										{
											await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
											() =>
											{
												this.ListItemError.Add(item);
												this.TbxTotalProductError++;
											});
											await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
											() =>
											{
												lock (this._lockObj)
												{
													this.TbxCurrentProgress++;
													if (this.TbxCurrentProgress == this.TbxTotalUploadingProduct)
													{
														this._evUpdateDone(this, null);
													}
												}
											});
										}
									}), StaticResources.ListProductDetails[i]);
								}
							});
						}
					});
				}
			}
			catch (WebException ex)
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					this.GrUploadingProductTag = "Collapsed";
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
									this.SelectedShop.ShopStatus = "Out of date";
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
									this.SelectedShop.ShopStatus = "Out of date";
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

		private async Task ImageProcessing(ItemSearch item)
		{
			try
			{
				StorageFolder avatarImgStorageFolder = null;
				StorageFolder downloadImageStorageFolder = null;
				StorageFolder processeddownloadImageStorageFolder = null;

				try
				{
					processeddownloadImageStorageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ProcessedImageDownloaded");
					string imageFolderToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(processeddownloadImageStorageFolder);
					ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"] = imageFolderToken;
				}
				catch
				{
					if (ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"] != null)
					{
						string token = ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"].ToString();
						processeddownloadImageStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
					}
				}

				if (ApplicationData.Current.LocalSettings.Values["AvataDownloaded"] != null)
				{
					string token = ApplicationData.Current.LocalSettings.Values["AvataDownloaded"].ToString();
					avatarImgStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
				}

				if (ApplicationData.Current.LocalSettings.Values["ImageDownloaded"] != null)
				{
					string token = ApplicationData.Current.LocalSettings.Values["ImageDownloaded"].ToString();
					downloadImageStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
				}

				if (this.InsertLogoOn == true)
				{
					string logoPath = avatarImgStorageFolder.Path + "\\" + SelectedShop.Shop.id + "_avt.jpeg";
					if (File.Exists(logoPath))
					{
						for (int i = 0; i < item.images.Count(); i++)
						{
							string processedImgPath = processeddownloadImageStorageFolder.Path + "\\" + item.itemid + "_" + item.images[i] + ".jpeg";

							StorageFile storageFile = null;
							StorageFile storageFile2 = null;
							StorageFile storageFile3 = null;
							StorageFile storageFile4 = null;
							StorageFile storageFile5 = null;
							StorageFile frameFile = null;

							storageFile = await downloadImageStorageFolder.CreateFileAsync(item.itemid + "_" + item.images[i] + ".jpeg", CreationCollisionOption.OpenIfExists);
							storageFile2 = this.LogoImage.sourceStorageFile;
							storageFile3 = await processeddownloadImageStorageFolder.CreateFileAsync(item.itemid + "_" + item.images[i] + ".jpeg", CreationCollisionOption.ReplaceExisting);
							storageFile4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/overlay.png"));
							storageFile5 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/overlay2.png"));
							frameFile = this.FrameImage.sourceStorageFile;
							using (var randomAccessStream = await storageFile.OpenAsync(FileAccessMode.Read))
							{
								if (storageFile2 != null && frameFile != null)
								{
									using (var rdAccesStream2 = await storageFile2.OpenAsync(FileAccessMode.Read))
									{
										using (var rdAccessStream3 = await storageFile4.OpenAsync(FileAccessMode.Read))
										{
											using (var rdAccessStream4 = await storageFile5.OpenAsync(FileAccessMode.Read))
											{
												using (var rdAccessFrameImg = await frameFile.OpenAsync(FileAccessMode.Read))
												{
													try
													{
														//orgin H: 280, W: 280
														//Scale: 1
														//logo: H: 280, W: 280





														var originImage = new BitmapImage();
														var logoImage = new BitmapImage();
														var logoOverlay = new BitmapImage();
														var logoOverlay2 = new BitmapImage();
														var frame = new BitmapImage();

														await originImage.SetSourceAsync(randomAccessStream);
														await logoImage.SetSourceAsync(rdAccesStream2);
														await logoOverlay.SetSourceAsync(rdAccessStream3);
														await logoOverlay2.SetSourceAsync(rdAccessStream4);
														await frame.SetSourceAsync(rdAccessFrameImg);

														WriteableBitmap originWriteableBitmap = new WriteableBitmap(originImage.PixelWidth, originImage.PixelHeight);
														int scale = 80;
														originWriteableBitmap = await originWriteableBitmap.FromStream(randomAccessStream);
														WriteableBitmap frameImgBitmap = new WriteableBitmap(frame.PixelWidth, frame.PixelHeight);
														frameImgBitmap = await frameImgBitmap.FromStream(rdAccessFrameImg);
														WriteableBitmap logoWriteableBitmap = new WriteableBitmap(logoImage.PixelWidth, logoImage.PixelHeight);
														logoWriteableBitmap = await logoWriteableBitmap.FromStream(rdAccesStream2);
														WriteableBitmap logoOverlayWriteableBitmap = new WriteableBitmap(80, 80);
														logoOverlayWriteableBitmap = await logoWriteableBitmap.FromStream(rdAccessStream3);
														WriteableBitmap logoOverlayWriteableBitmap2 = new WriteableBitmap(80, 80);
														logoOverlayWriteableBitmap2 = await logoWriteableBitmap.FromStream(rdAccessStream4);
														//var logoOverlayWriteableBitmap2 = await BitmapFactory.FromStream(rdAccessStream4);
														//logoOverlayWriteableBitmap2 = logoOverlayWriteableBitmap2.Resize(scale, scale, WriteableBitmapExtensions.Interpolation.Bilinear);

														//resize frame
														frameImgBitmap = frameImgBitmap.Resize(originImage.PixelWidth, originImage.PixelHeight, WriteableBitmapExtensions.Interpolation.Bilinear);

														//composite frame + origin image
														originWriteableBitmap.Blit(new Rect(0, 0, originImage.PixelWidth, originImage.PixelHeight), frameImgBitmap, new Rect(0, 0, originImage.PixelWidth, originImage.PixelHeight));


														//reszie logo
														logoWriteableBitmap = logoWriteableBitmap.Resize(scale, scale, WriteableBitmapExtensions.Interpolation.Bilinear);

														//overlay logo
														logoWriteableBitmap.Blit(new Rect(0, 0, scale, scale), logoOverlayWriteableBitmap, new Rect(0, 0, scale, scale), WriteableBitmapExtensions.BlendMode.Multiply);
														logoWriteableBitmap.Blit(new Rect(0, 0, scale, scale), logoOverlayWriteableBitmap2, new Rect(0, 0, scale, scale), WriteableBitmapExtensions.BlendMode.Additive);

														//scale logo
														var newSize = 80;
														if (this.LogoImage.Scale != 0)
														{
															newSize = (int)(80 * this.LogoImage.Scale);
															newSize = (newSize % 2 == 0) ? newSize : newSize + 1;
															logoWriteableBitmap = logoWriteableBitmap.Resize(newSize, newSize, WriteableBitmapExtensions.Interpolation.Bilinear);
														}
														//recaculate position of logo
														var rateY = (double)originImage.PixelHeight / 280;
														var rateX = (double)originImage.PixelWidth / 280;
														var newDeltaX = this.LogoImage.DeltaX * rateX;
														var newDeltaY = this.LogoImage.DeltaY * rateY;
														var positionX = Math.Abs(-originImage.PixelWidth / 2 - (int)(newDeltaX));
														var positionY = Math.Abs(originImage.PixelWidth / 2 + (int)(newDeltaY));

														originWriteableBitmap.FillEllipseCentered(positionX, positionY, newSize / 2, newSize / 2, Windows.UI.Color.FromArgb(220, 255, 255, 255));

														originWriteableBitmap.Blit(new Rect(positionX - newSize / 2, positionY - newSize / 2, newSize, newSize), logoWriteableBitmap, new Rect(0, 0, newSize, newSize), WriteableBitmapExtensions.BlendMode.Multiply);

														using (var stream = await storageFile3.OpenAsync(FileAccessMode.ReadWrite))
														{
															BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
															Stream pixelStream = originWriteableBitmap.PixelBuffer.AsStream();
															byte[] pixels = new byte[pixelStream.Length];
															await pixelStream.ReadAsync(pixels, 0, pixels.Length);

															encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
																				(uint)originWriteableBitmap.PixelWidth,
																				(uint)originWriteableBitmap.PixelHeight,
																				96.0,
																				96.0,
																				pixels);
															await encoder.FlushAsync();
														}
													}
													catch (Exception ex)
													{

													}
												}
											}

										}
									}
								}
								else if (storageFile2 != null && frameFile == null)
								{
									using (var rdAccesStream2 = await storageFile2.OpenAsync(FileAccessMode.Read))
									{
										using (var rdAccessStream3 = await storageFile4.OpenAsync(FileAccessMode.Read))
										{
											using (var rdAccessStream4 = await storageFile5.OpenAsync(FileAccessMode.Read))
											{

												try
												{

													var originImage = new BitmapImage();
													var logoImage = new BitmapImage();
													var logoOverlay = new BitmapImage();
													var logoOverlay2 = new BitmapImage();

													await originImage.SetSourceAsync(randomAccessStream);
													await logoImage.SetSourceAsync(rdAccesStream2);
													await logoOverlay.SetSourceAsync(rdAccessStream3);
													await logoOverlay2.SetSourceAsync(rdAccessStream4);

													WriteableBitmap originWriteableBitmap = new WriteableBitmap(originImage.PixelWidth, originImage.PixelHeight);
													int scale = 80;
													originWriteableBitmap = await originWriteableBitmap.FromStream(randomAccessStream);

													WriteableBitmap logoWriteableBitmap = new WriteableBitmap(logoImage.PixelWidth, logoImage.PixelHeight);
													logoWriteableBitmap = await logoWriteableBitmap.FromStream(rdAccesStream2);
													WriteableBitmap logoOverlayWriteableBitmap = new WriteableBitmap(80, 80);
													logoOverlayWriteableBitmap = await logoWriteableBitmap.FromStream(rdAccessStream3);
													WriteableBitmap logoOverlayWriteableBitmap2 = new WriteableBitmap(80, 80);
													logoOverlayWriteableBitmap2 = await logoWriteableBitmap.FromStream(rdAccessStream4);
													//var logoOverlayWriteableBitmap2 = await BitmapFactory.FromStream(rdAccessStream4);
													//logoOverlayWriteableBitmap2 = logoOverlayWriteableBitmap2.Resize(scale, scale, WriteableBitmapExtensions.Interpolation.Bilinear);

													//reszie logo
													logoWriteableBitmap = logoWriteableBitmap.Resize(scale, scale, WriteableBitmapExtensions.Interpolation.Bilinear);

													//overlay logo
													logoWriteableBitmap.Blit(new Rect(0, 0, scale, scale), logoOverlayWriteableBitmap, new Rect(0, 0, scale, scale), WriteableBitmapExtensions.BlendMode.Multiply);
													logoWriteableBitmap.Blit(new Rect(0, 0, scale, scale), logoOverlayWriteableBitmap2, new Rect(0, 0, scale, scale), WriteableBitmapExtensions.BlendMode.Additive);

													var newSize = 80;
													if (this.LogoImage.Scale != 0)
													{
														newSize = (int)(80 * this.LogoImage.Scale);
														newSize = (newSize % 2 == 0) ? newSize : newSize + 1;
														logoWriteableBitmap = logoWriteableBitmap.Resize(newSize, newSize, WriteableBitmapExtensions.Interpolation.Bilinear);
													}
													//recaculate position of logo
													var rateY = (double)originImage.PixelHeight / 280;
													var rateX = (double)originImage.PixelWidth / 280;
													var newDeltaX = this.LogoImage.DeltaX * rateX;
													var newDeltaY = this.LogoImage.DeltaY * rateY;
													var positionX = Math.Abs(-originImage.PixelWidth / 2 - (int)(newDeltaX));
													var positionY = Math.Abs(originImage.PixelWidth / 2 + (int)(newDeltaY));

													originWriteableBitmap.FillEllipseCentered(positionX, positionY, newSize / 2, newSize / 2, Windows.UI.Color.FromArgb(220, 255, 255, 255));

													originWriteableBitmap.Blit(new Rect(positionX - newSize / 2, positionY - newSize / 2, newSize, newSize), logoWriteableBitmap, new Rect(0, 0, newSize, newSize), WriteableBitmapExtensions.BlendMode.Multiply);

													using (var stream = await storageFile3.OpenAsync(FileAccessMode.ReadWrite))
													{
														BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
														Stream pixelStream = originWriteableBitmap.PixelBuffer.AsStream();
														byte[] pixels = new byte[pixelStream.Length];
														await pixelStream.ReadAsync(pixels, 0, pixels.Length);

														encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
																			(uint)originWriteableBitmap.PixelWidth,
																			(uint)originWriteableBitmap.PixelHeight,
																			96.0,
																			96.0,
																			pixels);
														await encoder.FlushAsync();
													}
												}
												catch (Exception ex)
												{

												}
											}
										}
									}
								}
								else if (storageFile2 == null && frameFile != null)
								{
									using (var rdAccessFrameImg = await frameFile.OpenAsync(FileAccessMode.Read))
									{
										try
										{

											var originImage = new BitmapImage();
											var frame = new BitmapImage();

											await originImage.SetSourceAsync(randomAccessStream);
											await frame.SetSourceAsync(rdAccessFrameImg);

											WriteableBitmap originWriteableBitmap = new WriteableBitmap(originImage.PixelWidth, originImage.PixelHeight);
											originWriteableBitmap = await originWriteableBitmap.FromStream(randomAccessStream);
											WriteableBitmap frameImgBitmap = new WriteableBitmap(frame.PixelWidth, frame.PixelHeight);
											frameImgBitmap = await frameImgBitmap.FromStream(rdAccessFrameImg);
											//resize frame
											frameImgBitmap = frameImgBitmap.Resize(originImage.PixelWidth, originImage.PixelHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
											//composite frame + origin image
											originWriteableBitmap.Blit(new Rect(0, 0, originImage.PixelWidth, originImage.PixelHeight), frameImgBitmap, new Rect(0, 0, originImage.PixelWidth, originImage.PixelHeight));

											using (var stream = await storageFile3.OpenAsync(FileAccessMode.ReadWrite))
											{
												BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
												Stream pixelStream = originWriteableBitmap.PixelBuffer.AsStream();
												byte[] pixels = new byte[pixelStream.Length];
												await pixelStream.ReadAsync(pixels, 0, pixels.Length);

												encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
																	(uint)originWriteableBitmap.PixelWidth,
																	(uint)originWriteableBitmap.PixelHeight,
																	96.0,
																	96.0,
																	pixels);
												await encoder.FlushAsync();
											}
										}
										catch (Exception ex)
										{

										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{

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
				}
			}
		}

		public ObservableCollection<ItemSearch> ListItem
		{
			get
			{
				return this._listItem;
			}
			set
			{
				SetProperty(ref this._listItem, value);
				StaticResources.ChoosingProductFeature.SelfCount = this._listItem.Count();
			}
		}

		private ObservableCollection<ItemSearch> ListItemClone
		{
			get
			{
				if (this._listItemClone == null)
				{
					this._listItemClone = new ObservableCollection<ItemSearch>();
				}
				return this._listItemClone;
			}
			set
			{
				SetProperty(ref this._listItemClone, value);
			}
		}

		public ObservableCollection<ItemSearch> ListItemError
		{
			get
			{
				if (this._listItemError == null)
				{
					this._listItemError = new ObservableCollection<ItemSearch>();
				}
				return this._listItemError;
			}
			set
			{
				SetProperty(ref this._listItemError, value);
			}
		}

		public string GrUploadingProductTag
		{
			get
			{
				return this._grUploadingProductTag;
			}
			set
			{
				SetProperty(ref this._grUploadingProductTag, value);
			}
		}

		public Visibility isGrUploadingProductShown
		{
			get
			{
				return this._isGrUploadingProductShown;
			}
			set
			{
				SetProperty(ref this._isGrUploadingProductShown, value);
			}
		}

		public int TbxTotalUploadingProduct
		{
			get
			{
				return this._tbxTotalUploadingProduct;
			}
			set
			{
				SetProperty(ref this._tbxTotalUploadingProduct, value);
			}
		}

		public int TbxCurrentProgress
		{
			get
			{
				return this._tbxCurrentProgress;
			}
			set
			{
				SetProperty(ref this._tbxCurrentProgress, value);
			}
		}

		public int TbxTotalProductError
		{
			get
			{
				return this._tbxTotalProductError;
			}
			set
			{
				SetProperty(ref this._tbxTotalProductError, value);
			}
		}

		public string TbNameInsertFromBegining
		{
			get
			{
				return this._tbNameInsertFromBegining;
			}
			set
			{
				SetProperty(ref this._tbNameInsertFromBegining, value);
				if (string.IsNullOrEmpty(this._tbNameInsertFromBegining))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbNameInsertToEnding
		{
			get
			{
				return this._tbNameInsertToEnding;
			}
			set
			{
				SetProperty(ref this._tbNameInsertToEnding, value);
				if (string.IsNullOrEmpty(this._tbNameInsertToEnding))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbNameReplaceFrom
		{
			get
			{
				return this._tbNameReplaceFrom;
			}
			set
			{
				SetProperty(ref this._tbNameReplaceFrom, value);
				if (string.IsNullOrEmpty(this._tbNameReplaceFrom))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbNameReplaceTo
		{
			get
			{
				return this._tbNameReplaceTo;
			}
			set
			{
				SetProperty(ref this._tbNameReplaceTo, value);
				if (string.IsNullOrEmpty(this._tbNameReplaceTo))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}

		public string TbDesInsertFromBegining
		{
			get
			{
				return this._tbDesInsertFromBegining;
			}
			set
			{
				SetProperty(ref this._tbDesInsertFromBegining, value);
				if (string.IsNullOrEmpty(this._tbDesInsertFromBegining))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbDesInsertToEnding
		{
			get
			{
				return this._tbDesInsertToEnding;
			}
			set
			{
				SetProperty(ref this._tbDesInsertToEnding, value);
				if (string.IsNullOrEmpty(this._tbDesInsertToEnding))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbDesReplaceFrom
		{
			get
			{
				return this._tbDesReplaceFrom;
			}
			set
			{
				SetProperty(ref this._tbDesReplaceFrom, value);
				if (string.IsNullOrEmpty(this._tbDesReplaceFrom))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbDesReplaceTo
		{
			get
			{
				return this._tbDesReplaceTo;
			}
			set
			{
				SetProperty(ref this._tbDesReplaceTo, value);
				if (string.IsNullOrEmpty(this._tbDesReplaceTo))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbIncreasePrice
		{
			get
			{
				return this._tbIncreasePrice;
			}
			set
			{
				SetProperty(ref this._tbIncreasePrice, value);
				if (string.IsNullOrEmpty(this._tbIncreasePrice))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public string TbDecreasePrice
		{
			get
			{
				return this._tbDecreasePrice;
			}
			set
			{
				SetProperty(ref this._tbDecreasePrice, value);
				if (string.IsNullOrEmpty(this._tbDecreasePrice))
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}
		public bool InsertLogoOn
		{
			get
			{
				return this._insertLogoOn;
			}
			set
			{
				SetProperty(ref this._insertLogoOn, value);
				if (this._insertLogoOn == false)
				{
					this._funcScore--;
				}
				else
				{
					this._funcScore++;
				}
			}
		}

		public LogoPosition SelectedLogoPostion
		{
			get
			{
				return this._logoPostion;
			}
			set
			{
				SetProperty(ref this._logoPostion, value);
			}
		}

		public string GrApplyProductTag
		{
			get
			{
				return this._grApplyProductTag;
			}
			set
			{
				SetProperty(ref this._grApplyProductTag, value);
			}
		}

		public string TbxHeaderUploading
		{
			get => this._tbxHeaderUploading;
			set => SetProperty(ref this._tbxHeaderUploading, value);
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
		public string TbxDescription
		{
			get => this._tbxDescription;
			set => SetProperty(ref this._tbxDescription, value);
		}
		public string TbxPrice
		{
			get => this._tbxPrice;
			set => SetProperty(ref this._tbxPrice, value);
		}
		public string TbxDeleteAll
		{
			get => this._tbxDeleteAll;
			set => SetProperty(ref this._tbxDeleteAll, value);
		}
		public string TbxUpload
		{
			get => this._tbxUpload;
			set => SetProperty(ref this._tbxUpload, value);
		}
		public string TbxDelete
		{
			get => this._tbxDelete;
			set => SetProperty(ref this._tbxDelete, value);
		}
		public string TbxError
		{
			get => this._tbxError;
			set => SetProperty(ref this._tbxError, value);
		}
		public string TbxErrorReason
		{
			get => this._tbxErrorReason;
			set => SetProperty(ref this._tbxErrorReason, value);
		}
		public string TbxSave
		{
			get => this._tbxSave;
			set => SetProperty(ref this._tbxSave, value);
		}
		public string TbxLoad
		{
			get => this._tbxLoad;
			set => SetProperty(ref this._tbxLoad, value);
		}
		public string TbxUploadTo
		{
			get => this._tbxUploadTo;
			set => SetProperty(ref this._tbxUploadTo, value);
		}
		public string CbxChooseShop
		{
			get => this._cbxChooseShop;
			set => SetProperty(ref this._cbxChooseShop, value);
		}
		public string TbxUploadAll
		{
			get => this._tbxUploadAll;
			set => SetProperty(ref this._tbxUploadAll, value);
		}
		public string TbxExampleImg
		{
			get => this._tbxExampleImg;
			set => SetProperty(ref this._tbxExampleImg, value);
		}
		public string TbxLogoPosition
		{
			get => this._tbxLogoPosition;
			set => SetProperty(ref this._tbxLogoPosition, value);
		}
		public string TbxImgSection
		{
			get => this._tbxImgSection;
			set => SetProperty(ref this._tbxImgSection, value);
		}
		public string TbxInsertBegining
		{
			get => this._tbxInsertBegining;
			set => SetProperty(ref this._tbxInsertBegining, value);
		}
		public string TbxInsertEnding
		{
			get => this._tbxInsertEnding;
			set => SetProperty(ref this._tbxInsertEnding, value);
		}
		public string TbxReplaceFrom
		{
			get => this._tbxReplaceFrom;
			set => SetProperty(ref this._tbxReplaceFrom, value);
		}
		public string TbxReplaceTo
		{
			get => this._tbxReplaceTo;
			set => SetProperty(ref this._tbxReplaceTo, value);
		}
		public string TbxNameSection
		{
			get => this._tbxNameSection;
			set => SetProperty(ref this._tbxNameSection, value);
		}
		public string TbxDescriptionSection
		{
			get => this._tbxDescriptionSection;
			set => SetProperty(ref this._tbxDescriptionSection, value);
		}
		public string TbxPriceIncrease
		{
			get => this._tbxPriceIncrease;
			set => SetProperty(ref this._tbxPriceIncrease, value);
		}
		public string TbxPriceDecrease
		{
			get => this._tbxPriceDecrease;
			set => SetProperty(ref this._tbxPriceDecrease, value);
		}
		public string TbxPriceSection
		{
			get => this._tbxPriceSection;
			set => SetProperty(ref this._tbxPriceSection, value);
		}
		public string TbxApplyAll
		{
			get => this._tbxApplyAll;
			set => SetProperty(ref this._tbxApplyAll, value);
		}
		public string TbxUndoAll
		{
			get => this._tbxUndoAll;
			set => SetProperty(ref this._tbxUndoAll, value);
		}

		public string TbxInsertLogo
		{
			get => this._tbxInsertLogo;
			set => SetProperty(ref this._tbxInsertLogo, value);
		}

		public string TbxImgResolution
		{
			get => this._tbxImgResolution;
			set => SetProperty(ref this._tbxImgResolution, value);
		}

		public string TbxImg
		{
			get => this._tbxImg;
			set => SetProperty(ref this._tbxImg, value);
		}

		public NewItemImages FrameImage
		{
			get
			{
				if (this._frameImg == null)
				{
					this._frameImg = new NewItemImages();
				}
				return this._frameImg;
			}
			set => SetProperty(ref this._frameImg, value);
		}

		public NewItemImages LogoImage
		{
			get
			{
				if (this._logoImg == null)
				{
					this._logoImg = new NewItemImages();
				}
				return this._logoImg;
			}
			set => SetProperty(ref this._logoImg, value);
		}

		public string TbxFrameResolution
		{
			get => this._tbxFrameResolution;
			set => SetProperty(ref this._tbxFrameResolution, value);
		}

		public string TbxFrame
		{
			get => this._tbxFrame;
			set => SetProperty(ref this._tbxFrame, value);
		}

		public string TbxImgDrag
		{
			get => this._tbxImgDrag;
			set => SetProperty(ref this._tbxImgDrag, value);
		}
		public string TbxImgScale
		{
			get => this._tbxImgScale;
			set => SetProperty(ref this._tbxImgScale, value);
		}

		public int TbxLimitation
		{
			get => this._tbxLimitation;
			set => SetProperty(ref this._tbxLimitation, value);
		}
		public int TbxCurrentProduct
		{
			get => this._tbxCurrentProduct;
			set => SetProperty(ref this._tbxCurrentProduct, value);
		}
		public string TbxCurrentProductT
		{
			get => this._tbxCurrentProductT;
			set => SetProperty(ref this._tbxCurrentProductT, value);
		}
		public string TbxLimitationT
		{
			get => this._tbxLimitationT;
			set => SetProperty(ref this._tbxLimitationT, value);
		}

		public int TbxRemainingProduct
		{
			get => this._tbxRemainingProduct;
			set => SetProperty(ref this._tbxRemainingProduct, value);
		}

		public string TbxRemainingProductT
		{
			get => this._tbxRemainingProductT;
			set => SetProperty(ref this._tbxRemainingProductT, value);
		}
	}
}
