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
    class ConfirmPasswordValidationBehavior : BehaviorBase<FrameLabelEntryControl>
    {
        private FrameLabelEntryControl _control;

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid", 
            typeof(bool), 
            typeof(ConfirmPasswordValidationBehavior), 
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create(
            propertyName: nameof(CompareToEntry),
            returnType: typeof(string),
            declaringType: typeof(FrameLabelEntryControl));

        public string CompareToEntry
        {
            get => (string)GetValue(CompareToEntryProperty);
            set => SetValue(CompareToEntryProperty, value);
        }

        #region -- Overrides --

        protected override void OnAttachedTo(FrameLabelEntryControl control)
        {
            base.OnAttachedTo(control);

            _control = control;
            control.Entry.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(FrameLabelEntryControl control)
        {
            base.OnDetachingFrom(control);

            _control = null;
            control.Entry.TextChanged -= OnTextChanged;
        }

        #endregion

        #region -- Private Helper --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (CompareToEntry != null)
            {
                var password = CompareToEntry;
                var confirmPassword = e.NewTextValue;

                IsValid = password.Equals(confirmPassword);

                if (!IsValid)
                {
                    _control.IsMessageVisible = true;
                    _control.Message = "Passwords should match";
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
