using System;

namespace BookCatalogAPI.Models
{
    public class Book
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Owner Owner { get; set; }
    }

    public class Owner
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
