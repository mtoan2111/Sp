using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class Dimensions
	{
		public string width { get; set; }
		public string length { get; set; }
		public int unit { get; set; }
		public string height { get; set; }
	}

	public class Value
	{
		public int status { get; set; }
		public int attr_id { get; set; }
		public string value { get; set; }
		public bool prefill { get; set; }
	}

	public class Itemmodel
	{
		public string currency { get; set; }
		public object flash_sale { get; set; }
		public object flash_sale_status { get; set; }
		public object id { get; set; }
		public int itemid { get; set; }
		public object modelid { get; set; }
		public object name { get; set; }
		public Int64? price { get; set; }
		public object price_before_discount { get; set; }
		public object promo_source { get; set; }
		public object promo_stock { get; set; }
		public object promotion_refresh_time { get; set; }
		public object promotionid { get; set; }
		public object rebate_price { get; set; }
		public string sku { get; set; }
		public object status { get; set; }
		public int stock { get; set; }
		public List<int> tier_index { get; set; }
	}

	public class Attributes
	{
		public List<Value> values { get; set; }
		public UInt64? modelid { get; set; }
	}

	public class ongoing_upcoming_campaigns
	{
		public List<object> ongoing_campaigns { get; set; }
		public List<object> upcoming_campaigns { get; set; }
	}

	public class newSubCategories
	{
		public List<UInt64?> catid { get; set; } 
	}

	public class Product
	{
		public List<CatsRecommend> cats_recommend { get; set; }
		public int cmt_count { get; set; }
		public string weight { get; set; }
		public int bundle_deal_id { get; set; }
		public int promo_stock { get; set; }
		public UInt64? shopid { get; set; }
		public bool can_use_wholesale { get; set; }
		public int ctime { get; set; }
		public string currency { get; set; }
		public int promo_source { get; set; }
		public int cooldown_seconds { get; set; }
		public bool can_use_bundle_sale { get; set; }
		public List<string> images { get; set; }
		public int estimated_days { get; set; }
		public int id { get; set; }
		public ongoing_upcoming_campaigns ongoing_upcoming_campaigns { get; set; }
		public int view_count { get; set; }
		public List<Itemmodel> itemmodels { get; set; }
		public int catid { get; set; }
		public string product_command { get; set; }
		public string price_min { get; set; }
		public List<object> wholesale_tier_list { get; set; }
		public List<object> rating_count { get; set; }
		public int liked_count { get; set; }
		public string flash_sale_status { get; set; }
		public ObservableCollection<LogisticsChannelClone> logistics_channel { get; set; }
		public string brand { get; set; }
		public string video_list { get; set; }
		public int stock { get; set; }
		public int status { get; set; }
		public string price_max { get; set; }
		public string description { get; set; }
		public string banned_infos { get; set; }
		public int promotion_refresh_time { get; set; }
		public Int64? price { get; set; }
		public int cat_status { get; set; }
		public int flag { get; set; }
		public List<List<UInt64?>> new_subcategories { get; set; }
		public int sold { get; set; }
		public string parent_sku { get; set; }
		public int condition { get; set; }
		public Dimensions dimensions { get; set; }
		public string price_before_discount { get; set; }
		public string name { get; set; }
		public double rating_star { get; set; }
		public string size_chart { get; set; }
		public bool is_pre_order { get; set; }
		public object flash_sale { get; set; }
		public Attributes attributes { get; set; }
		public int dts_lock { get; set; }
		public int self_discountid { get; set; }
		[JsonProperty(PropertyName = "lock")]
		public object _lock { get; set; }
		public object approved_flash_sale { get; set; }
		public List<object> subcategories { get; set; }
		public List<TierVariation> tier_variation { get; set; }
		public InstallmentTenures installment_tenures { get; set; }


		public bool unlist { get; set; }
	}

	public class Subcategory
	{
		public UInt64? id { get; set; }
		public UInt64? parentid { get; set; }
	}

	public class ProductDetails
	{
		public Product product { get; set; }
		//public List<Subcategory> subcategories { get; set; }
		//[JsonProperty(PropertyName = "logistics-channels")]
		//public List<LogisticsChannel> logistics_channels { get; set; }
		//[JsonProperty(PropertyName = "item-models")]
		//public List<object> item_models { get; set; }
		//[JsonProperty(PropertyName = "bundle-deals")]
		//public List<object> bundle_deals { get; set; }
		//[JsonProperty(PropertyName = "logistics-sizes")]
		//public List<object> logistics_sizes { get; set; }
	}
}
