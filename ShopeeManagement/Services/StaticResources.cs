using ShopeeManagement.Models;
using ShopeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ShopeeManagement.Services
{
	public class StaticResources
	{
		public StaticResources()
		{
		}

		public static string ShortRegion = string.Empty;
		public static string Currency = string.Empty;
		public static string MainUri = string.Empty;
		public static string SellerMainUri = string.Empty;
		public static string ImgServer = string.Empty;
		public static string HostUri = string.Empty;
		public static string SellerHostUri = string.Empty;
		public static string ReferUri = string.Empty;
		public static string SellerReferUri = string.Empty;
		public static string OriginUri = string.Empty;
		public static string SellerOriginUri = string.Empty;
		public static string ChatUri = string.Empty;
		public static string SaleUri = string.Empty;
		public static string ShopConfigUri = string.Empty;

		public static event EventHandler EvJumpToLoginPage;
		public static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };

		public static void OnGlobalPropertyChanged (string PropertyName)
		{
			GlobalPropertyChanged(typeof(StaticResources), new PropertyChangedEventArgs(PropertyName));
		}
		private static bool _isLoginPage;
		public static bool isLoginPage
		{
			get
			{
				return _isLoginPage;
			}
			set
			{
				if (value == true)
				{
					_isLoginPage = true;
					EvJumpToLoginPage(typeof(StaticResources), null);
				}
				else if (value == false)
				{
					_isLoginPage = false;
				}
			}
		}

		public static MultiLanguage choosingLanguage = new MultiLanguage();
		public static Dictionary<UInt64, string> DictGuid = new Dictionary<UInt64, string>();

		public static MultiLanguage Vietnamese = new MultiLanguage
		{
			MainPageProductListTitle = "Danh sách sản phẩm",
			MainPageProductSearchTitle = "Tìm kiếm sản phẩm",
			MainPageProductDowloaded = "Sản phẩm đã tải xuống",
			MainPageMessage = "Quản lý tin nhắn",
			MainPageOrder = "Quản lý đơn hàng",
			MainPageAccountManagement = "Quản lý tài khoản",
			MainPageShopeeConfig = "Cấu hình Shopee",
			MainPageChooseAShop = "   Chọn một shop của bạn",
			MainPageMessageCount = "Tin nhắn: ",
			MainPageOrderCount = "Đơn hàng: ",
			MainPageShopConfig = "Cấu hình shop",
			ProductPageProductList = "Sản phẩm",
			ProductPageTotalBoostingProduct = "Tổng sản phẩm đang đẩy",
			ProductPageThumbnail = "Ảnh sản phẩm",
			ProductPageName = "Tên sản phẩm",
			ProductPagePrice = "Giá bán",
			ProductPageStatus = "Trạng thái",
			ProductPageInStock = "Số lượng",
			ProductPageView = "Xem",
			ProductPageLike = "Thích",
			ProductPageSold = "Đã bán",
			ProductPageDownloadPage = "Tải trang",
			ProductPageBoost = "Đẩy S.P",
			ProductPageDeletePage = "Xoá trang",
			ProductPageTotalProduct = "Tổng sản phẩm:",
			ProductPageDelete = "Xoá",
			ProductPageDownload = "Tải xuống",
			ProductPagePage = "Trang",
			SearchPageSearchByShopName = "Tìm theo tên shop",
			SearchPageShopName = "Tên shop",
			SearchPageResult = "Kết quả",
			SearchPageCbxChooseShop = "Chọn một shop",
			SearchPageSearch = "Tìm kiếm",
			SearchPageCheck = " K.Tra  ",
			SearchPageSearchByProductName = "Tìm theo tên sản phẩm",
			SearchPageProductName = "Tên sản phẩm",
			SearchPageSearchByCategory = "Tìm theo ngành hàng",
			SearchPageMainCategory = "Ngành hàng chính",
			SearchPageCbxChooseMainCateogry = "Chọn một ngành hàng",
			SearchPageSubCategory = "Ngành hàng phụ",
			SearchPageCbxChooseSubCategory = "Chọn một ngành hàng",
			SearchPageThumbnail = "Ảnh sản phẩm",
			SearchPageName = "Tên sản phẩm",
			SearchPagePrice = "Giá bán",
			SearchPageOriginalPrice = "Giá gốc",
			SearchPageInStock = "Số lượng",
			SearchPageView = "Xem",
			SearchPageLike = "Thích",
			SearchPageDownloadPage = "Tải trang",
			SearchPageTotalProduct = "Tổng sản phẩm:",
			SearchPagePage = "Trang",
			SearchPageSold = "Đã bán",
			SearchPageCreatedDate = "Ngày đăng",
			SearchPageRequired = "*Bắt buộc",
			SearchPageDownload = "Tải xuống",
			SearchPageInStockS = "Còn hàng",
			SearchPageProductPerPage = "Sản phẩm/trang:",
			SearchPageSortPopular = "Sản phẩm phổ biến/liên quan",
			SearchPageSortCreatedTime = "Sản phẩm mới nhất",
			SearchPageSortHotSale = "Sản phẩm bán chạy",
			SearchPageSortPriceUp = "Giá sản phẩm tăng dần",
			SearchPageSortPriceDown = "Giá sản phẩm giảm dần",
			SearchPageSortProduct = "Sắp xếp sản phẩm",
			SearchPageSortType = "Sắp xếp theo",
			SearchPageSortDefault ="Mặc định: phổ biến/liên quan",
			SearchPageProductInShop = "Tên sản phẩm (không bắt buộc)",
			SearchPageProductInCategory = "Tên sản phẩm (không bắt buộc)",
			DownloadedPageUploading = "Sản phẩm chờ đăng",
			DownloadedPageError = "Sản phẩm đăng lỗi",
			DownloadedPageThumbnail = "Ảnh đại diện",
			DownloadedPageName = "Tên sản phẩm",
			DownloadedPageDescription = "Mô tả",
			DownloadedPagePrice = "Giá bán",
			DownloadedPageDeletePage = "Xoá trang",
			DownloadedPageImageSection = "Chỉnh sửa hình ảnh",
			DownloadedPageExampleImage = "Ảnh mẫu",
			DownloadedPageInsertLogo = "Chèn logo vào ảnh",
			DownloadedPageLogoPosition = "Vị trí chèn logo",
			DownloadedPageTop = "Bên trên",
			DownloadedPageBottom = "Bên dưới",
			DownloadedPageLeft = "Bên trái",
			DownloadedPageRight = "Bên phải",
			DownloadedPageNameSection = "Chỉnh sửa tên sản phẩm",
			DownloadedPageInsertBegining = "Thêm ký tự vào đầu",
			DownloadedPageInsertEnding = "Thêm ký tự vào cuối",
			DownloadedPageReplaceFrom = "Thay đổi ký tự từ",
			DownloadedPageReplaceTo = "Sang",
			DownloadedPageDescriptionSection = "Chỉnh sửa mô tả sản phẩm",
			DownloadedPagePriceSection = "Chỉnh sửa giá sản phẩm",
			DownloadedPagePriceDecrease = "Giảm giá (đơn vị: %)",
			DownloadedPagePriceIncrease = "Tăng giá (đơn vị: %)",
			DownloadedPageApplyAll = "Áp dụng toàn bộ",
			DownloadedPageUndoAll = "Hoàn tác",
			DownloadedPageUploadAll = "Đăng toàn bộ",
			DownloadedPageUploadTo = "Đăng sản phẩm lên",
			DownloadedPageCbxChooseShop = "chọn shop cần đăng sản phẩm",
			DownloadedPageSave = "Lưu ra tệp",
			DownloadedPageLoad = "Mở tệp cuối cùng",
			DownloadedPageUpload = "Đăng S.Phẩm",
			DownloadedPageDelete = "Xoá S.Phẩm",
			DownloadedErrorReason = "Nguyên nhân lỗi",
			DownloadedError = "Tài khoản bán hàng hết phiên làm việc. Vui lòng đăng nhập lại",
			DownloadedUnknownReason = "Không rõ nguyên nhân. Có thể phiên làm việc đã hết hạn.  Vui lòng kiểm tra lại!",
			DownloadedErrorMissingData = "Thiếu một vài thông tin quan trọng để đăng sản phẩm này. Vui lòng kiểm tra lại!",
			DownloadedPageChooseFrame = "Chọn khung ảnh",
			DownloadedPageChooseImg = "Chọn ảnh",
			DownloadedPageImgResolution = "*Nên chọn logo có độ phân giải (80 x 80)",
			DownloadedPageFrameResolution = "*Nên chọn khung ảnh độ phân giải (600 x 600)",
			DownloadedPageImgDrag = "*Click chuột trái và Kéo logo đến vị trí bạn muốn",
			DownloadedPageImgScale = "*Trỏ chuột vào logo và sử dụng CTRL + con lăn chuột để thay đổi kích thước logo",
			DownloadedApplyBeforeUploading = "Nhấn nút \"Áp dụng toàn bộ\" trước khi đăng sản phẩm",
			DownloadedPageLimitation = "Số sản phẩm tối đa: ",
			DownloadedPageCurrentProduct = "Số sản phẩm bạn đã có: ",
			DownloadedPageProductRemaining = "Số sản phẩm được đăng: ",
			LoginPageHeaderAccount = "Danh sách tài khoản Shopee",
			LoginPageUserName = "Tên đăng nhập",
			LoginPageStatus = "Trạng thái",
			LoginPageDeleteAll = "Xoá tất cả",
			LoginPageAddNewAccount = "Thêm tài khoản mới",
			LoginPageDelete = "Xoá tài khoản",
			LoginPageNewMessage = "Tin nhắn mới",
			LoginPageNewOrder = "Đơn hàng mới",
			LoginPageMessageOpen = "Mở Chat",
			LoginPageOrderOpen = "Mở đơn hàng",
			LoginPageWeekRevenue = "D.Thu tuần",
			LoginPageMonthRevenue = "D.Thu tháng",
			LoginPageTotalRevenue = "Tổng d.thu",
			LoginPageRevenueReport = "Báo cáo doanh thu",
			ConfigPageDimensionConfigSection = "Cấu hình kích thước đóng gói sản phẩm",
			ConfigPageWeight = "Cân nặng",
			ConfigPageWidth = "Chiều rộng",
			ConfigPageHeight = "Chiều cao",
			ConfigPageLength = "Chiều dài",
			ConfigPageCategoryConfigSection = "Cấu hình ngành hàng",
			ConfigPageMainCategory = "Ngành hàng chính",
			ConfigPageCbxChoosingMainCategory = "Chọn một ngành hàng",
			ConfigPageSubCategory = "Ngành hàng phụ",
			ConfigPageCbxChoosingSubCategory = "Chọn một ngành hàng",
			ConfigPageShippingConfigSection = "Cấu hình thời gian vận chuyển",
			ConfigPageEstDate = "Ngày chuyển hàng ước tính",
			ConfigPageDay = "Ngày",
			ConfigPagePreOder = "Đặt trước",
			ConfigPageLogisticChannelHeader = "Cấu hình kênh vận chuyển hàng hoá",
			ConfigPageShipingFee = "Cước phí:",
			ConfigPageMaxWeight = "Cân nặng tối đa:",
			ConfigPageMinWeight = "Cân nặng tối thiểu:",
			ConfigPageMaxWidth = "Chiều rộng tối đa:",
			ConfigPageMaxHeight = "Chiều cao tối đa:",
			ConfigPageMaxLength = "Chiều dài tối đa:",
			ConfigPageCheckShippingFee = "Kiểm tra cước phí vận chuyển",
			ConfigPageSaveConfig = "Lưu cấu hình cho các lần sau",
			ConfigPageHeightWarning = "*Phải là chữ số",
			ConfigPageLenghthWarning = "*Phải là chữ số",
			ConfigPageWeightWarning = "*Phải là chữ số",
			ConfigPageWidthWarning = "*Phải là chữ số",
			ConfigPageFeeWarning = "*Phải là chữ số",
			ConfigPageShippingWarning = "*Phải là chữ số",
			ConfigPageRestric = "Giới hạn đóng gói sản phẩm:",
			ConfigPageAnnoucement = "Cập nhật thay đổi",
			ConfigPageAutomation = "Giờ đây, thông tin về cân nặng và kích thước đóng gói sản phẩm sẽ được lấy tự động từ sản phẩm được clone",
			ConfigPageDefault = "Trong trường hợp xảy ra sự cố, các thông tin này sẽ được sử dụng mặc định với các giá trị như sau:",
			GlobalWarningLogin = "Hãy đăng nhập ít nhất một tài khoản bán hàng trước!!",
			GlobalWarningPick = "Hãy Chọn một tài khoản bán hàng để thực hiện các thao tác tiếp theo!!",
			GlobalWarningConfig = "Hãy cấu hình khuôn mẫu đóng gói hàng và kênh vận chuyển hàng hoá trước khi đăng sản phẩm!!",
			GlobalWarningValid = "Một vài tài khoản bán hàng của bạn hiện đã hết phiên làm việc. Vui lòng đăng nhập lại!!",
			GlobalWarningUpload = "Hãy chọn tài khoản bạn muốn đăng sản phẩm trước!!",
			GlobalWarningDelete = "Bạn có chắc muốn xoá sản phẩm này không?",
			GlobalWarningDeleteAll = "Bạn có chắc muốn xoá toàn bộ sản phẩm trong trang hiển thị này không?",
			GlobalWaringYes = "Có",
			GlobalWarningNo = "Không",
			GlobalWarningSession = "Tài khoản của bạn đã hết phiên làm việc. Vui lòng đăng nhập lại!",
			GlobalWarningChooseSubCategoryFirst = "Chọn ngành hàng phụ trước khi kiểm tra cước phí vận chuyển!",
			GlobalWarningCheckShippingFeeFirst = "Kiểm tra cước phí vận chuyển trước khi lưu cấu hình!",
			GlobalWarningCurrentShop = "Bạn đang làm việc với shop này, vui lòng chọn menu \"Tin nhắn\"/\"Đơn hàng\" trên thanh menu thay thế",
		};

		public static MultiLanguage English = new MultiLanguage
		{
			MainPageProductListTitle = "Product list",
			MainPageProductSearchTitle = "Search product",
			MainPageProductDowloaded = "Dowloaded product",
			MainPageMessage = "Message management",
			MainPageOrder = "Order management",
			MainPageAccountManagement = "Account management",
			MainPageShopeeConfig = "Shopee configuration",
			MainPageChooseAShop = "   Choose a shop",
			MainPageMessageCount = "Message: ",
			MainPageOrderCount = "Order",
			MainPageShopConfig = "Shop configuration",
			ProductPageProductList = "Product list",
			ProductPageTotalBoostingProduct = "Boosting product",
			ProductPageThumbnail = "Thumbnail",
			ProductPageName = "Name",
			ProductPagePrice = "Price",
			ProductPageStatus = "Status",
			ProductPageInStock = "In stock",
			ProductPageView = "View",
			ProductPageLike = "Like",
			ProductPageSold = "Sold",
			ProductPageDownloadPage = "Download page",
			ProductPageBoost = "Boost",
			ProductPageDeletePage = "Delete page",
			ProductPageTotalProduct = "Total product:",
			ProductPageDelete = "Delete",
			ProductPageDownload = "Download",
			ProductPagePage = "Page",
			SearchPageSearchByShopName = "Search by shop name",
			SearchPageShopName = "Shop name",
			SearchPageResult = "Result(s)",
			SearchPageCbxChooseShop = "Choose a shop",
			SearchPageSearch = "Search",
			SearchPageCheck = "Check",
			SearchPageSearchByProductName = "Search by product name",
			SearchPageProductName = "Product name",
			SearchPageSearchByCategory = "Search by category",
			SearchPageMainCategory = "Main category",
			SearchPageCbxChooseMainCateogry = "Choose a main category",
			SearchPageSubCategory = "Sub category",
			SearchPageCbxChooseSubCategory = "Choose a sub category",
			SearchPageThumbnail = "Thumbnail",
			SearchPageName = "Name",
			SearchPagePrice = "Price",
			SearchPageOriginalPrice = "Original price",
			SearchPageInStock = "In stock",
			SearchPageView = "View",
			SearchPageLike = "Like",
			SearchPageSold = "Sold",
			SearchPageCreatedDate = "Created Date",
			SearchPageDownloadPage = "Download page",
			SearchPageTotalProduct = "Total product:",
			SearchPagePage = "Page",
			SearchPageRequired = "*Required",
			SearchPageDownload = "Download",
			SearchPageInStockS = "Instock",
			SearchPageProductPerPage = "Products/Page:",
			SearchPageSortPopular = "Popular/relevancy product",
			SearchPageSortCreatedTime = "Newest product",
			SearchPageSortHotSale = "Hotest product",
			SearchPageSortPriceDown = "Highest to lowest pricing product",
			SearchPageSortPriceUp = "Lowest to highest pricing product",
			SearchPageSortProduct = "Sort products",
			SearchPageSortType = "Sort type",
			SearchPageSortDefault = "Default: popular/relevancy",
			SearchPageProductInShop = "Product name (Optional)",
			SearchPageProductInCategory = "Product name (Optional)",
			DownloadedPageUploading = "Uploading",
			DownloadedPageError = "Error",
			DownloadedPageThumbnail = "Thumbnail",
			DownloadedPageName = "Name",
			DownloadedPageDescription = "Description",
			DownloadedPagePrice = "Price",
			DownloadedPageDeletePage = "Delete page",
			DownloadedPageImageSection = "Image section",
			DownloadedPageExampleImage = "Example Image",
			DownloadedPageInsertLogo = "Insert your shop logo",
			DownloadedPageLogoPosition = "Logo's position",
			DownloadedPageTop = "Top",
			DownloadedPageBottom = "Bottom",
			DownloadedPageLeft = "Left",
			DownloadedPageRight = "Right",
			DownloadedPageNameSection = "Name section",
			DownloadedPageInsertBegining = "Insert text from begining",
			DownloadedPageInsertEnding = "Insert text to ending",
			DownloadedPageReplaceFrom = "Replace text from",
			DownloadedPageReplaceTo = "To",
			DownloadedPageDescriptionSection = "Description section",
			DownloadedPagePriceSection = "Price section",
			DownloadedPagePriceDecrease = "Decrease price (%)",
			DownloadedPagePriceIncrease = "Increase price (%)",
			DownloadedPageApplyAll = "Apply all",
			DownloadedPageUndoAll = "Undo all",
			DownloadedPageUploadAll = "Upload all",
			DownloadedPageUploadTo = "Upload product to",
			DownloadedPageCbxChooseShop = "Choose a shop",
			DownloadedPageSave = "Save",
			DownloadedPageLoad = "Load the last save",
			DownloadedPageUpload = "Upload",
			DownloadedPageDelete = "Delete",
			DownloadedErrorReason = "Error reason",
			DownloadedError = "Your account session has been expired. Login again!",
			DownloadedUnknownReason = "unknown reason. Please check again! Maybe your account session has been expired",
			DownloadedErrorMissingData = "Missing some data for uploading this product. Please check again!",
			DownloadedPageChooseImg = "Choose a logo",
			DownloadedPageChooseFrame = "Choose a frame",
			DownloadedPageImgResolution = "*You should choose a (80 x 80) resolution logo",
			DownloadedPageFrameResolution = "*You should choose a (600 x 600) resolution logo",
			DownloadedPageImgDrag = "The logo can be dragged to anywhere within the image",
			DownloadedPageImgScale = "Combine \"CTRL + Mouse wheel\" to scale the logo",
			DownloadedApplyBeforeUploading = "Please press \"Apply All\" button before uploading",
			DownloadedPageLimitation = "Maximum product your shop can upload: ",
			DownloadedPageCurrentProduct = "The total of product in your shop: ",
			DownloadedPageProductRemaining = "The number of products can be uploaded: ",
			LoginPageHeaderAccount = "Shopee accounts",
			LoginPageUserName = "Username",
			LoginPageStatus = "Status",
			LoginPageDeleteAll = "Delete all",
			LoginPageAddNewAccount = "Add new account",
			LoginPageDelete = "Delete",
			LoginPageNewMessage = "New message",
			LoginPageNewOrder = "New order",
			LoginPageMessageOpen = "Open chat",
			LoginPageOrderOpen = "Open Order",
			LoginPageWeekRevenue = "Week income",
			LoginPageMonthRevenue = "Month income",
			LoginPageTotalRevenue = "Total income",
			LoginPageRevenueReport = "Revenue report",
			ConfigPageDimensionConfigSection = "Dimension config",
			ConfigPageWeight = "Weight",
			ConfigPageWidth = "Width",
			ConfigPageHeight = "Height",
			ConfigPageLength = "Length",
			ConfigPageCategoryConfigSection = "Category config",
			ConfigPageMainCategory = "Main category",
			ConfigPageCbxChoosingMainCategory = "Choose a category",
			ConfigPageSubCategory = "Sub category",
			ConfigPageCbxChoosingSubCategory = "Choose a sub category",
			ConfigPageShippingConfigSection = "Shipping config",
			ConfigPageEstDate = "Estimated Delivery Date",
			ConfigPageDay = "Day(s)",
			ConfigPagePreOder = "Pre-order",
			ConfigPageLogisticChannelHeader = "Logistics Channels",
			ConfigPageShipingFee = "Shipping fee",
			ConfigPageMaxWeight = "Max weight",
			ConfigPageMinWeight = "Min weight",
			ConfigPageMaxWidth = "Max width",
			ConfigPageMaxHeight = "Max height",
			ConfigPageMaxLength = "Max length",
			ConfigPageCheckShippingFee = "Check shipping fee",
			ConfigPageSaveConfig = "Save configuration",
			ConfigPageHeightWarning = "*numberic only",
			ConfigPageLenghthWarning = "*numberic only",
			ConfigPageWeightWarning = "*numberic only",
			ConfigPageWidthWarning = "*numberic only",
			ConfigPageFeeWarning = "*numberic only",
			ConfigPageShippingWarning = "*numberic only",
			ConfigPageRestric = "Restricions:",
			ConfigPageSetShippingFee = "Set shipping fee",
			ConfigPageTbxCoverShippingFee = "Cover shipping fee by seller",
			ConfigPageShippingFeeSection = "Shipping fee config",
			ConfigPageAnnoucement = "Some interesting updates",
			ConfigPageAutomation = "Now, the dimension will be automatically extracted from clone product",
			ConfigPageDefault = "In case of an error, the default dimension will be applied, see below for further information:",
			GlobalWarningLogin = "Please login a Shopee seller account first!!",
			GlobalWarningPick = "Choose an account first before working!",
			GlobalWarningConfig = "Please config the information about the dimesion and the shipping method first!!",
			GlobalWarningValid = "Some of your seller account has been invalid! Please login again",
			GlobalWarningUpload = "Please choose the seller account before uploading!",
			GlobalWarningDelete = "Are you sure to delete this product?",
			GlobalWarningDeleteAll = "Are you sure to delete all your product(s) in this page?",
			GlobalWaringYes = "Yes",
			GlobalWarningNo = "No",
			GlobalWarningSession = "Your accout session has been expired. Please login again!",
			GlobalWarningChooseSubCategoryFirst = "Please choose a sub category before checking shipping fee!",
			GlobalWarningCheckShippingFeeFirst = "Checking shipping fee first before saving the current configuration!",
			GlobalWarningCurrentShop = "You are working with this shop, please tap the icon in menu bar instead!"
		};

		public static ObservableCollection<ShopInfoLogin> TotalSellerAccount { get; set; } = new ObservableCollection<ShopInfoLogin>();
		public static ObservableCollection<ShopInfoLogin> lstShopInfoLogin { get; set; }

		public static ShopInfoLogin _selectedShopLogin;
		public static bool isOpenChatPage;
		public static bool isOpenOrderPage;
		public static ShopInfoLogin SelectedShopLogin
		{
			get
			{
				return _selectedShopLogin;
			}
			set
			{
				if (_selectedShopLogin != value)
				{
					_selectedShopLogin = value;
					OnGlobalPropertyChanged("SelectedShopLogin");
				}
			}
		}
		private static Dictionary<UInt64?, bool> _dictCanDownLoad;
		public static Frame NavigationFrame { get; set; }

		public static MainFeatures ChoosingProductFeature { get; set; }
		public static MainFeatures MessageFeature { get; set; }
		public static MainFeatures OrderFeature { get; set; }

		private static ObservableCollection<ItemSearch> _listProductDetails;
		public static ObservableCollection<ItemSearch> ListProductDetails
		{
			get
			{
				if (_listProductDetails == null)
				{
					_listProductDetails = new ObservableCollection<ItemSearch>();
				}
				return _listProductDetails;
			}
			set
			{
				if (_listProductDetails != value)
				{
					_listProductDetails = value;
					OnGlobalPropertyChanged("ListProductDetails");
				}
			}
		}
		public static ObservableCollection<Categories> ListCategories { get; set; } = new ObservableCollection<Categories>();
		public static ObservableCollection<LogisticsChannelClone> LogisticsChannel { get; set; } = null;
		public static ShopeePreConfig ShopeePreConfig { get; set; } = null;
		public static Dictionary<UInt64?, bool> dictCanDownload
		{
			get
			{
				if (_dictCanDownLoad == null)
				{
					_dictCanDownLoad = new Dictionary<UInt64?, bool>();
				}
				return _dictCanDownLoad;
			}
		}

		public static int TotalChoosingProduct { get; set; } = 0;
	}
}
