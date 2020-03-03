using ShopeeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
	public class StringToUriConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var urlString = value as string;
			if (!string.IsNullOrEmpty(urlString))
			{
				return StaticResources.ImgServer + urlString;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
