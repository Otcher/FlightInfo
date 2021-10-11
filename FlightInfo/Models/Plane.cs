using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Plane
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int CruiseSpeed { get; set; }

        public List<Pilot> Pilots { get; set; }

    }
}
