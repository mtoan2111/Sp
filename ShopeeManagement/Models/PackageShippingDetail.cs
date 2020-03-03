using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class Channels {
	}
	public class ExtraData
	{
		public int? rebate_rounding_factor { get; set; }
		public int buyer_location_group_id { get; set; }
		public int buyer_list_entity_permission_group { get; set; }
		public int seller_location_group_id { get; set; }
		public Int64? percentage_rebate_cap { get; set; }
		public object min_order_total { get; set; }
		public string buyer_location_group_name { get; set; }
		public object entity_permission_group { get; set; }
		public Int64? discount_rounding_factor { get; set; }
		public int? percentage_discount_cap { get; set; }
		public int? payment_type { get; set; }
	}

	public class PromotionRule
	{
		public object discount_delta { get; set; }
		public Channels channels { get; set; }
		public UInt64? shopid { get; set; }
		public int discount_rule_flag { get; set; }
		public int rebate_flag { get; set; }
		public int priority { get; set; }
		public ExtraData extra_data { get; set; }
		public int id { get; set; }
		public int discount_flag { get; set; }
	}

	public class Discounts
	{
		public Int64? shopee { get; set; }
		public long seller { get; set; }
	}

	public class CostInfo
	{
		public int discount_promotion_rule_snapshot_id { get; set; }
		public long estimated_shipping_fee { get; set; }
		public bool enjoy_discount { get; set; }
		public string discount_source { get; set; }
		public int rebate_promotion_rule_id { get; set; }
		public int discount_promotion_rule_id { get; set; }
		public Int64? discounted_shipping_fee { get; set; }
		public Discounts discounts { get; set; }
		public long discount_amount { get; set; }
		public int discount_flag { get; set; }
	}

	public class DetailInfo
	{
		public string edt_max_dt { get; set; }
		public double he_pt { get; set; }
		public double cdt_max { get; set; }
		public double he_cdt { get; set; }
		public double apt { get; set; }
		public double cdt_min { get; set; }
		public string edt_min_dt { get; set; }
	}

	public class DeliveryInfo
	{
		public int is_rapid_sla { get; set; }
		public int estimated_delivery_date_from { get; set; }
		public Int64? estimated_delivery_time_min { get; set; }
		public bool is_shopee_24h { get; set; }
		public string display_mode { get; set; }
		public Int64? is_cross_border { get; set; }
		public Int64? estimated_delivery_time_max { get; set; }
		public DetailInfo detail_info { get; set; }
		public int estimated_delivery_date_to { get; set; }
		public bool has_edt { get; set; }
	}

	public class DiscountExtra
	{
		public string username { get; set; }
		public string shopname { get; set; }
		public string name { get; set; }
		public long min_order_total { get; set; }
	}

	public class Discounts2
	{
		public Int64? shopee { get; set; }
		public long seller { get; set; }
	}

	public class CostInfo2
	{
		public int discount_promotion_rule_snapshot_id { get; set; }
		public long estimated_shipping_fee { get; set; }
		public bool enjoy_discount { get; set; }
		public string discount_source { get; set; }
		public int rebate_promotion_rule_id { get; set; }
		public int discount_promotion_rule_id { get; set; }
		public Int64? discounted_shipping_fee { get; set; }
		public Discounts2 discounts { get; set; }
		public long discount_amount { get; set; }
		public int discount_flag { get; set; }
	}

	public class CostResult
	{
		public double cod_fee { get; set; }
		public long cost { get; set; }
	}

	public class CheckoutItem
	{
		public UInt64? itemid { get; set; }
		public List<UInt64?> item_ids { get; set; }
		public long price { get; set; }
		public UInt64? shopid { get; set; }
		public bool is_bundle { get; set; }
		public UInt64? groupid { get; set; }
		public int add_on_deal_id { get; set; }
		public object bundle_item_uuid { get; set; }
		public object bundle_item_details { get; set; }
		public int quantity { get; set; }
		public object extinfo { get; set; }
		public List<List<UInt64?>> item_details { get; set; }
		public UInt64? modelid { get; set; }
	}

	public class CostKwargs
	{
		public UInt64? shopid { get; set; }
		public long order_amount { get; set; }
		public long cogs { get; set; }
		public string distance_info { get; set; }
		public Int64? sender_addressid { get; set; }
		public string distance_data { get; set; }
		public List<CheckoutItem> checkout_items { get; set; }
		public string size_data { get; set; }
		public long cod_amount { get; set; }
		public UInt64? sender_userid { get; set; }
		public int item_flag { get; set; }
	}

	public class DiscountExtra2
	{
	}

	public class LegacyParams
	{
		public bool enjoy_discount { get; set; }
		public long possible_cost { get; set; }
		public DiscountExtra2 discount_extra { get; set; }
		public Int64? discount { get; set; }
		public long cost { get; set; }
		public Int64? possible_discount { get; set; }
		public int discount_flag { get; set; }
	}

	public class Discounts3
	{
		public long shopee { get; set; }
		public long seller { get; set; }
	}

	public class ShopPromoOnlyCostInfo
	{
		public int discount_promotion_rule_snapshot_id { get; set; }
		public long estimated_shipping_fee { get; set; }
		public bool enjoy_discount { get; set; }
		public object discount_source { get; set; }
		public int rebate_promotion_rule_id { get; set; }
		public LegacyParams legacy_params { get; set; }
		public int discount_promotion_rule_id { get; set; }
		public long discounted_shipping_fee { get; set; }
		public Discounts3 discounts { get; set; }
		public Int64? discount_amount { get; set; }
		public int discount_flag { get; set; }
	}

	public class SizesData
	{
		public double? width { get; set; }
		public double? length { get; set; }
		public double? weight { get; set; }
		public double? height { get; set; }
	}

	public class ShippingDebug
	{
		public CostInfo2 cost_info { get; set; }
		public CostResult cost_result { get; set; }
		public CostKwargs cost_kwargs { get; set; }
		public ShopPromoOnlyCostInfo shop_promo_only_cost_info { get; set; }
		public double total_weight { get; set; }
		public List<List<UInt64?>> item_uuids { get; set; }
		public List<SizesData> sizes_data { get; set; }
	}

	public class DiscountExtra3
	{
	}

	public class LegacyParams2
	{
		public bool enjoy_discount { get; set; }
		public long possible_cost { get; set; }
		public DiscountExtra3 discount_extra { get; set; }
		public Int64? discount { get; set; }
		public long cost { get; set; }
		public int possible_discount { get; set; }
		public int discount_flag { get; set; }
	}

	public class Discounts4
	{
		public long shopee { get; set; }
		public long seller { get; set; }
	}

	public class ShopPromoOnlyCostInfo2
	{
		public int discount_promotion_rule_snapshot_id { get; set; }
		public long estimated_shipping_fee { get; set; }
		public bool enjoy_discount { get; set; }
		public object discount_source { get; set; }
		public int rebate_promotion_rule_id { get; set; }
		public LegacyParams2 legacy_params { get; set; }
		public int discount_promotion_rule_id { get; set; }
		public long discounted_shipping_fee { get; set; }
		public Discounts4 discounts { get; set; }
		public int discount_amount { get; set; }
		public int discount_flag { get; set; }
	}

	public class DimensionCriterion
	{
		public int max_length { get; set; }
		public int max_width { get; set; }
		public int sum_height { get; set; }
	}

	public class ExtraData2
	{
		public Int64? default_price { get; set; }
		public int is_sls_asf { get; set; }
		public bool cod_max_price_including_shipping_fee { get; set; }
		public int decimal_places { get; set; }
		public DimensionCriterion dimension_criterion { get; set; }
		public int pickup_sameday_cutoff_hour { get; set; }
		public string lane_code { get; set; }
		public int is_sls { get; set; }
		public int min_size { get; set; }
		public double default_size { get; set; }
		public int volumetric_factor { get; set; }
		public long max_total_price { get; set; }
		public double item_min_size { get; set; }
		public int item_max_size { get; set; }
		public long cod_max_order_total { get; set; }
		public int max_size { get; set; }
		public int is_sync_init { get; set; }
	}

	public class Channel
	{
		public int shipping_method { get; set; }
		public Int64? custom_discount { get; set; }
		public int size_input { get; set; }
		public int processing_priority { get; set; }
		public Int64? distance_calculation { get; set; }
		public Int64? support_cod { get; set; }
		public Int64? category { get; set; }
		public string display_name { get; set; }
		public int priority { get; set; }
		public int need_check_ic { get; set; }
		public string name_key { get; set; }
		public ExtraData2 extra_data { get; set; }
		public int status { get; set; }
		public string desc_key { get; set; }
		public Int64? show_discount { get; set; }
		public Int64? express { get; set; }
		public int escrow { get; set; }
		public Int64? discount { get; set; }
		public long flag { get; set; }
		public int preprint { get; set; }
		public int size_selection { get; set; }
		public string icon { get; set; }
		public string name { get; set; }
		public Int64? cross_border { get; set; }
		public string country { get; set; }
		public Int64? channelid { get; set; }
		public int discount_rule_order_total { get; set; }
		public Int64? effective_start_time { get; set; }
		public long effective_end_time { get; set; }
		public int display_sequence { get; set; }
		public int undeletable { get; set; }
		public Int64? fixed_default_price { get; set; }
	}

	public class ShippingInfo
	{
		public double cod_fee { get; set; }
		public CostInfo cost_info { get; set; }
		public bool enjoy_discount { get; set; }
		public DeliveryInfo delivery_info { get; set; }
		public Int64? location_group_id { get; set; }
		public bool cod_available { get; set; }
		public DiscountExtra discount_extra { get; set; }
		public long discount { get; set; }
		public List<int> buyer_location_group_ids { get; set; }
		public Int64? cost { get; set; }
		public long possible_discount { get; set; }
		public List<int> seller_location_group_ids { get; set; }
		public ShippingDebug debug { get; set; }
		public bool cover_shipping_fee { get; set; }
		public long original_cost { get; set; }
		public ShopPromoOnlyCostInfo2 shop_promo_only_cost_info { get; set; }
		public Int64? possible_cost { get; set; }
		public Channel channel { get; set; }
		public int discount_flag { get; set; }
	}

	public class PackageShippingDetail
	{
		public List<PromotionRule> promotion_rules { get; set; }
		public string version { get; set; }
		public List<ShippingInfo> shipping_infos { get; set; }
	}
}
