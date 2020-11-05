using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            popup.TranslationY = 300;
        }

        private void PinClicked(object sender, Xamarin.Forms.GoogleMaps.PinClickedEventArgs e)
        {
            if (popup.TranslationY > 0)
            {
                popup.TranslateTo(0, popup.TranslationY - 300);
            }
        }

        private void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            if (popup.TranslationY <= 0)
            {
                popup.TranslateTo(0, popup.TranslationY + 300);
            }
        }

        protected override void OnDisappearing()
        {
            popup.TranslationY = 300;
        }
    }
}