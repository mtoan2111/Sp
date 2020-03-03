using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ShopeeManagement.Converters
{
	public class StringToStatusColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var _data = value as string;
			SolidColorBrush statusColor = null;
			if (!string.IsNullOrEmpty(_data))
			{
				switch (_data)
				{
					case "Working": statusColor = new SolidColorBrush(Color.FromArgb(255, 52, 168, 83));
						break;
					case "New": statusColor = new SolidColorBrush(Color.FromArgb(255, 66, 133, 244));
						break;
					case "Updated": statusColor = new SolidColorBrush(Color.FromArgb(255, 0, 210, 255));
						break;
					case "Out of date": statusColor = new SolidColorBrush(Color.FromArgb(255, 234, 67, 53));
						break;
					default:
						break;
				}
				return statusColor;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
