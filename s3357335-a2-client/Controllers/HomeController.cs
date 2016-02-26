using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace s3357335_a2_client.Controllers
{
    public class HomeController : Controller
    {
        MovieEntities db = new MovieEntities();

        // select cinema location on home page
        public ActionResult Index(int cinemaId = 0)
        {
            if (cinemaId == 0)
            {
                // check session firstly when no cinema selected
                var cinemaID = Session["cinemaID"];
                if (cinemaID != null)
                {
                    var movieIDs = from ms in db.MovieSessions
                                   where ms.CineplexID.Equals((int)cinemaID)
                                   select ms.MovieID;

                    var movies = from m in db.Movies
                                 where movieIDs.Contains(m.MovieID)
                                 select m;
                    ViewBag.movies = movies;
                }
                else
                {
                    var movieIDs = from ms in db.MovieSessions
                                   select ms.MovieID;

                    var movies = from m in db.Movies
                                 where movieIDs.Contains(m.MovieID)
                                 select m;
                    ViewBag.movies = movies;
                    Session["cinemaLocation"] = "";
                }
            }
            else
            {
                var movieIDs = from ms in db.MovieSessions
                               where ms.CineplexID.Equals(cinemaId)
                               select ms.MovieID;

                var movies = from m in db.Movies
                             where movieIDs.Contains(m.MovieID)
                             select m;
                ViewBag.movies = movies;

                var location = from c in db.Cineplexes
                               where c.CineplexID.Equals(cinemaId)
                               select c.Location;
                Session["cinemaID"] = cinemaId;
                Session["cinemaLocation"] = " At " + (String)location.FirstOrDefault();
            }

            var comingMovies = from m in db.MovieComingSoons
                               select m;
            ViewBag.comingMovies = comingMovies;

            return View();
        }

        // search movie title on home page
        [HttpPost]
        public ActionResult Index(string movieTitle = "")
        {
            // check session firstly when no cinema selected
            var cinemaID = Session["cinemaID"];

            if (cinemaID != null)
            {
                var movieIDs = from ms in db.MovieSessions
                               join m in db.Movies on ms.MovieID equals m.MovieID
                               join c in db.Cineplexes on ms.CineplexID equals c.CineplexID
                               where c.CineplexID.Equals((int)cinemaID) && m.Title.Contains(movieTitle)
                               select m.MovieID;

                var movies = from m in db.Movies
                             where movieIDs.Contains(m.MovieID)
                             select m;
                ViewBag.movies = movies;
            }
            else
            {
                var movieIDs = from ms in db.MovieSessions
                               join m in db.Movies on ms.MovieID equals m.MovieID
                               where m.Title.Contains(movieTitle)
                               select m.MovieID;

                var movies = from m in db.Movies
                             where movieIDs.Contains(m.MovieID)
                             select m;
                ViewBag.movies = movies;
            }

            var comingMovies = from m in db.MovieComingSoons
                               select m;
            ViewBag.comingMovies = comingMovies;

            return View();
        }

        // select cinema location on movie page
        public ActionResult Movie(int cinemaId = 0)
        {
            if (cinemaId == 0)
            {
                // check session firstly when no cinema selected
                var cinemaID = Session["cinemaID"];
                if (cinemaID != null)
                {
                    var movieIDs = from ms in db.MovieSessions
                                   where ms.CineplexID.Equals((int)cinemaID)
                                   select ms.MovieID;

                    var movies = from m in db.Movies
                                 where movieIDs.Contains(m.MovieID)
                                 select m;
                    ViewBag.movies = movies;
                }
                else
                {
                    var movieIDs = from ms in db.MovieSessions
                                   select ms.MovieID;

                    var movies = from m in db.Movies
                                 where movieIDs.Contains(m.MovieID)
                                 select m;
                    ViewBag.movies = movies;
                    Session["cinemaLocation"] = "";
                }
            }
            else
            {
                var movieIDs = from ms in db.MovieSessions
                               where ms.CineplexID.Equals(cinemaId)
                               select ms.MovieID;

                var movies = from m in db.Movies
                             where movieIDs.Contains(m.MovieID)
                             select m;
                ViewBag.movies = movies;

                var location = from c in db.Cineplexes
                               where c.CineplexID.Equals(cinemaId)
                               select c.Location;
                Session["cinemaID"] = cinemaId;
                Session["cinemaLocation"] = " At " + (String)location.FirstOrDefault();
            }

            var comingMovies = from m in db.MovieComingSoons
                               select m;
            ViewBag.comingMovies = comingMovies;
            return View();
        }

        // search movie title on movie page
        [HttpPost]
        public ActionResult Movie(string movieTitle = "")
        {
            // check session firstly when no cinema selected
            var cinemaID = Session["cinemaID"];

            if (cinemaID != null)
            {
                var movieIDs = from ms in db.MovieSessions
                               join m in db.Movies on ms.MovieID equals m.MovieID
                               join c in db.Cineplexes on ms.CineplexID equals c.CineplexID
                               where c.CineplexID.Equals((int)cinemaID) && m.Title.Contains(movieTitle)
                               select m.MovieID;

                var movies = from m in db.Movies
                             where movieIDs.Contains(m.MovieID)
                             select m;
                ViewBag.movies = movies;
            }
            else
            {
                var movieIDs = from ms in db.MovieSessions
                               join m in db.Movies on ms.MovieID equals m.MovieID
                               where m.Title.Contains(movieTitle)
                               select m.MovieID;

                var movies = from m in db.Movies
                             where movieIDs.Contains(m.MovieID)
                             select m;
                ViewBag.movies = movies;
            }

            var comingMovies = from m in db.MovieComingSoons
                               select m;
            ViewBag.comingMovies = comingMovies;

            return View();
        }

        // go to cinema page
        public ActionResult Cinema(int cinemaId = 0)
        {
            Cineplex cineplex;
            if (cinemaId == 0)
            {
                var cinemaID = Session["cinemaID"];
                // read cinemaId in session firstly
                if (cinemaID != null)
                {
                    cinemaId = (int)cinemaID;
                }
                else
                {
                    cinemaId = 1;// default cinema
                }
            }
            // cinemaId != 0 means a location had been selected
            else
            { 
                var location = from c in db.Cineplexes
                               where c.CineplexID.Equals(cinemaId)
                               select c.Location;
                // save location
                Session["cinemaID"] = cinemaId;
                Session["cinemaLocation"] = " At "+(String)location.FirstOrDefault();
            }

            cineplex = db.Cineplexes.Find(cinemaId);

            var movieIDs = from ms in db.MovieSessions
                           where ms.CineplexID.Equals(cinemaId)
                           select ms.MovieID;

            var movies = from m in db.Movies
                         where movieIDs.Contains(m.MovieID)
                         select m;

            ViewBag.movies = movies;

            var comingMovies = from m in db.MovieComingSoons
                               select m;
            ViewBag.comingMovies = comingMovies;

            if (cineplex == null)
            {
                return HttpNotFound();
            }

            return View(cineplex);
        }

        // search movie in cinema page
        [HttpPost]
        public ActionResult Cinema(int cinemaId = 1, string movieTitle = "")
        {
            Cineplex cineplex = db.Cineplexes.Find(cinemaId);

            var movieIDs = from ms in db.MovieSessions
                           join m in db.Movies on ms.MovieID equals m.MovieID
                           where ms.CineplexID.Equals(cinemaId) && m.Title.Contains(movieTitle)
                           select m.MovieID;

            var movies = from m in db.Movies
                         where movieIDs.Contains(m.MovieID)
                         select m;

            ViewBag.movies = movies;

            var comingMovies = from m in db.MovieComingSoons
                               select m;
            ViewBag.comingMovies = comingMovies;

            if (cineplex == null)
            {
                return HttpNotFound();
            }

            return View(cineplex);
        }
    }
}
