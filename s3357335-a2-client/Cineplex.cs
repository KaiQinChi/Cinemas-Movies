//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace s3357335_a2_client
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cineplex
    {
        public Cineplex()
        {
            this.MovieSessions = new HashSet<MovieSession>();
        }
    
        public int CineplexID { get; set; }
        public string Location { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
    
        public virtual ICollection<MovieSession> MovieSessions { get; set; }
    }
}
