using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ShopeeManagement.Models
{
    public class LogoPosition
    {
		public HorizontalAlignment horizontal { get; set; }
		public VerticalAlignment vertical { get; set; }

		public override string ToString()
		{
			return Enum.GetName(typeof(HorizontalAlignment), horizontal) + " - " + Enum.GetName(typeof(VerticalAlignment), vertical);
		}
	}
}
