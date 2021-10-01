using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "City Name")]
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}
