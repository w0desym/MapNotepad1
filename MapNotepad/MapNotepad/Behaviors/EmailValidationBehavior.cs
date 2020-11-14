using MapNotepad.Controls;
using MapNotepad.Validators;
using Prism.Behaviors;
using Xamarin.Forms;

namespace MapNotepad.Behaviors
{
    class EmailValidationBehavior : BehaviorBase<CustomEntryControl>
    {
        private CustomEntryControl _control;

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid", 
            typeof(bool), 
            typeof(EmailValidationBehavior), 
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
            _control.Message = "Email is not valid";
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
                IsValid = Validator.IsEmail(e.NewTextValue);

                _control.IsMessageVisible = !IsValid;
            }   
        }
        #endregion
    }
}
