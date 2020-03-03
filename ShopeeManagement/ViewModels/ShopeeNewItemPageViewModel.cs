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
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace ShopeeManagement.ViewModels
{
	public class ShopeeNewItemPageViewModel : ViewModelBase
	{
		public ICommand BtAddNewImageTapped { get; set; }
		public ICommand BtDeleteImageTapped { get; set; }
		public ICommand BtAddNewOptionTapped { get; set; }
		public ICommand BtAddNewVariationTapped { get; set; }
		public ICommand BtDeleteOptionTapped { get; set; }
		public ICommand BtDeleteVariationTapped { get; set; }
		public ICommand CbxAttributeSelectionChanged { get; set; }
		public ICommand BtUploadItemTapped { get; set; }
		public ICommand BtApplyForAllVariationsTapped { get; set; }
		public ICommand BtImageProcessingTapped { get; set; }
		public ICommand BtCancelGrImgProcessingTapped { get; set; }
		public ICommand BtDeleteFrameImageTapped { get; set; }
		public ICommand BtAddNewFrameImageTapped { get; set; }
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


		private ObservableCollection<Categories> _listCategories;
		private ObservableCollection<SubCategory> _listSubCategories;
		private ObservableCollection<SubSubCategory> _listSubSubCategories;
		private ObservableCollection<FullAttribute> _listFullAttribute;
		private ObservableCollection<NewItemImages> _listImages;
		private ObservableCollection<ItemVariations> _listVariations;

		private Categories _selectedMainCategory;
		private SubCategory _selectedSubCategory;
		private SubSubCategory _selectedSubSubCategory;

		private Visibility _isMultiVariationPricingShown;
		private Visibility _isAVariationPricingShown;
		private Visibility _isWarningTbxPriceShown;
		private Visibility _isWarningTbxStockShown;

		private bool _isMultiVariations;

		private string _tbItemPrice;
		private string _tbItemStock;
		private string _tbItemName;
		private string _tbItemDescription;
		private string _tgGrImgProcessing;

		private NewItemImages _frameImg;
		private NewItemImages _logoImg;

		private UInt64? modelID;

		private bool _isNumberic;

		private NewItemImages _selectedImage;
		private Border _brLogoImage;
		private CompositeTransform _brTransform;
		public ShopeeNewItemPageViewModel()
		{
			this.isWarningTbxPriceShown = Visibility.Collapsed;
			this.isWarningTbxStockShown = Visibility.Collapsed;
			this.ListImages.Add(new NewItemImages());
			this.ListVariations.Add(new ItemVariations
			{
				index = 1,
			});
			var Var = new Variations(this.ListVariations[0]);
			Var.OptionAdded += (m, e) =>
			{
				var sender = m as Variations;
				this.__UpdatePriceTable(sender);
			};
			this.ListVariations[0].variations.Add(Var);
			this.isMultiVariationPricingShown = Visibility.Collapsed;
			this.isAVariationPricingShown = Visibility.Visible;
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

			BrLogoImageLoaded = new DelegateCommand<Border>((s) =>
			{
				this._brLogoImage = s;
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

			MovableManipulationDelta = new DelegateCommand<ManipulationDeltaRoutedEventArgs>((s) =>
			{
				var calibrateSpace = (int)(7 * Math.Abs(this._brTransform.ScaleX - 1)*10);
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
				if (this._brTransform.TranslateX < -100 )
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
				this.TgGrImgProcessing = "Visible";
			});

			BtCancelGrImgProcessingTapped = new DelegateCommand(() =>
			{
				this.TgGrImgProcessing = "Collapsed";
			});

			BtDeleteFrameImageTapped = new DelegateCommand(() =>
			{
				this.FrameImage = new NewItemImages();
			});

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

			BtDeleteLogoImageTapped = new DelegateCommand(() =>
			{
				this.LogoImage = new NewItemImages();
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


			BtUploadItemTapped = new DelegateCommand(async () =>
			{
				if (string.IsNullOrEmpty(this.TbItemName))
				{
					await new MessageDialog("Product name could not be a null or empty value").ShowAsync();
					return;
				}
				if (string.IsNullOrEmpty(this.TbItemDescription))
				{
					await new MessageDialog("Product description could not be a null or empty value").ShowAsync();
					return;
				}
				if(this.SelectedMainCategory == null || this.SelectedSubCategory == null || this.SelectedSubSubCategory == null)
				{
					await new MessageDialog("Product category could not be a null value").ShowAsync();
					return;
				}
				if(this.ListImages.Where(p => !string.IsNullOrEmpty(p.Token)).ToList().Count == 0)
				{
					await new MessageDialog("Please upload atleast one images").ShowAsync();
					return;
				}
				if (string.IsNullOrEmpty(this.TbItemPrice) || string.IsNullOrEmpty(this.TbItemStock))
				{
					await new MessageDialog("Product pricing could not be a null value").ShowAsync();
					return;
				}
				else
				{
					if (this._checkTextBoxValidate(this.TbItemPrice) == false || this._checkTextBoxValidate(this.TbItemStock) == false)
					{
						await new MessageDialog("Product pricing must be a number").ShowAsync();
						this.TbItemPrice = string.Empty;
						this.TbItemStock = string.Empty;
						return;
					}
				}
				if (this.ListFullAttribute.Where(p => !string.IsNullOrEmpty(p.data.multi_lang[0].Choose)).ToList().Count != this.ListFullAttribute.Count())
				{
					await new MessageDialog("Please select all proper attributes for your product").ShowAsync();
					return;
				}
				if (StaticResources.LogisticsChannel.Count() == 0)
				{
					await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningConfig).ShowAsync();
					StaticResources.NavigationFrame.Navigate(typeof(ShopeeConfigPage), null, new DrillInNavigationTransitionInfo());
					return; 
				}
				var item = await this._createNewItemInstance();
				if (this.ListImages.Count > 0)
				{
					await Utility.UploadItemfromLocal_V3(StaticResources.SelectedShopLogin, null,false,this.ListImages.Where(p => !string.IsNullOrEmpty(p.Token)).ToList(),item, false);
				}
			});

			BtAddNewOptionTapped = new DelegateCommand<ItemVariations>((s) =>
			{
				if (!string.IsNullOrEmpty(s.variations[s.variations.Count() - 1].Option))
				{
					var newVar = new Variations(s);
					newVar.index = s.variations.Count() + 1;
					newVar.OptionAdded += (m, e) =>
					{
						var sender = m as Variations;
						this.__UpdatePriceTable(sender);
					};
					s.variations.Add(newVar);
				}
			});

			BtAddNewVariationTapped = new DelegateCommand(() =>
			{
				if (this.ListVariations.Count < 2)
				{
					this.ListVariations.Add(new ItemVariations
					{
						index = 2,
					});
					var Var1 = new Variations(this.ListVariations[1]);
					Var1.index = 1;
					Var1.OptionAdded += (m, e) =>
					{
						var sender = m as Variations;
						this.__UpdatePriceTable(sender);
					};
					this.ListVariations[1].variations.Add(Var1);
					this.isAVariationPricingShown = Visibility.Collapsed;
					this.isMultiVariationPricingShown = Visibility.Visible;
				}
			});

			BtDeleteVariationTapped = new DelegateCommand<ItemVariations>((s) =>
			{
				if (this.ListVariations.Count() > 1)
				{
					this.ListVariations.Remove(s);
					this.ListVariations[0].index = 1;
					this.isMultiVariationPricingShown = Visibility.Collapsed;
					this.isAVariationPricingShown = Visibility.Visible;
				}
			});

			BtDeleteImageTapped = new DelegateCommand<NewItemImages>((s) =>
			{
				this.ListImages.Remove(s);
				if (this.ListImages[this.ListImages.Count() - 1].Source != null)
				{
					this.ListImages.Add(new NewItemImages());
				}
			});

			BtDeleteOptionTapped = new DelegateCommand<Variations>((s) =>
			{
				var par = s.Parent;
				if (par.variations.Count() > 1)
				{
					par.variations.Remove(s);
				}
			});

			BtApplyForAllVariationsTapped = new DelegateCommand(async () =>
			{
				if (!string.IsNullOrEmpty(this.TbItemPrice) && !string.IsNullOrEmpty(this.TbItemStock))
				{
					if (this._isNumberic == true)
					{
						foreach (var Va in this.ListVariations[0].variations)
						{
							foreach (var subVa in Va.subVariations)
							{
								subVa.Price = this.TbItemPrice;
								subVa.Stock = this.TbItemStock;
							}
						}
					}
					else
					{
						await new MessageDialog("Product price/stock must be a number").ShowAsync();
					}
				}
				else
				{
					await new MessageDialog("Product price/stock could not be null").ShowAsync();
				}
			});

			BtAddNewImageTapped = new DelegateCommand<NewItemImages>(async (s) =>
			{
				if (this.ListImages.Where(p => p.Source != null).ToList().Count() >= 8)
				{
					await new MessageDialog("Maximum: 8 photos").ShowAsync();
					return;
				}
				var picker = new Windows.Storage.Pickers.FileOpenPicker();
				picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
				picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
				picker.FileTypeFilter.Add(".jpg");
				picker.FileTypeFilter.Add(".jpeg");
				picker.FileTypeFilter.Add(".png");
				var files = await picker.PickMultipleFilesAsync();
				if (files.Count() > 0)
				{
					this.ListImages.Remove(s);
					foreach (var file in files)
					{
						if (this.ListImages.Where(p => p.Source != null).ToList().Count() < 8)
						{
							using (Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
							{
								BitmapImage bitmapImage = new BitmapImage();
								bitmapImage.SetSource(fileStream);
								this.ListImages.Add(new NewItemImages()
								{
									Source = bitmapImage,
									sourceStorageFile = file,
									Token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file),
									isAddImageShown = Visibility.Collapsed,
									isCanDeleteShown = Visibility.Visible,
								});
							}
						}
					}
					if (this.ListImages.Count() < 8)
					{
						this.ListImages.Add(new NewItemImages());
					}
				}
			});
		}

		private void __UpdatePriceTable(Variations sender)
		{

			if (sender.Parent.index == 1)
			{
				if (this.ListVariations.Count() == 2)
				{
					foreach (var s in this.ListVariations[1].variations)
					{
						var sub = new SubVariations();
						sub.Option = s.Option;
						sub.Price = s.Price;
						sub.Sku = s.Sku;
						sub.Stock = s.Stock;
						sender.subVariations.Add(sub);
					}
				}
			}
			if (sender.Parent.index == 2)
			{
				foreach (var v in this.ListVariations[0].variations)
				{
					if (sender.index <= v.subVariations.Count())
					{
						v.subVariations[sender.index - 1].Option = sender.Option;
					}
					else
					{
						var sub = new SubVariations();
						sub.Option = sender.Option;
						sub.Price = sender.Price;
						sub.Sku = sender.Sku;
						sub.Stock = sender.Stock;
						v.subVariations.Add(sub);
					}
				}
			}
		}

		private async void _getCategoryAttributeCallback(IAsyncResult asynchronousResult)
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
						var fullData = Newtonsoft.Json.JsonConvert.DeserializeObject<FullAttributeCategories>(resultContent);
						if (fullData != null && fullData.categories != null && fullData.categories.Count() > 0)
						{
							this.modelID = fullData.categories[0].meta.modelid;
							this.ListFullAttribute = fullData.categories[0].attributes;
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

		private bool _checkTextBoxValidate(string strTest)
		{
			bool ret = true;
			double testVal;
			if (!double.TryParse(strTest, out testVal))
			{
				ret = false;
			}
			return ret;
		}

		private async Task<ProductDetail_V3> _createNewItemInstance()
		{
			ProductDetail_V3 newItem = new ProductDetail_V3();
			newItem.id = 0;
			newItem.name = this.TbItemName;
			var getBrand = this.ListFullAttribute.Where(p => p.name == "brand").ToList();
			if (getBrand != null && getBrand.Count() > 0)
			{

			}
			else
			{
				newItem.brand = "No brand";
			}
			newItem.size_chart = "";
			if (this.isMultiVariations ==  true && !string.IsNullOrEmpty(this.ListVariations[0].Name))
			{
				newItem.stock = 0;
				newItem.price = this.TbItemPrice;
				newItem.tier_variation = new List<TierVariation>();
				foreach(var _va in this.ListVariations)
				{
					var tier = new TierVariation();
					tier.images = new List<string>();
					tier.name = _va.Name;
					tier.options = new List<string>();
					foreach(var subVar in _va.variations)
					{
						tier.options.Add(subVar.Option);
					}
					newItem.tier_variation.Add(tier);
				}
			}
			else
			{
				newItem.tier_variation = new List<TierVariation>();
				newItem.stock = int.Parse(this.TbItemStock);
				newItem.price = this.TbItemPrice;
			}
			newItem.parent_sku = "";
			newItem.condition = 1;
			newItem.price_before_discount = "";
			newItem.description = this.TbItemDescription;
			newItem.category_path = new List<UInt64?>() { this.SelectedMainCategory.main.catid, this.SelectedSubCategory.catid, this.SelectedSubSubCategory.catid };
			newItem.attribute_model = new AttributeModel();
			newItem.attribute_model.attribute_model_id = this.modelID;
			newItem.attribute_model.attributes = new List<Attribute_V3>();
			foreach (var att in this.ListFullAttribute)
			{
				var attribute_tmp = new Attribute_V3();
				attribute_tmp.attribute_id = att.id;
				attribute_tmp.prefill = false;
				attribute_tmp.status = 0;
				attribute_tmp.value = att.data.multi_lang[0].Choose;
				newItem.attribute_model.attributes.Add(attribute_tmp);
			}

			newItem.wholesale_list = new List<object>();
			if (StaticResources.ShopeePreConfig != null)
			{
				switch (StaticResources.ShortRegion)
				{
					case "VN":
						newItem.weight = StaticResources.ShopeePreConfig.weight;
						break;
					case "SG":
						newItem.weight = (double.Parse(StaticResources.ShopeePreConfig.weight) / 1000.0).ToString();
						break;
					case "MA":
						newItem.weight = (double.Parse(StaticResources.ShopeePreConfig.weight) / 1000.0).ToString();
						break;
					case "ID":
						newItem.weight = StaticResources.ShopeePreConfig.weight;
						break;
					case "PH":
						newItem.weight = StaticResources.ShopeePreConfig.weight;
						break;
					default:
						break;
				}
				newItem.dimension = new Dimension()
				{
					height = int.Parse(StaticResources.ShopeePreConfig.height),
					length = int.Parse(StaticResources.ShopeePreConfig.length),
					width = int.Parse(StaticResources.ShopeePreConfig.width),
				};
				newItem.pre_order = StaticResources.ShopeePreConfig.isPreOrder;
				newItem.days_to_ship = StaticResources.ShopeePreConfig.estDate;
			}
			newItem.add_on_deal = new List<object>();
			newItem.logistics_channels = new List<LogisticsChannel_V3>();
			if (StaticResources.LogisticsChannel == null || StaticResources.LogisticsChannel.Count() <= 0)
			{
				await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningConfig).ShowAsync();
				StaticResources.NavigationFrame.Navigate(typeof(ShopeeConfigPage), null, new DrillInNavigationTransitionInfo());
				return null;
			}
			foreach (var channel in StaticResources.LogisticsChannel)
			{
				var channel_tmp = new LogisticsChannel_V3();
				channel_tmp.channelid = channel.channelid;
				channel_tmp.id = channel.id;
				channel_tmp.sizeid = channel.sizeid;
				channel_tmp.size = channel.size;
				channel_tmp.price = channel.price;
				channel_tmp.cover_shipping_fee = channel.cover_shipping_fee == 1 ? true : false;
				channel_tmp.enabled = channel.enabled == 1 ? true : false;
				channel_tmp.item_flag = channel.item_flag;
				newItem.logistics_channels.Add(channel_tmp);
			}
			newItem.unlisted = false;
			newItem.model_list = new List<ModelList>();
			for (int i = 0; i < this.ListVariations[0].variations.Count(); i++)
			{
				for (int j = 0; j < this.ListVariations[0].variations[i].subVariations.Count(); j++)
				{
					var model_tmp = new ModelList();
					model_tmp.id = 0;
					model_tmp.name = "";
					model_tmp.stock = int.Parse(this.ListVariations[0].variations[i].subVariations[j].Stock);
					model_tmp.price = this.ListVariations[0].variations[i].subVariations[j].Price;
					model_tmp.sku = string.IsNullOrEmpty(this.ListVariations[0].variations[i].subVariations[j].Sku) ? "" : this.ListVariations[0].variations[i].subVariations[j].Sku;
					model_tmp.tier_index = new List<int>() { i, j };
					newItem.model_list.Add(model_tmp);
				}
			}
			newItem.installment_tenures = new InstallmentTenures();
			newItem.category_recommend = new List<List<int>>();
			return newItem;
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

		public ObservableCollection<FullAttribute> ListFullAttribute
		{
			get
			{
				if (this._listFullAttribute == null)
				{
					this._listFullAttribute = new ObservableCollection<FullAttribute>();
				}
				return this._listFullAttribute;
			}
			set
			{
				SetProperty(ref this._listFullAttribute, value);
			}
		}

		public ObservableCollection<NewItemImages> ListImages
		{
			get
			{
				if (this._listImages == null)
				{
					this._listImages = new ObservableCollection<NewItemImages>();
				}
				return this._listImages;
			}
			set
			{
				SetProperty(ref this._listImages, value);
			}
		}

		public ObservableCollection<ItemVariations> ListVariations
		{
			get
			{
				if (this._listVariations == null)
				{
					this._listVariations = new ObservableCollection<ItemVariations>();
				}
				return this._listVariations;
			}
			set
			{
				SetProperty(ref this._listVariations, value);
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
						this.SelectedSubSubCategory = null;
						this.ListFullAttribute.Clear();
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
						this.ListSubSubCategories = this._selectedSubCategory.sub_sub;
						this.ListFullAttribute.Clear();
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
					if (this._selectedSubSubCategory != null)
					{
						Utility.getCategoryAttributeAsync(StaticResources.SelectedShopLogin, this._selectedSubSubCategory.catid, _getCategoryAttributeCallback);
					}
				}
			}
		}

		public Visibility isMultiVariationPricingShown
		{
			get => this._isMultiVariationPricingShown;
			set => SetProperty(ref this._isMultiVariationPricingShown, value);
		}

		public Visibility isAVariationPricingShown
		{
			get => this._isAVariationPricingShown;
			set => SetProperty(ref this._isAVariationPricingShown, value);
		}

		public string TbItemPrice
		{
			get => this._tbItemPrice;
			set
			{
				SetProperty(ref this._tbItemPrice, value);
				if (this._checkTextBoxValidate(this._tbItemPrice) == false)
				{
					this.isWarningTbxPriceShown = Visibility.Visible;
					this._isNumberic = false;
				}
				else
				{
					this.isWarningTbxPriceShown = Visibility.Collapsed;
					this._isNumberic = true;
				}
			}
		}

		public string TbItemStock
		{
			get => this._tbItemStock;
			set
			{
				SetProperty(ref this._tbItemStock, value);
				if (this._checkTextBoxValidate(this._tbItemStock) == false)
				{
					this.isWarningTbxStockShown = Visibility.Visible;
					this._isNumberic = false;
				}
				else
				{
					this.isWarningTbxStockShown = Visibility.Collapsed;
					this._isNumberic = true;
				}
			}
		}

		public string TbItemName
		{
			get => this._tbItemName;
			set => SetProperty(ref this._tbItemName, value);
		}

		public bool isMultiVariations
		{
			get => this._isMultiVariations;
			set => SetProperty(ref this._isMultiVariations, value);
		}

		public Visibility isWarningTbxPriceShown
		{
			get => this._isWarningTbxPriceShown;
			set => SetProperty(ref this._isWarningTbxPriceShown, value);
		}
		public Visibility isWarningTbxStockShown
		{
			get => this._isWarningTbxStockShown;
			set => SetProperty(ref this._isWarningTbxStockShown, value);
		}

		public string TbItemDescription
		{
			get => this._tbItemDescription;
			set => SetProperty(ref this._tbItemDescription, value);
		}

		public string TgGrImgProcessing
		{
			get => this._tgGrImgProcessing;
			set => SetProperty(ref this._tgGrImgProcessing, value);
		}

		public NewItemImages SelectedImage
		{
			get => this._selectedImage;
			set
			{
				if (this._selectedImage != value)
				{
					if (this._selectedImage != null)
					{
						this._selectedImage.isChecked = false;
					}
					SetProperty(ref this._selectedImage, value);
					if (this._selectedImage != null)
					{
						this._selectedImage.isChecked = true;
					}
				}
			}
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

	}
}
