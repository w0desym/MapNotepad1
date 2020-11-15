using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services;
using MapNotepad.Views;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class SignInPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IGoogleService _googleService;
        private readonly IUserDialogs _userDialogs;

        public SignInPageViewModel(
            INavigationService navigationService,
            IUserService userService,
            IGoogleService googleService,
            IUserDialogs userDialogs) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            _userService = userService;
            _googleService = googleService;
            _userDialogs = userDialogs;
        }

        #region -- Public Properties --

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ??= new Command(OnSignInCommandAsync);

        private ICommand _signUpViaGoogleCommand;
        public ICommand SignUpViaGoogleCommand => _signUpViaGoogleCommand ??= new Command(OnSignUpViaGoogleCommandAsync);

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync);

        #endregion

        #region -- INavigationAware implementation --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(Email), out string email) && parameters.TryGetValue(nameof(Password), out string password))
            {
                Email = email;
                Password = password;
            }      
        }

        #endregion

        #region -- Private helpers --
        private async void OnSignInCommandAsync()
        {
            var success = await _userService.LoginAsync(Email, Password);
            if (success)
            {
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TabsPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(Resources["Sorry"], Resources["NoSuchUser"], Resources["OK"]);
                Password = string.Empty;
            }
        }

        private async void OnSignUpViaGoogleCommandAsync()
        {
            _userDialogs.ShowLoading();

            var googleUser = await _googleService.TryLoginAsync();

            _userDialogs.HideLoading();


            if (googleUser != null)
            {
                var success = await _userService.LoginAsync(googleUser.Email, isSocialMediaAuthorizing: true);

                if (success)
                {
                    await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TabsPage)}");
                }
                else
                {
                    NavigationParameters navParams = new NavigationParameters { { nameof(User), googleUser } };

                    await _navigationService.NavigateAsync($"{nameof(SignUpPage)}", navParams);
                }
            }
            _googleService.Logout();
        }

        private async void OnSignUpCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }
        #endregion
    }
}
