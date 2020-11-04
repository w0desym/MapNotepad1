using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPinPage : PopupPage
    {
        public AddPinPage()
        {
            InitializeComponent();
        }

    }
}