using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class VariationLimit
	{
		public int one_tier_limit { get; set; }
		public int total_limit { get; set; }
	}

	public class Dts
	{
		public int pre_order_min_dts { get; set; }
		public int pre_order_max_dts { get; set; }
		public int in_stock_dts { get; set; }
	}
	public class ShopLimitation
	{
		public VariationLimit variation_limit { get; set; }
		public Dts dts { get; set; }
		public int product_image_limit { get; set; }
		public string mass_upload_file_path { get; set; }
		public int list_limit { get; set; }
	}
}
