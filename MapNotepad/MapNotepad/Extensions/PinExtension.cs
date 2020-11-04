using MapNotepad.Enums;
using MapNotepad.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Extensions
{
    public static class PinExtension
    {
        public static List<Pin> ToPins(this List<PinInfo> pinInfos)
        {
            List<Pin> pins = new List<Pin>();
            foreach(var pinInfo in pinInfos)
            {
                pins.Add(pinInfo.ToPin());
            }
            return pins;
        }
        public static Pin ToPin(this PinInfo pinInfo)
        {
            object other = new[] { pinInfo.Description, pinInfo.ImgPath };
            Pin pin = new Pin()
            {
                Label = pinInfo.Label,
                Position = new Position(pinInfo.Latitude, pinInfo.Longitude),
                Tag = other
            };
            return pin;
        }
        public static PinInfo ToPinInfo(this Pin pin)
        {
            string[] str = ((IEnumerable)pin.Tag).Cast<object>().Select(x => x.ToString()).ToArray();
            PinInfo pinInfo = new PinInfo()
            {
                Label = pin.Label,
                Latitude = pin.Position.Latitude,
                Longitude = pin.Position.Longitude,
                Description = str[(int)OtherPinInfo.Description],
                ImgPath = str[(int)OtherPinInfo.ImgPath]
            };
            return pinInfo;
        }
    }
}
