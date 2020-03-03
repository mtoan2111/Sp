using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class ModelList
	{
		public int id { get; set; }
		public string name { get; set; }
		public int stock { get; set; }
		public string price { get; set; }
		public string sku { get; set; }
		public List<int> tier_index { get; set; }
	}

	public class Attribute_V3
	{
		public int attribute_id { get; set; }
		public bool prefill { get; set; }
		public int status { get; set; }
		public string value { get; set; }
	}
	public class AttributeModel
	{
		public UInt64? attribute_model_id { get; set; }
		public List<Attribute_V3> attributes { get; set; }
	}

	public class Dimension
	{
		public int width { get; set; }
		public int height { get; set; }
		public int length { get; set; }
	}

	public class LogisticsChannel_V3
	{
		public string id { get; set; }
		public int channelid { get; set; }
		public int sizeid { get; set; }
		public double size { get; set; }
		public string price { get; set; }
		public bool cover_shipping_fee { get; set; }
		public bool enabled { get; set; }
		public string item_flag { get; set; }
	}

	public class ProductDetail_V3
	{
		public int id { get; set; }
		public string name { get; set; }
		public string brand { get; set; }
		public List<string> images { get; set; }
		public string description { get; set; }
		public List<ModelList> model_list { get; set; }
		public List<UInt64?> category_path { get; set; }
		public AttributeModel attribute_model { get; set; }
		public List<List<int>> category_recommend { get; set; }
		public int stock { get; set; }
		public string price { get; set; }
		public string price_before_discount { get; set; }
		public string parent_sku { get; set; }
		public List<object> wholesale_list { get; set; }
		public InstallmentTenures installment_tenures { get; set; }
		public string weight { get; set; }
		public Dimension dimension { get; set; }
		public bool pre_order { get; set; }
		public int days_to_ship { get; set; }
		public int condition { get; set; }
		public string size_chart { get; set; }
		public List<TierVariation> tier_variation { get; set; }
		public List<LogisticsChannel_V3> logistics_channels { get; set; }
		public bool unlisted { get; set; }
		public List<object> add_on_deal { get; set; }
	}
}
