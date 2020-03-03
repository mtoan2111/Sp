using ShopeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ShopeeManagement.Services
{
	public class ImageDownloadService
	{
		public static async void Download(ItemSearch item)
		{
			//Create folder
			StorageFolder imgStorageFolder = null;
			try
			{
				imgStorageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ImageDownloaded");
				string imageFolderToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(imgStorageFolder);
				ApplicationData.Current.LocalSettings.Values["ImageDownloaded"] = imageFolderToken;
			}
			catch
			{
				if (ApplicationData.Current.LocalSettings.Values["ImageDownloaded"] != null)
				{
					string token = ApplicationData.Current.LocalSettings.Values["ImageDownloaded"].ToString();
					imgStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
				}
			}
			foreach (var image in item.images)
			{
				StorageFile storageFile = null;
				string imageName = item.itemid + "_" + image + ".jpeg";
				string imgUri = "https://cf.shopee.vn/file/" + image;
				try
				{
					storageFile = await imgStorageFolder.CreateFileAsync(imageName, CreationCollisionOption.FailIfExists);
					try
					{
						//var httpWebRequest = HttpWebRequest.CreateHttp(imgUri);
						//HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
						//Stream resStream = response.GetResponseStream();
						//using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
						//{
						//	await resStream.CopyToAsync(stream.AsStreamForWrite());
						//}
						//response.Dispose();
						using (WebClient client = new WebClient())
						{
							client.DownloadFile(new Uri(imgUri), storageFile.Path);
						}
					}
					catch
					{

					}
				}
				catch
				{

				}
			}
		}

		public static async void DownloadAvatar(ShopInfo shopLogin)
		{
			//Create folder
			StorageFolder imgStorageFolder = null;
			try
			{
				imgStorageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("AvataDownloaded");
				string imageFolderToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(imgStorageFolder);
				ApplicationData.Current.LocalSettings.Values["AvataDownloaded"] = imageFolderToken;
			}
			catch
			{
				if (ApplicationData.Current.LocalSettings.Values["AvataDownloaded"] != null)
				{
					string token = ApplicationData.Current.LocalSettings.Values["AvataDownloaded"].ToString();
					imgStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
				}
			}
			StorageFile storageFile = null;
			string imageName = shopLogin.id +"_avt.jpeg";
			if (string.IsNullOrEmpty(shopLogin.portrait))
			{
				return;
			}
			string imgUri = "https://cf.shopee.vn/file/" + shopLogin.portrait;
			try
			{
				storageFile = await imgStorageFolder.CreateFileAsync(imageName, CreationCollisionOption.FailIfExists);
				try
				{
					//var httpWebRequest = HttpWebRequest.CreateHttp(imgUri);
					//HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
					//Stream resStream = response.GetResponseStream();
					//using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
					//{
					//	await resStream.CopyToAsync(stream.AsStreamForWrite());
					//}
					//response.Dispose();
					using (WebClient client = new WebClient())
					{
						client.DownloadFile(new Uri(imgUri), storageFile.Path);
					}
				}
				catch
				{

				}
			}
			catch
			{

			}
		}
	}
}
