using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
	public class IntegerToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			int? data = value as int?;
			if (data != null)
			{
				return data == 0 ? false : true;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			bool? data = value as bool?;
			if (data != null)
			{
				return data == true ? 1 : 0;
			}
			return value;
		}
	}
}
