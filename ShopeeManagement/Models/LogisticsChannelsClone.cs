using Newtonsoft.Json;
using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class LogisticsChannelClone : ViewModelBase
	{
		public double size { get; set; }
		public bool is_shipping_fee_promotion_rule { get; set; }
		public int sizeid { get; set; }
		public int enable_massship { get; set; }
		public string discount_json { get; set; }
		public string id { get; set; }
		public string item_flag { get; set; }
		public int category { get; set; }
		public int mass_apply_prices { get; set; }
		public string display_name { get; set; }
		public int cod_whitelist_enabled { get; set; }
		public int priority { get; set; }
		public string icon { get; set; }
		public string desc_key { get; set; }
		private string _price;
		public string price
		{
			get
			{
				return this._price;
			}
			set
			{
				SetProperty(ref this._price, value);
			}
		}
		public int preferred { get; set; }
		public double discount { get; set; }
		public string flag { get; set; }
		public string name_key { get; set; }
		public int level { get; set; }
		public int cod_enabled { get; set; }
		public string extra_data { get; set; }
		public string name { get; set; }
		public Limits limits { get; set; }
		public List<LogisticsSize> sizes { get; set; }
		[JsonProperty(PropertyName = "default")]
		public int _default { get; set; }
		public string country { get; set; }
		public int channelid { get; set; }
		private int _enabled = 0;
		public int enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				SetProperty(ref this._enabled, value);
			}
		}
		public int cover_shipping_fee { get; set; }
		public string command { get; set; }
		public object save_into_item { get; set; }
		[JsonIgnore]
		public bool supported { get; set; }
	}
}
