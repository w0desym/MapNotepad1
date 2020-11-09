using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotepad.Behaviors
{
    class ListViewBehavior : BehaviorBase<ListView>
    {
        private ListView _listView;
        protected override void OnAttachedTo(ListView listView)
        {
            base.OnAttachedTo(listView);

            _listView = listView;

        }

        protected override void OnDetachingFrom(ListView listView)
        {
            base.OnDetachingFrom(listView);

            _listView = null;
        }
    }
}
