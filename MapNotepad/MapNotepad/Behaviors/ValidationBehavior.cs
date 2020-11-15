using MapNotepad.Controls;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using MapNotepad.Enums;
using static MapNotepad.Validators.Validator;

namespace MapNotepad.Behaviors
{
    class ValidationBehavior : BehaviorBase<CustomEntryControl>
    {
        private CustomEntryControl _control;

        #region -- Public properties --

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            nameof(IsValid),
            typeof(bool),
            typeof(CustomEntryControl),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidPropertyKey, value);
        }

        public static readonly BindableProperty MatchWithProperty = BindableProperty.Create(
            propertyName: nameof(MatchWith),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryControl));

        public string MatchWith
        {
            get => (string)GetValue(MatchWithProperty);
            set => SetValue(MatchWithProperty, value);
        }

        public static readonly BindableProperty RegexProperty = BindableProperty.Create(
            propertyName: nameof(Regex),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryControl),
            defaultValue: string.Empty);

        public string Regex
        {
            get => (string)GetValue(RegexProperty);
            set => SetValue(RegexProperty, value);
        }

        public static readonly BindableProperty ValidationTypeProperty = BindableProperty.Create(
            propertyName: nameof(Enums.ValidationType),
            returnType: typeof(ValidationType),
            declaringType: typeof(CustomEntryControl),
            defaultValue: ValidationType.Custom);

        public ValidationType ValidationType
        {
            get => (ValidationType)GetValue(ValidationTypeProperty);
            set => SetValue(ValidationTypeProperty, value);
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            propertyName: nameof(Message),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryControl));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly BindableProperty SecondMessageProperty = BindableProperty.Create(
            propertyName: nameof(SecondMessage),
            returnType: typeof(string),
            declaringType: typeof(CustomEntryControl));

        public string SecondMessage
        {
            get => (string)GetValue(SecondMessageProperty);
            set => SetValue(SecondMessageProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntryControl));

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty PasswordMinimumLengthProperty = BindableProperty.Create(
            propertyName: nameof(PasswordMinimumLength),
            returnType: typeof(int),
            declaringType: typeof(CustomEntryControl),
            defaultValue: 0);

        public int PasswordMinimumLength
        {
            get => (int)GetValue(PasswordMinimumLengthProperty);
            set => SetValue(PasswordMinimumLengthProperty, value);
        }


        public static readonly BindableProperty PasswordMaximumLengthProperty = BindableProperty.Create(
            propertyName: nameof(PasswordMaximumLength),
            returnType: typeof(int),
            declaringType: typeof(CustomEntryControl),
            defaultValue: 35);

        public int PasswordMaximumLength
        {
            get => (int)GetValue(PasswordMaximumLengthProperty);
            set => SetValue(PasswordMaximumLengthProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(CustomEntryControl control)
        {
            base.OnAttachedTo(control);

            _control = control;
            control.Entry.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(CustomEntryControl control)
        {
            base.OnDetachingFrom(control);

            _control = null;
            control.Entry.TextChanged -= OnTextChanged;
        }

        #endregion

        #region -- Private helpers --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (IsPassword)
                {
                    if (string.IsNullOrEmpty(MatchWith))
                    {
                        //password validation

                        ValidatePassword(e.NewTextValue);
                    }
                    else
                    {
                        //confirm password validation

                        ValidateConfirmPassword(password: MatchWith, confirmPassword: e.NewTextValue);
                    }
                }
                else
                {
                    //any other string validation

                    ValidateString(e.NewTextValue);
                }
            }
        }

        private void ValidatePassword(string password)
        {
            _control.IsMessageVisible = password.Length < PasswordMinimumLength || password.Length > PasswordMaximumLength;

            if (_control.IsMessageVisible)
            {
                _control.Message = Message;
            }
            else
            {
                IsValid = IsMatch(Regex, password, ValidationType);

                _control.IsMessageVisible = !IsValid;

                if (_control.IsMessageVisible)
                {
                    _control.Message = SecondMessage;
                }

            }
        }

        private void ValidateConfirmPassword(string password, string confirmPassword)
        {
            IsValid = password.Equals(confirmPassword);

            _control.IsMessageVisible = !IsValid;

            _control.Message = Message;
        }

        private void ValidateString(string value)
        {
            IsValid = IsMatch(Regex, value, ValidationType);

            _control.IsMessageVisible = !IsValid;

            _control.Message = Message;
        }

        #endregion
    }
}
