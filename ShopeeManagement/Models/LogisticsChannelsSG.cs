using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class LimitsSG
	{
		public double item_min_weight { get; set; }
		public double order_min_weight { get; set; }
		public double checkout_min_weight { get; set; }
		public double? checkout_max_weight { get; set; }
		public double? order_max_weight { get; set; }
		public double? dimension_sum { get; set; }
		public double? item_max_weight { get; set; }
	}

	public class LogisticsChannelSG
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
		public string price { get; set; }
		public int preferred { get; set; }
		public double discount { get; set; }
		public string flag { get; set; }
		public string name_key { get; set; }
		public int level { get; set; }
		public int cod_enabled { get; set; }
		public string extra_data { get; set; }
		public string name { get; set; }
		public LimitsSG limits { get; set; }
		public List<object> sizes { get; set; }
		[JsonProperty(PropertyName = "default")]
		public int @default { get; set; }
		public string country { get; set; }
		public int channelid { get; set; }
		public int enabled { get; set; }
		public int cover_shipping_fee { get; set; }
		public string command { get; set; }
		public int save_into_item { get; set; }
	}



	public class LogisticsChannelsSG
	{
		[JsonProperty(PropertyName = "logistics-channels")]
		public ObservableCollection<LogisticsChannelSG> logistics_channels { get; set; }
		[JsonProperty(PropertyName = "logistics-sizes")]
		public List<LogisticsSize> logistics_sizes { get; set; }
}
}
