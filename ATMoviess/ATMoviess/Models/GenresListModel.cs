using System;
using System.Collections.Generic;
using System.Text;

namespace ATMoviess.Models
{
    public class GenresListModel
    {
        public List<Genre> Genres { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
