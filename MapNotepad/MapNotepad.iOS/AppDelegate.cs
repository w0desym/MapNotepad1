using System;
using System.Collections.Generic;
using System.Linq;

using Plugin.GoogleClient;
using Foundation;
using MapNotepad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using System.Threading.Tasks;
using ContextMenu.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace MapNotepad.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        #region -- Overrides --

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            GoogleClientManager.Initialize();
            Xamarin.FormsGoogleMaps.Init("AIzaSyAM5sdKD8GcVjBlhiLlaVrpHkdICoNWPPg");
            ContextMenuViewRenderer.Preserve();
            Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App());
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options) => GoogleClientManager.OnOpenUrl(app, url, options);

        #endregion
    }
}
