using MapNotepad.Controls;
using MapNotepad.Validators;
using Prism.Behaviors;
using Xamarin.Forms;

namespace MapNotepad.Behaviors
{
    class PasswordValidationBehavior : BehaviorBase<CustomEntryControl>
    {
        private CustomEntryControl _control;

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            nameof(IsValid),
            typeof(bool),
            typeof(PasswordValidationBehavior),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

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

        #region -- Private Helper --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var newValue = e.NewTextValue;

            if (!string.IsNullOrEmpty(newValue))
            {
                _control.IsMessageVisible = e.NewTextValue.Length < 8 || e.NewTextValue.Length > 16;

                if (_control.IsMessageVisible)
                {
                    _control.Message = "Should be between 8 and 16 characters long";
                }
                else
                {
                    IsValid = Validator.MatchesRequirements(e.NewTextValue);

                    _control.IsMessageVisible = !IsValid;

                    if (_control.IsMessageVisible)
                    {
                        _control.Message = "At least 1 lowercase, uppercase and number";
                    }
                }
            }
        }

        #endregion
    }
}
