using SQLite;

namespace MapNotepad.Models
{
    public class PinInfo : ICommonModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsFavorite { get; set; }
        public string ImgPath { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }
    }
}
