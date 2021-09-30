using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Airport
    {
        public int Id { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z\s]*$")]
        [Required]
        [Display(Name = "Airport Name")]
        public string Name { get; set; }

        public City City { get; set; }

        public IEnumerable<Flight> FlightTable { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
    }
}
