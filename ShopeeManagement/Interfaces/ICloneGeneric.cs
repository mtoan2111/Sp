using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeManagement.Interfaces
{
	public interface ICloneable<T>
	{
		T clone();
	}
}
