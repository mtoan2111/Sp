using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class SortProduct
	{
		public string sortType { get; set; }
		public string sort { get; set; }
		public string order { get; set; }
		public override string ToString()
		{
			return sortType;
		}
	}
}
