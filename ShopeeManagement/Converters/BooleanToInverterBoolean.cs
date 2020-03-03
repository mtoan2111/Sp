using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
	public class BooleanToInverterBoolean : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var data = value as bool?;
			if(data != null)
			{
				return data == true ? false : true;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
