using MapNotepad.Localization;
using MapNotepad.Resources;
using Prism.Mvvm;
using Prism.Navigation;
using static MapNotepad.Constants;

namespace MapNotepad.ViewModels
{
    class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        private INavigationService NavigationService { get; set; }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Resources = new LocalizedResources(typeof(AppResources), DefaultLanguage);
        }

        #region -- Public properties --

        public LocalizedResources Resources { get; private set; }

        #endregion

        #region -- IInitialize implementation

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        #endregion

        #region -- IDestructible implementation --

        public virtual void Destroy()
        {

        }

        #endregion
    }
}
