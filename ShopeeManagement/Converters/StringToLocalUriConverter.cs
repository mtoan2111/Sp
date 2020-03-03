using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
	public class StringToLocalUriConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var data = value as string;
			if (!string.IsNullOrEmpty(data))
			{
				return "ms-appx:///Assets/" + data;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
