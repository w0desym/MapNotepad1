namespace MapNotepad.Services
{
    public interface ISettingsManager
    {
        int CurrentUser { get; set; }
        double Zoom { get; set; }
        double LastLatitude { get; set; }
        double LastLongitude { get; set; }
    }
}
