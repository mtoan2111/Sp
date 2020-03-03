using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
    public class ShopeePreConfig
	{
		public string width { get; set; }
		public string length { get; set; }
		public string weight{ get; set; }
		public string height { get; set; }
		public bool isPreOrder { get; set; }
		public int estDate { get; set; }
		public string shippingfee { get; set; }
		public bool isCoverFee { get; set; }
		public UInt64? subCatID { get; set; }
		public UInt64? parCatID { get; set; }
	}
}
