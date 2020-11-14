using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad
{
    public class Grouping<K, T> : ObservableCollection<T>, INotifyPropertyChanged
    {
        private IEnumerable<T> _Items { get; set; }
        public K Category { get; private set; }
        public Grouping(K category, IEnumerable<T> items)
        {
            Category = category;
            foreach (T item in items)
            {
                Items.Add(item);
            }
        }
        private ICommand _hideGroupTapCommand;
        public ICommand HideGroupTapCommand => _hideGroupTapCommand ??= new Command(OnHideGroupTapCommand);

        private void OnHideGroupTapCommand()
        {
            if (IsHidden)
            {
                foreach (var t in _Items)
                {
                    this.Add(t);
                }
            }
            else
            {
                _Items = new List<T>(this);
                this.Clear();
            }

            IsHidden = !IsHidden;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
