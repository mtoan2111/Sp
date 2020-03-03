using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class AttributeCategory
	{
		public List<Attribute> attributes { get; set; }
		public AttributeMeta meta { get; set; }
	}
	public class AttributeMeta
	{
		public int catid { get; set; }
		public UInt64? modelid { get; set; }
	}

	public class AttributeCategories
	{
		public List<AttributeCategory> categories { get; set; }
	}

	public class ErrorMessage
	{
		public string err_message { get; set; }
		public string error { get; set; }
	}

	public class ErrorMessage2
	{
		public string message { get; set; }
		public string errcode { get; set; }
	}
}
