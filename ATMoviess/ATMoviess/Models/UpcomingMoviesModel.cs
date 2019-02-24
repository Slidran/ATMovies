using System;
using System.Collections.Generic;

namespace ATMoviess.Models
{
    public class UpcomingMoviesModel
    {
        public List<Result> Results { get; set; }
        public int Page { get; set; }
        public int Total_results { get; set; }
        public Dates Dates { get; set; }
        public int Total_pages { get; set; }
    }

    public class Result
    {
        public int Vote_count { get; set; }
        public int Id { get; set; }
        public bool Video { get; set; }
        public double Vote_average { get; set; }
        public string Title { get; set; }
        public double Popularity { get; set; }
        private string _poster_path;
        public string Poster_path
        {
            get
            {
                if (string.IsNullOrEmpty(_poster_path))
                    return "noposter.png";

                return "http://image.tmdb.org/t/p/w500" + _poster_path;
            }
            set
            {
                _poster_path = value;
            }
        }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public List<int> Genre_ids { get; set; }
        private string _backdrop_path;
        public string Backdrop_path { 
            get
            {
                if (string.IsNullOrEmpty(_backdrop_path))
                {
                    if (Poster_path == "noposter.png")
                        return "nobackdrop.png";
                    else
                        return Poster_path;
                }
                return "http://image.tmdb.org/t/p/w500" + _backdrop_path;
            }
            set
            {
                _backdrop_path = value;
            }
        }
        public bool Adult { get; set; }
        private string _overview { get; set; }
        public string Overview
        {
            get
            {
                if (string.IsNullOrEmpty(_overview))
                    return "No overview found.";
                else
                    return _overview;
            }
            set
            {
                _overview = value;
            }
        }
        public string Release_date { get; set; }
        public DateTime ReleaseDate
        {
            get
            {
                try
                {
                    var convertedDate = Convert.ToDateTime(Release_date);
                    return convertedDate;
                }
                catch (Exception ex)
                {
                    return DateTime.MinValue;
                }
            }
        }
        public string Genres { get; set; }
    }

    public class Dates
    {
        public string Maximum { get; set; }
        public string Minimum { get; set; }
    }
}
