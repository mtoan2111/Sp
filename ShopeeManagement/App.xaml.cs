using ShopeeManagement.Models;
using ShopeeManagement.Services;
using ShopeeManagement.ViewModels;
using ShopeeManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ShopeeManagement
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		private static MainPageViewModel _mainPageViewModel;
		private static ShopeeLoginPageViewModel _shopeeLoginPageViewModel;
		private static ShopeeProductPageViewModel _shopeeProductPageViewModel;
		private static ShopeeProductSearchViewModel _shopeeProductSearchViewModel;
		private static ShopeeProductDownloadedViewModel _shopeeProductDownloadedViewModel;
		private static ShopeeConfigViewModel _shopeeConfigViewModel;
		private static StartPageViewModel _startPageViewModel;
		private static ShopeeNewItemPageViewModel _shopeeNewItemPageViewModel;
		private static ShopeeChatPageViewModel _shopeeChatPageViewModel;
		private static ShopeeOrderPageViewModel _shopeeOrderPageViewModel;
		private static ShopeeShopConfigPageViewModel _shopeeShopConfigPage;

		public static ShopeeShopConfigPageViewModel _ShopeeShopConfigPage
		{
			get
			{
				if (_shopeeShopConfigPage == null)
				{
					_shopeeShopConfigPage = new ShopeeShopConfigPageViewModel();
				}
				return _shopeeShopConfigPage;
			}
		}

		public static ShopeeOrderPageViewModel _ShopeeOrderPageViewModel
		{
			get
			{
				if (_shopeeOrderPageViewModel == null)
				{
					_shopeeOrderPageViewModel = new ShopeeOrderPageViewModel();
				}
				return _shopeeOrderPageViewModel;
			}
		}

		public static ShopeeChatPageViewModel _ShopeeChatPageViewModel
		{
			get
			{
				if (_shopeeChatPageViewModel == null)
				{
					_shopeeChatPageViewModel = new ShopeeChatPageViewModel();
				}
				return _shopeeChatPageViewModel;
			}
		}

		public static ShopeeNewItemPageViewModel _ShopeeNewItemPageViewModel
		{
			get
			{
				if (_shopeeNewItemPageViewModel == null)
				{
					_shopeeNewItemPageViewModel = new ShopeeNewItemPageViewModel();
				}
				return _shopeeNewItemPageViewModel;
			}
		}

		public static StartPageViewModel _StartPageViewModel
		{
			get
			{
				if (_startPageViewModel == null)
				{
					_startPageViewModel = new StartPageViewModel();
				}
				return _startPageViewModel;
			}
		}
		public static ShopeeConfigViewModel _ShopeeConfigViewModel
		{
			get
			{
				if (_shopeeConfigViewModel == null)
				{
					_shopeeConfigViewModel = new ShopeeConfigViewModel();
				}
				return _shopeeConfigViewModel;
			}
		}
		public static ShopeeProductDownloadedViewModel _ShopeeProductDownloadedViewModel
		{
			get
			{
				if (_shopeeProductDownloadedViewModel == null)
				{
					_shopeeProductDownloadedViewModel = new ShopeeProductDownloadedViewModel();
				}
				return _shopeeProductDownloadedViewModel;
			}
		}
		public static MainPageViewModel _MainPageViewModel
		{
			get
			{
				if (_mainPageViewModel == null)
				{
					_mainPageViewModel = new MainPageViewModel();
				}
				return _mainPageViewModel;
			}
		}

		public static ShopeeLoginPageViewModel _ShopeeLoginPageViewModel
		{
			get
			{
				if (_shopeeLoginPageViewModel == null)
				{
					_shopeeLoginPageViewModel = new ShopeeLoginPageViewModel();
				}
				return _shopeeLoginPageViewModel;
			}
		}

		public static ShopeeProductPageViewModel _ShopeeProductPageViewModel
		{
			get
			{
				if (_shopeeProductPageViewModel == null)
				{
					_shopeeProductPageViewModel = new ShopeeProductPageViewModel();
				}
				return _shopeeProductPageViewModel;
			}
		}

		public static ShopeeProductSearchViewModel _ShopeeProductSearchViewModel
		{
			get
			{
				if (_shopeeProductSearchViewModel == null)
				{
					_shopeeProductSearchViewModel = new ShopeeProductSearchViewModel();
				}
				return _shopeeProductSearchViewModel;
			}
		}
		public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
			StaticResources.TotalSellerAccount = XMLService.xmlDeserializeShopInfo();
		}

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
					// When the navigation stack isn't restored navigate to the first page,
					// configuring the new page by passing required information as a navigation
					// parameter
					//rootFrame.Navigate(typeof(MainPage), e.Arguments);
					rootFrame.Navigate(typeof(StartPage), e.Arguments);
				}
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
