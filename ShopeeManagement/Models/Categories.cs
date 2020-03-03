using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class MainCategory
	{
		public string display_name { get; set; }
		public string name { get; set; }
		public UInt64? catid { get; set; }
		public string image { get; set; }
		public int parent_category { get; set; }
		public int is_adult { get; set; }
		public int sort_weight { get; set; }
	}

	public class SubCategory
	{
		public string display_name { get; set; }
		public string name { get; set; }
		public UInt64? catid { get; set; }
		public string image { get; set; }
		public int parent_category { get; set; }
		public int? is_adult { get; set; }
		public int sort_weight { get; set; }
		public ObservableCollection<SubSubCategory> sub_sub { get; set; }
	}

	public class SubSubCategory
	{
		public string display_name { get; set; }
		public string name { get; set; }
		public UInt64? catid { get; set; }
		public string image { get; set; }
		public int parent_category { get; set; }
		public int? is_adult { get; set; }
		public int sort_weight { get; set; }
	}

	public class Categories
	{
		public MainCategory main { get; set; }
		public ObservableCollection<SubCategory> sub { get; set; }
	}
}
