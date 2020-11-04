using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.ViewModels
{
    class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        private INavigationService NavigationService { get; set; }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region -- Public properties --

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

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
