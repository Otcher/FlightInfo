using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Country Name")]
        public string Name { get; set; }
    }
}
