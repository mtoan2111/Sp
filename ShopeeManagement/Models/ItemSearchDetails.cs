using Newtonsoft.Json;
using ShopeeManagement.Base;
using ShopeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class CoinInfo
	{
		public int spend_cash_unit { get; set; }
		public List<object> coin_earn_items { get; set; }
	}

	public class VideoInfoList
	{
		public int duration { get; set; }
		public string video_id { get; set; }
		public string thumb_url { get; set; }
	}

	public class Category
	{
		public string display_name { get; set; }
		public UInt64? catid { get; set; }
		public object image { get; set; }
		public bool? no_sub { get; set; }
		public bool? is_default_subcat { get; set; }
		public object block_buyer_platform { get; set; }
	}

	public class Extinfo
	{
		public int? seller_promotion_limit { get; set; }
		public bool? has_shopee_promo { get; set; }
		public object group_buy_info { get; set; }
		public object holiday_mode_old_stock { get; set; }
		public List<int> tier_index { get; set; }
		public Int64? seller_promotion_refresh_time { get; set; }
	}

	public class Model
	{
		public UInt64? itemid { get; set; }
		public int status { get; set; }
		public string name { get; set; }
		public int promotionid { get; set; }
		public Int64? price { get; set; }
		public string currency { get; set; }
		public Extinfo extinfo { get; set; }
		public object price_before_discount { get; set; }
		public object modelid { get; set; }
		public int sold { get; set; }
		public int stock { get; set; }
	}

	public class TierVariation
	{
		public List<string> images { get; set; }
		public string name { get; set; }
		public List<string> options { get; set; }
	}

	public class Attribute
	{
		public bool is_pending_qc { get; set; }
		public int idx { get; set; }
		public string value { get; set; }
		public int id { get; set; }
		public bool is_timestamp { get; set; }
		public string name { get; set; }
	}

	public class ItemSearch : ViewModelBase, ICloneable<ItemSearch>
	{
		private bool _isCanDownload = true;
		[JsonIgnore]
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

		private int _index;
		public int index
		{
			get => this._index;
			set => SetProperty(ref this._index, value);
		}

		private string _uploadingErrorReason;
		public string UploadingErrorReason
		{
			get
			{
				return this._uploadingErrorReason;
			}
			set
			{
				SetProperty(ref this._uploadingErrorReason, value);
			}
		}

		public UInt64? itemid { get; set; }
		public object welcome_package_info { get; set; }
		public bool liked { get; set; }
		public object recommendation_info { get; set; }
		public object bundle_deal_info { get; set; }
		public Int64? price_max_before_discount { get; set; }
		public int bundle_deal_id { get; set; }
		public object slash_lowest_price { get; set; }
		public string image { get; set; }
		public CoinInfo coin_info { get; set; }
		public bool is_cc_installment_payment_eligible { get; set; }
		public UInt64? shopid { get; set; }
		public bool can_use_wholesale { get; set; }
		public object group_buy_info { get; set; }
		public string currency { get; set; }
		public Int64? raw_discount { get; set; }
		public bool show_free_shipping { get; set; }
		public List<VideoInfoList> video_info_list { get; set; }
		public List<string> images { get; set; }
        public List<string> processedImages { get; set; }
		public object installment_plans { get; set; }
		public Int64? price_before_discount { get; set; }
		public bool is_category_failed { get; set; }
		public int show_discount { get; set; }
		public int estimated_days { get; set; }
		public int cmt_count { get; set; }
		public object view_count { get; set; }
		public List<Category> categories { get; set; }
		public int catid { get; set; }
		public bool is_slash_price_item { get; set; }
		public object upcoming_flash_sale { get; set; }
		public bool is_official_shop { get; set; }
		public string brand { get; set; }
		public Int64? price_min { get; set; }
		public bool is_pre_order { get; set; }
		public int liked_count { get; set; }
		public bool can_use_bundle_deal { get; set; }
		public bool show_official_shop_label { get; set; }
		public object coin_earn_label { get; set; }
		public Int64? price_min_before_discount { get; set; }
		public int cb_option { get; set; }
		public int sold { get; set; }
		public object is_hot_sales { get; set; }
		public int stock { get; set; }
		public int status { get; set; }
		public Int64? price_max { get; set; }
		public object add_on_deal_info { get; set; }
		public object is_group_buy_item { get; set; }
		public string _description;
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				SetProperty(ref this._description, value);
			}
		}
		public object flash_sale { get; set; }
		public List<string> hashtag_list { get; set; }
		public List<Model> models { get; set; }
		private Int64? _price;
		public Int64? price
		{
			get
			{
				return this._price;
			}
			set
			{
				if (this._price != value && this._price != null)
				{
					if (this.models != null)
					{
						if (this.models.Count() > 0)
						{
							var deltaPrice = value - this._price;
							foreach (var md in models)
							{
								md.price += deltaPrice;
							}
						}
					}
				}
				SetProperty(ref this._price, value);
			}
		}
		public string shop_location { get; set; }
		public ItemRating item_rating { get; set; }
		public bool show_official_shop_label_in_title { get; set; }
		public List<TierVariation> tier_variations { get; set; }
		public object is_adult { get; set; }
		public string reason { get; set; }
		public string discount { get; set; }
		public int flag { get; set; }
		public bool is_non_cc_installment_payment_eligible { get; set; }
		public bool has_lowest_price_guarantee { get; set; }
		public List<Attribute> attributes { get; set; }
		public bool has_group_buy_stock { get; set; }
		public object preview_info { get; set; }
		public int welcome_package_type { get; set; }
		public int condition { get; set; }
		private string _name;
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				SetProperty(ref this._name, value);
			}
		}
		public int ctime { get; set; }
		public List<object> wholesale_tier_list { get; set; }
		public bool show_shopee_verified_label { get; set; }
		public object show_official_shop_label_in_normal_position { get; set; }
		public string item_status { get; set; }
		public bool shopee_verified { get; set; }
		public object hidden_price_display { get; set; }
		public string size_chart { get; set; }
		public int shipping_icon_type { get; set; }
		public int cod_flag { get; set; }
		public List<int> label_ids { get; set; }
		public int service_by_shopee_flag { get; set; }
		public int badge_icon_type { get; set; }
		public int historical_sold { get; set; }

		public ItemSearch clone()
		{
			return new ItemSearch
			{
				isCanDownload = this.isCanDownload,
				index = this.index,
				UploadingErrorReason = this.UploadingErrorReason,
				itemid = this.itemid,
				welcome_package_info = this.welcome_package_info,
				liked = this.liked,
				recommendation_info = this.recommendation_info,
				bundle_deal_info = this.bundle_deal_info,
				price_max_before_discount = this.price_max_before_discount,
				bundle_deal_id = this.bundle_deal_id,
				slash_lowest_price = this.slash_lowest_price,
				image = this.image,
				coin_info = this.coin_info,
				is_cc_installment_payment_eligible = this.is_cc_installment_payment_eligible,
				shopid = this.shopid,
				can_use_wholesale = this.can_use_wholesale,
				group_buy_info = this.group_buy_info,
				currency = this.currency,
				raw_discount = this.raw_discount,
				show_free_shipping = this.show_free_shipping,
				video_info_list = this.video_info_list,
				images = new List<string>(this.images),
				installment_plans = this.installment_plans,
				price_before_discount = this.price_before_discount,
				is_category_failed = this.is_category_failed,
				cmt_count = this.cmt_count,
				view_count = this.view_count,
				categories = new List<Category>(this.categories),
				catid = this.catid,
				is_slash_price_item = this.is_slash_price_item,
				upcoming_flash_sale = this.upcoming_flash_sale,
				is_official_shop = this.is_official_shop,
				brand = this.brand,
				price_min = this.price_min,
				is_pre_order = this.is_pre_order,
				liked_count = this.liked_count,
				can_use_bundle_deal = this.can_use_bundle_deal,
				show_official_shop_label = this.show_official_shop_label,
				coin_earn_label = this.coin_earn_label,
				price_min_before_discount = this.price_min_before_discount,
				cb_option = this.cb_option,
				sold = this.sold,
				is_hot_sales = this.is_hot_sales,
				stock = this.stock,
				status = this.status,
				price_max = this.price_max,
				add_on_deal_info = this.add_on_deal_info,
				is_group_buy_item = this.is_group_buy_item,
				description = this.description,
				flash_sale = this.flash_sale,
				hashtag_list = new List<string>(this.hashtag_list),
				models = new List<Model>(this.models),
				price = this.price,
				shop_location = this.shop_location,
				item_rating = this.item_rating,
				show_official_shop_label_in_title = this.show_official_shop_label_in_title,
				tier_variations = new List<TierVariation>(this.tier_variations),
				is_adult = this.is_adult,
				reason = this.reason,
				discount = this.discount,
				flag = this.flag,
				is_non_cc_installment_payment_eligible = this.is_non_cc_installment_payment_eligible,
				has_lowest_price_guarantee = this.has_lowest_price_guarantee,
				attributes = new List<Attribute>(this.attributes),
				has_group_buy_stock = this.has_group_buy_stock,
				preview_info = this.preview_info,
				welcome_package_type = this.welcome_package_type,
				condition = this.condition,
				name = this.name,
				ctime = this.ctime,
				wholesale_tier_list = this.wholesale_tier_list,
				show_shopee_verified_label = this.show_shopee_verified_label,
				show_official_shop_label_in_normal_position = this.show_official_shop_label_in_normal_position,
				item_status = this.item_status,
				shopee_verified = this.shopee_verified,
				hidden_price_display = this.hidden_price_display,
				size_chart = this.size_chart,
				shipping_icon_type = this.shipping_icon_type,
				cod_flag = this.cod_flag,
				label_ids = new List<int>(this.label_ids),
				service_by_shopee_flag = this.service_by_shopee_flag,
				badge_icon_type = this.badge_icon_type,
				historical_sold = this.historical_sold,
			};
		}
	}

	public class ItemSearchDetails
	{
		public ItemSearch item { get; set; }
		public string version { get; set; }
		public object data { get; set; }
		public object error_msg { get; set; }
		public object error { get; set; }
	}
}
