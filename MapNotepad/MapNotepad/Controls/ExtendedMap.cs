using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotepad.Controls
{
    class ExtendedMap : ClusteredMap
    {
        public ExtendedMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
            UiSettings.ZoomControlsEnabled = true;
            UiSettings.ZoomGesturesEnabled = true;
            PinsCollection = new ObservableCollection<Pin>();
            PinsCollection.CollectionChanged += Pins_CollectionChanged;
        }

        public static readonly BindableProperty PinsCollectionProperty =
            BindableProperty.Create(
                propertyName: nameof(PinsCollection),
                returnType: typeof(ObservableCollection<Pin>),
                declaringType: typeof(ExtendedMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: PinsPropertyChanged);
        public ObservableCollection<Pin> PinsCollection
        {
            get => (ObservableCollection<Pin>)GetValue(PinsCollectionProperty);
            set => SetValue(PinsCollectionProperty, value);
        }

        public static readonly BindableProperty CurrentCameraPositionProperty =
            BindableProperty.Create(
                propertyName: nameof(CurrentCameraPosition),
                returnType: typeof(CameraPosition),
                declaringType: typeof(ExtendedMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: CurrentCameraPositionPropertyChanged);

        public CameraPosition CurrentCameraPosition
        {
            get => (CameraPosition)GetValue(CurrentCameraPositionProperty);
            set => SetValue(CurrentCameraPositionProperty, value);
        }

        #region -- Private Helpers -- 

        private static void CurrentCameraPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition((CameraPosition)newValue);

            if (newValue != null)
            {
                (bindable as ExtendedMap).MoveCamera(cameraUpdate);
            } 
        }

        private void Pins_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePins(this, sender as IEnumerable<Pin>);
        }
        private static void UpdatePins(ExtendedMap map, IEnumerable<Pin> newPins)
        {
            map.Pins.Clear();
            foreach (var pin in newPins)
            {
                map.Pins.Add(pin);
            }
        }
        private static void PinsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && bindable as ExtendedMap != null && newValue as ObservableCollection<Pin> != null)
            {
                UpdatePins(bindable as ExtendedMap, newValue as ObservableCollection<Pin>);
            }
        }

        #endregion
    }
}


