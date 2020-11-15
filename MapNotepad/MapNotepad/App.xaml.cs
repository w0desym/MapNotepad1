using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services;
using MapNotepad.ViewModels;
using MapNotepad.Views;
using Plugin.GoogleClient;
using Plugin.Settings;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Prism.Unity;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad
{
    public partial class App : PrismApplication
    {
        private IUserService userService;
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        #region -- Overrides --
        protected override async void OnInitialized()
        {
            InitializeComponent();

            userService = Container.Resolve<IUserService>();

            if (userService.CurrentUserId == -1)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TabsPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<TabsPage, TabsPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsPage, PinsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinPage, AddPinPageViewModel>();
            containerRegistry.RegisterForNavigation<QRCodePage, QRCodePageViewModel>();
            containerRegistry.RegisterForNavigation<QRScanPage, QRScanPageViewModel>();

            //plugins
            containerRegistry.RegisterInstance(CrossGoogleClient.Current);
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(CrossSettings.Current);
            containerRegistry.RegisterPopupNavigationService();

            //services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IGoogleService>(Container.Resolve<GoogleService>());
            containerRegistry.RegisterInstance<IPermissionService>(Container.Resolve<PermissionService>());
            containerRegistry.RegisterInstance<IMapService>(Container.Resolve<MapService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());
            
        }   

        #endregion
    }
}
