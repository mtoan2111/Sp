using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{

	public class MultiLang : ViewModelBase
	{
		public string lang { get; set; }
		public string display_name { get; set; }
		public string placeholder { get; set; }
		public string tooltip { get; set; }
		private string _choose;
		public string Choose
		{
			get => this._choose;
			set
			{
				SetProperty(ref this._choose, value);
				;
			}
		}
		public ObservableCollection<string> value { get; set; }
	}

	public class FullData
	{
		public string description { get; set; }
		public ObservableCollection<MultiLang> multi_lang { get; set; }
		public string tooltip { get; set; }
		public int mandatory_mall { get; set; }
		public int is_filter { get; set; }
		public int brand_option { get; set; }
		public string placeholder { get; set; }
	}

	public class FullAttribute
	{
		public int status { get; set; }
		public int mandatory { get; set; }
		public string name { get; set; }
		public int input_type { get; set; }
		public int validate_type { get; set; }
		public int id { get; set; }
		public int mandatory_mall { get; set; }
		public int mtime { get; set; }
		public string display_name { get; set; }
		public string country { get; set; }
		public FullData data { get; set; }
		public int attr_type { get; set; }
		public int ctime { get; set; }
	}

	public class Meta
	{
		public int catid { get; set; }
		public UInt64? modelid { get; set; }
	}

	public class FullCategory
	{
		public ObservableCollection<FullAttribute> attributes { get; set; }
		public Meta meta { get; set; }
	}

	public class FullAttributeCategories
	{
		public ObservableCollection<FullCategory> categories { get; set; }
	}
}
