using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class OrderItem
	{
		public Int64? status { get; set; }
		public object bundle_deal_id { get; set; }
		public string item_price { get; set; }
		public string comm_fee_rate { get; set; }
		public bool is_wholesale { get; set; }
		public List<object> item_list { get; set; }
		public string groupid { get; set; }
		public Int64? amount { get; set; }
		public string order_price { get; set; }
		public Int64? snapshotid { get; set; }
		public Int64? add_on_deal_id { get; set; }
		public Int64? price_before_bundle { get; set; }
		public bool is_add_on_sub_item { get; set; }
		public string id { get; set; }
		public string modelid { get; set; }
	}

	public class User
	{
		public Int64? status { get; set; }
		public bool followed { get; set; }
		public Int64? following_count { get; set; }
		public object pn_option { get; set; }
		public Int64? delivery_order_count { get; set; }
		public Int64? shopid { get; set; }
		public Int64? delivery_address_id { get; set; }
		public double rating_star { get; set; }
		public string portrait { get; set; }
		public Int64? id { get; set; }
		public List<Int64?> rating_count { get; set; }
		public string id_address_limit_info { get; set; }
		public Int64? hide_likes { get; set; }
		public bool feed_private { get; set; }
		public Int64? wallet_setting { get; set; }
		public Int64? score { get; set; }
		public string email { get; set; }
		public Int64? wholesale_setting { get; set; }
		public bool disable_new_device_login_otp { get; set; }
		public bool holiday_mode { get; set; }
		public Int64? holiday_mode_mtime { get; set; }
		public string phone { get; set; }
		public Int64? ba_check_status { get; set; }
		public string machine_code { get; set; }
		public bool phone_public { get; set; }
		public Int64? ctime { get; set; }
		public string language { get; set; }
		public Int64? birth_timestamp { get; set; }
		public Int64? gender { get; set; }
		public string fbid { get; set; }
		public string username { get; set; }
		public Int64? products { get; set; }
		public Int64? delivery_succ_count { get; set; }
		public bool kyc_consent { get; set; }
	}

	public class ItemModel
	{
		public Int64? itemid { get; set; }
		public Int64? status { get; set; }
		public string name { get; set; }
		public string flash_sale { get; set; }
		public Int64? promotion_refresh_time { get; set; }
		public Int64? promotionid { get; set; }
		public Int64? promo_stock { get; set; }
		public Int64? sold { get; set; }
		public string sku { get; set; }
		public string flash_sale_status { get; set; }
		public string currency { get; set; }
		public Int64? promo_source { get; set; }
		public Int64? stock { get; set; }
		public Int64? sip_stock { get; set; }
		public List<object> tier_index { get; set; }
		public string price { get; set; }
		public string price_before_discount { get; set; }
		public string rebate_price { get; set; }
		public string id { get; set; }
		public long modelid { get; set; }
	}

	public class OrderShipmentConfig
	{
		public Int64? order_id { get; set; }
		public bool display_shipment { get; set; }
	}

	public class OrderMeta
	{
		public bool from_seller_data { get; set; }
		public Int64? total { get; set; }
		public Int64? limit { get; set; }
		public Int64? offset { get; set; }
	}

	public class OrderProduct
	{
		public Int64? status { get; set; }
		public long itemid { get; set; }
		public Int64? cmt_count { get; set; }
		public Int64? catid { get; set; }
		public string name { get; set; }
		public string brand { get; set; }
		public Int64? shopid { get; set; }
		public Int64? id { get; set; }
		public string price { get; set; }
		public string currency { get; set; }
		public Int64? ctime { get; set; }
		public bool is_pre_order { get; set; }
		public Int64? estimated_days { get; set; }
		public List<string> images { get; set; }
		public Int64? liked_count { get; set; }
		public string price_before_discount { get; set; }
		public Int64? stock { get; set; }
		public string parent_sku { get; set; }
		public Int64? sold { get; set; }
		public Int64? condition { get; set; }
		public string description { get; set; }
	}

	public class DropshippingInfo
	{
		public string phone_number { get; set; }
		public Int64? enabled { get; set; }
		public string name { get; set; }
	}

	public class CardTxnFeeInfo
	{
		public string card_txn_fee { get; set; }
		public Int64? rule_id { get; set; }
	}

	public class SellerAddress
	{
		public Int64? status { get; set; }
		public Int64? orderid { get; set; }
		public string name { get; set; }
		public string district { get; set; }
		public string city { get; set; }
		public string country { get; set; }
		public string town { get; set; }
		public string storeid { get; set; }
		public Int64? userid { get; set; }
		public string zipcode { get; set; }
		public string full_address { get; set; }
		public string phone { get; set; }
		public Int64? logistics_status { get; set; }
		public string state { get; set; }
		public string command { get; set; }
		public string address { get; set; }
		public string geoinfo { get; set; }
		public Int64? type { get; set; }
		public Int64? id { get; set; }
	}

	public class Order
	{
		public string comm_fee { get; set; }
		public bool waybill_printed { get; set; }
		public Int64? shipping_method { get; set; }
		public string pickup_date_description { get; set; }
		public Int64? payment_method { get; set; }
		public string order_command { get; set; }
		public Int64? seller_due_date { get; set; }
		public string buyer_address_zipcode { get; set; }
		public string buyer_address_name { get; set; }
		public Int64? complete_time { get; set; }
		public Int64? days_to_ship { get; set; }
		public string actual_shipping_fee { get; set; }
		public Int64? ship_by_date { get; set; }
		public Int64? return_id { get; set; }
		public string voucher_code { get; set; }
		public string rate_comment { get; set; }
		public string total_price { get; set; }
		public string tax_amount { get; set; }
		public string credit_card_promotion { get; set; }
		public bool first_item_is_wholesale { get; set; }
		public Int64? userid { get; set; }
		public bool is_buyercancel_toship { get; set; }
		public Int64? checkoutid { get; set; }
		public Int64? list_type { get; set; }
		public Int64? seller_is_rated { get; set; }
		public Int64? shipping_confirm_time { get; set; }
		public Int64? payby_date { get; set; }
		public Int64? status_ext { get; set; }
		public string first_item_name { get; set; }
		public bool first_item_return { get; set; }
		public Int64? logistics_status { get; set; }
		public Int64? create_time { get; set; }
		public Int64? auto_cancel_3pl_ack_date { get; set; }
		public string price_before_discount { get; set; }
		public Int64? add_on_deal_id { get; set; }
		public string coins_cash_by_voucher { get; set; }
		public Int64? item_count { get; set; }
		public Int64? logistics_channel { get; set; }
		public Int64? buyer_rate_cmtid { get; set; }
		public double coin_used { get; set; }
		public bool voucher_absorbed_by_seller { get; set; }
		public string actual_price { get; set; }
		public Int64? pickup_time { get; set; }
		public Int64? first_item_count { get; set; }
		public Int64? buyer_is_rated { get; set; }
		public Int64? express_channel { get; set; }
		public string logistics_extra_data { get; set; }
		public DropshippingInfo dropshipping_info { get; set; }
		public string remark { get; set; }
		public object buyer_txn_fee { get; set; }
		public string voucher_price { get; set; }
		public Int64? order_type { get; set; }
		public Int64? cancel_reason_ext { get; set; }
		public object logid { get; set; }
		public Int64? shipping_proof_status { get; set; }
		public string buyer_address_phone { get; set; }
		public string cancellation_end_date { get; set; }
		public string buyer_paid_amount { get; set; }
		public Int64? ratecancel_by_date { get; set; }
		public Int64? end_of_pickup_window_time { get; set; }
		public Int64? shopid { get; set; }
		public string first_item_model { get; set; }
		public string estimated_shipping_rebate { get; set; }
		public bool instant_buyercancel_toship { get; set; }
		public string payment_channel_name { get; set; }
		public Int64? auto_cancel_arrange_ship_date { get; set; }
		public bool is_request_cancellation { get; set; }
		public Int64? cancel_reason { get; set; }
		public string note_mtime { get; set; }
		public Int64? delivery_time { get; set; }
		public Int64? pickup_attempts { get; set; }
		public Int64? seller_userid { get; set; }
		public Int64? logistics_flag { get; set; }
		public string origin_shipping_fee { get; set; }
		public string shipping_address { get; set; }
		public string shipping_fee { get; set; }
		public Int64? rate_star { get; set; }
		public string shipping_traceno { get; set; }
		public Int64? pickup_cutoff_time { get; set; }
		public Int64? buyer_last_change_address_time { get; set; }
		public string currency { get; set; }
		public Int64? escrow_release_time { get; set; }
		public string shipping_proof { get; set; }
		public Int64? id { get; set; }
		public string coin_offset { get; set; }
		public Int64? seller_address_id { get; set; }
		public string paid_amount { get; set; }
		public string wallet_discount { get; set; }
		public Int64? rate_by_date { get; set; }
		public string credit_card_number { get; set; }
		public bool order_ratable { get; set; }
		public string note { get; set; }
		public Int64? shipping_fee_discount { get; set; }
		public CardTxnFeeInfo card_txn_fee_info { get; set; }
		public Int64? status { get; set; }
		public bool pay_by_credit_card { get; set; }
		public Int64? buyer_cancel_reason { get; set; }
		public List<string> order_items { get; set; }
		public string actual_carrier { get; set; }
		public string shipping_rebate { get; set; }
		public Int64? used_voucher { get; set; }
		public Int64? seller_rate_cmtid { get; set; }
		public Int64? cancel_userid { get; set; }
		public SellerAddress seller_address { get; set; }
		public Int64? coins_by_voucher { get; set; }
		public Int64? arrange_pickup_by_date { get; set; }
		public string ordersn { get; set; }
		public string shipping_remark { get; set; }
		public bool pay_by_wallet { get; set; }
	}
	public class OrderBilling
	{
		[JsonProperty(PropertyName ="order-items")]
		public List<OrderItem> order_items { get; set; }
		public List<User> users { get; set; }
		public bool from_seller_data { get; set; }
		[JsonProperty(PropertyName = "item_models")]
		public List<ItemModel> item_models { get; set;}
		[JsonProperty(PropertyName = "bundle-deals")]
		public List<object> bundle_deals { get; set; }
		[JsonProperty(PropertyName = "order-shipment-config")]
		public List<OrderShipmentConfig> order_shipment_config { get; set; }
		public OrderMeta meta { get; set; }
		public List<OrderProduct> products { get; set; }
		public List<Order> orders { get; set; }
	}
}
