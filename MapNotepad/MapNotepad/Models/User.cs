using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Models
{
    public class User : ICommonModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
