using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
	public class StringToCurrencyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			string _format = parameter as string;
			var _value = value as long?;
			if (_value != null)
			{
				return ((double)_value / (double)100000.00).ToString();
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			string _data = value as string;
			if (!string.IsNullOrEmpty(_data))
			{
				return Int64.Parse((double.Parse(_data) * 100000).ToString());
			}
			return value;
		}
	}
}
