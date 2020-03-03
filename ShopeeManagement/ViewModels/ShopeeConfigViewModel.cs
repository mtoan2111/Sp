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

namespace ShopeeManagement.ViewModels
{
    public class ShopeeConfigViewModel : ViewModelBase
    {
		public ICommand BtCheckShippingFeeTapped { get; set; }
		public ICommand BtApplyConfigTapped { get; set; }
		public ICommand ShopeeConfigPageLoading { get; set; }
		public ICommand ShopeeConfigPageOnDisappearing { get; set; }

		private ObservableCollection<LogisticsChannel> _lstLogisticsChannel;
		private ObservableCollection<LogisticsChannelClone> _lstLogisticsChannelClone;
		private List<LogisticsSize> ListLogisticSize = new List<LogisticsSize>();

		private ObservableCollection<Categories> _listCategories;
		private ObservableCollection<SubCategory> _listSubCategories;

		private Categories _selectedMainCategory;
		private SubCategory _selectedSubCategory;

		private Visibility _isWarningTbxWeightShown;
		private Visibility _isWarningTbxWidthShown;
		private Visibility _isWarningTbxHeightShown;
		private Visibility _isWarningTbxLengthShown;
		private Visibility _isSubCategoriesShown;
		private Visibility _isSetShippingFeeShown;
		private Visibility _isWarningTbxShippingFeeShown;
		private Visibility _isWarningTbxDayShown;

		private string _tbxWeightWarning;
		private string _tbxWidthWarning;
		private string _tbxHeightWarning;
		private string _tbxLengthWarning;
		private string _tbxShippingWarning;
		private string _tbEstimatedDate;
		private string _tbWeight = "500";
		private string _tbWidth = "20";
		private string _tbHeight = "20";
		private string _tbLength = "20";
		private string _tbShippingFee;
		private string _tbxWeight;
		private string _tbxWidth;
		private string _tbxHeight;
		private string _tbxLength;
		private string _tbxDimensionSection;
		private string _tbxMainCategory;
		private string _cbxChoosingMainCategory;
		private string _tbxSubCategory;
		private string _tbxChooseSubCategory;
		private string _tbxCategorySection;
		private string _tbxEstDate;
		private string _tbxDay;
		private string _tbxPreOrder;
		private string _tbxShippingSection;
		private string _tbxCheckShippingFee;
		private string _tbxLogisticChannel;
		private string _tbxMaxWeight;
		private string _tbxMinWeight;
		private string _tbxMaxHeight;
		private string _tbxMaxWidth;
		private string _tbxMaxLength;
		private string _tbxSaveConfig;
		private string _tbxShippingFee;
		private string _tbxRestric;
		private string _tbxSetShippingFee;
		private string _tbxCoverShippingFee;
		private string _tbxShippingFeeSection;
		private string _tbxShippingFeeWarning;
		private string _dimensionConfigFileName;
		private string _logisticsChannelsFileName;
		private string _tbxAnnounce;
		private string _tbxAutomatic;
		private string _tbxDefault;

		private bool _isBtCheckShippingFeeEnable = true;
		private bool _isPreorder = false;
		private bool _isCoverShippingFee = false;

		private bool _isCheckShippingFee = false;
		public ShopeeConfigViewModel()
		{

			this.TbxWeight = StaticResources.choosingLanguage.ConfigPageWeight;
			this.TbxWidth = StaticResources.choosingLanguage.ConfigPageWidth;
			this.TbxHeight = StaticResources.choosingLanguage.ConfigPageHeight;
			this.TbxLength = StaticResources.choosingLanguage.ConfigPageLength;
			this.TbxDimensionSection = StaticResources.choosingLanguage.ConfigPageDimensionConfigSection;
			this.TbxMainCategory = StaticResources.choosingLanguage.ConfigPageMainCategory;
			this.CbxChoosingMainCategory = StaticResources.choosingLanguage.ConfigPageCbxChoosingMainCategory;
			this.TbxSubCategory = StaticResources.choosingLanguage.ConfigPageSubCategory;
			this.TbxChooseSubCategory = StaticResources.choosingLanguage.ConfigPageCbxChoosingSubCategory;
			this.TbxCategorySection = StaticResources.choosingLanguage.ConfigPageCategoryConfigSection;
			this.TbxEstDate = StaticResources.choosingLanguage.ConfigPageEstDate;
			this.TbxDay = StaticResources.choosingLanguage.ConfigPageDay;
			this.TbxPreOrder = StaticResources.choosingLanguage.ConfigPagePreOder;
			this.TbxShippingSection = StaticResources.choosingLanguage.ConfigPageShippingConfigSection;
			this.TbxCheckShippingFee = StaticResources.choosingLanguage.ConfigPageCheckShippingFee;
			this.TbxLogisticChannel = StaticResources.choosingLanguage.ConfigPageLogisticChannelHeader;
			this.TbxMaxWeight = StaticResources.choosingLanguage.ConfigPageMaxWeight;
			this.TbxMinWeight = StaticResources.choosingLanguage.ConfigPageMinWeight;
			this.TbxMaxHeight = StaticResources.choosingLanguage.ConfigPageMaxHeight;
			this.TbxMaxWidth = StaticResources.choosingLanguage.ConfigPageMaxWidth;
			this.TbxMaxLength = StaticResources.choosingLanguage.ConfigPageMaxLength;
			this.TbxSaveConfig = StaticResources.choosingLanguage.ConfigPageSaveConfig;
			this.TbxShippingFee = StaticResources.choosingLanguage.ConfigPageShipingFee;
			this.TbxRestric = StaticResources.choosingLanguage.ConfigPageRestric;
			this.TbxShippingFee = StaticResources.choosingLanguage.ConfigPageShipingFee;
			this.TbxCoverShippingFee = StaticResources.choosingLanguage.ConfigPageTbxCoverShippingFee;
			this.TbxShippingFeeSection = StaticResources.choosingLanguage.ConfigPageShippingFeeSection;
			this.TbxSetShippingFee = StaticResources.choosingLanguage.ConfigPageSetShippingFee;
			this.TbxAnnounce = StaticResources.choosingLanguage.ConfigPageAnnoucement;
			this.TbxAutomatic = StaticResources.choosingLanguage.ConfigPageAutomation;
			this.TbxDefault = StaticResources.choosingLanguage.ConfigPageDefault;

			ShopeeConfigPageOnDisappearing = new DelegateCommand(() =>
			{

			});

			if (StaticResources.ShortRegion == "SG")
			{
				this.isSetShippingFeeShown = Visibility.Visible;
			}
			else
			{
				this.isSetShippingFeeShown = Visibility.Collapsed;
			}

			switch (StaticResources.ShortRegion)
			{
				case "VN":
					this.TbEstimatedDate = "2";
					this.isSetShippingFeeShown = Visibility.Collapsed;
					this._dimensionConfigFileName = "/DimensionConfig.json";
					this._logisticsChannelsFileName = "/LogisticsConfig.json";
					break;
				case "SG":
					this.TbEstimatedDate = "2";
					this.isSetShippingFeeShown = Visibility.Visible;
					this._dimensionConfigFileName = "/DimensionConfigSG.json";
					this._logisticsChannelsFileName = "/LogisticsConfigSG.json";
					break;
				case "MA":
					this.TbEstimatedDate = "2";
					this.isSetShippingFeeShown = Visibility.Visible;
					this._dimensionConfigFileName = "/DimensionConfigMA.json";
					this._logisticsChannelsFileName = "/LogisticsConfigMA.json";
					break;
				case "ID":
					this.TbEstimatedDate = "2";
					this.isSetShippingFeeShown = Visibility.Visible;
					this._dimensionConfigFileName = "/DimensionConfigID.json";
					this._logisticsChannelsFileName = "/LogisticsConfigID.json";
					break;
				case "PH":
					this.TbEstimatedDate = "3";
					this.isSetShippingFeeShown = Visibility.Visible;
					this._dimensionConfigFileName = "/DimensionConfigPH.json";
					this._logisticsChannelsFileName = "/LogisticsConfigPH.json";
					break;
				default:
					break;
			}

			ShopeeConfigPageLoading = new DelegateCommand(async () =>
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
				//this.isSubCategoriesShown = Visibility.Collapsed;
				//if (StaticResources.ListCategories.Count() == 0)
				//{
				//	if (StaticResources.SelectedShopLogin != null)
				//	{
				//		Utility.getCategories(StaticResources.SelectedShopLogin, new AsyncCallback(_getCategoriesCallback));
				//	}
				//}
				//else
				//{
					//this.ListCategories = StaticResources.ListCategories;
					if (StaticResources.ShopeePreConfig != null)
					{
						this.TbHeight = StaticResources.ShopeePreConfig.height;
						this.TbLength = StaticResources.ShopeePreConfig.length;
						this.TbWeight = StaticResources.ShopeePreConfig.weight;
						this.TbWidth = StaticResources.ShopeePreConfig.width;
						this.isPreorder = StaticResources.ShopeePreConfig.isPreOrder;
						this.TbEstimatedDate = StaticResources.ShopeePreConfig.estDate.ToString();
						this.TbShippingFee = StaticResources.ShopeePreConfig.shippingfee;
						this.isCoverShippingFee = StaticResources.ShopeePreConfig.isCoverFee;
						//this.SelectedMainCategory = ListCategories.Single(p => p.main.catid == StaticResources.ShopeePreConfig.parCatID);
						//this.SelectedSubCategory = ListSubCategories.Single(p => p.catid == StaticResources.ShopeePreConfig.subCatID);
						if (StaticResources.LogisticsChannel != null)
						{
							this.ListLogisticsChannelClone = StaticResources.LogisticsChannel;
							//Utility.getLogisticChannel(StaticResources.SelectedShopLogin, new AsyncCallback(_getLogisticChannelCallback));
						}
						else
						{
							if (StaticResources.SelectedShopLogin != null)
							{
								Utility.getLogisticChannel(StaticResources.SelectedShopLogin, new AsyncCallback(_getLogisticChannelCallback));
							}
						}
					}
					else
					{
						if (StaticResources.SelectedShopLogin != null)
						{
							Utility.getLogisticChannel(StaticResources.SelectedShopLogin, new AsyncCallback(_getLogisticChannelCallback));
						}
					}
				//}

			}); 

			BtApplyConfigTapped = new DelegateCommand(() =>
			{
				//if (_isCheckShippingFee != true)
				//{
				//	await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningCheckShippingFeeFirst).ShowAsync();
				//	return;
				//}
				var dimensionCfg = new ShopeePreConfig
				{
					height = this.TbHeight,
					length = this.TbLength,
					weight = this.TbWeight,
					width = this.TbWidth,
					isPreOrder = this.isPreorder,
					estDate = int.Parse(this.TbEstimatedDate),
					isCoverFee = this.isCoverShippingFee,
					shippingfee = this.TbShippingFee,
				};
				StaticResources.ShopeePreConfig = dimensionCfg;
				StaticResources.LogisticsChannel = this.ListLogisticsChannelClone;
				try
				{
					var path = ApplicationData.Current.LocalFolder.Path;
					using (var jsonWrite = new StreamWriter(path + this._logisticsChannelsFileName))
					{
						jsonWrite.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ListLogisticsChannelClone));
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
				try
				{
					var path = ApplicationData.Current.LocalFolder.Path;
					using (var jsonWrite = new StreamWriter(path + this._dimensionConfigFileName))
					{
						jsonWrite.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(dimensionCfg));
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			});

			//BtCheckShippingFeeTapped = new DelegateCommand(async () =>
			//{
			//	var weightTemp = this.TbWeight;
			//	switch (StaticResources.ShortRegion)
			//	{
			//		case "VN":
			//			weightTemp = this.TbWeight;
			//			break;
			//		case "SG":
			//			weightTemp = (double.Parse(this.TbWeight) / 1000.0).ToString();
			//			break;
			//		case "MA":
			//			weightTemp = (double.Parse(this.TbWeight) / 1000.0).ToString();
			//			break;
			//		case "ID":
			//			weightTemp = this.TbWeight;
			//			break;
			//		case "PH":
			//			weightTemp = this.TbWeight;
			//			break;
			//		default:
			//			break;
			//	}
			//	foreach (var channel in ListLogisticsChannelClone)
			//	{
			//		channel.cod_whitelist_enabled = 0;
			//		channel.cod_enabled = 0;
			//		//channel.level = 1;
			//		channel.save_into_item = true;
			//		//channel.sizeid = 0;
			//		switch (StaticResources.ShortRegion)
			//		{
			//			case "VN":
			//				channel.id = "0-" + channel.id;
			//				break;
			//			case "SG":
			//				channel.id = channel.id;
			//				break;
			//			case "MA":
			//				channel.id = channel.id;
			//				break;
			//			case "ID":
			//				channel.id = "New-" + channel.id;
			//				break;
			//			case "PH":
			//				channel.id = channel.id;
			//				break;
			//			default:
			//				break;
			//		}
			//		//channel.id = StaticResources.ShortRegion == "SG" ? channel.id : "0-" + channel.id;
			//		channel.enable_massship = 0;
			//		if (this.SelectedSubCategory == null)
			//		{
			//			await new MessageDialog(StaticResources.choosingLanguage.GlobalWarningChooseSubCategoryFirst).ShowAsync();
			//			return;
			//		}
			//		new Thread(() =>
			//		{
			//			Utility.getShippingFee(StaticResources.SelectedShopLogin, channel, weightTemp, this.TbWidth, this.TbHeight, this.TbLength, this.SelectedSubCategory.catid);
			//			this._isCheckShippingFee = true;
			//		}).Start();
			//	}
			//});
		}

		private async void _getLogisticChannelCallback(IAsyncResult asynchronousResult)
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
						var dumpjson = Newtonsoft.Json.JsonConvert.DeserializeObject<LogisticsChannels>(resultContent);
						this.ListLogisticSize = dumpjson.logistics_sizes;
						var listChannel =  new ObservableCollection<LogisticsChannel>(dumpjson.logistics_channels.Where(p => p.enabled == 1).ToList());
						if (listChannel.Count() != this.ListLogisticsChannelClone.Count())
						{
							foreach(var channel in listChannel)
							{
								LogisticsChannelClone channelClone = new LogisticsChannelClone();
								if (double.Parse(channel.price) > 0)
								{
									channel.supported = true;
								}
								this.CloneLogisticChannel(channel, channelClone, this.ListLogisticSize);
								this.ListLogisticsChannelClone.Add(channelClone);
							}
						}
					});
				}
			}
			catch (WebException ex)
			{
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
						if (StaticResources.ShopeePreConfig != null)
						{
							this.TbHeight = StaticResources.ShopeePreConfig.height;
							this.TbLength = StaticResources.ShopeePreConfig.length;
							this.TbWeight = StaticResources.ShopeePreConfig.weight;
							this.TbWidth = StaticResources.ShopeePreConfig.width;
							this.isPreorder = StaticResources.ShopeePreConfig.isPreOrder;
							this.TbShippingFee = StaticResources.ShopeePreConfig.shippingfee;
							this.isCoverShippingFee = StaticResources.ShopeePreConfig.isCoverFee;
							this.TbEstimatedDate = StaticResources.ShopeePreConfig.estDate.ToString();
							this.SelectedMainCategory = ListCategories.Single(p => p.main.catid == StaticResources.ShopeePreConfig.parCatID);
							this.SelectedSubCategory = ListSubCategories.Single(p => p.catid == StaticResources.ShopeePreConfig.subCatID);
							if (StaticResources.LogisticsChannel != null)
							{
								this.ListLogisticsChannelClone = StaticResources.LogisticsChannel;
								Utility.getLogisticChannel(StaticResources.SelectedShopLogin, new AsyncCallback(_getLogisticChannelCallback));
							}
							else
							{
								Utility.getLogisticChannel(StaticResources.SelectedShopLogin, new AsyncCallback(_getLogisticChannelCallback));
							}
						}
						else
						{
							Utility.getLogisticChannel(StaticResources.SelectedShopLogin, new AsyncCallback(_getLogisticChannelCallback));
						}
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private void CloneLogisticChannel(LogisticsChannel channel, LogisticsChannelClone cloneChannel, List<LogisticsSize> sizes)
		{
			cloneChannel.size = channel.size;
			cloneChannel.is_shipping_fee_promotion_rule = channel.is_shipping_fee_promotion_rule;
			cloneChannel.sizeid = channel.sizeid;
			cloneChannel.enable_massship = channel.enable_massship;
			cloneChannel.discount_json = channel.discount_json;
			cloneChannel.id = channel.id;
			cloneChannel.item_flag = channel.item_flag;
			cloneChannel.category = channel.category;
			cloneChannel.mass_apply_prices = channel.mass_apply_prices;
			cloneChannel.display_name = channel.display_name;
			cloneChannel.cod_whitelist_enabled = channel.cod_whitelist_enabled;
			cloneChannel.priority = channel.priority;
			cloneChannel.icon = channel.icon;
			cloneChannel.desc_key = channel.desc_key;
			cloneChannel.price = channel.price;
			cloneChannel.preferred = channel.preferred;
			cloneChannel.discount = channel.discount;
			cloneChannel.flag = channel.flag;
			cloneChannel.name_key = channel.name_key;
			cloneChannel.level = channel.level;
			cloneChannel.cod_enabled = channel.cod_enabled;
			cloneChannel.extra_data = channel.extra_data;
			cloneChannel.name = channel.name;
			cloneChannel.limits = channel.limits;
			if (channel.sizes.Count() > 0)
			{
				var sizeID = channel.sizes[0];
				if (sizes.Count() > 0)
				{
					cloneChannel.sizes = new List<LogisticsSize>() { sizes[sizeID.GetValueOrDefault() - 1]};
				}
			}
			else
			{
				cloneChannel.sizes = new List<LogisticsSize>();
			}
			cloneChannel._default = channel._default;
			cloneChannel.country = channel.country;
			cloneChannel.channelid = channel.channelid;
			cloneChannel.enabled = channel.enabled;
			cloneChannel.cover_shipping_fee = channel.cover_shipping_fee;
			cloneChannel.command = channel.command;
			cloneChannel.save_into_item = channel.save_into_item;
			cloneChannel.supported = channel.supported;
		}

		private bool _checkTextBoxValidate(string strTest)
		{
			bool ret = true;
			double testVal;
			if(!double.TryParse(strTest, out testVal))
			{
				ret = false;
			}
			return ret;
		}

		public ObservableCollection<LogisticsChannel> ListLogisticsChannel
		{
			get
			{
				if (this._lstLogisticsChannel == null)
				{
					this._lstLogisticsChannel = new ObservableCollection<LogisticsChannel>();
				}
				return this._lstLogisticsChannel;
			}
			set
			{
				SetProperty(ref this._lstLogisticsChannel, value);
			}
		}

		public ObservableCollection<LogisticsChannelClone> ListLogisticsChannelClone
		{
			get
			{
				if (this._lstLogisticsChannelClone == null)
				{
					this._lstLogisticsChannelClone = new ObservableCollection<LogisticsChannelClone>();
				}
				return this._lstLogisticsChannelClone;
			}
			set
			{
				SetProperty(ref this._lstLogisticsChannelClone, value);
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
					}
					else
					{
						this.isSubCategoriesShown = Visibility.Collapsed;
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

					}
				}
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

		public string TbxWeightWarning
		{
			get
			{
				return this._tbxWeightWarning;
			}
			set
			{
				SetProperty(ref this._tbxWeightWarning, value);
			}
		}

		public string TbWeight
		{
			get
			{
				return this._tbWeight;
			}
			set
			{
				SetProperty(ref this._tbWeight, value);
				if(_checkTextBoxValidate(this._tbWeight) == false)
				{
					this.TbxWeightWarning = StaticResources.choosingLanguage.ConfigPageWeightWarning;
					this.isWarningTbxWeightShown = Visibility.Visible;
				}
				else
				{
					this.isWarningTbxWeightShown = Visibility.Collapsed;
				}
			}
		}
		public string TbWidth
		{
			get
			{
				return this._tbWidth;
			}
			set
			{
				SetProperty(ref this._tbWidth, value);
				if (_checkTextBoxValidate(this._tbWidth) == false)
				{
					this.TbxWidthWarning = StaticResources.choosingLanguage.ConfigPageWidthWarning;
					this.isWarningTbxWidthShown = Visibility.Visible;
				}
				else
				{
					this.isWarningTbxWidthShown = Visibility.Collapsed;
				}
			}
		}
		public string TbHeight
		{
			get
			{
				return this._tbHeight;
			}
			set
			{
				SetProperty(ref this._tbHeight, value);
				if (_checkTextBoxValidate(this._tbHeight) == false)
				{
					this.TbxHeightWarning = StaticResources.choosingLanguage.ConfigPageHeightWarning;
					this.isWarningTbxHeightShown = Visibility.Visible;
				}
				else
				{
					this.isWarningTbxHeightShown = Visibility.Collapsed;
				}
			}
		}
		public string TbLength
		{
			get
			{
				return this._tbLength;
			}
			set
			{
				SetProperty(ref this._tbLength, value);
				if (_checkTextBoxValidate(this._tbLength) == false)
				{
					this.TbxLengthWarning = StaticResources.choosingLanguage.ConfigPageLenghthWarning;
					this.isWarningTbxLengthShown = Visibility.Visible;
				}
				else
				{
					this.isWarningTbxLengthShown = Visibility.Collapsed;
				}
			}
		}

		public Visibility isWarningTbxWeightShown
		{
			get
			{
				return this._isWarningTbxWeightShown;
			}
			set
			{
				SetProperty(ref this._isWarningTbxWeightShown, value);
			}
		}

		public Visibility isWarningTbxWidthShown
		{
			get
			{
				return this._isWarningTbxWidthShown;
			}
			set
			{
				SetProperty(ref this._isWarningTbxWidthShown, value);
			}
		}

		public string TbxWidthWarning
		{
			get
			{
				return this._tbxWidthWarning;
			}
			set
			{
				SetProperty(ref this._tbxWidthWarning, value);
			}
		}

		public string TbxHeightWarning
		{
			get
			{
				return this._tbxHeightWarning;
			}
			set
			{
				SetProperty(ref this._tbxHeightWarning, value);
			}
		}

		public Visibility isWarningTbxHeightShown
		{
			get
			{
				return this._isWarningTbxHeightShown;
			}
			set
			{
				SetProperty(ref this._isWarningTbxHeightShown, value);
			}
		}

		public string TbxLengthWarning
		{
			get
			{
				return this._tbxLengthWarning;
			}
			set
			{
				SetProperty(ref this._tbxLengthWarning, value);
			}
		}

		public Visibility isWarningTbxLengthShown
		{
			get
			{
				return this._isWarningTbxLengthShown;
			}
			set
			{
				SetProperty(ref this._isWarningTbxLengthShown, value);
			}
		}

		public Visibility isSetShippingFeeShown
		{
			get => this._isSetShippingFeeShown;
			set => SetProperty(ref this._isSetShippingFeeShown, value);
		}

		public bool isBtCheckShippingFeeEnable
		{
			get
			{
				return this._isBtCheckShippingFeeEnable;
			}
			set
			{
				SetProperty(ref this._isBtCheckShippingFeeEnable, value);
			}
		}

		public bool isPreorder
		{
			get
			{
				return this._isPreorder;
			}
			set
			{
				SetProperty(ref this._isPreorder, value);
				if (this._isPreorder == false)
				{
					this.TbEstimatedDate = "2";
				}
			}
		}
		
		public string TbEstimatedDate
		{
			get
			{
				return this._tbEstimatedDate;
			}
			set
			{
				SetProperty(ref this._tbEstimatedDate, value);
				if (_checkTextBoxValidate(this._tbEstimatedDate) == false)
				{
					this.TbxShippingWarning = StaticResources.choosingLanguage.ConfigPageShippingWarning;
					this.isWarningTbxDayShown = Visibility.Visible;
				}
				else
				{
					this.isWarningTbxDayShown = Visibility.Collapsed;
				}
			}
		}

		public string TbxWeight
		{
			get => this._tbxWeight;
			set => SetProperty(ref this._tbxWeight, value);
		}
		public string TbxWidth
		{
			get => this._tbxWidth;
			set => SetProperty(ref this._tbxWidth, value);
		}
		public string TbxHeight
		{
			get => this._tbxHeight;
			set => SetProperty(ref this._tbxHeight, value);
		}
		public string TbxLength
		{
			get => this._tbxLength;
			set => SetProperty(ref this._tbxLength, value);
		}
		public string TbxDimensionSection
		{
			get => this._tbxDimensionSection;
			set => SetProperty(ref this._tbxDimensionSection, value);
		}
		public string TbxMainCategory
		{
			get => this._tbxMainCategory;
			set => SetProperty(ref this._tbxMainCategory, value);
		}
		public string CbxChoosingMainCategory
		{
			get => this._cbxChoosingMainCategory;
			set => SetProperty(ref this._cbxChoosingMainCategory, value);
		}
		public string TbxSubCategory
		{
			get => this._tbxSubCategory;
			set => SetProperty(ref this._tbxSubCategory, value);
		}
		public string TbxChooseSubCategory
		{
			get => this._tbxChooseSubCategory;
			set => SetProperty(ref this._tbxChooseSubCategory, value);
		}
		public string TbxCategorySection
		{
			get => this._tbxCategorySection;
			set => SetProperty(ref this._tbxCategorySection, value);
		}
		public string TbxEstDate
		{
			get => this._tbxEstDate;
			set => SetProperty(ref this._tbxEstDate, value);
		}
		public string TbxDay
		{
			get => this._tbxDay;
			set => SetProperty(ref this._tbxDay, value);
		}
		public string TbxPreOrder
		{
			get => this._tbxPreOrder;
			set => SetProperty(ref this._tbxPreOrder, value);
		}
		public string TbxShippingSection
		{
			get => this._tbxShippingSection;
			set => SetProperty(ref this._tbxShippingSection, value);
		}
		public string TbxCheckShippingFee
		{
			get => this._tbxCheckShippingFee;
			set => SetProperty(ref this._tbxCheckShippingFee, value);
		}
		public string TbxLogisticChannel
		{
			get => this._tbxLogisticChannel;
			set => SetProperty(ref this._tbxLogisticChannel, value);
		}
		public string TbxMaxWeight
		{
			get => this._tbxMaxWeight;
			set => SetProperty(ref this._tbxMaxWeight, value);
		}
		public string TbxMinWeight
		{
			get => this._tbxMinWeight;
			set => SetProperty(ref this._tbxMinWeight, value);
		}
		public string TbxMaxHeight
		{
			get => this._tbxMaxHeight;
			set => SetProperty(ref this._tbxMaxHeight, value);
		}
		public string TbxMaxWidth
		{
			get => this._tbxMaxWidth;
			set => SetProperty(ref this._tbxMaxWidth, value);
		}
		public string TbxMaxLength
		{
			get => this._tbxMaxLength;
			set => SetProperty(ref this._tbxMaxLength, value);
		}
		public string TbxSaveConfig
		{
			get => this._tbxSaveConfig;
			set => SetProperty(ref this._tbxSaveConfig, value);
		}
		public string TbxShippingFee
		{
			get => this._tbxShippingFee;
			set => SetProperty(ref this._tbxShippingFee, value);
		}
		public string TbxRestric
		{
			get => this._tbxRestric;
			set => SetProperty(ref this._tbxRestric, value);
		}
		public string TbxSetShippingFee
		{
			get => this._tbxSetShippingFee;
			set => SetProperty(ref this._tbxSetShippingFee, value);
		}
		public string TbxCoverShippingFee
		{
			get => this._tbxCoverShippingFee;
			set => SetProperty(ref this._tbxCoverShippingFee, value);
		}
		public string TbxShippingFeeSection
		{
			get => this._tbxShippingFeeSection;
			set => SetProperty(ref this._tbxShippingFeeSection, value);
		}

		public bool isCoverShippingFee
		{
			get => this._isCoverShippingFee;
			set
			{
				SetProperty(ref this._isCoverShippingFee, value);
				if (isCoverShippingFee == true)
				{
					this.TbShippingFee = "0.0";
				}
			}
		}
		public Visibility isWarningTbxShippingFeeShown
		{
			get => this._isWarningTbxShippingFeeShown;
			set => SetProperty(ref this._isWarningTbxShippingFeeShown, value);
		}
		public Visibility isWarningTbxDayShown
		{
			get => this._isWarningTbxDayShown;
			set => SetProperty(ref this._isWarningTbxDayShown, value);
		}
		public string TbxShippingWarning
		{
			get => this._tbxShippingWarning;
			set => SetProperty(ref this._tbxShippingWarning, value);
		}

		public string TbxShippingFeeWarning
		{
			get => this._tbxShippingWarning;
			set => SetProperty(ref this._tbxShippingWarning, value);
		}

		public string TbShippingFee
		{
			get => this._tbShippingFee;
			set
			{
				SetProperty(ref this._tbShippingFee, value);
				if (_checkTextBoxValidate(this._tbShippingFee) == false)
				{
					this.TbxShippingFeeWarning = StaticResources.choosingLanguage.ConfigPageFeeWarning;
					this.isWarningTbxShippingFeeShown = Visibility.Visible;
				}
				else
				{
					this.isWarningTbxShippingFeeShown = Visibility.Collapsed;
					if (this.isCoverShippingFee == false)
					{
						foreach (var channel in this.ListLogisticsChannelClone)
						{
							if (channel.supported != true)
							{
								channel.price = this._tbShippingFee;
							}
						}
					}
					else
					{
						foreach (var channel in this.ListLogisticsChannelClone)
						{
							if (channel.supported != true)
							{
								channel.price = "0.00";
							}
						}
					}
				}
			}
		}

		public string TbxDefault
		{
			get => this._tbxDefault;
			set => SetProperty(ref this._tbxDefault, value);
		}

		public string TbxAutomatic
		{
			get => this._tbxAutomatic;
			set => SetProperty(ref this._tbxAutomatic, value);
		}

		public string TbxAnnounce
		{
			get => this._tbxAnnounce;
			set => SetProperty(ref this._tbxAnnounce, value);
		}
	}
}
