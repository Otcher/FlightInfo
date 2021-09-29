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
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z\s]*$")]
        [Required]
        [Display(Name = "Airport Name")]
        public string Name { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public IEnumerable<Flight> FlightTable { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
    }
}
