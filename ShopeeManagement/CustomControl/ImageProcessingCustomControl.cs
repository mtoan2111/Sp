using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace ShopeeManagement.CustomControl
{
	public sealed class ImageProcessingCustomControl : Control
	{
		public ImageProcessingCustomControl()
		{
			this.DefaultStyleKey = typeof(ImageProcessingCustomControl);
		}

		public ImageSource MasterImagePath
		{
			get => (ImageSource)GetValue(MasterImagePathProperty);
			set => SetValue(MasterImagePathProperty, value);
		}



		public static readonly DependencyProperty MasterImagePathProperty =
			DependencyProperty.Register("MasterImagePath", typeof(ImageSource), typeof(ImageProcessingCustomControl), new PropertyMetadata(null));
	}
}
