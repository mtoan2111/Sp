using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
    public class StringToDateConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var data = value as object;
			if (!string.IsNullOrEmpty(data.ToString()))
			{
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				double tick = double.Parse(data.ToString());
				TimeSpan t = TimeSpan.FromSeconds(tick);
				return (epoch + t).ToString("dd/MM/yyyy");
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
