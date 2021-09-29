﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Pilot: Person
    {
        public IEnumerable<Plane> Qualification { get; set; }
        public IEnumerable<Flight> FlightHistory { get; set; }
    }
}
