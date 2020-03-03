using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ShopeeManagement.Models
{
	public class PageInfo
	{
		public int total { get; set; }
		public int page_number { get; set; }
		public int page_size { get; set; }
	}

	public class OngoingUpcomingCampaigns
	{
		public List<object> upcoming_campaigns { get; set; }
		public List<object> ongoing_campaigns { get; set; }
	}

	public class InstallmentTenures
	{
		//public int status { get; set; }
		//public List<object> enables { get; set; }
	}

	public class ListProduct : ViewModelBase
	{
		private DispatcherTimer timer;
		public ListProduct()
		{
			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Tick += (s, e) =>
			{
				this.CountDown--;
				if (this.CountDown== 0)
				{
					timer.Stop();
					OnThresholdReached(null);
					this.isCanBoost = true;
				}
			};
		}

		protected virtual void OnThresholdReached(EventArgs e)
		{
			EventHandler handler = CountDownSuccess;
			handler?.Invoke(this, e);
		}

		public event EventHandler CountDownSuccess;

		private bool _isCanDownload =  true;
		public bool isCanDownload
		{
			get
			{
				return this._isCanDownload;
			}
			set
			{
				SetProperty(ref this._isCanDownload, value);
			}
		}

		private bool _isCanBoost = true;
		public bool isCanBoost
		{
			get
			{
				return this._isCanBoost;
			}
			set
			{
				SetProperty(ref this._isCanBoost, value);
			}
		}

		private string _image;
		public string image
		{
			get
			{
				return this._image;
			}
			set
			{
				SetProperty(ref this._image, value);
			}
		}
		private int _count_down;
		public int CountDown
		{
			get
			{
				return this._count_down;
			}
			set
			{
				SetProperty(ref this._count_down, value);
			}
		}
		private int _boost_cool_down_seconds;
		public int boost_cool_down_seconds
		{
			get
			{
				return this._boost_cool_down_seconds;
			}
			set
			{
				SetProperty(ref this._boost_cool_down_seconds, value);
				if (this._boost_cool_down_seconds > 0)
				{
					this.isCanBoost = false;
					this.CountDown = this.boost_cool_down_seconds;
					timer.Start();
				}
			}
		}
		public List<object> add_on_deal { get; set; }
		public int like_count { get; set; }
		public List<string> images { get; set; }
		public string price_before_discount { get; set; }
		public UInt64 id { get; set; }
		public OngoingUpcomingCampaigns ongoing_upcoming_campaigns { get; set; }
		public bool in_promotion { get; set; }
		public int view_count { get; set; }
		public bool use_wholesale { get; set; }
		public int attribute_status { get; set; }
		public string price_min { get; set; }
		public List<object> model_list { get; set; }
		public string price { get; set; }
		public int stock { get; set; }
		public int status { get; set; }
		public string price_max { get; set; }
		public object banned_infos { get; set; }
		public int category_status { get; set; }
		public List<object> wholesale_list { get; set; }
		public int flag { get; set; }
		public int sold { get; set; }
		public string parent_sku { get; set; }
		public List<int> category_path { get; set; }
		public bool use_bundle_sale { get; set; }
		public string name { get; set; }
		public InstallmentTenures installment_tenures { get; set; }
		public object bundle_deal { get; set; }
		public int create_time { get; set; }
		public int modify_time { get; set; }
	}

	public class Data
	{
		public PageInfo page_info { get; set; }
		public ObservableCollection<ListProduct> list { get; set; }
	}

	public class PageProductList
	{
		public string message { get; set; }
		public int code { get; set; }
		public Data data { get; set; }
		public string user_message { get; set; }
	}
}
