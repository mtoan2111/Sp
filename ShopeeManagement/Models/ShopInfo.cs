using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ShopeeManagement.Models
{
	public class ShopInfo
	{
		public string username { get; set; }
		public UInt64? shopid { get; set; }
		[XmlIgnore]
		public int errocode { get; set; }
		public string phone { get; set; }
		[XmlIgnore]
		public string sso { get; set; }
		public string email { get; set; }
		public string token { get; set; }
		public string cs_token { get; set; }
		public string portrait { get; set; }
		public UInt64 id { get; set; }
		[XmlIgnore]
		public object sub_account_token { get; set; }
	}

	public class ShopInfoLogin : ViewModelBase
	{
		public ShopInfo Shop { get; set; }
		public string ShortRegion { get; set; }
		public List<ShortCookie> lstLoginCookie { get; set; }
		public string strLoginCookie { get; set; }

		private int _newMessageCount = 0;

		[XmlIgnore]
		public int NewMessageCount
		{
			get => this._newMessageCount;
			set
			{
				SetProperty(ref this._newMessageCount, value);
				if (this._newMessageCount == 0 && this._newOrderCount == 0)
				{
					this.BorderColor = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
				}
				else if (this._newMessageCount > 0)
				{
					this.BorderColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
				}
			}
		}

		private int _newOrderCount = 0;

		[XmlIgnore]
		public int NewOrderCount
		{
			get => this._newOrderCount;
			set
			{
				SetProperty(ref this._newOrderCount, value);
				if (this._newMessageCount == 0 && this._newOrderCount == 0)
				{
					this.BorderColor = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
				}
				else if (this._newOrderCount > 0)
				{
					this.BorderColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
				}
			}
		}

		private SolidColorBrush _borderColor = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
		[XmlIgnore]
		public SolidColorBrush BorderColor
		{
			get => this._borderColor;
			set => SetProperty(ref this._borderColor, value);
		}


		private string _shopStatus;
		[XmlIgnore]
		public string ShopStatus
		{
			get
			{
				return this._shopStatus;
			}
			set
			{
				SetProperty(ref this._shopStatus, value);
			}
		}
		[XmlIgnore]
		public string token { get; set; }
		private string _weekRevenue = string.Empty;
		[XmlIgnore]
		public string WeekRevenue
		{
			get => this._weekRevenue;
			set => SetProperty(ref this._weekRevenue, value);
		}

		private string _monthRevenue;
		[XmlIgnore]
		public string MonthRevenue
		{
			get => this._monthRevenue;
			set => SetProperty(ref this._monthRevenue, value);
		}

		private string _totalRevenue;
		[XmlIgnore]
		public string TotalRevenue
		{
			get => this._totalRevenue;
			set => SetProperty(ref this._totalRevenue, value);
		}
	}

	[XmlRoot("ListShop")]
	public class ListShopInfoLogin
	{
		[XmlElement(ElementName = "ShopInfoLogin")]
		public ObservableCollection<ShopInfoLogin> ShopInfo { get; set; }
	}
}
