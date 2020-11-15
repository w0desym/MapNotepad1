using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using static MapNotepad.Constants;

namespace MapNotepad.Localization
{
    public class LocalizedResources : INotifyPropertyChanged
    {
        private readonly ResourceManager _ResourceManager;
        private CultureInfo _CurrentCultureInfo;

        public LocalizedResources(Type resource, string language = null)
            : this(resource, new CultureInfo(language ?? DefaultLanguage))
        { }

        public LocalizedResources(Type resource, CultureInfo cultureInfo)
        {
            _CurrentCultureInfo = cultureInfo;
            _ResourceManager = new ResourceManager(resource);

            MessagingCenter.Subscribe<object, CultureChangedMessage>(this,
                string.Empty, OnCultureChanged);
        }


        #region -- Public properties --

        public string this[string key]
        {
            get
            {
                return _ResourceManager.GetString(key, _CurrentCultureInfo);
            }
        }

        #endregion

        #region -- IPropertyChanged implementation --

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region -- Private helpers --

        private void OnCultureChanged(object s, CultureChangedMessage ccm)
        {
            _CurrentCultureInfo = ccm.NewCultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }

        #endregion
    }
}
