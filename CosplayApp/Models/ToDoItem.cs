﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosplayApp
{
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Done { get; set; }
        public int CosplayCardId { get; set; }
    }
}
