using System;
using System.Collections.Generic;
using System.Text;
using SL;
using SQLite;

namespace FinalApp.Model
{
    public class Employee: IBaseTable
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
    }
}
