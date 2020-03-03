using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class ListProductBoosted
	{
		public List<UInt64?> list { get; set; }
	}

	public class BoostProductResult
	{
		public string message { get; set; }
		public int code { get; set; }
		public ListProductBoosted data { get; set; }
		public string user_message { get; set; }
	}

	public class BoostProduct
	{

	}
}
