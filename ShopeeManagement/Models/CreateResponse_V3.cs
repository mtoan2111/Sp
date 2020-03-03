using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
	public class Result
	{
		public string message { get; set; }
		public int code { get; set; }
		public string user_message { get; set; }
	}

	public class CreateData
	{
		public List<Result> result { get; set; }
	}
	public class CreateResponse_V3
    {
		public string message { get; set; }
		public int code { get; set; }
		public CreateData data { get; set; }
		public string user_message { get; set; }
	}
}
