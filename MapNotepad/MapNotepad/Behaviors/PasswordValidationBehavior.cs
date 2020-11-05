using MapNotepad.Controls;
using MapNotepad.Validators;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MapNotepad.Behaviors
{
    class PasswordValidationBehavior : BehaviorBase<ValidationEntryControl>
    {
        private ValidationEntryControl _control;

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid", 
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

        protected override void OnAttachedTo(ValidationEntryControl control)
        {
            base.OnAttachedTo(control);

            _control = control;
            control.Entry.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(ValidationEntryControl control)
        {
            base.OnDetachingFrom(control);

            _control = null;
            control.Entry.TextChanged -= OnTextChanged;
        }

        #endregion

        #region -- Private Helper --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 8 || e.NewTextValue.Length > 16)
            {
                _control.IsMessageVisible = true;
                _control.Message = "Should be between 8 and 16 characters long";
            }
            else
            {
                IsValid = Validator.MatchesRequirements(e.NewTextValue);

                if (!IsValid)
                {
                    _control.IsMessageVisible = true;
                    _control.Message = "At least 1 lowercase, uppercase and number";
                }
                else
                {
                    _control.IsMessageVisible = false;
                }
            }
        }

        #endregion
    }
}
