using MapNotepad.Views;
using Prism.Behaviors;
using Prism.Common;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotepad.Behaviors
{
    public class TabbedPageNavigationBehavior : BehaviorBase<TabbedPage>
    {
        private Page _currentPage;

        #region -- Overrides --

        protected override void OnAttachedTo(TabbedPage bindable)
        {
            bindable.CurrentPageChanged += this.OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            bindable.CurrentPageChanged -= this.OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region -- Private Helper --

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            var newPage = this.AssociatedObject.CurrentPage;

            if (this._currentPage != null)
            {
                var parameters = new NavigationParameters();
                PageUtilities.OnNavigatedFrom(this._currentPage, parameters);
                PageUtilities.OnNavigatedTo(newPage, parameters);
            }
            this._currentPage = newPage;
        }

        #endregion
    }
}
