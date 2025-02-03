using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace CosplayApp
{
    public class CosplayCard
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? MainImage { get; set; }
        public bool Done { get; set; }
    }

}