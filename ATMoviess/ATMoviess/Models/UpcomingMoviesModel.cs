using System;
using System.Collections.Generic;

namespace ATMoviess.Models
{
    public class UpcomingMoviesModel
    {
        public List<Movie> Results { get; set; }
        public int Page { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
    }

    public class Movie
    {
        private const string URL_TMDB_IMAGE_PATH = "http://image.tmdb.org/t/p/w500";
        private const string NO_POSTER_IMAGE = "noposter.png";
        private const string NO_BACKDROP_IMAGE = "nobackdrop.png";
        private const string NO_OVERVIEW_TEXT = "No overview found.";

        public int VoteCount { get; set; }
        public int Id { get; set; }
        public bool Video { get; set; }
        public double VoteAverage { get; set; }
        public string Title { get; set; }
        public double Popularity { get; set; }
        private string _posterPath;
        public string PosterPath
        {
            get
            {
                if (string.IsNullOrEmpty(_posterPath))
                    return NO_POSTER_IMAGE;

                return URL_TMDB_IMAGE_PATH + _posterPath;
            }
            set
            {
                _posterPath = value;
            }
        }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public List<int> GenreIds { get; set; }
        private string _backdropPath;
        public string BackdropPath { 
            get
            {
                if (string.IsNullOrEmpty(_backdropPath))
                {
                    if (PosterPath == NO_POSTER_IMAGE)
                        return NO_BACKDROP_IMAGE;
                    else
                        return PosterPath;
                }
                return URL_TMDB_IMAGE_PATH + _backdropPath;
            }
            set
            {
                _backdropPath = value;
            }
        }
        public bool Adult { get; set; }
        private string _overview { get; set; }
        public string Overview
        {
            get
            {
                if (string.IsNullOrEmpty(_overview))
                    return NO_OVERVIEW_TEXT;
                else
                    return _overview;
            }
            set
            {
                _overview = value;
            }
        }
        public string ReleaseDate { get; set; }
        public DateTime ReleaseDateConverted
        {
            get
            {
                try
                {
                    var convertedDate = Convert.ToDateTime(ReleaseDate);
                    return convertedDate;
                }
                catch (Exception)
                {
                    return DateTime.MinValue;
                }
            }
        }
        public string Genres { get; set; }
    }
}
