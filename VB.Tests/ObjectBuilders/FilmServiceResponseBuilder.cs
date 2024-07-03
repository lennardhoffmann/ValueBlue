﻿using ExternalFilmService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB.Tests.ObjectBuilders
{
    internal class FilmServiceResponseBuilder
    {
        public FilmServiceResponse Build()
        {
            return new FilmServiceResponse
            {
                Title = "Inception",
                Year = "2010",
                Rated = "PG-13",
                Released = "16 Jul 2010",
                Runtime = "148 min",
                Genre = "Action, Adventure, Sci-Fi",
                Director = "Christopher Nolan",
                Writer = "Christopher Nolan",
                Actors = "Leonardo DiCaprio, Joseph Gordon-Levitt, Elliot Page",
                Plot = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                Language = "English, Japanese, French",
                Country = "United States, United Kingdom",
                Awards = "Won 4 Oscars. 159 wins & 220 nominations total",
                Poster = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SX300.jpg",
                imdbID = "tt1375666",
                imdbRating = "8.8",
                imdbVotes = "2,564,438",
                Type = "movie"
            };
        }
    }
}
