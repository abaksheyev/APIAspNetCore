using System;

namespace APIAspNetCore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}