using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class DataSearch2
	{
		public int total_count { get; set; }
		public List<object> items { get; set; }
	}

	public class ShopUser
	{
		public string username { get; set; }
		public bool followed { get; set; }
		public string shopname { get; set; }
		public int following_count { get; set; }
		public UInt64? shopid { get; set; }
		public string portrait { get; set; }
		public int last_login_time { get; set; }
		public DataSearch2 data { get; set; }
		public bool is_official_shop { get; set; }
		public int score { get; set; }
		public bool show_official_shop_label { get; set; }
		public int follower_count { get; set; }
		public int? shopee_verified_flag { get; set; }
		public int status { get; set; }
		public string nickname { get; set; }
		public int? response_time { get; set; }
		public int pickup_address_id { get; set; }
		public string country { get; set; }
		public bool show_shopee_verified_label { get; set; }
		public int userid { get; set; }
		public int products { get; set; }
		public int? response_rate { get; set; }
		public double? shop_rating { get; set; }
	}

	public class DataSearch
	{
		public int total_count { get; set; }
		public ObservableCollection<ShopUser> users { get; set; }
	}

	public class ShopSearchInfo
	{
		public string version { get; set; }
		public DataSearch data { get; set; }
		public object error_msg { get; set; }
		public int error { get; set; }
	}
}
