using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class TabsPageViewModel : ViewModelBase
    {
        public TabsPageViewModel(
            INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Map Notepad";
        }
    }
}
