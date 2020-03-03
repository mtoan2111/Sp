using ShopeeManagement.Models;
using ShopeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.Web.Http.Filters;

namespace ShopeeManagement.Services
{
	public static class Utility
	{
		public static void ClearCookie(HttpBaseProtocolFilter filter, Uri _uri)
		{
			foreach (var cookie in filter.CookieManager.GetCookies(_uri))
			{
				filter.CookieManager.DeleteCookie(cookie);
			}
		}

		public static void getProductPerPage(ShopInfoLogin shop, AsyncCallback callback, int Page)
		{
			string ProductPageAPIUri = "";
			try
			{
				var SPC_CDS = shop.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					ProductPageAPIUri = StaticResources.SellerMainUri + "api/v3/product/page_product_list/?" + SPC_CDS.value + "&SPC_CDS_VER=2&page_number=" + Page + "&page_size=48&list_type=";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(ProductPageAPIUri));
			req.Headers.Add("Cookie", shop.strLoginCookie);
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.SellerReferUri + "portal/product/list/all?page=" + Page + "&size=48";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static void getProductDetails(ShopInfoLogin shop, AsyncCallback callback, UInt64? ProductID)
		{
			string ProductDetailsAPIUri = "";
			try
			{
				var SPC_CDS = shop.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					ProductDetailsAPIUri = StaticResources.SellerMainUri + "api/v2/products/" + ProductID + "/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2 HTTP/1.1";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(ProductDetailsAPIUri));
			req.Headers.Add("Cookie", shop.strLoginCookie);
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.SellerReferUri + "portal/product/" + ProductID + "/";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static void getItemSearchDetails(ShopInfoLogin shopLogin, UInt64? shopID, string itemName, UInt64? itemID, AsyncCallback callback)
		{
			string ItemSearchDetailsAPI = string.Empty;
			ItemSearchDetailsAPI = StaticResources.MainUri + "api/v2/item/get?itemid=" + itemID + "&shopid=" + shopID;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(ItemSearchDetailsAPI));
			req.Host = StaticResources.HostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.ReferUri + HttpUtility.UrlEncode(itemName) + "-i." + shopID + "." + itemID;
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.Headers.Add("X-API-SOURCE", "pc");
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static void getShopID(string strUsername, AsyncCallback callback)
		{
			string SearchShopIDAPI = string.Empty;
			SearchShopIDAPI = StaticResources.MainUri + "api/v2/search_users/?keyword=" + HttpUtility.UrlEncode(strUsername) + "&limit=6&offset=0&with_search_cover=true";
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(SearchShopIDAPI));
			req.Host = StaticResources.HostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.ReferUri + "search_user?keyword=" + HttpUtility.UrlEncode(strUsername);
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.Headers.Add("X-API-SOURCE", "pc");
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static bool deleteProduct(ShopInfoLogin shopLogin, string strDeletingProduct)
		{
			string DeleteProductAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					DeleteProductAPIUri = StaticResources.SellerMainUri + "api/v3/product/delete_product/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{
				return false;
			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(DeleteProductAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.SellerReferUri + "portal/product/list/all";
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Origin", StaticResources.SellerOriginUri);
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.ContentType = "application/json;charset=UTF-8";
			req.Method = "POST";
			using (var streamWriter = new StreamWriter(req.GetRequestStream()))
			{
				streamWriter.Write(strDeletingProduct);
				streamWriter.Flush();
				streamWriter.Close();
				try
				{
					var httpResponse = (HttpWebResponse)req.GetResponse();
					using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
					{
						var response = streamReader.ReadToEnd();
					}
					httpResponse.Close();
					return true;
				}
				catch (WebException ex)
				{
					try
					{
						var streamReader = new StreamReader(ex.Response.GetResponseStream());
						var resp = streamReader.ReadToEnd();
						streamReader.Close();
					}
					catch (Exception)
					{
					}
					return false;
				}
			}

		}

		public static void getShopItems(ShopInfoLogin shopLogin, string shopName, UInt64? shopID, int offset, string type, AsyncCallback callback, string sortType, string sortOrder)
		{
			string SearchShopItemAPI = string.Empty;
			SearchShopItemAPI = StaticResources.MainUri + "api/v2/search_items/?by=" + sortType + "&limit=30&match_id=" + shopID + "&newest=" + offset + "&order=" + sortOrder + "&page_type=" + type;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(SearchShopItemAPI));
			req.Host = StaticResources.HostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			if (!string.IsNullOrEmpty(shopName))
			{
				if (offset > 0)
				{
					req.Referer = StaticResources.ReferUri + "shop/" + shopID + "/search?page=" + (offset / 30) + "&sortBy=pop";

				}
				else
				{
					req.Referer = StaticResources.ReferUri + shopName;
				}
			}
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.Headers.Add("X-API-SOURCE", "pc");
			req.Headers.Add("If-None-Match-", "*");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.KeepAlive = true;
			req.BeginGetResponse(callback, req);
		}

		public static void getRelevantItemsInShop(ShopInfoLogin shopLogin, string shopName, UInt64? shopID, int offset, string type, string keyword, AsyncCallback callback, string sortType, string sortOrder)
		{
			string SearchShopItemAPI = string.Empty;
			SearchShopItemAPI = StaticResources.MainUri + "api/v2/search_items/?by=" + sortType + "&keyword=" + HttpUtility.UrlEncode(keyword) + "&limit=30&match_id=" + shopID + "&newest=" + offset + "&order=" + sortOrder + "&page_type=" + type;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(SearchShopItemAPI));
			req.Host = StaticResources.HostUri;
			req.Accept = "*/*";
			req.KeepAlive = true;
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.Headers.Add("X-API-SOURCE", "pc");
			req.Headers.Add("If-None-Match-", "*");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.KeepAlive = true;
			req.BeginGetResponse(callback, req);
		}

		public static void getRelevantItems(ShopInfoLogin shopLogin, string itemName, int page, int offset, AsyncCallback callback, string sortType, string sortOrder)
		{
			string SearchRelevantItemsAPI = string.Empty;
			SearchRelevantItemsAPI = StaticResources.MainUri + "api/v2/search_items/?by=" + sortType +  "&keyword=" + HttpUtility.UrlEncode(itemName) + "&limit=30&newest=" + offset + "&order=" + sortOrder + "&page_type=search";
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(SearchRelevantItemsAPI));
			req.Host = StaticResources.HostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.ReferUri + "search?keyword=" + HttpUtility.UrlEncode(itemName) + "&page=" + (page - 1) + "&sortBy=relevancy";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.Headers.Add("X-API-SOURCE", "pc");
			req.Headers.Add("If-None-Match-", "*");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static void getCategories(ShopInfoLogin shopLogin, AsyncCallback callback)
		{
			string SearchCategoryAPI = string.Empty;
			SearchCategoryAPI = StaticResources.MainUri + "api/v1/category_list/";
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(SearchCategoryAPI));
			req.Host = StaticResources.HostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static string UploadImage(ShopInfoLogin shopLogin, string imagePath)
		{
			string UploadImageAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					UploadImageAPIUri = StaticResources.SellerMainUri + "api/v1/images/?aspect=1&SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			string strResult = "";
			string boundary = "----WebKitFormBoundaryrF0eVQ2ivUgBasQP";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UploadImageAPIUri);
			request.ContentType = "multipart/form-data; boundary=" + boundary;
			request.Method = "POST";
			request.KeepAlive = true;
			request.AllowAutoRedirect = false;
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/70.4.208 Chrome/64.4.3282.208 Safari/537.36";
			request.Headers.Add("Cookie", shopLogin.strLoginCookie);

			Stream memStream = new System.IO.MemoryStream();

			var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
			var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");

			string headerTemplate = "Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: image/jpeg\r\n\r\n";

			memStream.Write(boundarybytes, 0, boundarybytes.Length);
			var header = string.Format(headerTemplate, imagePath);
			var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
			memStream.Write(headerbytes, 0, headerbytes.Length);

			try
			{
				using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
				{
					var buffer = new byte[1024];
					var bytesRead = 0;
					while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
					{
						memStream.Write(buffer, 0, bytesRead);
					}
				}
				memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
				request.ContentLength = memStream.Length;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			try
			{
				using (Stream requestStream = request.GetRequestStream())
				{

					memStream.Position = 0;
					byte[] tempBuffer = new byte[memStream.Length];
					memStream.Read(tempBuffer, 0, tempBuffer.Length);
					memStream.Close();
					requestStream.Write(tempBuffer, 0, tempBuffer.Length);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			try
			{
				using (var response = request.GetResponse())
				{
					Stream stream2 = response.GetResponseStream();
					StreamReader reader2 = new StreamReader(stream2);
					string fullpage = reader2.ReadToEnd();

					ImageUploadResponse imageUploadResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageUploadResponse>(fullpage);
					if (imageUploadResponse.errcode == 0)
					{
						stream2.Close();
						return imageUploadResponse.id;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return "";
			}
			return strResult;
		}

		public static async Task<string> UploadImage_V3(ShopInfoLogin shopLogin, string token)
		{
			string UploadImageAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					UploadImageAPIUri = StaticResources.SellerMainUri + "api/v1/images/?aspect=1&SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			var uploadingImg = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(token);
			string strResult = "";
			string boundary = "----WebKitFormBoundaryrF0eVQ2ivUgBasQP";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UploadImageAPIUri);
			request.ContentType = "multipart/form-data; boundary=" + boundary;
			request.Method = "POST";
			request.KeepAlive = true;
			request.AllowAutoRedirect = false;
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/70.4.208 Chrome/64.4.3282.208 Safari/537.36";
			request.Headers.Add("Cookie", shopLogin.strLoginCookie);

			Stream memStream = new System.IO.MemoryStream();

			var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
			var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");

			string headerTemplate = "Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: image/jpeg\r\n\r\n";

			memStream.Write(boundarybytes, 0, boundarybytes.Length);
			var header = string.Format(headerTemplate, uploadingImg);
			var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
			memStream.Write(headerbytes, 0, headerbytes.Length);

			try
			{
				var readStream = await uploadingImg.OpenStreamForReadAsync();
				{
					var buffer = new byte[1024];
					var bytesRead = 0;
					while ((bytesRead = readStream.Read(buffer, 0, buffer.Length)) != 0)
					{
						memStream.Write(buffer, 0, bytesRead);
					}
				}
				memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
				request.ContentLength = memStream.Length;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			try
			{
				using (Stream requestStream = request.GetRequestStream())
				{

					memStream.Position = 0;
					byte[] tempBuffer = new byte[memStream.Length];
					memStream.Read(tempBuffer, 0, tempBuffer.Length);
					memStream.Close();
					requestStream.Write(tempBuffer, 0, tempBuffer.Length);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			try
			{
				using (var response = request.GetResponse())
				{
					Stream stream2 = response.GetResponseStream();
					StreamReader reader2 = new StreamReader(stream2);
					string fullpage = reader2.ReadToEnd();

					ImageUploadResponse imageUploadResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageUploadResponse>(fullpage);
					if (imageUploadResponse.errcode == 0)
					{
						stream2.Close();
						return imageUploadResponse.id;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return "";
			}
			return strResult;
		}

		public static async Task<bool> UploadItemfromLocal(ShopInfoLogin shopLogin, ItemSearch item, bool isInsertLogo)
		{
			List<string> imageIds = new List<string>();
			StorageFolder imgStorageFolder = null;
			StorageFolder imgProcessedStorageFolder = null;
			string errorReason = StaticResources.choosingLanguage.DownloadedUnknownReason;
			List<AttributeCategory> findModeId = new List<AttributeCategory>();
			if (item.categories.Count() > 0)
			{
				findModeId = Utility.getCategoryAttribute(shopLogin, item.categories[item.categories.Count() - 1].catid);
			}
			else
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					item.UploadingErrorReason = errorReason;
				});
				return false;
			}
			if (ApplicationData.Current.LocalSettings.Values["ImageDownloaded"] != null)
			{
				string token = ApplicationData.Current.LocalSettings.Values["ImageDownloaded"].ToString();
				imgStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
			}
			if (ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"] != null)
			{
				string token = ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"].ToString();
				imgProcessedStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
			}
			string imgPath = string.Empty;
			if (isInsertLogo == false)
			{
				if (imgStorageFolder != null)
				{
					imgPath = imgStorageFolder.Path;
				}
			}
			else
			{
				if (imgProcessedStorageFolder != null)
				{
					imgPath = imgProcessedStorageFolder.Path;
				}
			}
			int isHadImage = 0;
			for (int i = 0; i < item.images.Count(); i++)
			{
				var _imgPath = imgPath + "\\" + item.itemid + "_" + item.images[i] + ".jpeg";
				if (!File.Exists(_imgPath))
				{
					isHadImage++;
				}
			}
			if (isHadImage == item.images.Count())
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					item.UploadingErrorReason = "Checking your image again! (If insert logo button is ON, Taping \"Apply All\" button before uploading)";
				});
				return false;
			}

			for (int i = 0; i < item.images.Count(); i++)
			{
				string imageId = Utility.UploadImage(shopLogin, imgPath + "\\" + item.itemid + "_" + item.images[i] + ".jpeg");
				if (!string.IsNullOrEmpty(imageId))
				{
					imageIds.Add(imageId);
				}
			}
			string UploadItemAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					UploadItemAPIUri = StaticResources.SellerMainUri + "api/v2/products/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{
				return false;
			}

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(UploadItemAPIUri);

			req.ProtocolVersion = HttpVersion.Version10;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36";
			req.Host = StaticResources.SellerHostUri;
			req.ContentType = "application/json; charset=utf-8";
			req.Referer = StaticResources.SellerReferUri + "portal/product/new";
			req.Headers.Add("Origin", StaticResources.SellerOriginUri);
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "POST";

			using (var streamWriter = new StreamWriter(req.GetRequestStream()))
			{
				string json7 = "";
				if (findModeId != null)
				{
					if (findModeId.Count() > 0)
					{
						json7 = Newtonsoft.Json.JsonConvert.SerializeObject(createNewItem(item, shopLogin, imageIds, findModeId[0].meta.modelid));
					}
					else
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							item.UploadingErrorReason = StaticResources.choosingLanguage.DownloadedError;
						});
					}
				}
				else
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						item.UploadingErrorReason = StaticResources.choosingLanguage.DownloadedError;
					});
				}

				json7.Replace(@"\\u", @"\u");
				streamWriter.Write(json7);
				streamWriter.Flush();
				streamWriter.Close();
				try
				{
					string response = string.Empty;
					using (var httpResponse = (HttpWebResponse)req.GetResponse())
					{
						using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
						{
							response = streamReader.ReadToEnd();
						}
					}
					try
					{
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								errorReason = dumpJson.error;
							}
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								item.UploadingErrorReason = errorReason;
							});
							return false;
						}
					}
					catch
					{
					}
					return true;
				}
				catch (WebException ex)
				{
					try
					{
						using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
						{
							var resp = streamReader.ReadToEnd();
							var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
							if (dumpJson != null)
							{
								if (!string.IsNullOrEmpty(dumpJson.err_message))
								{
									errorReason = dumpJson.err_message;
								}
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									item.UploadingErrorReason = errorReason;
								});
							}
						}
						return false;
					}
					catch (Exception)
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							item.UploadingErrorReason = errorReason;
						});
						return false;
					}

				}
			}
		}

		public static async Task<bool> UploadItemfromLocal_V3(ShopInfoLogin shopLogin, ItemSearch item, bool isInsertLogo, List<NewItemImages> newImgs, ProductDetail_V3 newItem, bool isClone)
		{

			// Catid
			// Modelid
			// Att Catid

			// value
			// upload img
			// [md5]
			// serilze
			// api
			// cookie

			List<string> imageIds = new List<string>();
			StorageFolder imgStorageFolder = null;
			StorageFolder imgProcessedStorageFolder = null;
			string errorReason = StaticResources.choosingLanguage.DownloadedUnknownReason;
			List<AttributeCategory> findModeId = new List<AttributeCategory>();
			if (isClone)
			{
				findModeId = Utility.getCategoryAttribute(shopLogin, item.categories[item.categories.Count() - 1].catid);
				if (ApplicationData.Current.LocalSettings.Values["ImageDownloaded"] != null)
				{
					string token = ApplicationData.Current.LocalSettings.Values["ImageDownloaded"].ToString();
					imgStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
				}
				if (ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"] != null)
				{
					string token = ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"].ToString();
					imgProcessedStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
				}
				string imgPath = string.Empty;
				if (isInsertLogo == false)
				{
					if (imgStorageFolder != null)
					{
						imgPath = imgStorageFolder.Path;
					}
				}
				else
				{
					if (imgProcessedStorageFolder != null)
					{
						imgPath = imgProcessedStorageFolder.Path;
					}
				}
				int isHadImage = 0;
				for (int i = 0; i < item.images.Count(); i++)
				{
					var _imgPath = imgPath + "\\" + item.itemid + "_" + item.images[i] + ".jpeg";
					if (!File.Exists(_imgPath))
					{
						isHadImage++;
					}
				}
				if (isHadImage == item.images.Count())
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						item.UploadingErrorReason = "Checking your image again! (If insert logo button is ON, Taping \"Apply All\" button before uploading)";
					});
					return false;
				}

				for (int i = 0; i < item.images.Count(); i++)
				{
					string imageId = Utility.UploadImage(shopLogin, imgPath + "\\" + item.itemid + "_" + item.images[i] + ".jpeg");
					if (!string.IsNullOrEmpty(imageId))
					{
						imageIds.Add(imageId);
					}
				}
			} // <-- Clone
			{ // --> New
				newItem.images = new List<string>();
				for (int i = 0; i < newImgs.Count(); i++)
				{
					string imageId = await Utility.UploadImage_V3(shopLogin, newImgs[i].Token);
					if (!string.IsNullOrEmpty(imageId))
					{
						newItem.images.Add(imageId);
					}
				}
			}
			string UploadItemAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					if (isClone)
					{
						UploadItemAPIUri = StaticResources.SellerMainUri + "api/v2/products/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
					}
					else
					{
						UploadItemAPIUri = StaticResources.SellerMainUri + "/api/v3/product/create_product/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
					}
				}
			}
			catch
			{
				return false;
			}

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(UploadItemAPIUri);

			req.ProtocolVersion = HttpVersion.Version10;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36";
			req.Host = StaticResources.SellerHostUri;
			req.ContentType = "application/json; charset=utf-8";
			req.Referer = StaticResources.SellerReferUri + "portal/product/new";
			req.Headers.Add("Origin", StaticResources.SellerOriginUri);
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "POST";

			using (var streamWriter = new StreamWriter(req.GetRequestStream()))
			{
				string json7 = "";
				if (isClone)
				{
					if (findModeId != null)
					{
						if (findModeId.Count() > 0)
						{
							if (isClone)
							{
								json7 = Newtonsoft.Json.JsonConvert.SerializeObject(createNewItem(item, shopLogin, imageIds, findModeId[0].meta.modelid));
							}
						}
						else
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								item.UploadingErrorReason = StaticResources.choosingLanguage.DownloadedError;
							});
						}
					}
					else
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							item.UploadingErrorReason = StaticResources.choosingLanguage.DownloadedError;
						});
					}
				}
				else
				{
					json7 = Newtonsoft.Json.JsonConvert.SerializeObject(new List<ProductDetail_V3>() { newItem });
				}

				json7.Replace(@"\\u", @"\u");
				streamWriter.Write(json7);
				streamWriter.Flush();
				streamWriter.Close();
				try
				{
					string response = string.Empty;
					using (var httpResponse = (HttpWebResponse)req.GetResponse())
					{
						using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
						{
							response = streamReader.ReadToEnd();
						}
					}
					try
					{
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								errorReason = dumpJson.error;
							}
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								item.UploadingErrorReason = errorReason;
							});
							return false;
						}
					}
					catch
					{
					}
					return true;
				}
				catch (WebException ex)
				{
					try
					{
						using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
						{
							var resp = streamReader.ReadToEnd();
							var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
							if (dumpJson != null)
							{
								if (!string.IsNullOrEmpty(dumpJson.err_message))
								{
									errorReason = dumpJson.err_message;
								}
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									item.UploadingErrorReason = errorReason;
								});
							}
						}
						return false;
					}
					catch (Exception)
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							item.UploadingErrorReason = errorReason;
						});
						return false;
					}

				}
			}
		}

		public static async Task checkSellerCookieValidate(ShopInfoLogin shopLogin)
		{
			string LoginAPIUri = "";
			var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
			if (SPC_CDS != null)
			{
				LoginAPIUri = StaticResources.SellerMainUri + "api/v2/login/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(LoginAPIUri));
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.SellerReferUri;
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			//req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			try
			{
				using (var res = (HttpWebResponse)req.GetResponse())
				{
					string content = string.Empty;
					using (var reader = new StreamReader(res.GetResponseStream()))
					{
						content = reader.ReadToEnd();
					}
					if (!string.IsNullOrEmpty(content))
					{
						ShopInfo dumpjson = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopInfo>(content);
						if (dumpjson.username == null)
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								shopLogin.ShopStatus = "Out of date";
							});
						}
						else if (!string.IsNullOrEmpty(dumpjson.username))
						{
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								shopLogin.ShopStatus = "Working";
							});
						}
					}
				}
			}
			catch
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					shopLogin.ShopStatus = "Out of date";
				});
			}
		}

		public static void getCategoryAttributeAsync(ShopInfoLogin shopLogin, UInt64? catIds, AsyncCallback callback)
		{
			string GetCategoryAttributeAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetCategoryAttributeAPIUri = StaticResources.SellerMainUri + "api/v2/categories/attributes/?catids=%5B" + catIds + "%5D&SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			List<AttributeCategory> lstAttribute = new List<AttributeCategory>();
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetCategoryAttributeAPIUri);
			WebHeaderCollection headerCollectionGET = new WebHeaderCollection();
			req.ContentType = "text/javascript; charset=utf-8";
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/70.4.208 Chrome/64.4.3282.208 Safari/537.36";
			req.Headers["X-Requested-With"] = "XMLHttpRequest";
			req.BeginGetResponse(callback, req);
		}

		public static List<AttributeCategory> getCategoryAttribute(ShopInfoLogin shopLogin, UInt64? catIds)
		{
			string GetCategoryAttributeAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetCategoryAttributeAPIUri = StaticResources.SellerMainUri + "api/v2/categories/attributes/?catids=%5B" + catIds + "%5D&SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			List<AttributeCategory> lstAttribute = new List<AttributeCategory>();
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetCategoryAttributeAPIUri);
			WebHeaderCollection headerCollectionGET = new WebHeaderCollection();
			req.ContentType = "text/javascript; charset=utf-8";
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/70.4.208 Chrome/64.4.3282.208 Safari/537.36";
			req.Headers["X-Requested-With"] = "XMLHttpRequest";

			try
			{
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				string fullPage;
				using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
				{
					fullPage = sr.ReadToEnd();
					sr.Close();
				}
				AttributeCategories attributeCategories = Newtonsoft.Json.JsonConvert.DeserializeObject<AttributeCategories>(fullPage);
				lstAttribute = attributeCategories.categories;
				resp.Close();
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine("getModelid;{0}", ex.InnerException.Message);
				return null;
			}
			return lstAttribute;
		}

		public static void getLogisticChannel(ShopInfoLogin shopLogin, AsyncCallback callback)
		{
			string LogisticChannelAPIUri = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					LogisticChannelAPIUri = StaticResources.SellerMainUri + "api/v2/logisticsChannels/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(LogisticChannelAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/javascript, */*; q=0.01";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.Referer = StaticResources.SellerReferUri + "portal/settings/shop/logistics";
			req.BeginGetResponse(callback, req);
		}


		public static bool boostProductFromLocal(ShopInfoLogin shopLogin, UInt64? itemID)
		{
			string ProductBoostAPIUri = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					ProductBoostAPIUri = StaticResources.SellerMainUri + "api/v3/product/boost_product/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{
				return false;
			}

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(ProductBoostAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.ContentType = "application/json;charset=UTF-8";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.SellerReferUri + "portal/product/list/all";
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			req.Method = "POST";
			try
			{
				using (var writer = new StreamWriter(req.GetRequestStream()))
				{
					string js = "{\"id\":" + itemID + "}";
					writer.Write(js);
				};
				try
				{
					using (var res = (HttpWebResponse)req.GetResponse())
					{
						string fullContent = string.Empty;
						using (var reader = new StreamReader(res.GetResponseStream()))
						{
							fullContent = reader.ReadToEnd();
						}
						if (!string.IsNullOrEmpty(fullContent))
						{
							var dumpContent = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(fullContent);
							string code = dumpContent["message"];
							if (code.Equals("success"))
							{
								return true;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					}
				}
				catch (Exception)
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static void getListProductBoost(ShopInfoLogin shopLogin, AsyncCallback callback)
		{
			string ListProductBoostAPIUri = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					ListProductBoostAPIUri = StaticResources.SellerMainUri + "api/v3/product/get_boost_product_id_list/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(ListProductBoostAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Referer = StaticResources.SellerReferUri + "portal/product/list/all";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.BeginGetResponse(callback, req);
		}

		public static async void getShippingFee(ShopInfoLogin shopLogin, LogisticsChannelClone channel, string weight, string width, string height, string length, UInt64? catID)
		{
			string GetShippingFeeAPIUri = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetShippingFeeAPIUri = StaticResources.SellerMainUri + "api/v2/logisticsChannels/shipping_status/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2&channel_ids=[" + channel.channelid + "]&weight=" + weight + "&width=" + width + "&length=" + length + "&height=" + height + "&category_id=" + catID;
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetShippingFeeAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					var jsonContent = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultContent);
					var price = jsonContent["channel_status"][0]["shipping_fee"] / 100000;
					if (price > 0)
					{
						channel.price = string.Format("{0:#.00}", price);
						channel.supported = true;
					}
				});
			}
			catch
			{

			}
		}

		public static ProductDetails createNewItem(ItemSearch cloneItem, ShopInfoLogin shopLogin, List<string> images, UInt64? modelID)
		{
			ProductDetails newItem = new ProductDetails();
			newItem.product = new Product();
			newItem.product.catid = cloneItem.catid;
			newItem.product.images = images;
			newItem.product.id = 0;
			newItem.product.name = cloneItem.name;
			newItem.product.brand = "No brand";
			newItem.product.size_chart = "";
			if (cloneItem.tier_variations != null && cloneItem.tier_variations.Count() > 0)
			{
				newItem.product.tier_variation = new List<TierVariation>();
				foreach (var tier in cloneItem.tier_variations)
				{
					newItem.product.tier_variation.Add(new TierVariation()
					{
						images = new List<string>(),
						name = tier.name,
						options = tier.options
					});
				}
			}
			else
			{
				newItem.product.tier_variation = new List<TierVariation>();
			}
			newItem.product.subcategories = new List<object>() { cloneItem.categories[1].catid };
			newItem.product.unlist = false;
			newItem.product.promo_source = 0;
			newItem.product.cat_status = 0;
			newItem.product.itemmodels = new List<Itemmodel>();
			foreach (var model in cloneItem.models)
			{
				var newModel = new Itemmodel
				{
					currency = StaticResources.Currency,
					name = null,
					flash_sale = null,
					flash_sale_status = null,
					id = null,
					itemid = 0,
					modelid = null,
					price = model.price / 100000,
					price_before_discount = null,
					promo_source = null,
					promo_stock = null,
					promotion_refresh_time = null,
					promotionid = null,
					status = null,
					stock = model.stock,
					tier_index = model.extinfo.tier_index,
				};
				newItem.product.itemmodels.Add(newModel);
			}
			if (newItem.product.itemmodels.Count() > 0 && newItem.product.tier_variation.Count() == 0)
			{
				newItem.product.itemmodels = new List<Itemmodel>();
			}
			newItem.product.bundle_deal_id = 0;
			newItem.product.can_use_bundle_sale = false;
			newItem.product.new_subcategories = new List<List<UInt64?>>() { new List<UInt64?>() };
			newItem.product.new_subcategories[0] = new List<UInt64?>();
			foreach (var cat in cloneItem.categories)
			{
				newItem.product.new_subcategories[0].Add(cat.catid);
			}
			newItem.product.attributes = new Attributes()
			{
				values = new List<Value>(),
				modelid = modelID
			};
			foreach (var attr in cloneItem.attributes)
			{
				newItem.product.attributes.values.Add(new Value
				{
					status = 2,
					attr_id = attr.id,
					prefill = false,
					value = attr.value
				});
			}
			newItem.product.shopid = shopLogin.Shop.shopid;
			newItem.product.stock = cloneItem.stock;
			newItem.product.status = 0;
			newItem.product.ctime = 0;
			newItem.product.parent_sku = "";
			newItem.product.price = cloneItem.price / 100000;
			newItem.product.price_min = "";
			newItem.product.price_max = "";
			newItem.product.price_before_discount = "";
			newItem.product.wholesale_tier_list = new List<object>();
			newItem.product.view_count = 0;
			newItem.product.liked_count = 0;
			newItem.product.sold = 0;
			newItem.product.cooldown_seconds = 0;
			newItem.product.product_command = "update_general";
			newItem.product.promotion_refresh_time = 0;
			newItem.product.flag = 0;
			if (StaticResources.ShopeePreConfig != null)
			{
				switch (StaticResources.ShortRegion)
				{
					case "VN":
						newItem.product.weight = StaticResources.ShopeePreConfig.weight;
						break;
					case "SG":
						newItem.product.weight = (double.Parse(StaticResources.ShopeePreConfig.weight) / 1000.0).ToString();
						break;
					case "MA":
						newItem.product.weight = (double.Parse(StaticResources.ShopeePreConfig.weight) / 1000.0).ToString();
						break;
					case "ID":
						newItem.product.weight = StaticResources.ShopeePreConfig.weight;
						break;
					case "PH":
						newItem.product.weight = StaticResources.ShopeePreConfig.weight;
						break;
					default:
						break;
				}
				newItem.product.dimensions = new Dimensions()
				{
					height = StaticResources.ShopeePreConfig.height,
					length = StaticResources.ShopeePreConfig.length,
					width = StaticResources.ShopeePreConfig.width,
					unit = 1
				};
				newItem.product.is_pre_order = StaticResources.ShopeePreConfig.isPreOrder;
				newItem.product.estimated_days = StaticResources.ShopeePreConfig.estDate;
			}

			if (StaticResources.LogisticsChannel != null)
			{
				newItem.product.logistics_channel = StaticResources.LogisticsChannel;
			}
			newItem.product.can_use_wholesale = false;
			newItem.product.condition = 1;
			newItem.product.rating_count = new List<object>();
			newItem.product.rating_star = 0;
			newItem.product.description = cloneItem.description;
			newItem.product.flash_sale_status = "empty";
			newItem.product.self_discountid = 0;
			newItem.product.cats_recommend = new List<CatsRecommend>();
			newItem.product.video_list = "[]";
			newItem.product.ongoing_upcoming_campaigns = new ongoing_upcoming_campaigns()
			{
				ongoing_campaigns = new List<object>(),
				upcoming_campaigns = new List<object>()
			};
			newItem.product.approved_flash_sale = null;
			newItem.product.dts_lock = 0;
			newItem.product.cmt_count = 0;
			newItem.product.currency = cloneItem.currency;
			newItem.product.flash_sale = "";
			newItem.product.promo_stock = 0;
			newItem.product.installment_tenures = new InstallmentTenures();
			return newItem;
		}
		public static string GetTokenFromGuid(ShopInfoLogin shopLogin)
		{
			string token = string.Empty;
			string GetTokenUriAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetTokenUriAPI = StaticResources.SellerMainUri + "webchat/api/v1/sessions";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetTokenUriAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.ContentLength = 52;
			req.Referer = StaticResources.SellerReferUri + "webchat/";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Headers.Add("Origin", StaticResources.SellerHostUri);
			req.Method = "POST";
			using (var streamWriter = new StreamWriter(req.GetRequestStream()))
			{
				string json7 = "";

				json7 = "{\"device_id\":\"" + StaticResources.DictGuid[shopLogin.Shop.id] + "\"}";
				streamWriter.Write(json7);
				streamWriter.Flush();
				streamWriter.Close();
				try
				{
					string response = string.Empty;
					using (var httpResponse = (HttpWebResponse)req.GetResponse())
					{
						using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
						{
							response = streamReader.ReadToEnd();
						}
					}
					var re = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
					token = re["token"];
				}
				catch
				{
				}
			}
			return token;
		}

		public async static void SyncMessage(ShopInfoLogin shopLogin, string token)
		{
			string SyncMessageUriAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					SyncMessageUriAPI = StaticResources.SellerMainUri + "webchat/api/v1/user/sync?_uid=0-" + shopLogin.Shop.id;
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(SyncMessageUriAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.ContentLength = 0;
			req.Referer = StaticResources.SellerReferUri + "webchat/conversations";
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Headers.Add("Origin", StaticResources.SellerHostUri);
			req.Headers.Add("Authorization", "Bearer " + token);
			req.Method = "POST";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
					if (!string.IsNullOrEmpty(resultContent))
					{
						try
						{
							var r = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultContent);
							string mess = r["message"];
							if (mess.Contains("unauthorized"))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
						catch
						{

						}
					}
				}
			}
			catch {
			}
		}

		public static int GetNotifiCationCount(ShopInfoLogin shopLogin, string token)
		{
			int messCount = 0;
			string GetMessageCountUriAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetMessageCountUriAPI = StaticResources.SellerMainUri + "webchat/api/v1/notifications/unread_count?_uid=0-" + shopLogin.Shop.id;
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetMessageCountUriAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "*/*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.Referer = StaticResources.SellerReferUri + "webchat/conversations";
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Headers.Add("Authorization", "Bearer " + token);
			req.Method = "GET";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultContent);
				messCount = ret["all"];
			}
			catch
			{
			}
			return messCount;
		}

		public async static Task<int> GetMessageCount(ShopInfoLogin shopLogin)
		{
			int messCount = 0;
			string GetMessageCountUriAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetMessageCountUriAPI = StaticResources.SellerMainUri + "webchat/api/v1/sc/messages/unread-count?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetMessageCountUriAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.Referer = StaticResources.SellerReferUri + "webchat";
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultContent);
				messCount = ret["total_unread_count"];
			}
			catch(WebException ex)
			{
				try
				{
					using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
					{
						var resp = streamReader.ReadToEnd();
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
						var dumpJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage2>(resp);
						if (dumpJson2.errcode != null && dumpJson2.message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson2.errcode))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
			return messCount;
		}

		public async static Task<int> GetNewOrderCount(ShopInfoLogin shopLogin)
		{
			int _orderCount = 0;
			string GetNewOrderCountAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetNewOrderCountAPI = StaticResources.SellerMainUri + "api/v2/orders/meta/?SPC_CDS= " + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetNewOrderCountAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.Referer = StaticResources.SellerReferUri;
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultContent);
				_orderCount = ret["toship_unprocessed"];
			}
			catch (WebException ex)
			{
				try
				{
					using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
					{
						var resp = streamReader.ReadToEnd();
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
						var dumpJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage2>(resp);
						if (dumpJson2.errcode != null && dumpJson2.message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson2.errcode))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
			return _orderCount;
		}

		public async static Task GetRevenueCount(ShopInfoLogin shopLogin)
		{
			ShopRevenue revenue = null;
			string GetRevenueCountAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetRevenueCountAPI = StaticResources.SellerMainUri + "api/v1/transactions/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2&trans_type=under_escrow&limit=10&offset=0";
				}
			}
			catch
			{

			}

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetRevenueCountAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Headers.Add("Upgrade-Insecure-Requests", "1");
			req.Method = "GET";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopRevenue>(resultContent);
				revenue = ret;
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					shopLogin.WeekRevenue = revenue.meta.lastweek_income;
					shopLogin.MonthRevenue = revenue.meta.lastmonth_income;
					shopLogin.TotalRevenue = revenue.meta.available;
				});
			}
			catch (WebException ex)
			{
				try
				{
					using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
					{
						var resp = streamReader.ReadToEnd();
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
						var dumpJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage2>(resp);
						if (dumpJson2.errcode != null && dumpJson2.message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson2.errcode))
							{
								await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
								() =>
								{
									shopLogin.ShopStatus = "Out of date";
								});
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public static OrderBilling GetToProcessingBillCount(ShopInfoLogin shopLogin)
		{
			OrderBilling bcount = null;
			string GetToProcessingBillCountUriAPI = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetToProcessingBillCountUriAPI = StaticResources.SellerMainUri + "api/v2/orders/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2&limit=40&offset=0&sort_type=sort_desc&is_massship=false&type=toship&shipping_center_status=pickup_arranged";
				}
			}
			catch
			{
			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetToProcessingBillCountUriAPI));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.KeepAlive = true;
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Sec-Fetch-Mode", "cors");
			req.Headers.Add("Sec-Fetch-Site", "same-origin");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Referer = StaticResources.SellerReferUri + "portal/sale?type=toship&shipping_center_status=pickup_arranged";
			req.Method = "GET";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				bcount = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderBilling>(resultContent);

			}
			catch (Exception ex)
			{
				Debug.WriteLine("GetToProcessingBillCount: {0}", ex);
			}

			return bcount;
		}

		public static PackageShippingDetail GetDimensionItem(UInt64? shopId, UInt64? itemId)
		{
			PackageShippingDetail dm = null;
			string GetDimensionItemUriAPI = string.Empty;
			try
			{
				GetDimensionItemUriAPI = StaticResources.MainUri + "api/v0/shop/" + shopId + "/item/" + itemId + "/shipping_info_to_address/?city=Qu%E1%BA%ADn%20Hai%20B%C3%A0%20Tr%C6%B0ng&district=Ph%C6%B0%E1%BB%9Dng%20B%C3%A1ch%20Khoa&state=H%C3%A0%20N%E1%BB%99i";
			}
			catch
			{
			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetDimensionItemUriAPI));
			req.Host = StaticResources.HostUri;
			req.KeepAlive = true;
			req.Headers.Add("Sec-Fetch-Mode", "cors");
			req.Headers.Add("X-Requested-With", "XMLHttpRequest");
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("X-API-SOURCE", "pc");
			req.Accept = "*/*";
			req.Headers.Add("Sec-Fetch-Site", "same-origin");
			req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Method = "GET";
			req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				res.Close();
				var dmm = Newtonsoft.Json.JsonConvert.DeserializeObject<PackageShippingDetail>(resultContent);
				dm = dmm;

			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine("getDimension{0}", ex.InnerException.Message);
				
			}

			return dm;
		}

		public static async Task<bool> UploadItem_V3(ShopInfoLogin shopLogin, ItemSearch item, bool isInsertLogo)
		{
			List<string> imageIds = new List<string>();
			StorageFolder imgStorageFolder = null;
			StorageFolder imgProcessedStorageFolder = null;
			string errorReason = StaticResources.choosingLanguage.DownloadedUnknownReason;
			List<AttributeCategory> findModeId = new List<AttributeCategory>();
			if (item.categories.Count() > 0)
			{
				findModeId = Utility.getCategoryAttribute(shopLogin, item.categories[item.categories.Count() - 1].catid);
			}
			else
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					item.UploadingErrorReason = errorReason;
				});
				return false;
			}
				
			if (ApplicationData.Current.LocalSettings.Values["ImageDownloaded"] != null)
			{
				string token = ApplicationData.Current.LocalSettings.Values["ImageDownloaded"].ToString();
				imgStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
			}
			if (ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"] != null)
			{
				string token = ApplicationData.Current.LocalSettings.Values["ProcessedImageDownloaded"].ToString();
				imgProcessedStorageFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
			}
			string imgPath = string.Empty;
			if (isInsertLogo == false)
			{
				if (imgStorageFolder != null)
				{
					imgPath = imgStorageFolder.Path;
				}
			}
			else
			{
				if (imgProcessedStorageFolder != null)
				{
					imgPath = imgProcessedStorageFolder.Path;
				}
			}
			int isHadImage = 0;
			for (int i = 0; i < item.images.Count(); i++)
			{
				var _imgPath = imgPath + "\\" + item.itemid + "_" + item.images[i] + ".jpeg";
				if (!File.Exists(_imgPath))
				{
					isHadImage++;
				}
			}
			if (isHadImage == item.images.Count())
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					item.UploadingErrorReason = "Checking your image again! (If insert logo button is ON, Taping \"Apply All\" button before uploading)";
				});
				return false;
			}

			for (int i = 0; i < item.images.Count(); i++)
			{
				string imageId = Utility.UploadImage(shopLogin, imgPath + "\\" + item.itemid + "_" + item.images[i] + ".jpeg");
				if (!string.IsNullOrEmpty(imageId))
				{
					imageIds.Add(imageId);
				}
			}
			string UploadItemAPIUri = "";
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					UploadItemAPIUri = StaticResources.SellerMainUri + "api/v3/product/create_product/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{
				return false;
			}

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(UploadItemAPIUri);

			req.ProtocolVersion = HttpVersion.Version10;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36";
			req.Host = StaticResources.SellerHostUri;
			req.ContentType = "application/json; charset=utf-8";
			req.Referer = StaticResources.SellerReferUri + "portal/product/new";
			req.Headers.Add("Origin", StaticResources.SellerOriginUri);
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "POST";

			using (var streamWriter = new StreamWriter(req.GetRequestStream()))
			{
				string json7 = "";
				if (findModeId != null)
				{
					if (findModeId.Count() > 0)
					{
						var cit = CreateNewItem_V3(item, shopLogin, imageIds, findModeId[0].meta.modelid);
						if (cit == null)
						{
							errorReason = StaticResources.choosingLanguage.DownloadedErrorMissingData;
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								item.UploadingErrorReason = errorReason;
							});
							return false;
						}
						json7 = Newtonsoft.Json.JsonConvert.SerializeObject(new List<ProductDetail_V3>() { cit });
					}
					else
					{
						await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							item.UploadingErrorReason = StaticResources.choosingLanguage.DownloadedError;
						});
					}
				}
				else
				{
					await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					() =>
					{
						item.UploadingErrorReason = StaticResources.choosingLanguage.DownloadedError;
					});
				}
				streamWriter.Write(json7);
				streamWriter.Flush();
				streamWriter.Close();
				try
				{
					string response = string.Empty;
					using (var httpResponse = (HttpWebResponse)req.GetResponse())
					{
						using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
						{
							response = streamReader.ReadToEnd();
						}
						try
						{
							var rel = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateResponse_V3>(response);
							if (rel.message != "success")
							{
								if (rel.data != null)
								{
									errorReason = rel.data.result[0].user_message;
									await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
									() =>
									{
										item.UploadingErrorReason = errorReason;
									});
								}
								else
								{
									errorReason = rel.data.result[0].user_message;
									await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
									() =>
									{
										item.UploadingErrorReason = errorReason;
									});

								}
								return false;
							}
						}
						catch (HttpRequestException ex)
						{
							Debug.WriteLine("UploadItem_V3_1: {0}", ex.InnerException.Message);
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								item.UploadingErrorReason = errorReason;
							});
							return false;
						}
					}
					try
					{
						var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
						if (dumpJson.error != null && dumpJson.err_message != null)
						{
							if (!string.IsNullOrEmpty(dumpJson.error))
							{
								errorReason = dumpJson.error;
							}
							await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
							() =>
							{
								item.UploadingErrorReason = errorReason;
							});
							return false;
						}
					}
					catch (HttpRequestException ex)
					{
						Debug.WriteLine("UploadItem_V3_2: {0}", ex.InnerException.Message);
					}
					return true;
				}
				catch (WebException ex)
				{
					try
					{
						using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
						{
							var resp = streamReader.ReadToEnd();
							var dumpJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(resp);
							if (dumpJson != null)
							{
								if (!string.IsNullOrEmpty(dumpJson.err_message))
								{
									errorReason = dumpJson.err_message;
								}
								item.UploadingErrorReason = errorReason;
							}
						}
						return false;
					}
					catch (HttpRequestException e)
					{
						Debug.WriteLine("UploadItem_V3_3: {0}", e.InnerException.Message);
						item.UploadingErrorReason = errorReason;
						return false;
					}

				}
			}
		}

		private static ProductDetail_V3 CreateNewItem_V3(ItemSearch cloneItem, ShopInfoLogin shopLogin, List<string> images, UInt64? modelID)
		{
			ProductDetail_V3 newItem = new ProductDetail_V3();
			newItem.id = 0;
			newItem.name = cloneItem.name;
			newItem.brand = cloneItem.brand;
			if (string.IsNullOrEmpty(newItem.brand))
			{
				newItem.brand = "No brand";
			}

			newItem.size_chart = "";
			if (cloneItem.tier_variations != null && cloneItem.tier_variations.Count() > 0)
			{
				newItem.tier_variation = new List<TierVariation>();
				foreach (var tier in cloneItem.tier_variations)
				{
					newItem.tier_variation.Add(new TierVariation()
					{
						images = new List<string>(),
						name = tier.name,
						options = tier.options
					});
				}
			}
			else
			{
				newItem.tier_variation = new List<TierVariation>();
			}
			newItem.images = images;
			newItem.parent_sku = "";
			newItem.condition = 1;
			newItem.price_before_discount = "";
			newItem.description = cloneItem.description;
			newItem.category_path = new List<UInt64?>();
			foreach(var cat in cloneItem.categories)
			{
				newItem.category_path.Add(cat.catid);
			}

			newItem.attribute_model = new AttributeModel();
			newItem.attribute_model.attribute_model_id = modelID;
			newItem.attribute_model.attributes = new List<Attribute_V3>();
			foreach (var att in cloneItem.attributes)
			{
				var attribute_tmp = new Attribute_V3();
				attribute_tmp.attribute_id = att.id;
				attribute_tmp.prefill = false;
				attribute_tmp.status = 0;
				attribute_tmp.value = att.value;
				newItem.attribute_model.attributes.Add(attribute_tmp);
			}

			newItem.wholesale_list = new List<object>();
			PackageShippingDetail original_dimension = null;
			try
			{
				original_dimension = Utility.GetDimensionItem(cloneItem.shopid, cloneItem.itemid);
			}
			catch
			{

			}
			if (original_dimension == null || original_dimension.shipping_infos.Count() == 0)
			{
				if (StaticResources.ShopeePreConfig != null)
				{
					switch (StaticResources.ShortRegion)
					{
						case "VN":
							newItem.weight = StaticResources.ShopeePreConfig.weight;
							break;
						case "SG":
							newItem.weight = (double.Parse(StaticResources.ShopeePreConfig.weight) / 1000.0).ToString();
							break;
						case "MA":
							newItem.weight = (double.Parse(StaticResources.ShopeePreConfig.weight) / 1000.0).ToString();
							break;
						case "ID":
							newItem.weight = StaticResources.ShopeePreConfig.weight;
							break;
						case "PH":
							newItem.weight = StaticResources.ShopeePreConfig.weight;
							break;
						default:
							break;
					}
					newItem.dimension = new Dimension()
					{
						height = int.Parse(StaticResources.ShopeePreConfig.height),
						length = int.Parse(StaticResources.ShopeePreConfig.length),
						width = int.Parse(StaticResources.ShopeePreConfig.width),
					};
					newItem.pre_order = StaticResources.ShopeePreConfig.isPreOrder;
					newItem.days_to_ship = StaticResources.ShopeePreConfig.estDate;
				}
			}
			else
			{
				if (StaticResources.ShopeePreConfig != null)
				{
					switch (StaticResources.ShortRegion)
					{
						case "VN":
							newItem.weight = (original_dimension.shipping_infos[0].debug.sizes_data[0].weight * 1000).ToString();
							break;
						case "SG":
							newItem.weight = (original_dimension.shipping_infos[0].debug.sizes_data[0].weight).ToString();
							break;
						case "MA":
							newItem.weight = (original_dimension.shipping_infos[0].debug.sizes_data[0].weight).ToString();
							break;
						case "ID":
							newItem.weight = (original_dimension.shipping_infos[0].debug.sizes_data[0].weight * 1000).ToString();
							break;
						case "PH":
							newItem.weight = (original_dimension.shipping_infos[0].debug.sizes_data[0].weight * 1000).ToString();
							break;
						default:
							break;
					}
					newItem.dimension = new Dimension()
					{
						height = (int)(original_dimension.shipping_infos[0].debug.sizes_data[0].height),
						length = (int)original_dimension.shipping_infos[0].debug.sizes_data[0].length,
						width = (int)original_dimension.shipping_infos[0].debug.sizes_data[0].width,
					};
					newItem.pre_order = StaticResources.ShopeePreConfig.isPreOrder;
					newItem.days_to_ship = StaticResources.ShopeePreConfig.estDate;
				}
			}
			newItem.add_on_deal = new List<object>();
			newItem.logistics_channels = new List<LogisticsChannel_V3>();
			if (newItem.category_path.Count()  == 0)
			{
				return null;
			}

			try
			{
				foreach (var channel in StaticResources.LogisticsChannel)
				{
					var channel_tmp = new LogisticsChannel_V3();
					channel_tmp.channelid = channel.channelid;
					channel_tmp.id = channel.id;
					channel_tmp.sizeid = channel.sizeid;
					channel_tmp.size = channel.size;
					channel_tmp.price = Utility.getShippingFee_V3(shopLogin, channel_tmp.channelid, newItem.weight, newItem.dimension.width.ToString(), newItem.dimension.height.ToString(), newItem.dimension.length.ToString(), newItem.category_path[newItem.category_path.Count() - 1]);
					if (double.Parse(channel_tmp.price) == 0)
					{
						switch (StaticResources.ShortRegion)
						{
							case "SG":
								channel_tmp.price = StaticResources.ShopeePreConfig.shippingfee;
								break;
							case "MA":
								channel_tmp.price = StaticResources.ShopeePreConfig.shippingfee;
								break;
							default:
								break;
						}
					}
					channel_tmp.cover_shipping_fee = channel.cover_shipping_fee == 1 ? true : false;
					channel_tmp.enabled = channel.enabled == 1 ? true : false;
					channel_tmp.item_flag = channel.item_flag;
					newItem.logistics_channels.Add(channel_tmp);
				}
			}
			catch
			{

			}

			newItem.unlisted = false;
			newItem.model_list = new List<ModelList>();
			foreach(var model in cloneItem.models)
			{
				var model_tmp = new ModelList();
				model_tmp.id = 0;
				model_tmp.name = model.name;
				model_tmp.stock = model.stock;
				model_tmp.price = ((double)model.price / 100000.00).ToString();
				model_tmp.sku = model.modelid.ToString();
				model_tmp.tier_index = model.extinfo.tier_index;
				newItem.model_list.Add(model_tmp);
			}
			if (cloneItem.price != null)
			{
				newItem.price = ((double)cloneItem.price / 100000.00).ToString();
			}
			else
			{
				newItem.price = cloneItem.price.ToString();
			}
			if (newItem.model_list.Count() ==  0)
			{
				newItem.stock = cloneItem.stock;
			}
			if (newItem.model_list.Count() > 0 && newItem.tier_variation.Count() == 0)
			{
				newItem.tier_variation = new List<TierVariation>();
				TierVariation ti = new TierVariation();
				ti.name = "Phân loại hàng";
				ti.images = new List<string>();
				ti.options = new List<string>();
				int k = 0;
				foreach(var model in newItem.model_list)
				{
					model.tier_index = new List<int>(){ k++};
					ti.options.Add(model.name);

				}
				newItem.tier_variation.Add(ti);
			}
			newItem.installment_tenures = new InstallmentTenures();
			newItem.category_recommend = new List<List<int>>();
			return newItem;
		}

		public static string getShippingFee_V3(ShopInfoLogin shopLogin, int channelId, string weight, string width, string height, string length, UInt64? catID)
		{
			string price = "";
			string GetShippingFeeAPIUri = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetShippingFeeAPIUri = StaticResources.SellerMainUri + "api/v2/logisticsChannels/shipping_status/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2&channel_ids=[" + channelId + "]&weight=" + weight + "&width=" + width + "&length=" + length + "&height=" + height + "&category_id=" + catID;
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetShippingFeeAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				res.Close();
				var jsonContent = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultContent);
				try
				{
					price = jsonContent["channel_status"][0]["shipping_fee"] / 100000;
					if (string.IsNullOrEmpty(price))
					{
						price = "0";
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine("getShippingFee_V3 {0}", ex);
				}
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine("getShippingFee_V3 {0}", ex.InnerException.Message);
			}
			return price;
		}

		public static int GetShopLimitation(ShopInfoLogin shopLogin, AsyncCallback _callback)
		{
			int limits = 0;
			string GetLimitationAPIUri = string.Empty;
			try
			{
				var SPC_CDS = shopLogin.lstLoginCookie.Where(p => p.name == "SPC_CDS").FirstOrDefault();
				if (SPC_CDS != null)
				{
					GetLimitationAPIUri = StaticResources.SellerMainUri + "api/v2/feature_configs/get_configs/?SPC_CDS=" + SPC_CDS.value + "&SPC_CDS_VER=2";
				}
			}
			catch
			{

			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(GetLimitationAPIUri));
			req.Host = StaticResources.SellerHostUri;
			req.KeepAlive = true;
			req.Accept = "application/json, text/plain, */*";
			req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36";
			req.Headers.Add("Sec-Fetch-Mode", "cors");
			req.Headers.Add("Sec-Fetch-Site", "same-origin");
			req.Headers.Add("Accept-Encoding", "zip, deflate, br");
			req.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			req.Headers.Add("Cookie", shopLogin.strLoginCookie);
			req.Method = "GET";
			req.BeginGetResponse(_callback, req);
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				string resultContent = string.Empty;
				using (var reader = new StreamReader(res.GetResponseStream()))
				{
					resultContent = reader.ReadToEnd();
				}
				res.Close();
				var jsonContent = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopLimitation>(resultContent);
				try
				{
					limits = jsonContent.list_limit;
				}
				catch (HttpRequestException ex)
				{
					Debug.WriteLine("getLimit{0}", ex.InnerException.Message);
				}
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine("getLimit{0}", ex.InnerException.Message);
			}
			return limits;
		}
	}
}
