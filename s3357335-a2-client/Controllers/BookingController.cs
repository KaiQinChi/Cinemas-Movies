using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using s3357335_a2_client.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using s3357335_a2_client.Utilities;

namespace s3357335_a2_client.Controllers
{
    public class BookingController : Controller
    {
        private MovieEntities db = new MovieEntities();

        //
        // GET: /Booking/

        public ActionResult Index()
        {
            // check the seats booked in the Cookie, but not pay yet currently.----------
            List<BookingCookies> bookingCookiesList = new List<BookingCookies>();
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);

            // read and store current booking item infor to cookie.
            if (cookie != null)
            {
                bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
            }

            ViewBag.bookingCookiesList = bookingCookiesList;

            // The default card type selected is master card.
            return View(new CreditCardForm { CreditCardType = CardType.MasterCard });
        }

        //
        // POST: /Booking/Payment

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CreditCardForm creditCardForm)
        {
            // check the seats booked in the Cookie, but not pay yet currently. 
            List<BookingCookies> bookingCookiesList = new List<BookingCookies>();
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);

            // read and store current booking item infor to cookie.
            if (cookie != null)
            {
                bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
            }

            // Validate card type.
            CardType expectedCardType = CardTypeInfo.GetCardType(creditCardForm.CreditCardNumber);
            if (expectedCardType == CardType.Unknown || expectedCardType != creditCardForm.CreditCardType)
            {
                Debug.WriteLine("expectedCardType");
                ModelState.AddModelError("CreditCardType", "The Credit Card Type field does not match against the credit card number.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.bookingCookiesList = bookingCookiesList;
                return View(creditCardForm);
            }

            // start insert booking information from bookingCookies List ------------
            int ticketNum = 0;
            double totalMoney = 0;
            Booking book = db.Bookings.Add(new Booking { CustomerEmail = creditCardForm.Email});
            db.SaveChanges();

            foreach (var bookingCookie in bookingCookiesList)
            {
                totalMoney += bookingCookie.payTotal;
                //the seatNo string format like "2,7|9"
                string[] seats = bookingCookie.seatNO.Split('|');
                foreach (string s in seats)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                    {
                        //insert each BookingItem
                        int seatNum = Int32.Parse(s);
                        db.BookingItems.Add(new BookingItem { BookingID = book.BookingID, SessionID = bookingCookie.sessionId, SeatNum = seatNum });
                        db.SaveChanges();
                        ticketNum++;
                    }
                }
            }

            // update booking final total price
            book.TicketNum = ticketNum;
            book.TotalPrice = (decimal)totalMoney;
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Payment");
        }

        //
        // GET: /Booking/Payment

        public ActionResult Payment()
        {
            // delete bookingCookie 
            MyUtils.expireCookie("bookingCookiesList", this);
            return View();
        }

        //
        // GET: /Booking/Create new item

        public ActionResult Create(int sessionId = 0)
        {
            MovieSession session = db.MovieSessions.Find(sessionId);

            // check the seats booked in the DBase, those were paid before.---------
            var bookedItems = from bi in db.BookingItems
                              where bi.SessionID.Equals(sessionId)
                              select bi;
            List<string> bookedSeats = new List<string>();

            // generate the strings for showing seat selected on website
            // like 1_2 indicates first line - second seat 
            foreach (BookingItem bi in bookedItems)
            {
                // which line which colum in cinema 5*4 cells
                int pre = bi.SeatNum / 5 + 1;
                int remainder = bi.SeatNum % 5;
                if ((pre - 1) > 0 && remainder == 0)
                    pre--;
                remainder = (remainder == 0) ? 5 : remainder;
                string seat = "" + pre + "_" + remainder;
                bookedSeats.Add(seat);
            }

            // check the seats booked in the Cookie, but not pay yet currently.----------
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);

            // read and store current booking item infor to cookie.
            if (cookie != null)
            {
                List<BookingCookies> bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
                foreach (BookingCookies bc in bookingCookiesList)
                {
                    // check the sessions have same ID 
                    if (bc.sessionId.Equals(sessionId))
                    {
                        //the seatNo string format like "2,7|9"
                        string[] seats = bc.seatNO.Split('|');
                        foreach (string s in seats)
                        {
                            if (!String.IsNullOrWhiteSpace(s))
                            {
                                Debug.WriteLine(s);
                                int seatNum = Int32.Parse(s);
                                // which line which colum in cinema 5*4 cells
                                int pre = seatNum / 5 + 1;
                                int remainder = seatNum % 5;
                                if ((pre - 1) > 0 && remainder == 0)
                                {
                                    pre--;
                                }
                                remainder = (remainder == 0) ? 5 : remainder;
                                string seat = "" + pre + "_" + remainder;
                                Debug.WriteLine(seat);
                                bookedSeats.Add(seat);
                            }
                        }
                    }
                }
            }

            ViewBag.bookedSeats = bookedSeats;

            return View(session);
        }

        //
        // POST: /Booking/Create

        [HttpPost]
        public ActionResult Create(int sessionId = 0, int quantity = 0, string seatNO = "")
        {
            MovieSession session = db.MovieSessions.Find(sessionId);
            string title = session.Movie.Title;
            string location = session.Cineplex.Location;
            DateTime sessionTime = session.ShowTime;
            double payTotal = (int)session.Price * quantity;

            BookingCookies bookingCookie = new BookingCookies(sessionId, title, quantity, location, sessionTime, seatNO, payTotal);
            List<BookingCookies> bookingCookiesList = new List<BookingCookies>();
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);

            // read and add current booking item infor to cookie
            if (cookie != null)
            {
                bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
                bookingCookiesList.Add(bookingCookie);
                cookie = new HttpCookie("bookingCookiesList");
                cookie.Value = JsonConvert.SerializeObject(bookingCookiesList);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            else
            {
                cookie = new HttpCookie("bookingCookiesList");
                bookingCookiesList.Add(bookingCookie);
                cookie.Value = JsonConvert.SerializeObject(bookingCookiesList);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Booking/Edit/5

        public ActionResult Edit(string bookingCookieId = "")
        {
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);
            List<BookingCookies> bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
            // find the item
            BookingCookies bookingItem = bookingCookiesList.Find(x => x.Id.Equals(bookingCookieId));

            MovieSession session = db.MovieSessions.Find(bookingItem.sessionId);

            List<string> bookedSeats = new List<string>();

            // selected Seats in this item needs editing
            List<string> selectSeats = new List<string>();

            // check the seats booked in the DBase, those were paid before.------------- 
            var bookedItems = from bi in db.BookingItems
                              where bi.SessionID.Equals(bookingItem.sessionId)
                              select bi;

            // generate the strings for showing seat selected on website
            // like 1_2 indicates first line - second seat 
            foreach (BookingItem bi in bookedItems)
            {
                // which line which colum in cinema 5*4 cells
                int pre = bi.SeatNum / 5 + 1;
                int remainder = bi.SeatNum % 5;
                if ((pre - 1) > 0 && remainder == 0)
                    pre--;
                remainder = (remainder == 0) ? 5 : remainder;
                string seat = "" + pre + "_" + remainder;
                bookedSeats.Add(seat);
            }

            // read seats stored in current booking items in Cookie.------------- 
            if (cookie != null)
            {
                foreach (BookingCookies bc in bookingCookiesList)
                {
                    // check the booked sessions have same ID, But not itself (it need editing)
                    if (bc.sessionId.Equals(bookingItem.sessionId))
                    {
                        //the seatNo string format like "2,7|9"
                        string[] seats = bc.seatNO.Split('|');
                        foreach (string s in seats)
                        {
                            if (!String.IsNullOrWhiteSpace(s))
                            {
                                Debug.WriteLine(s);
                                int seatNum = Int32.Parse(s);
                                // which line which colum in cinema 5*4 cells
                                int pre = seatNum / 5 + 1;
                                int remainder = seatNum % 5;
                                if ((pre - 1) > 0 && remainder == 0)
                                {
                                    pre--;
                                }
                                remainder = (remainder == 0) ? 5 : remainder;
                                string seat = "" + pre + "_" + remainder;
                                //Debug.WriteLine(seat);
                                // But not the item needs editing
                                if (bc.Id.Equals(bookingCookieId))
                                    selectSeats.Add(seat);
                                else
                                    bookedSeats.Add(seat);
                            }
                        }
                    }
                }
            }

            ViewBag.selectSeats = selectSeats;
            ViewBag.bookedSeats = bookedSeats;
            ViewBag.bookingItem = bookingItem;

            return View(session);
        }

        //
        // POST: /Booking/Edit/5

        [HttpPost]
        public ActionResult Edit(string bookingCookieId, int quantity = 0, string seatNO = "", double payTotal = 0)
        {
            List<BookingCookies> bookingCookiesList = new List<BookingCookies>();
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);

            // read and Delete current booking item infor to cookie
            if (cookie != null)
            {
                bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
                BookingCookies item = bookingCookiesList.Find(x => x.Id.Equals(bookingCookieId));
                // update the values
                item.quantity = quantity;
                item.seatNO = seatNO;
                item.payTotal = payTotal;

                cookie = new HttpCookie("bookingCookiesList");
                cookie.Value = JsonConvert.SerializeObject(bookingCookiesList);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Booking/Delete/5

        public ActionResult Delete(string bookingCookieId = "")
        {
            List<BookingCookies> bookingCookiesList = new List<BookingCookies>();
            HttpCookie cookie = MyUtils.findCookie("bookingCookiesList", this);

            // read and Delete current booking item infor to cookie
            if (cookie != null)
            {
                bookingCookiesList = JsonConvert.DeserializeObject<List<BookingCookies>>(cookie.Value);
                var index = bookingCookiesList.FindIndex(x => x.Id.Equals(bookingCookieId));
                bookingCookiesList.RemoveAt(index);

                cookie = new HttpCookie("bookingCookiesList");
                cookie.Value = JsonConvert.SerializeObject(bookingCookiesList);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index");
        }

        // view session of a movie in one cinema
        public ActionResult ViewSession(int movieId, int cinemaId = 0)
        {
            if (cinemaId == 0)
            {
                var cinemaID = Session["cinemaID"];

                if (cinemaID != null)
                {
                    var sessions = from ms in db.MovieSessions
                                   join c in db.Cineplexes on ms.CineplexID equals c.CineplexID
                                   where ms.CineplexID.Equals((int)cinemaID) && ms.MovieID.Equals(movieId)
                                   select ms;

                    ViewBag.sessions = sessions;
                }
                else
                {
                    var sessions = from ms in db.MovieSessions
                                   join c in db.Cineplexes on ms.CineplexID equals c.CineplexID
                                   where ms.MovieID.Equals(movieId)
                                   select ms;

                    ViewBag.sessions = sessions;
                }
            }
            // select a location for a movie's sessions
            else
            {
                var sessions = from ms in db.MovieSessions
                               join c in db.Cineplexes on ms.CineplexID equals c.CineplexID
                               where ms.CineplexID.Equals(cinemaId) && ms.MovieID.Equals(movieId)
                               select ms;

                ViewBag.sessions = sessions;

                var location = from c in db.Cineplexes
                               where c.CineplexID.Equals(cinemaId)
                               select c.Location;

                // save location
                Session["cinemaID"] = cinemaId;
                Session["cinemaLocation"] = " At " + (String)location.FirstOrDefault();

            }

            Movie movie = db.Movies.Find(movieId);

            return View(movie);
        }
    }
}