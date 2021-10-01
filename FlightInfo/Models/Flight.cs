using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Flight
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; }
        public Airport Origin { get; set; }
        
        public Airport Destination { get; set; }

        public Pilot Pilot { get; set; }

        public DateTime DepartureTime { get; set; }

        public IEnumerable<Passenger> PassengerManifest { get; set; }

        public Plane Plane { get; set; }
    }
}
