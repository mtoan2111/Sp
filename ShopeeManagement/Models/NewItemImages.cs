using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace ShopeeManagement.Models
{
	public class NewItemImages : ViewModelBase
	{
		private BitmapImage _source;
		public BitmapImage Source
		{
			get => this._source;
			set => SetProperty(ref this._source, value);
		}
		private Visibility _isAddImageShown = Visibility.Visible;
		private Visibility _isCanDeleteShown = Visibility.Collapsed;
		public Visibility isAddImageShown
		{
			get => this._isAddImageShown;
			set => SetProperty(ref this._isAddImageShown, value);
		}

		public Visibility isCanDeleteShown
		{
			get => this._isCanDeleteShown;
			set => SetProperty(ref this._isCanDeleteShown, value);
		}
		public StorageFile sourceStorageFile { get; set; }
		public string Token { get; set; }
		private bool _isChecked;
		public bool isChecked
		{
			get => this._isChecked;
			set => SetProperty(ref this._isChecked, value);
		}

		public double DeltaX { get; set; }
		public double DeltaY { get; set; }
		public double Scale { get; set; }

	}
}
