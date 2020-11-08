using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Controls
{
    public partial class ValidationEntryControl : ContentView
    {
        public ValidationEntryControl()
        {
            InitializeComponent();
            Entry = entry;
        }

        public Entry Entry;

        #region -- Public Properties --

        public static readonly BindableProperty FieldLabelProperty = BindableProperty.Create(
            propertyName: nameof(FieldLabel),
            returnType: typeof(string),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public string FieldLabel
        {
            get => (string)GetValue(FieldLabelProperty);
            set => SetValue(FieldLabelProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: nameof(Placeholder),
            returnType: typeof(string),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(
            propertyName: nameof(TextChangedCommand),
            returnType: typeof(ICommand),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            propertyName: nameof(Message),
            returnType: typeof(string),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly BindableProperty IsMessageVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsMessageVisible),
            returnType: typeof(bool),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public bool IsMessageVisible
        {
            get => (bool)GetValue(IsMessageVisibleProperty);
            set => SetValue(IsMessageVisibleProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(ValidationEntryControl));

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        #endregion

    }
}