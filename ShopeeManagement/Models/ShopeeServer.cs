using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class ShopeeServer
	{
		public string Region { get; set; }
		public string ShortRegion { get; set; }
		public string ImgUri { get; set; }
		public string Currency { get; set; }
		public ApiUri UriSet { get; set; }
	}

	public class ApiUri
	{
		public string MainUri { get; set; }
		public string SellerMainUri { get; set; }
		public string ImgServer { get; set; }
		public string HostUri { get; set; }
		public string SellerHostUri { get; set; }
		public string ReferUri { get; set; }
		public string SellerReferUri { get; set; }
		public string OriginUri { get; set; }
		public string SellerOriginUri { get; set; }
		public string ChatUri { get; set; }
		public string SaleUri { get; set; }
		public string ShopConfigUri { get; set; }
	}
}
