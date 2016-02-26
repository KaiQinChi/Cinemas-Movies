using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace s3357335_a2_client.Controllers
{
    public class EventController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET api/Event
        public IEnumerable<CorporateEvent> GetCorporateEvents()
        {
            return db.CorporateEvents.AsEnumerable();
        }

        // go to event page
        public ActionResult Index(string mes = null)
        {
            var events = from e in db.CorporateEvents
                         select e;
            ViewBag.events = events;
            ViewBag.mes = mes;

            return View();
        }

        //
        // POST: /Enquiry/Create

        [HttpPost]
        public ActionResult Index(string email, string message)
        {
            db.Enquiries.Add(new Enquiry { Email = email, Message = message });
            db.SaveChanges();

            string mes = "Message was sent!";
            return RedirectToAction("Index", new { mes = mes });
        }

        // GET api/Event/5
        public ActionResult ViewEvent(int eventId)
        {
            CorporateEvent corporateEvent = db.CorporateEvents.Find(eventId);
            if (corporateEvent == null)
            {
                return HttpNotFound();
            }

            return View(corporateEvent);
        }
    }
}