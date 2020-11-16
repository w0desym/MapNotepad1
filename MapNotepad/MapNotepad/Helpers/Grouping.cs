using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad
{
    public class Grouping<K, T> : ObservableCollection<T>, INotifyPropertyChanged
    {
        public Grouping(K category, IEnumerable<T> items)
        {
            Category = category;
            foreach (T item in items)
            {
                Items.Add(item);
            }
        }

        #region -- Public properties --

        public IEnumerable<T> Groups { get; set; }
        public K Category { get; private set; }

        private bool _isHidden;
        public bool IsHidden
        {
            get => _isHidden;
            set
            {
                _isHidden = value;
                OnPropertyChanged(nameof(IsHidden));
                OnPropertyChanged(nameof(IconSource));
            }
        }

        public string IconSource
        {
            get
            {
                if (IsHidden)
                {
                    return "chevron-right";
                }
                else
                {
                    return "chevron-down";
                }
            }
        }

        private ICommand _hideGroupTapCommand;
        public ICommand HideGroupTapCommand => _hideGroupTapCommand ??= new Command(OnHideGroupTapCommand);

        #endregion

        #region -- INotifyPropertyChanged implementation --

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region -- Private helpers --

        private void OnHideGroupTapCommand()
        {
            if (IsHidden)
            {
                foreach (var t in Groups)
                {
                    this.Add(t);
                }
            }
            else
            {
                Groups = new List<T>(this);
                this.Clear();
            }

            IsHidden = !IsHidden;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
