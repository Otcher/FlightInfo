using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Passenger: Person
    {
        public IEnumerable<Flight> FlightHistory { get; set; }
    }
}
