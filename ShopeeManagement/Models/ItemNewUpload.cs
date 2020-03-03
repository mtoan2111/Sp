using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class CatsRecommend
	{
		public List<int> catid { get; set; }
	}


	public class NewItem
	{
		public List<CatsRecommend> cats_recommend { get; set; }
		public List<int> subcategories { get; set; }
		public int cmt_count { get; set; }
		public string weight { get; set; }
		public object bundle_deal_id { get; set; }
		public int promo_stock { get; set; }
		public int shopid { get; set; }
		public int can_use_wholesale { get; set; }
		public int ctime { get; set; }
		public string currency { get; set; }
		public int promo_source { get; set; }
		public int cooldown_seconds { get; set; }
		public bool can_use_bundle_sale { get; set; }
		public int mtime { get; set; }
		public List<string> images { get; set; }
		public int estimated_days { get; set; }
		public int id { get; set; }
		public string ongoing_upcoming_campaigns { get; set; }
		public int view_count { get; set; }
		public List<object> itemmodels { get; set; }
		public int catid { get; set; }
		public string product_command { get; set; }
		public string price_min { get; set; }
		public List<object> wholesale_tier_list { get; set; }
		public List<object> rating_count { get; set; }
		public int liked_count { get; set; }
		public string flash_sale_status { get; set; }
		public List<string> logistics_channel { get; set; }
		public string brand { get; set; }
		public string video_list { get; set; }
		public int stock { get; set; }
		public int status { get; set; }
		public string price_max { get; set; }
		public string description { get; set; }
		public string banned_infos { get; set; }
		public int promotion_refresh_time { get; set; }
		public string price { get; set; }
		public int cat_status { get; set; }
		public int flag { get; set; }
		public List<List<int>> new_subcategories { get; set; }
		public int sold { get; set; }
		public string parent_sku { get; set; }
		public int condition { get; set; }
		public Dimensions dimensions { get; set; }
		public string price_before_discount { get; set; }
		public string name { get; set; }
		public double rating_star { get; set; }
		public List<object> tier_variation { get; set; }
		public InstallmentTenures installment_tenures { get; set; }
		public string size_chart { get; set; }
		public bool is_pre_order { get; set; }
		public string flash_sale { get; set; }
		public Attributes attributes { get; set; }
		public bool dts_lock { get; set; }
		public int self_discountid { get; set; }
		public string approved_flash_sale { get; set; }
	}

	public class ItemNewUpload
	{
		public NewItem product { get; set; }
	}
}
