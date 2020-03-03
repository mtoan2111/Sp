using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ShopeeManagement.Converters
{
    public class CheckedToSolidColorConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var _data = value as bool?;
			if (_data != null)
			{
				return (_data == true) ? new Thickness(5) : new Thickness(2);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
