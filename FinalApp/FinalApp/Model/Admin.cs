
using Root.Services.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalApp.Model
{
   public class Admin : IBaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
