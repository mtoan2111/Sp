using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
	public class IntegerToDateTimeFormatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var _data = value as int?;
			if (_data != null)
			{
				return TimeSpan.FromSeconds((double)_data).ToString(@"hh\:mm\:ss");
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			var _data = value as string;
			if (_data != null)
			{
				return TimeSpan.Parse(_data).TotalMilliseconds;
			}
			return value;
		}
	}
}
