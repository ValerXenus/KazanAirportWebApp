//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KazanAirportWebApp.Models.Data_Access
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flights
    {
        public int id { get; set; }
        public string flightNumber { get; set; }
        public System.DateTime departureScheduled { get; set; }
        public System.DateTime arrivalScheduled { get; set; }
        public System.DateTime departureActual { get; set; }
        public System.DateTime arrivalActual { get; set; }
        public int timeOnBoard { get; set; }
        public int flightType { get; set; }
        public int planeId { get; set; }
        public int cityId { get; set; }
        public int statusId { get; set; }
    }
}
