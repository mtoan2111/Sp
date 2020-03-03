using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ShopeeManagement.Models
{
    public class MainFeatures : ViewModelBase
    {
		public string Icon { get; set; }
		public string Title { get; set; }
		public string Tag { get; set; }
		public Type PageType { get; set; }
		private bool _isCountProductShown;
		public bool isCountProductShown
		{
			get
			{
				return this._isCountProductShown;
			}
			set
			{
				SetProperty(ref this._isCountProductShown, value);
			}
		}
		private int _selfCount = 0;
		public int SelfCount
		{
			get
			{
				return this._selfCount;
			}
			set
			{
				SetProperty(ref this._selfCount, value);
				//this.isCountProductShown = this._choosingProductCount > 0 ? true : false;
			}
		}
	}
}
