using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapNotepad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MapNotepad.Controls;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace MapNotepad.iOS.Renderers
{
    class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}