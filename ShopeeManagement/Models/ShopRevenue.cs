using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class MetaRevenue
	{
		public string available { get; set; }
		public string currency { get; set; }
		public string frozen { get; set; }
		public string lastweek_income { get; set; }
		public string processing { get; set; }
		public int userid { get; set; }
		public string withdraw_limit { get; set; }
		public int last_withdraw_time { get; set; }
		public string other { get; set; }
		public int limit { get; set; }
		public int offset { get; set; }
		public string lastmonth_income { get; set; }
		public string total_withdrawn { get; set; }
		public int total { get; set; }
		public int id { get; set; }
	}
	public class ShopRevenue
	{
		public MetaRevenue meta { get; set; }
		public List<object> transactions { get; set; }
	}
}
