﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ShopeeManagement.Converters
{
    public class IntegerToVisibilityConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var _data = value as int?;
			if (_data != null)
			{
				return _data > 0 ? Visibility.Visible : Visibility.Collapsed;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
