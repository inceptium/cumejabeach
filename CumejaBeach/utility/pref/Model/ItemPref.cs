using System;
using SQLite;

namespace CumejaBeach.utility.pref.Model
{
    public class ItemPref
    {
   

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public string Symbol { get; internal set; }
    }
}
