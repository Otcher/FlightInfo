using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Passenger: Person
    {
        public List<Flight> FlightHistory { get; set; }
    }
}
