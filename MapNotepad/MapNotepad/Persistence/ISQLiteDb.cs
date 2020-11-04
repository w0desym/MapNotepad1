using SQLite;

namespace MapNotepad
{
    public interface ISQLiteDb
    {
        SQLiteConnection GetConnection();
    }
}
