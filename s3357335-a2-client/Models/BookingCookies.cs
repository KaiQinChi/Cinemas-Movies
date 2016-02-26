using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace s3357335_a2_client.Models
{
    public class BookingCookies
    {
        public BookingCookies()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public BookingCookies(int sessionId, string title, int quantity, string location, DateTime sessionTime, string seatNO, double payTotal) : this()
        {
            this.sessionId = sessionId;
            this.quantity = quantity;
            this.location = location;
            this.title = title;
            this.sessionTime = sessionTime;
            this.seatNO = seatNO;
            this.payTotal = payTotal;
        }

        public string Id { get; set; }
        public int sessionId { get; set; }
        public int quantity { get; set; }
        public string location { get; set; }
        public string title { get; set; }
        public DateTime sessionTime { get; set; }
        public string seatNO { get; set; }
        public double payTotal { get; set; }
    }
}