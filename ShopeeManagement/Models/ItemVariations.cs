using ShopeeManagement.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Models
{
    public class ItemVariations : ViewModelBase
    {
		public ItemVariations()
		{
			this.variations = new ObservableCollection<Variations>();
		}
		private string name;
		public string Name
		{
			get => this.name;
			set => SetProperty(ref this.name, value);
		}
		public ObservableCollection<Variations> variations { get; set; }
		public int index { get; set; }
    }
	public class Variations : ViewModelBase
	{
		public int index { get; set; }
		public event EventHandler OptionAdded;
		protected virtual void NewOptionAdded(EventArgs e)
		{
			EventHandler handler = OptionAdded;
			handler?.Invoke(this, e);
		}
		public ItemVariations Parent { get; set; }
		public Variations(ItemVariations _parent)
		{
			this.Parent = _parent;
		}
		private string option;
		public string Option
		{
			get => this.option;
			set
			{
				SetProperty(ref this.option, value);
				if (!string.IsNullOrEmpty(this.option))
				{
					NewOptionAdded(null);
				}
			}
		}
		private string price;
		public string Price
		{
			get => this.price;
			set => SetProperty(ref this.price, value);
		}
		private string stock;
		public string Stock
		{
			get => this.stock;
			set => SetProperty(ref this.stock, value);
		}
		private string sku;
		public string Sku
		{
			get => this.sku;
			set => SetProperty(ref this.sku, value);
		}
		private ObservableCollection<SubVariations> _subVariations;
		public ObservableCollection<SubVariations> subVariations
		{
			get
			{
				if (this._subVariations == null)
				{
					this._subVariations = new ObservableCollection<SubVariations>();
				}
				return this._subVariations;
			}
			set
			{
				SetProperty(ref this._subVariations, value);
			}
		}
	}

	public class SubVariations : ViewModelBase
	{
		private string option;
		public string Option
		{
			get => this.option;
			set => SetProperty(ref this.option, value);
		}
		private string price;
		public string Price
		{
			get => this.price;
			set => SetProperty(ref this.price, value);
		}
		private string stock;
		public string Stock
		{
			get => this.stock;
			set => SetProperty(ref this.stock, value);
		}
		private string sku;
		public string Sku
		{
			get => this.sku;
			set => SetProperty(ref this.sku, value);
		}
	}
}
