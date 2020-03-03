using ShopeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace ShopeeManagement.Services
{
	public static class XMLService
	{
		public static ObservableCollection<ShopInfoLogin> xmlDeserializeShopInfo()
		{
			try
			{
				var path = ApplicationData.Current.LocalFolder.Path;
				ListShopInfoLogin lstShopInfo = new ListShopInfoLogin();
				using (var xmlReader = new FileStream(path + "/ShopInfo.xml", FileMode.Open))
				{
					XmlSerializer deserializer = new XmlSerializer(typeof(ListShopInfoLogin));
					return ((ListShopInfoLogin)deserializer.Deserialize(xmlReader)).ShopInfo;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return new ObservableCollection<ShopInfoLogin>();
			}
		}

		public static bool xmlSerializeShopInfo(ObservableCollection<ShopInfoLogin> shop)
		{
			try
			{
				ListShopInfoLogin lstShopInfo = new ListShopInfoLogin();
				lstShopInfo.ShopInfo = shop;
				var path = ApplicationData.Current.LocalFolder.Path;
				using (var xmlWriter = new StreamWriter(path + "/ShopInfo.xml"))
				{
					XmlSerializer serialize = new XmlSerializer(typeof(ListShopInfoLogin));
					serialize.Serialize(xmlWriter, lstShopInfo);
				}
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
